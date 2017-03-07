using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySiksu.service;

namespace MySiksu
{
    public partial class EnterComCode : Form
    {
        SicsuService siksuService = new SicsuService();
        DataTable dataTable = new DataTable();

        public String comNum { get; set; }
        public String comCode { get; set; }
        public String comName { get; set; }
        public String ceoName { get; set; }
        public String comPh { get; set; }
        public String userGroup { get; set; }

        public EnterComCode()
        {
            InitializeComponent();
            // System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["Form1"];

            //textEdit1.Text = ((Form1)f).
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataTable = siksuService.SearchCom(textEdit1.Text);
            gridControl1.DataSource = dataTable;
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            comNum = "" + dataTable.Rows[e.RowHandle].ItemArray[0];
            comCode = "" + dataTable.Rows[e.RowHandle].ItemArray[1];
            comName = "" + dataTable.Rows[e.RowHandle].ItemArray[2];
            ceoName = "" + dataTable.Rows[e.RowHandle].ItemArray[3];
            comPh = "" + dataTable.Rows[e.RowHandle].ItemArray[4];
            userGroup = "" + dataTable.Rows[e.RowHandle].ItemArray[5];

            textBox1.Text = "" + dataTable.Rows[e.RowHandle].ItemArray[2];

            if (e.Clicks == 2)
            {
                button3_Click(sender, e);
            }
        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
