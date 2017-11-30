using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages.Orders;
using EventSourcingPoc.Messages.Shipping;

namespace EventSourcingPoc.Domain.Orders
{
    public class Order : Aggregate
    {
        protected override void RegisterAppliers()
        {
            this.RegisterApplier<OrderCreated>(this.Apply);
            this.RegisterApplier<PaymentReceived>(this.Apply);
            this.RegisterApplier<ShippingAddressConfirmed>(this.Apply);
            this.RegisterApplier<OrderCompleted>(this.Apply);
        }

        private bool paidFor;
        private bool shippingAddressProvided;
        private bool completed;

        public static Order Create(Guid orderId, Guid customerId, IEnumerable<OrderItem> items)
        {
            return new Order(orderId, customerId, items);
        }

        private Order(Guid orderId, Guid customerId, IEnumerable<OrderItem> items)
        {
            this.ApplyChanges(new OrderCreated(orderId, customerId, items.ToArray()));
        }

        public Order()
        {
        }

        private void Apply(OrderCreated evt)
        {
            this.id = evt.OrderId;
        }

        public void ProvideShippingAddress(Address address)
        {
            if(!this.shippingAddressProvided && !this.completed)
            {
                this.ApplyChanges(new ShippingAddressConfirmed(this.id, address));
            }
        }

        private void Apply(ShippingAddressConfirmed evt)
        {
            this.shippingAddressProvided = true;
        }

        public void Pay()
        {
            if (!this.paidFor && !this.completed)
            {
                this.ApplyChanges(new PaymentReceived(this.id));
            }
        }

        public void Apply(PaymentReceived evt)
        {
            this.paidFor = true;
        }

        public void CompleteOrder()
        {
            if(!this.paidFor || !this.shippingAddressProvided)
            {
                throw new CannotCompleteOrderException();
            }
            this.ApplyChanges(new OrderCompleted(this.id));
        }

        private void Apply(OrderCompleted evt)
        {
            this.completed = true;
        }
    }
}