using System;
using System.Collections.Generic;

namespace EventSourcingPoc.Domain.Shipping
{
    using EventSourcing.Domain;
    using EventSourcing.Handlers;
    using Messages;
    using Messages.Orders;
    using Messages.Shipping;

    public class ShippingSaga : Saga
    {
        private enum Status
        {
            Started,
            PaymentReceived,
            AddressReceived,
            ReadyToComplete,
            Complete
        }

        private Status _status = Status.Started;

        public static ShippingSaga Create(Guid orderId)
        {
            return new ShippingSaga(orderId);
        }

        public ShippingSaga() {}
        private ShippingSaga(Guid orderId)
        {
            ApplyChanges(new ShippingProcessStarted(orderId));
        }

        public void ConfirmPayment()
        {
            if (!AwaitingPayment()) return;

            ApplyChanges(new PaymentConfirmed(id));
        }

        public void ConfirmAddress()
        {
            if (!AwaitingAddress()) return;

            ApplyChanges(new AddressConfirmed(id));
        }

        public void CompleteIfPossible()
        {
            if (_status != Status.ReadyToComplete) return;

            ApplyChanges(new OrderDelivered(id));
        }

        protected override IEnumerable<KeyValuePair<Type, Action<IEvent>>> EventAppliers
        {
            get
            {
                yield return CreateApplier<ShippingProcessStarted>(Apply);
                yield return CreateApplier<PaymentConfirmed>(Apply);
                yield return CreateApplier<AddressConfirmed>(Apply);
                yield return CreateApplier<OrderDelivered>(Apply);
            }
        }

        private bool AwaitingPayment()
        {
            return _status == Status.Started || _status == Status.AddressReceived;
        }

        private bool AwaitingAddress()
        {
            return _status == Status.Started || _status == Status.PaymentReceived;
        }

        private void Apply(ShippingProcessStarted evt)
        {
            id = evt.OrderId;
        }

        private void Apply(PaymentConfirmed evt)
        {
            _status = _status == Status.AddressReceived
                ? Status.ReadyToComplete
                : Status.PaymentReceived;
        }

        private void Apply(AddressConfirmed evt)
        {
            _status = _status == Status.PaymentReceived
                ? Status.ReadyToComplete
                : Status.AddressReceived;
        }

        private void Apply(OrderDelivered evt)
        {
            _status = Status.Complete;
            Queue(new CompleteOrder(evt.OrderId));
        }
    }
}
