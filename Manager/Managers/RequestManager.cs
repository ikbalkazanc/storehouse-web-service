using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entities;
using Entities.DatabaseEntities;
using Services.DatabaseServices;
using KeyGenerator;
using System.Threading.Tasks;

namespace Manager.Managers
{
    public class RequestManager
    {
        private static RequestManager _RequestManager;
        private static object lockObject = new object();
        private RequestManager() { }

        public static RequestManager CreateAsRequestManager()
        {
            lock (lockObject)
                return _RequestManager ?? (_RequestManager = new RequestManager());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
        }


        RequestService service = new RequestService();
        CompanyService companyService = new CompanyService();

        public List<Request> List()
        {
            return service.List().Objects.ToList();
        }
        public void Add(RequestTypes type,int company_id,string api_key)
        {
            Request request = new Request(DateTime.Now,type,company_id,api_key);
            request.id = Int32.Parse(KeyGenerator.KeyGenerator.GenerateRequestId());
            Task Process()
            {
                return Task.Run(() =>
                {
                    companyService.Pay(request.company_id, request.calculateBill());
                    EntityResult<Request> result = service.Insert(request);
                });
            }

            Process();
        }
        public Request Get(int id)
        {
            EntityResult<Request> result = service.Get(id);
            return result.Object;
        }
        public bool Delete(int id)
        {
            EntityResult<Request> result = service.Delete(id);
            return result.Result;
        }
    }
}
