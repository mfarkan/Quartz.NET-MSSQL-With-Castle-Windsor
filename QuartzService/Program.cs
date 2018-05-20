using System;
using QuartzService.Folders.Classes;
using QuartzService.Folders.Interfaces;
using Castle.Windsor;
using System.IO;

namespace QuartzService
{
    public class Program
    {
        static void Main(string[] args)
        {

            IWindsorContainer container = new WindsorContainer();
            Bootstrapper bs = new Bootstrapper(container);
            bs.Run();
            container.Resolve<IQuartzInitializer>().Start();
            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }     
    }
}
