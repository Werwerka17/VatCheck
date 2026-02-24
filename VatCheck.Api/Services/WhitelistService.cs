using System.Net;
using System.Net.Http.Json;
using VatCheck.Api.Models;

namespace VatCheck.Api.Services
{

    public class WhitelistService
    {
        private readonly HttpClient _httpClient;

        public WhitelistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CompanyDto?> GetByNipAsync(string nip, CancellationToken ct = default)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");

            var url = $"api/search/nip/{nip}?date={date}";

            using var response = await _httpClient.GetAsync(url, ct);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new HttpRequestException("Nieprawidłowy NIP.", null, HttpStatusCode.BadRequest);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Błąd po stronie MF.", null, HttpStatusCode.BadGateway);
            }

            var whitelistResponse = await response.Content.ReadFromJsonAsync<WhitelistResponse>(cancellationToken: ct);

            var subject = whitelistResponse?.Result?.Subject;

            if (subject is null)
                return null;

            return new CompanyDto
            {
                Name = subject.Name,
                Nip = subject.Nip ?? nip,
                IsActive = string.Equals(subject.StatusVat, "Czynny", StringComparison.OrdinalIgnoreCase)
            };
        }
    }
}
