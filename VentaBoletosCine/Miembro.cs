using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    class Miembro
    {
        public int id_miembro{ get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public int nivel { get; set; }
        private MySqlDataReader reader;

        public bool Registrar(DBConnection conexionBD)
        {
            string commandtxt = "INSERT INTO miembro (nombre, telefono, correo, nivel) VALUES ('" + nombre + "', " + telefono + ",'" + correo + "'," + nivel + ")";
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

        public bool Recuperar(DBConnection conexionBD)
        {
            return false;
        }

        public bool Recuperar(DBConnection conexionBD, int num, string busqueda)
        {
            string commandtxt = "";

            switch (num)
            {
                case 0:
                    commandtxt = "SELECT * FROM miembro WHERE id_miembro = " + busqueda;
                    break;
                case 1:
                    commandtxt = "SELECT * FROM miembro WHERE nombre = " + busqueda;
                    break;
                case 2:
                    commandtxt = "SELECT * FROM miembro WHERE telefono = " + busqueda;
                    break;
                case 3:
                    commandtxt = "SELECT * FROM miembro WHERE correo = " + busqueda;
                    break;
                case 4:
                    commandtxt = "SELECT * FROM miembro WHERE nivel = " + busqueda;
                    break;
            }
            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    id_miembro = reader.GetInt32("id_miembro");
                    nombre = reader.GetString("nombre");
                    telefono = reader.GetInt32("telefono").ToString();
                    correo = reader.GetString("correo");
                    nivel = reader.GetInt32("nivel");
                }
                reader.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

    }
}
