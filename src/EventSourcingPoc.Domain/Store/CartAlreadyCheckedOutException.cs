using System;
using EventSourcingPoc.EventSourcing.Domain;

namespace EventSourcingPoc.Domain.Store
{
    [Serializable]
    public class CartAlreadyCheckedOutException : DomainException
    {
    }
}