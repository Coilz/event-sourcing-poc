using EventSourcingPoc.Readmodels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EventSourcingPoc.Data
{
    public class InMemoryReadModelStore<T> : IReadModelStore<T>
        where T : IReadModel
    {
        private static IReadModelStore<T> _instance;
        private readonly ConcurrentDictionary<Guid, T> _carts = new ConcurrentDictionary<Guid, T>();

        public static IReadModelStore<T> GetInstance()
        {
            if (_instance == null) _instance = new InMemoryReadModelStore<T>();

            return _instance;
        }

        private InMemoryReadModelStore() {}

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<T>>(_carts.Values);
        }

        public Task<T> GetAsync(Guid id)
        {
            if (_carts.TryGetValue(id, out var value)) return Task.FromResult(value);

            throw new InvalidOperationException($"Model {id} not found");
        }

        public Task SaveAsync(T model)
        {
            if (_carts.TryAdd(model.Id, model)) return Task.FromResult(0);
            if (_carts.TryGetValue(model.Id, out var value) && _carts.TryUpdate(model.Id, model, value)) return Task.FromResult(0);

            throw new InvalidOperationException("Persisting readModel failed.");
        }

        public Task RemoveAsync(Guid id)
        {
            _carts.TryRemove(id, out var value);
            return Task.FromResult(0);
        }
    }
}
