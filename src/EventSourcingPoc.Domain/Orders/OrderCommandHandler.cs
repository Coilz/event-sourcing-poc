using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages.Orders;

namespace EventSourcingPoc.Domain.Orders
{
    public class OrderCommandHandler // TODO: Move from Domain to CommandProcessing
        : ICommandHandler<PayForOrder>
        , ICommandHandler<ConfirmShippingAddress>
        , ICommandHandler<CompleteOrder>
    {
        private readonly IRepository _repository;

        public OrderCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(PayForOrder cmd)
        {
            var order = _repository.GetById<Order>(cmd.OrderId);
            order.Pay();
            _repository.Save(order);
        }

        public void Handle(ConfirmShippingAddress cmd)
        {
            var order = _repository.GetById<Order>(cmd.OrderId);
            order.ProvideShippingAddress(cmd.Address);
            _repository.Save(order);
        }

        public void Handle(CompleteOrder cmd)
        {
            var order = _repository.GetById<Order>(cmd.OrderId);
            order.CompleteOrder();
            _repository.Save(order);
        }
    }
}
