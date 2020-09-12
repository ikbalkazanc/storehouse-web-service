using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Manager.Managers;
using Entities.DatabaseEntities;
using AutoStorehouse.Model;
using Entities;
using Newtonsoft.Json;

namespace AutoStorehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private UserManager userManager = UserManager.CreateAsUserManager();
        private RequestManager requestManager = RequestManager.CreateAsRequestManager();
        // GET: api/example/5
        [HttpGet("login/{mail}/{password}", Name = "Login")]
        public string Login(string mail,string password)
        {
            LoginModel model = new LoginModel();
            User user = userManager.Get(mail);
            if (user.mail != null)
            {
                if (password == user.password)
                {
                    model.admission = true;
                    model.AccountUser = user;

                    requestManager.Add(RequestTypes.login,user.company_id,user.api_key);
                }
                else
                {
                    model.admission = false;
                    model.error = Error.errorText[0];
                }
            }
            else
            {
                model.admission = false;
                model.error = Error.errorText[1];
            }
            return JsonConvert.SerializeObject(model);
        }
        [HttpPost("resetpassword/{mail}/{oldpass}/{newpass}")]
        public IActionResult ResetPassword(string mail,string oldpass,string newpass)
        {
            ErrorModel model = new ErrorModel();

            EntityResult<User> result = userManager.resetPassword(mail, oldpass, newpass);
            if (!result.Result)
            {
                model.error = result.ErrorText;
                model.admission = false;
            }
            return Ok(JsonConvert.SerializeObject(model));
        }
    }
}