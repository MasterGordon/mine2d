using mine2d.backend.data;
using mine2d.frontend.renderer;
using Mine2d.frontend;
using Mine2d.frontend.renderer;

namespace mine2d.frontend;

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
