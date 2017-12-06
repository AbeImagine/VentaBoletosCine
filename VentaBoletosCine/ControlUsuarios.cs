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
        int menuChange;
        private Usuario user;
        private DBConnection conexionBD;
        MySqlDataReader reader;
        MySqlDataAdapter DA;
        private int index;

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

            menuChange = 0;
            this.cambioMenu();

            dataGridView1.Visible = false;
            LlenarTablaAuxiliar();
        }

        /*  Evento: button1_Click (Click registro)
         *  Descripción: evento encargado de registrar un usuario nuevo en la Base de Datos.
         */
        private void button1_Click(object sender, EventArgs e)
        {
            if (tbContraseña.Text.Equals(tbConfirmacion.Text) &&
                tbContraseña.Text != "" &&
                tbNombreUsuario.Text != ""
                )
            {
                user.nombreUsusario = tbNombreUsuario.Text;
                user.contraseña = tbContraseña.Text;
                user.permisos = cbPermisos.SelectedIndex;

                if (user.Registrar(conexionBD) == true)
                {
                    MessageBox.Show("Registro exitoso");
                }
                else
                {
                    MessageBox.Show("Error en el registro / Usuario duplicado");
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden");
            }
            this.cambioMenu();
        }

        public void cambioMenu()
        {
            if(menuChange == 0)
            {
                label1.Hide();
                label2.Hide();
                label3.Hide();
                label4.Hide();
                tbConfirmacion.Hide();
                tbContraseña.Hide();
                tbNombreUsuario.Hide();
                cbPermisos.Hide();
                button1.Hide();
                btCancela.Hide();
                btElimina.Show();
                btRegistraN.Show();
                btRegistros.Show();
                menuChange = 1;
            }
            else
            {
                label1.Show();
                label2.Show();
                label3.Show();
                label4.Show();
                tbConfirmacion.Show();
                tbContraseña.Show();
                tbNombreUsuario.Show();
                cbPermisos.Show();
                button1.Show();
                btCancela.Show();
                btElimina.Hide();
                btRegistraN.Hide();
                btRegistros.Hide();
                menuChange = 0;
            }
        }

        private void btRegistraN_Click(object sender, EventArgs e)
        {
            this.cambioMenu();
        }

        private void btElimina_Click(object sender, EventArgs e)
        {
            DataGrid dG = new DataGrid();
            dG.GetData((BindingSource)dataGridView1.DataSource);
            dG.ShowDialog();
            user = new Usuario();

            if (dG.uid != "a")
            {
                if (user.Eliminar(conexionBD, dG.uid) == true)
                {
                    MessageBox.Show("Eliminación exitosa");
                    LlenarTablaAuxiliar();
                }
                else
                {
                    MessageBox.Show("Error en la eliminacion");
                }
            }
        }

        private void btRegistros_Click(object sender, EventArgs e)
        {
            MySqlDataAdapter DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from usuario";
            DA.SelectCommand = new MySqlCommand(sqlSelectAll, conexionBD.Connection);

            DataTable table = new DataTable();
            DA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;

            DataGrid DG = new DataGrid();
            DG.ShowData(bSource);
            DG.Show();
        }

        private void LlenarTablaAuxiliar()
        {
            DA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from usuario";
            DA.SelectCommand = new MySqlCommand(sqlSelectAll, conexionBD.Connection);

            DataTable dataTable = new DataTable();
            BindingSource bS = new BindingSource();
            DA.Fill(dataTable);
            bS.DataSource = dataTable;
            dataGridView1.DataSource = bS;
        }

        private void btCancela_Click(object sender, EventArgs e)
        {
            this.cambioMenu();
        }
    }
}
