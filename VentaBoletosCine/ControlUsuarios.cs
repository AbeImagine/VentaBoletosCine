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
    /*  Clase: ControlUsuarios
     *  Descripción: clase encargada de el registro de nuevos usuarios en el sistema.
     *  Atributos:
     *      user: variable auxiliar de Usuario para la inserción de datos.
     *      conexiónBD: objeto concetor con la Base de Datos.
     */
    public partial class ControlUsuarios : Form
    {
        private Usuario user;
        private DBConnection conexionBD;

        /*  Constructor: ControlUsuarios.
         *  Descripcion: constructor de la clase de control de usuarios.
         *  Parámetros:
         *      conexion: objeto conector con la Base de Datos.
         */
        public ControlUsuarios(DBConnection conexion)
        {
            conexionBD = conexion;
            user = new Usuario();
            InitializeComponent();
            cbPermisos.Items.Add("Auxiliar");
            cbPermisos.Items.Add("Administrador");
            cbPermisos.Items.Add("Supervisor");
        }

        /*  Evento: button1_Click (Click registro)
         *  Descripción: evento encargado de registrar un usuario nuevo en la Base de Datos.
         */
        private void button1_Click(object sender, EventArgs e)
        {
            if (tbContraseña.Text.Equals(tbConfirmacion.Text))
            {
                user.nombreUsusario = tbNombreUsuario.Text;
                user.contraseña = tbContraseña.Text;
                user.permisos = cbPermisos.SelectedIndex;

                if (user.Registrar(conexionBD) == true)
                {
                    MessageBox.Show("Registro exitoso");
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden");
            }
        }
    }
}
