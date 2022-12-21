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
        return ctx.GameState.Players.Find(
            p => p.Id == ctx.FrontendGameState.PlayerGuid
        );
    }

    public static void Move(Player player)
    {
        var context = Context.Get();
        var frontEndState = context.FrontendGameState;
        var inputState = frontEndState.InputState;

        var movement = player.PlayerMovementState;
        
        if (!movement.IsGrounded)
            movement.CurrentVelocity += Constants.Gravity;

        movement.CurrentVelocity = movement.CurrentVelocity with
        {
            X = inputState.GetAxis(InputAxis.Horizontal)
        };
        movement.CurrentMovement = movement.CurrentVelocity * movement.Speed * (float)context.GameState.DeltaTime;
        
        player.Position += movement.CurrentMovement;
    }

    public static void Collide(Player player)
    {
        var movement = player.PlayerMovementState;
        var world = Context.Get().GameState.World;
        bool hasCollision;
        do
        {
            var pL = player.Position + new Vector2(0, -8);
            var pL2 = player.Position + new Vector2(0, -24);
            hasCollision =
                world.HasChunkAt(pL) && world.GetChunkAt(pL).HasSolidTileAt(pL)
            || world.HasChunkAt(pL2) && world.GetChunkAt(pL2).HasSolidTileAt(pL2);
            if (hasCollision)
            {
                movement.CurrentVelocity = movement.CurrentVelocity with { X = 0 };
                player.Position += new Vector2(0.1f, 0);
            }
        } while (hasCollision);
        do
        {
            var pR = player.Position + new Vector2(14, -8);
            var pR2 = player.Position + new Vector2(14, -24);
            hasCollision =
                world.HasChunkAt(pR) && world.GetChunkAt(pR).HasSolidTileAt(pR)
            || world.HasChunkAt(pR2) && world.GetChunkAt(pR2).HasSolidTileAt(pR2);
            if (hasCollision)
            {
                movement.CurrentVelocity = movement.CurrentVelocity with { X = 0 };
                player.Position += new Vector2(-0.1f, 0);
            }
        } while (hasCollision);
        do
        {
            var pL = player.Position + new Vector2(0, 0);
            var pR = player.Position + new Vector2(14, 0);
            hasCollision =
                world.HasChunkAt(pL) && world.GetChunkAt(pL).HasSolidTileAt(pL)
                || world.HasChunkAt(pR) && world.GetChunkAt(pR).HasSolidTileAt(pR);
            if (hasCollision)
            {
                movement.CurrentVelocity = movement.CurrentVelocity with { Y = 0 };
                player.Position += new Vector2(0, -0.01f);
            }
        } while (hasCollision);
        do
        {
            var pL = player.Position + new Vector2(0, -28);
            var pR = player.Position + new Vector2(14, -28);
            hasCollision =
                world.HasChunkAt(pL) && world.GetChunkAt(pL).HasSolidTileAt(pL)
                || world.HasChunkAt(pR) && world.GetChunkAt(pR).HasSolidTileAt(pR);
            if (hasCollision)
            {
                movement.CurrentVelocity = movement.CurrentVelocity with { Y = 0 };
                player.Position += new Vector2(0, 0.1f);
            }
        } while (hasCollision);
        
        {
            var groundCheckPosition = player.Position - new Vector2(0, -1f);
            var pL = groundCheckPosition + new Vector2(0, 0);
            var pR = groundCheckPosition + new Vector2(14, 0);
            movement.IsGrounded = world.HasChunkAt(pL) && world.GetChunkAt(pL).HasTileAt(pL) 
                               || world.HasChunkAt(pR) && world.GetChunkAt(pR).HasTileAt(pR);
        }
    }

    public static bool HasCollisionWithAnyPlayer(Vector2 pos)
    {
        var ctx = Context.Get();
        const int ts = Constants.TileSize;
        var tilePos = new Vector2(pos.X - pos.X % ts, pos.Y - pos.Y % ts);
        return ctx.GameState.Players.Any(
            player =>
            {
                var playerPos = player.Position;
                var playerSize = new Vector2(14, 28);
                var playerRect = new SDL_Rect
                {
                    x = (int)playerPos.X,
                    y = (int)playerPos.Y - 5,
                    w = (int)playerSize.X,
                    h = (int)playerSize.Y
                };
                var tileRect = new SDL_Rect
                {
                    x = (int)tilePos.X,
                    y = 24 + (int)tilePos.Y,
                    w = 16,
                    h = 16
                };
                return SDL_HasIntersection(ref playerRect, ref tileRect) == SDL_bool.SDL_TRUE;
            }
        );
    }
}
