using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerRegistered : IEvent
    {
        public CustomerRegistered(string name, string email, DateTime birthDate)
        {
            this.Name = name;
            this.Email = email;
            this.BirthDate = birthDate;

        }
        public string Name { get; }

        public string Email { get; }

        public DateTime BirthDate { get; }
    }
}
