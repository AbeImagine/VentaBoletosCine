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
    public partial class boleto : Form
    {
        DBConnection conexionBD;

        List<Button> listaAsientos;
        int numAsiento=0;
        public delegate void delegadoPasaDato(int valor);
        public event delegadoPasaDato eventoPasaNumBoleto;


        public boleto(DBConnection conexion)
        {
            conexion = conexionBD;
            listaAsientos = new List<Button>();
            
            InitializeComponent();
        }

        public void llenaListaBotones()
        {
            listaAsientos.Add(bt1);
            listaAsientos.Add(bt2);
            listaAsientos.Add(bt3);
            listaAsientos.Add(bt4);
            listaAsientos.Add(bt5);
            listaAsientos.Add(bt6);
            listaAsientos.Add(bt7);
            listaAsientos.Add(bt8);
            listaAsientos.Add(bt9);
            listaAsientos.Add(bt10);

            listaAsientos.Add(bt11);
            listaAsientos.Add(bt12);
            listaAsientos.Add(bt13);
            listaAsientos.Add(bt14);
            listaAsientos.Add(bt15);
            listaAsientos.Add(bt16);
            listaAsientos.Add(bt17);
            listaAsientos.Add(bt18);
            listaAsientos.Add(bt19);
            listaAsientos.Add(bt20);

            listaAsientos.Add(bt21);
            listaAsientos.Add(bt22);
            listaAsientos.Add(bt23);
            listaAsientos.Add(bt24);
            listaAsientos.Add(bt25);
            listaAsientos.Add(bt26);
            listaAsientos.Add(bt27);
            listaAsientos.Add(bt28);
            listaAsientos.Add(bt29);
            listaAsientos.Add(bt30);

            listaAsientos.Add(bt31);
            listaAsientos.Add(bt32);
            listaAsientos.Add(bt33);
            listaAsientos.Add(bt33);
            listaAsientos.Add(bt34);
            listaAsientos.Add(bt35);
            listaAsientos.Add(bt36);
            listaAsientos.Add(bt37);
            listaAsientos.Add(bt38);
            listaAsientos.Add(bt39);
            listaAsientos.Add(bt40);



        }

        private void boleto_Load(object sender, EventArgs e)
        {
            llenaListaBotones();

            this.DoubleBuffered = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bt1.BackColor = Color.Red;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            checaSeleecionAsiento();
        }

        public void checaSeleecionAsiento()
        {
            foreach (var listaAux in listaAsientos)
            {
                if (listaAux.BackColor == Color.Red)
                {
                    numAsiento = listaAsientos.IndexOf(listaAux);
                    eventoPasaNumBoleto(numAsiento);
                    this.Close();
                }

            }

        }

        private void bt2_Click(object sender, EventArgs e)
        {
            bt2.BackColor = Color.Red;
            checaSeleecionAsiento();
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            bt3.BackColor = Color.Red;
        }

        private void bt4_Click(object sender, EventArgs e)
        {
            bt4.BackColor = Color.Red;

        }

        private void bt5_Click(object sender, EventArgs e)
        {
            bt5.BackColor = Color.Red;
        }

        private void bt6_Click(object sender, EventArgs e)
        {
            bt6.BackColor = Color.Red;
        }

        private void bt7_Click(object sender, EventArgs e)
        {
            bt7.BackColor = Color.Red;
        }

        private void bt8_Click(object sender, EventArgs e)
        {
            bt8.BackColor = Color.Red;
        }

        private void bt9_Click(object sender, EventArgs e)
        {
            bt9.BackColor = Color.Red;
        }

        private void bt10_Click(object sender, EventArgs e)
        {
            bt10.BackColor = Color.Red;
        }

        private void bt11_Click(object sender, EventArgs e)
        {
            bt11.BackColor = Color.Red;
        }

        private void bt12_Click(object sender, EventArgs e)
        {
            bt12.BackColor = Color.Red;
        }

        private void bt13_Click(object sender, EventArgs e)
        {
            bt13.BackColor = Color.Red;
        }

        private void bt14_Click(object sender, EventArgs e)
        {
            bt14.BackColor = Color.Red;
        }

        private void bt15_Click(object sender, EventArgs e)
        {
            bt15.BackColor = Color.Red;
        }

        private void bt16_Click(object sender, EventArgs e)
        {
            bt16.BackColor = Color.Red;
        }

        private void bt17_Click(object sender, EventArgs e)
        {
            bt17.BackColor = Color.Red;
        }

        private void bt18_Click(object sender, EventArgs e)
        {
            bt18.BackColor = Color.Red;
        }

        private void bt19_Click(object sender, EventArgs e)
        {
            bt19.BackColor = Color.Red;
        }

        private void bt20_Click(object sender, EventArgs e)
        {
            bt20.BackColor = Color.Red;
        }

        private void bt21_Click(object sender, EventArgs e)
        {
            bt21.BackColor = Color.Red;
        }

        private void bt22_Click(object sender, EventArgs e)
        {
            bt22.BackColor = Color.Red;
        }

        private void bt23_Click(object sender, EventArgs e)
        {
            bt23.BackColor = Color.Red;
        }

        private void bt24_Click(object sender, EventArgs e)
        {
            bt24.BackColor = Color.Red;
        }

        private void bt25_Click(object sender, EventArgs e)
        {
            bt25.BackColor = Color.Red;
        }

        private void bt26_Click(object sender, EventArgs e)
        {
            bt26.BackColor = Color.Red;
        }

        private void bt27_Click(object sender, EventArgs e)
        {
            bt27.BackColor = Color.Red;
        }

        private void bt28_Click(object sender, EventArgs e)
        {
            bt28.BackColor = Color.Red;
        }

        private void bt29_Click(object sender, EventArgs e)
        {
            bt29.BackColor = Color.Red;
        }

        private void bt30_Click(object sender, EventArgs e)
        {
            bt30.BackColor = Color.Red;
        }

        private void bt31_Click(object sender, EventArgs e)
        {
            bt31.BackColor = Color.Red;
        }

        private void bt32_Click(object sender, EventArgs e)
        {
            bt32.BackColor = Color.Red;
        }

        private void bt33_Click(object sender, EventArgs e)
        {
            bt33.BackColor = Color.Red;
        }

        private void bt34_Click(object sender, EventArgs e)
        {
            bt34.BackColor = Color.Red;
        }

        private void bt35_Click(object sender, EventArgs e)
        {
            bt35.BackColor = Color.Red;
        }

        private void bt36_Click(object sender, EventArgs e)
        {
            bt36.BackColor = Color.Red;
        }

        private void bt37_Click(object sender, EventArgs e)
        {
            bt37.BackColor = Color.Red;
        }

        private void bt38_Click(object sender, EventArgs e)
        {
            bt38.BackColor = Color.Red;
        }

        private void bt39_Click(object sender, EventArgs e)
        {
            bt39.BackColor = Color.Red;
        }

        private void bt40_Click(object sender, EventArgs e)
        {
            bt40.BackColor = Color.Red;
        }
    }
}
