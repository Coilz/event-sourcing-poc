using System;
using System.Collections.Generic;

namespace EventSourcingPoc.Readmodels
{
    public class ShoppingCartReadModel
    {
        public ShoppingCartReadModel()
        {
            Items = new List<ShoppingCartItemReadModel>();
        }

        public List<ShoppingCartItemReadModel> Items { get; }
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
    }
}