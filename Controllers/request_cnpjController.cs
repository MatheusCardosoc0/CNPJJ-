using Api.Models.ModelsForExternalApis;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers.Assets
{
    [Route("api/[controller]")]
    [ApiController]
    public class request_cnpjController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public request_cnpjController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FetchData(string id)
        {
            string cnpj = id;
            string apiUrl = $"https://api.cnpja.com/office/{cnpj}";

            string authToken = _configuration["MY_TOKEN_AUTHORIZATION"];

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", authToken);
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    Company.EmpresaInfo companyInfo = JsonConvert.DeserializeObject<Company.EmpresaInfo>(responseBody);

                    var result = new
                    {
                        nome = companyInfo.alias,
                        razao = companyInfo.company.name,
                        ibge = companyInfo.address.municipality,
                        cep = companyInfo.address.zip,
                        endereco = companyInfo.address.details,
                        bairro = companyInfo.address.district,
                        ddd = companyInfo.phones[0].area,
                        telcom = companyInfo.phones[0].number,
                        email = companyInfo.emails[0].address
                    };

                    return Ok(result);
                }
                catch (HttpRequestException error)
                {
                    Console.WriteLine($"{error.Message}");
                    return BadRequest($"Erro: {error.Message}");
                }
            }
        }
    }
}