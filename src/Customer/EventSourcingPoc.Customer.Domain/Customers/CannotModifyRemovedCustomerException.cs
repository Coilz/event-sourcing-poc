using System;
using EventSourcingPoc.EventSourcing.Domain;

namespace EventSourcingPoc.Customer.Domain.Customers
{
    [Serializable]
    public class CannotModifyRemovedCustomerException : DomainException
    {
    }
}