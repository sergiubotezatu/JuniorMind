using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linq
{
    public class Stock
    {
        public readonly List<Notification> notifications;
        private readonly List<Product> products;
        private readonly CallBack StockAlert;
        
        public Stock(IEnumerable<Product> Products)
        {
            this.products = (List<Product>)Products;
            StockAlert = new CallBack();
        }

        public void Add(Product newProduct)
        {            
            if (!products.Exists(x => x.Name.Equals(newProduct.Name)))
            {
                products.Add(newProduct);
                return;
            }

            TryGet(newProduct).Quantity += newProduct.Quantity;
        }

        public void BulkAdd(IEnumerable<Product> products)
        {
            foreach (Product item in products)
            {
                Add(item);
            }
        }

        public void SellProduct(Product[] bought)
        {
            foreach (Product product in bought)
            {
                Product sold = products.Find(x => x.Name.Equals(product.Name));
                if (!sold.Equals(null))
                {
                    ThrowInvalidRequest(sold, product);
                    int beforeSale = product.Quantity;
                    product.Quantity -= sold.Quantity;
                    if (ExceededThreshold(beforeSale, product.Quantity))
                    {
                        int exceeded = GetExceededLimit(product.Quantity);
                        StockAlert.critical = product;
                        notifications.Add(StockAlert.GetAlert(Notify, exceeded));
                    }
                }
            }            
        }      

        public Notification Notify(Product product, int threshold)
        {
            string message = $"Running out of {product.Name}. Quantity left is below {threshold}." +
                $" Products left : {product.Quantity}";
            return new Notification(product, message);
        }

        private Product TryGet(Product product)
        {
            return products.Find(x => x.Name.Equals(product.Name));            
        }

        private void ThrowInvalidRequest(Product inStock, Product requested)
        {
            if (requested.Quantity > inStock.Quantity)
            {
                throw new InvalidOperationException($"Invalid quantity. There are only {inStock.Quantity} products in stock.");
            }
        }

        private bool ExceededThreshold(int initialQty, int currentQty)
        {
            int[] thresholds = new int[] { 10, 5, 2 };
            for (int i = 0; i < thresholds.Length; i++)
            {
                if (initialQty >= thresholds[i])
                {
                    return currentQty < thresholds[i];
                }
            }

            return false;
        }

        private int GetExceededLimit(int quantity)
        {
            if (quantity < 2)
            {
                return 2;
            }

            if (quantity < 5)
            {
                return 5;
            }

            return 10;
        }
    }
}
