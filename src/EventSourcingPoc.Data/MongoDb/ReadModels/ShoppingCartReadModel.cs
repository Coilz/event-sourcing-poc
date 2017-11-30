using System;
using System.Collections.Generic;

namespace EventSourcingPoc.Data.MongoDb.ReadModels
{
    public class ShoppingCartReadModel
    {
        public ShoppingCartReadModel()
        {
            this.Items = new List<ShoppingCartItemReadModel>();
        }
        public List<ShoppingCartItemReadModel> Items { get; private set; }
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
    }
}