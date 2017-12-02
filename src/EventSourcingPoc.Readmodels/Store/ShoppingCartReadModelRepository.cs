using System;
using System.Linq;

namespace EventSourcingPoc.Readmodels.Store
{
    public class ShoppingCartReadModelRepository : IShoppingCartReadModelRepository
    {
        private readonly IReadModelStore<ShoppingCartReadModel> _store;
        public ShoppingCartReadModelRepository(IReadModelStore<ShoppingCartReadModel> store)
        {
            _store = store;
        }
        public ShoppingCartReadModel Get(Guid id)
        {
            return _store.Get(id);
        }

        public bool HasCart(Guid customerId)
        {
            var carts = _store.GetAll();

            return Enumerable.Any(carts, cart => cart.CustomerId == customerId);
        }

        public void Remove(Guid id)
        {
            _store.Remove(id);
        }

        public void Save(ShoppingCartReadModel cart)
        {
            _store.Save(cart);
        }
    }
}
