using Mine2d.engine;

namespace Mine2d.game.frontend.renderer;

public class GameRenderer : IRenderer
{
    private readonly List<IRenderer> renderers = new();

    public GameRenderer()
    {
        this.renderers.Add(new BackgroundRenderer());
        this.renderers.Add(new WorldRenderer());
        this.renderers.Add(new PlayerRenderer());
        this.renderers.Add(new ItemEnitityRenderer());
        this.renderers.Add(new WorldCursorRenderer());
        this.renderers.Add(new HudRenderer());
        this.renderers.Add(new InventoryRenderer());
        this.renderers.Add(new TooltipRenderer());
    }

    public void Render()
    {
        Context.Get().FrontendGameState.Tooltip = null;
        foreach (var renderer in this.renderers)
        {
            renderer.Render();
        }
    }
}
