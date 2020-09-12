using System;
using System.Collections.Generic;
using System.Text;
using Entities;


namespace Services.Interfaces
{
    public interface IInfo<T> where T : class
    {
        EntityResult<T> GetInfo(string api_key,int company_id);
    }
}
