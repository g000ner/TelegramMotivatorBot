using Telegram.Bot;

namespace DK.Telegram.WhipAndCakeBot.Host.Bots;

public class Bot
{
    public ITelegramBotClient BotClient;
    private readonly IConfiguration _configuration;

    public Bot(IConfiguration configuration)
    {
        _configuration = configuration;
        SetupBotClientAsync().Wait();
    }

    public async Task SetupBotClientAsync()
    {
        var telegramBotClient = new TelegramBotClient(_configuration.GetSection("Token").Value);
        var hook = $"{_configuration.GetSection("Url").Value}/api/message";
        await telegramBotClient.SetWebhookAsync(hook);
        BotClient = telegramBotClient;
    }
}