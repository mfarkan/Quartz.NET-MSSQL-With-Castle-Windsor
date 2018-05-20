using System;
using QuartzService.Folders.Interfaces;
using Quartz.Spi;
using Quartz;
using Quartz.Logging;
using QuartzService.Folders.Jobs;
using QuartzService.Folders.Classes;
using System.Linq;

namespace QuartzService.Folders.Classes
{
    public class QuartzInitializer : IQuartzInitializer
    {
        private readonly IJobFactory _jobFactory;
        private readonly IScheduler _sched;
        public void Start()
        {
            _sched.JobFactory = _jobFactory;
            _sched.Start();
            var JobList = JobRegistrar.GetJobTypes();
            foreach (var jobType in JobList)
            {
                var JobDescriptionAtt = jobType.GetCustomAttributes(typeof(JobDescriptionAttribute), false);

                if (JobDescriptionAtt.Length > 0)
                {
                    string jobDescription = ((JobDescriptionAttribute)(JobDescriptionAtt[0])).JobDescription;
                    string jobCron = ((JobDescriptionAttribute)(JobDescriptionAtt[0])).JobCron;

                    IJobDetail job = JobBuilder.Create(jobType)
                                    .WithDescription(jobDescription)
                                    .WithIdentity(jobDescription)
                                    .Build();

                    ITrigger trigger = TriggerBuilder.Create()
                            .WithIdentity(jobDescription, "BankJobGroup")
                            .WithCronSchedule(jobCron, q => q.InTimeZone(TimeZoneInfo.Local))
                            .ForJob(job)
                            .Build();
                    ScheduleJob(job, trigger);
                }
            }
        }
        public ITrigger CreateCronScheduler(string triggerId, string triggerDesc, IJobDetail job, string cronExp)
        {
            return TriggerBuilder.Create()
                .WithIdentity(triggerId)
                .WithDescription(triggerDesc)
                .StartNow()
                .WithCronSchedule(cronExp, x => x.InTimeZone(TimeZoneInfo.Local))
                .ForJob(job)
                .Build();
        }
        public ITrigger CreateSimpleMinutesTriggerForever(string triggerId, string triggerDesc, IJobDetail job, int min)
        {
            return TriggerBuilder.Create()
                .WithIdentity(triggerId)
                .WithDescription(triggerDesc)
                .ForJob(job)
                .WithSimpleSchedule(
                        x => x.WithIntervalInMinutes(min)
                        .RepeatForever()
                        )
                .Build();
        }
        public ITrigger CreateSimpleOneTimeTrigger(string triggerId, string triggerDesc, IJobDetail job, double min)
        {
            return TriggerBuilder.Create()
                .WithIdentity(triggerId)
                .WithDescription(triggerDesc)
                .ForJob(job)
                .StartAt(DateTime.Now.AddMinutes(min))
                .Build();
        }
        public IJobDetail CreateJob<T>(string jobDescription, string jobName)
        {
            IJobDetail job = JobBuilder.Create(typeof(T))
                .WithDescription(jobDescription)
                .WithIdentity(jobName)
                .Build();
            return job;
        }
        public void ScheduleJob(IJobDetail job, ITrigger trigger)
        {
            _sched.ScheduleJob(job, trigger);
        }
        public QuartzInitializer(IJobFactory jobFactory, IScheduler sched)
        {
            this._jobFactory = jobFactory;
            this._sched = sched;
        }
    }
}