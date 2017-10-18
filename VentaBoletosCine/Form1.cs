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
    public partial class Capturista : Form
    {
        public Capturista()
        {
            InitializeComponent();
        }

        private void Capturista_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Annabelle 2: La Creación";
            textBox2.Text = "B15";
            textBox3.Text = "TERROR";
            textBox4.Text = "109 minutos";
            richTextBox1.Text = "Anabelle 2 sucede varios años después de la trágica muerte de la pequeña hija de un fabricante de muñecas y su esposa, quienes dan albergue en su casa a una monja y a varias niñas de un orfanato clausurado. Al poco tiempo cada uno de ellos se volverá el objetivo de Anabelle, la muñeca poseída creada por el dueño de la casa.";
            richTextBox2.Text = " Actores:Talitha Bateman,Stephanie Sigman Directores: David F. Sandberg";
        }
    }
}
