using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace VentaBoletosCine
{
    public partial class membresias : Form
    {
        DBConnection conexionBD;
        MySqlDataReader reader;
        MySqlCommand comando;

        public membresias(DBConnection conexion)
        {
            conexionBD = conexion;
            InitializeComponent();
            cbTipoMemb.Items.Add("Administrador");
            cbTipoMemb.Items.Add("Regular");
        }

        private void membresias_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comando = new MySqlCommand("INSERT INTO miembro (id_miembro, nombre, telefono, correo, contraseña, administrador) VALUES (3,'"+textBox1.Text+"',"+textBox3.Text+",'"+textBox4.Text+"','"+textBox5.Text+"',"+cbTipoMemb.SelectedIndex+")", conexionBD.Connection);
            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("Registro exitoso");
            }
        }
    }
}
