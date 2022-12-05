using System.Globalization;

namespace AOC2022_Day_05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2022 Day 04");

            var lines = File.ReadAllLines("Input2.txt");
            var stacks = CreateStacks(lines);
            var foundTheFirstOperationLine = false;
            foreach (var line in lines)
            {
                if (line == string.Empty)
                {
                    foundTheFirstOperationLine = true;
                    continue;
                }

                if (foundTheFirstOperationLine)
                {
                    var opParts = line.Split(' ');
                    var howMany = int.Parse(opParts[1]);
                    var from = int.Parse(opParts[3]) - 1;
                    var to = int.Parse(opParts[5]) - 1;
                    Stack<char> tempStack = new();
                    for (int i = 0; i < howMany; i++)
                    {
                        tempStack.Push(stacks[from].Pop());                        
                    }

                    while (tempStack.Any())
                    {
                        stacks[to].Push(tempStack.Pop());
                    }
                }
            }
            Console.WriteLine("--------------------------------");

            foreach (var stack in stacks)
            {
                Console.Write(stack.Peek());
            }
            Console.WriteLine("");
            Console.WriteLine("--------------------------------");




        }

        static Stack<char>[] CreateStacks(string[] lines)
        {
            var stacks = new List<Stack<char>>();
            List<string> stacksInput = new();
            var j = 0;
            while (lines[j] != string.Empty)
            {
                stacksInput.Add(lines[j++]);
            }

            stacksInput.Reverse();
            stacksInput = stacksInput.Skip(1).ToList();
            foreach (var level in stacksInput)
            {
                for (int i = 1, k = 0; i < level.Length; i += 4, k++)
                {
                    if (level[i] != ' ')
                    {
                        if (stacks.Count < k + 1)
                        {
                            stacks.Add(new());
                        }
                        stacks[k].Push(level[i]);
                    }
                }
            }

            return stacks.ToArray();
        }
    }
}