using System;

namespace BowlingExample
{
    class Program
    {
        static void Main(string[] _)
        {
            Console.WriteLine("Game is starting...");
            //IThrowManager throwManager = new RandomThrowManager();
            IThrowManager throwManager = new ConsoleReadThrowManager();
            Game game = new Game(throwManager);
            game.Play();
        }
    }
}
