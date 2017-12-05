using System.Threading.Tasks;
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

        public async Task HandleAsync(PayForOrder command)
        {
            await ExecuteAsync(command.OrderId, order => order.Pay());
        }

        public async Task HandleAsync(ConfirmShippingAddress command)
        {
            await ExecuteAsync(command.OrderId, order => order.ProvideShippingAddress(command.Address));
        }

        public async Task HandleAsync(CompleteOrder command)
        {
            await ExecuteAsync(command.OrderId, order => order.CompleteOrder());
        }
    }
}