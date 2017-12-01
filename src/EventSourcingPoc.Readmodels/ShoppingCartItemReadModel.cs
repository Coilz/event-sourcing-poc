using System;

namespace EventSourcingPoc.Readmodels
{
    public class ShoppingCartItemReadModel
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
    }
}