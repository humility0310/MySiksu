using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HSHI.DB;

using System.Windows.Forms;

namespace MySiksu.repository
{
    class SiksuRepository
    {
        public void deleteUser(String userCode)
        {
            String qry = "delete FROM kyj_empCateInfo WHERE empNo in (select kyj_emp.empNo FROM kyj_emp where kyj_emp.empCode='" + userCode + "')";
            XDB.DsOpenAdo(qry);

            qry = "delete from kyj_emp WHERE empCode='" + userCode + "'";
            XDB.DsOpenAdo(qry);
            MessageBox.Show("삭제가 완료되었습니다.");
        }

        public DataTable findUserCode(String userCode)
        {
           String qry = "select empNo from kyj_emp where empCode='" + userCode + "'";
           return XDB.DsOpenAdo(qry);
        }

        public void userUpdate(String empCode, String empName, String COMP_NUMBER, String AUTHORITY_CODE, String USE_YN, String userInfoCode, String PREPAID_CARD,
            String BUSINESS_CARD, String IDCARD_TYPE, String PRICE_GROUP, String ACCOUNT_CODE)
        {
            String qry = "update kyj_emp SET empName = '" + empName + "' WHERE empCode = '" + empCode + "'";
            XDB.DsOpenAdo(qry);

            qry = "update kyj_empCateInfo  SET AUTHORITY_CODE = '" + AUTHORITY_CODE + "'  ,MOD_USER_ID = 'W118166' ,MOD_DATE = getdate() ,USE_YN = '" + USE_YN + "'   ,userInfoCode = '" + userInfoCode + "' " +
                ",PREPAID_CARD = '" + PREPAID_CARD + "' ,BUSINESS_CARD = '" + BUSINESS_CARD + "' ,IDCARD_TYPE = '" + IDCARD_TYPE + "'   ,PRICE_GROUP = '" + PRICE_GROUP + "' ,ACCOUNT_CODE = '" + ACCOUNT_CODE + "' " +
                " WHERE kyj_empCateInfo.empNo in (select empNo FROM kyj_emp where empCode='" + empCode + "')";
            XDB.DsOpenAdo(qry);
            MessageBox.Show("수정이 완료되었습니다.");
        }
        public void insertUser(String USER_GROUP, String COMP_CODE, String COMP_NAME,
                               String COMP_NUMBER, String CEO_NAME, String PHONE_NUMBER,
                               String empCode, String empName, String PRICE_GROUP,
                               String AUTHORITY_CODE, String ACCOUNT_CODE, String MANAGER_ID,
                               String PREPAID_CARD, String BUSINESS_CARD, String IDCARD_TYPE,
                               String USE_YN, String userInfoCode)
        {

            String qry = "insert into kyj_emp (empCode ,empName ,COMP_NUMBER ) VALUES ('" + empCode + "' ,'" + empName + "' ,'" + COMP_NUMBER + "' ) ";
            XDB.DsOpenAdo(qry);

            qry = "insert into kyj_empCateInfo( empNo ,AUTHORITY_CODE ,REG_USER_ID ,REG_DATE ,MOD_USER_ID ,MOD_DATE ,USE_YN ,userInfoCode ,MEMO ,PREPAID_CARD ,BUSINESS_CARD ,IDCARD_TYPE ,PRICE_GROUP ,ACCOUNT_CODE )" +
                        "select  " +
              "(select empNo FROM kyj_emp where empCode = '" + empCode + "')    " +
              ",'" + AUTHORITY_CODE + "'  " +
              ",'W118166'   " +
              ",getdate()   " +
              ",''   " +
              ",''  " +
              ",'" + USE_YN + "'   " +
              ",'" + userInfoCode + "'   " +
              ",''   " +
              ",'" + PREPAID_CARD + "'   " +
              ",'" + BUSINESS_CARD + "'   " +
              ",'" + IDCARD_TYPE + "'   " +
              ",'" + PRICE_GROUP + "'  " +
              ",'" + ACCOUNT_CODE + "'  ";
            XDB.DsOpenAdo(qry);
            MessageBox.Show("입력되었습니다");
        }

        public DataTable searchEmp(String empName, String empCode)
        {
            DataTable empTable = new DataTable();
            String qry = "Select * FROM kyj_emp , kyj_empCateInfo where empCode like '" + empCode + "' and empName like '" + empName + "' and  dbo.kyj_emp.empNo = dbo.kyj_empCateInfo.empNo";
            empTable = XDB.DsOpenAdo(qry);
            return empTable;
        }

        public DataTable SearchCom(String code)
        {
            DataTable comTable = new DataTable();
            String qry = "select COMP_NUMBER, COMP_CODE, COMP_NAME, CEO_NAME, PHONE_NUMBER, USER_GROUP FROM kyj_com where COMP_CODE like '" + code + "'";
            comTable = XDB.DsOpenAdo(qry);
            return comTable;
        }

        public DataTable getUserList(String USER_GROUP, String comCode, String comName, String empCode, String empName)
        {
            DataTable datatable = new DataTable();

            String qry = "select * " +
"from kyj_restaurant, kyj_com, kyj_emp, kyj_empCateInfo " +
"where dbo.kyj_emp.empNo = dbo.kyj_empCateInfo.empNo " +
"and dbo.kyj_emp.COMP_NUMBER = dbo.kyj_com.COMP_NUMBER " +
"and dbo.kyj_com.USER_GROUP = dbo.kyj_restaurant.USER_GROUP " +
"and dbo.kyj_emp.empCode like '"+empCode+"' " +
"and kyj_emp.empName like '"+empName+"' " +
"and dbo.kyj_com.COMP_NAME like '"+comName+"' " +
"and dbo.kyj_restaurant.USER_GROUP like '"+USER_GROUP+"' " +
"and dbo.kyj_com.COMP_CODE like '" + comCode + "' ";

            datatable = XDB.DsOpenAdo(qry);
            return datatable;
        }

        public static void setDBConnection()
        {
            XDB.SetSybAdoStr("172.17.10.123", "4300", "i6950v8", "delphig00d", "midsys");
        }
    }
}