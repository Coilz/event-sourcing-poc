using System;
using EventSourcingPoc.EventSourcing.Domain;

namespace EventSourcingPoc.EventSourcing.Exceptions
{
    [Serializable]
    public class EventStreamNotFoundException : EventSourceException
    {
        public EventStreamNotFoundException(StreamIdentifier identifier)
            : base(string.Format("Stream Not Found Id: {0}", identifier.Value))
        {
        }
    }
}