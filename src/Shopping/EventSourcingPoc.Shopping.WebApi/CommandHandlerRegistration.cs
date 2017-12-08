using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Shopping.CommandProcessing;
using EventSourcingPoc.Shopping.Messages.Orders;
using EventSourcingPoc.Shopping.Messages.Shop;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcingPoc.Shopping.WebApi
{
    static class CommandHandlerRegistration
    {
        public static void RegisterCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<PayForOrder>, OrderCommandHandler>();
            services.AddScoped<ICommandHandler<ConfirmShippingAddress>, OrderCommandHandler>();
            services.AddScoped<ICommandHandler<CompleteOrder>, OrderCommandHandler>();

            services.AddScoped<ICommandHandler<CreateNewCart>, ShoppingCartCommandHandler>();
            services.AddScoped<ICommandHandler<AddProductToCart>, ShoppingCartCommandHandler>();
            services.AddScoped<ICommandHandler<RemoveProductFromCart>, ShoppingCartCommandHandler>();
            services.AddScoped<ICommandHandler<EmptyCart>, ShoppingCartCommandHandler>();
            services.AddScoped<ICommandHandler<Checkout>, ShoppingCartCommandHandler>();
        }
    }
}
