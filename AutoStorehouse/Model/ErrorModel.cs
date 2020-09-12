using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStorehouse.Model
{
    public class ErrorModel
    {
        public bool admission = true;

        public string error = "succes";

        public ErrorModel() { }
        public ErrorModel(bool admission,string error)
        {
            this.admission = admission;
            this.error = error;
        }
    }
}
