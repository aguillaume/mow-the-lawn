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
            var filePath = args[0];
            var manager = new LawnMowerManagerParallel(filePath);

            Console.WriteLine(manager.RunMowers());
            Console.ReadLine();
        }
    }
}
