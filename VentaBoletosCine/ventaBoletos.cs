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
    public partial class ventaBoletos : Form
    {

        List<Label> listaetiquetas;
        DBConnection conexionBD;
        MySqlDataReader reader;
        private Funcion func;
        private Pelicula peli;
        private Venta venta;

        
        double efectivo = 0;
        double total=0;
        double precioBotelo = 50;
        double cambio = 0;

        public ventaBoletos(DBConnection conexion)
        {
            listaetiquetas = new List<Label>();

            conexionBD = conexion;
            InitializeComponent();


            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);

            // Enable timer.
            timer1.Enabled = true;

            FillComboBoxFunc();
        }

        private void FillComboBoxFunc()
        {
            string commandtxt = "SELECT id_funcion from funcion";

            MySqlCommand command = new MySqlCommand(commandtxt, conexionBD.Connection);

            try
            {
                reader = command.ExecuteReader();
                int id_func;
                while (reader.Read())
                {
                    id_func = reader.GetInt32(0);
                    cbIdFunc.Items.Add(id_func);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                reader.Close();
            }
        }

        public int generaNumBoleto()
        {
            Random random = new Random();
            int numBoleto = random.Next(); ;
            return numBoleto;
        }

        private void ventaBoletos_Load(object sender, EventArgs e)
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            timer1.Start();

            tbFecha.ReadOnly = true;
            tbHora.ReadOnly = true;
            tbNumBoleto.ReadOnly = true;
            tbNumBoleto.Text = "Sin número";
            tbNumasiento.Text = "Sin seleccionar";

            this.DoubleBuffered = true;
            tbFecha.Text = DateTime.Now.ToShortTimeString();
            tbHora.Text = DateTime.Now.ToShortDateString();
            llenaLista();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Nombre_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      

        private void timer1_Tick(object sender, EventArgs e)
        {
            tbFecha.Text = DateTime.Now.ToShortDateString();
            tbHora.Text = DateTime.Now.ToShortTimeString();
        }

        /*
         * Limpia los textbox y regresa el combo boz a su estado inicial
         */
        public void limpiaRegistro()
        {
            cbIdFunc.Text = "Número de función";
            tbDuracion.Clear();
            tbNombrePeli.Clear();
            tbNumasiento.Clear();
            tbNumBoleto.Clear();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            boleto boleto = new boleto(conexionBD, listaetiquetas);
            boleto.eventoPasaNumBoleto += new boleto.delegadoPasaDato(BuscaAsiento);
            boleto.ShowDialog();
        }

        /*
         * Se llena la lista con todas las etiquetas que son los asiento de las salas de cine 
         */
        public void llenaLista()
        {
            listaetiquetas.Add(lb1);
            listaetiquetas.Add(lb2);
            listaetiquetas.Add(lb3);
            listaetiquetas.Add(lb4);
            listaetiquetas.Add(lb5);
            listaetiquetas.Add(lb6);
            listaetiquetas.Add(lb7);
            listaetiquetas.Add(lb8);
            listaetiquetas.Add(lb9);
            listaetiquetas.Add(lb10);

            listaetiquetas.Add(lb11);
            listaetiquetas.Add(lb12);
            listaetiquetas.Add(lb13);
            listaetiquetas.Add(lb14);
            listaetiquetas.Add(lb15);
            listaetiquetas.Add(lb16);
            listaetiquetas.Add(lb17);
            listaetiquetas.Add(lb18);
            listaetiquetas.Add(lb19);
            listaetiquetas.Add(lb20);

            listaetiquetas.Add(lb21);
            listaetiquetas.Add(lb22);
            listaetiquetas.Add(lb23);
            listaetiquetas.Add(lb24);
            listaetiquetas.Add(lb25);
            listaetiquetas.Add(lb26);
            listaetiquetas.Add(lb27);
            listaetiquetas.Add(lb28);
            listaetiquetas.Add(lb29);
            listaetiquetas.Add(lb30);

            listaetiquetas.Add(lb31);
            listaetiquetas.Add(lb32);

        }

        /*
         *Descripcion: Cambia de color la etiqueta que representa el numero de asiento  
         */
        public void BuscaAsiento(int numAsiento)
        {
            if (listaetiquetas[numAsiento].BackColor == Color.Red)
            {
                MessageBox.Show("Asiento ocupado");
            }
            else
            {
                listaetiquetas[numAsiento].BackColor = Color.Red;
                tbNumasiento.Text = Convert.ToString(numAsiento + 1);
                tbNumBoleto.Text = Convert.ToString(generaNumBoleto());
            }
              
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(tbNumBoleto.Text, tbNumasiento.Text, cbIdFunc.Text, tbPrecio.Text);
            total = (dataGridView1.Rows.Count - 1) * precioBotelo;
            tbTotal.Text = Convert.ToString(total.ToString("C"));

            if (total == 0)
            {
                cambio = 0;
                tbCambio.Text = Convert.ToString(cambio.ToString("C"));
            }


        }

        private void bt500_Click(object sender, EventArgs e)
        {
            if (total >=50 || total>=500)
            {
                efectivo = 500;
                DialogResult dialogResult = MessageBox.Show("¿Desea realizar esta operación?", "Información", MessageBoxButtons.YesNo);
                tbEfectivo.Text = Convert.ToString(efectivo.ToString("C"));
                if (dialogResult == DialogResult.Yes)
                {
                    checaTotal();
                }
                else if (dialogResult == DialogResult.No)
                {
                    efectivo = 0;
                    tbEfectivo.Text = Convert.ToString(efectivo.ToString("C"));
                }
            }
            else
                MessageBox.Show("No se ha registrado ningún boleto");
        }

        public void checaTotal()
        {
            int precioTotal = (int)total;
            venta = new Venta();
            tbEfectivo.Text = Convert.ToString(efectivo.ToString("C"));
            cambio = efectivo - total;
            tbCambio.Text = Convert.ToString(cambio.ToString("C"));
            total = total - efectivo;
            if (total <= 0)
            {
                /*
                venta.id_funcion = func.id_funcion;
                venta.precio = precioTotal;
                ui
                venta.Registrar(conexionBD);
                 */

                total = 0;
                dataGridView1.Rows.Clear();
                efectivo = 0;
                tbEfectivo.Text = Convert.ToString(efectivo.ToString("C"));
            }
            tbTotal.Text = Convert.ToString(total.ToString("C"));

            if (cambio < 0 || cambio <= 0)
            {
                cambio = 0;
                tbCambio.Text = Convert.ToString( cambio.ToString("C"));
            }
        }


        private void bt200_Click(object sender, EventArgs e)
        {
            if (total >= 50 )
            {
                efectivo = 200;
                DialogResult dialogResult = MessageBox.Show("¿Desea realizar esta operación?", "Información", MessageBoxButtons.YesNo);
                tbEfectivo.Text = Convert.ToString(efectivo.ToString("C"));
                if (dialogResult == DialogResult.Yes)
                {

                    checaTotal();
                }
                else if (dialogResult == DialogResult.No)
                {
                    efectivo = 0;
                    tbEfectivo.Text = Convert.ToString(efectivo.ToString("C") );
                }
            }
            else
                MessageBox.Show("No se ha registrado ningún boleto");
        }

        private void bt100_Click(object sender, EventArgs e)
        {
            if (total >= 50)
            {
                efectivo = 100;
                DialogResult dialogResult = MessageBox.Show("¿Desea realizar esta operación?", "Información", MessageBoxButtons.YesNo);
                tbEfectivo.Text = Convert.ToString(efectivo.ToString("C"));
                if (dialogResult == DialogResult.Yes)
                {
                    checaTotal();
                }
                else if (dialogResult == DialogResult.No)
                {
                    efectivo = 0;
                    //tbEfectivo.Text = Convert.ToString(efectivo.ToString("C"));
                }
            }
            else
                MessageBox.Show("No se ha registrado ningún boleto");

        }

        private void bt50_Click(object sender, EventArgs e)
        {
           if (total != 0)
           {
                efectivo = 50;
                DialogResult dialogResult = MessageBox.Show("¿Desea realizar esta operación?", "Información", MessageBoxButtons.YesNo);
                tbEfectivo.Text = Convert.ToString(efectivo.ToString("C"));
                if (dialogResult == DialogResult.Yes)
                {
                    checaTotal();
                }
                else if (dialogResult == DialogResult.No)
                {
                    efectivo = 0;
                    tbEfectivo.Text = Convert.ToString( efectivo.ToString("C"));
                }
            }
            else
                MessageBox.Show("No se ha registrado ningún boleto");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 1)
            {
                MessageBox.Show("No se ha registrado ningun boleto");
            }
            else
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                total = (dataGridView1.Rows.Count - 1) * precioBotelo;
                tbTotal.Text = Convert.ToString( total.ToString("C"));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 1)
            {
                MessageBox.Show("No se ha registrado ningun boleto");
            }
            else
            {
            }
        }

        private void cbNumSala_SelectedIndexChanged(object sender, EventArgs e)
        {
            func = new Funcion();
            peli = new Pelicula();

            int id_func = (int)(cbIdFunc.Items[cbIdFunc.SelectedIndex]);

            if (func.Recuperar(conexionBD, id_func) == true)
            {
                if (peli.Recuperar(conexionBD, func.id_pelicula) == true)
                {
                    //MessageBox.Show("Registro exitoso");
                    func.pelicula = peli;
                }
            }
            else
            {
                MessageBox.Show("No se pudo recuperar la información");
            }

            tbDuracion.Text = func.pelicula.duracion.ToString();
            tbNombrePeli.Text = func.pelicula.nombre;
            tbPrecio.Text = func.precio.ToString();
        }
    }
}
