using Mine2d.engine;

namespace Mine2d.game.frontend.renderer;

public class TooltipRenderer : IRenderer
{
    private const int TooltipTextureWidth = 9;
    private const int TooltipTextureHeight = 9;
    private readonly IntPtr tooltipTexture;

    public TooltipRenderer()
    {
        var rl = Context.Get().ResourceLoader;
        var (ptr, size) = rl.LoadToIntPtr("assets.hud.tooltip.png");
        var sdlBuffer = SDL_RWFromMem(ptr, size);
        var surface = IMG_Load_RW(sdlBuffer, 1);
        this.tooltipTexture = Context.Get().Renderer.CreateTextureFromSurface(surface);
        SDL_FreeSurface(surface);
    }

    public void Render()
    {
        var ctx = Context.Get();
        var tooltip = ctx.FrontendGameState.Tooltip;
        if(tooltip == null || tooltip.Trim().Length == 0)
        {
            return;
        }
        var renderer = ctx.Renderer;
        var uiScale = ctx.FrontendGameState.Settings.UiScale;
        var tooltipPosition = ctx.FrontendGameState.CursorPosition + new Vector2(8, 8);
        var tooltipX = (int)tooltipPosition.X;
        var tooltipY = (int)tooltipPosition.Y;
        var (texture, width, height, surfaceMessage) = renderer.CreateTextTexture(tooltip);
        renderer.DrawTexture(
            this.tooltipTexture,
            tooltipX,
            tooltipY,
            4 * uiScale,
            4 * uiScale,
            0,
            0,
            4,
            4
        );        

        renderer.DrawTexture(
            this.tooltipTexture,
            tooltipX + 4 * uiScale,
            tooltipY,
            width,
            4 * uiScale,
            4,
            0,
            1,
            4
        );

        renderer.DrawTexture(
            this.tooltipTexture,
            tooltipX + width + 4 * uiScale,
            tooltipY,
            4 * uiScale,
            4 * uiScale,
            5,
            0,
            4,
            4
        );

        Console.WriteLine(height);
        renderer.DrawTexture(
            this.tooltipTexture,
            tooltipX,
            tooltipY + 4 * uiScale,
            4 * uiScale,
            height,
            0,
            4,
            4,
            1
        );

        renderer.DrawTexture(
            this.tooltipTexture,
            tooltipX + 4 * uiScale,
            tooltipY + 4 * uiScale,
            width,
            height,
            4,
            4,
            1,
            1
        );

        renderer.DrawTexture(
            this.tooltipTexture,
            tooltipX + width + 4 * uiScale,
            tooltipY + 4 * uiScale,
            4 * uiScale,
            height,
            5,
            4,
            4,
            1
        );

        renderer.DrawTexture(
            this.tooltipTexture,
            tooltipX,
            tooltipY + height + 4 * uiScale,
            4 * uiScale,
            4 * uiScale,
            0,
            5,
            4,
            4
        );

        renderer.DrawTexture(
            this.tooltipTexture,
            tooltipX + 4 * uiScale,
            tooltipY + height + 4 * uiScale,
            width,
            4 * uiScale,
            4,
            5,
            1,
            4
        );

        renderer.DrawTexture(
            this.tooltipTexture,
            tooltipX + width + 4 * uiScale,
            tooltipY + height + 4 * uiScale,
            4 * uiScale,
            4 * uiScale,
            5,
            5,
            4,
            4
        );

        renderer.DrawTexture(
            texture,
            tooltipX + 4 * uiScale,
            tooltipY + 4 * uiScale,
            width,
            height
        );

        SDL_DestroyTexture(texture);

        SDL_FreeSurface(surfaceMessage);
    }
}