class PlayerEntity
{
    public static bool isSelf(Player p)
    {
        return p.Guid == GetSelf().Guid;
    }

    public static Player GetSelf()
    {
        var ctx = Context.Get();
        var player = ctx.GameState.Players.FirstOrDefault(
            p => p.Guid == ctx.FrontendGameState.PlayerGuid
        );
        return player;
    }

    public static void Move(Player p)
    {
        p.Movement += Constants.gravity;
        p.Position += p.Movement;
    }

    public static void Collide(Player p)
    {
        var world = Context.Get().GameState.World;
        bool hasCollision;
        do
        {
            var pL = p.Position + new Vector2(0, -8);
            hasCollision =
                world.HasChunkAt(pL) && world.GetChunkAt(pL).hasTileAt(pL);
            if (hasCollision)
            {
                p.Movement = p.Movement with { X = 0 };
                p.Position += new Vector2(0.1f, 0);
            }
        } while (hasCollision);
        do
        {
            var pR = p.Position + new Vector2(16, -8);
            hasCollision =
                world.HasChunkAt(pR) && world.GetChunkAt(pR).hasTileAt(pR);
            if (hasCollision)
            {
                p.Movement = p.Movement with { X = 0 };
                p.Position += new Vector2(-0.1f, 0);
            }
        } while (hasCollision);
        do
        {
            var pL = p.Position;
            var pR = p.Position + new Vector2(16, 0);
            hasCollision =
                world.HasChunkAt(pL) && world.GetChunkAt(pL).hasTileAt(pL)
                || world.HasChunkAt(pR) && world.GetChunkAt(pR).hasTileAt(pR);
            Console.WriteLine(World.ToChunkPos(p.Position));
            if (world.HasChunkAt(p.Position))
            {
                var chunk = world.GetChunkAt(p.Position);
                Console.WriteLine($"Chunk: {chunk.X}, {chunk.Y}");
            }
            if (hasCollision)
            {
                p.Movement = p.Movement with { Y = 0 };
                p.Position += new Vector2(0, -0.1f);
            }
        } while (hasCollision);
    }
}
