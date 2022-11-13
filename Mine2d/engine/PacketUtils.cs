using mine2d.backend.data;

namespace mine2d.engine;

public static class PacketUtils
{
    public static string GetType(ValueType packet)
    {
        var t = packet.GetType();
        var p = t.GetProperty(nameof(IPacket.Type));
        if (p == null)
        {
            throw new ArgumentNullException(nameof(p), "p undef");
        }
        var v = p.GetValue(packet);
        if (v == null)
        {
            throw new ArgumentNullException(nameof(v), "v undef");
        }
        return (string)v;
    }
}
