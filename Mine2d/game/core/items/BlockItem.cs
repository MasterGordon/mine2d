using Mine2d.game.core.data;
using Mine2d.game.state;

namespace Mine2d.game.core.items;

public class BlockItem : Item
{
    public BlockItem(ItemId id, string name, string[] textureName) : base(id, name, textureName)
    {
    }

    public BlockItem(ItemId id, string name, string textureName) : base(id, name, textureName)
    {
    }

    public override void Interact(ItemStack stack, Vector2 target, Player player)
    {
        var ctx = Context.Get();
        if ((player.GetCenter() - target).LengthSquared() > Constants.BreakDistance * Constants.BreakDistance)
        {
            return;
        }
        if (PlayerEntity.HasCollisionWithAnyPlayer(target))
        {
            return;
        }
        if (ctx.GameState.World.HasChunkAt(target))
        {
            var chunk = ctx.GameState.World.GetChunkAt(target);
            var tile = chunk.GetTileAt(target);

            var tileId = tile.Id;
            if (tileId != 0)
            {
                return;
            }
            stack.Count--;
            var itemId = stack.Id;
            var itemTileId = ctx.TileRegistry.GetTileIdByItemId(itemId);
            chunk.SetTileAt(target, STile.From(itemTileId));
        }
    }
}