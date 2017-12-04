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
    public partial class ControlUsuarios : Form
    {
        private Usuario user;
        private DBConnection conexionBD;

        public ControlUsuarios(DBConnection conexion)
        {
            conexionBD = conexion;
            user = new Usuario();
            InitializeComponent();
            cbPermisos.Items.Add("Auxiliar");
            cbPermisos.Items.Add("Administrador");
            cbPermisos.Items.Add("Supervisor");
        }

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
