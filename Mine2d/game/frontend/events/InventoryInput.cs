using Mine2d.engine;
using Mine2d.engine.system;
using Mine2d.engine.system.annotations;
using Mine2d.game.state;

namespace Mine2d.game.frontend.events;

public class InventoryInput
{
    [EventListener(EventType.KeyDown)]
    public static void OnKeyDownSwitchHotbar(SDL_Event e)
    {
        var frontendGameState = Context.Get().FrontendGameState;
        frontendGameState.HotbarIndex = e.key.keysym.sym switch
        {
            SDL_Keycode.SDLK_1 => 0,
            SDL_Keycode.SDLK_2 => 1,
            SDL_Keycode.SDLK_3 => 2,
            SDL_Keycode.SDLK_4 => 3,
            SDL_Keycode.SDLK_5 => 4,
            SDL_Keycode.SDLK_6 => 5,
            SDL_Keycode.SDLK_7 => 6,
            SDL_Keycode.SDLK_8 => 7,
            SDL_Keycode.SDLK_9 => 8,
            _ => frontendGameState.HotbarIndex
        };
    }

    [EventListener(EventType.MouseWheel)]
    public static void OnMouseWheel(SDL_Event e)
    {
        var frontendGameState = Context.Get().FrontendGameState;
        frontendGameState.HotbarIndex -= e.wheel.y;
        if (frontendGameState.HotbarIndex < 0)
        {
            frontendGameState.HotbarIndex = 8;
        }
        if (frontendGameState.HotbarIndex > 8)
        {
            frontendGameState.HotbarIndex = 0;
        }
    }

    [EventListener(EventType.KeyDown, EventPriority.Highest)]
    public static void OnKeyDownOpenInventory(SDL_Event e)
    {
        var frontendGameState = Context.Get().FrontendGameState;
        if(e.key.keysym.sym == SDL_Keycode.SDLK_TAB)
        {
            if(frontendGameState.OpenInventory != InventoryKind.Player) {
                frontendGameState.OpenInventory = InventoryKind.Player;
            } else {
                frontendGameState.OpenInventory = InventoryKind.None;
            }
        }
        if(frontendGameState.OpenInventory != InventoryKind.None)
        {
            throw new CancelEventException();
        }
    }

    [EventListener(EventType.KeyUp, EventPriority.Highest)]
    public static void OnKeyUpOpenInventory(SDL_Event e)
    {
        var frontendGameState = Context.Get().FrontendGameState;
        if(frontendGameState.OpenInventory != InventoryKind.None)
        {
            throw new CancelEventException();
        }
    }

    [EventListener(EventType.MouseButtonDown, EventPriority.Highest)]
    public static void OnMouseButtonDownOpenInventory(SDL_Event e)
    {
        var frontendGameState = Context.Get().FrontendGameState;
        Context.Get().InventoryRegistry.GetInventory(frontendGameState.OpenInventory)?.OnClick(e);
        if(frontendGameState.OpenInventory != InventoryKind.None)
        {
            throw new CancelEventException();
        }
    }
}
