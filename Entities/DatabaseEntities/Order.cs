using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DatabaseEntities
{
    public class Order
    {
        public int id { get; set; }
        public string product_qr { get; set; }
        public string user_mail { get; set; }
        public int company_id { get; set; }
        public string summary { get; set; }
        public int quantity { get; set; }
        
    }
  
}
