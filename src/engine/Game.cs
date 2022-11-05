abstract class Game
{
    public const int TPS = 128;
    private Queue<int> fpsQueue = new Queue<int>();
    protected abstract void update(double dt);
    protected abstract void draw();

    public void Run()
    {
        var tLast = DateTime.Now;
        var tAcc = TimeSpan.Zero;
        while (true)
        {
            var dt = DateTime.Now - tLast;
            tLast = DateTime.Now;
            tAcc += dt;
            var fps = (int)(1 / dt.TotalSeconds);
            fpsQueue.Enqueue(fps);
            while (fpsQueue.Count > fps)
                fpsQueue.Dequeue();
            while (tAcc >= TimeSpan.FromSeconds(1.0 / TPS))
            {
                update(dt.TotalSeconds);
                tAcc -= TimeSpan.FromSeconds(1.0 / TPS);
            }

            draw();
        }
    }
}
