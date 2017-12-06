using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
{
    public class PaymentReceived : IEvent
    {
        public Guid AggregateId { get; }
        public PaymentReceived(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
