namespace Mine2d.frontend.renderer;

public class BackgroundRenderer : IRenderer
{
    private readonly IntPtr texture;

    public BackgroundRenderer()
    {
        var rl = Context.Get().ResourceLoader;
        var (ptr, size) = rl.LoadToIntPtr("assets.background.png");
        var sdlBuffer = SDL_RWFromMem(ptr, size);
        var surface = IMG_Load_RW(sdlBuffer, 1);
        this.texture = Context.Get().Renderer.CreateTextureFromSurface(surface);
        SDL_FreeSurface(surface);
    }

    ~BackgroundRenderer()
    {
        SDL_DestroyTexture(this.texture);
    }

    public void Render()
    {
        var ctx = Context.Get();
        var camera = ctx.FrontendGameState.Camera;
        var scale = ctx.FrontendGameState.Settings.GameScale;
        var renderer = ctx.Renderer;
        var (w, h) = ctx.Window.GetSize();
        var offsetX = camera.Position.X / scale % 16;
        var offsetY = camera.Position.Y / scale % 16;
        w /= scale;
        h /= scale;

        for (var x = -16; x < w + 16; x += 16)
        {
            for (var y = -16; y < h + 16; y += 16)
            {
                var rx = (x - (int)offsetX) * scale;
                var ry = (y - (int)offsetY) * scale;
                var rw = 16 * scale;
                var rh = 16 * scale;
                renderer.DrawTexture(this.texture, rx, ry, rw, rh);
            }
        }
    }
}
