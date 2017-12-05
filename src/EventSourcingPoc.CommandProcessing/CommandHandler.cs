using System;
using System.Threading.Tasks;
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

        protected async Task ExecuteAsync(Guid id, Action<T> action)
        {
            var eventStream = await Repository.GetByIdAsync<T>(id);
            action(eventStream);
            await Repository.SaveAsync(eventStream);
        }
    }
}
