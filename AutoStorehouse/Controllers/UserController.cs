using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using Manager.Managers;
using Entities.DatabaseEntities;
using KeyGenerator;
using AutoStorehouse.Model;
using Entities;

namespace AutoStorehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager userManager = UserManager.CreateAsUserManager();
        private RequestManager requestManager = RequestManager.CreateAsRequestManager();
        private KeyGenerator.KeyGenerator generator = new KeyGenerator.KeyGenerator(); 
        // GET: api/user
        [HttpGet]
        public IEnumerable<User> Get()
        {
            

            List<User> users = userManager.List();
            //string json = JsonConvert.SerializeObject(users);

            return users;
        }
        // GET: api/example/5
        [HttpGet("getinfo/{api}/{mail}/{company_id}", Name = "GetInfo")]
        public string GetInfo(string api,string mail,int company_id)
        {
            InfoModel model = new InfoModel();
            if (userManager.checkApiKey(api, mail,company_id)) {
                model.AccountInfo = userManager.getInfo(api, company_id);
                requestManager.Add(RequestTypes.info,company_id,api);
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