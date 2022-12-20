using Mine2d.game.core;

namespace Mine2d.game.frontend.inventory
{
    public class PlayerInventoryRenderer : IInventoryRenderer
    {
        private IntPtr texture = IntPtr.Zero;

        public void Render()
        {
            var ctx = Context.Get();
            if (this.texture == IntPtr.Zero)
            {
                this.texture = ctx.TextureFactory.LoadTexture("hud.player-inventory");
            }
            var (width, height) = ctx.Renderer.GetTextureSize(this.texture);
            var uiScale = ctx.FrontendGameState.Settings.UiScale;
            width *= uiScale;
            height *= uiScale;
            var extraSlotsWidth = InventoryConstants.ExtraSlotsWidth * uiScale;
            var (windowWidth, windowHeight) = (ctx.FrontendGameState.WindowWidth, ctx.FrontendGameState.WindowHeight);
            var x = (windowWidth - (width - extraSlotsWidth)) / 2;
            var y = (windowHeight - height) / 2;
            ctx.Renderer.DrawTexture(this.texture, x, y, width, height);

            var player = PlayerEntity.GetSelf();
            var inventory = player.Inventory.Inventory;
            var hotbar = player.Inventory.Hotbar;
            var cursorPosition = Context.Get().FrontendGameState.CursorPosition;
            for (var i = 0; i < hotbar.Length; i++)
            {
                var stack = hotbar[i];
                if (stack == null)
                {
                    continue;
                }

                var itemTexture = stack.GetTexture();
                ctx.Renderer.DrawTexture(
                        itemTexture,
                        ((4 + (i * 22)) * uiScale) + x,
                        (4 * uiScale) + y,
                        16 * uiScale,
                        16 * uiScale
                    );
                ctx.Renderer.DrawText(
                    "" + stack.Count,
                    ((4 + (i * 22)) * uiScale) + x,
                    (14 * uiScale) + y
                );
                if (cursorPosition.X >= ((4 + (i * 22)) * uiScale) + x
                    && cursorPosition.X <= ((4 + (i * 22)) * uiScale) + x + (16 * uiScale)
                    && cursorPosition.Y >= (4 * uiScale) + y
                    && cursorPosition.Y <= (4 * uiScale) + y + (16 * uiScale)
                )
                {
                    Context.Get().FrontendGameState.Tooltip = stack.GetName();
                }
            }
            for (var i = 0; i < inventory.Length; i++)
            {
                var stack = inventory[i];
                if (stack == null)
                {
                    continue;
                }

                var itemTexture = stack.GetTexture();
                ctx.Renderer.DrawTexture(
                        itemTexture,
                        ((4 + ((i % 9) * 22)) * uiScale) + x,
                        ((4 + 21 + ((i / 9) * 22)) * uiScale) + y,
                        16 * uiScale,
                        16 * uiScale
                    );
                ctx.Renderer.DrawText(
                    "" + stack.Count,
                    ((4 + ((i % 9) * 22)) * uiScale) + x,
                    ((14 + 21 + ((i / 9) * 22)) * uiScale) + y
                );
                if (cursorPosition.X >= ((4 + ((i % 9) * 22)) * uiScale) + x
                    && cursorPosition.X <= ((4 + ((i % 9) * 22)) * uiScale) + x + (16 * uiScale)
                    && cursorPosition.Y >= ((4 + 21 + ((i / 9) * 22)) * uiScale) + y
                    && cursorPosition.Y <= ((4 + 21 + ((i / 9) * 22)) * uiScale) + y + (16 * uiScale)
                )
                {
                    Context.Get().FrontendGameState.Tooltip = stack.GetName();
                }
            }
        }
    }
}
