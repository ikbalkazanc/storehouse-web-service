﻿using Entities.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStorehouse.Model
{
    public class CompanyModel : ErrorModel
    {
        
        public Company AccountCompany { get; set; }  
    }
}
