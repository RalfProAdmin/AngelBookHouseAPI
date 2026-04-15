using EkartAPI.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EkartAPI.Repository.Implementation
{
    public class SmsRepository : ISmsRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl = "https://hisocial.in/api/send";
        private readonly string _instanceId = "68C3E0D5AC35D";   // put your real instance id
        private readonly string _accessToken = "68c3cc9e08e35"; // put your real token

        public SmsRepository(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        private async Task SendMessageInternalAsync(string phoneNumber, string message)
        {
            var payload = new
            {
                number = phoneNumber,
                type = "text",
                message = message,
                instance_id = _instanceId,
                access_token = _accessToken
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);

            response.EnsureSuccessStatusCode();
        }

        public async Task<string> SendMessageAsync(string mobileNumber, string message)
        {
            var apiKey = _configuration["TwoFactor:ApiKey"];

            // 2Factor free-style SMS API (for testing only; not using template)
            var url = $"https://2factor.in/API/V1/{apiKey}/SMS/{mobileNumber}/{Uri.EscapeDataString(message)}";

            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task SendOrderPlacedwhatsappAsync(string customerPhone, string message)
        { 
            await SendMessageInternalAsync("91" + customerPhone, message);
        }
    }
}
