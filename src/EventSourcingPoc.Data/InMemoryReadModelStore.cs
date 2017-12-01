using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingPoc.Readmodels;

namespace EventSourcingPoc.Data
{
    public class InMemoryReadModelStore : IReadModelStore<ShoppingCartReadModel>
    {
        private readonly Dictionary<Guid, ShoppingCartReadModel> _carts = new Dictionary<Guid, ShoppingCartReadModel>();

        public IEnumerable<ShoppingCartReadModel> GetAll()
        {
            return _carts.Values;
        }

        public ShoppingCartReadModel Get(Guid id)
        {
            return _carts[id];
        }

        public void Save(ShoppingCartReadModel model)
        {
            if (_carts.ContainsKey(model.Id))
                _carts[model.Id] = model;
            else
                _carts.Add(model.Id, model);
        }

        public void Remove(Guid id)
        {
            if (_carts.ContainsKey(id))
            {
                _carts.Remove(id);
            }
        }
    }
}
