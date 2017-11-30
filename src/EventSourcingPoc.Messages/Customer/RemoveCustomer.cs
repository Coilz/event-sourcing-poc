using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Messages.Customers
{
    public class RemoveCustomer : ICommand
    {
        public Guid EntityId { get; }
        public Guid OriginalVersion { get; }
        public RemoveCustomer(Guid entityId, Guid originalVersion)
        {
            this.OriginalVersion = originalVersion;
            this.EntityId = entityId;
        }
    }
}
