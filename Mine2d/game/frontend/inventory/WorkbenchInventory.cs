using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mine2d.game.frontend.inventory;

public class WorkbenchInventory : Inventory
{
    private IntPtr texture = IntPtr.Zero;
    public override void Render()
    {
        var ctx = Context.Get();
        if (this.texture == IntPtr.Zero)
        {
            this.texture = ctx.TextureFactory.LoadTexture("hud.workbench-inventory");
        }
        var (width, height) = ctx.Renderer.GetTextureSize(this.texture);
        var (windowWidth, windowHeight) = (ctx.FrontendGameState.WindowWidth, ctx.FrontendGameState.WindowHeight);
        var x = (windowWidth - width) / 2;
        var y = (windowHeight - height) / 2;
        var uiScale = ctx.FrontendGameState.Settings.UiScale;
        ctx.Renderer.DrawTexture(this.texture, x, y, width * uiScale, height * uiScale);
    }
}