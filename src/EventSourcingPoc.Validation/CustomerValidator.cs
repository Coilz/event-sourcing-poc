using System;
using EventSourcingPoc.Domain;
using FluentValidation;

namespace EventSourcingPoc.Validation
{
    public class CustomerValidator : EntityValidator<Customer>
    {
        public CustomerValidator()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }

        private void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }

        private void ValidateBirthDate()
        {
            RuleFor(c => c.BirthDate)
                .NotEmpty().WithMessage("Please ensure you have entered the birth date")
                .Must(HaveMinimumAge).WithMessage("The customer must have 18 years or more");
        }

        private void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Please ensure you have entered the email")
                .EmailAddress().WithMessage("The email must be a valid email address");
        }

        private static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.UtcNow.AddYears(-18);
        }
    }
}
