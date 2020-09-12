using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IObjects<T> where T : class
    {
        EntityResult<T> Delete(int id);
        EntityResult<T> Get(int id);
    }
}
