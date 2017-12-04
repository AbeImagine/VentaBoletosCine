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
    public partial class DataGrid : Form
    {
        private bool eliminate;
        public int id;

        public DataGrid()
        {
            InitializeComponent();
            eliminate = false;
            id = -1;
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
            if (eliminate)
            {
                id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                this.Close();
            }
        }
    }
}