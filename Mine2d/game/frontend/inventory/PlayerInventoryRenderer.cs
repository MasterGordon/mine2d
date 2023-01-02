using Mine2d.game.core;

namespace Mine2d.game.frontend.inventory;

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
        for (var i = 0; i < hotbar.Length; i++)
        {
            var stack = hotbar[i];
            if (stack == null)
            {
                continue;
            }
            ItemRenderer.RenderItemStack(stack, new Vector2(((4 + (i * 21)) * uiScale) + this.x, (4 * uiScale) + this.y), player.Inventory.Cursor == null);
        }
        for (var i = 0; i < inventory.Length; i++)
        {
            var stack = inventory[i];
            if (stack == null)
            {
                continue;
            }

            ItemRenderer.RenderItemStack(stack, new Vector2(((4 + ((i % 9) * 21)) * uiScale) + this.x, ((4 + 21 + ((i / 9) * 21)) * uiScale) + this.y), player.Inventory.Cursor == null);
        }
        var pickaxe = player.Inventory.Pickaxe;
        if (pickaxe != null)
        {
            ItemRenderer.RenderItemStack(pickaxe, new Vector2(((4 + (9 * 21)) * uiScale) + this.x, (((5 * 21) + 4) * uiScale) + this.y), player.Inventory.Cursor == null);
        }
    }

    public override void OnClick(SDL_Event e)
    {
        var cursorPosition = Context.Get().FrontendGameState.CursorPosition;
        var player = PlayerEntity.GetSelf();
        // is hotbar
        if (cursorPosition.X >= this.x + (4 * Context.Get().FrontendGameState.Settings.UiScale)
            && cursorPosition.X <= this.x + (4 * Context.Get().FrontendGameState.Settings.UiScale) + (21 * 9 * Context.Get().FrontendGameState.Settings.UiScale)
            && cursorPosition.Y >= this.y + (4 * Context.Get().FrontendGameState.Settings.UiScale)
            && cursorPosition.Y <= this.y + (4 * Context.Get().FrontendGameState.Settings.UiScale) + (21 * Context.Get().FrontendGameState.Settings.UiScale)
        )
        {
            var hotbar = player.Inventory.Hotbar;
            var index = (int)((cursorPosition.X - (this.x + (4 * Context.Get().FrontendGameState.Settings.UiScale))) / (21 * Context.Get().FrontendGameState.Settings.UiScale));
            if (e.button.button == SDL_BUTTON_LEFT)
            {
                player.Inventory.SwapWithCursor(index, hotbar);
            }
            if (e.button.button == SDL_BUTTON_RIGHT)
            {
                if (player.Inventory.Cursor == null)
                {
                    player.Inventory.TakeHalf(index, hotbar);
                }
                else
                {
                    player.Inventory.DropOne(index, hotbar);
                }
            }
        }
        if (cursorPosition.X >= this.x + (4 * Context.Get().FrontendGameState.Settings.UiScale)
            && cursorPosition.X <= this.x + (4 * Context.Get().FrontendGameState.Settings.UiScale) + (21 * 9 * Context.Get().FrontendGameState.Settings.UiScale)
            && cursorPosition.Y >= this.y + (4 * Context.Get().FrontendGameState.Settings.UiScale) + (21 * Context.Get().FrontendGameState.Settings.UiScale)
            && cursorPosition.Y <= this.y + (4 * Context.Get().FrontendGameState.Settings.UiScale) + (21 * 6 * Context.Get().FrontendGameState.Settings.UiScale)
        )
        {
            var inventory = player.Inventory.Inventory;
            var index = (int)((cursorPosition.X - (this.x + (4 * Context.Get().FrontendGameState.Settings.UiScale))) / (21 * Context.Get().FrontendGameState.Settings.UiScale))
                + ((int)((cursorPosition.Y - (this.y + (4 * Context.Get().FrontendGameState.Settings.UiScale) + (21 * Context.Get().FrontendGameState.Settings.UiScale))) / (21 * Context.Get().FrontendGameState.Settings.UiScale)) * 9);
            if (e.button.button == SDL_BUTTON_LEFT)
            {
                player.Inventory.SwapWithCursor(index, inventory);
            }
            if (e.button.button == SDL_BUTTON_RIGHT)
            {
                if (player.Inventory.Cursor == null)
                {
                    player.Inventory.TakeHalf(index, inventory);
                }
                else
                {
                    player.Inventory.DropOne(index, inventory);
                }
            }
        }
        if (IsCursorOnSlot(((4 + (9 * 21)) * Context.Get().FrontendGameState.Settings.UiScale) + this.x, (((5 * 21) + 4) * Context.Get().FrontendGameState.Settings.UiScale) + this.y))
        {
            if (e.button.button == SDL_BUTTON_LEFT)
            {
                player.Inventory.SwapPickaxeWithCursor();
            }
            if (e.button.button == SDL_BUTTON_RIGHT)
            {
                player.Inventory.SwapPickaxeWithCursor();
            }
        }
    }

    private static bool IsCursorOnSlot(int x, int y)
    {
        var cursorPosition = Context.Get().FrontendGameState.CursorPosition;
        var width = 21 * Context.Get().FrontendGameState.Settings.UiScale;
        var height = 21 * Context.Get().FrontendGameState.Settings.UiScale;
        return cursorPosition.X >= x
            && cursorPosition.X <= x + width
            && cursorPosition.Y >= y
            && cursorPosition.Y <= y + height;
    }
}
