namespace AOC2022_Day_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2022 Day 03");

            var lines = File.ReadAllLines("Input2.txt");
            var sum = lines.Sum(line => GetValue(line));
            Console.WriteLine($"The sum is: {sum}");
        }

        static int GetValue(string line)
        {
            var half = line.Length / 2;
            var first = line.Substring(0, half);
            var second = line.Substring(half);

            var commons = first.Intersect(second).ToList();
            if (!commons.Any())
            {
                return 0;
            }

            var common = commons.First();
            var val = common > 90 ? common - 96: common - 38;
            return val;
        }
    }
}