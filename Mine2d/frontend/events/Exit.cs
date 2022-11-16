using Mine2d.engine.system;
using Mine2d.engine.system.annotations;

namespace Mine2d.frontend.events;

public class Exit
{
    [EventListener(EventType.Quit)]
    public static void onExit()
    {
        Environment.Exit(0);
    }

    [EventListener(EventType.KeyDown)]
    public static void onKeyDown(SDL_Event e)
    {
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_ESCAPE)
        {
            Environment.Exit(0);
        }
    }
}
