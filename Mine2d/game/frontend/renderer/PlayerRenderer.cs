using Mine2d.engine;

namespace Mine2d.game.frontend.renderer;

public class PlayerRenderer : IRenderer
{
    private IntPtr playerTexture;

    public void Render()
    {
        if(this.playerTexture == IntPtr.Zero)
        {
            this.playerTexture = Context.Get().TextureFactory.LoadTexture("character.character");
        }
        var ctx = Context.Get();
        var camera = ctx.FrontendGameState.Camera;
        var scale = ctx.FrontendGameState.Settings.GameScale;
        var (width, height) = ctx.Window.GetSize();
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

            // ctx.Renderer.DrawRect(
            //     (player.Position.X - (int)camera.Position.X) * scale,
            //     (player.Position.Y - (int)camera.Position.Y) * scale - 28 * scale,
            //     14 * scale,
            //     28 * scale
            // );
            ctx.Renderer.DrawTexture(
                this.playerTexture,
                width / 2,
                (height / 2) - (31 * scale),
                16 * scale,
                32 * scale
            );
        }
    }
}
