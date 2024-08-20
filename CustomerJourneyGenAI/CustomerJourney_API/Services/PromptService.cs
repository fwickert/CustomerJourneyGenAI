using CustomerJourney.API.Utilities;

namespace CustomerJourney_API.Services
{
    public class PromptService
    {
        public async Task<string> GetPrompt(string functionName, string plugin)
        {
            string? promptStream = await ReadFunction.Read(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins", plugin, functionName),
                FunctionFileType.Prompt);
            return promptStream!;
        }
    }
}
