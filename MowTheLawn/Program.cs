using Microsoft.Extensions.DependencyInjection;
using MowTheLawn.FileRepo;
using MowTheLawn.InversionOfControl;
using System;
using System.Collections.Generic;

namespace MowTheLawn
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath;

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file path for instructions");
                filePath = Console.ReadLine();
            }
            else
            {
                if (args.Length > 1)
                {
                    Console.WriteLine("Only expected one Path to be passed in. Please provide a single path");
                    filePath = Console.ReadLine();
                }
                else
                {
                    filePath = args[0];
                }
            }
            try
            {
                var manager = new LawnMowerManagerParallel(filePath);
                Console.WriteLine(manager.RunMowers());

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Something went. See error below");
                Console.Error.WriteLine(ex.Message);
            }

            Console.WriteLine("End of Application. Press any key to close it.");
            Console.ReadKey();
        }
    }
}
