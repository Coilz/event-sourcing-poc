using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels.Shipping
{
    public class ShipmentReadModelRepository : IShipmentReadModelRepository
    {
        private readonly IReadModelStore<ShipmentReadModel> _store;
        public ShipmentReadModelRepository(IReadModelStore<ShipmentReadModel> store)
        {
            _store = store;
        }
        public async Task<ShipmentReadModel> GetAsync(Guid id)
        {
            return await _store.GetAsync(id);
        }

        public async Task<IEnumerable<Guid>> GetShipmentsAsync(Guid customerId)
        {
            var models = await _store.GetAllAsync();
            return models.Select(model => model.Id);
        }

        public async Task<bool> HasShipmentAsync(Guid customerId)
        {
            var models = await _store.GetAllAsync();

            return Enumerable.Any(models, model => model.CustomerId == customerId);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _store.RemoveAsync(id);
        }

        public async Task SaveAsync(ShipmentReadModel model)
        {
            await _store.SaveAsync(model);
        }
    }
}
