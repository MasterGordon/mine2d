using Mine2d.game.core.tiles;

namespace Mine2d.game.core.world;

public class GenerationSettings
{
    public int xOffset { get; set; }
    public int yOffset { get; set; }
    public Tiles tile { get; set; }

    public int GetWeight(int height)
    {
        return (int)((-Math.Pow(height - (this.xOffset + (32 * 10)), 2) * 0.001) + this.yOffset);
    }
}

public class WorldGenerator
{
    private readonly List<GenerationSettings> settings = new();

    public WorldGenerator()
    {
        this.settings.Add(new GenerationSettings
        {
            xOffset = 10,
            yOffset = 15,
            tile = Tiles.IronOre
        });
        this.settings.Add(new GenerationSettings
        {
            xOffset = 15,
            yOffset = 20,
            tile = Tiles.CopperOre
        });
        this.settings.Add(new GenerationSettings
        {
            xOffset = 20,
            yOffset = 10,
            tile = Tiles.TinOre
        });
        this.settings.Add(new GenerationSettings
        {
            xOffset = 40,
            yOffset = 3,
            tile = Tiles.SilverOre
        });
        this.settings.Add(new GenerationSettings
        {
            xOffset = 80,
            yOffset = 3,
            tile = Tiles.GoldOre
        });
        this.settings.Add(new GenerationSettings
        {
            xOffset = 90,
            yOffset = 15,
            tile = Tiles.LeadOre
        });
        this.settings.Add(new GenerationSettings
        {
            xOffset = 160,
            yOffset = 3,
            tile = Tiles.PlatinumOre
        });
        this.settings.Add(new GenerationSettings
        {
            xOffset = 160,
            yOffset = 3,
            tile = Tiles.CobaltOre
        });
        this.settings.Add(new GenerationSettings
        {
            xOffset = 165,
            yOffset = 10,
            tile = Tiles.TungstenOre
        });
        this.settings.Add(new GenerationSettings
        {
            xOffset = 290,
            yOffset = 1,
            tile = Tiles.DiamondOre
        });
        this.settings.Add(new GenerationSettings
        {
            xOffset = 294,
            yOffset = 2,
            tile = Tiles.UraniumOre
        });
    }

    public Tiles GetRandomOreAt(int height)
    {
        var random = new Random();
        var ores = new List<Tiles>();
        foreach (var setting in this.settings)
        {
            for (var i = 0; i < setting.GetWeight(height); i++)
            {
                ores.Add(setting.tile);
            }
        }

        var rng = random.Next(0, 700);
        if (rng < ores.Count)
        {
            return ores[rng];
        }
        return Tiles.Stone;
    }
}
