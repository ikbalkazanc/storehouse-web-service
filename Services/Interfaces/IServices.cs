using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace Services
{
    public interface IServices<T> where T : class  
    {
        EntityResult<T> List();
        EntityResult<T> Insert(T entities);
        
    }
}
