﻿using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Shopping.Messages.Orders;

namespace EventSourcingPoc.Shopping.Domain.Orders
{
    public class Order : AggregateRoot
    {
        private bool _paidFor;
        private bool _shippingAddressProvided;
        private bool _completed;

        public static Order Create(Guid id, Guid customerId, IEnumerable<OrderItem> items)
        {
            return new Order(id, customerId, items);
        }

        public Order() { }
        private Order(Guid id, Guid customerId, IEnumerable<OrderItem> items)
        {
            ApplyChange(new OrderCreated(id, customerId, items.ToArray()));
        }

        public void ProvideShippingAddress(Address address)
        {
            if (_shippingAddressProvided || _completed) return;

            ApplyChange((id, version) => new ShippingAddressProvided(id, version, address));
        }

        public void Pay()
        {
            if (_paidFor || _completed) return;

            ApplyChange((id, version) => new PaymentReceived(id, version));
        }

        public void CompleteOrder()
        {
            if (!_paidFor || !_shippingAddressProvided) throw new CannotCompleteOrderException();

            ApplyChange((id, version) => new OrderCompleted(id, version));
        }

        protected override IEnumerable<KeyValuePair<Type, Action<Event>>> EventAppliers
        {
            get
            {
                yield return CreateApplier<OrderCreated>(Apply);
                yield return CreateApplier<PaymentReceived>(Apply);
                yield return CreateApplier<ShippingAddressProvided>(Apply);
                yield return CreateApplier<OrderCompleted>(Apply);
            }
        }

        private void Apply(OrderCreated evt)
        {
        }

        private void Apply(ShippingAddressProvided evt)
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
