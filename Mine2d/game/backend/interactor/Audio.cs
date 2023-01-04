using Mine2d.engine;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;
using Mine2d.game.core;

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

    [Interaction(InteractorKind.Client, PacketType.Tick)]
    public static void Tick()
    {
        var ctx = Context.Get();
        if (ctx.FrontendGameState.NextMusicPlay < DateTime.Now)
        {
            ctx.GameAudio.Play(Sound.MusicLoop);
            ctx.FrontendGameState.NextMusicPlay = DateTime.Now.AddSeconds(130);
        }
        if (false && ctx.FrontendGameState.NextStepPlay < DateTime.Now && PlayerEntity.GetSelf().PlayerMovementState.CurrentVelocity != Vector2.Zero)
        {
            ctx.GameAudio.Play(GetRandomStepSound());
            ctx.FrontendGameState.NextStepPlay = DateTime.Now.AddSeconds(0.2);
        }
    }

    private static Sound GetRandomStepSound()
    {
        var sound = new Random().NextInt64(0, 6);
        return sound switch
        {
            0 => Sound.Step0,
            1 => Sound.Step1,
            2 => Sound.Step2,
            3 => Sound.Step3,
            4 => Sound.Step4,
            5 => Sound.Step5,
            _ => Sound.Step0
        };
    }
}
