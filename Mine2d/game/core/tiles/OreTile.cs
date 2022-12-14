using Mine2d.game.core.data;

namespace Mine2d.game.core.tiles;

public class OreTile : Tile
{
    public OreTile(string name, string[] texturePath, int hardness) : base(name, Context.Get().TextureFactory.CreateTexture(texturePath), hardness, ItemId.Air)
    {
    }

    public OreTile(string name, string[] texturePath, int hardness, ItemId drop) : base(name, Context.Get().TextureFactory.CreateTexture(texturePath), hardness, drop)
    {
    }
}
