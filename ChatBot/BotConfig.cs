using Newtonsoft.Json;

namespace ChatBot
{
    public class BotConfig
    {
        public const string TelegramTokenName = "telegrambottoken";
        public const string DiscordTokenName = "discordbottoken";

        [JsonProperty(PropertyName = TelegramTokenName)]
        public string TelegramToken { get; set; }
        [JsonProperty(PropertyName = DiscordTokenName)]
        public string DiscordToken { get; set; }
    }
}
