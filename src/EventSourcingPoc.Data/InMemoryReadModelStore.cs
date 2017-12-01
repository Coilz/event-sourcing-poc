using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingPoc.Readmodels;

namespace EventSourcingPoc.Data
{
    public class InMemoryReadModelStore : IShoppingCartReadModelRepository
    {
        private readonly Dictionary<Guid, ShoppingCartReadModel> _carts = new Dictionary<Guid, ShoppingCartReadModel>();

        public ShoppingCartReadModel GetCartById(Guid id)
        {
            return _carts[id];
        }

        public bool HasCart(Guid customerId)
        {
            return _carts.Any(cart => cart.Value.CustomerId == customerId);
        }

        public void SaveCart(ShoppingCartReadModel cart)
        {
            if (_carts.ContainsKey(cart.Id))
                _carts[cart.Id] = cart;
            else
                _carts.Add(cart.Id, cart);
        }

        public void RemoveCart(Guid cartId)
        {
            if (_carts.ContainsKey(cartId))
            {
                _carts.Remove(cartId);
            }
        }
    }
}