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
    public partial class Menu : Form
    {
        DBConnection conexionBD;

        public Menu(DBConnection conexionBD, bool admin)
        {
            InitializeComponent();
            if (admin == false)
            {
                button7.Visible = false;
                button5.Visible = false;
                button2.Visible = false;
            }
            this.conexionBD = conexionBD;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ventaBoletos  venta = new ventaBoletos(conexionBD);
            this.Hide();
            venta.ShowDialog();
            this.Show();
            this.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Capturista capturista = new Capturista(conexionBD);
            this.Hide();
            capturista.ShowDialog();
            this.Show();
            this.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            membresias mem = new membresias(conexionBD);
            this.Hide();
            mem.ShowDialog();
            this.Show();
            this.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //boleto bole = new boleto(conexionBD);
            this.Hide();
            //bole.ShowDialog();
            this.Show();
            this.Focus();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            venLineacs venta = new venLineacs(conexionBD);
            this.Hide();
            venta.ShowDialog();
            this.Show();
            this.Focus();

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ventaUsuario ventaUsuario = new ventaUsuario(conexionBD);
            this.Hide();
            ventaUsuario.ShowDialog();
            this.Show();
            this.Focus();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            funcionesSalas funcionesSala = new funcionesSalas(conexionBD);
            this.Hide();
            funcionesSala.ShowDialog();
            this.Show();
            this.Focus();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
