using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Messages.Customers
{
    public class CreateNewCustomer : ICommand
    {
        public Guid CustomerId { get; }
        public CustomerInfo Info { get; }
        public CreateNewCustomer(Guid id, CustomerInfo info)
        {
            CustomerId = id;
            Info = info;
        }
    }
}
