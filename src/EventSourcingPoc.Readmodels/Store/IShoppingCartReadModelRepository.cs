using System;

namespace EventSourcingPoc.Readmodels.Store
{
    public interface IShoppingCartReadModelRepository : IReadModelRepository<ShoppingCartReadModel>
    {
        bool HasCart(Guid customerId);
    }
}
