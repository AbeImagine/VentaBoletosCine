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
    public partial class membresias : Form
    {
        DBConnection conexionBD;

        public membresias(DBConnection conexion)
        {
            conexionBD = conexion;
            InitializeComponent();
        }

        private void membresias_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }
    }
}
