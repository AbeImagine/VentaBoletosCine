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
        public int indexNumber;

        public Funcion()
        {
            id_funcion = -1;
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

        public bool Recuperar(DBConnection conexionBD, int opcion, string busqueda)
        {
            string commandtxt = "";

            switch (opcion)
            {
                case 0:
                    commandtxt = "SELECT * FROM funcion WHERE id_funcion = " + busqueda;
                    break;
                case 1:
                    commandtxt = "SELECT * FROM funcion WHERE hora = '" + busqueda + "'";
                    break;
                case 2:
                    commandtxt = "SELECT * FROM funcion WHERE num_sala = " + busqueda;
                    break;
                case 3:
                    commandtxt = "SELECT * FROM funcion WHERE id_pelicula = " + busqueda;
                    break;
                case 4:
                    commandtxt = "SELECT * FROM funcion WHERE precio = " + busqueda;
                    break;
            }
            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    id_funcion = reader.GetInt32("id_funcion");
                    id_pelicula = reader.GetInt32("id_pelicula");
                    hora = reader.GetString("hora");
                    precio = reader.GetInt32("precio");
                    num_sala = reader.GetInt32("num_sala");
                }
                reader.Close();
            }
            catch (Exception e)
            {
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

            commandtxt = "SELECT id_funcion FROM funcion WHERE id_funcion = (SELECT MAX(id_funcion) FROM funcion)";
            command = new MySqlCommand(commandtxt, conexionBD.Connection);
            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    id_funcion = reader.GetInt32(0);
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                reader.Close();
                return false;
            }


            for (int i = 0; i < 32; i++)
            {
                commandtxt = "INSERT INTO asiento (disponibilidad, num_sala, id_funcion, num_asiento) VALUES (" + true + "," + num_sala + "," + id_funcion + "," + i + ")";

                command = new MySqlCommand(commandtxt, conexionBD.Connection);

                try
                {
                    reader = command.ExecuteReader();
                    reader.Close();
                }
                catch (Exception exception)
                {
                    reader.Close();
                    return false;
                }
            }

                return true;
        }

        public bool Eliminar(DBConnection conexionBD, int id)
        {
            string commandtxt = "DELETE FROM funcion WHERE id_funcion=" + id;
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

        public bool Actualizar(DBConnection conexionBD)
        {
            string commandtxt = "UPDATE funcion SET id_pelicula=" + id_pelicula + ", hora='" + hora + "', precio=" + precio + ", num_sala=" + num_sala + "' WHERE id_funcion=" + id_funcion;
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

        public List<Asiento> RecuperarAsientos(DBConnection conexionBD)
        {
            List<Asiento> asientos = new List<Asiento>();

            string commandtxt = "SELECT * FROM asiento WHERE id_funcion = " + id_funcion + "";
            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Asiento a = new Asiento();

                    a.id_asiento = reader.GetInt32("id_asiento");
                    a.disponible = reader.GetBoolean("disponibilidad");
                    a.num_sala = reader.GetInt32("num_sala");
                    a.id_funcion = id_funcion;
                    a.num_asiento = reader.GetInt32("num_asiento");

                    asientos.Add(a);
                }

                reader.Close();
            }
            catch (Exception exception)
            {

            }
            return asientos;
        }
    }
}
