using System.Diagnostics;
using Mine2d.game.core;

namespace Mine2d.game.frontend.inventory
{

    public class PlayerInventoryRenderer : Inventory
    {
        private IntPtr texture = IntPtr.Zero;
        private int x, y;

        public override void Render()
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
            this.x = (windowWidth - (width - extraSlotsWidth)) / 2;
            this.y = (windowHeight - height) / 2;
            ctx.Renderer.DrawTexture(this.texture, this.x, this.y, width, height);

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
                        ((4 + (i * 21)) * uiScale) + this.x,
                        (4 * uiScale) + this.y,
                        16 * uiScale,
                        16 * uiScale
                    );
                ctx.Renderer.DrawText(
                    "" + stack.Count,
                    ((4 + (i * 21)) * uiScale) + this.x,
                    (14 * uiScale) + this.y
                );
                if (player.Inventory.cursor == null &&
                    cursorPosition.X >= ((4 + (i * 21)) * uiScale) + this.x
                    && cursorPosition.X <= ((4 + (i * 21)) * uiScale) + this.x + (16 * uiScale)
                    && cursorPosition.Y >= (4 * uiScale) + this.y
                    && cursorPosition.Y <= (4 * uiScale) + this.y + (16 * uiScale)
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
                        ((4 + ((i % 9) * 21)) * uiScale) + this.x,
                        ((4 + 21 + ((i / 9) * 21)) * uiScale) + this.y,
                        16 * uiScale,
                        16 * uiScale
                    );
                ctx.Renderer.DrawText(
                    "" + stack.Count,
                    ((4 + ((i % 9) * 21)) * uiScale) + this.x,
                    ((14 + 21 + ((i / 9) * 21)) * uiScale) + this.y
                );
                if (player.Inventory.cursor == null &&
                    cursorPosition.X >= ((4 + ((i % 9) * 21)) * uiScale) + this.x
                    && cursorPosition.X <= ((4 + ((i % 9) * 21)) * uiScale) + this.x + (16 * uiScale)
                    && cursorPosition.Y >= ((4 + 21 + ((i / 9) * 21)) * uiScale) + this.y
                    && cursorPosition.Y <= ((4 + 21 + ((i / 9) * 21)) * uiScale) + this.y + (16 * uiScale)
                )
                {
                    Context.Get().FrontendGameState.Tooltip = stack.GetName();
                }
            }
        }

        public override void OnClick(SDL_Event e)
        {
            var cursorPosition = Context.Get().FrontendGameState.CursorPosition;
            // is hotbar
            if (cursorPosition.X >= this.x + (4 * Context.Get().FrontendGameState.Settings.UiScale)
                && cursorPosition.X <= this.x + (4 * Context.Get().FrontendGameState.Settings.UiScale) + (21 * 9 * Context.Get().FrontendGameState.Settings.UiScale)
                && cursorPosition.Y >= this.y + (4 * Context.Get().FrontendGameState.Settings.UiScale)
                && cursorPosition.Y <= this.y + (4 * Context.Get().FrontendGameState.Settings.UiScale) + (21 * Context.Get().FrontendGameState.Settings.UiScale)
            )
            {
                var player = PlayerEntity.GetSelf();
                var hotbar = player.Inventory.Hotbar;
                var index = (int)((cursorPosition.X - (this.x + (4 * Context.Get().FrontendGameState.Settings.UiScale))) / (21 * Context.Get().FrontendGameState.Settings.UiScale));
                if (e.button.button == SDL_BUTTON_LEFT)
                {
                    player.Inventory.SwapWithCursor(index, hotbar);
                }
            }
            // is inventory
            if (cursorPosition.X >= this.x + (4 * Context.Get().FrontendGameState.Settings.UiScale)
                && cursorPosition.X <= this.x + (4 * Context.Get().FrontendGameState.Settings.UiScale) + (21 * 9 * Context.Get().FrontendGameState.Settings.UiScale)
                && cursorPosition.Y >= this.y + (4 * Context.Get().FrontendGameState.Settings.UiScale) + (21 * Context.Get().FrontendGameState.Settings.UiScale)
                && cursorPosition.Y <= this.y + (4 * Context.Get().FrontendGameState.Settings.UiScale) + (21 * 5 * Context.Get().FrontendGameState.Settings.UiScale)
            )
            {
                var player = PlayerEntity.GetSelf();
                var inventory = player.Inventory.Inventory;
                var index = (int)((cursorPosition.X - (this.x + (4 * Context.Get().FrontendGameState.Settings.UiScale))) / (21 * Context.Get().FrontendGameState.Settings.UiScale))
                    + ((int)((cursorPosition.Y - (this.y + (4 * Context.Get().FrontendGameState.Settings.UiScale) + (21 * Context.Get().FrontendGameState.Settings.UiScale))) / (21 * Context.Get().FrontendGameState.Settings.UiScale)) * 9);
                if (e.button.button == SDL_BUTTON_LEFT)
                {
                    player.Inventory.SwapWithCursor(index, inventory);
                }
            }
        }
    }
}
