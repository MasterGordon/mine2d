using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine2d.game.core.data;

namespace Mine2d.game.core.items;

public class PickaxeItem : Item
{
    private readonly int miningSpeed;
    private readonly int harvestLevel;

    public PickaxeItem(ItemId id, string name, string textureName, int miningSpeed, int harvestLevel) : base(id, name, textureName)
    {
        this.miningSpeed = miningSpeed;
        this.harvestLevel = harvestLevel;
    }

    public override ItemKind GetKind()
    {
        return ItemKind.Pickaxe;
    }

    public int GetMiningSpeed()
    {
        return this.miningSpeed;
    }

    public int GetHarvestLevel()
    {
        return this.harvestLevel;
    }
}