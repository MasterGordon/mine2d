using mine2d.backend.data;
using mine2d.core;
using mine2d.core.data;
using mine2d.frontend.renderer;

namespace mine2d.frontend;

class Frontend : IFrontend
{
    public void Init()
    {
        var ctx = Context.Get();
        ctx.FrontendGameState.PlayerName = ctx.IsHost ? "Host" : "Client";
        var guid = Guid.NewGuid();
        ctx.FrontendGameState.PlayerGuid = guid;
        var connectPacket = new ConnectPacket(ctx.FrontendGameState.PlayerName, guid);
        ctx.Backend.ProcessPacket(connectPacket);
        ctx.TileRegistry.RegisterTile();
        var (width, height) = ctx.Window.GetSize();
        ctx.FrontendGameState.WindowWidth = width;
        ctx.FrontendGameState.WindowHeight = height;
    }

    public void Process()
    {
        var ctx = Context.Get();
        while (SDL_PollEvent(out var e) != 0)
        {
            if (e.type == SDL_EventType.SDL_QUIT)
            {
                Environment.Exit(0);
            }
            if (e.type == SDL_EventType.SDL_WINDOWEVENT)
            {
                if (e.window.windowEvent == SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED)
                {
                    ctx.FrontendGameState.WindowWidth = e.window.data1;
                    ctx.FrontendGameState.WindowHeight = e.window.data2;
                    Console.WriteLine($"Window resized to {e.window.data1}x{e.window.data2}");
                    var player = ctx.GameState.Players.Find(
                        p => p.Guid == ctx.FrontendGameState.PlayerGuid
                    );
                    ctx.FrontendGameState.Camera.CenterOn(player.Position);
                }
            }
            if (e.type == SDL_EventType.SDL_MOUSEMOTION)
            {
                var mousePos = new Vector2(e.motion.x, e.motion.y);
                ctx.FrontendGameState.MousePosition = mousePos;
                if (ctx.GameState.Players.Find(player => player.Guid == ctx.FrontendGameState.PlayerGuid).Mining != Vector2.Zero)
                {
                    var amp = ctx.FrontendGameState.MousePosition / ctx.FrontendGameState.Settings.GameScale + ctx.FrontendGameState.Camera.Position;
                    ctx.Backend.ProcessPacket(new BreakPacket(ctx.FrontendGameState.PlayerGuid, amp));
                }
            }
            if (e.type == SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL_BUTTON_LEFT)
            {
                var amp = ctx.FrontendGameState.MousePosition / ctx.FrontendGameState.Settings.GameScale + ctx.FrontendGameState.Camera.Position;
                ctx.Backend.ProcessPacket(new BreakPacket(ctx.FrontendGameState.PlayerGuid, amp));
            }
            if (e.type == SDL_EventType.SDL_MOUSEBUTTONUP && e.button.button == SDL_BUTTON_LEFT)
            {
                ctx.Backend.ProcessPacket(new BreakPacket(ctx.FrontendGameState.PlayerGuid, Vector2.Zero));
            }
            if (e.type == SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0)
            {
                var movementInput = ctx.FrontendGameState.MovementInput;
                if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_F11)
                {
                    if (!ctx.FrontendGameState.Settings.Fullscreen)
                    {
                        _ = SDL_SetWindowFullscreen(
                            ctx.Window.GetRaw(),
                            (uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP
                        );
                    }
                    else
                    {
                        _ = SDL_SetWindowFullscreen(ctx.Window.GetRaw(), 0);
                    }
                    ctx.FrontendGameState.Settings.Fullscreen = !ctx.FrontendGameState.Settings.Fullscreen;
                }
                if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_A)
                {
                    movementInput.X -= 1;
                }
                if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_D)
                {
                    movementInput.X += 1;
                }
                if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_W)
                {
                    movementInput.Y -= 1;
                }
                if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_S)
                {
                    movementInput.Y += 1;
                }
                ctx.FrontendGameState.MovementInput = movementInput;
            }
            if (e.type == SDL_EventType.SDL_KEYUP && e.key.repeat == 0)
            {
                var movementInput = ctx.FrontendGameState.MovementInput;
                if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_A)
                {
                    movementInput.X += 1;
                }
                if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_D)
                {
                    movementInput.X -= 1;
                }
                if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_W)
                {
                    movementInput.Y += 1;
                }
                if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_S)
                {
                    movementInput.Y -= 1;
                }
                ctx.FrontendGameState.MovementInput = movementInput;
            }
            if (
                e.key.keysym.scancode
                    is SDL_Scancode.SDL_SCANCODE_A
                    or SDL_Scancode.SDL_SCANCODE_D
                    or SDL_Scancode.SDL_SCANCODE_W
                    or SDL_Scancode.SDL_SCANCODE_S
                && e.key.repeat == 0
                && e.type is SDL_EventType.SDL_KEYDOWN or SDL_EventType.SDL_KEYUP
            )
            {
                if (e.key.repeat == 1)
                {
                    continue;
                }

                var movement = ctx.FrontendGameState.MovementInput;
                if (movement.Length() > 0)
                {
                    movement = Vector2.Normalize(movement);
                }

                ctx.Backend.ProcessPacket(new MovePacket(ctx.FrontendGameState.PlayerName, movement));
            }
            if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_ESCAPE)
            {
                Environment.Exit(0);
            }
        }

        ctx.Renderer.Clear();
        var scale = ctx.FrontendGameState.Settings.GameScale;
        var camera = Context.Get().FrontendGameState.Camera;
        new WorldRenderer().Render();
        ctx.GameState.Players.ForEach(player =>
        {
            if (player.Name == ctx.FrontendGameState.PlayerName)
            {
                ctx.Renderer.SetColor(0, 0, 255);
            }
            else
            {
                ctx.Renderer.SetColor(255, 0, 0);
            }

            ctx.Renderer.DrawRect(
                (player.Position.X - (int)camera.Position.X) * scale,
                (player.Position.Y - (int)camera.Position.Y) * scale - 32 * scale,
                16 * scale,
                32 * scale
            );
        });
        var absoluteMousePos = ctx.FrontendGameState.MousePosition / ctx.FrontendGameState.Settings.GameScale + camera.Position;
        if (ctx.GameState.World.HasTileAt((int)absoluteMousePos.X, (int)absoluteMousePos.Y))
        {
            var a = Constants.TileSize;
            var tilePos = new Vector2(absoluteMousePos.X - absoluteMousePos.X % a, absoluteMousePos.Y - absoluteMousePos.Y % a);
            ctx.Renderer.SetColor(255, 255, 255);
            ctx.Renderer.DrawOutline(
                (int)tilePos.X * scale - (int)camera.Position.X * scale,
                (int)tilePos.Y * scale - (int)camera.Position.Y * scale,
                16 * scale,
                16 * scale
            );
        }

        ctx.Renderer.Present();
    }
}
