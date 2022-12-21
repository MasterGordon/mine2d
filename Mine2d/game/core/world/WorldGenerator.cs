using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine2d.game.core.tiles;

namespace Mine2d.game.core.world;

public struct GenerationSettings {
    public int xOffset { get; set; }
    public int yOffset { get; set; }
    public Tiles tile { get; set; }

    public int GetWeight(int height) {
        return (int)((-Math.Pow(height - this.xOffset, 2)*0.01) + this.yOffset);
    }
}

public class WorldGenerator
{
    List<GenerationSettings> settings = new ();

    public WorldGenerator() {
        this.settings.Add(new GenerationSettings {
            xOffset = 50,
            yOffset = 16,
            tile = Tiles.Stone
        });
    }
}