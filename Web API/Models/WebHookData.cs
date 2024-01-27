namespace Sample07_Service.Models
{

    public class WebHookData
    {
        public string destination { get; set; }
        public Event[] events { get; set; }
    }

    public class Event
    {
        public string replyToken { get; set; }
        public string type { get; set; }
        public string mode { get; set; }
        public long timestamp { get; set; }
        public Source source { get; set; }
        public string webhookEventId { get; set; }
        public Deliverycontext deliveryContext { get; set; }
        public HookMessage message { get; set; }
    }

    public class Source
    {
        public string type { get; set; }
        //public string groupId { get; set; } //
        public string userId { get; set; }
    }

    public class Deliverycontext
    {
        public bool isRedelivery { get; set; }
    }

    public class HookMessage
    {
        public string id { get; set; }
        public string type { get; set; }
        public string quoteToken { get; set; } //
        public string text { get; set; }
        //public Emoji[] emojis { get; set; } 
        //public Mention mention { get; set; }
    }

    public class Mention
    {
        public Mentionee[] mentionees { get; set; }
    }

    public class Mentionee
    {
        public int index { get; set; }
        public int length { get; set; }
        public string type { get; set; }
        public string userId { get; set; }
    }

    public class Emoji
    {
        public int index { get; set; }
        public int length { get; set; }
        public string productId { get; set; }
        public string emojiId { get; set; }
    }

}