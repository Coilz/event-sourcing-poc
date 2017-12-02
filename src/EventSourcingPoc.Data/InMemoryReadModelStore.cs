using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using EventSourcingPoc.Readmodels;

namespace EventSourcingPoc.Data
{
    public class InMemoryReadModelStore : IReadModelStore<ShoppingCartReadModel>
    {
        private static IReadModelStore<ShoppingCartReadModel> _instance;
        private readonly ConcurrentDictionary<Guid, ShoppingCartReadModel> _carts = new ConcurrentDictionary<Guid, ShoppingCartReadModel>();

        public static IReadModelStore<ShoppingCartReadModel> GetInstance()
        {
            if (_instance == null) _instance = new InMemoryReadModelStore();

            return _instance;
        }

        private InMemoryReadModelStore() {}

        public IEnumerable<ShoppingCartReadModel> GetAll()
        {
            return _carts.Values;
        }

        public ShoppingCartReadModel Get(Guid id)
        {
            if (_carts.TryGetValue(id, out var value)) return value;

            throw new InvalidOperationException($"ShoppingCartReadModel {id} not found");
        }

        public void Save(ShoppingCartReadModel model)
        {
            if (_carts.TryAdd(model.Id, model)) return;
            if (_carts.TryGetValue(model.Id, out var value))
            {
                if (_carts.TryUpdate(model.Id, model, value)) return;
            }

            throw new InvalidOperationException("Persisting readModel failed.");
        }

        public void Remove(Guid id)
        {
            _carts.TryRemove(id, out var value);
        }
    }
}
