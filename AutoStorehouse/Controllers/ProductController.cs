using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Manager.Managers;
using Entities;
using Entities.DatabaseEntities;
using KeyGenerator;
using AutoStorehouse.Model;
using Newtonsoft.Json;

namespace AutoStorehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //
        //AP' KONTROL EDERKEN FRMADA KONTROLE DELEECK
        //
        private ProductManager productManager = ProductManager.CreateAsProductManager();
        private UserManager userManager = UserManager.CreateAsUserManager();
        private RequestManager requestManager = RequestManager.CreateAsRequestManager();
        [HttpGet("getproduct/{api}/{mail}/{company_id}/{qr}", Name = "GetProduct")]
        public string Get(string api,string mail,int company_id,string qr)
        {
            ProductModel model = new ProductModel();
            if (userManager.checkApiKey(api, mail, company_id))
            {
                EntityResult<Product> product = productManager.Get(qr.ToUpper());
                if (product.Result) {
                    model.Product = product.Object;
                    requestManager.Add(RequestTypes.get, company_id, api);
                }
                else
                {
                    model.admission = false;
                    model.error = Error.errorText[3];
                }               
            }
            else
            {
                model.admission = false;
                model.error = Error.errorText[2];
            }
            //ekleme
            /*
            api = "n28355pf8yoqmw59g9bczq9yl1y00512cdu3bp";
            Product product = new Product();
            product.id = Int32.Parse(KeyGenerator.KeyGenerator.GenerateDecimal());
            product.name = "Bilgisayar";
            product.quantity = 10;
            product.sumarry = "Dell";
            product.cost = 3500;
            product.sold = 0;
            product.qr = KeyGenerator.KeyGenerator.GenerateQrCode();
            product.image = "";
            product.company_id = 1;
            productManager.Add(product);

            Request request = new Request();
            request.type.Add(RequestTypes.insert);
            request.api_key = api;
            request.company_id = 1;
            request.date = DateTime.Now;
            requestManager.Add(request);
            */

            return JsonConvert.SerializeObject(model); 
        }
        [HttpGet("getlist/{api}/{mail}/{company_id}")]
        public IActionResult List(string api,string mail,int company_id)
        {
            ProductList list = new ProductList();
            if (userManager.checkApiKey(api, mail, company_id))
            {
                EntityResult<Product> result = productManager.List(company_id);
                if (result.Result == true)
                {
                   
                    list.Products = result.Objects.ToList();
                    requestManager.Add(RequestTypes.list, company_id, api);
                }
                else
                {
                    list.admission = false;
                    list.error = Error.errorText[5];
                }
            }
            else
            {
                list.admission = false;
                list.error = Error.errorText[2];
            }
            return Ok(JsonConvert.SerializeObject(list));
        }
        [HttpGet("increase/{api}/{mail}/{company_id}/{qr}/{amount}", Name = "IncreaseProduct")]
        public string Increase(string api, string mail, int company_id, string qr,int amount)
        {
            ErrorModel model = new ErrorModel();
            if (userManager.checkApiKey(api, mail, company_id))
            {
                productManager.Increase(qr.ToUpper(),amount);
                requestManager.Add(RequestTypes.update, company_id, api);

            }
            else
            {
                model.admission = false;
                model.error = Error.errorText[2];
            }
           


            return JsonConvert.SerializeObject(model);
        }
        [HttpGet("decrease/{api}/{mail}/{company_id}/{qr}/{amount}", Name = "DecreaseProduct")]
        public string Decrease(string api, string mail, int company_id, string qr, int amount)
        {
            ErrorModel model = new ErrorModel();
            if (userManager.checkApiKey(api, mail, company_id))
            {
                EntityResult<Product> result =  productManager.Decrease(qr.ToUpper(),amount);
                if (result.Result)
                {
                    requestManager.Add(RequestTypes.update, company_id, api);
                }
                else
                {
                    if(result.ErrorCode == 9999)
                    {
                        model.error = Error.errorText[4];
                        model.admission = false;
                    }
                    else
                    {
                        model.error = Error.errorText[5];
                        model.admission = false;
                    }
                }
                

            }
            else
            {
                model.admission = false;
                model.error = Error.errorText[2];
            }



            return JsonConvert.SerializeObject(model);
        }

        [HttpDelete("delete/{api}/{company_id}/{qr}")]
        public IActionResult Delete(string api,int company_id,string qr)
        {
            ErrorModel model = new ErrorModel();
            if (productManager.Delete(qr))
            {
                requestManager.Add(RequestTypes.delete, company_id, api);
            }
            else
            {
                model.admission = false;
                model.error = Error.errorText[5];
            }
            return Ok(JsonConvert.SerializeObject(model));
        }
    
        [HttpPost("insert/{api}/{mail}/{company_id}")]
        public IActionResult Add([FromBody]Product product,string api,string mail,int company_id)
        {
            ErrorModel model = new ErrorModel();
            if (userManager.checkApiKey(api, mail, company_id))
            {
                EntityResult<Product> result = productManager.Add(product);
                if (result.Result)
                {
                    requestManager.Add(RequestTypes.insert, company_id, api);
                }
                else
                {                
                        model.error = Error.errorText[5];
                        model.admission = false;                  
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