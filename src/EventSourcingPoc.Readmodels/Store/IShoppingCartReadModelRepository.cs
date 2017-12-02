using System;

namespace EventSourcingPoc.Readmodels.Store
{
    public interface IShoppingCartReadModelRepository
    {
        ShoppingCartReadModel Get(Guid id);
        bool HasCart(Guid customerId);
        void Save(ShoppingCartReadModel cart);
        void Remove(Guid cartId);
    }
}
