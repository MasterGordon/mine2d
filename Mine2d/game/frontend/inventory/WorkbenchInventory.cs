using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine2d.game.core;
using Mine2d.game.core.tiles;

namespace Mine2d.game.frontend.inventory;

public class WorkbenchInventory : Inventory
{
    private IntPtr texture = IntPtr.Zero;
    private int x, y;
    public override void Render()
    {
        var ctx = Context.Get();
        if (this.texture == IntPtr.Zero)
        {
            this.texture = ctx.TextureFactory.LoadTexture("hud.workbench-inventory");
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

        PlayerInventoryRenderer.Render(this.x, this.y + (50 * uiScale));
        var rX = this.x + (4 * uiScale);
        var xOffset = 0;
        var yOffset = 0;
        var rY = this.y + (4 * uiScale);
        foreach (var r in Workbench.Recipes)
        {
            var desc = "Requires:\n";
            foreach (var i in r.Ingredients)
            {
                desc += $"{i.Count}x {i.GetName()}\n";
            }
            ItemRenderer.RenderItemStack(r.Result, new Vector2(rX + xOffset, rY + yOffset), true, desc);

            xOffset += 21 * uiScale;
            if (xOffset == 21 * uiScale * 9)
            {
                xOffset = 0;
                yOffset += 21 * uiScale;
            }
        }
    }

    public override void OnClick(SDL_Event e)
    {
        var ctx = Context.Get();
        var uiScale = ctx.FrontendGameState.Settings.UiScale;
        PlayerInventoryRenderer.OnClick(this.x, this.y + (50 * uiScale), e);
        var cursorPosition = Context.Get().FrontendGameState.CursorPosition;
        var player = PlayerEntity.GetSelf();
        var rX = this.x + (4 * uiScale);
        var xOffset = 0;
        var yOffset = 0;
        var rY = this.y + (4 * uiScale);
        foreach (var r in Workbench.Recipes)
        {
            if (cursorPosition.X >= rX + xOffset && cursorPosition.X <= rX + xOffset + (21 * uiScale) && cursorPosition.Y >= rY + yOffset && cursorPosition.Y <= rY + yOffset + (21 * uiScale))
            {
                Console.WriteLine("Clicked on recipe" + r.Result.GetName());
                var canCraft = true;
                foreach (var i in r.Ingredients)
                {
                    if (!player.Inventory.HasItemStack(i))
                    {
                        canCraft = false;
                        break;
                    }
                }
                if (canCraft)
                {
                    foreach (var i in r.Ingredients)
                    {
                        player.Inventory.RemoveItemStack(i);
                    }
                    player.Inventory.PickupItemStack(r.Result);
                }
            }

            xOffset += 21 * uiScale;
            if (xOffset == 21 * uiScale * 9)
            {
                xOffset = 0;
                yOffset += 21 * uiScale;
            }
        }
    }
}