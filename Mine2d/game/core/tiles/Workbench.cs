using Mine2d.game.core.data;

namespace Mine2d.game.core.tiles
{
    public class Workbench : Tile
    {
        public Workbench(string name, string texturePath, int hardness) : base(name, texturePath, hardness, ItemId.Workbench)
        {
        }

        public override bool IsSolid()
        {
            return false;
        }
    }
}