namespace AOC2022_Day_2
{
    internal class Program
    {
        //A - Rock - X - 1
        //B - Paper - Y - 2
        //C - Scissors Z - 3
        static readonly Dictionary<string, int> rules = new(){
            {"A X", 4 },
            {"A Y", 8 },
            {"A Z", 3 },

            {"B X", 1 },
            {"B Y", 5 },
            {"B Z", 9 },

            {"C X", 7 },
            {"C Y", 2 },
            {"C Z", 6 }
        };

        //X -lose
        //Y - draw
        //Z - win
        static readonly Dictionary<string, int> rules2 = new(){
            {"A X", 3 },
            {"A Y", 4 },
            {"A Z", 8 },

            {"B X", 1 },
            {"B Y", 5 },
            {"B Z", 9 },

            {"C X", 2 },
            {"C Y", 6 },
            {"C Z", 7 }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2022 Day 02");

            var lines = File.ReadAllLines("Input2.txt");
            var sum = 0;

            foreach (var line in lines)
            {
                sum += rules2[line];
            }

            Console.WriteLine($"The sum is: {sum}");
        }
    }
}