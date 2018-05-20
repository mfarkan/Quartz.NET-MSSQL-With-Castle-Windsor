using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartzWebUI.Models
{
    public class Jobs
    {
        public string JobId { get; set; }
        public string Cron { get; set; }
        public string TimeZone { get; set; }
        public string JobName { get; set; }
        public string NextExecution { get; set; }
        public string LastExecution { get; set; }
        public string CreateTime { get; set; }


    }
}