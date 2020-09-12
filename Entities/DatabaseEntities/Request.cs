using System;
using System.Collections.Generic;
using System.Text;


namespace Entities.DatabaseEntities
{
    public class Request
    {
       public Request()
        {

        }
        public Request(DateTime date_,RequestTypes type_,int company_id_,string api_key_) {
            date = date_;
            type.Add(type_);
            company_id = company_id_;
            api_key = api_key_;
        }
        public Request(DateTime date_, List<RequestTypes> type_, int company_id_, string api_key_)
        {
            date = date_;
            type = type_;
            company_id = company_id_;
            api_key = api_key_;
        }
        public int id { get; set; }
        public DateTime date {get;set;}
        public List<RequestTypes> type = new List<RequestTypes>();
        
        public int company_id { get; set; }
        public string api_key { get; set; }
        public static RequestTypes convert(string RequestTypeString)
        {
            
            if (RequestTypeString == "insert")
                return RequestTypes.insert;
            else if (RequestTypeString == "list")
                return RequestTypes.list;
            else if (RequestTypeString == "get")
                return RequestTypes.get;
            else if (RequestTypeString == "delete")
                return RequestTypes.delete;
            else if (RequestTypeString == "login")
                return RequestTypes.login;
            else if (RequestTypeString == "register")
                return RequestTypes.register;
            else if (RequestTypeString == "info")
                return RequestTypes.info;
            else if (RequestTypeString == "update")
                return RequestTypes.update ;
            else
                return RequestTypes.none;

        }
        public double calculateBill()
        {
            double cost = 0;
            foreach(var item in type)
            {
                cost = cost + (int)item; 
            }
            cost = cost / 100;
            return cost;
        }
    }
    public enum RequestTypes
    {
        insert = 35,
        list = 25,
        get = 11,
        delete = 10,
        login = 20,
        register = 400,
        info = 5,
        update = 10,
        none
    }
   
}
