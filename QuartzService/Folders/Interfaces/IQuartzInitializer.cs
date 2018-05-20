using Quartz;

namespace QuartzService.Folders.Interfaces
{
    public interface IQuartzInitializer
    {
        void Start();
        ITrigger CreateCronScheduler(string triggerId, string triggerDesc, IJobDetail job, string cronExp);
        ITrigger CreateSimpleMinutesTriggerForever(string triggerId, string triggerDesc, IJobDetail job, int min);
        ITrigger CreateSimpleOneTimeTrigger(string triggerId, string triggerDesc, IJobDetail job, double min);
        IJobDetail CreateJob<T>(string jobDescription, string jobName);
        void ScheduleJob(IJobDetail job, ITrigger trigger);
    }
}