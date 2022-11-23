using Mine2d.engine.system;
using Mine2d.engine.system.annotations;

namespace Mine2d.game.frontend.events;

public class Resize
{
    [EventListener(EventType.WindowResize)]
    public static void OnResize(SDL_Event e)
    {
        var ctx = Context.Get();
        ctx.FrontendGameState.WindowWidth = e.window.data1;
        ctx.FrontendGameState.WindowHeight = e.window.data2;
        var player = ctx.GameState.Players.Find(
            p => p.Id == ctx.FrontendGameState.PlayerGuid
        );
        ctx.FrontendGameState.Camera.CenterOn(player.Position);
    }
}
