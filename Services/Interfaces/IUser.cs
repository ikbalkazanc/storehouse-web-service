using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IUser<T> where T : class
    {
        EntityResult<T> Delete(string mail);
        EntityResult<T> Get(string mail);
    }
}
