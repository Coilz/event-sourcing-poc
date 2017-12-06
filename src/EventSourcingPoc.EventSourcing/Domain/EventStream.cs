using System.Collections.Generic;
using System;
using System.Linq;
using EventSourcingPoc.EventSourcing.Exceptions;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Domain
{
    public abstract class EventStream
    {
        private readonly List<IEvent> _changes;
        private readonly Lazy<IDictionary<Type, Action<IEvent>>> _eventAppliers;

        protected EventStream()
        {
            Version = -1;
            _changes = new List<IEvent>();
            _eventAppliers = new Lazy<IDictionary<Type, Action<IEvent>>>(() =>
                EventAppliers.ToDictionary(pair =>
                    pair.Key,
                    pair => pair.Value));
        }

        protected Guid Id { get; private set; }

        public string Name => GetType().Name;

        public StreamIdentifier StreamIdentifier => new StreamIdentifier(Name, Id);

        public int Version { get; private set; }

        public void LoadFromHistory(IEnumerable<IEvent> history)
        {
            foreach(var evt in history.OrderBy(e => e.Version))
            {
                if (evt.Version != Version + 1)
                    throw new EventsOutOfOrderException(evt.AggregateId, GetType(), Version + 1, evt.Version);

                Replay(evt);
            }
        }

        public IEnumerable<IEvent> GetUncommitedChanges()
        {
            return _changes.AsReadOnly();
        }

        public void MarkChangesAsCommitted()
        {
            Version = Version + _changes.Count;
            _changes.Clear();
        }

        protected abstract IEnumerable<KeyValuePair<Type, Action<IEvent>>> EventAppliers { get; }

        protected static KeyValuePair<Type, Action<IEvent>> CreateApplier<TEvent>(Action<TEvent> applier)
            where TEvent : IEvent
        {
            return new KeyValuePair<Type, Action<IEvent>>(
                typeof(TEvent),
                x => applier((TEvent)x));
        }

        protected void ApplyChange(IEvent evt)
        {
            Apply(evt);
            _changes.Add(evt);
        }

        protected void ApplyChange(Func<Guid, int, IEvent> eventCreator)
        {
            var evt = eventCreator(Id, Version + _changes.Count + 1);
            ApplyChange(evt);
        }

        private void Replay(IEvent evt)
        {
            Version++;
            Apply(evt);
        }

        private void Apply(IEvent evt)
        {
            Id = evt.AggregateId;
            var evtType = evt.GetType();
            if (!_eventAppliers.Value.ContainsKey(evtType)) throw new NoEventApplyMethodRegisteredException(evt, this);

            _eventAppliers.Value[evtType](evt);
        }
    }
}
