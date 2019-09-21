using AnimalAdoption.Service.Cart.Api;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using Xunit;

namespace AnimalAdoption.Service.Cart.UnitTests
{
    public class CartTests
    {
        [Fact]
        public void CartManagement_EmptyCartAddAnimal_AnAnimalIsAdded()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var resultingCart = new CartLogic(memoryCache).AddAnimal("TEST_CART", 1);

            var animal = resultingCart.CartContents.First();

            Assert.Equal("TEST_CART", resultingCart.Id);
            Assert.Equal(1, resultingCart.CartContents.First().Quantity);
            Assert.Equal(1, resultingCart.CartContents.First().Id);


        }
    }
}
