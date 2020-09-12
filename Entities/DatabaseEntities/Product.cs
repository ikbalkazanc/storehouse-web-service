using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DatabaseEntities
{
    public class Product
    {
        public string qr { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        
       
        public int quantity { get; set; }
        public int price { get; set; }
        public int sold { get; set; }
        public int company_id { get; set; }
        public string user_mail { get; set; }
    }
}
