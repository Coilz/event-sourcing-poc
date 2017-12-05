using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Shopping.Domain.Orders;
using EventSourcingPoc.Shopping.Messages.Orders;

namespace EventSourcingPoc.Shopping.CommandProcessing
{
    public class OrderCommandHandler
        : CommandHandler<Order>
        , ICommandHandler<PayForOrder>
        , ICommandHandler<ConfirmShippingAddress>
        , ICommandHandler<CompleteOrder>
    {
        public OrderCommandHandler(IRepository repository)
            : base((IRepository) repository)
        {}

        public void Handle(PayForOrder command)
        {
            Execute(command.OrderId, order => order.Pay());
        }

        public void Handle(ConfirmShippingAddress command)
        {
            Execute(command.OrderId, order => order.ProvideShippingAddress(command.Address));
        }

        public void Handle(CompleteOrder command)
        {
            Execute(command.OrderId, order => order.CompleteOrder());
        }
    }
}
