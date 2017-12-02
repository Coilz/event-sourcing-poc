using System;

namespace EventSourcingPoc.Readmodels.Store
{
    public class ShoppingCartItemReadModel
    {
        public ShoppingCartItemReadModel(Guid productId, decimal price)
        {
            ProductId = productId;
            Price = price;
        }
        public Guid ProductId { get; }
        public decimal Price { get; }
    }
}
