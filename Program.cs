namespace _2048
{
    internal class Program
    {
        private static void Main()
        {
            Console.CursorVisible = false;

            while (true)
            {
                var game = new Game();
                var isNotFinish = true;
                var isWin = false;

                while (isNotFinish)
                {
                    game.Draw();
                    isNotFinish = game.NextTurn(Console.ReadKey(true).Key);

                    if (!game.Field.Is2048)
                    {
                        continue;
                    }

                    isNotFinish = false;
                    isWin = true;
                }

                Console.Clear();

                if (isWin)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You won");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game over");
                    Console.ResetColor();
                }

                Console.WriteLine("Score : " + game.Score);
                Console.ReadKey(true);
            }
        }
    }
}