class Mine2d : Game
{
    private Context ctx;

    public Mine2d(bool isHost)
    {
        var window = new Window("MultiPlayerGame" + (isHost ? " - host" : ""), 1200, 800);
        if (isHost)
        {
            this.ctx = new Context(
                isHost,
                new Backend(),
                new Frontend(),
                new GameState(),
                new FrontendGameState(),
                new Renderer(window),
                window
            );
        }
        else
        {
            this.ctx = new Context(
                isHost,
                new RemoteBackend(),
                new Frontend(),
                new GameState(),
                new FrontendGameState(),
                new Renderer(window),
                window
            );
        }
        Bootstrapper.Bootstrap();
        ctx.Backend.Init();
        ctx.Frontend.Init();
    }

    protected override void draw()
    {
        ctx.Frontend.Process();
    }

    protected override void update(double dt)
    {
        ctx.Backend.Process(dt);
    }
}
