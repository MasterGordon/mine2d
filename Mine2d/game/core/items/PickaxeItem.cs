using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine2d.game.core.data;

namespace Mine2d.game.core.items;

public class PickaxeItem : Item
{
    public PickaxeItem(ItemId id, string name, string textureName) : base(id, name, textureName)
    {
    }

    public override ItemKind GetKind()
    {
        return ItemKind.Pickaxe;
    }
}