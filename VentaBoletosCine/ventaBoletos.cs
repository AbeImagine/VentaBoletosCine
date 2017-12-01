using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VentaBoletosCine
{
    public partial class ventaBoletos : Form
    {

        List<Label> listaetiquetas;
        DBConnection conexionBD;

        int numAsiento;
        float efectivo = 0;
        float total;
       
        public ventaBoletos(DBConnection conexion)
        {
            listaetiquetas = new List<Label>();

            conexionBD = conexion;
            InitializeComponent();


            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);

            // Enable timer.
            timer1.Enabled = true;
        }

        public int generaNumBoleto()
        {
            Random random = new Random();

            int numBoleto = random.Next(); ;

            return numBoleto;

            

        }

        private void ventaBoletos_Load(object sender, EventArgs e)
        {

            timer1.Start();

            tbFecha.ReadOnly = true;
            tbHora.ReadOnly = true;
            tbNumBoleto.ReadOnly = true;
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
            tbFecha.Text = DateTime.Now.ToShortTimeString();
            tbHora.Text = DateTime.Now.ToShortDateString();
        }

        /*
         * Limpia los textbox y regresa el combo boz a su estado inicial
         */
        public void limpiaRegistro()
        {
            comboBox1.Text = "Número de Sala";
            tbDuracion.Clear();
            tbNombrePeli.Clear();
            tbNumasiento.Clear();
            tbNumBoleto.Clear();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            limpiaRegistro();
            boleto boleto = new boleto(conexionBD);
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
            listaetiquetas.Add(lb33);
            listaetiquetas.Add(lb34);
            listaetiquetas.Add(lb35);
            listaetiquetas.Add(lb36);
            listaetiquetas.Add(lb37);
            listaetiquetas.Add(lb38);
            listaetiquetas.Add(lb39);
            listaetiquetas.Add(lb40);
        }

        /*
         *Descripcion: Cambia de color la etiqueta que representa el numero de asiento  
         */
        public void BuscaAsiento(int numAsiento)
        {
            listaetiquetas[numAsiento].BackColor = Color.Red;
            tbNumasiento.Text = Convert.ToString( numAsiento+1 );
        }

        private void button5_Click(object sender, EventArgs e)
        {
            

        }

        private void bt500_Click(object sender, EventArgs e)
        {
            if (total != 0)
            {
                DialogResult dialogResult = MessageBox.Show("¿Desea realizar esta operación?", "Información", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    efectivo = 500;
                    tbEfectivo.Text = Convert.ToString(efectivo);
                }
                else if (dialogResult == DialogResult.No)
                {
                    efectivo = 0;
                    tbEfectivo.Text = Convert.ToString(efectivo);
                }
            }
            else
                MessageBox.Show("No se ha registrado ningún boleto");


            
            



        }

        private void bt200_Click(object sender, EventArgs e)
        {
            if (total != 0)
            {
                DialogResult dialogResult = MessageBox.Show("¿Desea realizar esta operación?", "Información", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    efectivo = 200;
                    tbEfectivo.Text = Convert.ToString(efectivo);
                }
                else if (dialogResult == DialogResult.No)
                {
                    efectivo = 0;
                    tbEfectivo.Text = Convert.ToString(efectivo);
                }
            }
            else
                MessageBox.Show("No se ha registrado ningún boleto");
        }

        private void bt100_Click(object sender, EventArgs e)
        {
            if (total != 0)
            {
                DialogResult dialogResult = MessageBox.Show("¿Desea realizar esta operación?", "Información", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    efectivo = 100;
                    tbEfectivo.Text = Convert.ToString(efectivo);
                }
                else if (dialogResult == DialogResult.No)
                {
                    efectivo = 0;
                    tbEfectivo.Text = Convert.ToString(efectivo);
                }
            }
            else
                MessageBox.Show("No se ha registrado ningún boleto");

        }

        private void bt50_Click(object sender, EventArgs e)
        {
            if (total != 0)
            {

                DialogResult dialogResult = MessageBox.Show("¿Desea realizar esta operación?", "Información", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    efectivo = 50;
                    tbEfectivo.Text = Convert.ToString(efectivo);
                }
                else if (dialogResult == DialogResult.No)
                {
                    efectivo = 0;
                    tbEfectivo.Text = Convert.ToString(efectivo);
                }
            }
            else
                MessageBox.Show("No se ha registrado ningún boleto");
        }
    }
}
