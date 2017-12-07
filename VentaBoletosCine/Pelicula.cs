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
            id_pelicula = -1;
        }

        public bool Registrar(DBConnection conexionBD)
        {
            MySqlCommand comando = new MySqlCommand("INSERT INTO pelicula (nombre, duracion, genero, sinopsis, reparto) VALUES ('" + nombre + "'," + duracion + ",'" + genero + "','" + sinopsis + "','" + reparto + "')", conexionBD.Connection);
            try
            {
                reader = comando.ExecuteReader();
                reader.Close();
            }
            catch (Exception exc)
            {
                reader.Close();
                return false;
            }
            return true;
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

        public bool Recuperar(DBConnection conexionBD, int opcion, string busqueda)
        {
            string commandtxt = "";

            switch (opcion)
            {
                case 0:
                    commandtxt = "SELECT * FROM pelicula WHERE id_pelicula = " + busqueda;
                    break;
                case 1:
                    commandtxt = "SELECT * FROM pelicula WHERE nombre like '%" + busqueda + "%'";
                    break;
                case 2:
                    commandtxt = "SELECT * FROM pelicula WHERE duracion = " + busqueda;
                    break;
                case 3:
                    commandtxt = "SELECT * FROM pelicula WHERE genero like '%" + busqueda + "%'";
                    break;
                case 4:
                    commandtxt = "SELECT * FROM pelicula WHERE sinopsis like '%" + busqueda + "%'";
                    break;
                case 5:
                    commandtxt = "SELECT * FROM pelicula WHERE reparto like '%" + busqueda + "%'";
                    break;
            }
            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    id_pelicula = reader.GetInt32("id_pelicula");
                    nombre = reader.GetString("nombre");
                    duracion = reader.GetInt32("duracion");
                    genero = reader.GetString("genero");
                    sinopsis = reader.GetString("sinopsis");
                    reparto = reader.GetString("reparto");
                }
                reader.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Actualizar(DBConnection conexionBD)
        {
            string commandtxt = "UPDATE pelicula SET nombre='" + nombre + "', duracion=" + duracion + ", genero='" + genero + "', sinopsis='" + sinopsis + "', reparto='" + reparto + "' WHERE id_pelicula=" + id_pelicula;
            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
            catch (Exception exception)
            {
                return false;
            }
            return true;
        }

        public bool Eliminar(DBConnection conexionBD, int id)
        {
            string commandtxt = "DELETE FROM pelicula WHERE id_pelicula=" + id;
            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
            catch (Exception exception)
            {
                return false;
            }
            return true;
        }
    }
}
