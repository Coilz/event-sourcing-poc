using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerRegistered : Event
    {
        public CustomerRegistered(Guid aggregateId, int version, string name, string email, DateTime birthDate)
            : base(aggregateId, version)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public string Name { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }
    }
}
