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
            stock.SellProduct(toBuy);
            string firstResult = "Running out of {product.Name}. Quantity left is below 10. Products left: 9";
            Assert.True(firstResult.Equals(stock.notifications[0]));
        }
    }
}
