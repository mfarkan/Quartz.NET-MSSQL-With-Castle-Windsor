using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System.Threading.Tasks;
using Quartz.Core;
using QuartzWebUI.Models;
using QuartzWebUI.Scheduler;

namespace QuartzWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuartzSchedulerClient _client;
        private readonly IScheduler _scheduler;
        public HomeController(IQuartzSchedulerClient client, IScheduler scheduler)
        {
            this._client = client;
            this._scheduler = scheduler;
            
        }
        public ActionResult Index()
        {
            this.models = new List<Jobs>();
            models = _client.GetSchedulerDetails();
            return View(models);
        }
        private IList<Jobs> models { get; set; }

        public ActionResult TriggerQuartzJob(string JobId)
        {
            try
            {
                _client.TriggerJob(JobId);
                TempData["Success"] = "Job was succesfully triggered!";
                return RedirectToAction("Index");
            }
            catch (SchedulerException e)
            {
                TempData["Error"] = "Unexpected error : " + e.Message + "";
                return RedirectToAction("Index");
            }
        }
        public ActionResult DeleteAllQJob()
        {
            try
            {
                _client.DeleteJobs();
                TempData["Success"] = "Jobs were succesfully deleted!";
                return RedirectToAction("Index");
            }
            catch (SchedulerException e)
            {
                TempData["Error"] = "Unexpected error : " + e.Message + "";
                return RedirectToAction("Index");
            }
        }
        public ActionResult TriggerAllQJob()
        {
            try
            {
                _client.TriggerJobs();
                TempData["Success"] = "Jobs were succesfully triggered!";
                return RedirectToAction("Index");
            }
            catch (SchedulerException e)
            {
                TempData["Error"] = "Unexpected error : " + e.Message + "";
                return RedirectToAction("Index");
            }
        }
        public ActionResult DeleteQuartzJob(string JobId)
        {
            try
            {
                _client.DeleteJob(JobId);
                TempData["Success"] = "Job was succesfully deleted!";
                return RedirectToAction("Index");

            }
            catch (SchedulerException e)
            {
                TempData["Error"] = "Unexpected error : " + e.Message + "";
                return RedirectToAction("Index");
            }
        }
    }
}