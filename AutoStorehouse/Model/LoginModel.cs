using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DatabaseEntities;

namespace AutoStorehouse.Model
{
    public class LoginModel : ErrorModel
    {
        public User AccountUser { get; set; }

    }
}
