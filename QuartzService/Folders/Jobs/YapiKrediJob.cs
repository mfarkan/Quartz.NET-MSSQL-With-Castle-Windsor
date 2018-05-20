using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Logging;
using QuartzService.Folders.Interfaces;
using QuartzService.Folders.Classes;

namespace QuartzService.Folders.Jobs
{
    [JobDescription("YapıKrediJob", "0 */10 * ? * *")]
    public class YapiKrediJob : IYapiKrediJob
    {
        public YapiKrediJob()
        {
            this.BankName = "YapıKredi";
            this.BankRequestTimeSec = 1;
            this.BankId = 1;
        }
        public int BankId { get; set; }

        public string BankName { get; set; }
        public int BankRequestTimeSec { get; set; }
        public string IBAN { get; set; }

        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from " + context.JobDetail.Key.Name + "! " + DateTime.Now.ToLongTimeString() + " ");
        }
    }
}
