using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSHI.DB;
using System.Windows.Forms;
using System.Data;

namespace MySiksu.repository
{
    class SiksuRepository_proc
    {
        public void deleteUser(String userCode)
        {
            XDB.DsOpenAdo("exec kyj_delete_user @userCode ='" + userCode + "'");
            MessageBox.Show("삭제가 완료되었습니다.");
        }

        public DataTable findUserCode(String userCode)
        {
            return XDB.DsOpenAdo("exec kyj_find_user_code @userCode = '" + userCode + "'");
        }

        public void userUpdate(String empCode, String empName, String COMP_NUMBER, String AUTHORITY_CODE, String USE_YN, String userInfoCode, String PREPAID_CARD,
            String BUSINESS_CARD, String IDCARD_TYPE, String PRICE_GROUP, String ACCOUNT_CODE)
        {
            XDB.DsOpenAdo("exec kyj_user_update @empCode = '" + empCode + "', @empName ='" + empName + "', @COMP_NUMBER = '" + COMP_NUMBER + "', @AUTHORITY_CODE = '" + AUTHORITY_CODE + "', @USE_YN ='" + USE_YN + "', @userInfoCode ='" + userInfoCode + "', @PREPAID_CARD = '" + PREPAID_CARD + "', @BUSINESS_CARD ='" + BUSINESS_CARD + "', @IDCARD_TYPE ='" + IDCARD_TYPE + "', @PRICE_GROUP ='" + PRICE_GROUP + "', @ACCOUNT_CODE ='" + ACCOUNT_CODE + "'");
            MessageBox.Show("수정이 완료되었습니다.");
        }
        public void insertUser(String USER_GROUP, String COMP_CODE, String COMP_NAME,
                               String COMP_NUMBER, String CEO_NAME, String PHONE_NUMBER,
                               String empCode, String empName, String PRICE_GROUP,
                               String AUTHORITY_CODE, String ACCOUNT_CODE, String MANAGER_ID,
                               String PREPAID_CARD, String BUSINESS_CARD, String IDCARD_TYPE,
                               String USE_YN, String userInfoCode)
        {
            XDB.DsOpenAdo("exec kyj_insert_user  @empCode ='" + empCode + "', @empName ='" + empName + "', @COMP_NUMBER ='" + COMP_NUMBER + "', @AUTHORITY_CODE ='" + AUTHORITY_CODE + "', @USE_YN ='" + USE_YN + "', " + "@userInfoCode ='" + userInfoCode + "', @PREPAID_CARD ='" + PREPAID_CARD + "', @BUSINESS_CARD ='" + BUSINESS_CARD + "', @IDCARD_TYPE ='" + IDCARD_TYPE + "', @PRICE_GROUP ='" + PRICE_GROUP + "', @ACCOUNT_CODE ='" + ACCOUNT_CODE + "'");
            MessageBox.Show("입력되었습니다");
        }

        public DataTable searchEmp(String empName, String empCode)
        {
            return XDB.DsOpenAdo("exec kyj_search_emp @empName ='" + empName + "', @empCode ='" + empCode + "'");
        }

        public DataTable SearchCom(String code)
        {
            return XDB.DsOpenAdo("exec kyj_search_com @code ='" + code + "'"); ;
        }

        public DataTable getUserList(String USER_GROUP, String comCode, String comName, String empCode, String empName)
        {
            return XDB.DsOpenAdo("exec kyj_get_list @empCode ='" + empCode + "', @empName ='" + empName + "', @comName ='" + comName + "', @USER_GROUP ='" + USER_GROUP + "', @comCode ='" + comCode + "'");
        }

        public static void setDBConnection()
        {
            XDB.SetSybAdoStr("172.17.10.123", "4300", "i6950v8", "delphig00d", "midsys");
        }
    }
}
