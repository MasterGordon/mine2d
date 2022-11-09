using mine2d.state;

namespace mine2d.core;

class PlayerEntity
{
    public static bool IsSelf(Player p)
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
        p.Movement += Constants.Gravity;
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
                world.HasChunkAt(pL) && world.GetChunkAt(pL).HasTileAt(pL);
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
                world.HasChunkAt(pR) && world.GetChunkAt(pR).HasTileAt(pR);
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
                world.HasChunkAt(pL) && world.GetChunkAt(pL).HasTileAt(pL)
                || world.HasChunkAt(pR) && world.GetChunkAt(pR).HasTileAt(pR);
            if (hasCollision)
            {
                p.Movement = p.Movement with { Y = 0 };
                p.Position += new Vector2(0, -0.1f);
            }
        } while (hasCollision);
        do
        {
            var pL = p.Position + new Vector2(0, -32);
            var pR = p.Position + new Vector2(16, -32);
            hasCollision =
                world.HasChunkAt(pL) && world.GetChunkAt(pL).HasTileAt(pL)
                || world.HasChunkAt(pR) && world.GetChunkAt(pR).HasTileAt(pR);
            if (hasCollision)
            {
                p.Movement = p.Movement with { Y = 0 };
                p.Position += new Vector2(0, 0.1f);
            }
        } while (hasCollision);
    }
}