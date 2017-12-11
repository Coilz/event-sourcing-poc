using EventSourcingPoc.Messages;
using System;

namespace EventSourcingPoc.Logistics.Messages.Shipment
{
    public class DeliverShipment : ICommand
    {
        public DeliverShipment(Guid aggragateId)
        {
            AggregateId = aggragateId;
        }

        public Guid AggregateId { get; }
    }
}
