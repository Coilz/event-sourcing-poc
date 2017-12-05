using System;

namespace EventSourcingPoc.Readmodels.Shop
{
    public interface IShoppingCartReadModelRepository : IReadModelRepository<ShoppingCartReadModel>
    {
        bool HasCart(Guid customerId);
    }
}
