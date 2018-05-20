using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzService.Folders.Classes
{
    [AttributeUsage(AttributeTargets.All)]
    public class JobDescriptionAttribute : Attribute
    {
        public string JobDescription { get; set; }
        public string JobCron { get; set; }
        public JobDescriptionAttribute(string jobDescription, string jobCron)
        {
            this.JobDescription = jobDescription;
            this.JobCron = jobCron;
        }
    }
}
