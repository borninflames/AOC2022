namespace AOC2022_Day_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2022 Day 03");

            var lines = File.ReadAllLines("Input2.txt");
            var sum = 0;
            for (int i = 0; i < lines.Length; i+=3)
            {
                sum += GetValue2(lines[i], lines[i + 1], lines[i + 2]);
            }

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

        static int GetValue2(string line1, string line2, string line3)
        {
            var commons = line1.Intersect(line2).ToList();
            if (!commons.Any())
            {
                return 0;
            }
            commons = commons.Intersect(line3).ToList();
            if (!commons.Any())
            {
                return 0;
            }

            var common = commons.First();
            var val = common > 90 ? common - 96 : common - 38;
            return val;
        }
    }
}