namespace _2048
{
    internal class Game
    {
        public int Score { get; private set; }

        public Game()
        {
            Start();
        }

        public Field Field { get; private set; }

        public void Draw()
        {
            Console.Clear();
            Console.WriteLine("Score: " + Score);
            Field.Draw();
            Console.WriteLine("Restart : Enter");
        }

        public bool NextTurn(
            ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    Score += Field.MoveUp();

                    break;

                case ConsoleKey.RightArrow:
                    Score += Field.MoveRight();

                    break;

                case ConsoleKey.DownArrow:
                    Score += Field.MoveDown();

                    break;

                case ConsoleKey.LeftArrow:
                    Score += Field.MoveLeft();

                    break;

                case ConsoleKey.Enter:
                    Restart();

                    return true;

                default:
                    return true;
            }

            return RandomSpawn();
        }

        public void Restart()
        {
            Start();
        }

        private bool RandomSpawn()
        {
            if (Field.CellsCount == Field.Height * Field.Width)
            {
                return false;
            }

            var rnd = new Random();
            var x = 0;
            var y = 0;

            do
            {
                x = rnd.Next(0, Field.Height);
                y = rnd.Next(0, Field.Width);
            } while (Field.Cells[x, y].Value != 0);

            var value = rnd.Next(0, 10) == 9
                            ? 4
                            : 2;

            Field.SetCell(x, y, value);

            return true;
        }

        private void Start()
        {
            Field = new Field();
            Score = 0;

            RandomSpawn();
            RandomSpawn();
        }
    }
}