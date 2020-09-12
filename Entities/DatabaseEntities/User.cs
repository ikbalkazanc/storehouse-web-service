using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DatabaseEntities
{
    public class User
    {
        public string mail { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string api_key { get; set; }
        public int api_request { get; set; }
        public int company_id { get; set; }
        public string phone { get; set; }


        
    }
}
