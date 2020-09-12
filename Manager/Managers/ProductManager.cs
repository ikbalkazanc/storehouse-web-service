using System;
using System.Collections.Generic;
using System.Text;

using Services.DatabaseServices;
using Entities;
using Entities.DatabaseEntities;
using System.Linq;
using KeyGenerator;

namespace Manager.Managers
{
    public class ProductManager
    {
        private static ProductManager _ProductManager;
        private static object lockObject = new object();
        private ProductManager() { }

        public static ProductManager CreateAsProductManager()
        {
            lock (lockObject)
                return _ProductManager ?? (_ProductManager = new ProductManager());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
        }

        ProductService service = new ProductService();
        RequestService requestservice = new RequestService();

        public EntityResult<Product> List(int company_id)
        {
            return service.List(company_id);
        }
        public EntityResult<Product> Add(Product Product)
        {
            Product.qr = KeyGenerator.KeyGenerator.GenerateQrCode();
            Product.sold = 0;
            return service.Insert(Product);
        }
        public EntityResult<Product> Get(string qr)
        {
            return service.Get(qr);
        }
        public bool Delete(string qr)
        {
            EntityResult<Product> result = service.Delete(qr);
            return result.Result;
        }
        public EntityResult<Product> Decrease(string qr, int amount)
        {
            
            return service.Decrease(qr, amount);
        }
        public EntityResult<Product> Increase(string qr, int amount)
        {
            return service.Increase(qr, amount);
        }
    }
}
