using System;
using System.Collections.Generic;
using EventSourcingPoc.Domain.Orders;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.Messages.Orders;
using EventSourcingPoc.Messages.Store;
using System.Linq;

namespace EventSourcingPoc.Domain.Store
{
    public class ShoppingCart : Aggregate
    {
        public ShoppingCart() {}

        protected override void RegisterAppliers()
        {
            this.RegisterApplier<CartCreated>(this.Apply);
            this.RegisterApplier<ProductAddedToCart>(this.Apply);
            this.RegisterApplier<ProductRemovedFromCart>(this.Apply);
            this.RegisterApplier<CartEmptied>(this.Apply);
            this.RegisterApplier<CartCheckedOut>(this.Apply);
        }

        private readonly Dictionary<Guid, Decimal> products = new Dictionary<Guid, decimal>();
        private bool checkedOut;
        private Guid customerId;

        private ShoppingCart(Guid cartId, Guid customerId)
        {
            this.ApplyChanges(new CartCreated(cartId, customerId));
        }

        public static ShoppingCart Create(Guid cartId, Guid customerId)
        {
            return new ShoppingCart(cartId, customerId);
        }

        private void Apply(CartCreated evt)
        {
            this.id = evt.CartId;
            this.customerId = evt.CustomerId;
        }

        public void AddProduct(Guid productId, decimal price)
        {
            if(checkedOut)
            {
                throw new CartAlreadyCheckedOutException();
            }
            if (!products.ContainsKey(productId))
            {
                this.ApplyChanges(new ProductAddedToCart(this.id, productId, price));
            }
        }

        private void Apply(ProductAddedToCart evt)
        {
            products.Add(evt.ProductId, evt.Price);
        }

        public void RemoveProduct(Guid productId)
        {
            if (checkedOut)
            {
                throw new CartAlreadyCheckedOutException();
            }
            if(products.ContainsKey(productId))
            {
                this.ApplyChanges(new ProductRemovedFromCart(this.id, productId));
            }
        }

        private void Apply(ProductRemovedFromCart evt)
        {
            products.Remove(evt.ProductId);
        }

        public void Empty()
        {
            if (checkedOut)
            {
                throw new CartAlreadyCheckedOutException();
            }
            this.ApplyChanges(new CartEmptied(this.id));
        }

        private void Apply(CartEmptied evt)
        {
            products.Clear();
        }

        public EventStream Checkout()
        {
            if(this.products.Count == 0)
            {
                throw new CannotCheckoutEmptyCartException();
            }
            this.ApplyChanges(new CartCheckedOut(this.id));
            return Order.Create(this.id, this.customerId, this.products.Select(x => new OrderItem(x.Key, x.Value)));
        }

        private void Apply(CartCheckedOut evt)
        {
            this.checkedOut = true;
        }
    }
}
