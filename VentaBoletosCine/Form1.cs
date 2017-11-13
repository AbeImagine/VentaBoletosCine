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
    public partial class Capturista : Form
    {
        DBConnection conexionBD;

        String nombrePelicula;
        String tipoPelicula;
        String categoriaPelicula;
        String duracionPelicula;    
        String creditosRepPelicula;
        String sinopsis;
        MySqlCommand comando;
        MySqlDataReader reader;


        public Capturista(DBConnection conexion)
        {
            conexionBD = conexion;
            InitializeComponent();
        }

        private void Capturista_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            /*
            tbNombre.Text = "Annabelle 2: La Creación";
            tbcategoria.Text = "B15";
            tbTipo.Text = "TERROR";
            tbDuracion.Text = "109 minutos";
            tbSipnosis.Text = "Anabelle 2 sucede varios años después de la trágica muerte de la pequeña hija de un fabricante de muñecas y su esposa, quienes dan albergue en su casa a una monja y a varias niñas de un orfanato clausurado. Al poco tiempo cada uno de ellos se volverá el objetivo de Anabelle, la muñeca poseída creada por el dueño de la casa.";
            tbCreditosRep.Text = " Actores:Talitha Bateman,Stephanie Sigman Directores: David F. Sandberg";
             */
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            limpiaRegistro();
        }

        /*
         * Descripcion: Este método sirva para limpiar todos los textbox y el richtextbox 
         */
        public void limpiaRegistro()
        {
            tbNombre.Clear();
            tbcategoria.Clear();
            tbDuracion.Clear();
            tbSipnosis.Clear();
            tbCreditosRep.Clear();
        }

        /*
         * Descripcion: Este método guarda el registro en la base de datos
         */
        private void button1_Click(object sender, EventArgs e)
        {
            if ((tbNombre.Text != "") &&
                (tbcategoria.Text != "") &&
                (tbDuracion.Text != "") &&
                (tbSipnosis.Text != "")&&
                (tbCreditosRep.Text !="")
             )
            {
                nombrePelicula = tbNombre.Text;
                categoriaPelicula = tbcategoria.Text;
                duracionPelicula = tbDuracion.Text;
                creditosRepPelicula = tbCreditosRep.Text;
                sinopsis =tbSipnosis.Text ;

                this.Refresh();


                comando = new MySqlCommand("INSERT INTO pelicula (nombre, duracion, genero, sinopsis, reparto) VALUES ('" + nombrePelicula + "'," + duracionPelicula + ",'" + categoriaPelicula + "','" + sinopsis + "','" + creditosRepPelicula + "')", conexionBD.Connection);
                try
                {
                    reader = comando.ExecuteReader();
                    MessageBox.Show("Registro exitoso");
                    reader.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

            }
            else
                MessageBox.Show("Ingrese todos los campos","No se puedo guardar",
                                MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        /*
         * Descripcion: Este método muestra los registros de la base de datos
         */
        private void button5_Click(object sender, EventArgs e)
        {
            MySqlDataAdapter DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from pelicula";
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
