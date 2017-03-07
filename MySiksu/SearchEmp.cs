using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using MySiksu.service;

namespace MySiksu
{
    public partial class SearchEmp : Form
    {

        SicsuService siksuService = new SicsuService();
        DataTable dataTable = new DataTable();

        public String empNum { get; set; }
        public String empName { get; set; }
        public String AUTHORITY_CODE { get; set; }
        public String PREPAID_CARD { get; set; }
        public String BUSINESS_CARD { get; set; }
        public String IDCARD_TYPE { get; set; }
        public String ACCOUNT_CODE { get; set; }
        public String USE_YN { get; set; }
        public String YARD_MORNING { get; set; }
        public String YARD_NOON { get; set; }
        public String YARD_EVENING { get; set; }
        public String YARD_NIGHT { get; set; }
        public String PRICE_GROUP { get; set; }

        public SearchEmp()
        {
            InitializeComponent();

            repositoryItemCheckEdit1.ValueChecked = "1";//!!!
            repositoryItemCheckEdit1.ValueUnchecked = "0";//!!!
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataTable = siksuService.searchEmp(textEdit2.Text, textEdit1.Text);
            gridControl1.DataSource = dataTable;
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            empNum = "" + dataTable.Rows[e.RowHandle].ItemArray[1];
            empName = "" + dataTable.Rows[e.RowHandle].ItemArray[2];
            AUTHORITY_CODE = "" + dataTable.Rows[e.RowHandle].ItemArray[5];
            PREPAID_CARD = "" + dataTable.Rows[e.RowHandle].ItemArray[13];
            BUSINESS_CARD = "" + dataTable.Rows[e.RowHandle].ItemArray[14];
            IDCARD_TYPE = "" + dataTable.Rows[e.RowHandle].ItemArray[15];
            PRICE_GROUP = "" + dataTable.Rows[e.RowHandle].ItemArray[16];
            ACCOUNT_CODE = "" + dataTable.Rows[e.RowHandle].ItemArray[17];
            USE_YN = "" + dataTable.Rows[e.RowHandle].ItemArray[10];
            YARD_MORNING = "" + dataTable.Rows[e.RowHandle].ItemArray[18];
            YARD_NOON = "" + dataTable.Rows[e.RowHandle].ItemArray[19];
            YARD_EVENING = "" + dataTable.Rows[e.RowHandle].ItemArray[20];
            YARD_NIGHT = "" + dataTable.Rows[e.RowHandle].ItemArray[21];

            textEdit3.Text = empName;

            if (e.Clicks ==2)
            {
                button1_Click(sender, e);
            }
        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }

        private void textEdit2_KeyUp(object sender, KeyEventArgs e)
        {
            textEdit1_KeyUp(sender, e);
        }
    }
}
