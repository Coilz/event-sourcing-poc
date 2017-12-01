using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingPoc.Domain.Store
{
    using Orders;
    using EventSourcing.Domain;
    using Messages;
    using Messages.Orders;
    using Messages.Store;

    public class ShoppingCart : Aggregate
    {
        private readonly Dictionary<Guid, decimal> _products = new Dictionary<Guid, decimal>();
        private bool _checkedOut;
        private Guid _customerId;

        public static ShoppingCart Create(Guid cartId, Guid customerId)
        {
            return new ShoppingCart(cartId, customerId);
        }

        public ShoppingCart() {}
        private ShoppingCart(Guid cartId, Guid customerId)
        {
            ApplyChanges(new CartCreated(cartId, customerId));
        }

        public void AddProduct(Guid productId, decimal price)
        {
            if (_checkedOut) throw new CartAlreadyCheckedOutException();

            if (!_products.ContainsKey(productId))
            {
                ApplyChanges(new ProductAddedToCart(id, productId, price));
            }
        }

        public void RemoveProduct(Guid productId)
        {
            if (_checkedOut) throw new CartAlreadyCheckedOutException();

            if (_products.ContainsKey(productId))
            {
                ApplyChanges(new ProductRemovedFromCart(id, productId));
            }
        }

        public void Empty()
        {
            if (_checkedOut) throw new CartAlreadyCheckedOutException();

            ApplyChanges(new CartEmptied(id));
        }

        public EventStream Checkout()
        {
            if (_products.Count == 0) throw new CannotCheckoutEmptyCartException();

            ApplyChanges(new CartCheckedOut(id));
            return Order.Create(id, _customerId, _products.Select(x => new OrderItem(x.Key, x.Value)));
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
            _products.Add(evt.ProductId, evt.Price);
        }

        private void Apply(ProductRemovedFromCart evt)
        {
            _products.Remove(evt.ProductId);
        }

        private void Apply(CartEmptied evt)
        {
            _products.Clear();
        }

        private void Apply(CartCheckedOut evt)
        {
            _checkedOut = true;
        }
    }
}
