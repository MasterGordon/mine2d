using mine2d.backend.data;
using mine2d.core;
using mine2d.frontend.renderer;
using Mine2d.frontend;

namespace mine2d.frontend;

public class Frontend : IFrontend
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
        EventService.PollEvents();

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
