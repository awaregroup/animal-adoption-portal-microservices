using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AnimalAdoption.Service.AnimalInformation.Contracts;
using AnimalAdoption.Service.AnimalInformation.Contracts.v0;
using AnimalAdoption.Web.Portal.Model;
using AnimalAdoption.Web.Portal.Plumbing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AnimalAdoption.Web.Portal.Controllers
{
    [Route("api/image")]
    public class ImageController : Controller
    {
        public ImageController(IOptions<EndpointSettings> endpointSettings, IHttpClientFactory httpClientFactory)
        {
            _endpointSettings = endpointSettings;
            _httpClient = httpClientFactory.CreateClient();
        }

        public IOptions<EndpointSettings> _endpointSettings { get; }

        private HttpClient _httpClient;

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            using (var result = await _httpClient.GetAsync($"{_endpointSettings.Value.ImageApi}/api/v0/animalimage/{id}"))
            {
                if (result.IsSuccessStatusCode)
                {
                    return File((await result.Content.ReadAsByteArrayAsync()), "image/jpeg"); ;
                }
                else
                {
                    return StatusCode((int)result.StatusCode);
                }
            }
        }

    }
}
