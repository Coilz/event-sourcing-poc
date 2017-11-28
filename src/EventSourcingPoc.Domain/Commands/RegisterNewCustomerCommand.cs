using System;
using EventSourcingPoc.Domain.Core.Commands;

namespace EventSourcingPoc.Domain.Commands
{
    public class RegisterNewCustomerCommand : EntityCommand
    {
        public RegisterNewCustomerCommand(Guid entityId, Guid originalVersion, string name, string email, DateTime birthDate)
            : base(entityId, originalVersion)
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
