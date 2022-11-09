class Tile
{
    public string Name { get; set; }
    public IntPtr Texture { get; set; }

    public Tile(string name, string textureName)
    {
        this.Name = name;

        var rl = Context.Get().ResourceLoader;
        var (ptr, size) = rl.LoadToIntPtr("assets." + textureName + ".png");
        var sdlBuffer = SDL_RWFromMem(ptr, size);
        var surface = IMG_Load_RW(sdlBuffer, 1);
        var texture = Context.Get().Renderer.CreateTextureFromSurface(surface);
        this.Texture = texture;
        SDL_FreeSurface(surface);
    }

    ~Tile()
    {
        SDL_DestroyTexture(this.Texture);
    }

    public void Render(int x, int y)
    {
        var renderer = Context.Get().Renderer;
        var scale = Context.Get().FrontendGameState.Settings.GameScale;
        var camera = Context.Get().FrontendGameState.Camera;
        renderer.DrawTexture(
            this.Texture,
            (x - (int)camera.position.X) * scale,
            (y - (int)camera.position.Y) * scale,
            Constants.TileSize * scale,
            Constants.TileSize * scale
        );
    }
}
