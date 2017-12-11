using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels.Shop
{
    public class ShoppingCartReadModelRepository : IShoppingCartReadModelRepository
    {
        private readonly IReadModelStore<ShoppingCartReadModel> _store;
        public ShoppingCartReadModelRepository(IReadModelStore<ShoppingCartReadModel> store)
        {
            _store = store;
        }
        public async Task<ShoppingCartReadModel> GetAsync(Guid id)
        {
            return await _store.GetAsync(id);
        }

        public async Task<bool> HasCartAsync(Guid customerId)
        {
            var carts = await _store.GetAllAsync();

            return carts.Any(cart => cart.CustomerId == customerId);
        }

        public async Task<IEnumerable<Guid>> GetCartsAsync(Guid customerId)
        {
            var carts = await _store.GetAllAsync();

            return carts.Select(cart => cart.Id);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _store.RemoveAsync(id);
        }

        public async Task SaveAsync(ShoppingCartReadModel cart)
        {
            await _store.SaveAsync(cart);
        }
    }
}
