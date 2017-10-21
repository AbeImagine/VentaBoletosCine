using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VentaBoletosCine
{
    public partial class ventaUsuario : Form
    {
        DBConnection conexionBD;

        public ventaUsuario(DBConnection conexion)
        {
            conexionBD = conexion;
            InitializeComponent();
        }
    }
}
