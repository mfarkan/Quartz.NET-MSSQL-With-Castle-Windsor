# Quartz.NET-3.0.3-Multiple-Instance-with-MSSQL-Castle Windsor
<p>Quartz.NET scheduler kütüphanesi kullanılarak oluşturulan jobların yönetimi ve bu jobların izlenmesi için ise 
Sql Server Veritabanı kullanılmıştır.Bunun yanında uygulama içerisinde Quartz.NET'in desteklediği JobFactory ve JobLogger 
yapılarını da içinde barındırırken bunlarla birlikte yer alan Castle Windsor kullanımıyla bağımlılıklar yönetilmeye çalışılmıştır.</p>



<h5>Quartz.NET Nedir?</h5>
<p>Quartz.NET öncelikle java dili için çıkartılmış daha sonrasında .NET çatısı içinde geliştirilen , zamana dayalı görevler(job) 
tanımlayabildiğimiz open source bir kütüphanedir. </p>
<p><a href="https://www.quartz-scheduler.net/">Resmi sitesi</a> aracılığıyla dökümantasyonuna bakabilirsiniz.</p>

<p>Geliştirilen uygulama aşağıda bahsedilen istekleri kapsamaktadır ; </p>
<ul>
<li>Scheduler'ı console uygulaması olarak oluşturup multiple application server'da çalışacak şekilde düzenlendi.</li>
<li>Scheduler içerisinde JobFactory yer aldığı için Castle Windsor ile entegre edildi.</li>
<li>Quartz.NET içerisinde Log yapısı mevcut fakat Log4Net entegre edilebilir.</li>
<li>Uygulama içerisinde tanımlanan joblar Sql Server veritabanına kaydedildi.</li>
<li>Ayrıca uygulama içerisinde tanımlanan jobların Asp.NET MVC uygulaması sayesinde görsel olarak da izlendi ve tetiklenmesi , silinmesi sağlandı.</li>
  <li>Uygulama içerisinde tanımlanan Joblar (IJob kalıtım alanlar) dinamik olarak eklenir ve JobDescriptionAttribute ile açıklaması ve Cron zamanı belirlenir.</li>
</ul>

<h4>Uygulamanın Web Arayüzü</h4>
<img src="https://image.ibb.co/ftb6U7/image.png"/>

<h4>JobDescriptionAttribute</h4>
<p>Bu attribute sayesinde job cron zamanı ve job açıklamasını girdiğiniz de job Quartz içerisine ve veritabanına kayıt edilir , eğer bu attribute set edilmez ise herhangi bir kayıt işlemi olmaz.</p>

Örnek kayıt aşağıda ki şekildedir ;

YapıKredi Job'ı için 10 dakika da bir çalışması söylenmiştir.

    [JobDescription("YapıKrediJob", "0 */10 * ? * *")]
    public class YapiKrediJob : IYapiKrediJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from " + context.JobDetail.Key.Name + "! " + DateTime.Now.ToLongTimeString() + " ");
        }
<h4>Uygulama içerisindeki classlar</h4>
<ul>
  <li><b>Bootstrapper:</b> Container oluşturur ve installerları container'a atar.</li>
  <li><b>JobRegistrar:</b> IJob interface'den kalıtım alan tüm jobları aynı container atar. </li>
  <li><b>QuartzInitializer:</b> Quartz.NET için joblarının tanımlandığı ve attribute tanımlı olan classları Job'lara dahil eder.</li>
  <li><b>WindsorJobFactory:</b>Quartz.NET içerisinde tanımlı olan jobların instancelarının oluşturulması için container'dan ilgili job nesnesini resolve eder.</li>

</ul>
