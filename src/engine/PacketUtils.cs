class PacketUtils
{
    public static string GetType(ValueType packet)
    {
        var t = packet.GetType();
        foreach (var pp in t.GetProperties())
        {
            Console.WriteLine(pp.Name);
        }
        var p = t.GetField("type");
        if (p == null)
        {
            throw new Exception("p undef");
        }
        var v = p.GetValue(packet);
        if (v == null)
        {
            throw new Exception("v undef");
        }
        return (string)v;
    }
}
