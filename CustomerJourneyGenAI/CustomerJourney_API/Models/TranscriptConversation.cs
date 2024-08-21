namespace CustomerJourneyGenAI.API.Models
{
    public class TranscriptConversation
    {

        public string ID { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;

        public string HtmlContent { get; set; } = string.Empty;
    }
}
