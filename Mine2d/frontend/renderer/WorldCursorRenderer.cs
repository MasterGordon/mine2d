using mine2d;
using mine2d.core;
using mine2d.frontend.renderer;

namespace Mine2d.frontend.renderer;

public class WorldCursorRenderer : IRenderer
{
    public void Render()
    {
        var ctx = Context.Get();
        var scale = ctx.FrontendGameState.Settings.GameScale;
        var camera = ctx.FrontendGameState.Camera;
        var absoluteMousePos = ctx.FrontendGameState.MousePosition / ctx.FrontendGameState.Settings.GameScale + camera.Position;
        if (ctx.GameState.World.HasTileAt((int)absoluteMousePos.X, (int)absoluteMousePos.Y))
        {
            var ts = Constants.TileSize;
            var tilePos = new Vector2(absoluteMousePos.X - absoluteMousePos.X % ts, absoluteMousePos.Y - absoluteMousePos.Y % ts);
            ctx.Renderer.SetColor(255, 255, 255);
            ctx.Renderer.DrawOutline(
                (int)tilePos.X * scale - (int)camera.Position.X * scale,
                (int)tilePos.Y * scale - (int)camera.Position.Y * scale,
                16 * scale,
                16 * scale
            );
        }
    }
}
