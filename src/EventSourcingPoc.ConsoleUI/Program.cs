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

                if (!app.ReadModelRepository.HasCart(customerId))
                {
                    app.Send(new CreateNewCart(cartId, customerId));
                    Console.WriteLine($"Create cart {cartId} for customer {customerId}.");
                }

                app.Send(new AddProductToCart(cartId, Guid.NewGuid(), 50));
                Console.WriteLine($"Add product to cart {cartId}.");

                app.Send(new AddProductToCart(cartId, Guid.NewGuid(), 10));
                Console.WriteLine($"Add product to cart {cartId}.");

                var cartModel = app.ReadModelRepository.GetCartById(cartId);
                Console.WriteLine($"Customer {cartModel.CustomerId} has cart {cartModel.Id} with {cartModel.Items.Count} items.");

                app.Send(new Checkout(cartId));
                Console.WriteLine($"Checkout cart {cartId}.");
                var hasCartBeenRemovedAfterCheckout = app.ReadModelRepository.HasCart(customerId);
                Console.WriteLine($"Customer {customerId} has cart: {hasCartBeenRemovedAfterCheckout}.");

                var orderId = cartId;
                app.Send(new ConfirmShippingAddress(orderId, new Address("My Home")));
                Console.WriteLine($"Confirm shipping order {orderId}.");

                app.Send(new PayForOrder(orderId));
                Console.WriteLine($"Pay for order {orderId}.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            finally
            {
                Console.WriteLine("Hit enter to exit");
                Console.ReadLine();
            }
        }
    }
}
