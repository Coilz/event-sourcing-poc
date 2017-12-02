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
            _changes = new List<IEvent>();
            _eventAppliers = new Lazy<IDictionary<Type, Action<IEvent>>>(() =>
                EventAppliers.ToDictionary(pair =>
                    pair.Key,
                    pair => pair.Value));
        }

        protected Guid id { get; set; }

        public string Name => GetType().Name;

        public StreamIdentifier StreamIdentifier => new StreamIdentifier(Name, id);

        public void LoadFromHistory(IEnumerable<IEvent> history)
        {
            foreach(var evt in history)
            {
                Apply(evt);
            }
        }

        public IEnumerable<IEvent> GetUncommitedChanges()
        {
            return _changes.AsReadOnly();
        }

        public void Commit()
        {
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

        protected void ApplyChanges(IEvent evt)
        {
            Apply(evt);
            _changes.Add(evt);
        }

        private void Apply(IEvent evt)
        {
            var evtType = evt.GetType();
            if (!_eventAppliers.Value.ContainsKey(evtType)) throw new NoEventApplyMethodRegisteredException(evt, this);

            _eventAppliers.Value[evtType](evt);
        }
    }
}
