namespace Mine2d.game.frontend.inventory
{
    public class PlayerInventoryRenderer : IInventoryRenderer
    {
        private IntPtr texture = IntPtr.Zero;

        public void Render()
        {
            var ctx = Context.Get();
            if(this.texture == IntPtr.Zero) {
                this.texture = ctx.TextureFactory.LoadTexture("hud.player-inventory");
            }
            var (width, height) = ctx.Renderer.GetTextureSize(this.texture);
            width *= ctx.FrontendGameState.Settings.UiScale;
            height *= ctx.FrontendGameState.Settings.UiScale;
            var extraSlotsWidth = InventoryConstants.ExtraSlotsWidth * ctx.FrontendGameState.Settings.UiScale;
            var (windowWidth, windowHeight) = (ctx.FrontendGameState.WindowWidth, ctx.FrontendGameState.WindowHeight);
            var x = (windowWidth - (width - extraSlotsWidth)) / 2;
            var y = (windowHeight - height) / 2;
            ctx.Renderer.DrawTexture(this.texture, x, y, width, height);
        }
    }
}