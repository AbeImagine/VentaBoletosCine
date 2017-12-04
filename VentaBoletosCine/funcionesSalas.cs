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
    public partial class funcionesSalas : Form
    {
        DBConnection conexionBD;
        MySqlDataReader reader;
        MySqlDataAdapter DA;
        Funcion funcion;
        int precio;
        private int index;

        public funcionesSalas(DBConnection conexion)
        {
            precio = 50;
            conexionBD = conexion;
            InitializeComponent();
            FillComboBoxPelicula();
            FillComboBoxSala();
            FillComboBoxHorario();
            tbPrecio.Text = precio.ToString("C");

            dataGridView1.Visible = false;
            LlenarTablaAuxiliar();
            btGuardar.Enabled = false;
            btActualizar.Enabled = false;
        }

        private void LlenarTablaAuxiliar()
        {
            DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from funcion";
            DA.SelectCommand = new MySqlCommand(sqlSelectAll, conexionBD.Connection);

            DataTable dataTable = new DataTable();
            BindingSource bS = new BindingSource();
            DA.Fill(dataTable);
            bS.DataSource = dataTable;
            dataGridView1.DataSource = bS;
        }

        private void FillComboBoxSala()
        {
            string commandtxt = "SELECT num_sala from sala";
            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                reader = command.ExecuteReader();
                int text;
                while (reader.Read())
                {
                    text = reader.GetInt32(0);
                    comboBoxSala.Items.Add(text);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                reader.Close();
            }
        }

        private void FillComboBoxPelicula()
        {
            string commandtxt = "SELECT nombre from pelicula";
            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                reader = command.ExecuteReader();

                string text;
                while (reader.Read())
                {
                    text = reader.GetString(0);
                    comboBoxPeliculas.Items.Add(text);
                }

                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                reader.Close();
            }
        }

        private void FillComboBoxHorario()
        {
            int aux = 4;
            string saux;
            for (int i = 0; i < 6; i++)
            {
                saux = (i * aux) + ":00";
                comboBoxHorario.Items.Add(saux);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar)&& e.KeyChar !='\b')
            {
                e.Handled = true;
            }
            else
                MessageBox.Show("Ingrese solamente numeros ");
        }

        /*
         * Descripción: Configuración para la tecla TAB de todos 
         * los elementos del form 
         */
        public void configuraIndex()
        {
            //this.textBox1.TabIndex = 0;
            //this.textBox2.TabIndex = 1;
            //this.textBox3.TabIndex = 2;
            this.tbPrecio.TabIndex = 3;
            //this.textBox5.TabIndex = 4;
            //this.button1.TabIndex = 5;
            this.btBuscar.TabIndex = 5;
            //this.button3.TabIndex = 5;
            //this.button4.TabIndex = 5;
            
        }

        private void funcionesSalas_Load(object sender, EventArgs e)
        {
            configuraBotones();
            configuraIndex();


        
        }

        /*
         *Descripción: Este método deshailita los textbox para proteger la información
         *que sea modificada por error, etc. 
         */
        public void configuraBotones()
        {

            //textBox1.Enabled = false ;
            //textBox2.Enabled = false ;
            //textBox3.Enabled = false ;
            tbPrecio.Enabled = false ;
            //textBox5.Enabled = false ;
        }

        /*
         * Descripción: Este método habilita  os botones para poder hacer su edición
         */
        public void habilitaBotones()
        {

            //textBox1.Enabled = false;
            //textBox2.Enabled = false;
            //textBox3.Enabled = false;
            tbPrecio.Enabled = false;
            //textBox5.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Buscador busca;
            busca = new Buscador(conexionBD, "funcion");
            busca.LlenarComboBox((BindingSource)dataGridView1.DataSource);
            busca.ShowDialog();
            if (busca.id != -1)
            {
                LlenaCampos(busca.id, busca.busqueda);
                btActualizar.Enabled = true;
                btGuardar.Enabled = false;
            }
            else
                MessageBox.Show("Registro no encontrado");
        }

        private void LlenaCampos(int opcion, string busqueda)
        {
            funcion = new Funcion();
            funcion.Recuperar(conexionBD, opcion, busqueda);

            tbPrecio.Text = funcion.precio.ToString();
            comboBoxHorario.Text = funcion.hora;
            comboBoxSala.Text = funcion.num_sala.ToString();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            funcion = new Funcion();
            comboBoxHorario.Text = "";
            comboBoxPeliculas.Text = "";
            comboBoxSala.Text = "";
            btGuardar.Enabled = true;
            btActualizar.Enabled = false;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            funcion = new Funcion();

            if ((tbPrecio.Text != "") &&
               (comboBoxPeliculas.Text !="") &&
               (comboBoxSala.Text !="") &&
               (comboBoxHorario.Text != "")
               )
            {
                funcion.hora = (string)comboBoxHorario.Items[comboBoxHorario.SelectedIndex];
                funcion.num_sala = (int)comboBoxSala.Items[comboBoxSala.SelectedIndex];
                funcion.id_pelicula = (int)comboBoxPeliculas.SelectedIndex + 1;
                funcion.precio = precio;

                if (funcion.Registrar(conexionBD) == true)
                {
                    MessageBox.Show("Registro exitoso");
                }
                else
                    MessageBox.Show("Error en el registro");
            }
            else
                MessageBox.Show("Ingrese todos los datos para poder guardar el registro");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_2(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataGrid dG = new DataGrid();
            dG.GetData((BindingSource)dataGridView1.DataSource);
            dG.ShowDialog();
            funcion = new Funcion();

            if (dG.id != -1)
            {
                if (funcion.Eliminar(conexionBD, dG.id) == true)
                {
                    MessageBox.Show("Eliminación exitosa");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlDataAdapter DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from funcion";
            DA.SelectCommand = new MySqlCommand(sqlSelectAll, conexionBD.Connection);

            DataTable table = new DataTable();
            DA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;

            DataGrid DG = new DataGrid();
            DG.ShowData(bSource);
            DG.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MySqlDataAdapter DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from funcion";
            DA.SelectCommand = new MySqlCommand(sqlSelectAll, conexionBD.Connection);

            DataTable table = new DataTable();
            DA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;

            DataGrid DG = new DataGrid();
            DG.ShowData(bSource);
            DG.Show();
        }

        private void button3_Click_3(object sender, EventArgs e)
        {
            index++;
            if (index == dataGridView1.Rows.Count - 1)
                index = 0;
            ActualizaCampos();
            btActualizar.Enabled = true;
            btGuardar.Enabled = false;
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            index--;
            if (index < 0)
                index = dataGridView1.Rows.Count - 2;
            ActualizaCampos();
            btActualizar.Enabled = true;
            btGuardar.Enabled = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            index = 0;
            ActualizaCampos();
            btActualizar.Enabled = true;
            btGuardar.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            index = dataGridView1.Rows.Count - 2;
            ActualizaCampos();
            btActualizar.Enabled = true;
            btGuardar.Enabled = false;
        }

        private void ActualizaCampos()
        {
            funcion = new Funcion();

            funcion.id_funcion = (int)dataGridView1.Rows[index].Cells[0].Value;
            funcion.hora = (string)dataGridView1.Rows[index].Cells[1].Value;
            funcion.num_sala = (int)dataGridView1.Rows[index].Cells[2].Value;
            funcion.id_pelicula = (int)dataGridView1.Rows[index].Cells[3].Value;
            funcion.precio = (int)dataGridView1.Rows[index].Cells[4].Value;

            tbPrecio.Text = funcion.precio.ToString();
            comboBoxHorario.Text = funcion.hora;
            comboBoxSala.Text = funcion.num_sala.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (funcion.id_funcion != -1)
            {
                funcion.id_pelicula = (int)comboBoxPeliculas.SelectedIndex + 1;
                funcion.hora = (string)comboBoxHorario.Items[comboBoxHorario.SelectedIndex];
                funcion.num_sala = (int)comboBoxSala.Items[comboBoxSala.SelectedIndex];
                funcion.precio = int.Parse(tbPrecio.Text);

                if (funcion.Actualizar(conexionBD) == true)
                {
                    MessageBox.Show("Actualización exitosa");
                }
            }
        }
    }
}
