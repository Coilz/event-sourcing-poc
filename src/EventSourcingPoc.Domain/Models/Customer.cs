using System;
using EventSourcingPoc.Domain.Core;
using EventSourcingPoc.Domain.Core.Models;

namespace EventSourcingPoc.Domain
{
    public class Customer : Aggregate
    {
        public Customer(Guid id, CustomerInfo info)
            : base(id)
        {
            this.Info = info;
        }

        // Empty constructor for EF
        protected Customer() : base() { }

        public CustomerInfo Info { get; }
    }
}
