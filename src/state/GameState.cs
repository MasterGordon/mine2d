using mine2d.core;
using mine2d.core.data;

namespace mine2d.state;

class FrontendGameState
{
    public Vector2 MovementInput;
    public Vector2 CameraPosition;
    public int WindowWidth;
    public int WindowHeight;
    public Guid PlayerGuid;
    public Camera Camera = new Camera();
    public Vector2 MousePosition;
    public Settings Settings { get; set; } = new Settings();
    public string PlayerName { get; set; } = "Player";
}

class Settings
{
    public int GameScale = 4;
    public int UiScale = 4;
    public bool ShowCollision = true;
    public bool Fullscreen = false;
}

class GameState
{
    public List<Player> Players { get; set; } = new List<Player>();
    public World World { get; set; }
}