using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;

namespace Mine2d.game.backend.interactor;

[Interactor]
public class PlayerInteract
{
    [Interaction(InteractorKind.Server, PacketType.PlayerInteract)]
    public static void InteractServer(PlayerInteractPacket packet)
    {
        var ctx = Context.Get();
        var player = ctx.GameState.Players.Find(p => p.Id == packet.PlayerGuid);
        if (player == null)
        {
            return;
        }
        if (ctx.GameState.World.HasTileAt(packet.Target))
        {
            var tile = ctx.TileRegistry.GetTile(ctx.GameState.World.GetTileAt(packet.Target).Id);
            tile.OnInteract(packet.Target);
            return;
        }
        var stack = player.Inventory.Hotbar[packet.Slot];
        if (stack == null || stack.Count <= 0)
        {
            return;
        }
        ctx.ItemRegistry.GetItem(stack.Id).Interact(stack, packet.Target, player);
    }
}
