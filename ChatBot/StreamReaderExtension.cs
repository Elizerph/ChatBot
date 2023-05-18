namespace ChatBot.Provider
{
    public static class StreamReaderExtension
    {
        public static async IAsyncEnumerable<string> ReadLinesAsync(this StreamReader instance)
        {
            string? line = null;
            while ((line = await instance.ReadLineAsync()) != null)
                yield return line;
        }
    }
}
