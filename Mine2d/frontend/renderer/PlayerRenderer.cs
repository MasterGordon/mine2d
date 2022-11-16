using Mine2d;
using Mine2d.frontend.renderer;

namespace Mine2d.frontend.renderer;

public class PlayerRenderer : IRenderer
{
    public void Render()
    {
        var ctx = Context.Get();
        var camera = ctx.FrontendGameState.Camera;
        var scale = ctx.FrontendGameState.Settings.GameScale;
        foreach (var player in ctx.GameState.Players)
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
        }
    }
}
