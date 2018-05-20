using Quartz;

namespace QuartzService.Folders.Interfaces
{
    public interface IBankJobBase : IJob
    {
        int BankId { get; set; }
        string BankName { get; set; }
        int BankRequestTimeSec
        {
            get; set;
        }
    }
}