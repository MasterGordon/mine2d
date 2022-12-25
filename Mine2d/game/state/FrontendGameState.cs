using Mine2d.game.core;
using Mine2d.game.frontend.inventory;

namespace Mine2d.game.state;


public class FrontendGameState
{
    public Vector2 MovementInput { get; set; }
    public Vector2 CameraPosition { get; set; }
    public int WindowWidth { get; set; }
    public int WindowHeight { get; set; }
    public Guid PlayerGuid { get; set; }
    public Camera Camera { get; set; } = new();
    public Vector2 CursorPosition { get; set; }
    public Settings Settings { get; set; } = new Settings();
    public string PlayerName { get; set; } = "Player";
    public int HotbarIndex { get; set; }
    public string Tooltip { get; set; } = "Test";
    public InventoryKind OpenInventory { get; set; } = InventoryKind.None;
    public InputState InputState { get; set; } = new();
    public DebugState DebugState { get; set; } = new();
}

public class DebugState {
    public Queue<string> Messages { get; set; } = new();
    public string ConsoleInput { get; set; } = "";
    public List<string> ConsoleHistory { get; set; } = new();
    public int ConsoleHistoryIndex { get; set; } = 0;
    public bool NoClip { get; set; } = false;
}

public class Settings
{
    public int GameScale { get; set; } = 6;
    public int UiScale { get; set; } = 4;
    public bool ShowCollision { get; set; } = true;
    public bool Fullscreen { get; set; } = false;
}