using Entities;
using Entities.DatabaseEntities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Services.DatabaseServices
{
    public class OrderService : PostgreConnection, IObjects<Order>
    {
        
        public EntityResult<Order> Delete(int id)
        {
            EntityResult<Order> result = new EntityResult<Order>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM autostorehouse.order WHERE id=@id_;", connectionOpen());
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

        public EntityResult<Order> Get(int id)
        {
            EntityResult<Order> result = new EntityResult<Order>();
            Order _order = new Order();
            result.Result = true;
            result.ErrorText = "Sucsess";

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM autostorehouse.order WHERE id=@id_;", connectionOpen());
                command.Parameters.Add(new NpgsqlParameter("@id_", id));
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();

                _order.id = reader.GetInt32(0);
                _order.user_mail = reader.GetString(1);
                _order.summary = reader.GetString(2);
                _order.quantity = reader.GetInt32(3);
                _order.company_id = reader.GetInt32(4);
                _order.product_qr = reader.GetString(5);
                

            }
            catch (NpgsqlException Ex)
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
                result.ErrorCode = Ex.ErrorCode;
            }
            connectionClose();
            result.Object = _order;
            return result;
        }

        public EntityResult<Order> Insert(Order entity)
        {
            EntityResult<Order> result = new EntityResult<Order>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO autostorehouse.order VALUES(@id_,@user_mail_,@summary_,@quantity_,@company_id_,@product_qr_)", connectionOpen());
                command.Parameters.AddWithValue("@id_", entity.id);
               
                command.Parameters.AddWithValue("@user_mail_", entity.user_mail);
                command.Parameters.AddWithValue("@summary_", entity.summary);
                command.Parameters.AddWithValue("@quantity_", entity.quantity);
                command.Parameters.AddWithValue("@product_qr_", entity.product_qr);
                command.Parameters.AddWithValue("@company_id_", entity.company_id);


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

        public EntityResult<Order> List(int company_id)
        {
            EntityResult<Order> result = new EntityResult<Order>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                List<Order> orderList = new List<Order>();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM autostorehouse.order WHERE company_id = @company_id_", connectionOpen());
                command.Parameters.AddWithValue("@company_id_", company_id);
                NpgsqlDataReader reader = command.ExecuteReader();

                Order _order = new Order();
                while (reader.Read())
                {

                    _order.id = reader.GetInt32(0);
                    _order.user_mail = reader.GetString(1);
                    _order.summary = reader.GetString(2);
                    _order.quantity = reader.GetInt32(3);
                    _order.company_id = reader.GetInt32(4);
                    _order.product_qr = reader.GetString(5);

                    orderList.Add(_order);
                }
                result.Objects = orderList;
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
