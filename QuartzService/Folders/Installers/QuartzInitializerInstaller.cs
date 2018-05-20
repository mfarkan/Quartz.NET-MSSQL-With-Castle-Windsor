using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using QuartzService.Folders.Interfaces;
using QuartzService.Folders.Classes;

namespace QuartzService.Folders.Installers
{
    public class QuartzInitializerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IQuartzInitializer>().ImplementedBy<QuartzInitializer>().LifeStyle.Transient);
        }
    }
}