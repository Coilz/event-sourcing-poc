using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingPoc.Readmodels
{
    public class ShoppingCartReadModel
    {
        public ShoppingCartReadModel(ShoppingCartReadModel cart)
            : this(cart.Id, cart.CustomerId)
        {
        }

        public ShoppingCartReadModel(ShoppingCartReadModel cart, IEnumerable<ShoppingCartItemReadModel> items)
            : this(cart.Id, cart.CustomerId, items)
        {
        }

        public ShoppingCartReadModel(Guid id, Guid customerId)
            : this(id, customerId, Enumerable.Empty<ShoppingCartItemReadModel>())
        {
        }

        public ShoppingCartReadModel(Guid id, Guid customerId, IEnumerable<ShoppingCartItemReadModel> items)
        {
            Id = id;
            CustomerId = customerId;
            Items = items;
        }

        public IEnumerable<ShoppingCartItemReadModel> Items { get; }
        public Guid Id { get; }
        public Guid CustomerId { get; }
    }
}
