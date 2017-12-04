using System;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.EventSourcing.Persistence;

namespace EventSourcingPoc.EventProcessing
{
    public abstract class EventHandler<T>
        where T : EventStream, new()
    {
        protected EventHandler(IRepository repository)
        {
            Repository = repository;
        }

        protected IRepository Repository { get; }

        protected void Execute(Guid id, Action<T> action)
        {
            var eventStream = Repository.GetById<T>(id);
            action(eventStream);
            Repository.Save(eventStream);
        }
    }
}
