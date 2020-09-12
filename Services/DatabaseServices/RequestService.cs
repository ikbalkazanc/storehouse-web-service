using System;
using System.Collections.Generic;
using System.Text;

using Services;
using Entities.DatabaseEntities;
using Entities;
using Services.Interfaces;
using Npgsql;
using System.Linq;

namespace Services.DatabaseServices
{

    public class RequestService : PostgreConnection, IServices<Request>,IObjects<Request>
    {
        public EntityResult<Request> Delete(int id)
        {
            EntityResult<Request> result = new EntityResult<Request>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM autostorehouse.request WHERE id=@id_;", connectionOpen());
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

        public EntityResult<Request> Get(int id)
        {
            EntityResult<Request> result = new EntityResult<Request>();
            Request _Request = new Request();
            result.Result = true;
            result.ErrorText = "Sucsess";

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM autostorehouse.request WHERE id=@id_;", connectionOpen());
                command.Parameters.Add(new NpgsqlParameter("@id_", id));
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    _Request.id = reader.GetInt32(0);
                    _Request.date = reader.GetDateTime(1);
                    _Request.type.Add(Request.convert(reader.GetString(2)));
                    _Request.company_id = reader.GetInt32(3);
                    _Request.api_key = reader.GetString(4);
                }

            }
            catch (NpgsqlException Ex)
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
                result.ErrorCode = Ex.ErrorCode;
            }
            connectionClose();
            result.Object = _Request;
            return result;
        }

        public EntityResult<Request> Insert(Request entities)
        {
            EntityResult<Request> result = new EntityResult<Request>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO autostorehouse.request VALUES(@id_,@date_,@type_,@company_id_,@api_key_)", connectionOpen());
                command.Parameters.AddWithValue("@id_", entities.id);
                command.Parameters.AddWithValue("@date_", entities.date);
                command.Parameters.AddWithValue("@type_", entities.type[0].ToString());
                command.Parameters.AddWithValue("@company_id_", entities.company_id);
                command.Parameters.AddWithValue("@api_key_", entities.api_key);
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

        public EntityResult<Request> List()
        {
            EntityResult<Request> result = new EntityResult<Request>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                List<Request> RequestList = new List<Request>();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM autostorehouse.request", connectionOpen());
                NpgsqlDataReader reader = command.ExecuteReader();

                Request _Request = new Request();
                while (reader.Read())
                {
                    _Request.id = reader.GetInt32(0);
                    _Request.date = reader.GetDateTime(1);
                    _Request.type.Append(Request.convert(reader.GetString(2)));
                    _Request.company_id = reader.GetInt32(3);
                    _Request.api_key = reader.GetString(4);

                    RequestList.Add(_Request);
                }
                result.Objects = RequestList;
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
