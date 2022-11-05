class Publisher
{
    private readonly Dictionary<string, HashSet<Delegate>> subscribers =
        new();
    private readonly InteractorKind kind;

    public Publisher(InteractorKind kind)
    {
        this.kind = kind;
        this.scan();
    }

    private void scan()
    {
        var assembly = this.GetType().Assembly;
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            var attrs = type.GetCustomAttributes(typeof(Interactor), false);
            if (attrs.Length == 0)
            {
                continue;
            }
            var methods = type.GetMethods();
            foreach (var method in methods)
            {
                var attrs2 = method.GetCustomAttributes(typeof(Interaction), false);
                if (attrs2.Length == 0)
                {
                    continue;
                }
                var attr = (Interaction)attrs2[0];
                if (attr.Kind != this.kind && this.kind != InteractorKind.Hybrid)
                {
                    continue;
                }
                var del = Delegate.CreateDelegate(
                    typeof(Action<>).MakeGenericType(method.GetParameters()[0].ParameterType),
                    method
                );
                this.subscribe(attr.Type, del);
            }
        }
    }

    private void subscribe(string type, Delegate callback)
    {
        if (!this.subscribers.ContainsKey(type))
        {
            this.subscribers[type] = new HashSet<Delegate>();
        }
        this.subscribers[type].Add(callback);
    }

    public void Dump()
    {
        foreach (var pair in this.subscribers)
        {
            Console.WriteLine(pair.Key);
            foreach (var del in pair.Value)
            {
                Console.WriteLine(del);
            }
        }
    }

    public void Publish(ValueType packet)
    {
        var type = PacketUtils.GetType(packet);
        if (type != "tick")
        {
            Console.WriteLine("Publishing packet: " + type);
        }
        if (this.subscribers.ContainsKey(type))
        {
            if (type != "tick")
            {
                Console.WriteLine("Found " + this.subscribers[type].Count + " subscribers");
            }
            foreach (var del in this.subscribers[type])
            {
                del.DynamicInvoke(packet);
            }
        }
    }
}
