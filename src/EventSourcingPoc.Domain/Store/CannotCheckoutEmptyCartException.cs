using System;
using EventSourcingPoc.EventSourcing.Domain;

namespace EventSourcingPoc.Domain.Store
{
    [Serializable]
    public class CannotCheckoutEmptyCartException : DomainException
    {
    }
}