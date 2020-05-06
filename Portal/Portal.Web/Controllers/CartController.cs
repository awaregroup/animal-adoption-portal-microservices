using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AnimalAdoption.Web.Portal.Model;
using AnimalAdoption.Web.Portal.Plumbing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AnimalAdoption.Web.Portal.Controllers
{
    [Route("api/cart")]
    public class CartController : Controller
    {
        public CartController(IOptions<EndpointSettings> endpointSettings, IHttpClientFactory httpClientFactory)
        {
            _endpointSettings = endpointSettings;
            _httpClient = httpClientFactory.CreateClient();
        }

        public IOptions<EndpointSettings> _endpointSettings { get; }

        private HttpClient _httpClient;

        [HttpGet]
        [Route("{cartId}")]
        public async Task<ActionResult> GetAsync(string cartId)
        {
            using (var result = await _httpClient.GetAsync($"{_endpointSettings.Value.CartApi}/api/v0/cart/{cartId}"))
            {
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var cartContents = JsonConvert.DeserializeObject<Service.Cart.Contracts.v0.Cart>(content);

                    return Json(new Cart
                    {
                        CartContents = cartContents.CartContents?.Select(x => new CartContent
                        {
                            Id = x.Id,
                            Quantity = x.Quantity
                        })
                    });

                }
                else
                {
                    return StatusCode((int)result.StatusCode);
                }
            }
        }

        [HttpPost]
        [Route("{cartId}/{animalId}")]
        public async Task<ActionResult> PostAsync(string cartId, int animalId)
        {
            using (var result = await _httpClient.PostAsync($"{_endpointSettings.Value.CartApi}/api/v0/cart/{cartId}/{animalId}", null))
            {
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var cartContents = JsonConvert.DeserializeObject<Service.Cart.Contracts.v0.Cart>(content);

                    return Json(new Cart
                    {
                        CartContents = cartContents.CartContents?.Select(x => new CartContent
                        {
                            Id = x.Id,
                            Quantity = x.Quantity
                        })
                    });
                }
                else
                {
                    return StatusCode((int)result.StatusCode);
                }
            }
        }

        [HttpDelete]
        [Route("{cartId}/{animalId}")]
        public async Task<ActionResult> DeleteAsync(string cartId, int animalId)
        {
            using (var result = await _httpClient.DeleteAsync($"{_endpointSettings.Value.CartApi}/api/v0/cart/{cartId}/{animalId}"))
            {
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var cartContents = JsonConvert.DeserializeObject<Service.Cart.Contracts.v0.Cart>(content);

                    return Json(new Cart
                    {
                        CartContents = cartContents.CartContents?.Select(x => new CartContent
                        {
                            Id = x.Id,
                            Quantity = x.Quantity
                        })
                    });
                }
                else
                {
                    return StatusCode((int)result.StatusCode);
                }
            }
        }

    }
}
