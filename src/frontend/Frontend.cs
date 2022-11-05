class Frontend : IFrontend
{
    private string playerName = "Player";
    private bool fullscreen = false;

    public void Init()
    {
        var ctx = Context.Get();
        this.playerName = Context.Get().IsHost ? "Host" : "Client";
        var guid = Guid.NewGuid();
        ctx.FrontendGameState.PlayerGuid = guid;
        var connectPacket = new ConnectPacket(playerName, guid);
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
            if (e.type == SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0)
            {
                var movementInput = ctx.FrontendGameState.MovementInput;
                if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_F11)
                {
                    if (!fullscreen)
                    {
                        SDL_SetWindowFullscreen(
                            ctx.Window.GetRaw(),
                            (uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP
                        );
                    }
                    else
                    {
                        SDL_SetWindowFullscreen(ctx.Window.GetRaw(), 0);
                    }
                    fullscreen = !fullscreen;
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
                    continue;
                var movement = ctx.FrontendGameState.MovementInput;
                if (movement.Length() > 0)
                    movement = Vector2.Normalize(movement);
                ctx.Backend.ProcessPacket(new MovePacket(playerName, movement));
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
            if (player.Name == playerName)
                ctx.Renderer.SetColor(0, 0, 255, 255);
            else
                ctx.Renderer.SetColor(255, 0, 0, 255);
            ctx.Renderer.DrawRect(
                (player.Position.X - (int)camera.position.X) * scale,
                (player.Position.Y - (int)camera.position.Y) * scale - 32 * scale,
                16 * scale,
                32 * scale
            );
        });
        ctx.Renderer.Present();
    }
}
