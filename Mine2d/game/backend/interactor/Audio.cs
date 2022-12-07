using Mine2d.engine;
using Mine2d.engine.system.annotations;

namespace Mine2d.game.backend.interactor;

[Interactor]
public class Audio
{
    [Interaction(InteractorKind.Client, "blockBroken")]
    public static void BlockBroken()
    {
        var ctx = Context.Get();
        ctx.GameAudio.Play(Sound.BlockBreak);
    }
}
