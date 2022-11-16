using Mine2d;
using Mine2d.backend.data;
using Mine2d.engine.system;
using Mine2d.engine.system.annotations;

namespace Mine2d.frontend.events;

public class PlayerBreakInput
{
    [EventListener(EventType.MouseMotion)]
    public static void onBreak(SDL_Event e)
    {
        var ctx = Context.Get();
        var mousePos = new Vector2(e.motion.x, e.motion.y);
        ctx.FrontendGameState.MousePosition = mousePos;
        if (ctx.GameState.Players.Find(player => player.Id == ctx.FrontendGameState.PlayerGuid)?.Mining
            != Vector2.Zero)
        {
            var amp = ctx.FrontendGameState.MousePosition
                / ctx.FrontendGameState.Settings.GameScale
                + ctx.FrontendGameState.Camera.Position;
            ctx.Backend.ProcessPacket(new BreakPacket(ctx.FrontendGameState.PlayerGuid, amp));
        }
    }

    [EventListener(EventType.MouseButtonDown)]
    public static void onMouseDown(SDL_Event e)
    {
        if (e.button.button != SDL_BUTTON_LEFT)
        {
            return;
        }

        var ctx = Context.Get();
        var amp = ctx.FrontendGameState.MousePosition / ctx.FrontendGameState.Settings.GameScale + ctx.FrontendGameState.Camera.Position;
        ctx.Backend.ProcessPacket(new BreakPacket(ctx.FrontendGameState.PlayerGuid, amp));
    }

    [EventListener(EventType.MouseButtonUp)]
    public static void onMouseUp(SDL_Event e)
    {
        if (e.button.button != SDL_BUTTON_LEFT)
        {
            return;
        }

        var ctx = Context.Get();
        ctx.Backend.ProcessPacket(new BreakPacket(ctx.FrontendGameState.PlayerGuid, Vector2.Zero));
    }
}
