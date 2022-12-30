using Mine2d.engine.system;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;
using Mine2d.game.core;
using Mine2d.game.core.data;
using Mine2d.game.core.data.entities;
using Mine2d.game.frontend.inventory;

namespace Mine2d.game.frontend.events;

public class DebugInput
{
    [EventListener(EventType.KeyDown, Priority = EventPriority.Important)]
    public static void OnKeyDown(SDL_Event e)
    {
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_F10)
        {
            if (Context.Get().FrontendGameState.OpenInventory == InventoryKind.None)
            {
                Context.Get().FrontendGameState.OpenInventory = InventoryKind.DebugConsole;
            }
            else
            {
                Context.Get().FrontendGameState.OpenInventory = InventoryKind.None;
            }
        }
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_BACKSPACE)
        {
            var frontendGameState = Context.Get().FrontendGameState;
            if (frontendGameState.OpenInventory == InventoryKind.DebugConsole)
            {
                if (frontendGameState.DebugState.ConsoleInput.Length > 0)
                {
                    frontendGameState.DebugState.ConsoleInput = frontendGameState.DebugState.ConsoleInput[0..^1];
                }
            }
        }
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_RETURN)
        {
            var frontendGameState = Context.Get().FrontendGameState;
            if (frontendGameState.OpenInventory == InventoryKind.DebugConsole)
            {
                frontendGameState.DebugState.Messages.Enqueue("> " + frontendGameState.DebugState.ConsoleInput);
                var split = frontendGameState.DebugState.ConsoleInput.Split(' ');
                var Command = split[0].ToLower() switch
                {
                    "give" => DebugCommand.Give,
                    "help" => DebugCommand.Help,
                    "noclip" => DebugCommand.NoClip,
                    "nofog" => DebugCommand.NoFog,
                    "overlay" => DebugCommand.Overlay,
                    "gamescale" => DebugCommand.GameScale,
                    _ => DebugCommand.Unknown
                };
                var Args = split.Length > 1 ? split[1..] : Array.Empty<string>();

                Context.Get().Backend.ProcessPacket(new DebugCommandPacket
                {
                    Command = Command,
                    Args = Args,
                    RawCommand = split[0]
                });
                frontendGameState.DebugState.ConsoleHistory.Add(frontendGameState.DebugState.ConsoleInput);
                frontendGameState.DebugState.ConsoleHistoryIndex = frontendGameState.DebugState.ConsoleHistory.Count;
                frontendGameState.DebugState.ConsoleInput = "";
            }
        }
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_UP)
        {
            var frontendGameState = Context.Get().FrontendGameState;
            if (frontendGameState.OpenInventory == InventoryKind.DebugConsole)
            {
                if (frontendGameState.DebugState.ConsoleHistoryIndex > 0)
                {
                    frontendGameState.DebugState.ConsoleHistoryIndex--;
                    if (frontendGameState.DebugState.ConsoleHistoryIndex < frontendGameState.DebugState.ConsoleHistory.Count)
                        frontendGameState.DebugState.ConsoleInput = frontendGameState.DebugState.ConsoleHistory[frontendGameState.DebugState.ConsoleHistoryIndex];
                }
            }
        }
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_DOWN)
        {
            var frontendGameState = Context.Get().FrontendGameState;
            if (frontendGameState.OpenInventory == InventoryKind.DebugConsole)
            {
                if (frontendGameState.DebugState.ConsoleHistoryIndex < frontendGameState.DebugState.ConsoleHistory.Count)
                {
                    frontendGameState.DebugState.ConsoleHistoryIndex++;
                    if (frontendGameState.DebugState.ConsoleHistoryIndex < frontendGameState.DebugState.ConsoleHistory.Count)
                        frontendGameState.DebugState.ConsoleInput = frontendGameState.DebugState.ConsoleHistory[frontendGameState.DebugState.ConsoleHistoryIndex];
                    else if (frontendGameState.DebugState.ConsoleHistoryIndex == frontendGameState.DebugState.ConsoleHistory.Count && frontendGameState.DebugState.ConsoleHistory[frontendGameState.DebugState.ConsoleHistoryIndex - 1] == frontendGameState.DebugState.ConsoleInput)
                        frontendGameState.DebugState.ConsoleInput = "";
                }
            }
        }
    }

    [EventListener(EventType.TextInput)]
    public static void OnTextInput(SDL_Event e)
    {
        var frontendGameState = Context.Get().FrontendGameState;
        if (frontendGameState.OpenInventory == InventoryKind.DebugConsole)
        {
            unsafe
            {
                var bytes = e.text.text;
                var text = System.Text.Encoding.UTF8.GetString(bytes, 32);
                frontendGameState.DebugState.ConsoleInput += text[0];
            }
        }
    }

    [EventListener(EventType.Tick)]
    public static void OnTick()
    {
        var frontendGameState = Context.Get().FrontendGameState;
        while (frontendGameState.DebugState.Messages.Count > 30)
        {
            var _ = frontendGameState.DebugState.Messages.Dequeue();
        }
    }
}
