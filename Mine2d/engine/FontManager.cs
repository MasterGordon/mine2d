namespace Mine2d.engine;

public class FontManager
{
    private readonly Dictionary<string, IntPtr> fonts = new();
    private readonly ResourceLoader resourceLoader;

    public FontManager(ResourceLoader resourceLoader)
    {
        this.resourceLoader = resourceLoader;
        if (TTF_Init() != 0)
        {
            throw new SDLException(TTF_GetError());
        }
    }

    public void RegisterFont(string name, string path, int fontSize)
    {
        if (this.fonts.ContainsKey(name))
        {
            return;
        }

        var (ptr, size) = this.resourceLoader.LoadToIntPtr(path);
        var sdlBuffer = SDL_RWFromConstMem(ptr, size);
        var font = TTF_OpenFontRW(sdlBuffer, 1, fontSize);
        if (font == IntPtr.Zero)
        {
            throw new SDLException(TTF_GetError());
        }
        this.fonts.Add(name, font);
    }

    public IntPtr GetFont(string name)
    {
        return this.fonts[name];
    }
}
