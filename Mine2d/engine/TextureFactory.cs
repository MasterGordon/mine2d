namespace Mine2d.engine;

public class TextureFactory
{
    private readonly ResourceLoader resourceLoader;
    private readonly Renderer renderer;
    private readonly Dictionary<string, IntPtr> textureCache = new();

    public TextureFactory(ResourceLoader resourceLoader, Renderer renderer)
    {
        this.resourceLoader = resourceLoader;
        this.renderer = renderer;
    }

    public IntPtr CreateTexture(string[] path)
    {
        var target = this.LoadTexture("blocks." + path[0]);
        for (var i = 1; i < path.Length; i++)
        {
            var t = this.LoadTexture("blocks." + path[i]);
            target = this.MergeTextures(target, t);
        }
        return target;
    }

    private IntPtr MergeTextures(IntPtr t1, IntPtr t2)
    {
        var (w, h) = this.renderer.GetTextureSize(t1);
        var target = SDL_CreateTexture(this.renderer.GetRaw(), SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, w, h);
        Renderer.ProcessStatus(SDL_SetRenderTarget(this.renderer.GetRaw(), target));
        Renderer.ProcessStatus(SDL_SetTextureBlendMode(target, SDL_BlendMode.SDL_BLENDMODE_BLEND));
        this.renderer.DrawTexture(t1, 0, 0, w, h);
        this.renderer.DrawTexture(t2, 0, 0, w, h);
        Renderer.ProcessStatus(SDL_SetRenderTarget(this.renderer.GetRaw(), IntPtr.Zero));
        return target;
    }

    public IntPtr LoadTexture(string path)
    {
        if (this.textureCache.TryGetValue(path, out var value))
        {
            return value;
        }
        var (ptr, size) = this.resourceLoader.LoadToIntPtr("assets." + path + ".png");
        var sdlBuffer = SDL_RWFromMem(ptr, size);
        var surface = IMG_Load_RW(sdlBuffer, 1);
        var texture = this.renderer.CreateTextureFromSurface(surface);
        SDL_FreeSurface(surface);
        this.textureCache.Add(path, texture);
        return texture;
    }
}
