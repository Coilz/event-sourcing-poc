using System;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingPoc.Shopping.Application;
using EventSourcingPoc.Shopping.Messages.Orders;
using EventSourcingPoc.Shopping.Messages.Shop;
using Microsoft.Extensions.Logging;

namespace EventSourcingPoc.Shopping.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ILogger logger = new Microsoft.Extensions.Logging.LoggerFactory().CreateLogger("Shopping.ConsoleUI");
                var app = Bootstrapper.Bootstrap(logger);
                var program = new Program(app);
                program.StartAsync().GetAwaiter().GetResult();;
            }
            catch (Exception e)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(e);
                Console.ForegroundColor = color;
            }

            finally
            {
                Console.WriteLine("Hit enter to exit");
                Console.ReadLine();
            }
        }

        private readonly Bootstrapper.PretendApplication _app;
        private readonly Guid _customerId = Guid.NewGuid();
        private readonly Guid _cartId = Guid.NewGuid();

        Program(Bootstrapper.PretendApplication app)
        {
            _app = app;
        }

        async Task StartAsync()
        {
            if (!await _app.ShoppingCartReadModelRepository.HasCartAsync(_customerId))
            {
                await CreateNewCart(_cartId, _customerId);
            }

            await AddProductToCart(_cartId, Guid.NewGuid(), 50);
            await AddProductToCart(_cartId, Guid.NewGuid(), 31);
            await AddProductToCart(_cartId, Guid.NewGuid(), 10);

            var productId = Guid.NewGuid();
            await AddProductToCart(_cartId, productId, 5);
            await AddProductToCart(_cartId, productId, 5);

            await ShowCart(_cartId);

            await Checkout(_cartId);
            await ShowCustomer(_customerId);
            await ShowOrder(_cartId);

            await ConfirmShippingAddress(_cartId);
            await ShowOrder(_cartId);

            await PayForOrder(_cartId);
            await ShowOrder(_cartId);
        }

        async Task CreateNewCart(Guid cartId, Guid customerId)
        {
            await _app.SendAsync(new CreateNewCart(cartId, customerId));
            Console.WriteLine($"Create cart {cartId} for customer {customerId}.");
        }

        async Task AddProductToCart(Guid cartId, Guid productId, decimal price)
        {
            await _app.SendAsync(new AddProductToCart(cartId, productId, price));
            Console.WriteLine($"Add product {productId} to cart {cartId}. Price {price}");
        }

        async Task ShowCart(Guid cartId)
        {
            var model = await _app.ShoppingCartReadModelRepository.GetAsync(cartId);
            Console.WriteLine($"Customer {model.CustomerId} has cart {model.Id} with {model.Items.Count()} items.");
        }

        async Task Checkout(Guid cartId)
        {
            await _app.SendAsync(new Checkout(cartId));
            Console.WriteLine($"Checkout cart {cartId}.");
        }

        async Task ShowCustomer(Guid customerId)
        {
            var hasCartBeenRemovedAfterCheckout = await _app.ShoppingCartReadModelRepository.HasCartAsync(customerId);
            Console.WriteLine($"Customer {customerId} has cart: {hasCartBeenRemovedAfterCheckout}.");
        }

        async Task ConfirmShippingAddress(Guid orderId)
        {
            await _app.SendAsync(new ConfirmShippingAddress(orderId, new Address("My Home")));
            Console.WriteLine($"Confirm shipping order {orderId}.");
        }

        async Task ShowOrder(Guid orderId)
        {
            var model = await _app.OrderReadModelRepository.GetAsync(orderId);
            Console.WriteLine($"Customer {model.CustomerId} has order {model.Id} with {model.Items.Count()} items.");
            Console.WriteLine($"Order {model.Id} with status {model.Status}.");
        }

        async Task PayForOrder(Guid orderId)
        {
            await _app.SendAsync(new PayForOrder(orderId));
            Console.WriteLine($"Pay for order {orderId}.");
        }
    }
}
