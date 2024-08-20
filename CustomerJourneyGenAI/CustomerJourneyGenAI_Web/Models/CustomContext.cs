namespace CustomerJourneyGenAI.Web.Models
{
    public class CustomContext
    {
        public string ID { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;

        public string HtmlContent { get; set; } = string.Empty;
    }
}
