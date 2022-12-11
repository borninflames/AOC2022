namespace AOC2022_Day_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2022 Day 10");
            var instructions = File.ReadAllLines("Input2.txt");
            var cpu = new CPU(instructions);
            Console.Clear();
            var signalLengths = cpu.Run(240).ToArray();
            
            //var sum = signalLengths[19] +
            //signalLengths[59]+
            //signalLengths[99]+
            //signalLengths[139]+
            //signalLengths[179]+
            //signalLengths[219];
            //Console.WriteLine(sum);
        }
    }

    internal class CPU
    {
        public CPU(string[] instructions)
        {
            foreach (var instruction in instructions)
            {
                Instructions.Add(new Instruction(instruction));
            }
        }
        public int X { get; set; } = 1;
        public int Circle { get; set; }

        public List<Instruction> Instructions { get; set; } = new();
        
        public IEnumerable<int> Run(int cycles) {
            var i = 0;
            for (int cycle = 1; cycle <= cycles; cycle++)
            {
                yield return cycle * X;
                var Instr = Instructions[i];
                DrawPixel(X, cycle);
                switch (Instr.Command)
                {
                    case "noop":                        
                        i++;
                        break;
                    case "addx":                       
                        if (Instr.CurrentCycle > 1)
                        {
                            X += Instr.Num;                            
                            Instr.CurrentCycle = 0;
                            i++;
                        }
                        Instr.CurrentCycle++;
                        break;
                    default:
                        break;
                }
                
                if (i == Instructions.Count)
                {
                    i = 0;
                }
            }
        }

        public static void DrawPixel(int spritePos, int cycle)
        {
            var pos = (cycle-1) % 40;
            if (spritePos - 1 <= pos && spritePos + 1 >= pos )
            {
                Console.Write('#');
            }
            else
            {
                Console.Write(' ');
            }
            if (cycle % 40 == 0 )
            {
                Console.WriteLine();
            }
        }
    }

    internal class Instruction
    {
        public Instruction(string instruction)
        {
            var instr = instruction.Split(' ');
            Command = instr[0];
            switch (Command)
            {
                case "noop":
                    Cycles = 1; 
                    break;
                case "addx":
                    Num = int.Parse(instr[1]);
                    Cycles = 2; 
                    break;
                default:
                    break;
            }
        }

        public string Command { get; set; }
        public int Num { get; set; }
        public int Cycles { get; set; }
        public int CurrentCycle { get; set; } = 1;
    }
}