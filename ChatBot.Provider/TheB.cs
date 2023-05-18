using Newtonsoft.Json;

namespace ChatBot.Provider
{
    public class TheB : ChatBot
    {
        private static readonly char[] _splitter = new[] { '\n', '\r' };

        public TheB()
            : base(CreateClient(), @"https://chatbot.theb.ai/api/chat-process")
        {
        }

        private static HttpClient CreateClient()
        {
            var result = new HttpClient();
            var headers = new Dictionary<string, string>
            {
                {  "authority", "chatbot.theb.ai" },
                {  "origin", "https://chatbot.theb.ai" },
                {  "user-agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36" },
            };
            foreach (var header in headers)
                result.DefaultRequestHeaders.Add(header.Key, header.Value);
            return result;
        }

        private class TheBRequest
        {
            public string prompt { get; set; }
            public TheBRequestOptions options { get; set; }
        }

        private class TheBRequestOptions
        {
            public string? parentMessageId { get; set; }
        }

        private class TheBReqponseItem
        {
            public string role { get; set; }
            public string id { get; set; }
            public string parentMessageId { get; set; }
            public string text { get; set; }
            public string delta { get; set; }
        }

        protected override string ConvertFromResponseLine(string response)
        {
            var lastLine = response.Split(_splitter).Last();
            return JsonConvert.DeserializeObject<TheBReqponseItem>(lastLine).text;
        }

        protected override string ConvertToRequestContent(string text, string? parentMessageId)
        {
            var request = new TheBRequest
            {
                prompt = text,
                options = new TheBRequestOptions
                {
                    parentMessageId = parentMessageId
                }
            };
            return JsonConvert.SerializeObject(request);
        }
    }
}
