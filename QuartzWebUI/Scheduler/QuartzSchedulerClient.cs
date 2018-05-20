using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using QuartzWebUI.Models;
using Quartz.Impl.Matchers;

namespace QuartzWebUI.Scheduler
{
    public sealed class QuartzSchedulerClient : IQuartzSchedulerClient
    {

        private List<Jobs> jobDTOList;
        private readonly IScheduler _scheduler = null;

        public QuartzSchedulerClient(IScheduler scheduler)
        {
            this._scheduler = scheduler;
        }

        public bool DeleteJob(string JobID)
        {
            try
            {
                _scheduler.DeleteJob(new JobKey(JobID));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteJobs()
        {
            try
            {
                _scheduler.DeleteJobs(getAllJobInSch());
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IReadOnlyCollection<JobKey> getAllJobInSch()
        {
            var list = _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result;
            return list;
        }

        public IReadOnlyCollection<ITrigger> getAllTriggerInSch(JobKey jobKey)
        {
            return _scheduler.GetTriggersOfJob(jobKey).Result;
        }

        public IJobDetail GetJobDetail(JobKey jobKey)
        {
            return
                _scheduler.GetJobDetail(jobKey).Result;
        }

        public IList<Jobs> GetSchedulerDetails()
        {
            jobDTOList = new List<Jobs>();
            foreach (var jobKey in getAllJobInSch())
            {
                Jobs JobDTO = new Jobs();
                var jobDetail = GetJobDetail(jobKey);
                foreach (var trigger in getAllTriggerInSch(jobKey))
                {
                    JobDTO.Cron = TryCronParse(trigger) ? ((ICronTrigger)trigger).CronExpressionString : "NULL";

                    JobDTO.CreateTime = trigger.StartTimeUtc.LocalDateTime.ToString();

                    JobDTO.NextExecution = trigger.GetNextFireTimeUtc().HasValue
                        ? trigger.GetNextFireTimeUtc().Value.LocalDateTime.ToString()
                        : string.Empty;

                    JobDTO.LastExecution = trigger.GetPreviousFireTimeUtc().HasValue
                        ? trigger.GetPreviousFireTimeUtc().Value.LocalDateTime.ToString()
                        : string.Empty;

                    JobDTO.JobId = jobDetail.Key.Name;
                    JobDTO.JobName = jobDetail.Key.Group + "" + jobDetail.Key.Name;

                    JobDTO.TimeZone = TryCronParse(trigger) ? ((ICronTrigger)trigger).TimeZone.DisplayName : "NULL";
                }
                jobDTOList.Add(JobDTO);
            }
            return jobDTOList;
        }

        public bool TriggerJob(string JobID)
        {
            try
            {
                _scheduler.TriggerJob(new JobKey(JobID));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool TriggerJobs()
        {
            try
            {
                foreach (var jobKey in getAllJobInSch())
                {
                    _scheduler.TriggerJob(jobKey);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool TryCronParse(object value)
        {
            try
            {
                var cronTrigger = ((ICronTrigger)value);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}