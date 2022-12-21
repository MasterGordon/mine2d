using Mine2d.engine.system;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;

namespace Mine2d.game.frontend.events;

public class PlayerPlaceInput
{
    [EventListener(EventType.MouseButtonDown)]
    public static void OnMouseDown(SDL_Event e)
    {
        if (e.button.button != SDL_BUTTON_RIGHT)
        {
            return;
        }

        var ctx = Context.Get();
        var amp = ctx.FrontendGameState.CursorPosition
            / ctx.FrontendGameState.Settings.GameScale
            + ctx.FrontendGameState.Camera.Position;

        ctx.Backend.ProcessPacket(new PlacePacket
        {
            PlayerGuid = ctx.FrontendGameState.PlayerGuid,
            Target = amp,
            Slot = ctx.FrontendGameState.HotbarIndex
        });
    }
}
