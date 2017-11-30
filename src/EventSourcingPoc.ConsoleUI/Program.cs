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
                }

                app.Send(new AddProductToCart(cartId, Guid.NewGuid(), 50));

                app.Send(new AddProductToCart(cartId, Guid.NewGuid(), 10));

                var cartModel = app.MongoDb.GetCartById(cartId);

                app.Send(new Checkout(cartId));
                var hasCartBeenRemovedAfterCheckout = app.MongoDb.HasCart(customerId);

                var orderId = cartId;
                app.Send(new ConfirmShippingAddress(orderId, new Address("My Home")));
                app.Send(new PayForOrder(orderId));


            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
