using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class EntityResult<T> where T : class
    {
        public bool Result { get; set; }
        public string ErrorText { get; set; }
        public int ErrorCode { get; set; }
        public T Object { get; set; }
        public IList<T> Objects { get; set; }
    }
}
