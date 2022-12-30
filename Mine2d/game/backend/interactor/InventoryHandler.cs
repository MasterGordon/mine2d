using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;

namespace Mine2d.game.backend.interactor;

[Interactor]
public class InventoryHandler
{
    [Interaction(InteractorKind.Server, PacketType.Tick)]
    public static void onServerTick() {
        var ctx = Context.Get();
        var players = ctx.GameState.Players;
        foreach(var player in players) {
            var inventory = player.Inventory.Inventory;
            var hotbar = player.Inventory.Hotbar;
            // Remove empty stacks
            for(var i = 0; i < hotbar.Length; i++) {
                if(hotbar[i]?.Count <= 0) {
                    hotbar[i] = null;
                }
            }
            for (var i = 0; i< inventory.Length; i++) {
                if(inventory[i]?.Count <= 0) {
                    inventory[i] = null;
                }
            }
        }
    }
}