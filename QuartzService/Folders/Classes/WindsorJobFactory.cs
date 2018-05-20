using System;
using Castle.Windsor;
using Quartz;
using Quartz.Spi;

namespace QuartzService.Folders.Classes
{
    public class WindsorJobFactory : IJobFactory
    {//trigger tetiklenirken ilgili job classını buradan çözüyor.
        private readonly IWindsorContainer _container;
        public WindsorJobFactory(IWindsorContainer container)
        {
            this._container = container;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_container.Resolve(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();
        }
    }
}