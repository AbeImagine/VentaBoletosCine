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

using System.Globalization;
using System.Text.RegularExpressions;


namespace VentaBoletosCine
{
    public partial class membresias : Form
    {
        DBConnection conexionBD;
        MySqlDataReader reader;
        MySqlCommand comando;
        MySqlDataAdapter DA;

        String nombreMembresia;
        String telefono;
        String email;
        String password;
        
        String cuentaNombre ="";
        String cuentaApellidoM="";
        String cuentaApellidoP="";
        String cuentaEmail="";
        String cuentaUsuario="";

        Miembro miembro;

        List<int> lista ;

        private int index;


        //Variable para comprobar que el email es verdadero 
        bool invalid = false;

        public membresias(DBConnection conexion)
        {
            conexionBD = conexion;
            InitializeComponent();
            cbTipoMemb.Items.Add("Normal");
            cbTipoMemb.Items.Add("VIP");
            cbTipoMemb.Items.Add("Súper VIP");

            lista = new List<int>();


            DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from miembro";
            DA.SelectCommand = new MySqlCommand(sqlSelectAll, conexionBD.Connection);

            DataTable dataTable = new DataTable();
            BindingSource bS = new BindingSource();
            DA.Fill(dataTable);
            bS.DataSource = dataTable;
            dataGridView1.DataSource = bS;

            btGuardar.Enabled = false;
            btActualizar.Enabled = false;
        }

        private void membresias_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            maskedTextBox1.Mask = "(###)-(#######)";
            tbApellidoM.CharacterCasing = CharacterCasing.Upper;
            tbApellidoP.CharacterCasing = CharacterCasing.Upper;
            tbNombre.CharacterCasing = CharacterCasing.Upper;

            tbApellidoM.MaxLength = 12;
            tbApellidoP.MaxLength = 12;
            tbNombre.MaxLength = 12; ;

            activaDesactivaTextbox(false);
            
        }


        public void activaDesactivaTextbox(bool interruptor)
        {
            tbApellidoM.Enabled = interruptor;
            tbEmail.Enabled = interruptor;
            tbNombre.Enabled = interruptor;
            tbApellidoP.Enabled = interruptor;
            maskedTextBox1.Enabled = interruptor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((cbTipoMemb.SelectedIndex != -1) &&
                  (tbNombre.Text != "") &&
                  (tbApellidoP.Text != "") &&
                  (maskedTextBox1.Text != "") &&
                  (tbEmail.Text != "") &&
                  (tbApellidoM.Text != "")
                )
            {
                miembro = new Miembro();

                miembro.nombre = tbNombre.Text + " " + tbApellidoP.Text + " " + tbApellidoM.Text;
                miembro.telefono = maskedTextBox1.Text;
                miembro.correo = tbEmail.Text;
                miembro.nivel = cbTipoMemb.SelectedIndex;

                if (miembro.Registrar(conexionBD) == true)
                {
                    MessageBox.Show("Registro exitoso");
                }
            }
            else
                MessageBox.Show("No se pueden dejar campos vacios", "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from miembro";
            DA.SelectCommand = new MySqlCommand(sqlSelectAll, conexionBD.Connection);

            DataTable dataTable = new DataTable();
            BindingSource bS = new BindingSource();
            DA.Fill(dataTable);
            bS.DataSource = dataTable;
            dataGridView1.DataSource = bS;

            DataGrid DG = new DataGrid();
            DG.ShowData(bS);
            DG.Show();
        }

        /*
         * Descripcion: Comprueba que una cadena si tiene un formato de email revisando los dominios
         * de correos electronicos que existen actualmente  
         */ 
        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Usamos IdnMapping para convertir los dominios de email
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                
                return false;
            }

            if (invalid)
            {

                return false;
            }

