using Mine2d.engine;
using Mine2d.game.core;

namespace Mine2d.game.frontend.renderer;

public class WorldCursorRenderer : IRenderer
{
    public void Render()
    {
        var ctx = Context.Get();
        var scale = ctx.FrontendGameState.Settings.GameScale;
        var camera = ctx.FrontendGameState.Camera;
        var absoluteMousePos = ctx.FrontendGameState.MousePosition / ctx.FrontendGameState.Settings.GameScale + camera.Position;
        if (PlayerEntity.GetSelf() == null || (absoluteMousePos - PlayerEntity.GetSelf().GetCenter()).LengthSquared() > Constants.BreakDistance * Constants.BreakDistance)
        {
            return;
        }

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
