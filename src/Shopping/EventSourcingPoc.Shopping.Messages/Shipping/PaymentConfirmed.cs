using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Shipping
{
    public class PaymentConfirmed : Event
    {
        public PaymentConfirmed(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
