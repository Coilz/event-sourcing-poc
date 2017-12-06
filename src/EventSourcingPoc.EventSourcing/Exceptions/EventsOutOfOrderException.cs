using System;

namespace EventSourcingPoc.EventSourcing.Exceptions
{
    [Serializable]
    public class EventsOutOfOrderException : EventSourceException
    {
        public EventsOutOfOrderException(Guid id, Type aggregateType, int currentVersion, int providedEventVersion)
            : base($"Eventstore gave event for aggregate '{id}' of type '{aggregateType.FullName}' out of order at version {currentVersion} by providing {providedEventVersion}")
        {
            Id = id;
            AggregateType = aggregateType;
            CurrentVersion = currentVersion;
            ProvidedEventVersion = providedEventVersion;
        }

        public Guid Id { get; set; }
        public Type AggregateType { get; set; }
        public int CurrentVersion { get; set; }
        public int ProvidedEventVersion { get; set; }
    }
}
