using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace QuartzWebUI.Installers
{
    public class ControllerInstallers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromThisAssembly()
                .Pick()
                .If(t => t.Name.EndsWith("Controller"))
                .Configure(configurer=>configurer.Named(configurer.Implementation.Name))
                .LifestylePerWebRequest());
        }
    }
}