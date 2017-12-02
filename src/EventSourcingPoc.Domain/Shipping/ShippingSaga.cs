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
            ApplyChanges(new StartedShippingProcess(orderId));
        }

        public void ConfirmPayment(ICommandDispatcher dispatcher) // TODO: Do we need the ICommandDispatcher?
        {
            if (!AwaitingPayment()) return;

            ApplyChanges(new PaymentConfirmed(id));
            CompleteIfPossible(dispatcher);
        }

        public void ConfirmAddress(ICommandDispatcher dispatcher) // TODO: Do we need the ICommandDispatcher?
        {
            if (!AwaitingAddress()) return;

            ApplyChanges(new AddressConfirmed(id));
            CompleteIfPossible(dispatcher);
        }

        protected override IEnumerable<KeyValuePair<Type, Action<IEvent>>> EventAppliers
        {
            get
            {
                yield return CreateApplier<StartedShippingProcess>(Apply);
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

        private void CompleteIfPossible(ICommandDispatcher dispatcher)
        {
            if (_status != Status.ReadyToComplete) return;

            ApplyChanges(new OrderDelivered(id));
            dispatcher.Send(new CompleteOrder(id)); // TODO: this is wierd should do it after events have been persisted
        }

        private void Apply(StartedShippingProcess evt)
        {
            id = evt.OrderId;
        }

        private void Apply(PaymentConfirmed evt)
        {
            _status = _status == Status.AddressReceived ? Status.ReadyToComplete : Status.PaymentReceived;
        }

        private void Apply(AddressConfirmed evt)
        {
            _status = _status == Status.PaymentReceived ? Status.ReadyToComplete : Status.AddressReceived;
        }

        private void Apply(OrderDelivered obj)
        {
            _status = Status.Complete;
        }
    }
}
