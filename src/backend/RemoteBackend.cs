using WatsonTcp;
using Newtonsoft.Json;
using System.Text;

class RemoteBackend : IBackend
{
    private WatsonTcpClient client;
    private Publisher publisher;
    private Queue<ValueType> pendingPackets = new Queue<ValueType>();
    private uint tick = 0;

    public void Process(double dt)
    {
        var ctx = Context.Get();
        this.ProcessPacket(new TickPacket(tick++));
        while (pendingPackets.Count > 0)
        {
            var packet = pendingPackets.Dequeue();
            this.ProcessPacket(packet);
        }
    }

    public void ProcessPacket(ValueType packet)
    {
        this.publisher.Publish(packet);
        var json = JsonConvert.SerializeObject(packet);
        var bytes = Encoding.UTF8.GetBytes(json);
        client.Send(bytes);
    }

    public void Init()
    {
        Task.Run(this.Run);
        this.publisher = new Publisher(InteractorKind.Client);
    }

    public void Run()
    {
        client = new WatsonTcpClient("127.0.0.1", 42069);
        client.Events.MessageReceived += (sender, args) =>
        {
            var ctx = Context.Get();
            var message = Encoding.UTF8.GetString(args.Data);
            var packet = JsonConvert.DeserializeObject<GameState>(message);
            if (packet != null)
            {
                ctx.GameState = packet;
            }
        };
        client.Connect();
    }
}
