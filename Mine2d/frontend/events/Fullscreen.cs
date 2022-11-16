using mine2d;
using Mine2d.engine.system;
using Mine2d.engine.system.annotations;

namespace Mine2d.frontend.events;

public class Fullscreen
{
    [EventListener(EventType.KeyDown)]
    public static void onToggleFullscreen(SDL_Event e)
    {
        if (e.key.keysym.scancode != SDL_Scancode.SDL_SCANCODE_F11)
        {
            return;
        }

        var ctx = Context.Get();
        _ = SDL_SetWindowFullscreen(
            ctx.Window.GetRaw(),
            ctx.FrontendGameState.Settings.Fullscreen ? 0 : (uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP);
        ctx.FrontendGameState.Settings.Fullscreen = !ctx.FrontendGameState.Settings.Fullscreen;
    }
}
