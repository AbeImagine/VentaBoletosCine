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


        String nombreMembresia;
        String telefono;
        String email;
        String password;

        //Variable para comprobar que el email es verdadero 
        bool invalid = false;

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
            maskedTextBox1.Mask = "(###)-(#######)";
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
                  (tbPass.Text != "") &&
                  (tbApellidoM.Text != "") &&
                  (tbConfPass.Text != "") 
                )
                
            {

                nombreMembresia = tbNombre.Text + " " + tbApellidoM.Text + " " + tbApellidoP.Text;

                telefono = maskedTextBox1.Text;

                if (tbPass.Text == tbConfPass.Text)
                {

                    if ( this.IsValidEmail( Convert.ToString(email)) == true)
                    {
                        MessageBox.Show("Correo electrónico aceptado", "Información", 
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);

                        email = tbEmail.Text;
                    }
                    else
                        MessageBox.Show("Correo electrónico no aceptado", "Información", 
                                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    

                    comando = new MySqlCommand("INSERT INTO miembro (nombre, telefono, correo, contraseña, administrador, usuario) VALUES ('" + nombreMembresia + "'," + telefono + ",'" + email + "','" + tbPass.Text + "'," + cbTipoMemb.SelectedIndex + ", '" + tbUsuario.Text + "')", conexionBD.Connection);
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
                    MessageBox.Show("Las contraseñas no coinciden");
            }
            else
                MessageBox.Show("No se pueden dejar campos vacios", "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MySqlDataAdapter DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from miembro";
            DA.SelectCommand = new MySqlCommand(sqlSelectAll, conexionBD.Connection);

            DataTable table = new DataTable();
            DA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;

            DataGrid DG = new DataGrid();
            DG.ShowData(bSource);
            DG.Show();
        }

/*
 * Descripcion: Comprueba que una cadena si tiene un formato de email revisando los dominios
 * de correos electronicos queexisten actualmente  
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

    }
}
