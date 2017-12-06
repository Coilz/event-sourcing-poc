using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Messages.Orders
{
    public class PaymentReceived : Event
    {
        public PaymentReceived(Guid aggregateId, int version)
            : base(aggregateId, version)
        {
        }
    }
}
