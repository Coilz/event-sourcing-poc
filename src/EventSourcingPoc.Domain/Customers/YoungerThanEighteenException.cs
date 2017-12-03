using System;
using EventSourcingPoc.EventSourcing.Domain;

namespace EventSourcingPoc.Domain.Customers
{
    [Serializable]
    public class YoungerThanEighteenException : DomainException
    {
    }
}