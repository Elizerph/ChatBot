namespace ChatBot.Provider
{
    public interface IChatBot
    {
        Task<string> SendRequest(string text, string? parentMessageId);
    }
}