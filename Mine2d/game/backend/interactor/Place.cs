using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;
using Mine2d.game.core;
using Mine2d.game.core.data;
using Mine2d.game.core.tiles;
using Mine2d.game.frontend.inventory;
using Mine2d.game.state;

namespace Mine2d.game.backend.interactor;

[Interactor]
public class Place
{
    [Interaction(InteractorKind.Server, PacketType.Place)]
    public static void PlaceServer(PlacePacket packet)
    {
        var ctx = Context.Get();
        var player = ctx.GameState.Players.Find(p => p.Id == packet.PlayerGuid);
        if (player == null)
        {
            return;
        }
        if ((player.GetCenter() - packet.Target).LengthSquared() > Constants.BreakDistance * Constants.BreakDistance)
        {
            return;
        }
        if (PlayerEntity.HasCollisionWithAnyPlayer(packet.Target))
        {
            return;
        }
        if (ctx.GameState.World.HasChunkAt(packet.Target))
        {
            var chunk = ctx.GameState.World.GetChunkAt(packet.Target);
            var tile = chunk.GetTileAt(packet.Target);
            
            var tileId = tile.Id;
            if(tileId == (int)Tiles.Workbench) {
                ctx.FrontendGameState.OpenInventory = InventoryKind.Workbench;
            }
            if (tileId != 0 || player.Inventory.Hotbar[packet.Slot] == null || player.Inventory.Hotbar[packet.Slot]?.Count <= 0)
            {
                return;
            }
            player.Inventory.Hotbar[packet.Slot].Count--;
            var itemId = player.Inventory.Hotbar[packet.Slot].Id;
            if (player.Inventory.Hotbar[packet.Slot].Count <= 0)
            {
                player.Inventory.Hotbar[packet.Slot] = null;
            }
            var itemTileId = ctx.TileRegistry.GetTileIdByItemId(itemId);
            chunk.SetTileAt(packet.Target, STile.From(itemTileId));
        }
    }
}
