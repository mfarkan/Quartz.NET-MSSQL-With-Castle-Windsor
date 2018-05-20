using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Quartz;
using Quartz.Impl;

namespace QuartzService.Folders.Installers
{
    public class QuartzSchedulerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            container.Register(
                Component.For<IScheduler>().Instance(schedFact.GetScheduler().Result).LifeStyle.Singleton
            );

        }
    }
}
