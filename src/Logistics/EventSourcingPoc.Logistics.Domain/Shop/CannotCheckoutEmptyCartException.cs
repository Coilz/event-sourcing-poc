using System;
using EventSourcingPoc.EventSourcing.Domain;

namespace EventSourcingPoc.Logistics.Domain.Shop
{
    [Serializable]
    public class CannotCheckoutEmptyCartException : DomainException
    {
    }
}