using System.Reflection;
using mine2d.core.extensions;
using mine2d.engine;
using mine2d.engine.system.annotations;

namespace mine2d.backend;

class Publisher
{
    private readonly Dictionary<string, HashSet<Delegate>> subscribers =
        new();
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
                var del = method.GetParameters().Length == 0 ?
                    Delegate.CreateDelegate(typeof(Action), method) :
                    Delegate.CreateDelegate(
                    typeof(Action<>).MakeGenericTypeSafely(method.GetParameters()[0].ParameterType),
                    method
                );
                this.Subscribe(attr.Type, del);
            }
        }
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
}
