namespace Mine2d.engine;

public enum Sound
{
    BlockHit,
    BlockBreak,
    ItemPickup,
    MusicLoop,
    Step0,
    Step1,
    Step2,
    Step3,
    Step4,
    Step5,
}

public class AudioPlayer
{
    private readonly Dictionary<Sound, byte[]> audioFiles = new();
    private readonly ResourceLoader resourceLoader = new();

    public AudioPlayer()
    {
        _ = SDL2.SDL_mixer.Mix_OpenAudio(44100, SDL2.SDL_mixer.MIX_DEFAULT_FORMAT, 2, 2048);
    }

    public void Register(Sound name, string path)
    {
        var buffer = this.resourceLoader.LoadBytes(path);
        this.audioFiles.Add(name, buffer);
    }

    public void Play(Sound name)
    {
        var buffer = this.audioFiles[name];
        var sound = SDL2.SDL_mixer.Mix_QuickLoad_WAV(buffer);

        var ret = SDL2.SDL_mixer.Mix_PlayChannel((int)name, sound, 0);
    }
}
