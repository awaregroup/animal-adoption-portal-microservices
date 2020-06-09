using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AnimalAdoption.Service.Cart.Api
{
    public class CartLogic
    {
        private IMemoryCache _cache;
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);

        public CartLogic(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public Contracts.v0.Cart AddAnimal(string cartId, int animalId)
        {
            semaphoreSlim.Wait(TimeSpan.FromSeconds(1).Milliseconds);
            try
            {
                var domainCart = _cache.Get<Domain.Cart>(cartId);

                if(domainCart == null)
                {
                    domainCart = new Domain.Cart();
                };

                if(domainCart.CartContents.TryGetValue(animalId, out int quantity))
                {
                    domainCart.CartContents[animalId] = quantity + 1;
                }
                else
                {
                    domainCart.CartContents.Add(animalId, 1);
                }

                _cache.Set(cartId, domainCart );
                return ListAnimals(cartId);
            }
            finally
            {
                semaphoreSlim.Release();
            }

        }

        public Contracts.v0.Cart RemoveAnimal(string cartId, int animalId)
        {
            semaphoreSlim.Wait(TimeSpan.FromSeconds(1).Milliseconds);
            try
            {
                var domainCart = _cache.Get<Domain.Cart>(cartId);

                if (domainCart == null)
                {
                    return new Contracts.v0.Cart();
                };

                if (domainCart.CartContents.TryGetValue(animalId, out int quantity))
                {
                    if (quantity > 1)
                    {
                        domainCart.CartContents[animalId] = quantity - 1;
                    }
                    else
                    {
                        domainCart.CartContents.Remove(animalId);
                    }
                }

                _cache.Set(cartId, domainCart);
                return ListAnimals(cartId);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public Contracts.v0.Cart ListAnimals(string cartId)
        {
            var domainCart = _cache.Get<Domain.Cart>(cartId);

            return new Contracts.v0.Cart
            {
                Id = cartId,
                CartContents = domainCart?.CartContents?.Select(x => new Contracts.v0.CartContent
                {
                    Id = x.Key,
                    Quantity = x.Value
                })
            };
        }
    }
}
