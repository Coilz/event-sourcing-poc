using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels.Shop
{
    public interface IShoppingCartReadModelRepository : IReadModelRepository<ShoppingCartReadModel>
    {
        Task<bool> HasCartAsync(Guid customerId);
        Task<IEnumerable<Guid>> GetCartsAsync(Guid customerId);
    }
}
