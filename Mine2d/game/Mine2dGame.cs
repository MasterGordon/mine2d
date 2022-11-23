using Mine2d.engine;
using Mine2d.engine.networking;
using Mine2d.game.core;
using Mine2d.game.state;

namespace Mine2d.game;

public class Mine2dGame : Game
{
    private readonly Context ctx;

    public Mine2dGame(bool isHost)
    {
        var window = new Window("MultiPlayerGame" + (isHost ? " - host" : ""), 1200, 800);
        this.ctx = isHost
            ? new Context(
                isHost,
                new Backend(),
                new Frontend(),
                new GameState(),
                new FrontendGameState(),
                new Renderer(window),
                window
            )
            : new Context(
                isHost,
                new RemoteBackend(),
                new Frontend(),
                new GameState(),
                new FrontendGameState(),
                new Renderer(window),
                window
            );
        Bootstrapper.Bootstrap();
        this.ctx.Backend.Init();
        this.ctx.Frontend.Init();
    }

    protected override void Draw()
    {
        this.ctx.Frontend.Process();
    }

    protected override void Update(double dt)
    {
        this.ctx.Backend.Process(dt);
    }
}
