using Newtonsoft.Json;

using System.Text;

namespace ChatBot.Provider
{
    public class TheBai
    {
        private const string ApiEndpoint = @"https://chatbot.theb.ai/api/chat-process";
        private readonly Lazy<HttpClient> _httpClient = new(CreateClient);

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

        public async Task<TheBaiResponseItem> SendRequest(TheBaiRequest request)
        {
            var requestJson = JsonConvert.SerializeObject(request);
            using var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            using var responseContent = await _httpClient.Value.PostAsync(ApiEndpoint, content);
            using var responseStream = await responseContent.Content.ReadAsStreamAsync();
            using var responseStreamReader = new StreamReader(responseStream);
            var responseLine = await responseStreamReader.ReadLinesAsync().LastAsync();
            return JsonConvert.DeserializeObject<TheBaiResponseItem>(responseLine);
        }
    }
}
