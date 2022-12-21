using Mine2d.game.core.data;

namespace Mine2d.game.state;

public class GameState
{
    public List<Player> Players { get; set; } = new List<Player>();
    public World World { get; set; }
    public uint Tick { get; set; }
    public double DeltaTime { get; set; }
}
