using System.Threading.Tasks;
using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Shopping.Domain.Shop;
using EventSourcingPoc.Shopping.Messages.Shop;

namespace EventSourcingPoc.Shopping.CommandProcessing
{
    public class ShoppingCartCommandHandler
        : CommandHandler<ShoppingCart>
        , ICommandHandler<CreateNewCart>
        , ICommandHandler<AddProductToCart>
        , ICommandHandler<RemoveProductFromCart>
        , ICommandHandler<EmptyCart>
        , ICommandHandler<Checkout>
    {
        public ShoppingCartCommandHandler(IRepository repository)
            : base((IRepository) repository)
        {}
        public async Task HandleAsync(CreateNewCart command)
        {
            await Repository.SaveAsync(ShoppingCart.Create(command.CartId, command.CustomerId));
        }

        public async Task HandleAsync(AddProductToCart command)
        {
            await ExecuteAsync(command.CartId, cart => cart.AddProduct(command.ProductId, command.Price));
        }

        public async Task HandleAsync(RemoveProductFromCart command)
        {
            await ExecuteAsync(command.CartId, cart => cart.RemoveProduct(command.ProductId));
        }

        public async Task HandleAsync(EmptyCart command)
        {
            await ExecuteAsync(command.CartId, cart => cart.Empty());
        }

        public async Task HandleAsync(Checkout command)
        {
            await ExecuteAsync(command.CartId, cart => cart.Checkout());
        }
    }
}
