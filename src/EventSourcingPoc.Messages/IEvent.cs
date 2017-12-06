using System;

namespace EventSourcingPoc.Messages
{
    public interface IEvent
    {
        Guid AggregateId { get; }
        /*
         Guid AggregateId {get;}
         DateTime TimeStamp {get;}
         Guid UserId {get;}
         int Version {get;}
         Guid CorrelationId {get;}
         */
    }
}