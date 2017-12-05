using System;
using System.Threading.Tasks;
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

        protected async Task ExecuteAsync(Guid id, Action<T> action)
        {
            var eventStream = await Repository.GetByIdAsync<T>(id);
            action(eventStream);
            await Repository.SaveAsync(eventStream);
        }
    }
}
