using System;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.EventSourcing.Persistence;

namespace EventSourcingPoc.CommandProcessing
{
    public abstract class CommandHandler<T>
        where T : EventStream, new()
    {
        public CommandHandler(IRepository repository)
        {
            Repository = repository;
        }

        protected IRepository Repository { get; }

        protected void Execute(Guid id, Action<T> action)
        {
            var aggregate = Repository.GetById<T>(id);
            action(aggregate);
            Repository.Save(aggregate);
        }
    }
}
