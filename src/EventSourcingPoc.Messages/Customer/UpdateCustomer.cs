using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Messages.Customers
{
    public class UpdateCustomer : ICommand
    {
        public Guid CustomerId { get; }
        public Guid OriginalVersion { get; }
        public CustomerInfo Info { get; }
        public UpdateCustomer(Guid id, Guid originalVersion, CustomerInfo info)
        {
            this.Info = info;
            this.OriginalVersion = originalVersion;
            this.CustomerId = id;
        }
    }
}
