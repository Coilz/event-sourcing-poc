using System;

namespace EventSourcingPoc.Messages
{
    public abstract class Event
    {
        protected Event(Guid aggregateId)
             : this(aggregateId, 0)
        {
        }

        protected Event(Guid aggregateId, int version)
        {
            AggregateId = aggregateId;
            Version = version;
            TimeStamp = DateTime.UtcNow;
        }

        public Guid AggregateId { get; }
        public int Version { get; }
        public DateTime TimeStamp { get; }

        /*
         DateTime TimeStamp {get;}
         Guid UserId {get;}
         Guid CorrelationId {get;}
         */
    }
}