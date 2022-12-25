using Mine2d.engine.extensions;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;
using Mine2d.game.core;

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
        if (packet.Command == DebugCommand.Help)
        {
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
}