using Mine2d.engine;

namespace Mine2d.game.frontend;

public class GameAudio
{
    private readonly AudioPlayer audioPlayer;

    public GameAudio()
    {
        this.audioPlayer = new();
        this.audioPlayer.Register(Sound.BlockBreak, "assets.audio.block_break.wav");
        this.audioPlayer.Register(Sound.BlockHit, "assets.audio.block_hit_alt.wav");
        this.audioPlayer.Register(Sound.ItemPickup, "assets.audio.item_pickup.wav");
        this.audioPlayer.Register(Sound.MusicLoop, "assets.audio.music-loop.wav");
        this.audioPlayer.Register(Sound.Step0, "assets.audio.step0.wav");
        this.audioPlayer.Register(Sound.Step1, "assets.audio.step1.wav");
        this.audioPlayer.Register(Sound.Step2, "assets.audio.step2.wav");
        this.audioPlayer.Register(Sound.Step3, "assets.audio.step3.wav");
        this.audioPlayer.Register(Sound.Step4, "assets.audio.step4.wav");
        this.audioPlayer.Register(Sound.Step5, "assets.audio.step5.wav");
    }

    public void Play(Sound sound)
    {
        this.audioPlayer.Play(sound);
    }
}
