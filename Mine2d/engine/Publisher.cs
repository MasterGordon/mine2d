using System.Reflection;
using Mine2d.engine.networking;
using Mine2d.engine.system.annotations;
using Mine2d.game.core.extensions;

namespace Mine2d.engine;

public class Publisher
{
    private readonly Dictionary<string, HashSet<Delegate>> subscribers =
        new();
    private readonly HashSet<string> clientOnlySubscriptions = new();
    private readonly HashSet<string> serverSubscriptions = new();
    private readonly InteractorKind kind;

    public Publisher(InteractorKind kind)
    {
        this.kind = kind;
        this.Scan();
    }

    private void Scan()
    {
        var types = Assembly
            .GetAssembly(this.GetType())!
            .GetTypesSafe();
        foreach (var type in types)
        {
            var classAttrs = type.GetCustomAttributes(typeof(InteractorAttribute), false);
            if (classAttrs.Length == 0)
            {
                continue;
            }
            var methods = type.GetMethods();
            foreach (var method in methods)
            {
                var methodAttrs = method.GetCustomAttributes(typeof(InteractionAttribute), false);
                if (methodAttrs.Length == 0)
                {
                    continue;
                }
                var attr = (InteractionAttribute)methodAttrs[0];
                if (attr.Kind is InteractorKind.Server or InteractorKind.Hybrid)
                {
                    this.serverSubscriptions.Add(attr.Type);
                }
                if (attr.Kind is InteractorKind.Client)
                {
                    this.clientOnlySubscriptions.Add(attr.Type);
                }
                if (attr.Kind != this.kind && this.kind != InteractorKind.Hybrid)
                {
                    continue;
                }
                var del = method.GetParameters().Length == 0 ?
                    Delegate.CreateDelegate(typeof(Action), method) :
                    Delegate.CreateDelegate(
                    typeof(Action<>).MakeGenericTypeSafely(method.GetParameters()[0].ParameterType),
                    method
                );
                this.Subscribe(attr.Type, del);
            }
        }
        this.clientOnlySubscriptions.ExceptWith(this.serverSubscriptions);
    }

    private void Subscribe(string type, Delegate callback)
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
                if (del.Method.GetParameters().Length == 0)
                {
                    del.DynamicInvoke();
                }
                else
                {
                    del.DynamicInvoke(packet);
                }
            }
        }
    }

    public bool IsClientOnlyPacket(string type)
    {
        return this.clientOnlySubscriptions.Contains(type);
    }

    public bool IsServerPacket(string type)
    {
        return this.serverSubscriptions.Contains(type);
    }
}
