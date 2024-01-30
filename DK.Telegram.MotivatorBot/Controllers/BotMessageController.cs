using DK.Telegram.MotivatorBot.Bots;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DK.Telegram.MotivatorBot.Controllers;

[ApiController]
[Route("api/message")]
public class BotMessageController: ControllerBase
{
    private readonly TelegramBotClient _telegramBotClient;

    public BotMessageController(Bot bot)
    {
        _telegramBotClient = bot.CreateTelegramBotClient().Result;
    }
    
    [HttpPost("update")]
    public async Task Update(Update update)
    {
        await _telegramBotClient.SendTextMessageAsync(update.Id, update.Message?.Text);
    }
}