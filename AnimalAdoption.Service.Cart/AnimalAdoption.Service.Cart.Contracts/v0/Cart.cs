using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalAdoption.Service.Cart.Contracts.v0
{
    public class Cart
    {
        public string Id { get; set; }
        public IEnumerable<CartContent> CartContents { get; set; }
    }
}
