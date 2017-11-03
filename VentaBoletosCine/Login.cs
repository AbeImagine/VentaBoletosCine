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

    public partial class Login : Form
    {
        DBConnection conexionBD;
        MySqlCommand comando;
        MySqlDataAdapter adaptadorDatos;
        MySqlDataReader reader;
        String query;

        public Login()
        {
            InitializeComponent();
            IniciarConexion();
        }

        /* Descripción: Hace la conexion con la base de datos he informa al usuario si se logro 
         * la conexión 
         * */
        private void IniciarConexion()
        {
            conexionBD = DBConnection.Instance();
            conexionBD.DatabaseName = "bdcine";
            comando = new MySqlCommand();

            if (conexionBD.IsConnected())
            {
                MessageBox.Show("Conexion lograda");
            }
            else
            {
                MessageBox.Show("Conexion fallida");
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        
         /*Descripción del método: Este método sirve para la autentificación y el acceso de usuarios
         *mediante la base de datos haciendo conexión en la tabla miembros en la cual se ub
          *ican los los campos de nombre de usuario y contraseña, también valida que el nombre de usuario
          *y contraseña esten correctos.
         */
        public void login()
        {
            if ((textBox1.Text != "") &&
                 (textBox2.Text != ""))
            {
                string usuario = textBox1.Text;
                string contraseña = textBox2.Text;
                query = "SELECT * FROM Miembro WHERE usuario = '" + usuario + "'";
                comando = new MySqlCommand(query, conexionBD.Connection);

                try
                {
                    reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader.GetString("contraseña").Equals(contraseña))
                        {
                            MessageBox.Show("Has accesado al sistema");
                            bool b = reader.GetBoolean("administrador");
                            Menu ventana = new Menu(conexionBD, b);
                            ventana.Show();
                        }
                        else
                        {
                            MessageBox.Show("Contraseña incorrecta");
                        }
                    }
                    reader.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            else
                MessageBox.Show("Ingresar nombre y contraseña",
                                "Error de autentificación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }

        
        private void Login_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                login();
            }
            if ((int)e.KeyChar == (int)Keys.Escape)
            {
                this.Close();
            }
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Escape)
            {
                this.Close();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
