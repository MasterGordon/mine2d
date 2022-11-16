using Mine2d;
using Mine2d.backend.data;
using Mine2d.core.data;
using Mine2d.core.tiles;
using Mine2d.core.world;
using Mine2d.engine.system.annotations;

namespace Mine2d.backend.interactor;

[Interactor]
public class WorldGeneration
{
    private static Vector2[] directions = new Vector2[]
    {
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(0, -1),
        new Vector2(-1, 0)
    };

    [Interaction(InteractorKind.Server, "playerMoved")]
    public static void PlayerMovedServer(PlayerMovedPacket packet)
    {
        var ctx = Context.Get();
        var world = ctx.GameState.World;
        foreach (var direction in directions)
        {
            var generationTarget = packet.Target + direction * 16 * 32;
            var hasChunkGenerated = world.HasChunkAt(generationTarget);

            if (!hasChunkGenerated)
            {
                var chunkPos = World.ToChunkPos(generationTarget);
                world.AddChunk(ChunkGenerator.CreateFilledChunk((int)chunkPos.X, (int)chunkPos.Y, STile.From(Tiles.Stone)));
            }
        }
    }
}
