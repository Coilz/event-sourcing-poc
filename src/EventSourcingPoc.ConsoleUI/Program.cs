using System;
using EventSourcingPoc.Application;
using EventSourcingPoc.Messages.Store;
using EventSourcingPoc.Messages.Orders;

namespace EventSourcingPoc.ConsoleUI
{
    class Program
    {
        private static readonly Guid customerId = Guid.NewGuid();
        private static readonly Guid cartId = Guid.NewGuid();

        static void Main(string[] args)
        {
            try
            {
                var app = Bootstrapper.Bootstrap();

                if(!app.MongoDb.HasCart(customerId))
                {
                    app.Send(new CreateNewCart(cartId, customerId));
                    Console.WriteLine($"Create cart {cartId} for customer {customerId}.");
                }

                app.Send(new AddProductToCart(cartId, Guid.NewGuid(), 50));
                Console.WriteLine($"Add product to cart {cartId}.");

                app.Send(new AddProductToCart(cartId, Guid.NewGuid(), 10));
                Console.WriteLine($"Add product to cart {cartId}.");

                var cartModel = app.MongoDb.GetCartById(cartId);

                app.Send(new Checkout(cartId));
                Console.WriteLine($"Checkout cart {cartId}.");
                var hasCartBeenRemovedAfterCheckout = app.MongoDb.HasCart(customerId);

                var orderId = cartId;
                app.Send(new ConfirmShippingAddress(orderId, new Address("My Home")));
                Console.WriteLine($"Confirm shipping order {orderId}.");

                app.Send(new PayForOrder(orderId));
                Console.WriteLine($"Pay for order {orderId}.");


            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
