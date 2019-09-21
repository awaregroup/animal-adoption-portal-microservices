using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AnimalAdoption.Service.Cart.Domain
{
    public class Cart
    {
        public Dictionary<int, int> CartContents { get; set; } = new Dictionary<int, int>();
    }
}
