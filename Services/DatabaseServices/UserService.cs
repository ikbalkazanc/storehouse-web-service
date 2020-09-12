using System;
using System.Collections.Generic;
using System.Text;

using Entities;
using Entities.DatabaseEntities;
using Services.Interfaces;

using Npgsql;

namespace Services.DatabaseServices
{
    public class UserService : PostgreConnection,IUser<User> ,IServices<User> , IInfo<Info>
    {
        public EntityResult<User> Delete(string mail)
        {
            EntityResult<User> result = new EntityResult<User>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM autostorehouse.user WHERE mail=@mail_;", connectionOpen());
                command.Parameters.Add(new NpgsqlParameter("@mail_", mail));
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

        public EntityResult<User> Get(string mail)
        {
            EntityResult<User> result = new EntityResult<User>();
            User _user = new User();

            result.Result = true;
            result.ErrorText = "Sucsess";

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM autostorehouse.user WHERE mail=@mail_;", connectionOpen()); 
                command.Parameters.Add(new NpgsqlParameter("@mail_", mail));
                command.ExecuteNonQuery(); 
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    _user.mail = reader.GetString(0);
                    _user.password = reader.GetString(1);
                    _user.name = reader.GetString(2);
                    _user.surname = reader.GetString(3);
                    _user.api_key = reader.GetString(4);
                    _user.api_request = reader.GetInt32(5);
                    _user.company_id = reader.GetInt32(6);
                    _user.phone = reader.GetString(7);
                }
            }
            catch (NpgsqlException Ex)
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
                result.ErrorCode = Ex.ErrorCode;
            }
            connectionClose();
            result.Object = _user;
            return result;
        }

        public EntityResult<Info> GetInfo(string api_key, int company_id)
        {
            EntityResult<Info> result = new EntityResult<Info>();
            Info _Info = new Info();

            result.Result = true;
            result.ErrorText = "Sucsess";

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM autostorehouse.request WHERE api_key=@api_key_;", connectionOpen());
                command.Parameters.Add(new NpgsqlParameter("@api_key_", api_key));
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {

                    _Info.userApiRequest = reader.GetInt32(0);
                }
                reader.Close();
                command.CommandText = "SELECT COUNT(*) FROM autostorehouse.product WHERE company_id=@company_id_";
                command.Parameters.Clear();
                command.Parameters.Add(new NpgsqlParameter("@company_id_", company_id));
                reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    _Info.productNumber = reader.GetInt32(0);
                }
                reader.Close();
                command.CommandText = "SELECT SUM(quantity) FROM autostorehouse.product WHERE company_id=@company_id_";
                command.Parameters.Clear();
                command.Parameters.Add(new NpgsqlParameter("@company_id_", company_id));
                reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    _Info.totalProductNumber = reader.GetInt32(0);
                }
            }
            catch (NpgsqlException Ex)
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
                result.ErrorCode = Ex.ErrorCode;
            }
            connectionClose();
            result.Object = _Info;
            return result;
        }

        public EntityResult<User> Insert(User entity)
        {
            EntityResult<User> result = new EntityResult<User>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO autostorehouse.user VALUES(@mail_,@pass_,@name_,@surname_,@api_key_,@api_request_,@company_id_,@phone_)", connectionOpen());

                command.Parameters.AddWithValue("@mail_", entity.mail);
                command.Parameters.AddWithValue("@pass_", entity.password);
                command.Parameters.AddWithValue("@name_", entity.name);
                command.Parameters.AddWithValue("@surname_", entity.surname);
                command.Parameters.AddWithValue("@api_key_", entity.api_key);
                command.Parameters.AddWithValue("@api_request_", entity.api_request);
                command.Parameters.AddWithValue("@company_id_", entity.company_id);
                command.Parameters.AddWithValue("@phone_", entity.phone);

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

        public EntityResult<User> List()
        {
            EntityResult<User> result = new EntityResult<User>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                List<User> userList = new List<User>();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM autostorehouse.user", connectionOpen());
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    User _user = new User();
                    _user.mail = reader.GetString(0);
                    _user.password = reader.GetString(1);
                    _user.name = reader.GetString(2);
                    _user.surname = reader.GetString(3);
                    _user.api_key = reader.GetString(4);
                    _user.api_request = reader.GetInt32(5);
                    _user.company_id = reader.GetInt32(6);
                    _user.phone = reader.GetString(7);

                    userList.Add(_user);
                }
                result.Objects = userList;
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
        public EntityResult<User> ResetPassword(string mail, string newpassword)
        {
            EntityResult<User> result = new EntityResult<User>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                List<User> userList = new List<User>();
                NpgsqlCommand command = new NpgsqlCommand("UPDATE autostorehouse.user SET password=@password_ WHERE mail=@mail_", connectionOpen());
                command.Parameters.Add(new NpgsqlParameter("@mail_", mail));
                command.Parameters.Add(new NpgsqlParameter("@password_", newpassword));
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
