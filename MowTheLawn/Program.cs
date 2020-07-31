using Microsoft.Extensions.DependencyInjection;
using MowTheLawn.FileRepo;
using System;
using System.Collections.Generic;

namespace MowTheLawn
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileRepository, FileRepository>()
                .BuildServiceProvider();

            var manager = new LawnMowerManagerParallel(serviceProvider.GetRequiredService<IFileRepository>());
            manager.RunMowers()

            Console.WriteLine("Hello World!");
            
            Console.ReadLine();
        }
    }
}
