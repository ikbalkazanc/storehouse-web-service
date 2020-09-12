using System;
using System.Collections.Generic;
using System.Text;

using Npgsql;

namespace Services
{
    public class PostgreConnection
    {
        private NpgsqlConnection connection = new NpgsqlConnection("Server=autostorehouse.postgres.database.azure.com; Port=5432; Database=postgres; User Id=postgres@autostorehouse; Password=Xl.3236825;Ssl Mode=Require;");

        protected NpgsqlConnection connectionOpen()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                return connection;
            }
            catch (NpgsqlException Ex)
            {
                var p = Ex.Message;
                return connection;
            }
        }
        protected NpgsqlConnection connectionClose()
        {
            if (connection.State == System.Data.ConnectionState.Open) { connection.Close(); }
            return connection;
        }
    }
}
