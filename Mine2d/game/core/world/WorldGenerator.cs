using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine2d.game.core.tiles;

namespace Mine2d.game.core.world;

public class GenerationSettings {
    public int xOffset { get; set; }
    public int yOffset { get; set; }
    public Tiles tile { get; set; }

    public int GetWeight(int height) {
        return (int)((-Math.Pow(height - this.xOffset, 2)*0.01) + this.yOffset + (32*10));
    }
}

public class WorldGenerator
{
    private readonly List<GenerationSettings> settings = new ();

    public WorldGenerator() {
        this.settings.Add(new GenerationSettings {
            xOffset = 10,
            yOffset = 15,
            tile = Tiles.IronOre
        });
        this.settings.Add(new GenerationSettings {
            xOffset = 15,
            yOffset = 20,
            tile = Tiles.CopperOre
        });
        this.settings.Add(new GenerationSettings {
            xOffset = 20,
            yOffset = 10,
            tile = Tiles.TinOre
        });
        this.settings.Add(new GenerationSettings {
            xOffset = 40,
            yOffset = 3,
            tile = Tiles.SilverOre
        });
        this.settings.Add(new GenerationSettings {
            xOffset = 40,
            yOffset = 3,
            tile = Tiles.GoldOre
        });
        this.settings.Add(new GenerationSettings {
            xOffset = 50,
            yOffset = 15,
            tile = Tiles.LeadOre
        });
        this.settings.Add(new GenerationSettings {
            xOffset = 60,
            yOffset = 2,
            tile = Tiles.PlatinumOre
        });
        this.settings.Add(new GenerationSettings {
            xOffset = 60,
            yOffset = 3,
            tile = Tiles.CobaltOre
        });
        this.settings.Add(new GenerationSettings {
            xOffset = 65,
            yOffset = 10,
            tile = Tiles.TungstenOre
        });
        this.settings.Add(new GenerationSettings {
            xOffset = 90,
            yOffset = 1,
            tile = Tiles.DiamondOre
        });
        this.settings.Add(new GenerationSettings {
            xOffset = 94,
            yOffset = 2,
            tile = Tiles.UraniumOre
        });
    }

    public Tiles GetRandomOreAt(int height) {
        var random = new Random();
        var weight = random.Next(0, 4000);
        var ore = this.settings.FirstOrDefault(x => x.GetWeight(height) > weight, null);
        if(ore == null) {
            return Tiles.Stone;
        }
        return ore.tile;
    }
}