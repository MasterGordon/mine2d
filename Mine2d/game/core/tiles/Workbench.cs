using Mine2d.game.core.data;
using Mine2d.game.frontend.inventory;

namespace Mine2d.game.core.tiles;

public class Recipe {
    public ItemStack Result { get; set; }
    public List<ItemStack> Ingredients { get; set; } = new();
}

public class Workbench : Tile
{
    public static List<Recipe> Recipes { get; set; } = new();

    public Workbench(string name, string texturePath, int hardness) : base(name, texturePath, hardness, ItemId.Workbench)
    {
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.PickaxeBasic, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.Stone, 2),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.PickaxeStone, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.Stone, 10),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.PickaxeIron, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.IronIngot, 10),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.PickaxeGold, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.GoldIngot, 10),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.IronIngot, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.Coal, 1),
                new ItemStack(ItemId.RawIron, 1),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.GoldIngot, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.Coal, 1),
                new ItemStack(ItemId.RawGold, 1),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.CopperIngot, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.Coal, 1),
                new ItemStack(ItemId.RawCopper, 1),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.SilverIngot, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.Coal, 1),
                new ItemStack(ItemId.RawSilver, 1),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.CopperWire, 4),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.CopperIngot, 1),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.Silicon, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.Stone, 16),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.CircuitBoard, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.Silicon, 1),
                new ItemStack(ItemId.CopperWire, 1),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.ElectricCircuit, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.CircuitBoard, 1),
                new ItemStack(ItemId.CopperWire, 5),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.Fuse, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.CopperWire, 1),
                new ItemStack(ItemId.Stone, 1),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.Gps, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.ElectricCircuit, 1),
                new ItemStack(ItemId.Fuse, 1),
            }
        });
        Recipes.Add(new Recipe
        {
            Result = new ItemStack(ItemId.Workbench, 1),
            Ingredients = new List<ItemStack>
            {
                new ItemStack(ItemId.Stone, 1),
                new ItemStack(ItemId.Fuse, 1),
                new ItemStack(ItemId.CopperWire, 3),
            }
        });
    }

    public override bool IsSolid()
    {
        return false;
    }

    public override void OnInteract(Vector2 position)
    {
        Context.Get().FrontendGameState.OpenInventory = InventoryKind.Workbench;
    }
}