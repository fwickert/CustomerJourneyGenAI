using CustomerJourney.API.Services;
using CustomerJourney_API.Models.Request;
using CustomerJourneyGenAI.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerJourney_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerServiceController : ControllerBase
    {
        private TranscriptConversation conversation = new TranscriptConversation()
        {
            ID = "1",
            Content = "Store: Hello, you have reached Luxor, the luxury watch store. My name is Lisa, how can I help you? Customer: Hello, my name is Jane Smith. I bought a watch from you six months ago and I accidentally broke it yesterday. Store: Oh, I’m sorry to hear that. What is the model of your watch? Customer: It’s a Luxor Chrono 3000, in rose gold with a black leather strap. Store: I see. And what is broken exactly? Customer: Well, the glass is cracked and the second hand doesn’t move anymore. Store: I understand. Fortunately, your watch is still under warranty. In order to determine whether the damages are eligible for coverage, we'll need to examine the watch. You can mail it to us free of charge or drop it off at the store. Customer: Ah, that’s good news! I prefer to come to the store, it’s safer. Store: Very well. We are open from Monday to Saturday, from 10 am to 7 pm. Our address is 12 rue du Faubourg Saint-Honoré, 75008 Paris. Can you give me your phone number and email address so that I can confirm the appointment? Customer: Yes, of course. My number is 06 12 34 56 78 and my email is jane.smith@gmail.com. Store: Perfect. I’ll send you a text message and an email confirmation. When do you think you can come to the store? Customer: I think I can come tomorrow afternoon, around 3 pm. Store: Okay. I’ll book a slot for you tomorrow at 3 pm. We look forward to seeing you. Do you have any other questions? Customer: No, that’s all. Thank you very much for your help and kindness. Store: You’re welcome. Thank you for contacting us. Goodbye and see you tomorrow. Customer: Goodbye and thank you again.",
            Language = "en",
            HtmlContent = "<p><strong>Store:</strong> Hello, you have reached Luxor, the luxury watch store. My name is Lisa, how can I help you?</p>\r\n<p><strong>Customer:</strong> Hello, my name is Jane Smith. I bought a watch from you six months ago and I accidentally broke it yesterday.</p>\r\n<p><strong>Store:</strong> Oh, I’m sorry to hear that. What is the model of your watch?</p>\r\n<p><strong>Customer:</strong> It’s a Luxor Chrono 3000, in rose gold with a black leather strap.</p>\r\n<p><strong>Store:</strong> I see. And what is broken exactly?</p>\r\n<p><strong>Customer:</strong> Well, the glass is cracked and the second hand doesn’t move anymore.</p>\r\n<p><strong>Store:</strong> I understand. Fortunately, your watch is still under warranty. In order to determine whether the damages are eligible for coverage, we'll need to examine the watch. You can mail it to us free of charge or drop it off at the store.</p>\r\n<p><strong>Customer:</strong> Ah, that’s good news! I prefer to come to the store, it’s safer.</p>\r\n<p><strong>Store:</strong> Very well. We are open from Monday to Saturday, from 10 am to 7 pm. Our address is 12 rue du Faubourg Saint-Honoré, 75008 Paris. Can you give me your phone number and email address so that I can confirm the appointment?</p>\r\n<p><strong>Customer:</strong> Yes, of course. My number is 06 12 34 56 78 and my email is jane.smith@gmail.com.</p>\r\n<p><strong>Store:</strong> Perfect. I’ll send you a text message and an email confirmation. When do you think you can come to the store?</p>\r\n<p><strong>Customer:</strong> I think I can come tomorrow afternoon, around 3 pm.</p>\r\n<p><strong>Store:</strong> Okay. I’ll book a slot for you tomorrow at 3 pm. We look forward to seeing you. Do you have any other questions?</p>\r\n<p><strong>Customer:</strong> No, that’s all. Thank you very much for your help and kindness.</p>\r\n<p><strong>Store:</strong> You’re welcome. Thank you for contacting us. Goodbye and see you tomorrow.</p>\r\n<p><strong>Customer:</strong> Goodbye and thank you again.</p>"
        };


        private readonly ILogger<MarketingController> _logger;

        private readonly LLMResponse _response;

        public CustomerServiceController(ILogger<MarketingController> logger, LLMResponse response)
        {
            _logger = logger;
            _response = response;
            _response.PluginName = "CustomerServicePlugin";
        }

        //Get Summariez of a conversation passed in the body
        [HttpPost("summarize", Name = "summarize")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IResult GetSummarize([FromBody] CustomerServiceRequest request, string connectionId)
        {
            _response.FunctionName = "Summarize";

            this._logger.LogDebug("Summarize receive request.");

            if (string.IsNullOrEmpty(request.Conversation) || (string.IsNullOrEmpty(request.Language)))
            {
                return TypedResults.BadRequest("Conversation is required.");
            }

            Task.Run(() => _response.GetAsync("Summarize",
                new Dictionary<string, string>()
                {
                    { "conversation", request.Conversation },
                    { "language", request.Language }
                    
                }, connectionId));

            return TypedResults.Ok("Summarize requested");
        }

        //Get Sentiment
        [HttpPost("sentiment", Name = "sentiment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IResult GetSentiment([FromBody] CustomerServiceRequest request, string connectionId)
        {
            _response.FunctionName = "Sentiment";

            this._logger.LogDebug("Sentiment receive request.");

            if (string.IsNullOrEmpty(request.Conversation) || (string.IsNullOrEmpty(request.Language)))
            {
                return TypedResults.BadRequest("Conversation is required.");
            }

            Task.Run(() => _response.GetAsync("Sentiment",
                new Dictionary<string, string>()
                {
                    { "conversation", request.Conversation },
                    { "language", request.Language }
                }, connectionId));

            return TypedResults.Ok("Sentiment requested");
        }

        //get Translate
        [HttpPost("translate", Name = "translate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IResult GetTranslate([FromBody] CustomerServiceRequest request, string connectionId)
        {
            _response.FunctionName = "Translate";

            this._logger.LogDebug("Translate receive request.");

            if (string.IsNullOrEmpty(request.Conversation) || (string.IsNullOrEmpty(request.Language)))
            {
                return TypedResults.BadRequest("Conversation is required.");
            }

            Task.Run(() => _response.GetAsync("Translate",
                new Dictionary<string, string>()
                {
                    { "conversation", request.Conversation },
                    { "language", request.Language }
                }, connectionId));

            return TypedResults.Ok("Translate requested");
        }

        //get Anonymize
        [HttpPost("anonymize", Name = "anonymize")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IResult GetAnonymize([FromBody] CustomerServiceRequest request, string connectionId)
        {
            _response.FunctionName = "Anonymize";

            this._logger.LogDebug("Anonymize receive request.");

            if (string.IsNullOrEmpty(request.Conversation))
            {
                return TypedResults.BadRequest("Conversation is required.");
            }

            Task.Run(() => _response.GetAsync("Anonymize",
                new Dictionary<string, string>()
                {
                    { "conversation", request.Conversation }
                    
                }, connectionId));

            return TypedResults.Ok("Anonymize requested");
        }

        //get next actions
        [HttpPost("nextactions", Name = "nextactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IResult GetNextActions([FromBody] CustomerServiceRequest request, string connectionId)
        {
            _response.FunctionName = "NextActions";

            this._logger.LogDebug("NextActions receive request.");

            if (string.IsNullOrEmpty(request.Conversation) || (string.IsNullOrEmpty(request.Language)))
            {
                return TypedResults.BadRequest("Conversation is required.");
            }

            Task.Run(() => _response.GetAsync("NextActions",
                new Dictionary<string, string>()
                {
                    { "conversation", request.Conversation },
                    { "language", request.Language },
                    { "actions", request.Actions.ToString() }
                }, connectionId));

            return TypedResults.Ok("NextActions requested");
        }

        //get composeMail
        [HttpPost("composemail", Name = "composemail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IResult GetComposeMail([FromBody] CustomerServiceRequest request, string connectionId)
        {
            _response.FunctionName = "ComposeMail";

            this._logger.LogDebug("ComposeMail receive request.");

            if (string.IsNullOrEmpty(request.Conversation) || (string.IsNullOrEmpty(request.Language)))
            {
                return TypedResults.BadRequest("Conversation is required.");
            }

            Task.Run(() => _response.GetAsync("ComposeMail",
                new Dictionary<string, string>()
                {
                    { "conversation", request.Conversation },
                    { "language", request.Language }
                }, connectionId));

            return TypedResults.Ok("ComposeMail requested");
        }

        [HttpPost("transcript", Name = "transcript")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string Transcript(int ID, string connectionId)
        {
            //change for CosmosDB Content and return transcript with ID
            return conversation.HtmlContent;
        }


    }
}
