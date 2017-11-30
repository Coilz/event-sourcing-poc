using System;

namespace EventSourcingPoc.EventSourcing.Exceptions
{
    [Serializable]
    public class StreamIdentifierException : EventSourceException
    {
        public StreamIdentifierException(string message) : base(message)
        {
        }
    }
}