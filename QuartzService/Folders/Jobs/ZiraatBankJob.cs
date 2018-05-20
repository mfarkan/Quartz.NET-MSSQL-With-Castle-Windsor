using System;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Quartz;
using QuartzService.Folders.Interfaces;
using Quartz.Logging;
using QuartzService.Folders.Classes;

namespace QuartzService.Folders.Jobs
{
    [JobDescription("ZiraatBankJob", "0 */5 * ? * *")]
    public class ZiraatBankJob : IZiraatBankJob
    {
        public ZiraatBankJob()
        {
            this.BankName = "ZiraatBank";
            this.BankRequestTimeSec = 1;
            this.BankId = 2;
        }
        public int BankId { get; set; }

        public string BankName { get; set; }

        public int BankRequestTimeSec { get; set; }
        public string TCKN_VKN { get; set; }

        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from " + context.JobDetail.Key.Name + "! " + DateTime.Now.ToLongTimeString() + " ");
        }
    }
}