using Mine2d.engine;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;

namespace Mine2d.game.backend.interactor;

[Interactor]
public class Audio
{
    [Interaction(InteractorKind.Client, PacketType.BlockBroken)]
    public static void BlockBroken()
    {
        var ctx = Context.Get();
        ctx.GameAudio.Play(Sound.BlockBreak);
    }
}
