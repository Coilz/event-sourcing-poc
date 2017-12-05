using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class CustomerBirthDateUpdated : IEvent
    {
        public Guid CustomerId { get; }
        public DateTime BirthDate { get; }

        public CustomerBirthDateUpdated(Guid customerId, DateTime birthDate)
        {
            this.CustomerId = customerId;
            this.BirthDate = birthDate;
        }
    }
}
