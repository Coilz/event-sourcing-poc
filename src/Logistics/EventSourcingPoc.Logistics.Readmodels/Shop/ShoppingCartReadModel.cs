using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingPoc.Readmodels.Shop
{
    public class LogisticsCartReadModel : IReadModel
    {
        public LogisticsCartReadModel(LogisticsCartReadModel cart)
            : this(cart.Id, cart.CustomerId)
        {
        }

        public LogisticsCartReadModel(LogisticsCartReadModel cart, IEnumerable<LogisticsCartItemReadModel> items)
            : this(cart.Id, cart.CustomerId, items)
        {
        }

        public LogisticsCartReadModel(Guid id, Guid customerId)
            : this(id, customerId, Enumerable.Empty<LogisticsCartItemReadModel>())
        {
        }

        public LogisticsCartReadModel(Guid id, Guid customerId, IEnumerable<LogisticsCartItemReadModel> items)
        {
            Id = id;
            CustomerId = customerId;
            Items = items;
        }

        public IEnumerable<LogisticsCartItemReadModel> Items { get; }
        public Guid Id { get; }
        public Guid CustomerId { get; }
    }
}
