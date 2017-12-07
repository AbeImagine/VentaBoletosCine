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
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace VentaBoletosCine
{
    public partial class reporteVenta : Form
    {
        private MySqlCommand command;
        private MySqlDataReader reader;
        private DBConnection conexionBD;

        public reporteVenta(DBConnection conexion, Usuario user)
        {
            conexionBD = conexion;
            InitializeComponent();
            LlenarComboBox();
        }

        private void LlenarComboBox()
        {
            cbClasificacion.Items.Add("Función");
            cbClasificacion.Items.Add("Pelicula");
            cbClasificacion.Items.Add("Sala");
        }

        private void cbClasificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbClasificacion.SelectedIndex)
            {
                case 0:
                    label2.Text = "Función: ";
                    RecuperaFunciones();
                    break;
                case 1:
                    label2.Text = "Pelicula: ";
                    RecuperaPeliculas();
                    break;
                case 2:
                    label2.Text = "Sala: ";
                    RecuperaSalas();
                    break;
            }
        }

        private void RecuperaSalas()
        {
            string commandtxt = "SELECT num_sala FROM sala";

            command = new MySqlCommand(commandtxt, conexionBD.Connection);

            cbSeleccion.Items.Clear();
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cbSeleccion.Items.Add(reader.GetInt32(0));
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void RecuperaPeliculas()
        {
            string commandtxt = "SELECT nombre FROM pelicula";

            command = new MySqlCommand(commandtxt, conexionBD.Connection);

            cbSeleccion.Items.Clear();
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cbSeleccion.Items.Add(reader.GetString(0));
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void RecuperaFunciones()
        {
            string commandtxt = "SELECT id_funcion FROM funcion";

            command = new MySqlCommand(commandtxt, conexionBD.Connection);

            cbSeleccion.Items.Clear();
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cbSeleccion.Items.Add(reader.GetInt32(0));
                }
                reader.Close();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrearPDF();
        }

        private void CrearPDF(){
            List<string> registrosVenta;
            string filename;

            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Reporte de venta " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            PdfPage page = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 15, XFontStyle.Bold);

            registrosVenta = ObtenerVentas();
            for (int i = 0; i < registrosVenta.Count; i++)
            {
                graph.DrawString(registrosVenta[i], font, XBrushes.Black, new XRect(30, 50 + i*20, page.Width.Point, page.Height.Point), XStringFormat.TopLeft);
            }
            SaveFileDialog sfD = new SaveFileDialog();
            sfD.Filter = "Archivo PDF|*.pdf";
            DialogResult a = sfD.ShowDialog();
            if (a == DialogResult.OK)
            {
                filename = sfD.FileName;
                pdf.Save(filename);
            }
        }

        private List<string> ObtenerVentas()
        {
            List<string> registros = new List<string>();

            switch (cbClasificacion.SelectedIndex)
            {
                case 0:
                    registros = VentasdeFuncion();
                    registros.Insert(0, "Ventas de la funcion " + (int)cbSeleccion.Items[cbSeleccion.SelectedIndex]);
                    break;

                case 1:
                    registros = VentasdePelicula();
                    registros.Insert(0, "Ventas de la pelicula " + (string)cbSeleccion.Items[cbSeleccion.SelectedIndex]);
                    break;
                case 2:
                    registros = VentasdeSala();
                    registros.Insert(0, "Ventas de la sala " + (int)cbSeleccion.Items[cbSeleccion.SelectedIndex]);
                    break;
            }
            registros.Insert(0, "Reporte de ventas del " + DateTime.Now.ToShortDateString() + "\nReporte generado a las: " + DateTime.Now.ToShortTimeString());
            return registros;
        }

        private List<string> VentasdeSala()
        {
            List<string> registros = new List<string>();
            List<int> listAux = new List<int>();
            int iAux = (int)cbSeleccion.Items[cbSeleccion.SelectedIndex]; ;
            
            string commandTxt = "SELECT id_funcion FROM funcion WHERE num_sala = " + iAux;
            command = new MySqlCommand(commandTxt, conexionBD.Connection);
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listAux.Add(reader.GetInt32(0));
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            foreach (int i in listAux)
            {
                commandTxt = "SELECT * FROM venta WHERE id_funcion = " + i;
                command = new MySqlCommand(commandTxt, conexionBD.Connection);
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string aux = "Venta " + reader.GetInt32("id_venta") + "\n" + reader.GetInt32("precio").ToString("C") + " vendido por el usuario "+reader.GetString("usuario_venta");
                        registros.Add(aux);
                    }
                    reader.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            return registros;
        }

        private List<string> VentasdePelicula()
        {
            List<string> registros = new List<string>();
            string peli = (string)cbSeleccion.Items[cbSeleccion.SelectedIndex];
            int iAux = 0;
            List<int> listAux = new List<int>();
            string commandTxt = "SELECT id_pelicula FROM pelicula WHERE nombre = '" + peli + "'";
            command = new MySqlCommand(commandTxt, conexionBD.Connection);
            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    iAux = reader.GetInt32(0);
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            commandTxt = "SELECT id_funcion FROM funcion WHERE id_pelicula = " + iAux;
            command = new MySqlCommand(commandTxt, conexionBD.Connection);
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listAux.Add(reader.GetInt32(0));
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            foreach (int i in listAux)
            {
                commandTxt = "SELECT * FROM venta WHERE id_funcion = " + i;
                command = new MySqlCommand(commandTxt, conexionBD.Connection);
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string aux = "Venta " + reader.GetInt32("id_venta") + "\n" + reader.GetInt32("precio").ToString("C") + " vendido por el usuario " + reader.GetString("usuario_venta"); ;
                        registros.Add(aux);
                    }
                    reader.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            return registros;
        }

        private List<string> VentasdeFuncion()
        {
            List<string> registros = new List<string>();
            int funcion = (int)cbSeleccion.Items[cbSeleccion.SelectedIndex];

            string commandTxt = "SELECT * FROM venta WHERE id_funcion = " + funcion;
            command = new MySqlCommand(commandTxt, conexionBD.Connection);
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string aux = "Venta " + reader.GetInt32("id_venta") + "\n" + reader.GetInt32("precio").ToString("C") + " vendido por el usuario " + reader.GetString("usuario_venta"); ;
                    registros.Add(aux);
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return registros;
        }
    }
}
