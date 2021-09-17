using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linq
{
    public class Stock
    {
        private List<Product> products;

        public Stock(List<Product> Products)
        {
            this.products = Products;
        }

        public void Add(Product newProduct)
        {
            if (!products.Contains(newProduct))
            {
                products.Add(newProduct);
                return;
            }

            products[products.IndexOf(newProduct)].Quantity += newProduct.Quantity;
        }

        public void SellProduct(Product[] bought)
        {
            foreach(Product product in bought)
            {
                int productIndex = products.IndexOf(product);
                if (productIndex != -1)
                {
                    products[productIndex].Quantity -= product.SelectedQty;
                }
            }

            NotifyCriticalQty();
        }

        public Func<Product, bool> isCriticalQty = leftQty => leftQty.Quantity < 10;

        private void NotifyCriticalQty()
        {
            var fewProducts = products.Where(isCriticalQty);            
            var notification = fewProducts.GroupBy(product => GetKey(product));
            notification.ForEach(Notify);            
        }

        private Action<IGrouping<string, Product>> Notify = leftInStock =>
        {
            Console.WriteLine("Running out of products. Quantity of following products is {0}", leftInStock.Key);
            foreach (Product product in leftInStock)
            {
                Console.WriteLine("{0} - left pieces: {1}", product.Name, product.Quantity);
            }            
        };

        private string GetKey(Product product)
        {
            if (product.Quantity < 2)
            {
                return "below 2";
            }

            if (product.Quantity < 5)
            {
                return "below 5";
            }

            return "below 10";
        }
    }
}
