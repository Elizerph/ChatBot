using ElizerBot.Adapter;

namespace ChatBot
{
    public static class BotAdapterExtension
    {
        public static async Task SendLongTextMessage(this IBotAdapter bot, string text, ChatAdapter chat)
        {
            foreach (var responsePartition in text.Split(' ').ByBatch(2000, e => e.Length + 1))
                await bot.SendMessage(new NewMessageAdapter(chat)
                {
                    Text = string.Join(' ', responsePartition)
                });
        }
    }
}
