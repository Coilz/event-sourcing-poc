using System.Collections.Generic;
using System;
using System.Linq;
using EventSourcingPoc.EventSourcing.Exceptions;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Domain
{
    public abstract class EventStream
    {
        private Guid _id;
        private int _version = -1;
        private readonly List<Event> _changes = new List<Event>();
        private readonly Lazy<IDictionary<Type, Action<Event>>> _eventAppliers;

        protected EventStream()
        {
            _eventAppliers = new Lazy<IDictionary<Type, Action<Event>>>(() =>
                EventAppliers.ToDictionary(pair =>
                    pair.Key,
                    pair => pair.Value));
        }

        public string Name => GetType().Name;

        public StreamIdentifier StreamIdentifier => new StreamIdentifier(Name, _id);

        public void LoadFromHistory(IEnumerable<Event> history)
        {
            foreach(var evt in history.OrderBy(e => e.Version))
            {
                if (evt.Version != _version + 1)
                    throw new EventsOutOfOrderException(evt.AggregateId, GetType(), _version + 1, evt.Version);

                Replay(evt);
            }
        }

        public IEnumerable<Event> GetUncommitedChanges()
        {
            return _changes.AsReadOnly();
        }

        public void MarkChangesAsCommitted()
        {
            _version = _version + _changes.Count;
            _changes.Clear();
        }

        protected abstract IEnumerable<KeyValuePair<Type, Action<Event>>> EventAppliers { get; }

        protected static KeyValuePair<Type, Action<Event>> CreateApplier<TEvent>(Action<TEvent> applier)
            where TEvent : Event
        {
            return new KeyValuePair<Type, Action<Event>>(
                typeof(TEvent),
                x => applier((TEvent)x));
        }

        protected void ApplyChange(Event evt)
        {
            Apply(evt);
            _changes.Add(evt);
        }

        protected void ApplyChange(Func<Guid, int, Event> eventCreator)
        {
            var evt = eventCreator(_id, _version + _changes.Count + 1);
            ApplyChange(evt);
        }

        private void Replay(Event evt)
        {
            _version++;
            Apply(evt);
        }

        private void Apply(Event evt)
        {
            _id = evt.AggregateId;
            var evtType = evt.GetType();
            if (!_eventAppliers.Value.ContainsKey(evtType)) throw new NoEventApplyMethodRegisteredException(evt, this);

            _eventAppliers.Value[evtType](evt);
        }
    }
}
