using System;
using System.Collections.Generic;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Shopping.Messages.Orders;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class ShippingProcessStarted : Event
    {
        public ShippingProcessStarted(Guid id, Guid cartId)
            : base(id)
        {
            CartId = cartId;
        }

        public Guid CartId { get; }
    }
}
