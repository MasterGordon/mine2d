using Mine2d.game;
using Mine2d.game.backend.data;
using Mine2d.game.frontend.renderer;

namespace Mine2d.engine.networking;

public class Frontend : IFrontend
{
    private IRenderer gameRenderer;

    public void Init()
    {
        var ctx = Context.Get();
        ctx.FrontendGameState.PlayerName = ctx.IsHost ? "Host" : "Client";
        var guid = Guid.NewGuid();
        ctx.FrontendGameState.PlayerGuid = guid;
        var connectPacket = new ConnectPacket(ctx.FrontendGameState.PlayerName, guid);
        ctx.Backend.ProcessPacket(connectPacket);
        ctx.TileRegistry.RegisterTile();
        ctx.ItemRegistry.RegisterItems();
        var (width, height) = ctx.Window.GetSize();
        ctx.FrontendGameState.WindowWidth = width;
        ctx.FrontendGameState.WindowHeight = height;
        this.gameRenderer = new GameRenderer();
    }

    public void Process()
    {
        var ctx = Context.Get();
        EventService.PollEvents();

        ctx.Renderer.Clear();
        this.gameRenderer.Render();

        ctx.Renderer.Present();
    }
}
