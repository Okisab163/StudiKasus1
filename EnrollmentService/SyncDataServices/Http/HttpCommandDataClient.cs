using EnrollmentService.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnrollmentService.SyncDataServices.Http
{
    public class HttpCommandDataClient : IEnrollmentDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendEnrollmentToPaymentService(EnrollmentDto enrollmentDto)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(enrollmentDto),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_configuration["PaymentService"],
                httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to PaymentService was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync POST to PaymentService failed");
            }
        }
    }
}
