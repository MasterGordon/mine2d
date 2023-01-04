namespace Mine2d.game.core;

public static class Debugger
{
    public static void Print(string message)
    {
        Context.Get().FrontendGameState.DebugState.Messages.Enqueue(message);
    }
}
