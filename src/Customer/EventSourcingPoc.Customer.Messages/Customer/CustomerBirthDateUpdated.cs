using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerBirthDateUpdated : Event
    {
        public DateTime BirthDate { get; }

        public CustomerBirthDateUpdated(Guid aggregateId, int version, DateTime birthDate)
            : base(aggregateId, version)
        {
            BirthDate = birthDate;
        }
    }
}
