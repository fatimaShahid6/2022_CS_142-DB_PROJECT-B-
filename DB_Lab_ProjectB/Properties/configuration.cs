using System;
using System.Data.SqlClient;

namespace DB_Lab_ProjectB
{
    public class Configuration
    {
        private static Configuration _instance;
        private string connectionString = @"Data Source=(local);Initial Catalog=ProjectB;Integrated Security=True";

        private Configuration()
        {
        }

        public static Configuration getInstance()
        {
            if (_instance == null)
            {
                _instance = new Configuration();
            }
            return _instance;
        }

        public SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        internal string GetConnectionString()
        {
            // Replace this with your actual connection string
            return "Data Source=DESKTOP-74I34NE;Initial Catalog=ProjectB;Integrated Security=True";
        }

    }
}
