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
    public partial class ventaBoletos : Form
    {
        DBConnection conexionBD;

        public ventaBoletos(DBConnection conexion)
        {
            conexionBD = conexion;
            InitializeComponent();
        }

        public int generaNumBoleto()
        {
            Random random = new Random();

            int numBoleto = random.Next(); ;

            return numBoleto;

        }

        private void ventaBoletos_Load(object sender, EventArgs e)
        {
            tbFecha.ReadOnly = true;
            tbHora.ReadOnly = true;
            tbNumBoleto.ReadOnly = true;


            this.DoubleBuffered = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Nombre_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
