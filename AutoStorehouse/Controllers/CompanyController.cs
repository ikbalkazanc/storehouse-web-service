using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Entities.DatabaseEntities;
using AutoStorehouse.Model;
using Entities;
using Manager.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AutoStorehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private UserManager userManager = UserManager.CreateAsUserManager();
        private CompanyManager companyManager = CompanyManager.CreateAsCompanyManager();
        private RequestManager requestManager = RequestManager.CreateAsRequestManager();

        [HttpGet("get/{api}/{mail}/{company_id}", Name = "GetCompany")]
        public string Get(string api, string mail, int company_id)
        {
            CompanyModel model = new CompanyModel();

            if (userManager.checkApiKey(api, mail,company_id)) {
                model.AccountCompany = companyManager.Get(company_id);
                requestManager.Add(RequestTypes.get,company_id,api);
            }
            else
            {
                model.admission = false;
                model.error = Error.errorText[2];
            }
            return JsonConvert.SerializeObject(model);
        }
        [HttpGet("getprofile/{api}/{mail}/{company_id}", Name = "GetCompanyProfile")]
        public string GetProfile(string api, string mail, int company_id)
        {
            CompanyModel model = new CompanyModel();

            if (userManager.checkApiKey(api, mail,company_id))
            {
                model.AccountCompany = companyManager.Get(company_id);
                model.AccountCompany.bill = 0;

                requestManager.Add(RequestTypes.get,company_id,api);
            }
            else
            {
                model.admission = false;
                model.error = Error.errorText[2];
            }
            return JsonConvert.SerializeObject(model);
        }

    }
}