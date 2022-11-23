using Mine2d.core;
using Mine2d.engine;

namespace Mine2d.frontend.renderer;

public class WorldRenderer : IRenderer
{
    private readonly IntPtr light;
    private IntPtr overlay;
    private int overlayWidth, overlayHeight;

    public WorldRenderer()
    {
        var rl = Context.Get().ResourceLoader;
        var (ptr, size) = rl.LoadToIntPtr("assets.light.png");
        var sdlBuffer = SDL_RWFromMem(ptr, size);
        var surface = IMG_Load_RW(sdlBuffer, 1);
        this.light = Context.Get().Renderer.CreateTextureFromSurface(surface);
        SDL_FreeSurface(surface);
        var (w, h) = Context.Get().Window.GetSize();
        this.overlay = SDL_CreateTexture(Context.Get().Renderer.GetRaw(), SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, w, h);
    }

    public void Render()
    {
        var ctx = Context.Get();
        var world = ctx.GameState.World;
        var tileRegistry = ctx.TileRegistry;
        Renderer.ProcessStatus(SDL_SetRenderTarget(ctx.Renderer.GetRaw(), this.overlay));
        var (ww, wh) = ctx.Window.GetSize();
        if (wh != this.overlayHeight || ww != this.overlayWidth)
        {
            SDL_DestroyTexture(this.overlay);
            this.overlay = SDL_CreateTexture(ctx.Renderer.GetRaw(), SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, ww, wh);
            this.overlayWidth = ww;
            this.overlayHeight = wh;
        }

        clearTexture(this.overlay);
        var renderer = Context.Get().Renderer;
        var scale = ctx.FrontendGameState.Settings.GameScale;
        var camera = Context.Get().FrontendGameState.Camera;
        Renderer.ProcessStatus(SDL_SetTextureBlendMode(this.light, SDL_BlendMode.SDL_BLENDMODE_BLEND));
        foreach (var (_, chunk) in world.Chunks)
        {
            for (var y = 0; y < Constants.ChunkSize; y++)
            {
                for (var x = 0; x < Constants.ChunkSize; x++)
                {
                    var stile = chunk.GetTile(x, y);
                    var chunkOffsetX = chunk.X * Constants.TileSize * Constants.ChunkSize;
                    var chunkOffsetY = chunk.Y * Constants.TileSize * Constants.ChunkSize;
                    var drawX = x * 16 + chunkOffsetX;
                    var drawY = y * 16 + chunkOffsetY;
                    if (stile.Id == 0)
                    {
                        Renderer.ProcessStatus(SDL_SetRenderTarget(ctx.Renderer.GetRaw(), this.overlay));
                        renderer.DrawTexture(
                            this.light,
                            (drawX - (int)camera.Position.X - 40) * scale,
                            (drawY - (int)camera.Position.Y - 40) * scale,
                            96 * scale,
                            96 * scale
                        );
                        continue;
                    }
                    else
                    {
                        Renderer.ProcessStatus(SDL_SetRenderTarget(ctx.Renderer.GetRaw(), IntPtr.Zero));
                    }

                    var tile = tileRegistry.GetTile(stile.Id);
                    tile.Render(drawX, drawY, stile);
                }
            }
        }
        Renderer.ProcessStatus(SDL_SetRenderTarget(ctx.Renderer.GetRaw(), IntPtr.Zero));
        Renderer.ProcessStatus(SDL_SetTextureBlendMode(this.overlay, SDL_BlendMode.SDL_BLENDMODE_MUL));
        Renderer.ProcessStatus(SDL_RenderCopy(ctx.Renderer.GetRaw(), this.overlay, IntPtr.Zero, IntPtr.Zero));
        Renderer.ProcessStatus(SDL_SetRenderTarget(ctx.Renderer.GetRaw(), IntPtr.Zero));
    }

    public static void clearTexture(IntPtr texture)
    {
        var ctx = Context.Get();
        var renderer = ctx.Renderer;
        var (w, h) = renderer.GetTextureSize(texture);
        var clearRect = new SDL_Rect { x = 0, y = 0, w = w, h = h };
        Renderer.ProcessStatus(SDL_SetRenderTarget(ctx.Renderer.GetRaw(), texture));
        _ = SDL_SetRenderDrawColor(ctx.Renderer.GetRaw(), 0, 0, 0, 255);
        Renderer.ProcessStatus(SDL_RenderFillRect(ctx.Renderer.GetRaw(), ref clearRect));
    }
}
