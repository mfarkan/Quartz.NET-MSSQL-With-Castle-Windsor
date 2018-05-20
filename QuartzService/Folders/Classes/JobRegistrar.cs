using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuartzService.Folders.Classes
{
    public class JobRegistrar
    {
        private readonly IWindsorContainer _container;

        public JobRegistrar(IWindsorContainer container)
        {
            _container = container;
        }

        public static IEnumerable<Type> GetJobTypes()
        {
            var JobList = AppDomain.CurrentDomain.GetAssemblies().ToList()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IJob).IsAssignableFrom(p) && !p.IsInterface);
            return JobList;
        }

        public void RegisterJobs()
        {//interceptor koyulabilir.
            var jobTypes = GetJobTypes();
            foreach (Type jobType in jobTypes)
            {
                _container.Register(
                    Component.For(jobType).ImplementedBy(jobType)
                    .LifeStyle.Singleton
                    );
            }
        }
    }
}