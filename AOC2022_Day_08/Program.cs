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
            var maximumScore = 0;

            for (int row = 1; row < size - 1; row++)
            {
                for (int col = 1; col < size - 1; col++)
                {
                    var score = ScenicScore(forest, row, col);
                    if (score > maximumScore)
                    {
                        maximumScore = score;
                    }
                }
            }

            Console.WriteLine(maximumScore);
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

        static int ScenicScore(string[] forest, int row, int col)
        {
            var tree = forest[row][col];
            var leftSide = forest[row][..col];
            var rightSide = forest[row][(col + 1)..];
            var above = string.Join("", forest[..row].Select(r => r[col]));
            var below = string.Join("", forest[(row + 1)..].Select(r => r[col]));

            var scoreLeft = 0;
            var scoreAbove = 0;
            var scoreRight = 0;
            var scoreBelow = 0;
            var isViewBlocked = false;

            for (int i = leftSide.Length - 1; i >= 0 && !isViewBlocked; i--, scoreLeft++)
            {
                isViewBlocked = leftSide[i] >= tree;
            }
            isViewBlocked = false;
            for (int i = above.Length - 1; i >= 0 && !isViewBlocked; i--, scoreAbove++)
            {
                isViewBlocked = above[i] >= tree;
            }
            isViewBlocked = false;
            for (int i = 0; i < rightSide.Length && !isViewBlocked; i++, scoreRight++)
            {
                isViewBlocked = rightSide[i] >= tree;
            }
            isViewBlocked = false;
            for (int i = 0; i < below.Length && !isViewBlocked; i++, scoreBelow++)
            {
                isViewBlocked = below[i] >= tree;
            }

            var score = scoreLeft * scoreAbove * scoreRight * scoreBelow;
            return score;
        }
    }
}