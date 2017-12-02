using EventSourcingPoc.Readmodels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

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

        public IEnumerable<T> GetAll()
        {
            return _carts.Values;
        }

        public T Get(Guid id)
        {
            if (_carts.TryGetValue(id, out var value)) return value;

            throw new InvalidOperationException($"Model {id} not found");
        }

        public void Save(T model)
        {
            if (_carts.TryAdd(model.Id, model)) return;
            if (_carts.TryGetValue(model.Id, out var value) && _carts.TryUpdate(model.Id, model, value)) return;

            throw new InvalidOperationException("Persisting readModel failed.");
        }

        public void Remove(Guid id)
        {
            _carts.TryRemove(id, out var value);
        }
    }
}
