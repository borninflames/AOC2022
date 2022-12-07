using System.Threading.Tasks.Dataflow;

namespace AOC2022_Day_07
{
    internal class Program
    {
        static int Sum { get; set; }
        static int SizeLimit { get; set; } = 100000;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2022 Day 07");
            var lines = File.ReadAllLines("Input2.txt");            
            var root = new Dir("/", null);
            var current = root;
            foreach (var line in lines)
            {
                Console.WriteLine(line);
                if (line.StartsWith("$"))
                {                    
                    var command = line.Split(' ');
                    switch (command[1])
                    {
                        case "cd":
                            if (current == null)
                            {
                                throw new Exception("Ez mán üres");
                            }
                            if (command[2] == "..")
                            {
                                if (current.Parent == null)
                                {
                                    
                                    Console.WriteLine($"{current} is the root, you can't go up...");
                                }
                                current = current?.Parent;
                            }
                            else
                            {
                                if (command[2] != "/")
                                {
                                    var nextDir = current.SubDirs.FirstOrDefault(d => d.Name == command[2]);
                                    if (nextDir == null) {
                                        Console.WriteLine($"Can't find {command[2]} in {current} dir...");
                                    }
                                    current = nextDir;
                                }                                
                            }
                            break;
                        case "ls":
                            break;
                        default:
                            throw new Exception("Houston, baj van!");
                    }
                }
                else
                {
                    var info = line.Split(' ');
                    if (info[0] == "dir")
                    {
                        current?.SubDirs.Add(new Dir(info[1], current));
                    }
                    else
                    {
                        current?.Files.Add(new FileNode(info[1], int.Parse(info[0]), current));
                    }
                }
            }

            Console.WriteLine(GetDirSize(root));
            Console.WriteLine(Sum);
        }

        static int GetDirSize(Dir node)
        {
            var fileSizeSum = node.Files.Sum(f => f.Size);
            var subDirSizeSum = node.SubDirs.Sum(d => GetDirSize(d));
            var fullSize = fileSizeSum + subDirSizeSum;
            if (fullSize <= SizeLimit)
            {
                Sum += fullSize;
            }
            return fullSize;
        }
    }

    internal class Dir
    {
        public Dir(string name, Dir? parent)
        {
            Name = name;
            Parent = parent;
        }

        public string Name { get; set; }
        public Dir? Parent { get; set; }

        public List<Dir> SubDirs { get; set; } = new();
        public List<FileNode> Files { get; set; } = new();

        public override string ToString()
        {
            return Name;
        }

    }
    internal class FileNode
    {
        public FileNode(string name, int size, Dir? parent)
        {
            Name = name;
            Size = size;
            Parent = parent;
        }

        public string Name { get; set; }
        public int Size { get; set; }

        public Dir? Parent { get; set; }

        public override string ToString()
        {
            return $"{Name} - [{Size}]";
        }
    }
}