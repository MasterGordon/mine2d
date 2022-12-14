using Mine2d.game.backend.network.packets;

namespace Mine2d.engine.networking;

public interface IBackend
{
    public void Process(double dt);
    public void ProcessPacket(Packet packet);
    public void Init();
}
