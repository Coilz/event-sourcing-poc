﻿using System;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Exceptions
{
    [Serializable]
    public class NoEventHandlerMethodRegisteredException : EventSourceException
    {
        public NoEventHandlerMethodRegisteredException(IEvent evt, Type handerType)
            : base(string.Format("No EventsHandler Registered For {0} on {1}", evt.GetType().Name, handerType.Name))
        {
        }
    }
}