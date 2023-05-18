using ChatBot.Provider;

using ElizerBot;

namespace ChatBot
{
    internal class Program
    {
        private const string BotConfigFile = "BotConfig.txt";

        static async Task Main(string[] args)
        {
            var gtp = new TheBai();
            var updateHandler = new UpdateHandler(gtp);
            var tokens = await GetTokens();
            var bots = tokens.Select(p => updateHandler.BuildAdapter(p.Key, p.Value));
            var commands = new Dictionary<string, string> { { "bonk", "Clears bot context" } };
            foreach (var bot in bots)
            {
                await bot.Init();
                await bot.SetCommands(commands);
            }
            await Task.Delay(-1);
        }

        private static async Task<Dictionary<SupportedMessenger, string>> GetTokens()
        {
            var result = new Dictionary<SupportedMessenger, string>();

            var telegramToken = Environment.GetEnvironmentVariable(BotConfig.TelegramTokenName);
            if (!string.IsNullOrEmpty(telegramToken))
                result[SupportedMessenger.Telegram] = telegramToken;
            var discordToken = Environment.GetEnvironmentVariable(BotConfig.DiscordTokenName);
            if (!string.IsNullOrEmpty(discordToken))
                result[SupportedMessenger.Discord] = discordToken;

            if (File.Exists(BotConfigFile))
            {
                var lines = await File.ReadAllLinesAsync(BotConfigFile);
                foreach (var line in lines)
                {
                    var parts = line.Split('\t');
                    if (parts.Length == 2)
                    {
                        if (string.Equals(parts[0], BotConfig.TelegramTokenName))
                            result[SupportedMessenger.Telegram] = parts[1];
                        if (string.Equals(parts[0], BotConfig.DiscordTokenName))
                            result[SupportedMessenger.Discord] = parts[1];
                    }
                }
            }
            return result;
        }
    }
}