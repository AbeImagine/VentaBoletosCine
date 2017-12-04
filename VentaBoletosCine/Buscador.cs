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
    public partial class Buscador : Form
    {
        public int id;
        private DBConnection conexionBD;
        private string tabla;
        public string busqueda;
        private DataTable dT;

        public Buscador(DBConnection conexionBD, string table)
        {
            tabla = table;
            id = -1;
            this.conexionBD = conexionBD;
            InitializeComponent();
        }

        public void LlenarComboBox(BindingSource bSource)
        {
            dataGridView1.DataSource = bSource;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                comboBox1.Items.Add(dataGridView1.Columns[i].Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            id = comboBox1.SelectedIndex;
            busqueda = textBox1.Text;
            this.Close();
        }
    }
}
