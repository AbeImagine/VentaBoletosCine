﻿using System;
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
    public partial class membresias : Form
    {
        DBConnection conexionBD;
        MySqlDataReader reader;
        MySqlCommand comando;


        String nombreMembresia;
        String telefono;
        String email;
        String password;



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

                nombreMembresia = tbNombre.Text + " " + tbApellidoM.Text + " " + tbApellidoP;

                telefono = maskedTextBox1.Text;

                if (tbPass.Text == tbConfPass.Text)
                {
                    email = tbEmail.Text;

                    comando = new MySqlCommand("INSERT INTO miembro (nombre, telefono, correo, contraseña, administrador, usuario) VALUES ('" + nombreMembresia + "'," + telefono + ",'" + email + "','" + tbPass.Text + "'," + cbTipoMemb.SelectedIndex + ", '" + tbUsuario.Text + "')", conexionBD.Connection);
                    try
                    {
                        reader = comando.ExecuteReader();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }

                    MessageBox.Show("Registro exitoso");
                    reader.Close();
                }
                else
                    MessageBox.Show("Las contraseñas no coinciden");
            }
            else
                MessageBox.Show("No se pueden dejar campos vacios", "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        
    }
}
