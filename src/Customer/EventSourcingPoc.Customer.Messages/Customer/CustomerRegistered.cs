using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerRegistered : IEvent
    {
        public CustomerRegistered(Guid aggregateId, string name, string email, DateTime birthDate)
        {
            AggregateId = aggregateId;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public Guid AggregateId { get; }
        public string Name { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }
    }
}
