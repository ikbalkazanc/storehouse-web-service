using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Entities;
using Entities.DatabaseEntities;

namespace AutoStorehouse.Model
{
    public class ProductModel : ErrorModel
    {
        public Product Product = new Product();
    }
}
