using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class PaymentConfirmed : IEvent
    {
        public Guid AggregateId { get; }
        public PaymentConfirmed(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
