using System.Runtime.InteropServices;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;

namespace Mine2d.game.state;

public enum InputAxis
{
    Horizontal,
    Vertical
}

public enum KeyEventType
{
    KeyPressed,
    KeyReleased
}

[Interactor]
public class InputState
{
    [Interaction(InteractorKind.Client, PacketType.Tick)]
    public static void OnTick()
    {
        Context.Get()
            .FrontendGameState
            .InputState
            .UpdateInput();
    }
    
    private readonly byte[] keys;

    public InputState()
    {
        var keysPtr = SDL_GetKeyboardState(out var keyCount);
        this.keys = new byte[keyCount];
        Marshal.Copy(keysPtr, this.keys, 0, keyCount);
    }
    
    public void UpdateInput()
    {
        var keysPtr = SDL_GetKeyboardState(out var keyCount);
        Marshal.Copy(keysPtr, this.keys, 0, keyCount);
    }
    
    public float GetAxis(InputAxis axis)
    {
        return axis switch
        {
            InputAxis.Horizontal 
                => this.keys[(int)SDL_Scancode.SDL_SCANCODE_D] - this.keys[(int)SDL_Scancode.SDL_SCANCODE_A],
            InputAxis.Vertical 
                => this.keys[(int)SDL_Scancode.SDL_SCANCODE_S] - this.keys[(int)SDL_Scancode.SDL_SCANCODE_W],
            _ => 0
        };
    }
}