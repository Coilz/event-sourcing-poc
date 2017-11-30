using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Messages.Customers
{
    public class UpdateCustomer : ICommand
    {
        public Guid EntityId { get; }
        public Guid OriginalVersion { get; }
        public CustomerInfo Info { get; }
        public UpdateCustomer(Guid entityId, Guid originalVersion, CustomerInfo info)
        {
            this.Info = info;
            this.OriginalVersion = originalVersion;
            this.EntityId = entityId;
        }
    }
}
