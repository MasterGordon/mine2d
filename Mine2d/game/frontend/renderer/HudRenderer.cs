using Mine2d.engine;
using Mine2d.engine.utils;
using Mine2d.game.core;

namespace Mine2d.game.frontend.renderer;

public class HudRenderer : IRenderer
{
    private readonly IntPtr hotbarTexture;

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
    }

    public void Render()
    {
        var renderer = Context.Get().Renderer;
        var uiScale = Context.Get().FrontendGameState.Settings.UiScale;
        // var window = Context.Get().Window;
        // var (width, height) = window.GetSize();
        var (hotbarWidth, hotbarHeight) = renderer.GetTextureSize(this.hotbarTexture);
        var player = PlayerEntity.GetSelf();
        renderer.DrawTexture(this.hotbarTexture, 0, 0, hotbarWidth * uiScale, hotbarHeight * uiScale);
        for (var i = 0; i < player?.Inventory.Hotbar.Length; i++)
        {
            var stack = player.Inventory.Hotbar[i];
            if (stack == null)
            {
                continue;
            }

            var texture = stack.GetTexture();
            renderer.DrawTexture(texture, (4 + i * 20) * uiScale, 4 * uiScale, 16 * uiScale, 16 * uiScale);
            renderer.DrawText(stack.Count.ToString(), (4 + i * 20) * uiScale, 20 * uiScale);
        }
    }
}
