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
    [Route("api/login")]
    public class LoginController : Controller
    {
        public LoginController(IOptions<EndpointSettings> endpointSettings, IHttpClientFactory httpClientFactory)
        {
            _endpointSettings = endpointSettings;
            _httpClient = httpClientFactory.CreateClient();
        }

        public IOptions<EndpointSettings> _endpointSettings { get; }

        private HttpClient _httpClient;

        [HttpGet]
        public ActionResult Get([FromQuery] string redirectUrl)
        {
           return Redirect($"{_endpointSettings.Value.IdentityApi}/authenticate?redirectUrl={redirectUrl}");
        }

    }
}
