using DK.Telegram.WhipAndCakeBot.Host.Bots;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DK.Telegram.WhipAndCakeBot.Host.Controllers;

[ApiController]
[Route("api/message")]
public class BotMessageController: ControllerBase
{
    private readonly ITelegramBotClient _telegramBotClient;

    public BotMessageController(Bot bot)
    {
        _telegramBotClient = bot.BotClient;
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] Update update)
    {
        await _telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, $"{update.Message.Chat.Username} написал(а): {update.Message?.Text}");
        return Ok();
    }
}