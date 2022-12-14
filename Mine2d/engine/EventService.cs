using Mine2d.engine.system;

namespace Mine2d.engine;

public class EventService
{
    private static readonly EventPublisher EventPublisher = new();

    public static void PollEvents()
    {
        while (SDL_PollEvent(out var e) != 0)
        {
            if (e.type == SDL_EventType.SDL_QUIT)
            {
                EventPublisher.Publish(EventType.Quit, e);
            }
            if (e.type == SDL_EventType.SDL_WINDOWEVENT && e.window.windowEvent == SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED)
            {
                EventPublisher.Publish(EventType.WindowResize, e);
            }
            if (e.type == SDL_EventType.SDL_MOUSEMOTION)
            {
                EventPublisher.Publish(EventType.MouseMotion, e);
            }
            if (e.type == SDL_EventType.SDL_MOUSEBUTTONDOWN)
            {
                EventPublisher.Publish(EventType.MouseButtonDown, e);
            }
            if (e.type == SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                EventPublisher.Publish(EventType.MouseButtonUp, e);
            }
            if (e.type == SDL_EventType.SDL_KEYDOWN)
            {
                EventPublisher.Publish(EventType.KeyDown, e);
            }
            if (e.type == SDL_EventType.SDL_KEYUP)
            {
                EventPublisher.Publish(EventType.KeyUp, e);
            }
            if (e.type == SDL_EventType.SDL_MOUSEWHEEL)
            {
                EventPublisher.Publish(EventType.MouseWheel, e);
            }
        }
    }
}
