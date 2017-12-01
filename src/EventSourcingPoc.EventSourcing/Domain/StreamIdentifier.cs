using System;
using EventSourcingPoc.EventSourcing.Exceptions;

namespace EventSourcingPoc.EventSourcing.Domain
{
    public struct StreamIdentifier
    {
        public StreamIdentifier(string name, Guid id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new StreamIdentifierException("StreamIdentifier Name Required");
            if(id == Guid.Empty) throw new StreamIdentifierException("StreamIdentifier Id Required");

            Value = $"{name}-{id}";
        }

        public string Value { get; }
    }
}
