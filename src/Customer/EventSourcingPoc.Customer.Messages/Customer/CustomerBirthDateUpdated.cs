using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerBirthDateUpdated : IEvent
    {
        public Guid AggregateId { get; }
        public DateTime BirthDate { get; }

        public CustomerBirthDateUpdated(Guid aggregateId, DateTime birthDate)
        {
            AggregateId = aggregateId;
            BirthDate = birthDate;
        }
    }
}
