using System;
using EventSourcingPoc.Domain.Core.Events;

namespace EventSourcingPoc.Domain.Core.Commands
{
    public abstract class EntityCommand : Message
    {
        protected EntityCommand(Guid entityId, Guid originalVersion)
        {
            EntityId = entityId;
            OriginalVersion = originalVersion;
        }

        public Guid EntityId { get; }
        public Guid OriginalVersion { get; }
    }
}
