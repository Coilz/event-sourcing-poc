using EventSourcingPoc.Domain.Orders;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages.Orders;

namespace EventSourcingPoc.CommandProcessing
{
    public class OrderCommandHandler
        : CommandHandler<Order>
        , ICommandHandler<PayForOrder>
        , ICommandHandler<ConfirmShippingAddress>
        , ICommandHandler<CompleteOrder>
    {
        public OrderCommandHandler(IRepository repository)
            : base(repository)
        {}

        public void Handle(PayForOrder cmd)
        {
            Execute(cmd.OrderId, order => order.Pay());
        }

        public void Handle(ConfirmShippingAddress cmd)
        {
            Execute(cmd.OrderId, order => order.ProvideShippingAddress(cmd.Address));
        }

        public void Handle(CompleteOrder cmd)
        {
            Execute(cmd.OrderId, order => order.CompleteOrder());
        }
    }
}