            // Regresamos true si es verdadero el formato para email
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {

                return false;
            }
        }

        /*
         * Descripcion:  Hace un mapeo de los dominios de correos electronicos para comprobar
         * que el cliente haya igresado un correo que todavía sigue dando servio conexion 
         * a los usuarios 
         */

        private string DomainMapper(Match match)
        {
            
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try{
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }

            return match.Groups[1].Value + domainName;
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

        private void tbApellidoM_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((!char.IsLetter(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Back)))
            {
                MessageBox.Show("Solo esta permitido ingresar letras");
                e.Handled = true;
                return;
            }
            else
                cuentaApellidoM = tbApellidoM.Text;
            

        }

        private void tbApellidoP_KeyPress(object sender, KeyPressEventArgs e)
        {


            if ((!char.IsLetter(e.KeyChar)) && ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Back)))
               
            {
                MessageBox.Show("Solo esta permitido ingresar letras");
                e.Handled = true;
                return;
            }
            else
                cuentaApellidoP = tbApellidoP.Text; 

            
        }
        /*
         * Descripcion : Verifica que solo ingresen numeros al telefono 
         */
        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if ((!char.IsLetter((char)e.KeyValue)) && (((char)e.KeyValue != (char)Keys.Back) && ((char)e.KeyValue != (char)Keys.Back)))
            {
                MessageBox.Show("Solo esta permitido ingresar números");
                e.Handled = true;
                return;
            }

            
        }


        private void button3_Click(object sender, EventArgs e)
        {
            miembro = new Miembro();
            limpiaTodo();
            activaDesactivaTextbox(false);
            btGuardar.Enabled = true;
            btActualizar.Enabled = false;
        }

        /*
         * Descripcion: Limpia todos los textbox, texbox masked y demas elementos 
         */
        public void limpiaTodo()
        {
            tbApellidoM.Clear();
            tbEmail.Clear();
            tbNombre.Clear();
            tbApellidoP.Clear();
            maskedTextBox1.Clear();
            cbTipoMemb.Text = "Sin seleccionar";
           
        }


        /*
       * Descripcion: Revisa que el nombre no sea menor de letres caracteres 
       */
        private void tbNombre_Leave(object sender, EventArgs e)
        {
            int contNombre = cuentaNombre.Length;

            if ((contNombre < 3))
            {
                MessageBox.Show("Nombre demasiado corto");
                tbNombre.Focus();
                 
            }
        }

        /*
         * Descripcion: Revisa que el apellido Materno no sea menor de letres caracteres 
         */
        private void tbApellidoM_Leave(object sender, EventArgs e)
        {
            int contApellidoM = cuentaApellidoM.Length;

            if ((contApellidoM <= 3))
            {
                MessageBox.Show("Apellido materno demasiado corto");
                tbApellidoM.Focus();
            }

        }

        private void tbApellidoP_Leave(object sender, EventArgs e)
        {
            int contApellidoP = cuentaApellidoP.Length;

            if ((contApellidoP <= 3))
            {
                MessageBox.Show("Apellido paterno demasiado corto");
                tbApellidoP.Focus();

            }


        }

        private void tbUsuario_Leave(object sender, EventArgs e)
        {
            int contUsuario = cuentaUsuario.Length;

            if ((contUsuario <= 3))
            {
                MessageBox.Show("Nombre de usuario demasiado corto");
                //tbUsuario.Focus();

            }
        }

        private void tbUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((!char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (!char.IsNumber(e.KeyChar)))
            {
                MessageBox.Show("Solo esta permitido ingresar letras y numeros");
                e.Handled = true;
                return;
            }
                //cuentaUsuario = tbUsuario.Text;
        }

        private void tbEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
                cuentaEmail = tbEmail.Text;
        }

        private void tbEmail_Leave(object sender, EventArgs e)
        {
            int contEmail = cuentaEmail.Length;

            if ((contEmail <= 3) || (contEmail <= 4))
            {
                MessageBox.Show("e-mail demasiado corto");
                tbEmail.Focus();

            }
            else
            {
                if (this.IsValidEmail(Convert.ToString( tbEmail.Text )) == false)
                {
                    MessageBox.Show("Correo electrónico no aceptado, ingrese otro", "Información",
                                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tbEmail.Focus();

                }
                
                    
                    

            }


        }

        private void cbTipoMemb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbTipoMemb.ValueMember != "Sin seleccionar")
            {
                activaDesactivaTextbox(true);
            }
            else
                activaDesactivaTextbox(false);
        }

        private void button4_Click(object sender, EventArgs e)
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
        }

        private void LlenaCampos(int num, string busqueda)
        {
            miembro = new Miembro();
            miembro.Recuperar(conexionBD, num, busqueda);
            string[] nombres = miembro.nombre.Split(' ');

            cbTipoMemb.SelectedIndex = miembro.nivel;
            tbNombre.Text = nombres[0];
            if (nombres.Count() > 1)
            {
                tbApellidoM.Text = nombres[1];
                tbApellidoP.Text = nombres[2];
            }
            tbEmail.Text = miembro.correo;
            maskedTextBox1.Text = miembro.telefono;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (miembro.id_miembro != -1)
            {
                miembro.nivel = cbTipoMemb.SelectedIndex;
                miembro.nombre = tbNombre.Text + " " + tbApellidoP.Text + " " + tbApellidoM.Text;
                miembro.telefono = maskedTextBox1.Text;
                miembro.correo = tbEmail.Text;

                if (miembro.Actualizar(conexionBD) == true)
                {
                    MessageBox.Show("Actualización exitosa");
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            index++;
            if (index == dataGridView1.Rows.Count - 1)
                index = 0;
            ActualizaCampos();
            btActualizar.Enabled = true;
            btGuardar.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
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
            miembro = new Miembro();

            miembro.id_miembro = (int)dataGridView1.Rows[index].Cells[0].Value;
            miembro.nombre = (string)dataGridView1.Rows[index].Cells[1].Value;
            miembro.telefono = dataGridView1.Rows[index].Cells[2].Value.ToString();
            miembro.correo = (string)dataGridView1.Rows[index].Cells[3].Value;
            miembro.nivel = (int)dataGridView1.Rows[index].Cells[4].Value;

            string nom = miembro.nombre;
            string[] nombres = nom.Split(' ');
            tbNombre.Text = nombres[0];
            if (nombres.Count() > 1)
            {
                tbApellidoP.Text = nombres[1];
                tbApellidoM.Text = nombres[2];
            }
            else
            {
                tbApellidoP.Text = "";
                tbApellidoM.Text = "";
            }

            maskedTextBox1.Text = miembro.telefono;
            tbEmail.Text = miembro.correo;
            cbTipoMemb.SelectedIndex = miembro.nivel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataGrid dG = new DataGrid();
            dG.GetData((BindingSource)dataGridView1.DataSource);
            dG.ShowDialog();
            miembro = new Miembro();

            if (dG.id != -1)
            {
                if (miembro.Eliminar(conexionBD, dG.id) == true)
                {
                    MessageBox.Show("Eliminación exitosa");
                }
            }
        }

    }
}
