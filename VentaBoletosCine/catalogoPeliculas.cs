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
        String categoriaPelicula;
        String duracionPelicula;    
        String creditosRepPelicula;
        String sinopsis;
        MySqlCommand comando;
        MySqlDataReader reader;
        MySqlDataAdapter DA;


        String cuentaDuracion="";
        String cuentaNombre="";
        String cuentaGenero="";
        String cuentaSinopsis="";
        String cuentaCredReparto="";

        private Pelicula pelicula;
        private int index;
        
        public Capturista(DBConnection conexion)
        {
            index = -1;
            conexionBD = conexion;
            InitializeComponent();
            dataGridView1.Visible = false;
            LlenarTablaAuxiliar();
        }

        private void LlenarTablaAuxiliar()
        {
            DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from pelicula";
            DA.SelectCommand = new MySqlCommand(sqlSelectAll, conexionBD.Connection);

            DataTable dataTable = new DataTable();
            BindingSource bS = new BindingSource();
            DA.Fill(dataTable);
            bS.DataSource = dataTable;
            dataGridView1.DataSource = bS;
        }

        /*
         * Descripcion: Este metodo sirve para configurar la longitud de los datos
         * que se almacenan en la base de datos
         */
        public void configuraTamTexBox()
        {
            tbNombre.MaxLength = 14;
            tbDuracion.MaxLength = 3;
            tbSipnosis.MaxLength = 100;
            tbcategoria.MaxLength = 16;
            tbCreditosRep.MaxLength = 50;
            
        }
        private void Capturista_Load(object sender, EventArgs e)
        {

            configuraTamTexBox();
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
            DataGrid dG = new DataGrid();
            dG.GetData((BindingSource)dataGridView1.DataSource);
            dG.ShowDialog();
            pelicula = new Pelicula();

            if (dG.id != -1)
            {
                if (pelicula.Eliminar(conexionBD, dG.id) == true)
                {
                    MessageBox.Show("Eliminación exitosa");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pelicula = new Pelicula();
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
                pelicula = new Pelicula();

                pelicula.nombre = nombrePelicula;
                pelicula.genero = categoriaPelicula;
                pelicula.duracion = int.Parse(duracionPelicula);
                pelicula.reparto = creditosRepPelicula;
                pelicula.sinopsis = sinopsis;

                if (pelicula.Registrar(conexionBD) == true)
                {
                    MessageBox.Show("Registro exitoso");
                }
                else
                {
                    MessageBox.Show("Error de registro");
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

        private void button6_Click(object sender, EventArgs e)
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

        private void tbNombre_KeyDown(object sender, KeyEventArgs e)
        {

        }

        

        

        private void tbDuracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Back)))
            {
                MessageBox.Show("Solo esta permitido ingresar números");
                e.Handled = true;
                return;
            }
            cuentaDuracion = tbDuracion.Text;
        }

        private void tbNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Back)))
            {
                MessageBox.Show("Solo esta permitido ingresar letras");
                e.Handled = true;
                return;
            }
            cuentaNombre = tbNombre.Text;
        }

        private void tbcategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Back)))
            {
                MessageBox.Show("Solo esta permitido ingresar letras");
                e.Handled = true;
                return;
            }
            cuentaGenero = tbcategoria.Text;


        }

        private void tbSipnosis_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((!char.IsLetter(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back) && e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo esta permitido ingresar letras");
                e.Handled = true;
                return;
            }
            cuentaSinopsis = tbSipnosis.Text;
            
        }

        private void tbSipnosis_Leave(object sender, EventArgs e)
        {
            int contSip = cuentaSinopsis.Length;

            if (contSip >= tbSipnosis.MaxLength - 2)
            {
                MessageBox.Show("Solo esta permitido ingresar " + Convert.ToString(tbSipnosis.MaxLength-2)+ " cáracteres");
                tbSipnosis.Focus();
            }
            
        }

        private void tbCreditosRep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back) && e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo esta permitido ingresar letras");
                e.Handled = true;
                return;
            }
            cuentaCredReparto = tbCreditosRep.Text;
        }

        private void tbCreditosRep_Leave(object sender, EventArgs e)
        {
            int contCreditos = cuentaCredReparto.Length;

            if ( contCreditos >=  tbCreditosRep.MaxLength - 2)
            {
                MessageBox.Show("Solo esta permitido ingresar " + Convert.ToString( tbCreditosRep.MaxLength-2 ) + " cáracteres");
                tbCreditosRep.Focus();
            }
        }

        private void tbSipnosis_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Buscador busca;
            busca = new Buscador(conexionBD, "miembro");
            busca.LlenarComboBox((BindingSource)dataGridView1.DataSource);
            busca.ShowDialog();
            if (busca.id != -1)
            {
                LlenaCampos(busca.id, busca.busqueda);
            }
            else
                MessageBox.Show("Registro no encontrado");
        }

        private void LlenaCampos(int opcion, string busqueda)
        {
            pelicula = new Pelicula();
            pelicula.Recuperar(conexionBD, opcion, busqueda);

            tbNombre.Text = pelicula.nombre;
            tbDuracion.Text = pelicula.duracion.ToString();
            tbCreditosRep.Text = pelicula.reparto;
            tbSipnosis.Text = pelicula.sinopsis;
            tbcategoria.Text = pelicula.genero;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            index++;
            if (index == dataGridView1.Rows.Count - 1)
                index = 0;
            ActualizaCampos();
        }

        private void ActualizaCampos()
        {
            pelicula = new Pelicula();

            pelicula.nombre = (string)dataGridView1.Rows[index].Cells[1].Value;
            pelicula.id_pelicula = (int)dataGridView1.Rows[index].Cells[0].Value;
            pelicula.genero = (string)dataGridView1.Rows[index].Cells[3].Value;
            pelicula.duracion = (int)dataGridView1.Rows[index].Cells[2].Value;
            pelicula.sinopsis = (string)dataGridView1.Rows[index].Cells[4].Value;
            pelicula.reparto = (string)dataGridView1.Rows[index].Cells[5].Value;

            tbNombre.Text = pelicula.nombre;
            tbDuracion.Text = pelicula.duracion.ToString();
            tbcategoria.Text = pelicula.genero;
            tbSipnosis.Text = pelicula.sinopsis;
            tbCreditosRep.Text = pelicula.reparto;
        }

        private void btAnterior_Click(object sender, EventArgs e)
        {
            index--;
            if (index < 0)
                index = dataGridView1.Rows.Count - 2;
            ActualizaCampos();
        }

        private void btPrimero_Click(object sender, EventArgs e)
        {
            index = 0;
            ActualizaCampos();
        }

        private void btUltimo_Click(object sender, EventArgs e)
        {
            index = dataGridView1.Rows.Count - 2;
            ActualizaCampos();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (pelicula.id_pelicula != -1)
            {
                pelicula.nombre = tbNombre.Text;
                pelicula.genero = tbcategoria.Text;
                pelicula.duracion = int.Parse(tbDuracion.Text);
                pelicula.sinopsis = tbSipnosis.Text;
                pelicula.reparto = tbCreditosRep.Text;

                if (pelicula.Actualizar(conexionBD) == true)
                {
                    MessageBox.Show("Actualización exitosa");
                }
            }
        }
    }
}
