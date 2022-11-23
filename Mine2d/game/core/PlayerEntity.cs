using Mine2d.game.state;

namespace Mine2d.game.core;

public class PlayerEntity
{
    public static bool IsSelf(Player p)
    {
        return p.Id == GetSelf().Id;
    }

    public static Player GetSelf()
    {
        var ctx = Context.Get();
        var player = ctx.GameState.Players.FirstOrDefault(
            p => p.Id == ctx.FrontendGameState.PlayerGuid
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
            var pL2 = p.Position + new Vector2(0, -24);
            hasCollision =
                world.HasChunkAt(pL) && world.GetChunkAt(pL).HasTileAt(pL)
            || world.HasChunkAt(pL2) && world.GetChunkAt(pL2).HasTileAt(pL2);
            if (hasCollision)
            {
                p.Movement = p.Movement with { X = 0 };
                p.Position += new Vector2(0.1f, 0);
            }
        } while (hasCollision);
        do
        {
            var pR = p.Position + new Vector2(14, -8);
            var pR2 = p.Position + new Vector2(14, -24);
            hasCollision =
                world.HasChunkAt(pR) && world.GetChunkAt(pR).HasTileAt(pR)
            || world.HasChunkAt(pR2) && world.GetChunkAt(pR2).HasTileAt(pR2);
            if (hasCollision)
            {
                p.Movement = p.Movement with { X = 0 };
                p.Position += new Vector2(-0.1f, 0);
            }
        } while (hasCollision);
        do
        {
            var pL = p.Position + new Vector2(0, 0);
            var pR = p.Position + new Vector2(14, 0);
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
            var pL = p.Position + new Vector2(0, -28);
            var pR = p.Position + new Vector2(14, -28);
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
