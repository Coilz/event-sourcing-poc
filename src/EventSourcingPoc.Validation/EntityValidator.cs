using System;
using EventSourcingPoc.Domain.Core;
using EventSourcingPoc.Domain.Core.Models;
using FluentValidation;

namespace EventSourcingPoc.Validation
{
    public class EntityValidator<T> : AbstractValidator<T> where T : Aggregate
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
