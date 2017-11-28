using System;
using EventSourcingPoc.Domain.Core.Commands;

namespace EventSourcingPoc.Domain.Commands
{
    public class RemoveCustomerCommand : EntityCommand
    {
        public RemoveCustomerCommand(Guid entityId, Guid originalVersion)
            : base(entityId, originalVersion)
        {
        }
    }
}
