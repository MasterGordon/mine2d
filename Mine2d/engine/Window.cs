namespace Mine2d.engine;

public class Window
{
    private readonly IntPtr window;

    public Window(string title, int w, int h)
    {
        this.window = SDL_CreateWindow(
            title,
            SDL_WINDOWPOS_CENTERED,
            SDL_WINDOWPOS_CENTERED,
            w,
            h,
            SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL_WindowFlags.SDL_WINDOW_RESIZABLE
        );
        var (ptr, size) = new ResourceLoader().LoadToIntPtr("assets.icon.png");
        var sdlBuffer = SDL_RWFromMem(ptr, size);
        var surface = IMG_Load_RW(sdlBuffer, 1);
        SDL_SetWindowIcon(this.window, surface);
    }

    public Window(string title, int x, int y, int w, int h, SDL_WindowFlags flags)
    {
        this.window = SDL_CreateWindow(title, x, y, w, h, flags);
    }

    public void Dispose()
    {
        SDL_DestroyWindow(this.window);
    }

    public IntPtr GetWindow()
    {
        return this.window;
    }

    public (int width, int height) GetSize()
    {
        SDL_GetWindowSize(this.window, out var w, out var h);
        return (w, h);
    }

    public IntPtr GetRaw()
    {
        return this.window;
    }
}
