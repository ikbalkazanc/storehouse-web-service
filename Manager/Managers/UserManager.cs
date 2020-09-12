using System;
using System.Collections.Generic;
using System.Text;
using Services.DatabaseServices;
using Entities.DatabaseEntities;
using Entities;
using System.Linq;

namespace Manager.Managers
{
    public class UserManager
    {
        private static UserManager _UserManager;
        private static object lockObject = new object();
        private UserManager() { }

        public static UserManager CreateAsUserManager()
        {
            lock (lockObject)
                return _UserManager ?? (_UserManager = new UserManager());  //_usermanager'in inheritancesi yoksa yeni oluşturuyor
        }


        UserService service = new UserService();

        public List<User> List()
        {
            return service.List().Objects.ToList();
        }
        public bool Add(User user)
        {

            EntityResult<User> result = service.Insert(user);
            return result.Result;
        }
        public User Get(string mail)
        {
            EntityResult<User> result = service.Get(mail);
            return result.Object;
        }
        public bool Delete(string mail)
        {
            EntityResult<User> result = service.Delete(mail);
            return result.Result;
        }
        public bool checkApiKey(string api_key,string mail,int company_id)
        {
            User user = this.Get(mail);
            if (!(user.api_key != api_key || user.company_id != company_id))
                return true;
            else
                return false;
        }
        public Info getInfo(string api_key, int company_id)
        {
            EntityResult<Info> result = service.GetInfo(api_key,company_id);
            return result.Object;
        }
        public EntityResult<User> resetPassword(string mail, string oldpassword,string newpassword)
        {
            EntityResult<User> result = new EntityResult<User>();
            User user = service.Get(mail).Object;
            if (user.mail != null)
            {
                if (user.password == oldpassword)
                {
                    result = service.ResetPassword(mail, newpassword);
                }
                else
                {
                    result.Result = false;
                    result.ErrorText = Error.errorText[0];
                }
            }
            else
            {
                result.Result = false;
                result.ErrorText = Error.errorText[6];
            }
            return result;
        }
       
    }
}
