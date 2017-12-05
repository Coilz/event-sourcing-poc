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
        public void Handle(CreateNewCart command)
        {
            Repository.Save(ShoppingCart.Create(command.CartId, command.CustomerId));
        }

        public void Handle(AddProductToCart command)
        {
            Execute(command.CartId, cart => cart.AddProduct(command.ProductId, command.Price));
        }

        public void Handle(RemoveProductFromCart command)
        {
            Execute(command.CartId, cart => cart.RemoveProduct(command.ProductId));
        }

        public void Handle(EmptyCart command)
        {
            Execute(command.CartId, cart => cart.Empty());
        }

        public void Handle(Checkout command)
        {
            Execute(command.CartId, cart => cart.Checkout());
        }
    }
}
