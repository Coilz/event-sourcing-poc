using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels.Shipping
{
    public interface IShipmentReadModelRepository : IReadModelRepository<ShipmentReadModel>
    {
        Task<bool> HasShipmentAsync(Guid customerId);
        Task<IEnumerable<Guid>> GetShipmentsAsync(Guid customerId);
    }
}
