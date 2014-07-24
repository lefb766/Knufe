using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Knufe
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("no command given.");
                Environment.Exit(1);
            }

            switch (args[0].ToLower())
            {
                case "help":
                    PrintHelp();
                    break;

                case "picklibdirs":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("no parameter given");
                        Environment.Exit(1);
                    }

                    PickLibDirs(args[1]);
                    break;

                default:
                    Console.WriteLine("unknown command: {0}", args[0]);
                    Console.WriteLine();
                    PrintHelp();
                    Environment.Exit(1);
                    break;
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("    knufe.exe <command> [options]");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.Write("    PickLibDir <packageDir> ");
            Console.WriteLine("# list lib dirs appropriate to the current framework");
            Console.Write("    Help ");
            Console.WriteLine("# print this help");
        }

        static void PickLibDirs(string packagePath)
        {
            packagePath = Path.GetFullPath(packagePath);

            var defaultLibDirPath = Path.Combine(new[] { packagePath, "lib" });

            Console.WriteLine(defaultLibDirPath);

            var frameworkSpecificLibDir =
                Directory.EnumerateDirectories(defaultLibDirPath)
                .Select(d => new FrameworkDirectory(d))
                .Where(d => d.IsAvailableOnCurrentFramework())
                .OrderByDescending(d => d.FrameworkName.Version)
                .FirstOrDefault();

            if (frameworkSpecificLibDir != null)
            {
                Console.WriteLine(frameworkSpecificLibDir.Path);
            }
        }
    }
}
