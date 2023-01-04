using Mine2d.engine;
using Mine2d.engine.utils;
using Mine2d.game.core;
using Mine2d.game.core.data;

namespace Mine2d.game.frontend.renderer;

public class HudRenderer : IRenderer
{
    private readonly IntPtr hotbarTexture;
    private readonly IntPtr hotbarActiveTexture;

    public HudRenderer()
    {
        var fontManager = new FontManager(Context.Get().ResourceLoader);
        fontManager.RegisterFont("font", "assets.NovaMono-Regular.ttf", 16);
        Context.Get().Renderer.SetFont(fontManager.GetFont("font"), new Color(255, 255, 255));
        var rl = Context.Get().ResourceLoader;
        var (ptr, size) = rl.LoadToIntPtr("assets.hud.hotbar.png");
        var sdlBuffer = SDL_RWFromMem(ptr, size);
        var surface = IMG_Load_RW(sdlBuffer, 1);
        this.hotbarTexture = Context.Get().Renderer.CreateTextureFromSurface(surface);
        SDL_FreeSurface(surface);
        (ptr, size) = rl.LoadToIntPtr("assets.hud.hotbar-active.png");
        sdlBuffer = SDL_RWFromMem(ptr, size);
        surface = IMG_Load_RW(sdlBuffer, 1);
        this.hotbarActiveTexture = Context.Get().Renderer.CreateTextureFromSurface(surface);
        SDL_FreeSurface(surface);
    }

    public void Render()
    {
        this.RenderHotbar();
        this.RenderGps();
    }

    private void RenderHotbar()
    {
        var renderer = Context.Get().Renderer;
        var uiScale = Context.Get().FrontendGameState.Settings.UiScale;
        var activeSlot = Context.Get().FrontendGameState.HotbarIndex;
        var (hotbarWidth, hotbarHeight) = renderer.GetTextureSize(this.hotbarTexture);
        var player = PlayerEntity.GetSelf();
        renderer.DrawTexture(this.hotbarTexture, 0, 0, hotbarWidth * uiScale, hotbarHeight * uiScale);
        renderer.DrawTexture(this.hotbarActiveTexture, activeSlot * 24 * uiScale, 0, 24 * uiScale, 24 * uiScale);
        for (var i = 0; i < player?.Inventory.Hotbar.Length; i++)
        {
            var stack = player.Inventory.Hotbar[i];
            if (stack == null)
            {
                continue;
            }

            ItemRenderer.RenderItemStack(stack, new Vector2((4 + (i * 24)) * uiScale, 4 * uiScale));
        }
    }

    private void RenderGps()
    {
        var player = PlayerEntity.GetSelf();
        if (player?.Inventory.HasItemStack(new ItemStack(ItemId.Gps, 1)) != true) return;
        var renderer = Context.Get().Renderer;
        var (wWidth, _) = Context.Get().Window.GetSize();
        var x = player.PrettyPosition.X.ToString("F2");
        var y = player.PrettyPosition.Y.ToString("F2");
        var (texture, width, height, surfaceMessage) = renderer.CreateTextTexture("GPS");
        renderer.DrawTexture(texture, wWidth - width, 0, width, height);
        SDL_DestroyTexture(texture);
        SDL_FreeSurface(surfaceMessage);
        (texture, width, height, surfaceMessage) = renderer.CreateTextTexture($"X: {x} Y: {y}");
        renderer.DrawTexture(texture, wWidth - width, height, width, height);
        SDL_DestroyTexture(texture);
        SDL_FreeSurface(surfaceMessage);
    }
}
