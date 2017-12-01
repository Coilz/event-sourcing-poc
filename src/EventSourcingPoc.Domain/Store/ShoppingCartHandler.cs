using System;

namespace EventSourcingPoc.Domain.Store
{
    using EventSourcing.Handlers;
    using EventSourcing.Persistence;
    using Messages.Store;

    public class ShoppingCartHandler
        : ICommandHandler<CreateNewCart>
        , ICommandHandler<AddProductToCart>
        , ICommandHandler<RemoveProductFromCart>
        , ICommandHandler<EmptyCart>
        , ICommandHandler<Checkout>
    {
        private readonly IRepository _repo;
        public ShoppingCartHandler(IRepository repo)
        {
            _repo = repo;
        }
        public void Handle(CreateNewCart cmd)
        {
            _repo.Save(ShoppingCart.Create(cmd.CartId, cmd.CustomerId));
        }

        public void Handle(AddProductToCart cmd)
        {
            Execute(cmd.CartId, cart => cart.AddProduct(cmd.ProductId, cmd.Price));
        }

        public void Handle(RemoveProductFromCart cmd)
        {
            Execute(cmd.CartId, cart => cart.RemoveProduct(cmd.ProductId));
        }

        public void Handle(EmptyCart cmd)
        {
            Execute(cmd.CartId, cart => cart.Empty());
        }

        public void Handle(Checkout cmd)
        {
            var cart = _repo.GetById<ShoppingCart>(cmd.CartId);
            var order = cart.Checkout();
            _repo.Save(cart, order);
        }

        private void Execute(Guid id, Action<ShoppingCart> action)
        {
            var cart = _repo.GetById<ShoppingCart>(id);
            action(cart);
            _repo.Save(cart);
        }
    }
}
