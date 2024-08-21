using CustomerJourney.API.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.SemanticKernel;
using CustomerJourney.API.Utilities;
using CustomerJourney.API.Models.Response;


namespace CustomerJourney.API.Services
{
    public class LLMResponse
    {

        private readonly ILogger<LLMResponse> _logger;
        private readonly IHubContext<MessageRelayHub> _messageRelayHubContext;
        private readonly Kernel _kernel;
        private string _pluginsDirectory = string.Empty;
        private readonly int DELAY = 25;

        public string PluginName { get; set; } = string.Empty;
        public string FunctionName { get; set; } = string.Empty;


        public LLMResponse(ILogger<LLMResponse> logger, [FromServices] Kernel kernel,
        [FromServices] IHubContext<MessageRelayHub> messageRelayHubContext)
        {
            _logger = logger;
            _kernel = kernel;
            _messageRelayHubContext = messageRelayHubContext;
            _pluginsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
        }

        // Method to handle the GetAsync request
        public async Task GetAsync(string whatAbout, Dictionary<string, string> variablesContext, string connectionId)
        {
            var arguments = new KernelArguments();

            foreach (var item in variablesContext)
            {
                arguments[item.Key] = item.Value;
            }

            await StreamResponseToClient("", whatAbout, arguments, connectionId);

        }

        // Method to read the prompt from a function in a plugin
        public async Task<string> ReadPrompt(string functionName, string plugin)
        {
            string? promptStream = await ReadFunction.Read(Path.Combine(_pluginsDirectory, plugin, functionName),
                FunctionFileType.Prompt);
            return promptStream!;
        }

        // Method to stream the response to the client
        private async Task<MessageResponse> StreamResponseToClient(string chatId, string whatAbout, KernelArguments arguments, string connectionId)
        {

            MessageResponse messageResponse = new MessageResponse
            {
                State = "Start",
                Content = "",
                WhatAbout = whatAbout
            };

            await foreach (StreamingChatMessageContent contentPiece in _kernel.InvokeStreamingAsync<StreamingChatMessageContent>(_kernel.Plugins[this.PluginName][this.FunctionName], arguments))
            {
                await this.UpdateMessageOnClient(messageResponse, connectionId);
                messageResponse.State = "InProgress";

                if (!string.IsNullOrEmpty(contentPiece.Content))
                {
                    messageResponse.Content += contentPiece.Content;
                    await this.UpdateMessageOnClient(messageResponse, connectionId);
                    Console.Write(contentPiece.Content);
                    await Task.Delay(DELAY);
                }
            }
           
            messageResponse.State = "End";
            await this.UpdateMessageOnClient(messageResponse, connectionId);
            return messageResponse;
        }

        /// <summary>
        /// Update the response on the client.
        /// </summary>
        /// <param name="message">The message</param>
        private async Task UpdateMessageOnClient(MessageResponse message, string connectionId)
        {
            await this._messageRelayHubContext.Clients.Client(connectionId).SendAsync(message.WhatAbout, message.Content);
        }



    }
}
