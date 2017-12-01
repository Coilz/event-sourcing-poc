using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingPoc.Domain.Orders
{
    using EventSourcing.Domain;
    using Messages;
    using Messages.Orders;

    public class Order : Aggregate
    {
        private bool _paidFor;
        private bool _shippingAddressProvided;
        private bool _completed;

        public static Order Create(Guid orderId, Guid customerId, IEnumerable<OrderItem> items)
        {
            return new Order(orderId, customerId, items);
        }

        public Order() { }
        private Order(Guid orderId, Guid customerId, IEnumerable<OrderItem> items)
        {
            ApplyChanges(new OrderCreated(orderId, customerId, items.ToArray()));
        }

        private void Apply(OrderCreated evt)
        {
            id = evt.OrderId;
        }

        public void ProvideShippingAddress(Address address)
        {
            if (_shippingAddressProvided || _completed) return;

            ApplyChanges(new ShippingAddressConfirmed(id, address));
        }

        public void Pay()
        {
            if (_paidFor || _completed) return;

            ApplyChanges(new PaymentReceived(id));
        }

        public void CompleteOrder()
        {
            if (!_paidFor || !_shippingAddressProvided) throw new CannotCompleteOrderException();

            ApplyChanges(new OrderCompleted(id));
        }

        protected override IEnumerable<KeyValuePair<Type, Action<IEvent>>> EventAppliers
        {
            get
            {
                yield return CreateApplier<OrderCreated>(Apply);
                yield return CreateApplier<PaymentReceived>(Apply);
                yield return CreateApplier<ShippingAddressConfirmed>(Apply);
                yield return CreateApplier<OrderCompleted>(Apply);
            }
        }

        private void Apply(ShippingAddressConfirmed evt)
        {
            _shippingAddressProvided = true;
        }

        private void Apply(PaymentReceived evt)
        {
            _paidFor = true;
        }

        private void Apply(OrderCompleted evt)
        {
            _completed = true;
        }
    }
}
