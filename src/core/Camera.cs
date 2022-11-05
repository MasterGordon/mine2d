class Camera
{
    public Vector2 position;

    public Camera()
    {
        position = Vector2.Zero;
    }

    public void CenterOn(Vector2 target)
    {
        Console.WriteLine("Centering camera on " + target);
        var ctx = Context.Get();
        var scale = ctx.FrontendGameState.Settings.GameScale;
        var windowWidth = ctx.FrontendGameState.WindowWidth;
        var windowHeight = ctx.FrontendGameState.WindowHeight;
        position = target - (new Vector2(windowWidth / 2, windowHeight / 2)) / scale;
    }
}
