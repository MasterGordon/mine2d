using System.Text;
using mine2d.backend.data;
using mine2d.engine.system.annotations;
using mine2d.state;
using Newtonsoft.Json;
using WatsonTcp;

namespace mine2d.backend;

class RemoteBackend : IBackend
{
    private WatsonTcpClient client;
    private Publisher publisher;
    private readonly Queue<ValueType> pendingPackets = new();
    private uint tick = 0;

    public void Process(double dt)
    {
        this.ProcessPacket(new TickPacket(this.tick++));
        while (this.pendingPackets.Count > 0)
        {
            var packet = this.pendingPackets.Dequeue();
            this.ProcessPacket(packet);
        }
    }

    public void ProcessPacket(ValueType packet)
    {
        this.publisher.Publish(packet);
        var json = JsonConvert.SerializeObject(packet);
        var bytes = Encoding.UTF8.GetBytes(json);
        this.client.Send(bytes);
    }

    public void Init()
    {
        Task.Run(this.Run);
        this.publisher = new Publisher(InteractorKind.Client);
    }

    public void Run()
    {
        this.client = new WatsonTcpClient("127.0.0.1", 42069);
        this.client.Events.MessageReceived += (_, args) =>
        {
            var ctx = Context.Get();
            var message = Encoding.UTF8.GetString(args.Data);
            var packet = JsonConvert.DeserializeObject<GameState>(message);
            if (packet != null)
            {
                ctx.GameState = packet;
            }
        };
        this.client.Connect();
    }
}