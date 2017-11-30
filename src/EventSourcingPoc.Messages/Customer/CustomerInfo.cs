using System;

namespace EventSourcingPoc.Messages.Customers
{
    public class CustomerInfo
    {
        public CustomerInfo(string name, string email, DateTime birthDate)
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
