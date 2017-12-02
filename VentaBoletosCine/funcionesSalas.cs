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

        public funcionesSalas(DBConnection conexion)
        {
            conexionBD = conexion;
            InitializeComponent();
            FillComboBoxPelicula();
            FillComboBoxSala();
            FillComboBoxHorario();
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
            int aux = 10;
            string saux;
            for (int i = 0; i < 5; i++)
            {
                saux = (i * aux) + ":00";
                comboBoxHorario.Items.Add(saux);
            }
        }

        /*
        private void button5_Click(object sender, EventArgs e)
        {
            if( (textBox4.Text != "" )&&
                comboBoxPeliculas.Items[comboBoxPeliculas.SelectedIndex] != null &&
                comboBoxSala.Items[comboBoxSala.SelectedIndex] != null &&
                comboBoxHorario.Items[comboBoxHorario.SelectedIndex] != null
                )
            {
                int horario = (int)comboBoxHorario.Items[comboBoxHorario.SelectedIndex];
                int num_sala = (int)comboBoxSala.Items[comboBoxSala.SelectedIndex];
                int id_pelicula = (int)comboBoxPeliculas.SelectedIndex + 1;

                string commandtxt = "INSERT INTO  (hora, num_sala, id_pelicula) VALUES (" + horario + "," + num_sala + "," + id_pelicula + ")";
                MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

                try
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    MessageBox.Show("Registro exitoso");
                    reader.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(e.Message);
                }
            }
            else
            MessageBox.Show("Ingrese todos los datos para poder guardar el registro");

        }
        */
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
            this.textBox4.TabIndex = 3;
            //this.textBox5.TabIndex = 4;
            //this.button1.TabIndex = 5;
            this.button2.TabIndex = 5;
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
            textBox4.Enabled = false ;
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
            textBox4.Enabled = false;
            //textBox5.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            habilitaBotones();
        }

        private void button6_Click(object sender, EventArgs e)
        {

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
            if ((textBox4.Text != "") &&
               (comboBoxPeliculas.Text !="") &&
               (comboBoxSala.Text !="") &&
               (comboBoxHorario.Text != "")
               )
            {
                string horario = (string)comboBoxHorario.Items[comboBoxHorario.SelectedIndex];
                int num_sala = (int)comboBoxSala.Items[comboBoxSala.SelectedIndex];
                int id_pelicula = (int)comboBoxPeliculas.SelectedIndex + 1;
                int precio = int.Parse(textBox4.Text);

                string commandtxt = "INSERT INTO funcion (hora, num_sala, id_pelicula, precio) VALUES ('" + horario + "'," + num_sala + "," + id_pelicula + "," + precio + ")";
                MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

                try
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    MessageBox.Show("Registro exitoso");
                    reader.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }

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
    }
}
