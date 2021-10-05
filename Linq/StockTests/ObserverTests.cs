using System;
using System.Collections.Generic;
using System.Text;
using Linq;
using Xunit;

namespace StockTests
{
    public class ObserverTests
    {
        [Fact]
        public void Test1()
        {
            ObservedProduct bread = new ObservedProduct("bread", 13);
            ObservedProduct soda = new ObservedProduct("soda", 11);
            ObservedProduct beer = new ObservedProduct("beer", 10);
            List<ObservedProduct> products = new List<ObservedProduct>() { bread, soda, beer };
            ObservedStock stock = new ObservedStock(products);
            ObservedProduct breadToBuy = new ObservedProduct("bread", 4);
            ObservedProduct sodaToBuy = new ObservedProduct("soda", 10);
            ObservedProduct beerToBuy = new ObservedProduct("beer", 5);
            ObservedProduct[] toBuy = new ObservedProduct[] { breadToBuy, sodaToBuy, beerToBuy };
            List<string> messages = new List<string>();
            LowStockAlert lowStock = new LowStockAlert(2, 5, 10);
            void GetMessages(LowStockAlert alert)
            {
                if (alert.WasTriggered)
                {
                    messages.Add(alert.notification.message);
                }
            }
            stock.CollectAlerts = GetMessages;
            stock.SellProduct(toBuy);
            string firstResult = "Running out of bread. Quantity left is below 10. ObservedProducts left: 9";
            string secondResult = "Running out of soda. Quantity left is below 2. ObservedProducts left: 1";
            string thirdResult = "Running out of beer. Quantity left is below 10. ObservedProducts left: 5";
            Assert.Equal(firstResult, messages[0]);
            Assert.Equal(secondResult, messages[1]);
            Assert.Equal(thirdResult, messages[2]);
        }
    }
}
