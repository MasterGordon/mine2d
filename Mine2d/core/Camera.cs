namespace mine2d.core;

public class Camera
{
    public Vector2 Position { get; set; }

    public Camera()
    {
        this.Position = Vector2.Zero;
    }

    public void CenterOn(Vector2 target)
    {
        var ctx = Context.Get();
        var scale = ctx.FrontendGameState.Settings.GameScale;
        var windowWidth = ctx.FrontendGameState.WindowWidth;
        var windowHeight = ctx.FrontendGameState.WindowHeight;
        this.Position = target - (new Vector2(windowWidth, windowHeight) / 2) / scale;
    }
}
