using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels
{
    public interface IReadModelStore<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task SaveAsync(T model);
        Task RemoveAsync(Guid id);
    }
}
