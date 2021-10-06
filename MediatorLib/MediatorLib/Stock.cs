using System;
using System.Collections.Generic;
using System.Text;

namespace MediatorLib
{
    public class Stock : ISubscriber
    {
        public List<Product> Products;
        Mediator mediator;

        public Stock(List<Product> products, Mediator mediator)
        {
            this.mediator = mediator;
            Products = products;
        }

        public void Add(Product newProduct)
        {
            if (!Products.Exists(x => x.Name.Equals(newProduct.Name)))
            {
                Products.Add(newProduct);
                newProduct.SubscribeTo(mediator);
                return;
            }

            TryGet(newProduct).Quantity += newProduct.Quantity;
        }

        public void SellProduct(Product[] bought)
        {
            foreach (Product product in bought)
            {
                Product sold = Products.Find(x => x.Name.Equals(product.Name));
                if (!sold.Equals(null))
                {
                    ThrowInvalidRequest(sold, product);
                    product.SellProduct(sold.Quantity);
                    this.mediator.Notify(product);
                }
            }
        }

        private Product TryGet(Product product)
        {
            return Products.Find(x => x.Name.Equals(product.Name));
        }

        private void ThrowInvalidRequest(Product inStock, Product requested)
        {
            if (requested.Quantity > inStock.Quantity)
            {
                throw new InvalidOperationException($"Invalid quantity. There are only {inStock.Quantity} products in stock.");
            }
        }

        public bool wasTriggered(out Notification n)
        {
            n = null;
            return this.mediator.NotificationsStatus();
        }
    }
}
