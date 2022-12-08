namespace AOC2022_Day_08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2022 Day 08");
            var forest = File.ReadAllLines("Input2.txt");
            var size = forest[0].Length;
            var visibleTrees = size * 4 - 4;

            for (int row = 1; row < size - 1; row++)
            {
                for (int col = 1; col < size - 1; col++)
                {
                    if (IsVisible(forest, row, col))
                    {
                        visibleTrees++;
                    }
                }
            }

            Console.WriteLine(visibleTrees);
        }

        static bool IsVisible(string[] forest, int row, int col)
        {
            var tree = forest[row][col];
            var leftSide = forest[row][..col];
            var rightSide = forest[row][(col + 1)..];
            var above = string.Join("", forest[..row].Select(r => r[col]));
            var below = string.Join("", forest[(row + 1)..].Select(r => r[col]));            
            return leftSide.All(t => t < tree) ||
                rightSide.All(t => t < tree) ||
                above.All(t => t < tree) ||
                below.All(t => t < tree);
        }
    }
}