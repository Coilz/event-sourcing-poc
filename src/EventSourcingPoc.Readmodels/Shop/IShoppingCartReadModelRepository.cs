using System;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels.Shop
{
    public interface IShoppingCartReadModelRepository : IReadModelRepository<ShoppingCartReadModel>
    {
        Task<bool> HasCartAsync(Guid customerId);
    }
}
