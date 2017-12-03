using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    class Funcion
    {
        public int id_funcion { get; set; }
        public int id_pelicula { get; set; }
        public string hora { get; set; }
        public float precio { get; set; }
        public int num_sala { get; set; }
        public Pelicula pelicula { get; set; }
        private MySqlDataReader reader;

        public Funcion()
        {

        }

        public bool Recuperar(DBConnection conexionBD, int id_func)
        {
            id_funcion = id_func;

            string commandtxt = "SELECT * FROM funcion WHERE id_funcion = " + id_funcion;

            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    id_pelicula = reader.GetInt32("id_pelicula");
                    hora = reader.GetString("hora");
                    precio = reader.GetInt32("precio");
                    num_sala = reader.GetInt32("num_sala");
                }
                reader.Close();
            }
            catch (Exception e)
            {
                reader.Close();
                return false;
            }
            return true;
        }

        public bool Registrar(DBConnection conexionBD)
        {
            string commandtxt = "INSERT INTO funcion (hora, num_sala, id_pelicula, precio) VALUES ('" + hora + "'," + num_sala + "," + id_pelicula + "," + precio + ")";
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
