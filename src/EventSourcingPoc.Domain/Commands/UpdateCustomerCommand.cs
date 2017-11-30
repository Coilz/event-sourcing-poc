using System;
using EventSourcingPoc.Domain.Core.Commands;

namespace EventSourcingPoc.Domain.Commands
{
    public class UpdateCustomerCommand : EntityCommand
    {
        public UpdateCustomerCommand(Guid entityId, Guid originalVersion, CustomerInfo info)
            : base(entityId, originalVersion)
        {
            this.Info = info;
        }

        public CustomerInfo Info { get; }
    }
}
