using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using MySiksu.repository;
using MySiksu.service;
using DevExpress.XtraGrid;

namespace MySiksu
{
    public partial class Form1 : Form
    {
        DataTable viewTable = new DataTable();
        DataTable rollbackTable = new DataTable();

        SicsuService sicsuService = new SicsuService();


        public String txtval { get; set; }

        public Form1()
        {
            InitializeComponent();
            //new SiksuRepository().setDBConnection();
            MySiksu.repository.SiksuRepository.setDBConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {//조회버튼
            String USER_GROUP, comCode, comName, empCode, empName;

            repositoryItemCheckEdit1.ValueChecked = "1";//!!!
            repositoryItemCheckEdit1.ValueUnchecked = "0";//!!!

            USER_GROUP = comboBoxEdit1.Text;
            comCode = "%" + buttonEdit2.Text + "%";
            comName = "%" + textEdit1.Text + "%";
            empCode = "%" + buttonEdit1.Text + "%";
            empName = "%" + buttonEdit3.Text + "%";

            viewTable = sicsuService.getUserList(USER_GROUP, comCode, comName, empCode, empName);

            gridControl1.DataSource = viewTable;
        }

        private void bandedGridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                comboBoxEdit3.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[0];
                buttonEdit4.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[4];
                textEdit2.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[5];
                textEdit4.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[6];
                textEdit5.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[7];
                buttonEdit5.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[10];
                buttonEdit6.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[11];
                comboBoxEdit5.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[14];
                checkBox1.Checked = sicsuService.chkReturn("" + viewTable.Rows[e.RowHandle].ItemArray[19]);
                comboBoxEdit7.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[22];
                comboBoxEdit8.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[23];
                comboBoxEdit6.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[26];
                comboBoxEdit4.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[25];
                comboBoxEdit9.Text = "" + viewTable.Rows[e.RowHandle].ItemArray[24];
                checkBox2.Checked = sicsuService.chkReturn("" + viewTable.Rows[e.RowHandle].ItemArray[27]);
                checkBox3.Checked = sicsuService.chkReturn("" + viewTable.Rows[e.RowHandle].ItemArray[28]);
                checkBox4.Checked = sicsuService.chkReturn("" + viewTable.Rows[e.RowHandle].ItemArray[29]);
                checkBox5.Checked = sicsuService.chkReturn("" + viewTable.Rows[e.RowHandle].ItemArray[30]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("error => " + ex);
            }

        }

        private void buttonEdit4_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (EnterComCode ec = new EnterComCode())
            {
                if (ec.ShowDialog() == DialogResult.OK)
                {
                    textEdit3.Text = ec.comNum;
                    textEdit4.Text = ec.ceoName;
                    buttonEdit4.Text = ec.comCode;
                    textEdit2.Text = ec.comName;
                    textEdit5.Text = ec.comPh;
                    comboBoxEdit3.Text = sicsuService.groupConverter(ec.userGroup.Trim());
                }
            }
        }

