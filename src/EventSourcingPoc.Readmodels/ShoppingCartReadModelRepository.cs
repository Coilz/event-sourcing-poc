using System;
using System.Linq;

namespace EventSourcingPoc.Readmodels
{
    public class ShoppingCartReadModelRepository : IShoppingCartReadModelRepository
    {
        private readonly IReadModelStore<ShoppingCartReadModel> _store;
        public ShoppingCartReadModelRepository(IReadModelStore<ShoppingCartReadModel> store)
        {
            _store = store;
        }
        public ShoppingCartReadModel GetCartById(Guid id)
        {
            return _store.Get(id);
        }

        public bool HasCart(Guid customerId)
        {
            var carts = _store.GetAll();

            return carts.Any(cart => cart.CustomerId == customerId);
        }

        public void RemoveCart(Guid id)
        {
            _store.Remove(id);
        }

        public void SaveCart(ShoppingCartReadModel cart)
        {
            _store.Save(cart);
        }
    }
}
