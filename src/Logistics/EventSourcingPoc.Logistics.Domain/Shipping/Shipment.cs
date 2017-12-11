using System;
using System.Collections.Generic;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Logistics.Messages.Shipping;

namespace EventSourcingPoc.Logistics.Domain.Shipping
{
    public class Shipment : AggregateRoot
    {
        private enum Status
        {
            None,
            Created,
            Started,
            Delivered
        }

        private Status _status = Status.Created;

        public static Shipment Create(Guid id, Guid customerId, IEnumerable<ShipmentItem> shippingItems)
        {
            return new Shipment(id, customerId, shippingItems);
        }

        public Shipment() {}
        private Shipment(Guid id, Guid customerId, IEnumerable<ShipmentItem> shippingItems)
        {
            ApplyChange(new ShipmentCreated(id, customerId, shippingItems));
        }

        public void Start()
        {
            if (_status != Status.Created) return;

            ApplyChange((id, version) => new ShipmentStarted(id, version));
        }
        public void Deliver()
        {
            if (_status == Status.Delivered) return;
            if(_status == Status.Created)
                ApplyChange((id, version) => new ShipmentStarted(id, version));

            ApplyChange((id, version) => new ShipmentDelivered(id, version));
        }

        protected override IEnumerable<KeyValuePair<Type, Action<Event>>> EventAppliers
        {
            get
            {
                yield return CreateApplier<ShipmentCreated>(Apply);
                yield return CreateApplier<ShipmentStarted>(Apply);
                yield return CreateApplier<ShipmentDelivered>(Apply);
            }
        }

        private void Apply(ShipmentCreated evt)
        {
            _status = Status.Created;
        }

        private void Apply(ShipmentStarted evt)
        {
            _status = Status.Started;
        }

        private void Apply(ShipmentDelivered evt)
        {
            _status = Status.Delivered;
        }
    }
}
