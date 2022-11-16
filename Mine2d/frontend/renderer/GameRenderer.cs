using Mine2d.frontend.renderer;

namespace Mine2d.frontend.renderer;

public class GameRenderer : IRenderer
{
    private readonly List<IRenderer> renderers = new();

    public GameRenderer()
    {
        this.renderers.Add(new WorldRenderer());
        this.renderers.Add(new PlayerRenderer());
        this.renderers.Add(new WorldCursorRenderer());
    }

    public void Render()
    {
        foreach (var renderer in this.renderers)
        {
            renderer.Render();
        }
    }
}
