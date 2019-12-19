using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAdoption.Service.Image.Api.v0
{
    [Route("api/v0/animalimage")]
    [ApiController]
    public class AnimalImageController : ControllerBase
    {
        // GET api/v0/animalimage/{id}
        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            var image = System.IO.File.OpenRead($".\\Images\\{id}.jpg");
            return File(image, "image/jpeg");
        }

    }
}
