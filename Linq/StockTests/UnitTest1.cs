using System;
using Xunit;
using Linq;
using System.Collections.Generic;

namespace StockTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Product bread = new Product("bread", 13);
            Product soda = new Product("soda", 11);
            Product beer = new Product("beer", 10);
            List<Product> products = new List<Product>() { bread, soda, beer };
            Stock stock = new Stock(products);
            Product breadToBuy = new Product("bread", 4);
            Product sodaToBuy = new Product("soda", 10);
            Product beerToBuy = new Product("beer", 5);
            Product[] toBuy = new Product[] { breadToBuy, sodaToBuy, beerToBuy };
            List<string> messages = new List<string>();
            void Notify(Product product, int threshold)
            {
                string message = $"Running out of {product.Name}. Quantity left is below {threshold}." +
                    $" Products left : {product.Quantity}";
                Notification notification = new Notification(product, message);
                messages.Add(message);
            }
            stock.LowStockAlert = Notify;
            stock.SellProduct(toBuy);
            string firstResult = "Running out of bread. Quantity left is below 10. Products left: 9";
            string secondResult = "Running out of soda. Quantity left is below 2. Products left: 1";
            string thirdResult = "Running out of beer. Quantity left is below 10. Products left: 5";
            Assert.Equal(firstResult, messages[0]);
            Assert.Equal(secondResult, messages[1]);
            Assert.Equal(thirdResult, messages[2]);
        }
    }
}
