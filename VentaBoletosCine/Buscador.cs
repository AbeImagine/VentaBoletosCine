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
        //private MySqlCommand command;
        //private MySqlDataReader reader;
        //private MySqlDataAdapter adapter;
        private string tabla;
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
            /*
            string sCommand = "SELECT * from " + tabla + " WHERE " + comboBox1.Items[comboBox1.SelectedIndex] + " = " + textBox1.Text + "";
            adapter.SelectCommand = new MySqlCommand(sCommand, conexionBD.Connection);

            dT = new DataTable();
            adapter.Fill(dT);
            dGV.DataSource = dT;
             */
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[comboBox1.SelectedIndex].Value.Equals(textBox1.Text))
                {
                    id = i;
                    break;
                }
            }
            this.Close();
            
        }
    }
}
