using EventSourcingPoc.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingPoc.Logistics.Messages.Shipment
{
    public class StartShipment : ICommand
    {
        public StartShipment(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        public Guid AggregateId { get; }
    }
}
