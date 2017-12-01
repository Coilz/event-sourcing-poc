using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages.Orders;
using EventSourcingPoc.Messages.Shipping;

namespace EventSourcingPoc.Domain.Orders
{
    public class OrderHandler
        : ICommandHandler<PayForOrder>
        , ICommandHandler<ConfirmShippingAddress>
        , ICommandHandler<CompleteOrder>
    {
        private readonly IRepository repository;

        public OrderHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(PayForOrder cmd)
        {
            var order = this.repository.GetById<Order>(cmd.OrderId);
            order.Pay();
            this.repository.Save(order);
        }

        public void Handle(ConfirmShippingAddress cmd)
        {
            var order = this.repository.GetById<Order>(cmd.OrderId);
            order.ProvideShippingAddress(cmd.Address);
            this.repository.Save(order);
        }

        public void Handle(CompleteOrder cmd)
        {
            var order = this.repository.GetById<Order>(cmd.OrderId);
            order.CompleteOrder();
            this.repository.Save(order);
        }
    }
}