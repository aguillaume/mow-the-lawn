using Microsoft.Extensions.DependencyInjection;
using MowTheLawn.FileRepo;
using MowTheLawn.Interfaces;
using MowTheLawn.InversionOfControl;
using System;
using System.Collections.Generic;

namespace MowTheLawn
{

    class Program
    {
        private readonly static LawnMowerManagerParallel _managerParallel = InversionOfControlService.I.IoC.GetRequiredService<LawnMowerManagerParallel>();
        private readonly static IFileRepository _fileRepo = InversionOfControlService.I.IoC.GetRequiredService<IFileRepository>();
        private readonly static IOutputBuilder _outputBuilder = InversionOfControlService.I.IoC.GetRequiredService<IOutputBuilder>();

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
                var instructions = _fileRepo.GetInstructions(filePath);
                var mowers = _managerParallel.RunMowers(instructions);
                Console.WriteLine(_outputBuilder.GetOutput(mowers));

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
