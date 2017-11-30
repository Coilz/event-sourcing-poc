using System;

namespace EventSourcingPoc.Messages.Orders
{
    public class ConfirmShippingAddress : ICommand
    {
        public Guid OrderId { get; }
        public Address Address { get; }
        public ConfirmShippingAddress(Guid orderId, Address address)
        {
            this.Address = address;
            this.OrderId = orderId;

        }
    }
}
