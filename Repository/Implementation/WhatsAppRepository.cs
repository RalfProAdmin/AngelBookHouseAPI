using EkartAPI.Models;
using EkartAPI.Repository.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime;
using System.Text;

namespace EkartAPI.Repository.Implementation
{
    public class WhatsAppRepository : IWhatsAppRepository
    {
        private readonly WhatsAppSettings _settings;
        private readonly HttpClient _httpClient;

        public WhatsAppRepository(
         IOptions<WhatsAppSettings> options,
         HttpClient httpClient)
        {
            _settings = options.Value;
            _httpClient = httpClient;
        }
        public async Task SendOrderConfirmationAsync(
            string mobileNumber,
            string customerName,
            string orderId,
            string orderDate)
        {
            var payload = new
            {
                messaging_product = "whatsapp",
                to = mobileNumber,
                type = "template",
                template = new
                {
                    name = "jaspers_market_order_confirmation_v1",
                    language = new { code = "en_US" },
                    components = new[]
                    {
                    new
                    {
                        type = "body",
                        parameters = new[]
                        {
                            new { type = "text", text = customerName },
                            new { type = "text", text = orderId },
                            new { type = "text", text = orderDate }
                        }
                    }
                }
                }
            };

            var url =
                $"{_settings.BaseUrl}/{_settings.ApiVersion}/{_settings.PhoneNumberId}/messages";

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", _settings.AccessToken);

            request.Content = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.SendAsync(request);

            // Optional: log failure but do NOT break order flow
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                // log error
            }
        }
    }
}
