using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    class Pelicula
    {
        public int id_pelicula { get; set; }
        public string nombre { get; set; }
        public int duracion { get; set; }
        public string genero { get; set; }
        public string sinopsis { get; set; }
        public string reparto { get; set; }
        private MySqlDataReader reader;

        public Pelicula()
        {
            
        }

        public bool Recuperar(DBConnection conexionBD, int id_peli)
        {
            id_pelicula = id_peli;

            string commandtxt = "SELECT * FROM pelicula WHERE id_pelicula = " + id_pelicula;

            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    duracion = reader.GetInt32("duracion");
                    nombre = reader.GetString("nombre");
                    genero = reader.GetString("genero");
                    sinopsis = reader.GetString("sinopsis");
                    reparto = reader.GetString("reparto");
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

    }
}
