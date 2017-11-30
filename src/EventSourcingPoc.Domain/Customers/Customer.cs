using System;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages.Customers;

namespace EventSourcingPoc.Domain
{
    public class Customer : Aggregate
    {
        protected override void RegisterAppliers()
        {
        }
    }
}
