using System;

namespace AnimalAdoption.Service.AnimalInformation.Contracts.v0
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
    }
}
