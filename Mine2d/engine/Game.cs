namespace mine2d.engine;

public abstract class Game
{
    public const int Tps = 128;

    private readonly bool running = true;
    private readonly Queue<int> fpsQueue = new();
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

            while (tAcc >= TimeSpan.FromSeconds(1.0 / Tps))
            {
                this.Update(dt.TotalSeconds);
                tAcc -= TimeSpan.FromSeconds(1.0 / Tps);
            }

            this.Draw();
        }
    }
}
