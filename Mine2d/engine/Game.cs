using Mine2d.game;

namespace Mine2d.engine;

public abstract class Game
{
    public const int Tps = 128;

    private readonly bool running = true;
    private readonly Queue<int> fpsQueue = new();
    private DateTime nextFpsUpdate = DateTime.Now;
    protected abstract void Update(double dt);
    protected abstract void Draw();

    public void Run()
    {
        var tLast = DateTime.Now;
        var tAcc = TimeSpan.Zero;
        while (this.running)
        {
            var dt = DateTime.Now - tLast;
            tLast = DateTime.Now;
            tAcc += dt;
            var fps = (int)(1 / dt.TotalSeconds);
            this.fpsQueue.Enqueue(fps);
            while (this.fpsQueue.Count > fps)
            {
                this.fpsQueue.Dequeue();
            }
            if (this.nextFpsUpdate < DateTime.Now && this.fpsQueue.Count > 0)
            {
                var avgFps = this.fpsQueue.Sum() / this.fpsQueue.Count;
                SDL_SetWindowTitle(Context.Get().Window.GetRaw(), "Mine2d - debug - " + avgFps + "fps");
                this.nextFpsUpdate = DateTime.Now.AddMilliseconds(200);
            }

            while (tAcc >= TimeSpan.FromSeconds(1.0 / Tps))
            {
                this.Update(dt.TotalSeconds);
                tAcc -= TimeSpan.FromSeconds(1.0 / Tps);
            }

            this.Draw();
        }
    }
}
