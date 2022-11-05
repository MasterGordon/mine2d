class Renderer
{
    private IntPtr renderer;
    private IntPtr font;
    private SDL_Color color;

    public Renderer(Window window)
    {
        this.renderer = SDL_CreateRenderer(
            window.GetWindow(),
            -1,
            SDL_RendererFlags.SDL_RENDERER_ACCELERATED
        );
    }

    public void Clear()
    {
        SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
        SDL_RenderClear(renderer);
    }

    public void Present()
    {
        SDL_RenderPresent(renderer);
    }

    public void DrawRect(double x, double y, int w, int h)
    {
        this.DrawRect((int)x, (int)y, w, h);
    }

    public void DrawRect(int x, int y, int w, int h)
    {
        SDL_Rect rect = new SDL_Rect();
        rect.x = x;
        rect.y = y;
        rect.w = w;
        rect.h = h;

        SDL_RenderFillRect(renderer, ref rect);
    }

    public void DrawLines(double[][] points)
    {
        SDL_Point[] sdlPoints = new SDL_Point[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            sdlPoints[i].x = (int)points[i][0];
            sdlPoints[i].y = (int)points[i][1];
        }

        SDL_RenderDrawLines(renderer, sdlPoints, points.Length);
    }

    public void SetColor(int r, int g, int b, int a = 255)
    {
        SDL_SetRenderDrawColor(renderer, (byte)r, (byte)g, (byte)b, (byte)a);
    }

    public void SetFont(IntPtr font, SDL_Color color)
    {
        this.font = font;
        this.color = color;
    }

    public void SetFont(IntPtr font, Color color)
    {
        this.font = font;
        this.color = color.toSDLColor();
    }

    public void DrawText(string text, int x, int y, bool center = false)
    {
        var surfaceMessage = TTF_RenderText_Solid(this.font, text, this.color);

        var texture = SDL_CreateTextureFromSurface(this.renderer, surfaceMessage);
        int width;
        int height;

        SDL_QueryTexture(texture, out _, out _, out width, out height);

        SDL_Rect rect = new SDL_Rect();
        rect.x = x;
        rect.y = y;
        rect.w = width;
        rect.h = height;

        if (center)
        {
            rect.x -= width / 2;
            rect.y -= height / 2;
        }

        SDL_RenderCopy(this.renderer, texture, IntPtr.Zero, ref rect);
        SDL_DestroyTexture(texture);
        SDL_FreeSurface(surfaceMessage);
    }

    public IntPtr GetRaw()
    {
        return renderer;
    }

    public IntPtr CreateTextureFromSurface(IntPtr surface)
    {
        return SDL_CreateTextureFromSurface(renderer, surface);
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
        SDL_RenderCopy(renderer, texture, IntPtr.Zero, ref rect);
    }
}
