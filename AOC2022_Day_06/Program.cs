namespace AOC2022_Day_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2022 Day 06");

            var signals = File.ReadAllText("Input2.txt");

            Console.WriteLine(HasDistincCharactersAt(signals, 4));
            Console.WriteLine(HasDistincCharactersAt(signals, 14));
        }

        private static int HasDistincCharactersAt(string signals, int length)
        {
            var isStartOfPacketFound = false;
            var i = -1;

            while (!isStartOfPacketFound && i < signals.Length - length)
            {
                i++;
                var subString = signals.Substring(i, length);
                isStartOfPacketFound = !subString.GroupBy(g => g).Select(g => g.Count()).Any(c => c > 1);
            }

            return i + length;
        }
    }
}