using Mine2d;
using Mine2d.backend.data;
using Mine2d.engine.system;

namespace Mine2d.frontend;

public class EventService
{
    private static readonly EventPublisher eventPublisher = new();

    public static void PollEvents()
    {
        while (SDL_PollEvent(out var e) != 0)
        {
            if (e.type == SDL_EventType.SDL_QUIT)
            {
                eventPublisher.Publish(EventType.Quit, e);
            }
            if (e.type == SDL_EventType.SDL_WINDOWEVENT && e.window.windowEvent == SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED)
            {
                eventPublisher.Publish(EventType.WindowResize, e);
            }
            if (e.type == SDL_EventType.SDL_MOUSEMOTION)
            {
                eventPublisher.Publish(EventType.MouseMotion, e);
            }
            if (e.type == SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL_BUTTON_LEFT)
            {
                eventPublisher.Publish(EventType.MouseButtonDown, e);
            }
            if (e.type == SDL_EventType.SDL_MOUSEBUTTONUP && e.button.button == SDL_BUTTON_LEFT)
            {
                eventPublisher.Publish(EventType.MouseButtonUp, e);
            }
            if (e.type == SDL_EventType.SDL_KEYDOWN)
            {
                eventPublisher.Publish(EventType.KeyDown, e);
            }
            if (e.type == SDL_EventType.SDL_KEYUP)
            {
                eventPublisher.Publish(EventType.KeyUp, e);
            }
        }
    }
}
