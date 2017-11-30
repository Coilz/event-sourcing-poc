using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Messages.Customers
{
    public class CustomerNameUpdated : IEvent
    {
        public Guid EntityId { get; }
        public string Name { get; }
        public CustomerNameUpdated(Guid entityId, string name)
        {
            this.Name = name;
            this.EntityId = entityId;
        }
    }
}
