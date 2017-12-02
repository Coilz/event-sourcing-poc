using System;

namespace EventSourcingPoc.Readmodels
{
    public interface IReadModelRepository<T>
    {
        T Get(Guid id);
        void Save(T model);
        void Remove(Guid id);
    }
}
