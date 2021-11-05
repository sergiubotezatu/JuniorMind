using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    struct Product2
    {
        public string Name;
        public int Quantity;
    }

    class ProductsQtyGroup
    {
        public IEnumerable<Product2> SortQuantityBased(List<Product2> first, List<Product2> second)
        {
            return first.GroupJoin(second, firstProds => firstProds.Name,
                secondProds => secondProds.Name,
                (product, productGroup) =>
                new Product2 { Name = product.Name, Quantity = productGroup.Select(x => x.Quantity).Sum() });
        }
    }
}
