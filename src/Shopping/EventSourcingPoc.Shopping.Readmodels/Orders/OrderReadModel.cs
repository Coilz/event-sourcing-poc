using System;
using System.Collections.Generic;

namespace EventSourcingPoc.Readmodels.Orders
{
    public class OrderReadModel : IReadModel
    {
        public enum OrderStatus
        {
            None = 0,
            Created,
            Complete,
            Shipped,
            Delivered,
            Completed
        }

        public OrderReadModel(Guid id, Guid customerId, IEnumerable<OrderItemReadModel> items)
        {
            Id = id;
            CustomerId = customerId;
            Items = items;
        }

        public Guid Id { get; }
        public Guid CustomerId { get; }
        public IEnumerable<OrderItemReadModel> Items { get; }
        public bool Payed { get; private set; }
        public bool ShippingAddressConfirmed { get; private set; }
        public bool Shipped { get; private set; }
        public bool Delivered { get; private set; }
        public bool Completed { get; private set; }

        public OrderStatus Status => Completed
            ? OrderStatus.Completed
            : Delivered
                ? OrderStatus.Delivered
                : Shipped
                    ? OrderStatus.Shipped
                    : Payed && ShippingAddressConfirmed
                        ? OrderStatus.Complete
                        : OrderStatus.Created;

        public void Pay()
        {
            Payed = true;
        }

        public void ConfirmShippingAddress()
        {
            ShippingAddressConfirmed = true;
        }

        public void Ship()
        {
            Shipped = true;
        }

        public void Deliver()
        {
            Delivered = true;
        }

        public void Complete()
        {
            Completed = true;
        }
    }
}
