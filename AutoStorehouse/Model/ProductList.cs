using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Entities.DatabaseEntities;

namespace AutoStorehouse.Model
{
    public class ProductList : ErrorModel
    {
        public List<Product> Products = new List<Product>();

        public ProductList() { }
        public ProductList(List<Product> products, ErrorModel error )
        {
            this.Products = products;
            this.error = error.error;
            this.admission = error.admission;

        }
    }
}
