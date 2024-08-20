using Microsoft.Extensions.Options;
using CustomerJourneyGenAI.Web.Options;

namespace CustomerJourneyGenAI.Web.Services
{
    public class CallAPI
    {
        private readonly HttpClient _httpClient;

        public CallAPI(HttpClient httpClient, IOptions<APIOptions> option)
        {
            _httpClient = httpClient;            
            _httpClient.BaseAddress = new Uri(option.Value.Base);
        }

        public async Task<string> GetAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }


        }

    }
}
