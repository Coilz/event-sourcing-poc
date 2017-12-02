using System;
using System.Linq;

namespace EventSourcingPoc.Readmodels.Orders
{
    public class OrderReadModelRepository : IOrderReadModelRepository
    {
        private readonly IReadModelStore<OrderReadModel> _store;
        public OrderReadModelRepository(IReadModelStore<OrderReadModel> store)
        {
            _store = store;
        }
        public OrderReadModel Get(Guid id)
        {
            return _store.Get(id);
        }

        public bool HasOrder(Guid customerId)
        {
            var models = _store.GetAll();

            return Enumerable.Any(models, model => model.CustomerId == customerId);
        }

        public void Remove(Guid id)
        {
            _store.Remove(id);
        }

        public void Save(OrderReadModel model)
        {
            _store.Save(model);
        }
    }
}
