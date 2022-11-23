using Mine2d.engine.system;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.data;

namespace Mine2d.game.frontend.events;

public class PlayerBreakInput
{
    [EventListener(EventType.MouseMotion)]
    public static void OnBreak(SDL_Event e)
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
    public static void OnMouseDown(SDL_Event e)
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
    public static void OnMouseUp(SDL_Event e)
    {
        if (e.button.button != SDL_BUTTON_LEFT)
        {
            return;
        }

        var ctx = Context.Get();
        ctx.Backend.ProcessPacket(new BreakPacket(ctx.FrontendGameState.PlayerGuid, Vector2.Zero));
    }
}
