using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mine2d.game.backend.network.packets;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class DebugCommandAttribute : Attribute
{
    public string Usage { get; set; }
    public string Desc { get; set; }

    public DebugCommandAttribute(string usage, string desc)
    {
        this.Usage = usage;
        this.Desc = desc;
    }
}

public enum DebugCommand {
    [DebugCommand("", "Disables gravity and collision")]
    NoClip,

    [DebugCommand("<scale>", "Sets the scale of the world")]
    GameScale,

    [DebugCommand("", "Disables fog")]
    NoFog,

    [DebugCommand(" <item> [amount]", "Gives you an item")]
    Give,

    [DebugCommand("<kind>", "Toggles the debug overlay")]
    Overlay,

    [DebugCommand("", "Lists all commands")]
    Help,

    Unknown
}

public class DebugCommandPacket : Packet
{
    public override PacketType Type => PacketType.DebugCommand;

    public DebugCommand Command { get; set; }
    public string RawCommand { get; set; }
    public string[] Args { get; set; }
}