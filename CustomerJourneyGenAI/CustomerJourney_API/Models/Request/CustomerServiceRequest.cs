namespace CustomerJourney_API.Models.Request
{
    public class CustomerServiceRequest
    {
        public string? Conversation { get; set; }
        public string? Language { get; set; }

        public int Actions { get; set; }

    }
}
