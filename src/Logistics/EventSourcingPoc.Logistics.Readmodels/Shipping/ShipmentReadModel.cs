using System;
using System.Collections.Generic;

namespace EventSourcingPoc.Readmodels.Shipping
{
    public class ShipmentReadModel : IReadModel
    {
        public enum ShipmentStatus
        {
            None = 0,
            Created,
            Started,
            Delivered
        }

        public ShipmentReadModel(Guid id, Guid customerId, IEnumerable<ShipmentItemReadModel> items)
        {
            Id = id;
            CustomerId = customerId;
            Items = items;
        }

        public Guid Id { get; }
        public Guid CustomerId { get; }
        public IEnumerable<ShipmentItemReadModel> Items { get; }
        public bool Started { get; private set; }
        public bool Delivered { get; private set; }

        public ShipmentStatus Status => Delivered
                ? ShipmentStatus.Delivered
                : Started
                    ? ShipmentStatus.Started
                    : ShipmentStatus.Created;

        public void Start()
        {
            Started = true;
        }

        public void Deliver()
        {
            Delivered = true;
        }
    }
}
