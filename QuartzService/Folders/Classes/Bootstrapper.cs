using Castle.Windsor;
using Castle.Windsor.Installer;
using Quartz.Spi;
using Castle.MicroKernel.Registration;

namespace QuartzService.Folders.Classes
{
    public class Bootstrapper
    {
        public IWindsorContainer Container;

        public void Run()
        {


            IJobFactory jobFactory = new WindsorJobFactory(Container);
            Container.Register(Component.For<IJobFactory>().Instance(jobFactory).LifeStyle.Transient);

            this.Container.Install(FromAssembly.This());

            JobRegistrar jobRegistrar=new JobRegistrar(this.Container);

            jobRegistrar.RegisterJobs();
        }

        public Bootstrapper(IWindsorContainer container)
        {
            this.Container = container;
        }
    }
}
