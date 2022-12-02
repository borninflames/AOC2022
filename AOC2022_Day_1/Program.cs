using System.Runtime.Serialization;

namespace AOC2022_Day_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2022 Day 01");

            var lines = File.ReadAllLines("Input2.txt");
            List<int> sums = new();
            var max = 0;
            var sum = 0;

            foreach (var line in lines)
            {
                //Console.WriteLine($"{line}");
                if (string.IsNullOrEmpty(line))
                {
                    if (sum > max)
                    {
                        max = sum;                       
                    }
                    sums.Add(sum);
                    sum = 0;
                    continue;
                }

                sum += int.Parse(line);
            }

            if (sum > max)
            {
                max = sum;
            }
            sums.Add(sum);

            Console.WriteLine($"The maximum calories are: {max}");

            var topThree = sums.OrderByDescending(x => x).Take(3).Sum();
            Console.WriteLine($"The top three maximum calories sum is: {topThree}");

        }
    }
}