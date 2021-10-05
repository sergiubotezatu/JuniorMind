using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
    public class ObservedStock
    {
        private readonly List<ObservedProduct> products;

        public ObservedStock(IEnumerable<IProduct> Products)
        {
            this.products = (List<ObservedProduct>)Products;
        }

        public LowStockAlert LowStockAlert { private get; set; }

        public void Add(ObservedProduct newProduct)
        {
            if (!products.Exists(x => x.Name.Equals(newProduct.Name)))
            {
                products.Add(newProduct);
                return;
            }

            TryGet(newProduct).Quantity += newProduct.Quantity;
        }

        public void BulkAdd(IEnumerable<ObservedProduct> products)
        {
            foreach (ObservedProduct item in products)
            {
                Add(item);
            }
        }

        public void SellProduct(ObservedProduct[] bought)
        {
            foreach (ObservedProduct product in bought)
            {
                ObservedProduct sold = products.Find(x => x.Name.Equals(product.Name));
                if (!sold.Equals(null))
                {
                    ThrowInvalidRequest(sold, product);
                    int possibleExceed = GetPossibleExceeding(product.Quantity);
                    product.Quantity -= sold.Quantity;
                    if (possibleExceed != 0 && !LowStockAlert.Equals(null))
                    {
                        product.AttachAlert(LowStockAlert);
                        product.Notify();
                        if (!CollectAlerts.Equals(null))
                        {
                            CollectAlerts(this.LowStockAlert);
                        }
                    }
                }
            }
        }

       public Action<LowStockAlert> CollectAlerts;
       private ObservedProduct TryGet(ObservedProduct product)
        {
            return products.Find(x => x.Name.Equals(product.Name));
        }

        private void ThrowInvalidRequest(ObservedProduct inStock, ObservedProduct requested)
        {
            if (requested.Quantity > inStock.Quantity)
            {
                throw new InvalidOperationException($"Invalid quantity. There are only {inStock.Quantity} products in stock.");
            }
        }

        private int GetPossibleExceeding(int initialQty)
        {
            List<int> thresholds = new List<int>() { 2, 5, 10 };
            int exceeded = thresholds.Find(x => initialQty >= x);
            return exceeded;

        }
    }
}
