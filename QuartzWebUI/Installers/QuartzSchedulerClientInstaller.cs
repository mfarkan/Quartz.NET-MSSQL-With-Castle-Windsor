using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using QuartzWebUI.Scheduler;

namespace QuartzWebUI.Installers
{
    public class QuartzSchedulerClientInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(
                Component.For<IQuartzSchedulerClient>().ImplementedBy<QuartzSchedulerClient>().LifestyleSingleton()
            );
        }
    }
}