namespace _2048
{
    internal class Field
    {
        private const int CellWidth = 8;

        public readonly int Height;
        public readonly int Width;
        public int CellsCount { get; private set; }
        public Cell[,] Cells { get; }

        public bool Is2048 { get; private set; }

        public Field(
            int height = 4,
            int width = 4)
        {
            Is2048 = false;
            Height = height;
            Width = width;
            CellsCount = 0;

            Cells = new Cell[Height, Width];

            for (var i = 0; i < Height; i++)
                for (var j = 0; j < Width; j++)
                    Cells[i, j] = new Cell();
        }

        public void Draw()
        {
            DrawBoarder('|', '-');

            for (var i = 0; i < Height; i++)
            {
                DrawBoarder('|', ' ');

                Console.Write("|");

                for (var j = 0; j < Width; j++)
                {
                    var cellValue = Cells[i, j].Value.ToString();
                    var color = ConsoleColor.Gray;

                    if (cellValue == "0")
                    {
                        cellValue = "";
                    }
                    else
                    {
                        var index = (int)Math.Log(Cells[i, j].Value, 2);
                        color = (ConsoleColor)(Enum.GetValues(typeof(ConsoleColor)).GetValue(index) ?? 7);
                    }

                    var cellLength = cellValue.Length;
                    var preLength = (CellWidth - cellLength) / 2 + 1;
                    var afterLength = CellWidth - preLength - cellLength;

                    Console.Write(new string(' ', preLength));

                    Console.ForegroundColor = color;
                    Console.Write(cellValue);
                    Console.ResetColor();

                    Console.Write(new string(' ', afterLength));

                    Console.Write("|");
                }

                Console.WriteLine();

                DrawBoarder('|', ' ');
                DrawBoarder('|', '-');
            }
        }

        public void SetCell(
            int i,
            int j,
            int value)
        {
            Cells[i, j] = new Cell(value);
            CellsCount++;
        }

        public int MoveUp()
        {
            var score = 0;

            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    Cells[i, j].UnLock();

                    if (Cells[i, j].Value != 0)
                    {
                        score += MoveVertical(i, j, -1);
                    }
                }
            }

            return score;
        }

        public int MoveRight()
        {
            var score = 0;

            for (var j = Width - 1; j >= 0; j--)
            {
                for (var i = 0; i < Height; i++)
                {
                    Cells[i, j].UnLock();

                    if (Cells[i, j].Value != 0)
                    {
                        score += MoveHorizontal(i, j, 1);
                    }
                }
            }

            return score;
        }

        public int MoveDown()
        {
            var score = 0;

            for (var i = Height - 1; i >= 0; i--)
            {
                for (var j = 0; j < Width; j++)
                {
                    Cells[i, j].UnLock();

                    if (Cells[i, j].Value != 0)
                    {
                        score += MoveVertical(i, j, 1);
                    }
                }
            }

            return score;
        }

        public int MoveLeft()
        {
            var score = 0;

            for (var j = 0; j < Width; j++)
            {
                for (var i = 0; i < Height; i++)
                {
                    Cells[i, j].UnLock();

                    if (Cells[i, j].Value != 0)
                    {
                        score += MoveHorizontal(i, j, -1);
                    }
                }
            }

            return score;
        }

        private void DrawBoarder(
            char c1,
            char c2)
        {
            for (var i = 0; i < Width; i++)
            {
                Console.Write(c1);
                Console.Write(new string(c2, CellWidth));
            }

            Console.WriteLine(c1);
        }

        private int MoveHorizontal(
            int x,
            int y,
            int vector)
        {
            var j = y;
            var score = 0;

            while (j + vector > 0 && j + vector < Width - 1 && Cells[x, j + vector].Value == 0)
                j += vector;

            try
            {
                if (Cells[x, j + vector].Value == 0)
                {
                    j += vector;
                }
            }
            catch (Exception)
            {
            }

            var temp = Cells[x, y];
            Cells[x, y] = new Cell();
            Cells[x, j] = temp;

            try
            {
                if (!Cells[x, j + vector].IsLock && Cells[x, j + vector].Value == Cells[x, j].Value)
                {
                    score = Merge(ref Cells[x, j + vector], ref Cells[x, j]);
                }
            }
            catch (Exception)
            {
            }

            return score;
        }

        private int MoveVertical(
            int x,
            int y,
            int vector)
        {
            var i = x;
            var score = 0;

            while (i + vector > 0 && i + vector < Height - 1 && Cells[i + vector, y].Value == 0)
                i += vector;

            try
            {
                if (Cells[i + vector, y].Value == 0)
                {
                    i += vector;
                }
            }
            catch (Exception)
            {
            }

            var temp = Cells[x, y];
            Cells[x, y] = new Cell();
            Cells[i, y] = temp;

            try
            {
                if (!Cells[i + vector, y].IsLock && Cells[i + vector, y].Value == Cells[i, y].Value)
                {
                    score = Merge(ref Cells[i + vector, y], ref Cells[i, y]);
                }
            }
            catch (Exception)
            {
            }

            return score;
        }

        private int Merge(
            ref Cell cell1,
            ref Cell cell2)
        {
            cell1 = new Cell(cell1.Value * 2);
            cell1.Lock();
            cell2 = new Cell();
            CellsCount--;

            if (cell1.Value == 2048)
            {
                Is2048 = true;
            }

            return cell1.Value;
        }
    }
}