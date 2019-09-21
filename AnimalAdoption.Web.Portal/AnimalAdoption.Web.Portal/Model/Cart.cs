using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoption.Web.Portal.Model
{
    public class Cart
    {
       public IEnumerable<CartContent> CartContents { get; set; }
    }
}
