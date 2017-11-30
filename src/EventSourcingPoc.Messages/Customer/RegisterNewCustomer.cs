using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Messages.Customers
{
    public class RegisterNewCustomer : ICommand
    {
        public Guid EntityId { get; }
        public CustomerInfo Info { get; }
        public RegisterNewCustomer(Guid entityId, Guid originalVersion, CustomerInfo info)
        {
            this.Info = info;
            this.EntityId = entityId;
        }
    }
}
