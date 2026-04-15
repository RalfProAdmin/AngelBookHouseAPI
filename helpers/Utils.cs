using System.Security.Cryptography;
using System.Text;


namespace EkartAPI.helpers
{
    public static class Utils
    {
        public static bool VerifyWebhookSignature(string payload, string actualSignature, string secret)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var payloadBytes = Encoding.UTF8.GetBytes(payload);

            using var hmac = new HMACSHA256(secretBytes);
            var computedHash = hmac.ComputeHash(payloadBytes);
            var expectedSignature = BitConverter.ToString(computedHash).Replace("-", "").ToLower();

            return expectedSignature == actualSignature;
        }
    }
}
