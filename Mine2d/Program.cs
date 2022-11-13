namespace mine2d;

class Program
{
    static void Main(string[] args)
    {
        bool isHost = args.Contains("--host");
        // bool isHost = true;
        var game = new Mine2d(isHost);
        game.Run();
        // var p = new Publisher(isHost ? InteractorKind.Server : InteractorKind.Client);
        // p.Dump();
        // Console.WriteLine("Hello World!");
    }
}