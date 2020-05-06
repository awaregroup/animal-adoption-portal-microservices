using System;
using System.Collections.Generic;
using AnimalAdoption.Service.AnimalInformation.Contracts.v0;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAdoption.Service.AnimalInformation.Api.Controllers
{
    [Route("api/v0/animalinfo")]
    [ApiController]
    public class AnimalInfoController : ControllerBase
    {
        // GET api/v0/animalinfo
        [HttpGet]
        public ActionResult<IEnumerable<Animal>> Get()
        {
            return new Animal[] {
                 new Animal { Id = 1, Name = "Sedi", Age = 50, Description = "Soft natured" },
                 new Animal { Id = 2, Name = "Metamorph", Age = 50, Description = "Under a lot of pressure" },
                 new Animal { Id = 3, Name = "Igno", Age = 50, Description = "Shiny and glasslike" },
                };
        }
    }
}
