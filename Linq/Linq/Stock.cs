using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linq
{
    public class Stock
    {
        public readonly List<string> notifications;
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
            string note = "";
            foreach(Product product in bought)
            {
                Product sold = products.Find(x => x.Name.Equals(product.Name));
                if (!sold.Equals(null))
                {
                    ThrowInvalidRequest(sold, product);
                    note = sold.GetWarningMessage(WarningMessage, product.Quantity);
                    sold.Quantity -= product.Quantity;                    
                }

                if (note != null)
                {
                    notifications.Add(note);
                }
            }            
        }      

        public static string WarningMessage(Product product, int sold)
        {
            string toReturn = $"Running out of {product.Name}. Quantity left is below ";
            int initialQty = product.Quantity;
            string limit = "";
            int rest = initialQty - sold;
            int[] thresholds = new int[] { 2, 5, 10 };
            for (int i = thresholds.Length - 1; i >= 0 ; i--)
            {
                if (initialQty >= thresholds[i])
                {
                    limit = ReturnThreshold(rest, thresholds, i);
                }
            }
            
            if (limit.Equals(null) || limit == "")
            {
                return null;
            }
            
            
            return toReturn + limit + "Products left: " + rest;
        }

        private Product TryGet(Product product)
        {
            return products.Find(x => x.Name.Equals(product.Name));            
        }

        private static string ReturnThreshold(int rest, int[] thresholds, int index)
        {
            for (int i = 0; i <= index; i++)
            {
                if (rest < thresholds[i])
                {
                    return thresholds[i].ToString() + ". ";
                }
            }           
            
                return null;            
        }

        private void ThrowInvalidRequest(Product inStock, Product requested)
        {
            if (requested.Quantity > inStock.Quantity)
            {
                throw new InvalidOperationException($"Invalid quantity. There are only {inStock.Quantity} products in stock.");
            }
        }
    }
}
