using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingPoc.Readmodels.Orders
{
    public interface IOrderReadModelRepository
    {
        OrderReadModel Get(Guid id);
        bool HasOrder(Guid customerId);
        void Save(OrderReadModel model);
        void Remove(Guid id);
    }
}
