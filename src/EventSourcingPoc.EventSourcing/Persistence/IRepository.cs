using System;
using System.Threading.Tasks;
using EventSourcingPoc.EventSourcing.Domain;

namespace EventSourcingPoc.EventSourcing.Persistence
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(Guid id)
            where T : EventStream, new();

        Task SaveAsync(EventStream streamItems);
    }
}
