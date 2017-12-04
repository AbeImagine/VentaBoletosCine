using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    class Venta
    {
        public int id_venta { get; set; }
        public string usuario { get; set; }
        public int id_funcion { get; set; }
        public int precio { get; set; }
        private MySqlDataReader reader;

        public Venta()
        {

        }

        public bool Registrar(DBConnection conexionBD)
        {
            string commandtxt = "INSERT INTO venta (usuario_venta, id_funcion, precio) VALUES (" + usuario + "," + id_funcion + "," + precio + ")";
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
