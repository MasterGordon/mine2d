using Mine2d.engine.system;

namespace Mine2d.engine;

public class EventService
{
    private static readonly eventPublisher EventPublisher = new();

    public static void PollEvents()
    {
        while (SDL_PollEvent(out var e) != 0)
        {
            var eventType = e.type switch
            {
                SDL_EventType.SDL_QUIT => EventType.Quit,
                SDL_EventType.SDL_WINDOWEVENT when e.window.windowEvent == SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED => EventType.WindowResize,
                SDL_EventType.SDL_MOUSEMOTION => EventType.MouseMotion,
                SDL_EventType.SDL_MOUSEBUTTONDOWN => EventType.MouseButtonDown,
                SDL_EventType.SDL_MOUSEBUTTONUP => EventType.MouseButtonUp,
                SDL_EventType.SDL_KEYDOWN => EventType.KeyDown,
                SDL_EventType.SDL_KEYUP => EventType.KeyUp,
                SDL_EventType.SDL_MOUSEWHEEL => EventType.MouseWheel,
                SDL_EventType.SDL_TEXTINPUT => EventType.TextInput,
                _ => EventType.Unhandled
            };
            EventPublisher.Publish(eventType, e);
        }
        EventPublisher.Publish(EventType.Tick, new SDL_Event());
    }
}
