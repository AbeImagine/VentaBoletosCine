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
    /*  Clase: DataGrid.
     *  Descripción: 
     * 
     */
    public partial class DataGrid : Form
    {
        private bool eliminate;
        public int id;
        public string uid;

        public DataGrid()
        {
            InitializeComponent();
            eliminate = false;
            id = -1;
            uid = "a";
        }

        public void ShowData(BindingSource bSource)
        {
            dataGridView1.DataSource = bSource;
        }

        public void GetData(BindingSource bSource)
        {
            dataGridView1.DataSource = bSource;
            eliminate = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (eliminate)
                {
                    id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                    //this.Close();
                }
            }
            catch (Exception)
            {
                //throw;
            }

            try
            {
                if (eliminate)
                {
                    uid = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    //this.Close();
                }
            }
            catch (Exception)
            {
                //throw;
            }

            this.Close();
        }
    }
}