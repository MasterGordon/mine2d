using Mine2d.game.core;

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
}

public class Settings
{
    public int GameScale { get; set; } = 4;
    public int UiScale { get; set; } = 3;
    public bool ShowCollision { get; set; } = true;
    public bool Fullscreen { get; set; } = false;
}