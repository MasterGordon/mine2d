using Mine2d.core.data;
using Mine2d.engine;

namespace Mine2d.core.tiles;

public class Tile
{
    public string Name { get; set; }
    public int Hardness { get; set; }
    private readonly IntPtr texture;
    private static IntPtr breakingTexture;

    public Tile(string name, string textureName, int hardness)
    {
        this.Name = name;
        this.Hardness = hardness;

        var rl = Context.Get().ResourceLoader;
        var (ptr, size) = rl.LoadToIntPtr("assets." + textureName + ".png");
        var sdlBuffer = SDL_RWFromMem(ptr, size);
        var surface = IMG_Load_RW(sdlBuffer, 1);
        this.texture = Context.Get().Renderer.CreateTextureFromSurface(surface);
        if (breakingTexture == IntPtr.Zero)
        {
            LoadBreakingTexture();
        }
        SDL_FreeSurface(surface);
    }

    public Tile(string name, IntPtr texture, int hardness)
    {
        this.Name = name;
        this.Hardness = hardness;
        this.texture = texture;
    }

    ~Tile()
    {
        SDL_DestroyTexture(this.texture);
    }

    public void Render(int x, int y, STile tile)
    {
        var renderer = Context.Get().Renderer;
        var scale = Context.Get().FrontendGameState.Settings.GameScale;
        var camera = Context.Get().FrontendGameState.Camera;
        Renderer.SetTextureColorModulate(this.texture, 255);
        renderer.DrawTexture(
            this.texture,
            (x - (int)camera.Position.X) * scale,
            (y - (int)camera.Position.Y) * scale,
            Constants.TileSize * scale,
            Constants.TileSize * scale
        );
        if (tile.Hits > 0)
        {
            var breakingOffset = (int)((double)tile.Hits / this.Hardness * 4);
            renderer.DrawTexture(
                breakingTexture,
                (x - (int)camera.Position.X) * scale,
                (y - (int)camera.Position.Y) * scale,
                Constants.TileSize * scale,
                Constants.TileSize * scale,
                breakingOffset,
                16,
                16
            );
        }
    }

    private static void LoadBreakingTexture()
    {
        var rl = Context.Get().ResourceLoader;
        var (ptr, size) = rl.LoadToIntPtr("assets.breaking.png");
        var sdlBuffer = SDL_RWFromMem(ptr, size);
        var surface = IMG_Load_RW(sdlBuffer, 1);
        breakingTexture = Context.Get().Renderer.CreateTextureFromSurface(surface);
        SDL_FreeSurface(surface);
    }
}
