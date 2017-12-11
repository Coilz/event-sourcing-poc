using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels.Shop
{
    public class LogisticsCartReadModelRepository : ILogisticsCartReadModelRepository
    {
        private readonly IReadModelStore<LogisticsCartReadModel> _store;
        public LogisticsCartReadModelRepository(IReadModelStore<LogisticsCartReadModel> store)
        {
            _store = store;
        }
        public async Task<LogisticsCartReadModel> GetAsync(Guid id)
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

        public async Task SaveAsync(LogisticsCartReadModel cart)
        {
            await _store.SaveAsync(cart);
        }
    }
}
