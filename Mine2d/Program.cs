namespace mine2d;

public class Program
{
    public static void Main(string[] args)
    {
        var isHost = !args.Contains("--client");
        // bool isHost = true;
        var game = new Mine2d(isHost);
        game.Run();
    }
}
