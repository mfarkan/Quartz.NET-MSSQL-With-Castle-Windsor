namespace QuartzService.Folders.Interfaces
{
    public interface IZiraatBankJob : IBankJobBase
    {//banka joblarına özel arayüz.
        string TCKN_VKN { get; set; }
    }
}