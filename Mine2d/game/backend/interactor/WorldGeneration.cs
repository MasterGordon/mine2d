using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;
using Mine2d.game.core.data;
using Mine2d.game.core.world;

namespace Mine2d.game.backend.interactor;

[Interactor]
public class WorldGeneration
{
    private static readonly Vector2[] Directions = {
        new(0, 1),
        new(1, 0),
        new(0, -1),
        new(-1, 0),
        new(1, 1),
        new(1, -1),
        new(-1, -1),
        new(-1, 1)
    };

    [Interaction(InteractorKind.Server, PacketType.PlayerMoved)]
    public static void PlayerMovedServer(PlayerMovedPacket packet)
    {
        var ctx = Context.Get();
        var world = ctx.GameState.World;
        foreach (var direction in Directions)
        {
            var generationTarget = packet.Target + direction * 16 * 32;
            var hasChunkGenerated = world.HasChunkAt(generationTarget);
    
            if (!hasChunkGenerated)
            {
                var chunkPos = World.ToChunkPos(generationTarget);
                world.AddChunk(ChunkGenerator.CreateChunk((int)chunkPos.X, (int)chunkPos.Y));
            }
        }
    }
}
