using System;
using System.Collections.Generic;
using System.Text;

using Entities;

namespace Services.Interfaces
{
    public interface IBill<T> where T : class
    {
        EntityResult<T> Pay(int company_id,double cost);

    }
}
