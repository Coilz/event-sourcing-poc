using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Shopping.Domain.Shop;
using EventSourcingPoc.Shopping.Messages.Orders;
using EventSourcingPoc.Shopping.Messages.Shipping;

namespace EventSourcingPoc.Shopping.Domain.Shipping
{
    public class ShippingSaga : Saga
    {
        private enum Status
        {
            None = 0,
            Started,
            OrderCreated,
            PaymentReceived,
            AddressReceived,
            ReadyToCompleteForShipping,
            CompleteForShipping,
            Shipping,
            Delivered
        }

        private Status _status;
        private Guid _cartId;
        private Guid _orderId;
        private Guid _customerId;
        private IEnumerable<OrderItem> _orderItems;

        public static ShippingSaga Create(Guid orderId, Guid cartId, Guid customerId, IEnumerable<ShoppingCartItem> items)
        {
            return new ShippingSaga(orderId, cartId, customerId, items);
        }

        public ShippingSaga() {}
        private ShippingSaga(Guid orderId, Guid cartId, Guid customerId, IEnumerable<ShoppingCartItem> items)
        {
            ApplyChange(new ShippingProcessStarted(orderId, cartId));

            var orderItems = items
                .Select(item =>
                    new OrderItem(item.ProductId, item.Price, item.Quantity));
            QueueCommand(new PlaceOrder(orderId, customerId, orderItems));
        }

        public void ConfirmOrder(Guid customerId, IEnumerable<OrderItem> items)
        {
            if (_status != Status.Started) return;

            ApplyChange((id, version) => new OrderConfirmed(id, version, customerId, items));
        }

        public void ConfirmPayment()
        {
            if (!AwaitingPayment()) return;

            ApplyChange((id, version) => new PaymentConfirmed(id, version));
        }

        public void ConfirmAddress()
        {
            if (!AwaitingAddress()) return;

            ApplyChange((id, version) => new AddressConfirmed(id, version));
        }

        public void CompleteForShipping()
        {
            if (_status != Status.ReadyToCompleteForShipping) return;

            ApplyChange((id, version) => new CompletedForShipping(id, version, _customerId, _orderItems));
            QueueCommand(new CompleteOrder(_orderId));
        }

        public void ShipOrder()
        {
            if (_status != Status.CompleteForShipping) return;

            ApplyChange((id, version) => new OrderShipped(id, version));
        }

        public void DelivereOrder()
        {
            // TODO: add conditions
            ApplyChange((id, version) => new OrderDelivered(id, version));
        }

        protected override IEnumerable<KeyValuePair<Type, Action<Event>>> EventAppliers
        {
            get
            {
                yield return CreateApplier<ShippingProcessStarted>(Apply);
                yield return CreateApplier<OrderConfirmed>(Apply);
                yield return CreateApplier<PaymentConfirmed>(Apply);
                yield return CreateApplier<AddressConfirmed>(Apply);
                yield return CreateApplier<CompletedForShipping>(Apply);
                yield return CreateApplier<OrderShipped>(Apply);
                yield return CreateApplier<OrderDelivered>(Apply);
            }
        }

        private bool AwaitingPayment()
        {
            return _status <= Status.OrderCreated || _status == Status.AddressReceived;
        }

        private bool AwaitingAddress()
        {
            return _status <= Status.OrderCreated || _status == Status.PaymentReceived;
        }

        private void Apply(ShippingProcessStarted evt)
        {
            _cartId = evt.CartId;
            _status = Status.Started;
        }

        private void Apply(OrderConfirmed evt)
        {
            _orderId = evt.AggregateId;
            _customerId = evt.CustomerId;
            _orderItems = evt.Items;

            _status = Status.OrderCreated;
        }

        private void Apply(PaymentConfirmed evt)
        {
            _status = _status == Status.AddressReceived
                ? Status.ReadyToCompleteForShipping
                : Status.PaymentReceived;
        }

        private void Apply(AddressConfirmed evt)
        {
            _status = _status == Status.PaymentReceived
                ? Status.ReadyToCompleteForShipping
                : Status.AddressReceived;
        }

        private void Apply(CompletedForShipping evt)
        {
            _status = Status.CompleteForShipping;
        }

        private void Apply(OrderShipped evt)
        {
            _status = Status.Shipping;
        }

        private void Apply(OrderDelivered evt)
        {
            _status = Status.Delivered;
        }
    }
}
