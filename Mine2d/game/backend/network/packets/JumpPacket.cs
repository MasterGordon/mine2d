using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mine2d.game.backend.network.packets;

public class JumpPacket : Packet
{
    public override PacketType Type => PacketType.Jump;

    public Guid PlayerId { get; init; }
}