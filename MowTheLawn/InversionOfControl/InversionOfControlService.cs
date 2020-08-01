using Microsoft.Extensions.DependencyInjection;
using MowTheLawn.FileRepo;
using MowTheLawn.Interfaces;

namespace MowTheLawn.InversionOfControl
{
    public sealed class InversionOfControlService
    {
        private static InversionOfControlService instance = null;
        private static readonly object padlock = new object();

        public ServiceProvider IoC;

        private InversionOfControlService()
        {
            //setup our DI
            IoC = new ServiceCollection()
                .AddSingleton<IFileRepository, FileRepository>()
                .AddSingleton<IInputParser, InputParser>()
                .AddSingleton<IOutputBuilder, OutputBuilder>()
                .BuildServiceProvider();
        }

        public static InversionOfControlService I
        {
            get
            {
                lock(padlock)
                {
                    if (instance == null) instance = new InversionOfControlService();
                    return instance;
                }
            }
        }


    }
}
