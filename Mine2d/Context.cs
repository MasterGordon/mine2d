using Mine2d.backend;
using Mine2d.core.tiles;
using Mine2d.engine;
using Mine2d.frontend;
using Mine2d.state;

namespace Mine2d;

public class Context
{
    public bool IsHost { get; set; }
    public IBackend Backend { get; set; }
    public IFrontend Frontend { get; set; }
    public GameState GameState { get; set; }
    public FrontendGameState FrontendGameState { get; set; }
    public Window Window { get; set; }
    public Renderer Renderer { get; set; }
    public TileRegistry TileRegistry { get; set; }
    public ResourceLoader ResourceLoader { get; set; }
    public static Context Instance { get; set; }

    public Context(
        bool isHost,
        IBackend backend,
        IFrontend frontend,
        GameState gameState,
        FrontendGameState frontendGameState,
        Renderer renderer,
        Window window
    )
    {
        this.IsHost = isHost;
        this.Backend = backend;
        this.Frontend = frontend;
        this.GameState = gameState;
        this.FrontendGameState = frontendGameState;
        this.Renderer = renderer;
        this.Window = window;
        this.TileRegistry = new TileRegistry();
        this.ResourceLoader = new ResourceLoader();
        Instance = this;
    }

    public static Context Get()
    {
        if (Instance == null)
        {
            throw new Exception("Context not initialized");
        }

        return Instance;
    }
}
