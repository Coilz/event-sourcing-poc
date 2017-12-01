using System;
using System.Collections.Generic;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Domain.Customers
{
    public class Customer : Aggregate
    {
        protected override IEnumerable<KeyValuePair<Type, Action<IEvent>>> EventAppliers { get; }
    }
}
