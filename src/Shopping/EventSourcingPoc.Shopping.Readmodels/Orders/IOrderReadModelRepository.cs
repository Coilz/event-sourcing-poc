using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels.Orders
{
    public interface IOrderReadModelRepository : IReadModelRepository<OrderReadModel>
    {
        Task<bool> HasOrderAsync(Guid customerId);
        Task<IEnumerable<Guid>> GetOrdersAsync(Guid customerId);
    }
}
