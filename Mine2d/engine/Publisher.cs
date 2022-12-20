using System.Reflection;
using Mine2d.engine.extensions;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;
using Mine2d.game.core.extensions;

namespace Mine2d.engine;

public delegate void InteractionHandler(Packet packet);

public class Publisher
{
    private readonly Dictionary<PacketType, HashSet<MethodInfo>> subscribers = new();
    private readonly HashSet<PacketType> clientOnlySubscriptions = new();
    private readonly HashSet<PacketType> serverSubscriptions = new();
    private readonly InteractorKind kind;

    public Publisher(InteractorKind kind)
    {
        Enum.GetValues<PacketType>()
            .ForEach(type => this.subscribers[type] = new HashSet<MethodInfo>());

        this.kind = kind;
        this.Scan();
    }

    private void Scan()
    {
        Assembly
            .GetAssembly(this.GetType())!
            .GetTypesSafe()
            .Where(t => t.HasAttribute<InteractorAttribute>())
            .SelectMany(t => t.GetMethods())
            .Where(m => m.HasAttribute<InteractionAttribute>())
            .ForEach(method =>
            {
                var attribute = method.GetCustomAttribute<InteractionAttribute>()!;
                Console.WriteLine($"Registering interaction method {method.Name} declared in {method.DeclaringType}");
                Console.WriteLine($"InteractorKind: {attribute.Kind}");
                Console.WriteLine($"PacketType: {attribute.Type}");

                switch (attribute.Kind)
                {
                    case InteractorKind.Hybrid:
                    case InteractorKind.Server:
                        this.serverSubscriptions.Add(attribute.Type);
                        break;
                    case InteractorKind.Client:
                        this.clientOnlySubscriptions.Add(attribute.Type);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(attribute.Kind), attribute.Kind, null);
                }

                if (attribute.Kind == this.kind || this.kind == InteractorKind.Hybrid)
                {
                    this.subscribers[attribute.Type].Add(method);
                    Console.WriteLine("Subscribed!");
                }

            });
    }

    public void Publish(Packet packet)
    {
        if (packet.Type != PacketType.Tick)
            Console.WriteLine($"[{nameof(Publisher)}] Publishing {packet.Type}");

        this.subscribers[packet.Type]
            .ForEach(handler =>
            {
                var parameterCount = handler.GetParameters().Length;
                handler.Invoke(null, parameterCount > 0 ? new object[] { packet } : null);
            });
    }

    public bool IsClientOnlyPacket(Packet packet)
        => this.clientOnlySubscriptions.Contains(packet.Type);

    public bool IsServerPacket(Packet packet)
        => this.serverSubscriptions.Contains(packet.Type);

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
}
