using Entities;
using Entities.DatabaseEntities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Services.DatabaseServices
{
    public class ProductService : PostgreConnection
    {
        public EntityResult<Product> Delete(string qr)
        {
            EntityResult<Product> result = new EntityResult<Product>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM autostorehouse.product WHERE qr=@qr_;", connectionOpen());
                command.Parameters.Add(new NpgsqlParameter("@qr_", qr));
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

        public EntityResult<Product> Get(string qr)
        {
            EntityResult<Product> result = new EntityResult<Product>();
            Product _product = new Product();
            result.Result = true;
            result.ErrorText = "Sucsess";

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM autostorehouse.product WHERE qr=@qr_ ;", connectionOpen());      
                command.Parameters.Add(new NpgsqlParameter("@qr_", qr));
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {

                    _product.qr = reader.GetString(0);
                    _product.name = reader.GetString(1);
                    _product.summary = reader.GetString(2);
                    _product.quantity = reader.GetInt32(3);
                    _product.price = reader.GetInt32(4);
                    _product.sold = reader.GetInt32(5);
                    _product.company_id = reader.GetInt32(6);
                    _product.user_mail = reader.GetString(7);
                }
                else
                {
                    result.Result = false;
                    result.ErrorText = "Veritabanında Qr Kod bulunamadı";
                }
            }
            catch (NpgsqlException Ex)
            {
                result.Result = false;
                result.ErrorText = Ex.Message;
                result.ErrorCode = Ex.ErrorCode;
            }
            connectionClose();
            result.Object = _product;
            return result;
        }


        public EntityResult<Product> Insert(Product entity)
        {
            EntityResult<Product> result = new EntityResult<Product>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO autostorehouse.product VALUES(@qr_,@name_,@summary_,@quantity_,@price_,@sold_,@company_id_,@user_mail_)", connectionOpen());
                command.Parameters.AddWithValue("@qr_", entity.qr);
                command.Parameters.AddWithValue("@name_", entity.name);
                command.Parameters.AddWithValue("@summary_", entity.summary);
                command.Parameters.AddWithValue("@quantity_", entity.quantity);
                command.Parameters.AddWithValue("@price_", entity.price);
                command.Parameters.AddWithValue("@sold_", entity.sold);
                command.Parameters.AddWithValue("@company_id_", entity.company_id);
                command.Parameters.AddWithValue("@user_mail_", entity.user_mail);

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

        public EntityResult<Product> List(int company_id)
        {
            EntityResult<Product> result = new EntityResult<Product>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                List<Product> productList = new List<Product>();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM autostorehouse.product WHERE company_id=@company_id_", connectionOpen());
                command.Parameters.AddWithValue("@company_id_", company_id);
                NpgsqlDataReader reader = command.ExecuteReader();

                
                while (reader.Read())
                {
                    Product _product = new Product();
                    _product.qr = reader.GetString(0);
                    _product.name = reader.GetString(1);
                    _product.summary = reader.GetString(2);
                    _product.quantity = reader.GetInt32(3);
                    _product.price = reader.GetInt32(4);
                    _product.sold = reader.GetInt32(5);
                    _product.company_id = reader.GetInt32(6);
                    _product.user_mail = reader.GetString(7);

                    productList.Add(_product);
                }
                result.Objects = productList;
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
        public EntityResult<Product> Increase(string qr,int amount)
        {
            EntityResult<Product> result = new EntityResult<Product>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE autostorehouse.product SET quantity = quantity + @amount_ WHERE qr = @qr_", connectionOpen());
                command.Parameters.AddWithValue("@qr_", qr);
                command.Parameters.AddWithValue("@amount_", amount);
                NpgsqlDataReader reader = command.ExecuteReader();        
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
        public EntityResult<Product> Decrease(string qr, int amount)
        {
            EntityResult<Product> result = new EntityResult<Product>();
            result.Result = true;
            result.ErrorText = "Sucsess";
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT quantity FROM autostorehouse.product WHERE qr = @qr_", connectionOpen());
                command.Parameters.AddWithValue("@qr_", qr);
                
                NpgsqlDataReader reader = command.ExecuteReader();
                reader.Read();

                if(reader.GetInt32(0) >= amount)
                {
                    reader.Close();
                    command.CommandText = "UPDATE autostorehouse.product SET quantity = quantity - @amount_, sold = sold + @amount_ WHERE qr = @qr_";
                    command.Parameters.AddWithValue("@amount_", amount);
                    reader = command.ExecuteReader();
                    reader.Close();
                }
                else
                {
                    result.Result = false;
                    result.ErrorText = "Siliceğin miktar stoktan fazla olamaz";
                    result.ErrorCode = 9999;

                }

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