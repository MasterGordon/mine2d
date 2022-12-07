using Mine2d.engine;
using Mine2d.engine.networking;
using Mine2d.game.core.items;
using Mine2d.game.core.tiles;
using Mine2d.game.frontend;
using Mine2d.game.state;

namespace Mine2d.game;

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
    public ItemRegistry ItemRegistry { get; set; }
    public ResourceLoader ResourceLoader { get; set; }
    public TextureFactory TextureFactory { get; set; }
    public GameAudio GameAudio { get; set; }
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
        this.ResourceLoader = new ResourceLoader();
        this.TextureFactory = new TextureFactory(this.ResourceLoader, this.Renderer);
        this.TileRegistry = new TileRegistry();
        this.ItemRegistry = new ItemRegistry();
        this.GameAudio = new GameAudio();
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
