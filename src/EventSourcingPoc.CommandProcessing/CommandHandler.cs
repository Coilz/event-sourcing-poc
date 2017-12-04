using System;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.EventSourcing.Persistence;

namespace EventSourcingPoc.CommandProcessing
{
    public abstract class CommandHandler<T>
        where T : EventStream, new()
    {
        protected CommandHandler(IRepository repository)
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
