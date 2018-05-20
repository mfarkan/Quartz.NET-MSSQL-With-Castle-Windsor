using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace QuartzWebUI.Installers.ContainerSetup
{
    public class IocContainer
    {
        private static IWindsorContainer _container;

        public static void Setup()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());

            WindsorControllerFactory.WindsorControllerFactory controllerFactory = new WindsorControllerFactory.WindsorControllerFactory(_container.Kernel);

            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}