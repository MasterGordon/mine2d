using Mine2d.engine.system;
using Mine2d.engine.system.annotations;

namespace Mine2d.game.frontend.events;

public class PlayerController
{
    [EventListener(EventType.KeyDown)]
    public static void OnKeyDown(SDL_Event e)
    {
        if (!IsMovementKey(e.key.keysym.scancode))
            return;

        // Context
        //     .Get()
        //     .Backend
        //     .ProcessPacket(new PlayerInputPacket
        //     {
        //         InputVector = e.key.keysym.scancode switch
        //         {
        //             SDL_Scancode.SDL_SCANCODE_A 
        //                 => new Vector2(-1, 0),
        //             SDL_Scancode.SDL_SCANCODE_D 
        //                 => new Vector2(1, 0),
        //             SDL_Scancode.SDL_SCANCODE_W or SDL_Scancode.SDL_SCANCODE_SPACE 
        //                 => new Vector2(0, 1),
        //             SDL_Scancode.SDL_SCANCODE_S 
        //                 => new Vector2(0, -1)
        //         }
        //     });
    }

    [EventListener(EventType.KeyUp)]
    public static void OnKeyUp(SDL_Event e)
    {
        if (!IsMovementKey(e.key.keysym.scancode))
            return;
    }

    private static bool IsMovementKey(SDL_Scancode scancode)
    {
        return scancode 
            is SDL_Scancode.SDL_SCANCODE_A
            or SDL_Scancode.SDL_SCANCODE_D
            or SDL_Scancode.SDL_SCANCODE_W
            or SDL_Scancode.SDL_SCANCODE_S;
    }
}
