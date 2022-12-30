using Mine2d.engine.extensions;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;
using Mine2d.game.core;
using Mine2d.game.core.data;

namespace Mine2d.game.backend.interactor;

[Interactor]
public class DebugCommandHandler
{
    [Interaction(InteractorKind.Hybrid, PacketType.DebugCommand)]
    public static void HandleUnknownDebugCommand(DebugCommandPacket packet)
    {
        if (packet.Command == DebugCommand.Unknown)
        {
            Debugger.Print("Unknown debug command '" + packet.RawCommand + "'");
            Debugger.Print("Available commands:");
            var cmds = string.Join(", ", Enum.GetValues<DebugCommand>().Where(command => command != DebugCommand.Unknown).Select(command => command.ToString().ToLower()));
            Debugger.Print(cmds);
        }
    }

    [Interaction(InteractorKind.Hybrid, PacketType.DebugCommand)]
    public static void HandleHelpDebugCommand(DebugCommandPacket packet)
    {
        if (packet.Command != DebugCommand.Help) return;

        var enumType = typeof(DebugCommand);
        Enum.GetValues<DebugCommand>()
            .Where(command => command != DebugCommand.Unknown)
            .ForEach(command =>
            {
                var memberInfo = enumType.GetMember(command.ToString());
                var enumValueMemberInfo = memberInfo[0];
                var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(DebugCommandAttribute), false);
                var attribute = (DebugCommandAttribute)valueAttributes[0];
                Debugger.Print(command.ToString().ToLower() + attribute.Usage + " - " + attribute.Desc);
            });
    }

    [Interaction(InteractorKind.Hybrid, PacketType.DebugCommand)]
    public static void HandleNoClipDebugCommand(DebugCommandPacket packet)
    {
        if (packet.Command == DebugCommand.NoClip)
        {
            var ds = Context.Get().FrontendGameState.DebugState;
            ds.NoClip = !ds.NoClip;
            Debugger.Print("noclip: " + ds.NoClip);
        }
    }

    [Interaction(InteractorKind.Hybrid, PacketType.DebugCommand)]
    public static void HandleNoFogDebugCommand(DebugCommandPacket packet)
    {
        if (packet.Command == DebugCommand.NoFog)
        {
            var ds = Context.Get().FrontendGameState.DebugState;
            ds.NoFog = !ds.NoFog;
            Debugger.Print("nofog: " + ds.NoFog);
        }
    }

    [Interaction(InteractorKind.Hybrid, PacketType.DebugCommand)]
    public static void HandleOverlayDebugCommand(DebugCommandPacket packet)
    {
        if (packet.Command == DebugCommand.Overlay)
        {
            if (packet.Args.Length == 0)
            {
                Debugger.Print("Usage: overlay <kind>");
                return;
            }
            var ds = Context.Get().FrontendGameState.DebugState;
            ds.Overlay = packet.Args[0];
            Debugger.Print("overlay: " + ds.Overlay);
        }
    }

    [Interaction(InteractorKind.Hybrid, PacketType.DebugCommand)]
    public static void HandleGameScaleDebugCommand(DebugCommandPacket packet)
    {
        if (packet.Command == DebugCommand.GameScale)
        {
            if (packet.Args.Length == 0)
            {
                Debugger.Print("Usage: gamescale <scale>");
                return;
            }
            var scale = int.Parse(packet.Args[0]);
            var ds = Context.Get().FrontendGameState.Settings;
            ds.GameScale = scale;
            Debugger.Print("gamescale: " + ds.GameScale);
        }
    }

    [Interaction(InteractorKind.Hybrid, PacketType.DebugCommand)]
    public static void HandleGiveCommand(DebugCommandPacket packet)
    {
        if (packet.Command != DebugCommand.Give) return;
        if (packet.Args.Length == 0)
        {
            Debugger.Print("Usage 'give <item> [amount]'");
            return;
        }
        var ctx = Context.Get();
        var item = int.Parse(packet.Args[0]);
        var amount = packet.Args.Length > 1 ? int.Parse(packet.Args[1]) : 1;
        if (!Enum.IsDefined(typeof(ItemId), item))
        {
            Debugger.Print("Unknown item!");
            return;
        }
        var itemId = (ItemId)Enum.ToObject(typeof(ItemId), item);
        PlayerEntity.GetSelf().Inventory.PickupItemStack(new ItemStack(itemId, amount));
        var registeredItem = ctx.ItemRegistry.GetItem(itemId);
        Debugger.Print("Gave "+amount+"x "+registeredItem.Name);
    }
}