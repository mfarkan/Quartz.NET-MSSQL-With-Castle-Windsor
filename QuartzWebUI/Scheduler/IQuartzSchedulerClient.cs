using Quartz;
using QuartzWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzWebUI.Scheduler
{
    public interface IQuartzSchedulerClient
    {
        IList<Jobs> GetSchedulerDetails();
        IReadOnlyCollection<JobKey> getAllJobInSch();

        IReadOnlyCollection<ITrigger> getAllTriggerInSch(JobKey jobKey);
        IJobDetail GetJobDetail(JobKey jobKey);
        bool DeleteJobs();
        bool TriggerJob(string JobID);
        bool DeleteJob(string JobID);
        bool TriggerJobs();

    }
}
