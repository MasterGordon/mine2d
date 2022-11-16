using Mine2d;
using Mine2d.backend.data;
using Mine2d.engine.system;
using Mine2d.engine.system.annotations;

namespace Mine2d.frontend.events;

public class PlayerInput
{
    [EventListener(EventType.KeyDown)]
    public static void move(SDL_Event e)
    {
        var ctx = Context.Get();
        if (!isMovementKey(e.key.keysym.scancode) || e.key.repeat == 1)
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
        sendMovement();
    }

    [EventListener(EventType.KeyUp)]
    public static void stopMove(SDL_Event e)
    {
        var ctx = Context.Get();
        if (!isMovementKey(e.key.keysym.scancode) || e.key.repeat == 1)
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
        sendMovement();
    }

    private static bool isMovementKey(SDL_Scancode scancode)
    {
        return scancode is SDL_Scancode.SDL_SCANCODE_A
            or SDL_Scancode.SDL_SCANCODE_D
            or SDL_Scancode.SDL_SCANCODE_W
            or SDL_Scancode.SDL_SCANCODE_S;
    }

    private static void sendMovement()
    {
        var ctx = Context.Get();
        var movement = ctx.FrontendGameState.MovementInput;
        if (movement.Length() > 0)
        {
            movement = Vector2.Normalize(movement);
        }
        ctx.Backend.ProcessPacket(new MovePacket(ctx.FrontendGameState.PlayerName, movement));
    }
}
