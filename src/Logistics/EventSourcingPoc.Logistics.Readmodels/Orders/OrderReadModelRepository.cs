using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels.Orders
{
    public class OrderReadModelRepository : IOrderReadModelRepository
    {
        private readonly IReadModelStore<OrderReadModel> _store;
        public OrderReadModelRepository(IReadModelStore<OrderReadModel> store)
        {
            _store = store;
        }
        public async Task<OrderReadModel> GetAsync(Guid id)
        {
            return await _store.GetAsync(id);
        }

        public async Task<bool> HasOrderAsync(Guid customerId)
        {
            var models = await _store.GetAllAsync();

            return Enumerable.Any(models, model => model.CustomerId == customerId);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _store.RemoveAsync(id);
        }

        public async Task SaveAsync(OrderReadModel model)
        {
            await _store.SaveAsync(model);
        }
    }
}
