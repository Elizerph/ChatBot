using ChatBot.Provider;

using ElizerBot.Adapter;

namespace ChatBot
{
    internal class UpdateHandler : IBotAdapterUpdateHandler
    {
        private readonly AutoMap<string, bool> _lock = new(e => false);
        private readonly AutoMap<string, string?> _historyId = new(e => null);
        private readonly TheBai _chatBot;

        public UpdateHandler(TheBai chatBot)
        {
            _chatBot = chatBot ?? throw new ArgumentNullException(nameof(chatBot));
        }

        public Task HandleButtonPress(IBotAdapter bot, PostedMessageAdapter message, UserAdapter user, string buttonData)
        {
            return Task.CompletedTask;
        }

        public async Task HandleCommand(IBotAdapter bot, ChatAdapter sourceChat, UserAdapter sourceUser, string command)
        {
            if (string.Equals(command, "bonk"))
            {
                var key = $"{sourceChat.Id}:{sourceUser.Id}";
                if (_lock[key])
                {
                    await bot.SendMessage(new NewMessageAdapter(sourceChat)
                    {
                        Text = "Держу удар!"
                    });
                }
                else
                {
                    _historyId[key] = null;
                    await bot.SendMessage(new NewMessageAdapter(sourceChat)
                    {
                        Text = "Ай, больно!"
                    });
                }
            }
        }

        public async Task HandleIncomingMessage(IBotAdapter bot, PostedMessageAdapter message)
        {
            if (!string.IsNullOrEmpty(message.Text))
            {
                try
                {
                    var key = $"{message.Chat.Id}:{message.User.Id}";
                    if (!_lock[key])
                    {
                        _lock[key] = true;
                        var request = new TheBaiRequest
                        {
                            prompt = message.Text,
                            options = new TheBaiRequestOptions
                            {
                                parentMessageId = _historyId[key]
                            }
                        };
                        var response = await _chatBot.SendRequest(request);
                        _historyId[key] = response.id;
                        await bot.SendLongTextMessage(response.text, message.Chat);
                        _lock[key] = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await bot.SendMessage(new NewMessageAdapter(message.Chat)
                    {
                        Text = "Упс, что-то сломалось"
                    });
                }
            }
        }
    }
}
