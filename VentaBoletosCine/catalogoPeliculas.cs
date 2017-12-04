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

    /*  Clase: Capturista.
     *  Descripción: Clase encargada de la captura de información de peliculas.
     *  Atributos:
     *      conexionBD: objeto conector con la Base de Datos.
     *      comando: objeto contenedor de comandos SQL.
     *      reader: objeto lector de Base de Datos SQL.
     *      DA: objeto adaptador de datos de SQL.
     *      pelicula: objeto contenedor de la información de Pelicula.
     *      index: indice señalador de registro en navegación.
     */
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
        
        /*  Constructor: Capturista.
         *  Descrpición: constructor de objeto capturista.
         *  Parámetros:
         *      conexion: objeto conector con la Base de Datos de SQL,
         * 
         */
        public Capturista(DBConnection conexion)
        {
            index = -1;
            conexionBD = conexion;
            InitializeComponent();
            dataGridView1.Visible = false;
            LlenarTablaAuxiliar();
            btGuardar.Enabled = false;
            btActualizar.Enabled = false;
        }

        /*  Método: LlenarTablaAuxiliar.
         *  Descrpición: método encargado de llenar la tabla de registros para auxiliar en la navegación.
         * 
         */
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
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        /*  Evento: button2_Click (Click Eliminacion)
         *  Descrpición: evento encargado de la eliminacion en la Base de Datos.
         * 
         */
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

        /*  Evento: button3_Click (Click Nuevo)
         *  Descrpición: evento encargado de la generación de un nuevo registro.
         * 
         */
        private void button3_Click(object sender, EventArgs e)
        {
            pelicula = new Pelicula();
            limpiaRegistro();
            btGuardar.Enabled = true;
            btActualizar.Enabled = false;
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

        /*  Evento: button1_Click (Click Guardar)
         *  Descrpición: evento encargado de el registro de datos en la Base de Datos.
         * 
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

        /*  Evento: button6_Click (Click Mostrar Registros)
         *  Descrpición: evento encargado de mostrar los registros que actualmente se encuentran en la Base de Datos
         * 
         */
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

        /*  Evento: tbDuracion_KeyPress (Presion de tecla en tbDuracion)
         *  Descrpición: evento encargado de hacer validación de caracteres
         * 
         */
        private void tbDuracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Back)))
            {
                MessageBox.Show("Solo esta permitido ingresar números");
                e.Handled = true;
                return;
            }
            cuentaDuracion = tbDuracion.Text;
        }

        /*  Evento: tbNombre_KeyPress (Presion de tecla en tbNombre)
         *  Descrpición: evento encargado de hacer validación de caracteres
         * 
         */
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

        /*  Evento: tbcategoria_KeyPress (Presion de tecla en tbcategoria)
         *  Descrpición: evento encargado de hacer validación de caracteres
         * 
         */
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

        /*  Evento: tbSinopsis_KeyPress (Presion de tecla en tbSinopsis)
         *  Descrpición: evento encargado de hacer validación de caracteres
         * 
         */
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

        /*  Evento: tbSinopsis_Leave (Abandonar tbSinopsis)
         *  Descrpición: evento encargado de hacer validación de número de caracteres.
         * 
         */
        private void tbSipnosis_Leave(object sender, EventArgs e)
        {
            int contSip = cuentaSinopsis.Length;

            if (contSip >= tbSipnosis.MaxLength - 2)
            {
                MessageBox.Show("Solo esta permitido ingresar " + Convert.ToString(tbSipnosis.MaxLength-2)+ " cáracteres");
                tbSipnosis.Focus();
            }
            
        }


        /*  Evento: tbCreditosRep_KeyPress (Presion de tecla en tbCreditosRep)
         *  Descrpición: evento encargado de hacer validación de caracteres
         * 
         */
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

        /*  Evento: tbCreditosRep_Leave (Abandonar tbCreditosRep)
         *  Descrpición: evento encargado de hacer validación de número de caracteres.
         * 
         */
        private void tbCreditosRep_Leave(object sender, EventArgs e)
        {
            int contCreditos = cuentaCredReparto.Length;

            if ( contCreditos >=  tbCreditosRep.MaxLength - 2)
            {
                MessageBox.Show("Solo esta permitido ingresar " + Convert.ToString( tbCreditosRep.MaxLength-2 ) + " cáracteres");
                tbCreditosRep.Focus();
            }
        }

        /*  Evento: button4_Click (Click buscar)
         *  Descrpición: evento encargado de llamar la pantalla de busqueda.
         * 
         */
        private void button4_Click_1(object sender, EventArgs e)
        {
            Buscador busca;
            busca = new Buscador(conexionBD, "miembro");
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

        /*  Método: LlenaCampos.
         *  Descrpición: método encargado de rellenar los textBox con los valores recuperados de busqueda.
         * 
         */
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

        /*  Evento: button8_Click (Click siguiente)
         *  Descrpición: evento encargado de moverse al siguiente registro en la navegación.
         * 
         */
        private void button8_Click(object sender, EventArgs e)
        {
            index++;
            if (index == dataGridView1.Rows.Count - 1)
                index = 0;
            ActualizaCampos();
            btActualizar.Enabled = true;
            btGuardar.Enabled = false;
        }

        /*  Método: ActualizarCampos
         *  Descripción: método encargado de actualizar campos de acuerdo al registro de navegación.
         */
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

        /*  Evento: btAnterior_Click (Click anterior)
         *  Descrpición: evento encargado de moverse al registro anterior en la navegación.
         * 
         */
        private void btAnterior_Click(object sender, EventArgs e)
        {
            index--;
            if (index < 0)
                index = dataGridView1.Rows.Count - 2;
            ActualizaCampos();
            btActualizar.Enabled = true;
            btGuardar.Enabled = false;
        }

        /*  Evento: btPrimero_Click (Click primero)
         *  Descrpición: evento encargado de moverse al primer registro en la navegación.
         * 
         */
        private void btPrimero_Click(object sender, EventArgs e)
        {
            index = 0;
            ActualizaCampos();
            btActualizar.Enabled = true;
            btGuardar.Enabled = false;
        }

        /*  Evento: btUltimo_Click (Click primero)
         *  Descrpición: evento encargado de moverse al último registro en la navegación.
         * 
         */
        private void btUltimo_Click(object sender, EventArgs e)
        {
            index = dataGridView1.Rows.Count - 2;
            ActualizaCampos();
            btActualizar.Enabled = true;
            btGuardar.Enabled = false;
        }

        /*  Evento: button5_Click (Click actualización)
         *  Descrpición: evento encargado de actualizar un registro de la Base de Datos.
         * 
         */
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
