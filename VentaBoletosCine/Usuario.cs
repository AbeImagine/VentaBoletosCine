using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    class Usuario
    {
        public int id_usuario{ get; set;}
        public int precioTotal { get; set; }
        public int id_funcion { get; set; }
        public int id_venta { get; set; }
        private MySqlDataReader reader;

        public Usuario()
        {

        }

        public bool Registrar(DBConnection conexionBD)
        {

            return true;
        }

    }
}
