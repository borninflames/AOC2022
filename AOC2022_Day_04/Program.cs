namespace AOC2022_Day_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2022 Day 04");

            var lines = File.ReadAllLines("Input2.txt");
            var sum = lines.Where(line => FullyContains(line)).ToList().Count;

            Console.WriteLine($"The sum is: {sum}");
        }

        static bool FullyContains(string line)
        {
            var sections = line.Split(',');
            var minMax = sections[0].Split("-").Select(int.Parse).ToArray();
            var section1 = Enumerable.Range(minMax[0], minMax[1] - minMax[0]+1).ToList();
            minMax = sections[1].Split("-").Select(int.Parse).ToArray();
            var section2 = Enumerable.Range(minMax[0], minMax[1] - minMax[0]+1).ToList();

            var common = section1.Intersect(section2).ToList();

            return common.Count == section1.Count || common.Count == section2.Count;
        }
    }
}