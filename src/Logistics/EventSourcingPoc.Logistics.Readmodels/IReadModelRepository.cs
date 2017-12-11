using System;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels
{
    public interface IReadModelRepository<T>
    {
        Task<T> GetAsync(Guid id);
        Task SaveAsync(T model);
        Task RemoveAsync(Guid id);
    }
}
