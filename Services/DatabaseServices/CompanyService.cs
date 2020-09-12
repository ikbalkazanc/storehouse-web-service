using System;
using System.Collections.Generic;
using System.Text;

using Services;
using Entities.DatabaseEntities;
using Entities;
using Services.Interfaces;
using Npgsql;
namespace Services.DatabaseServices
{
    public class CompanyService : PostgreConnection, IServices<Company>, IObjects<Company>, IBill<Company>
    {
        public EntityResult<Company> Delete(int id)
        {
            EntityResult<Company> result = new EntityResult<Company>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM autostorehouse.company WHERE id=@id_;", connectionOpen());
                command.Parameters.Add(new NpgsqlParameter("@id_", id));
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException Ex)
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
                result.ErrorCode = Ex.ErrorCode;
            }
            connectionClose();
            return result;
        }

        public EntityResult<Company> Get(int id)
        {
            EntityResult<Company> result = new EntityResult<Company>();
            Company _Company = new Company();
            result.Result = true;
            result.ErrorText = "Sucsess";

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM autostorehouse.company WHERE id=@id_;", connectionOpen());
                command.Parameters.Add(new NpgsqlParameter("@id_", id));
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    _Company.id = reader.GetInt32(0);
                    _Company.name = reader.GetString(1);
                    _Company.bill = reader.GetDouble(2);
                    _Company.phone = reader.GetString(4);
                    _Company.mail = reader.GetString(3);
                }
            }
            catch (NpgsqlException Ex)
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
                result.ErrorCode = Ex.ErrorCode;
            }
            connectionClose();
            result.Object = _Company;
            return result;
        }

        public EntityResult<Company> Insert(Company entities)
        {
            EntityResult<Company> result = new EntityResult<Company>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO autostorehouse.Company VALUES(@id_,@name_,@bill_,@phone_,@mail_)", connectionOpen());
                command.Parameters.AddWithValue("@id_", entities.id);
                command.Parameters.AddWithValue("@name_", entities.name);
                command.Parameters.AddWithValue("@bill_", entities.bill);
                command.Parameters.AddWithValue("@phone_", entities.phone);
                command.Parameters.AddWithValue("@mail_", entities.mail);

                command.ExecuteNonQuery();
            }
            catch (NpgsqlException Ex)
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
                result.ErrorCode = Ex.ErrorCode;
            }
            connectionClose();
            return result;
        }

        public EntityResult<Company> List()
        {
            EntityResult<Company> result = new EntityResult<Company>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                List<Company> CompanyList = new List<Company>();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM autostorehouse.Company", connectionOpen());
                NpgsqlDataReader reader = command.ExecuteReader();

                Company _Company = new Company();
                while (reader.Read())
                {
                    _Company.id = reader.GetInt32(0);
                    _Company.name = reader.GetString(1);
                    _Company.bill = reader.GetInt32(2);
                    _Company.phone = reader.GetString(4);
                    _Company.mail = reader.GetString(3);

                    CompanyList.Add(_Company);
                }
                result.Objects = CompanyList;
            }
            catch (NpgsqlException Ex)
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
                result.ErrorCode = Ex.ErrorCode;
            }
            connectionClose();
            return result;
        }

        public EntityResult<Company> Pay(int company_id, double cost)
        {
            EntityResult<Company> result = new EntityResult<Company>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE autostorehouse.company SET bill = bill + @cost_ WHERE id = @id_ ", connectionOpen());
                command.Parameters.Add(new NpgsqlParameter("@id_", company_id));
                command.Parameters.Add(new NpgsqlParameter("@cost_", cost));
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException Ex)
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
                result.ErrorCode = Ex.ErrorCode;
            }
            connectionClose();
            return result;
        }
    }
}
