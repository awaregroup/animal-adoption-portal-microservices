using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAdoption.Service.Cart.Api.Controllers
{
    [Route("api/v0/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private CartLogic _cartLogic;

        public CartController(CartLogic cartLogic)
        {
            _cartLogic = cartLogic;
        }


        // GET api/v0/cart/{cartId}
        [HttpGet]
        [Route("{cartId}")]
        public ActionResult<Contracts.v0.Cart> Get(string cartId)
        {
            return _cartLogic.ListAnimals(cartId);
        }

        [HttpPost]
        [Route("{cartId}")]
        public ActionResult<Contracts.v0.Cart> Post(string cartId, [FromBody]int animalId)
        {
            return _cartLogic.AddAnimal(cartId, animalId);
        }

        [HttpDelete]
        [Route("{cartId}/{animalId}")]
        public ActionResult<Contracts.v0.Cart> Delete(string cartId, int animalId)
        {
            return _cartLogic.RemoveAnimal(cartId, animalId);
        }
    }
}
