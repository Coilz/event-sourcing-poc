﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingPoc.Domain.Store
{
    using EventSourcing.Domain;
    using Messages;
    using Messages.Store;

    public class ShoppingCart : AggregateRoot
    {
        public static ShoppingCart Create(Guid cartId, Guid customerId)
        {
            return new ShoppingCart(cartId, customerId);
        }

        private readonly List<ShoppingCartItem> _shoppingCartItems = new List<ShoppingCartItem>();
        private bool _checkedOut;
        private Guid _customerId;

        public ShoppingCart() {}
        private ShoppingCart(Guid cartId, Guid customerId)
        {
            ApplyChanges(new CartCreated(cartId, customerId));
        }

        public Guid CustomerId => _customerId;
        public IEnumerable<ShoppingCartItem> ShoppingCartItems => _shoppingCartItems.AsReadOnly();

        public void AddProduct(Guid productId, decimal price)
        {
            if (_checkedOut) throw new CartAlreadyCheckedOutException();

            ApplyChanges(new ProductAddedToCart(id, productId, price));
        }

        public void RemoveProduct(Guid productId)
        {
            if (_checkedOut) throw new CartAlreadyCheckedOutException();

            ApplyChanges(new ProductRemovedFromCart(id, productId));
        }

        public void Empty()
        {
            if (_checkedOut) throw new CartAlreadyCheckedOutException();

            ApplyChanges(new CartEmptied(id));
        }

        public void Checkout()
        {
            if (_shoppingCartItems.Count == 0) throw new CannotCheckoutEmptyCartException();

            ApplyChanges(new CartCheckedOut(id));
        }

        protected override IEnumerable<KeyValuePair<Type, Action<IEvent>>> EventAppliers
        {
            get
            {
                yield return CreateApplier<CartCreated>(Apply);
                yield return CreateApplier<ProductAddedToCart>(Apply);
                yield return CreateApplier<ProductRemovedFromCart>(Apply);
                yield return CreateApplier<CartEmptied>(Apply);
                yield return CreateApplier<CartCheckedOut>(Apply);
            }
        }

        private void Apply(CartCreated evt)
        {
            id = evt.CartId;
            _customerId = evt.CustomerId;
        }

        private void Apply(ProductAddedToCart evt)
        {
            var item = _shoppingCartItems.SingleOrDefault(sci => sci.ProductId == evt.ProductId);
            if (item == null)
                _shoppingCartItems.Add(
                    new ShoppingCartItem(evt.ProductId, evt.Price, 1));
            else
                item.AddItems(1);
        }

        private void Apply(ProductRemovedFromCart evt)
        {
            var item = _shoppingCartItems.SingleOrDefault(sci => sci.ProductId == evt.ProductId);

            if (item == null) return;

            if (item.Quantity > 1)
                item.RemoveItems(1);
            else
                _shoppingCartItems.Remove(item);
        }

        private void Apply(CartEmptied evt)
        {
            _shoppingCartItems.Clear();
        }

        private void Apply(CartCheckedOut evt)
        {
            _checkedOut = true;
        }
    }
}
