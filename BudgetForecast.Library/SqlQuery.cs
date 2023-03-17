using My.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BudgetForecast.Library
{
    public class SqlQuery
    {
        string connectionstring = Utils.GetConfig("MobileOrder_ConnectionString");
        public DataTable GetSqlQuery(string sql, SqlParameters param)
        {
            var dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                foreach (object[] item in param)
                    command.Parameters.AddWithValue(item[0].ToString(), item[1]);

                SqlDataReader dataReader = command.ExecuteReader();

                dt.Load(dataReader);

                dataReader.Close();
                command.Dispose();
                connection.Close();
            }

            return dt;
        }

        public DataTable GetSqlQuery(string sql)
        {
            var dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataReader dataReader = command.ExecuteReader();

                dt.Load(dataReader);

                dataReader.Close();
                command.Dispose();
                connection.Close();
            }

            return dt;
        }
    }
}
