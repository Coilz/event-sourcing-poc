﻿using System;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Exceptions
{
    [Serializable]
    public class NoEventApplyMethodRegisteredException : EventSourceException
    {
        public NoEventApplyMethodRegisteredException(Event evt, EventStream eventStream)
            : base (string.Format("No Event Applier Registered For {0} on {1}", evt.GetType().Name, eventStream.Name))
        {
        }
    }
}