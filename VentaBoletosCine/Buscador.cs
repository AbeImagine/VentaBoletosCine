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
    /*  Clase: Buscador.
     *  Descripción: Esta clase es utilizada para buscar registros utilizando las columnas de la tabla.
     */
    public partial class Buscador : Form
    {
        public int id;
        private DBConnection conexionBD;
        private string tabla;
        public string busqueda;
        private DataTable dT;

        /*  Constructor: Buscador.
         *  Descripción: Constructor de la clase Buscador.
         *  Parámetros:
         *      conexionBD: objeto que crea la conexión con la Base de Datos.
         *      table: string que contiene el nombre de la tabla en la que se raliza la busqueda.
         */
        public Buscador(DBConnection conexionBD, string table)
        {
            tabla = table;
            id = -1;
            this.conexionBD = conexionBD;
            InitializeComponent();
        }

        /*  Método: LlenarComboBox
         *  Descripción: Se encarga de llenar el comboBox de la ventana con las columnas de la tabla.
         *  Parámetros:
         *      bSource: contiene la fuente de informacion de la tabla.
         */
        public void LlenarComboBox(BindingSource bSource)
        {
            dataGridView1.DataSource = bSource;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                comboBox1.Items.Add(dataGridView1.Columns[i].Name);
            }
        }

        /*  Evento: button1_Click
         *  Descripción: Se encarga de terminar la busqueda y regresar los datos.
         * 
         */
        private void button1_Click(object sender, EventArgs e)
        {
            id = comboBox1.SelectedIndex;
            busqueda = textBox1.Text;
            this.Close();
        }
    }
}
