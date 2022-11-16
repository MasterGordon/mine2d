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
            SDL_WindowFlags.SDL_WINDOW_VULKAN | SDL_WindowFlags.SDL_WINDOW_RESIZABLE
        );
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
