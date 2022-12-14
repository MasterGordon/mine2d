using Mine2d.engine;
using Mine2d.engine.utils;
using Mine2d.game.core;

namespace Mine2d.game.frontend.renderer;

public class HudRenderer : IRenderer
{
    private readonly IntPtr hotbarTexture;
    private readonly IntPtr hotbarActiveTexture;

    public HudRenderer()
    {
        var fontManager = new FontManager(Context.Get().ResourceLoader);
        fontManager.RegisterFont("font", "assets.font.ttf", 12);
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
        var renderer = Context.Get().Renderer;
        var uiScale = Context.Get().FrontendGameState.Settings.UiScale;
        var activeSlot = Context.Get().FrontendGameState.HotbarIndex;
        // var window = Context.Get().Window;
        // var (width, height) = window.GetSize();
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

            var texture = stack.GetTexture();
            renderer.DrawTexture(texture, (4 + i * 20) * uiScale, 4 * uiScale, 16 * uiScale, 16 * uiScale);
            renderer.DrawText("" + stack.Count, (4 + i * 20) * uiScale, 20 * uiScale);
        }
    }
}
