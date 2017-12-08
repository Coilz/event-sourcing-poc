using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Shopping.Messages.Shop;
using EventSourcingPoc.Shopping.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingPoc.Shopping.WebApi.Controllers
{
    [Produces("application/json")]
    public abstract class ShoppingCartController<TCommand> : Controller
        where TCommand : ICommand
    {
        protected readonly ICommandHandler<TCommand> commandHandler;

        public ShoppingCartController(ICommandHandler<TCommand> commandHandler)
        {
            this.commandHandler = commandHandler;
        }
    }

    [Route(template: "api/Customer/{customerId}/ShoppingCart")]
    public class CreateNewCartController : ShoppingCartController<CreateNewCart>
    {
        public CreateNewCartController(ICommandHandler<CreateNewCart> commandHandler)
            : base(commandHandler)
        {
        }

        [HttpPost]
        public async Task PostAsync(Guid customerId)
        {
            await commandHandler.HandleAsync(new CreateNewCart(Guid.NewGuid(), customerId));
        }
    }

    [Route(template: "api/ShoppingCart/{cartId}/Product/{productId}")]
    public class AddProductToCartController : ShoppingCartController<AddProductToCart>
    {
        public AddProductToCartController(ICommandHandler<AddProductToCart> commandHandler)
            : base(commandHandler)
        {
        }

        [HttpPost]
        public async Task PostAsync(Guid cartId, Guid productId, AddProductToCartDTO dto)
        {
            await commandHandler.HandleAsync(new AddProductToCart(cartId, productId, dto.Price));
        }
    }

    [Route(template: "api/ShoppingCart/{cartId}/Product/{productId}")]
    public class RemoveProductFromCartController : ShoppingCartController<RemoveProductFromCart>
    {
        public RemoveProductFromCartController(ICommandHandler<RemoveProductFromCart> commandHandler)
            : base(commandHandler)
        {
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid cartId, Guid productId)
        {
            await commandHandler.HandleAsync(new RemoveProductFromCart(cartId, productId));
        }
    }

    [Route(template: "api/ShoppingCart/{cartId}/Product")]
    public class EmptyCartController : ShoppingCartController<EmptyCart>
    {
        public EmptyCartController(ICommandHandler<EmptyCart> commandHandler)
            : base(commandHandler)
        {
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid cartId)
        {
            await commandHandler.HandleAsync(new EmptyCart(cartId));
        }
    }

    [Route(template: "api/ShoppingCart/{cartId}/Checkout")]
    public class CheckoutController : ShoppingCartController<Checkout>
    {
        public CheckoutController(ICommandHandler<Checkout> commandHandler)
            : base(commandHandler)
        {
        }

        [HttpPut]
        public async Task PutAsync(Guid cartId)
        {
            await commandHandler.HandleAsync(new Checkout(cartId));
        }
    }
}
