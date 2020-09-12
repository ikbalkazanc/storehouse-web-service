using System;
using System.Collections.Generic;
using System.Text;

using Entities.DatabaseEntities;
using Entities;
using Services.DatabaseServices;
using System.Linq;

namespace Manager.Managers
{
    public class OrderManager
    {
        private static OrderManager _OrderManager;
        private static object lockObject = new object();
        private OrderManager() { }

        public static OrderManager CreateAsOrderManager()
        {
            lock (lockObject)
                return _OrderManager ?? (_OrderManager = new OrderManager());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
        }

        OrderService service = new OrderService();
        

        public List<Order> List(int company_id)
        {
            return service.List(company_id).Objects.ToList();
        }
        public EntityResult<Order> Add(Order order)
        {
            order.id = Int32.Parse(KeyGenerator.KeyGenerator.GenerateDecimal());
            return service.Insert(order);
        }
        public Order Get(int id)
        {
            EntityResult<Order> result = service.Get(id);
            return result.Object;
        }
        public bool Delete(int id)
        {
            EntityResult<Order> result = service.Delete(id);
            return result.Result;
        }
    }
}
