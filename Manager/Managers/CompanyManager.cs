using Services.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Text;

using Entities;
using Entities.DatabaseEntities;
using System.Linq;

namespace Manager.Managers
{
    public class CompanyManager
    {
        private static CompanyManager _CompanyManager;
        private static object lockObject = new object();
        private CompanyManager() { }

        public static CompanyManager CreateAsCompanyManager()
        {
            lock (lockObject)
                return _CompanyManager ?? (_CompanyManager = new CompanyManager());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
        }

        CompanyService service = new CompanyService();
        

        public List<Company> List()
        {
            return service.List().Objects.ToList();
        }
        public bool Add(Company company)
        {
            EntityResult<Company> result = service.Insert(company);

            return result.Result;
        }
        public Company Get(int id)
        {
            EntityResult<Company> result = service.Get(id);
            return result.Object;
        }
        public bool Delete(int id)
        {
            EntityResult<Company> result = service.Delete(id);
            return result.Result;
        }
    }
}
