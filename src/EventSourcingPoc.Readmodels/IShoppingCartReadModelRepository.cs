using System;

namespace EventSourcingPoc.Readmodels
{
    public interface IShoppingCartReadModelRepository
    {
        ShoppingCartReadModel GetCartById(Guid id);
        bool HasCart(Guid customerId);
        void SaveCart(ShoppingCartReadModel cart);
        void RemoveCart(Guid cartId);
    }
}
