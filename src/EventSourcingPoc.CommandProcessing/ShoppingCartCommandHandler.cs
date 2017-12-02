using System;
using System.Linq;
using EventSourcingPoc.Domain.Orders;
using EventSourcingPoc.Domain.Store;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages.Orders;
using EventSourcingPoc.Messages.Store;

namespace EventSourcingPoc.CommandProcessing
{
    public class ShoppingCartCommandHandler
        : ICommandHandler<CreateNewCart>
        , ICommandHandler<AddProductToCart>
        , ICommandHandler<RemoveProductFromCart>
        , ICommandHandler<EmptyCart>
        , ICommandHandler<Checkout>
    {
        private readonly IRepository _repository;
        public ShoppingCartCommandHandler(IRepository repository)
        {
            _repository = repository;
        }
        public void Handle(CreateNewCart cmd)
        {
            _repository.Save(ShoppingCart.Create(cmd.CartId, cmd.CustomerId));
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
            var cart = _repository.GetById<ShoppingCart>(cmd.CartId);
            cart.Checkout();
            _repository.Save(cart);

            var orderItems = cart.ShoppingCartItems
                .Select(item =>
                    new OrderItem(item.ProductId, item.Price, item.Quantity));

            var order = Order.Create(cmd.CartId, cart.CustomerId, orderItems);
            _repository.Save(order);
        }

        private void Execute(Guid id, Action<ShoppingCart> action)
        {
            var cart = _repository.GetById<ShoppingCart>(id);
            action(cart);
            _repository.Save(cart);
        }
    }
}
