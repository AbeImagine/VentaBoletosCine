using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    public class Usuario
    {
        public string nombreUsusario{ get; set;}
        public string contraseña { get; set; }
        public int permisos { get; set; }
        private MySqlDataReader reader;

        public Usuario()
        {

        }

        public bool Registrar(DBConnection conexionBD)
        {
            string commandtxt = "INSERT INTO usuario (usuario, contrasena, permisos) VALUES ('" + nombreUsusario + "','" + contraseña + "'," + permisos + ")";
            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
            catch (Exception exception)
            {
                reader.Close();
                return false;
            }
            return true;
        }

    }
}
