using Mine2d.game.core;
using Mine2d.game.core.data;

namespace Mine2d.game.state;

public class FrontendGameState
{
    public Vector2 MovementInput { get; set; }
    public Vector2 CameraPosition { get; set; }
    public int WindowWidth { get; set; }
    public int WindowHeight { get; set; }
    public Guid PlayerGuid { get; set; }
    public Camera Camera { get; set; } = new();
    public Vector2 MousePosition { get; set; }
    public Settings Settings { get; set; } = new Settings();
    public string PlayerName { get; set; } = "Player";
}

public class Settings
{
    public int GameScale { get; set; } = 4;
    public int UiScale { get; set; } = 4;
    public bool ShowCollision { get; set; } = true;
    public bool Fullscreen { get; set; } = false;
}

public class GameState
{
    public List<Player> Players { get; set; } = new List<Player>();
    public World World { get; set; }
}
