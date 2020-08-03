using Microsoft.Extensions.DependencyInjection;
using MowTheLawn.FileRepo;
using MowTheLawn.Interfaces;
using System;

namespace MowTheLawn.InversionOfControl
{
    public sealed class InversionOfControlService
    {
        private static InversionOfControlService _instance = null;
        private static readonly object _padlock = new object();

        public IServiceProvider IoC;

        private InversionOfControlService()
        {
            IoC = new ServiceCollection()
                .AddSingleton<IFileRepository, FileRepository>()
                .AddSingleton<IInputParser, InputParser>()
                .AddSingleton<IOutputBuilder, OutputBuilder>()
                .AddSingleton<LawnMowerManagerParallel>()
                .BuildServiceProvider();
        }

        public static InversionOfControlService I
        {
            get
            {
                lock(_padlock)
                {
                    if (_instance == null) _instance = new InversionOfControlService();
                    return _instance;
                }
            }
        }
    }
}
