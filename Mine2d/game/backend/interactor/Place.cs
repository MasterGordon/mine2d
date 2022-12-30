using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;
using Mine2d.game.core;
using Mine2d.game.core.data;
using Mine2d.game.core.tiles;
using Mine2d.game.frontend.inventory;

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
        var stack = player.Inventory.Hotbar[packet.Slot];
        if(stack == null || stack.Count <= 0)
        {
            return;
        }
        ctx.ItemRegistry.GetItem(stack.Id).Interact(stack, packet.Target, player);
    }
}
