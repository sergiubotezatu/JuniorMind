using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linq
{
    public class Stock
    {
        private readonly List<Product> products;

        public Stock(IEnumerable<Product> Products)
        {
            this.products = (List<Product>)Products;
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
                    int exceeded = ExceededThreshold(beforeSale, product.Quantity);
                    if (exceeded != 0 && !LowStockAlert.Equals(null))
                    {
                        LowStockAlert(product, exceeded);
                    }
                }
            }
        }

        public Action<Product, int> LowStockAlert;

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

        private int ExceededThreshold(int initialQty, int currentQty)
        {
            List<int> thresholds = new List<int>() { 2, 5, 10 };
            int exceeded = thresholds.Find(x => initialQty >= x && currentQty < x);
            return exceeded;
        }
    }
}
