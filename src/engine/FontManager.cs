using static SDL2.SDL_ttf;
using static SDL2.SDL;

class FontManager
{
    private Dictionary<string, IntPtr> fonts = new();
    private ResourceLoader resourceLoader;

    public FontManager(ResourceLoader resourceLoader)
    {
        this.resourceLoader = resourceLoader;
        if (TTF_Init() != 0)
        {
            throw new Exception("TTF_Init failed");
        }
    }

    public void RegisterFont(string name, string path, int fontSize)
    {
        if (fonts.ContainsKey(name))
            return;
        var res = resourceLoader.LoadToIntPtr(path);
        var sdlBuffer = SDL_RWFromConstMem(res.ptr, res.size);
        var font = TTF_OpenFontRW(sdlBuffer, 1, fontSize);
        if (font == IntPtr.Zero)
        {
            throw new Exception("TTF_OpenFont failed");
        }
        fonts.Add(name, font);
    }

    public IntPtr GetFont(string name)
    {
        return fonts[name];
    }
}
