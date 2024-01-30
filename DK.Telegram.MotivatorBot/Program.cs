using DSK.Telegram.MotivatorBot;

public class Program
{
    public static Task Main(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(app =>
            {
                app.UseStartup<Startup>();
            })
            .Build()
            .RunAsync();
    }
}