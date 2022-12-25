using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mine2d.game.core;

public static class Debugger
{
    public static void Print(string message)
    {
        Context.Get().FrontendGameState.DebugState.Messages.Enqueue(message);
    }
}