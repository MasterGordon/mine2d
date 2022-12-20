using Mine2d.engine;
using Mine2d.game.core;
using Mine2d.game.state;

namespace Mine2d.game.frontend.renderer
{
    public class InventoryRenderer : IRenderer
    {
        public void Render()
        {
            var ctx = Context.Get();
            var inventory = ctx.FrontendGameState.OpenInventory;
            if (inventory == InventoryKind.None) return;
            ctx.Renderer.SetColor(0, 0, 0, 200);
            ctx.Renderer.SetDrawBlendMode(SDL_BlendMode.SDL_BLENDMODE_BLEND);
            ctx.Renderer.DrawRect(0, 0, ctx.FrontendGameState.WindowWidth, ctx.FrontendGameState.WindowHeight);
            var inventoryRenderer = ctx.InventoryRegistry.GetInventory(inventory);
            inventoryRenderer.Render();
            var player = PlayerEntity.GetSelf();
            if (player.Inventory.cursor != null)
            {
                var cursorPosition = ctx.FrontendGameState.CursorPosition;
                var itemTexture = player.Inventory.cursor.GetTexture();
                var uiScale = ctx.FrontendGameState.Settings.UiScale;
                ctx.Renderer.DrawTexture(
                    itemTexture,
                    (int)(cursorPosition.X + 2),
                    (int)(cursorPosition.Y + 2),
                    16 * uiScale,
                    16 * uiScale
                );
                ctx.Renderer.DrawText(
                    "" + player.Inventory.cursor.Count,
                    (int)(cursorPosition.X + 2),
                    (int)(cursorPosition.Y + (12 * uiScale))
                );
            }
        }
    }
}