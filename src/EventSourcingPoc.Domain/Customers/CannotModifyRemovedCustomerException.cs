﻿using System;
using EventSourcingPoc.EventSourcing.Domain;

namespace EventSourcingPoc.Domain.Customers
{
    [Serializable]
    public class CannotModifyRemovedCustomerException : DomainException
    {
    }
}