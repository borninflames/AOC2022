using System.ComponentModel;

namespace AOC2022_Day_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2022 Day 11");
            Monkey[] monkeys = ParseMonkeyDescriptions();

            long mod = monkeys.Aggregate(1L, (mod, monkey) => mod * monkey.TestNumber);
            for (int round = 1; round <= 10000; round++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.InspectItems(mod);
                }
                if (round % 10000 == 0)
                {
                    Console.WriteLine($"After round {round}:");
                    foreach (var monkey in monkeys)
                    {
                        Console.WriteLine(monkey);

                    }
                    Console.WriteLine();
                }

            }
            var maxTwo = monkeys.Select(x => x.InspectedItems).OrderByDescending(x => x).ToList().Take(2).ToArray();
            Console.WriteLine(maxTwo[0] * maxTwo[1]);
        }

        private static Monkey[] ParseMonkeyDescriptions()
        {
            var monkeysDescriptions = File.ReadAllLines("Input2.txt");

            Monkey[] monkeys = new Monkey[monkeysDescriptions.Where(d => d.StartsWith("Monkey")).Count()];
            var monkeyNumber = 0;
            foreach (var line in monkeysDescriptions)
            {
                if (line.StartsWith("Monkey "))
                {
                    monkeyNumber = int.Parse(line.Split("Monkey ")[1][0].ToString());
                    monkeys[monkeyNumber] = new Monkey(monkeys, monkeyNumber);
                }
                if (line.StartsWith("  Starting items: "))
                {
                    var itemsString = line.Split("  Starting items: ")[1].Split(", ");
                    foreach (var item in itemsString)
                    {
                        monkeys[monkeyNumber].Items.Enqueue(int.Parse(item));
                    }
                }
                if (line.StartsWith("  Operation: new = old "))
                {
                    var operationParts = line.Split("  Operation: new = old ")[1].Split(" ");
                    monkeys[monkeyNumber].Operation = operationParts[0];
                    operationParts[1] = operationParts[1] == "old" ? "-1" : operationParts[1];
                    monkeys[monkeyNumber].OperationNumber = int.Parse(operationParts[1]);
                }
                if (line.StartsWith("  Test: divisible by "))
                {
                    monkeys[monkeyNumber].TestNumber = int.Parse(line.Split("  Test: divisible by ")[1]);
                }
                if (line.StartsWith("    If true: throw to monkey "))
                {
                    monkeys[monkeyNumber].IfTrueThrowToMonkey = int.Parse(line.Split("    If true: throw to monkey ")[1]);
                }
                if (line.StartsWith("    If false: throw to monkey "))
                {
                    monkeys[monkeyNumber].IfFalseThrowToMonkey = int.Parse(line.Split("    If false: throw to monkey ")[1]);
                }
            }

            return monkeys;
        }
    }

    internal class Monkey
    {
        public Monkey(Monkey[] monkeys, int name)
        {
            Monkeys = monkeys;
            Name = name;
        }

        public int Name { get; set; }
        public Queue<long> Items = new();

        public string? Operation { get; set; }
        public int OperationNumber { get; set; }

        public long TestNumber { get; set; }
        public int IfTrueThrowToMonkey { get; set; }
        public int IfFalseThrowToMonkey { get; set; }

        public long InspectedItems { get; private set; }

        public void InspectItems(long mod)
        {
            if (Items.Count > 0)
            {
                while (Items.TryDequeue(out long item))
                {
                    InspectedItems++;
                    switch (Operation)
                    {
                        case "*":
                            if (OperationNumber != -1)
                            {
                                item *= OperationNumber;
                            }
                            else
                            {
                                item *= item;
                            }
                            break;
                        case "+":
                            item += OperationNumber;
                            break;
                        default:
                            break;
                    }

                    item %= mod;

                    Test(item);
                }
            }
        }

        public void Test(long item)
        {
            if (item % TestNumber == 0)
            {
                Monkeys[IfTrueThrowToMonkey].Items.Enqueue(item);
            }
            else
            {
                Monkeys[IfFalseThrowToMonkey].Items.Enqueue(item);
            }
        }
        public Monkey[] Monkeys { get; set; }

        public override string ToString()
        {
            return $"Monkey {Name} (Inspected: {InspectedItems}): {string.Join(", ", Items.ToList())}";
        }
    }
}