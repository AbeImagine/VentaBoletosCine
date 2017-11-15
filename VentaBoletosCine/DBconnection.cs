using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    public class DBConnection
    {
        private DBConnection()
        {
        }

        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnected()
        {
            bool result = true;
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(databaseName))
                    result = false;
                //string connstring = string.Format("Server=localhost; database={0}; UID=UserName; password=your password", databaseName);
<<<<<<< HEAD
<<<<<<< HEAD
                string connstring = string.Format("Server=localhost; database={0}; UID=root; password=3MM@nu3l", databaseName);
=======
                string connstring = string.Format("Server=localhost; database={0}; UID=root; password=''", databaseName);
>>>>>>> parent of 7a0f7db... Implementacion final de inserción
=======
                string connstring = string.Format("Server=localhost; database={0}; UID=root; password=''", databaseName);
>>>>>>> parent of 7a0f7db... Implementacion final de inserción
                connection = new MySqlConnection(connstring);
                connection.Open();
                result = true;
            }

            return result;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
