using System;
using System.Collections.Generic;

namespace EventSourcingPoc.Readmodels
{
    public interface IReadModelStore<T>
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Save(T model);
        void Remove(Guid id);
    }
}
