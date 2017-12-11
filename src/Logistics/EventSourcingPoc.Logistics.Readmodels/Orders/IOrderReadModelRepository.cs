using System;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels.Orders
{
    public interface IOrderReadModelRepository : IReadModelRepository<OrderReadModel>
    {
        Task<bool> HasOrderAsync(Guid customerId);
    }
}
