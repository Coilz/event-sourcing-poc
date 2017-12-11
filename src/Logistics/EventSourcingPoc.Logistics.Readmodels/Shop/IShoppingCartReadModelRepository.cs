using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels.Shop
{
    public interface ILogisticsCartReadModelRepository : IReadModelRepository<LogisticsCartReadModel>
    {
        Task<bool> HasCartAsync(Guid customerId);
        Task<IEnumerable<Guid>> GetCartsAsync(Guid customerId);
    }
}
