using System;
using System.Collections.Generic;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Messages.Customers;

namespace EventSourcingPoc.Domain.Customers
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

            ApplyChanges(new CustomerCreated(id, customerInfo));
        }

        public void Update(CustomerInfo customerInfo)
        {
            if (customerInfo.Name != _name)
                ChangeName(customerInfo.Name);

            if (customerInfo.Email != _email)
                ChangeEmail(customerInfo.Email);

            if (customerInfo.BirthDate != _birthDate)
                ChangeBirthDate(customerInfo.BirthDate);
        }

        public void ChangeName(string name)
        {
            if (_removed) throw new CannotModifyRemovedCustomerException();
            if (string.IsNullOrWhiteSpace(name)) throw new EmptyNameException();

            ApplyChanges(new CustomerNameUpdated(id, name));
        }

        public void ChangeEmail(string email)
        {
            if (_removed) throw new CannotModifyRemovedCustomerException();
            if (string.IsNullOrWhiteSpace(email)) throw new EmptyEmailException();

            ApplyChanges(new CustomerEmailUpdated(id, email));
        }

        public void ChangeBirthDate(DateTime birthDate)
        {
            if (_removed) throw new CannotModifyRemovedCustomerException();
            if (birthDate.AddYears(18) < DateTime.UtcNow) throw new YoungerThanEighteenException();

            ApplyChanges(new CustomerBirthDateUpdated(id, birthDate));
        }

        public void Remove()
        {
            ApplyChanges(new CustomerRemoved(id));
        }

        protected override IEnumerable<KeyValuePair<Type, Action<IEvent>>> EventAppliers
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
            id = evt.Id;
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
