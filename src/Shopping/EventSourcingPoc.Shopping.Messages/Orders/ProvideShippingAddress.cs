using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
{
    public class ProvideShippingAddress : ICommand
    {
        public Guid OrderId { get; }
        public Address Address { get; }
        public ProvideShippingAddress(Guid orderId, Address address)
        {
            this.OrderId = orderId;
            this.Address = address;
        }
    }
}
