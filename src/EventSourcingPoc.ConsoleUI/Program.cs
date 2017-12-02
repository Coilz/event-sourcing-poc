﻿using System;
using EventSourcingPoc.Application;
using EventSourcingPoc.Messages.Store;
using EventSourcingPoc.Messages.Orders;
using System.Linq;
using static EventSourcingPoc.Application.Bootstrapper;

namespace EventSourcingPoc.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var app = Bootstrapper.Bootstrap();
                var program = new Program(app);
                program.Start();
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

        private readonly PretendApplication _app;
        private readonly Guid _customerId = Guid.NewGuid();
        private readonly Guid _cartId = Guid.NewGuid();

        Program(PretendApplication app)
        {
            _app = app;
        }

        void Start()
        {
            if (!_app.ReadModelRepository.HasCart(_customerId))
            {
                CreateNewCart(_cartId, _customerId);
            }

            AddProductToCart(_cartId, Guid.NewGuid(), 50);
            AddProductToCart(_cartId, Guid.NewGuid(), 31);
            AddProductToCart(_cartId, Guid.NewGuid(), 10);

            var productId = Guid.NewGuid();
            AddProductToCart(_cartId, productId, 5);
            AddProductToCart(_cartId, productId, 5);

            ShowCart(_cartId);

            Checkout(_cartId);
            ShowCustomer(_customerId);

            ConfirmShippingAddress(_cartId);
            PayForOrder(_cartId);
        }

        void CreateNewCart(Guid cartId, Guid customerId)
        {
            _app.Send(new CreateNewCart(cartId, customerId));
            Console.WriteLine($"Create cart {cartId} for customer {customerId}.");
        }

        void AddProductToCart(Guid cartId, Guid productId, decimal price)
        {
            _app.Send(new AddProductToCart(cartId, productId, price));
            Console.WriteLine($"Add product {productId} to cart {cartId}. Price {price}");
        }

        void ShowCart(Guid cartId)
        {
            var cartModel = _app.ReadModelRepository.Get(cartId);
            Console.WriteLine($"Customer {cartModel.CustomerId} has cart {cartModel.Id} with {cartModel.Items.Count()} items.");
        }

        void Checkout(Guid cartId)
        {
            _app.Send(new Checkout(cartId));
            Console.WriteLine($"Checkout cart {cartId}.");
        }

        void ShowCustomer(Guid customerId)
        {
            var hasCartBeenRemovedAfterCheckout = _app.ReadModelRepository.HasCart(customerId);
            Console.WriteLine($"Customer {customerId} has cart: {hasCartBeenRemovedAfterCheckout}.");
        }

        void ConfirmShippingAddress(Guid orderId)
        {
            _app.Send(new ConfirmShippingAddress(orderId, new Address("My Home")));
            Console.WriteLine($"Confirm shipping order {orderId}.");
        }

        void PayForOrder(Guid orderId)
        {
            _app.Send(new PayForOrder(orderId));
            Console.WriteLine($"Pay for order {orderId}.");
        }
    }
}
