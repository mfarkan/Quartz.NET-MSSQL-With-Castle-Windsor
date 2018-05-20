namespace QuartzService.Folders.Interfaces
{
    public interface IYapiKrediJob : IBankJobBase
    {//yapı kredi bankasına özel.
        string IBAN { get; set; }
    }
}