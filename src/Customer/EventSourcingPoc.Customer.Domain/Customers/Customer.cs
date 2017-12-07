using System;
using System.Collections.Generic;
using EventSourcingPoc.Customer.Messages.Customer;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Domain.Customers
{
    public class Customer : AggregateRoot
    {
        public static Customer Create(Guid id, CustomerInfo customerInfo)
        {
            return new Customer(id, customerInfo);
        }

        private string _name;
        private string _email;
        private DateTime _birthDate;
        private bool _removed;

        public Customer() { }
        private Customer(Guid id, CustomerInfo customerInfo)
        {
            if (customerInfo.BirthDate.AddYears(18) < DateTime.UtcNow) throw new YoungerThanEighteenException();

            ApplyChange(new CustomerCreated(id, customerInfo));
        }

        public void Update(CustomerInfo customerInfo)
        {
            ChangeName(customerInfo.Name);
            ChangeEmail(customerInfo.Email);
            ChangeBirthDate(customerInfo.BirthDate);
        }

        public void ChangeName(string name)
        {
            if (_removed) throw new CannotModifyRemovedCustomerException();
            if (string.IsNullOrWhiteSpace(name)) throw new EmptyNameException();

            if (name != _name)
                ApplyChange((id, version) => new CustomerNameUpdated(id, version, name));
        }

        public void ChangeEmail(string email)
        {
            if (_removed) throw new CannotModifyRemovedCustomerException();
            if (string.IsNullOrWhiteSpace(email)) throw new EmptyEmailException();

            if (email != _email)
                ApplyChange((id, version) => new CustomerEmailUpdated(id, version, email));
        }

        public void ChangeBirthDate(DateTime birthDate)
        {
            if (_removed) throw new CannotModifyRemovedCustomerException();
            if (birthDate.AddYears(18) < DateTime.UtcNow) throw new YoungerThanEighteenException();

            if (birthDate != _birthDate)
                ApplyChange((id, version) => new CustomerBirthDateUpdated(id, version, birthDate));
        }

        public void Remove()
        {
            ApplyChange((id, version) => new CustomerRemoved(id, version));
        }

        protected override IEnumerable<KeyValuePair<Type, Action<Event>>> EventAppliers
        {
            get
            {
                yield return CreateApplier<CustomerCreated>(Apply);
                yield return CreateApplier<CustomerNameUpdated>(Apply);
                yield return CreateApplier<CustomerEmailUpdated>(Apply);
                yield return CreateApplier<CustomerBirthDateUpdated>(Apply);
                yield return CreateApplier<CustomerRemoved>(Apply);
            }
        }

        private void Apply(CustomerCreated evt)
        {
            _name = evt.CustomerInfo.Name;
            _email = evt.CustomerInfo.Email;
            _birthDate = evt.CustomerInfo.BirthDate;
        }

        private void Apply(CustomerNameUpdated evt)
        {
            _name = evt.Name;
        }

        private void Apply(CustomerEmailUpdated evt)
        {
            _email = evt.Email;
        }

        private void Apply(CustomerBirthDateUpdated evt)
        {
            _birthDate = evt.BirthDate;
        }

        private void Apply(CustomerRemoved evt)
        {
            _removed = true;
        }
    }
}
