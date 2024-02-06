using DK.Telegram.WhipAndCakeBot.Host.Bots;
using Newtonsoft.Json.Serialization;

namespace DK.Telegram.WhipAndCakeBot.Host;

public class Startup
{
    /// <summary>
    ///     Configuration of web application
    /// </summary>
    private IConfiguration Configuration { get; }

    /// <summary>
    ///     Application web host environment
    /// </summary>
    private IWebHostEnvironment WebHostEnvironment { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="webHostEnvironment"></param>
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        Configuration = configuration;
        WebHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    ///     Метод, в котором происходит конфигурация сервисов приложения. Таких как логирование, 
    /// доступ к БД, и к другим инфраструктурным вещам
    /// </summary>
    /// <param name="services">Web app services collection</param>
    public void ConfigureServices(IServiceCollection services)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        builder.AddEnvironmentVariables();
        var config = builder.Build();
        
        services.AddLogging();
        // todo пока создаю бота через контроллер - иначе не пробрасывается веб хук до tg api
        services.AddSingleton(new Bot(config));
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
        });
    }

    /// <summary>
    /// Этот метод вызывается во время работы приложения (в рантайме) 
    /// Здесь происходит конфигурация пайплайна обработки входящих запросов к серверу
    /// </summary>
    /// <param name="app">IApplication Builder object</param>
    /// <param name="env">IWwbHostEnvironment object</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}