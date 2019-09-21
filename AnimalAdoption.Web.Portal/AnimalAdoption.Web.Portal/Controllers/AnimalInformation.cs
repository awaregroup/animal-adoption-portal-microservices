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
using Newtonsoft.Json;

namespace AnimalAdoption.Web.Portal.Controllers
{
    [Route("api/animalinformation")]
    public class AnimalInformationController : Controller
    {
        public AnimalInformationController(IOptions<EndpointSettings> endpointSettings, IHttpClientFactory httpClientFactory)
        {
            _endpointSettings = endpointSettings;
            _httpClient = httpClientFactory.CreateClient();
        }

        public IOptions<EndpointSettings> _endpointSettings { get; }

        private HttpClient _httpClient;

        [HttpGet]
        public async Task<ActionResult> GetAnimalInformationAsync()
        {
            using (var result = await _httpClient.GetAsync($"{_endpointSettings.Value.AnimalInfoApi}/api/v0/animalinfo"))
            {
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var remoteAnimals = JsonConvert.DeserializeObject<List<Animal>>(content);
                    return Json(remoteAnimals.Select(animal =>
                        new AnimalDescription
                        {
                            Id = animal.Id,
                            Name = animal.Name,
                            Description = animal.Description,
                            Age = animal.Age,
                            AnimalType = "Placeholder Animal"
                        }
                     ));
                }
                else
                {
                    return StatusCode((int)result.StatusCode);
                }
            }
        }

    }
}
