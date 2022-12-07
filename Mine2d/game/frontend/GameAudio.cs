using Mine2d.engine;

namespace Mine2d.game.frontend;

public class GameAudio
{
    private readonly AudioPlayer audioPlayer;

    public GameAudio()
    {
        this.audioPlayer = new();
        this.audioPlayer.Register(Sound.BlockBreak, "assets.audio.block_break.wav");
        this.audioPlayer.Register(Sound.BlockHit, "assets.audio.block_hit.wav");
        this.audioPlayer.Register(Sound.ItemPickup, "assets.audio.item_pickup.wav");
    }

    public void Play(Sound sound)
    {
        this.audioPlayer.Play(sound);
    }
}
