using Mine2d.engine;

namespace Mine2d.core.tiles;

public class OreTile : Tile
{
    public OreTile(string name, string[] texturePath, int hardness) : base(name, new TextureFactory().CreateTexture(texturePath), hardness)
    {
    }
}
