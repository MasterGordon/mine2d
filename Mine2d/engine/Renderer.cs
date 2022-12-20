using Mine2d.engine.utils;

namespace Mine2d.engine;

public class Renderer
{
    private readonly IntPtr renderer;
    private IntPtr font;
    private SDL_Color color;

    public Renderer(Window window)
    {
        this.renderer = SDL_CreateRenderer(
            window.GetWindow(),
            -1,
            SDL_RendererFlags.SDL_RENDERER_ACCELERATED
        );
        if (this.renderer == IntPtr.Zero)
        {
            throw new SDLException(SDL_GetError());
        }
    }

    public void Clear()
    {
        ProcessStatus(SDL_SetRenderDrawColor(this.renderer, 0, 0, 0, 255));
        ProcessStatus(SDL_RenderClear(this.renderer));
    }

    public void Present()
    {
        SDL_RenderPresent(this.renderer);
    }

    public void DrawRect(double x, double y, int w, int h)
    {
        this.DrawRect((int)x, (int)y, w, h);
    }

    public void DrawRect(int x, int y, int w, int h)
    {
        var rect = new SDL_Rect
        {
            x = x,
            y = y,
            w = w,
            h = h
        };

        ProcessStatus(SDL_RenderFillRect(this.renderer, ref rect));
    }

    public void DrawOutline(int x, int y, int w, int h)
    {
        var rect = new SDL_Rect
        {
            x = x,
            y = y,
            w = w,
            h = h
        };

        ProcessStatus(SDL_RenderDrawRect(this.renderer, ref rect));
    }

    public void DrawLines(double[][] points)
    {
        var sdlPoints = new SDL_Point[points.Length];
        for (var i = 0; i < points.Length; i++)
        {
            sdlPoints[i].x = (int)points[i][0];
            sdlPoints[i].y = (int)points[i][1];
        }

        ProcessStatus(SDL_RenderDrawLines(this.renderer, sdlPoints, points.Length));
    }

    public void SetColor(int r, int g, int b, int a = 255)
    {
        ProcessStatus(SDL_SetRenderDrawColor(this.renderer, (byte)r, (byte)g, (byte)b, (byte)a));
    }

    public void SetFont(IntPtr font, SDL_Color color)
    {
        this.font = font;
        this.color = color;
    }

    public void SetFont(IntPtr font, Color color)
    {
        this.font = font;
        this.color = color.ToSdlColor();
    }

    public (IntPtr texture, int width, int height, IntPtr surfaceMessage) CreateTextTexture(string text)
    {
        var surfaceMessage = TTF_RenderText_Solid(this.font, text, this.color);
        var texture = SDL_CreateTextureFromSurface(this.renderer, surfaceMessage);
        ProcessStatus(SDL_QueryTexture(texture, out _, out _, out var width, out var height));
        return (texture, width, height, surfaceMessage);
    }

    public void DrawText(string text, int x, int y, bool center = false)
    {
        var (texture, width, height, surfaceMessage) = this.CreateTextTexture(text);

        var rect = new SDL_Rect
        {
            x = x,
            y = y,
            w = width,
            h = height
        };

        if (center)
        {
            rect.x -= width / 2;
            rect.y -= height / 2;
        }

        ProcessStatus(SDL_RenderCopy(this.renderer, texture, IntPtr.Zero, ref rect));
        SDL_DestroyTexture(texture);
        SDL_FreeSurface(surfaceMessage);
    }

    public IntPtr GetRaw()
    {
        return this.renderer;
    }

    public IntPtr CreateTextureFromSurface(IntPtr surface)
    {
        return SDL_CreateTextureFromSurface(this.renderer, surface);
    }

    public void DrawTexture(IntPtr texture, int x, int y, int w, int h)
    {
        SDL_Rect rect = new()
        {
            x = x,
            y = y,
            w = w,
            h = h
        };
        ProcessStatus(SDL_RenderCopy(this.renderer, texture, IntPtr.Zero, ref rect));
    }
    public void DrawTexture(IntPtr texture, int x, int y, int w, int h, int srcX, int srcY, int srcWidth, int srcHeight)
    {
        SDL_Rect rect = new()
        {
            x = x,
            y = y,
            w = w,
            h = h
        };
        SDL_Rect srcRect = new()
        {
            x = srcX,
            y = srcY,
            w = srcWidth,
            h = srcHeight,
        };
        ProcessStatus(SDL_RenderCopy(this.renderer, texture, ref srcRect, ref rect));
    }

    public void DrawTexture(IntPtr texture, int x, int y, int w, int h, int offsetIndex, int srcWidth, int srcHeight)
    {
        SDL_Rect rect = new()
        {
            x = x,
            y = y,
            w = w,
            h = h
        };
        SDL_Rect srcRect = new()
        {
            x = srcWidth * offsetIndex,
            y = 0,
            w = srcWidth,
            h = srcHeight,
        };
        ProcessStatus(SDL_RenderCopy(this.renderer, texture, ref srcRect, ref rect));
    }

    public static void SetTextureColorModulate(IntPtr texture, int r, int g, int b)
    {
        ProcessStatus(SDL_SetTextureColorMod(texture, (byte)r, (byte)g, (byte)b));
    }

    public static void SetTextureColorModulate(IntPtr texture, int a)
    {
        SetTextureColorModulate(texture, a, a, a);
    }

    public static void ProcessStatus(int status)
    {
        if (status != 0)
        {
            throw new SDLException(SDL_GetError());
        }
    }

    public (int srcW, int srcH) GetTextureSize(IntPtr src)
    {
        ProcessStatus(SDL_QueryTexture(src, out _, out _, out var srcW, out var srcH));
        return (srcW, srcH);
    }

    public void SetDrawBlendMode(SDL_BlendMode blendMode)
    {
        ProcessStatus(SDL_SetRenderDrawBlendMode(this.renderer, blendMode));
    }
}
