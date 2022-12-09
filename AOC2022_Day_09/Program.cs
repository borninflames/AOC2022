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
                //Show();
                var prevHeadPos = new Position(Head);
                switch (direction)
                {
                    case 'R':
                        Head.X++;
                        break;
                    case 'L':
                        Head.X--;
                        break;
                    case 'U':
                        Head.Y--;
                        break;
                    case 'D':
                        Head.Y++;
                        break;
                    default:
                        break;
                }
                
                PastTailPositions.Add(Tail);

                if (Head.IsTooFarFrom(Tail))
                {
                    Tail = prevHeadPos;
                }
            }
        }

        public Position Head { get; set; } = new(0, 0);
        public Position Tail { get; set; } = new(0, 0);

        public List<Position> PastTailPositions { get; set; } = new();

        public void Show()
        {
            Console.Clear();

            Console.SetCursorPosition(Tail.X + 40, 12 - Tail.Y);
            Console.Write("T");
            Console.SetCursorPosition(Head.X + 40, 12 - Head.Y);
            Console.Write("H");

            //Thread.Sleep(200);
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