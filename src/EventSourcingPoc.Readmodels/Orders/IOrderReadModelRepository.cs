using System;

namespace EventSourcingPoc.Readmodels.Orders
{
    public interface IOrderReadModelRepository : IReadModelRepository<OrderReadModel>
    {
        bool HasOrder(Guid customerId);
    }
}
