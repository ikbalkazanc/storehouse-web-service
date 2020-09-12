using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Entities;
using Entities.DatabaseEntities;
using Manager.Managers;
using Newtonsoft;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using AutoStorehouse.Model;

namespace AutoStorehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderManager orderManager = OrderManager.CreateAsOrderManager();
        private UserManager userManager = UserManager.CreateAsUserManager();
        private RequestManager requestManager = RequestManager.CreateAsRequestManager();
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("insert/{api}")]
        public IActionResult Post(string api,[FromBody] Order order)
        {
            ErrorModel model = new ErrorModel();
            
            
            if (userManager.checkApiKey(api, order.user_mail,order.company_id))
            {
                EntityResult<Order> result =  orderManager.Add(order);
                if (result.Result)
                {
                    requestManager.Add(RequestTypes.insert, order.company_id, api);
       
                }
                else
                {
                    model.admission = false;
                    model.error = Error.errorText[5];
                    
                }
               
            }
            else
            {
                model.admission = false;
                model.error = Error.errorText[2];
                
            }
            
            return Ok(JsonConvert.SerializeObject(model));
        }


    }
    
}