        private void buttonEdit5_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (SearchEmp se = new SearchEmp())
            {
                if (se.ShowDialog() == DialogResult.OK)
                {
                    buttonEdit5.Text = se.empNum;
                    buttonEdit6.Text = se.empName;
                    comboBoxEdit4.Text = sicsuService.groupConverter(se.PRICE_GROUP.Trim());
                    comboBoxEdit5.Text = se.AUTHORITY_CODE;
                    comboBoxEdit7.Text = se.PREPAID_CARD;
                    comboBoxEdit8.Text = se.BUSINESS_CARD;
                    comboBoxEdit9.Text = se.IDCARD_TYPE;
                    comboBoxEdit6.Text = se.ACCOUNT_CODE;
                    checkBox1.Checked = sicsuService.chkReturn(se.USE_YN);
                    checkBox2.Checked = sicsuService.chkReturn(se.YARD_MORNING);
                    checkBox3.Checked = sicsuService.chkReturn(se.YARD_NOON);
                    checkBox4.Checked = sicsuService.chkReturn(se.YARD_EVENING);
                    checkBox5.Checked = sicsuService.chkReturn(se.YARD_NIGHT);
                }
            }
        }

        private void buttonEdit6_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            buttonEdit5_ButtonClick(sender, e);
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                checkBox2.Checked = false; checkBox2.Enabled = false;
                checkBox3.Checked = false; checkBox3.Enabled = false;
                checkBox4.Checked = false; checkBox4.Enabled = false;
                checkBox5.Checked = false; checkBox5.Enabled = false;

            }
            else
            {
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                return;
                //솔직불필요
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sicsuService.insertUser(comboBoxEdit3.Text, buttonEdit4.Text, textEdit2.Text,
                                    textEdit3.Text, textEdit4.Text, textEdit5.Text,
                                    buttonEdit5.Text, buttonEdit6.Text, comboBoxEdit4.Text,
                                    comboBoxEdit5.Text, comboBoxEdit6.Text, textEdit6.Text,
                                    comboBoxEdit7.Text, comboBoxEdit8.Text, comboBoxEdit9.Text,
            checkBox1.Checked ? "1" : "0", sicsuService.checkConverter(checkBox2.Checked, checkBox3.Checked, checkBox4.Checked, checkBox5.Checked));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (sicsuService.findUserCode(buttonEdit5.Text) != true) { MessageBox.Show("사번이 존재하지 않습니다."); return; }
            sicsuService.findUserCode(buttonEdit5.Text);
            sicsuService.userUpdate(buttonEdit5.Text, buttonEdit6.Text, textEdit3.Text, comboBoxEdit5.Text, checkBox1.Checked ? "1" : "0", sicsuService.checkConverter(checkBox2.Checked, checkBox3.Checked, checkBox4.Checked, checkBox5.Checked),
               "" + comboBoxEdit7.Text, "" + comboBoxEdit8.Text, "" + comboBoxEdit9.Text, "" + comboBoxEdit4.Text, "" + comboBoxEdit6.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (sicsuService.findUserCode(buttonEdit5.Text) != true) { MessageBox.Show("사번이 존재하지 않습니다."); return; }
            sicsuService.deleteUser(buttonEdit5.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //declaring the application
            Microsoft.Office.Interop.Excel.Application oAppln;
            //declaring work book
            Microsoft.Office.Interop.Excel.Workbook oWorkBook;
            //declaring worksheet
            Microsoft.Office.Interop.Excel.Worksheet oWorkSheet;
            //declaring the range
            Microsoft.Office.Interop.Excel.Range oRange;
            try
            {
                oAppln = new Microsoft.Office.Interop.Excel.Application();
                oWorkBook = (Microsoft.Office.Interop.Excel.Workbook)(oAppln.Workbooks.Add(true));
                oWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWorkBook.ActiveSheet;
                int iRow = 2;

                if (bandedGridView1.RowCount > 0)
                {

                    for (int i = 0; i < bandedGridView1.Columns.Count; i++)
                    {
                        oWorkSheet.Cells[1, i + 1] = bandedGridView1.Columns[i].Caption;
                    }
                    for (int rowNo = 0; rowNo < bandedGridView1.RowCount; rowNo++)
                    {//in each row
                        /* for (int colNo = 0; colNo < viewTable.Columns.Count; colNo++){
                             oWorkSheet.Cells[iRow, colNo + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[colNo].ToString();
                         }*/

                        oWorkSheet.Cells[iRow, 0 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[0].ToString();
                        oWorkSheet.Cells[iRow, 1 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[5].ToString();
                        oWorkSheet.Cells[iRow, 2 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[4].ToString();
                        oWorkSheet.Cells[iRow, 3 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[16].ToString();
                        oWorkSheet.Cells[iRow, 4 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[10].ToString();
                        oWorkSheet.Cells[iRow, 5 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[11].ToString();
                        oWorkSheet.Cells[iRow, 6 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[14].ToString();
                        oWorkSheet.Cells[iRow, 7 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[26].ToString();
                        oWorkSheet.Cells[iRow, 8 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[25].ToString();
                        oWorkSheet.Cells[iRow, 9 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[22].ToString();
                        oWorkSheet.Cells[iRow, 10 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[23].ToString();
                        oWorkSheet.Cells[iRow, 11 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[24].ToString();
                        oWorkSheet.Cells[iRow, 12 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[1].ToString();
                        oWorkSheet.Cells[iRow, 13 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[19].ToString();
                        oWorkSheet.Cells[iRow, 14 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[27].ToString();
                        oWorkSheet.Cells[iRow, 15 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[28].ToString();
                        oWorkSheet.Cells[iRow, 16 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[29].ToString();
                        oWorkSheet.Cells[iRow, 17 + 1] = bandedGridView1.GetDataRow(rowNo).ItemArray[30].ToString();


                        //moving to next row
                        iRow++;
                    }
                }

                //range of the excel sheet
                oRange = oWorkSheet.get_Range("A1", "IV1");
                oRange.EntireColumn.AutoFit();
                oAppln.UserControl = false;
                //path declaration
                string strFile = "c:\\" + "report" + ".xls";
                // to view Excel sheet...
                oAppln.Visible = true;
                // to save the excel sheet....
                oWorkBook.SaveAs(
                strFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, null, null, false, false,
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, false, false, null, null, null);
            }
            catch (Exception theException)
            {
                MessageBox.Show(theException.Message.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            // Check whether the GridControl can be previewed.
            // Open the Preview window.
            bandedGridView1.ShowPrintPreview();
           // bandedGridView1.Print();
        }
    }
}
