using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
{
    public class ConfirmShippingAddress : ICommand
    {
        public Guid OrderId { get; }
        public Address Address { get; }
        public ConfirmShippingAddress(Guid orderId, Address address)
        {
            this.OrderId = orderId;
            this.Address = address;
        }
    }
}
