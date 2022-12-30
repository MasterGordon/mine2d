using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine2d.game.core.data;

namespace Mine2d.game.core.tiles;

public class DecoTile : Tile
{
    public DecoTile(string name, string textureName, int hardness, ItemId drop) : base(name, textureName, hardness, drop)
    {
    }

    public override bool IsSolid()
    {
        return false;
    }
}