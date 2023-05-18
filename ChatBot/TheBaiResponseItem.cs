namespace ChatBot
{
    public class TheBaiResponseItem
    {
        public string role { get; set; }
        public string id { get; set; }
        public string parentMessageId { get; set; }
        public string text { get; set; }
        public string delta { get; set; }
    }
}
