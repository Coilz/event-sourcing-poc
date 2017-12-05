using System;
using EventSourcingPoc.EventSourcing.Domain;

namespace EventSourcingPoc.Domain.Shop
{
    [Serializable]
    public class CartAlreadyCheckedOutException : DomainException
    {
    }
}