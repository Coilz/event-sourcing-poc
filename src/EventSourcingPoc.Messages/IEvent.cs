using System;

namespace EventSourcingPoc.Messages
{
    public abstract class IEvent
    {
        protected IEvent(Guid aggregateId, int version)
        {
            AggregateId = aggregateId;
            Version = version;
        }

        public Guid AggregateId { get; }
        public int Version { get; }

        /*
         DateTime TimeStamp {get;}
         Guid UserId {get;}
         Guid CorrelationId {get;}
         */
    }
}