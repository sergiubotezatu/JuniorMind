using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diverse
{
    public class Product
    {
        public string Name { get; set; }
        public ICollection<Feature> Features { get; set; }
    }

    public class Feature
    {
        public int Id { get; set; }
    }

    class ProductsQuery
    {
        private ICollection<Product> products;
        private ICollection<Feature> features;

        public IEnumerable<Product> GetLeastOneFeature()
        {
            return products.Where(x => x.Features.Any(feat => features.Contains(feat)));
        }

        public IEnumerable<Product> GetProductsAllFeatures()
        {
            return products.Where(x => x.Features.All(feat => features.Contains(feat)));
        }

        public IEnumerable<Product> GetProductsWithNoFeatures()
        {
            return products.Except(GetLeastOneFeature());
        }
    }
}
