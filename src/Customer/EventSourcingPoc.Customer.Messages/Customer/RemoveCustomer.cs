﻿using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Customer.Messages.Customer
{
    public class RemoveCustomer : ICommand
    {
        public Guid CustomerId { get; }
        public Guid OriginalVersion { get; }
        public RemoveCustomer(Guid id, Guid originalVersion)
        {
            CustomerId = id;
            OriginalVersion = originalVersion;
        }
    }
}
