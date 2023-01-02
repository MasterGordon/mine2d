using Mine2d.engine;

namespace Mine2d.game.frontend.renderer;

public class PlayerRenderer : IRenderer
{
    private IntPtr playerTexture;

    public void Render()
    {
        if (this.playerTexture == IntPtr.Zero)
        {
            this.playerTexture = Context.Get().TextureFactory.LoadTexture("character.character");
        }
        var ctx = Context.Get();
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
            var y = player.PlayerMovementState.MovingRight ? 32 * 3 : 32 * 1;
            var moving = player.PlayerMovementState.CurrentVelocity.X != 0;
            var dt = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
            var x = 0;
            if(moving)
            {
                x = (int)((dt / 100) % 6) * 16;
                y += 32;
            }
            if(!player.PlayerMovementState.IsGrounded)
            {
                x = 0;
                y = player.PlayerMovementState.MovingRight ? 32 * 6 : 32 * 5;
            }
            ctx.Renderer.DrawTexture(
                this.playerTexture,
                width / 2,
                (height / 2) - (32 * scale),
                16 * scale,
                32 * scale,
                x,
                y,
                16,
                32
            );
        }
    }
}
