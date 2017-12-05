using System;
using EventSourcingPoc.EventSourcing.Domain;

namespace EventSourcingPoc.Shopping.Domain.Shop
{
    [Serializable]
    public class CannotCheckoutEmptyCartException : DomainException
    {
    }
}