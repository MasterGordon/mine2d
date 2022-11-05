using System.Text;
using Newtonsoft.Json;
using WatsonTcp;

class Backend : IBackend
{
    private WatsonTcpServer server;
    private Publisher publisher;
    private Queue<ValueType> pendingPackets = new();
    private uint tick = 0;

    public void Process(double dt)
    {
        this.ProcessPacket(new TickPacket(this.tick++));
        while (this.pendingPackets.Count > 0)
        {
            var packet = this.pendingPackets.Dequeue();
            this.publisher.Publish(packet);
        }
        this.sendGameState();
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
        this.server.Events.ClientConnected += this.clientConnected;
        this.server.Events.ClientDisconnected += this.clientDisconnected;
        this.server.Events.MessageReceived += this.messageReceived;
        this.server.Start();
    }

    private void clientConnected(object sender, ConnectionEventArgs args)
    {
        Console.WriteLine("Client connected: " + args.IpPort);
        var gameState = Context.Get().GameState;
        var json = JsonConvert.SerializeObject(gameState);
        if (sender is WatsonTcpServer server)
        {
            server.Send(args.IpPort, Encoding.UTF8.GetBytes(json));
        }
    }

    private void clientDisconnected(object sender, DisconnectionEventArgs args)
    {
        Console.WriteLine("Client disconnected: " + args.IpPort);
    }

    private void messageReceived(object sender, MessageReceivedEventArgs args)
    {
        var time = DateTime.Now;
        Console.WriteLine("Message Received: " + args.IpPort);
        var packet = Converter.ParsePacket(args.Data);
        Console.WriteLine("Received packet: " + packet);
        if (packet != null)
        {
            pendingPackets.Enqueue(packet);
        }
        Console.WriteLine(DateTime.Now - time);
    }

    private void sendGameState()
    {
        if (server == null)
            return;
        var clients = server.ListClients();
        if (clients.Count() == 0)
            return;
        var gameState = Context.Get().GameState;
        var json = JsonConvert.SerializeObject(gameState);
        var bytes = Encoding.UTF8.GetBytes(json);
        foreach (var client in clients)
        {
            server.Send(client, bytes);
        }
    }
}
