using System.Text;
using mine2d.backend.data;
using mine2d.engine.system.annotations;
using mine2d.network;
using Newtonsoft.Json;
using WatsonTcp;

namespace mine2d.backend;

class Backend : IBackend
{
    private WatsonTcpServer server;
    private Publisher publisher;
    private Queue<ValueType> pendingPackets = new();
    private uint tick;

    public void Process(double dt)
    {
        this.ProcessPacket(new TickPacket(this.tick++));
        while (this.pendingPackets.Count > 0)
        {
            var packet = this.pendingPackets.Dequeue();
            this.publisher.Publish(packet);
        }
        this.SendGameState();
    }

    public void ProcessPacket(ValueType packet)
    {
        this.pendingPackets.Enqueue(packet);
    }

    public void Init()
    {
        Task.Run(this.Run);
        this.publisher = new Publisher(InteractorKind.Hybrid);
    }

    public void Run()
    {
        this.server = new WatsonTcpServer("127.0.0.1", 42069);
        this.server.Events.ClientConnected += this.ClientConnected;
        this.server.Events.ClientDisconnected += this.ClientDisconnected;
        this.server.Events.MessageReceived += this.MessageReceived;
        this.server.Start();
    }

    private void ClientConnected(object sender, ConnectionEventArgs args)
    {
        Console.WriteLine("Client connected: " + args.IpPort);
        var gameState = Context.Get().GameState;
        var json = JsonConvert.SerializeObject(gameState);
        if (sender is WatsonTcpServer server)
        {
            server.Send(args.IpPort, Encoding.UTF8.GetBytes(json));
        }
    }

    private void ClientDisconnected(object sender, DisconnectionEventArgs args)
    {
        Console.WriteLine("Client disconnected: " + args.IpPort);
    }

    private void MessageReceived(object sender, MessageReceivedEventArgs args)
    {
        var time = DateTime.Now;
        Console.WriteLine("Message Received: " + args.IpPort);
        var packet = Converter.ParsePacket(args.Data);
        Console.WriteLine("Received packet: " + packet);
        if (packet != null)
        {
            this.pendingPackets.Enqueue(packet);
        }
        Console.WriteLine(DateTime.Now - time);
    }

    private void SendGameState()
    {
        if (this.server == null)
            return;
        var clients = this.server.ListClients().ToArray();
        if (!clients.Any())
            return;
        var gameState = Context.Get().GameState;
        var json = JsonConvert.SerializeObject(gameState);
        var bytes = Encoding.UTF8.GetBytes(json);
        foreach (var client in clients)
        {
            this.server.Send(client, bytes);
        }
    }
}