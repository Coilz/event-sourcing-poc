using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingPoc.Readmodels.Orders
{
    public interface IOrderReadModelRepository : IReadModelRepository<OrderReadModel>
    {
        bool HasOrder(Guid customerId);
    }
}
