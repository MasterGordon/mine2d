using Mine2d.engine.system;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;

namespace Mine2d.game.frontend.events;

public class PlayerInput
{
    [EventListener(EventType.KeyDown)]
    public static void Move(SDL_Event e)
    {
        var ctx = Context.Get();
        if (!IsMovementKey(e.key.keysym.scancode) || e.key.repeat == 1)
        {
            return;
        }
        var movementInput = ctx.FrontendGameState.MovementInput;
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_A)
        {
            movementInput.X -= 1;
        }
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_D)
        {
            movementInput.X += 1;
        }
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_W)
        {
            movementInput.Y -= 1;
        }
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_S)
        {
            movementInput.Y += 1;
        }
        ctx.FrontendGameState.MovementInput = movementInput;
        SendMovement();
    }

    [EventListener(EventType.KeyUp)]
    public static void StopMove(SDL_Event e)
    {
        var ctx = Context.Get();
        if (!IsMovementKey(e.key.keysym.scancode) || e.key.repeat == 1)
        {
            return;
        }
        var movementInput = ctx.FrontendGameState.MovementInput;
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_A)
        {
            movementInput.X += 1;
        }
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_D)
        {
            movementInput.X -= 1;
        }
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_W)
        {
            movementInput.Y += 1;
        }
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_S)
        {
            movementInput.Y -= 1;
        }
        ctx.FrontendGameState.MovementInput = movementInput;
        SendMovement();
    }

    private static bool IsMovementKey(SDL_Scancode scancode)
    {
        return scancode is SDL_Scancode.SDL_SCANCODE_A
            or SDL_Scancode.SDL_SCANCODE_D
            or SDL_Scancode.SDL_SCANCODE_W
            or SDL_Scancode.SDL_SCANCODE_S;
    }

    private static void SendMovement()
    {
        var ctx = Context.Get();
        var movement = ctx.FrontendGameState.MovementInput;
        if (movement.Length() > 0)
        {
            movement = Vector2.Normalize(movement);
        }
        ctx.Backend.ProcessPacket(new MovePacket
        {
            PlayerName = ctx.FrontendGameState.PlayerName,
            Movement = movement
        });
    }
}
