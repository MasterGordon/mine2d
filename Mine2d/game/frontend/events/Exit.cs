using Mine2d.engine.system;
using Mine2d.engine.system.annotations;

namespace Mine2d.game.frontend.events;

public class Exit
{
    [EventListener(EventType.Quit)]
    public static void OnExit()
    {
        Environment.Exit(0);
    }

    [EventListener(EventType.KeyDown)]
    public static void OnKeyDown(SDL_Event e)
    {
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_ESCAPE)
        {
            Environment.Exit(0);
        }
    }
}
