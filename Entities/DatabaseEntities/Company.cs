using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DatabaseEntities
{
    public class Company
    {
        public int id = 0;
        public string name { get; set; }
        public double bill { get; set; }
        public string phone { get; set; }
        public string mail { get; set; }
    }
}
