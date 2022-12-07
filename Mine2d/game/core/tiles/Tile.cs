using Mine2d.engine;
using Mine2d.game.core.data;
using Mine2d.game.core.data.entities;

namespace Mine2d.game.core.tiles;

public class Tile
{
    public string Name { get; set; }
    public int Hardness { get; set; }
    private readonly IntPtr texture;
    private static IntPtr breakingTexture;
    public ItemId Drop { get; set; }

    public Tile(string name, string textureName, int hardness, ItemId drop)
    {
        this.Name = name;
        this.Hardness = hardness;
        this.Drop = drop;

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

    public Tile(string name, IntPtr texture, int hardness, ItemId drop)
    {
        this.Name = name;
        this.Drop = drop;
        this.Hardness = hardness;
        this.texture = texture;
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

    public void DropItem(Vector2 position)
    {
        if (this.Drop == ItemId.Air)
        {
            return;
        }
        var world = Context.Get().GameState.World;
        var itemEntity = new ItemEntity
        {
            ItemId = this.Drop,
            Position = position + new Vector2(8, 8),
            Velocity = new Vector2((float)(new Random().NextDouble() - 0.5), 0),
        };
        world.GetChunkAt(position).Entities.Add(itemEntity);
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
