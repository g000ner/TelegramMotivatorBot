using Telegram.Bot;

namespace DK.Telegram.MotivatorBot.Bots;

public class Bot
{
    private readonly IConfiguration _configuration;

    public Bot(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<TelegramBotClient> CreateTelegramBotClient()
    {
        var telegramBotClient = new TelegramBotClient(_configuration.GetSection("Token").Value);
        var hook = $"{_configuration.GetSection("Url").Value}/api/message/update";
        await telegramBotClient.SetWebhookAsync(hook);
        return telegramBotClient;
    }
}