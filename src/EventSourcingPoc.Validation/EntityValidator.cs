using System;
using EventSourcingPoc.Domain.Core;
using FluentValidation;

namespace EventSourcingPoc.Validation
{
    public class EntityValidator<T> : AbstractValidator<T> where T : Entity
    {
        public EntityValidator()
        {
            ValidateId();
        }

        private void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
