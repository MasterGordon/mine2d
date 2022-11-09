class Mine2d : Game
{
    private readonly Context ctx;

    public Mine2d(bool isHost)
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

    protected override void draw()
    {
        this.ctx.Frontend.Process();
    }

    protected override void update(double dt)
    {
        this.ctx.Backend.Process(dt);
    }
}
