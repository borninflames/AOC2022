namespace AOC2022_Day_09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2022 Day 09");
            var moves = File.ReadAllLines("Input2.txt");
            var rope = new Rope();
            Console.CursorVisible = false;
            rope.Show();
            foreach (var move in moves)
            {
                var command = move.Split(' ');
                rope.Move(command[0][0], int.Parse(command[1]));
            }
            
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine(rope.PastTailPositions.Distinct().ToList().Count);
        }
    }

    internal class Rope
    {
        public void Move(char direction, int times)
        {
            for (int i = 0; i < times; i++)
            {
                switch (direction)
                {
                    case 'R':
                        Knots[0].X++;
                        break;
                    case 'L':
                        Knots[0].X--;
                        break;
                    case 'U':
                        Knots[0].Y++;
                        break;
                    case 'D':
                        Knots[0].Y--;
                        break;
                    default:
                        break;
                }

                for (int j = 1; j < Knots.Length; j++)
                {
                    if (j == Knots.Length - 1)
                    {
                        PastTailPositions.Add(new Position(Knots[j]));
                    }
                    if (Knots[j - 1].IsTooFarFrom(Knots[j]))
                    {
                        if (Knots[j - 1].X == Knots[j].X)
                        {
                            if (Knots[j - 1].Y > Knots[j].Y)
                            {
                                Knots[j].Y++;
                            }
                            else
                            {
                                Knots[j].Y--;
                            }
                        }
                        else if (Knots[j - 1].Y == Knots[j].Y)
                        {
                            if (Knots[j - 1].X > Knots[j].X)
                            {
                                Knots[j].X++;
                            }
                            else
                            {
                                Knots[j].X--;
                            }
                        }
                        else
                        {
                            if (Knots[j - 1].Y > Knots[j].Y)
                            {
                                Knots[j].Y++;
                            }
                            else
                            {
                                Knots[j].Y--;
                            }
                            if (Knots[j - 1].X > Knots[j].X)
                            {
                                Knots[j].X++;
                            }
                            else
                            {
                                Knots[j].X--;
                            }
                        }
                    }

                    if (j == Knots.Length - 1)
                    {
                        PastTailPositions.Add(new Position(Knots[j]));
                    }
                }
            }
            //Show();
        }

        public Position[] Knots { get; set; } =
        {
            new(0,0),
            new(0,0),
            new(0,0),
            new(0,0),
            new(0,0),
            new(0,0),
            new(0,0),
            new(0,0),
            new(0,0),
            new(0,0)
        };

        public List<Position> PastTailPositions { get; set; } = new();

        public void Show()
        {
            Console.Clear();

            for (int i = Knots.Length - 1; i >= 0; i--)
            {
                Console.SetCursorPosition(Knots[i].X + 50, 20 - Knots[i].Y);
                Console.Write(i);
            }

            Thread.Sleep(400);
        }
    }
    internal class Position : IEquatable<Position>
    {
        public Position(Position p)
        {
            X = p.X;
            Y = p.Y;
        }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public bool IsTooFarFrom(Position p)
        {
            return Math.Abs(X - p.X) > 1 || Math.Abs(Y - p.Y) > 1;
        }
        public bool Equals(Position? other)
        {
            return other != null && X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj) => Equals(obj as Position);

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}