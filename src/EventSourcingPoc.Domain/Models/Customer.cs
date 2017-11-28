using System;
using EventSourcingPoc.Domain.Core;

namespace EventSourcingPoc.Domain
{
    public class Customer : Entity
    {
        public Customer(Guid id, string name, string email, DateTime birthDate)
            : base(id)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        // Empty constructor for EF
        protected Customer() { }

        public string Name { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }
    }
}
