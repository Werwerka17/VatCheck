using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using VatCheck.Api.Models;
using VatCheck.Api.Services;

namespace VatCheck.Api.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly WhitelistService _whitelistService;

        public SearchController(WhitelistService whitelistService) 
        {
            _whitelistService = whitelistService;
        }

        [HttpGet("nip/{nip}")]
        public async Task<ActionResult<CompanyDto>> GetByNip(string nip, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(nip))
            {
                return BadRequest("NIP jest wymagany!");
            }
            else if (nip.Length != 10 || !nip.All(char.IsDigit))
            {
                return BadRequest("NIP musi mieć 10 cyfr!");
            }

            try
            {
                var company = await _whitelistService.GetByNipAsync(nip, ct);

                if (company is null)
                    return NotFound("Nie znaleziono podmiotu dla podanego NIP.");

                return Ok(company);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest("Nieprawidłowy NIP.");
            }
            catch (HttpRequestException)
            {
                return StatusCode(502, "Usługa chwilowo nie odpowiada. Spróbuj ponownie za chwilę.");
            }


        }
    }
}
