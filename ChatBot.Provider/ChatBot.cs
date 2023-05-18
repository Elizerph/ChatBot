using System.Text;

namespace ChatBot.Provider
{
    public abstract class ChatBot : IChatBot
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiEndpoint;

        protected ChatBot(HttpClient httpClient, string apiEndpoint)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiEndpoint = apiEndpoint ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> SendRequest(string text, string? parentMessageId)
        {
            using var content = new StringContent(ConvertToRequestContent(text, parentMessageId), Encoding.UTF8, "application/json");
            using var responseContent = await _httpClient.PostAsync(_apiEndpoint, content);
            using var responseStream = await responseContent.Content.ReadAsStreamAsync();
            using var responseStreamReader = new StreamReader(responseStream);
            var responseLines = await responseStreamReader.ReadLinesAsync().ToListAsync();
            return ConvertFromResponseLine(string.Join(Environment.NewLine, responseLines));
        }

        protected abstract string ConvertFromResponseLine(string response);

        protected abstract string ConvertToRequestContent(string text, string? parentMessageId);
    }
}
