using Entities.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStorehouse.Model
{
    public class CompanyModel : ErrorModel
    {
        int t = 0;
        public Company AccountCompany { get; set; }  
    }
}
