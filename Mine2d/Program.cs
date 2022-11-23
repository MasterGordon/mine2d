﻿using Mine2d.game;

namespace Mine2d;

public class Program
{
    public static void Main(string[] args)
    {
        var isHost = !args.Contains("--client");
        // bool isHost = true;
        var game = new Mine2dGame(isHost);
        game.Run();
    }
}
