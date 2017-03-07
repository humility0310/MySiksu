using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySiksu.repository;
using System.Data;
using System.Windows.Forms;

namespace MySiksu.service
{
    class SicsuService
    {
        SiksuRepository_proc siksuRepository = new SiksuRepository_proc();

        public void deleteUser(String userCode)
        {
            siksuRepository.deleteUser(userCode);
        }

        public Boolean findUserCode(String userCode)
        {
            if (((siksuRepository.findUserCode(userCode)).Rows.Count).Equals(0)) { return false; }
            //MessageBox.Show(((siksuRepository.findUserCode(userCode)).Rows.Count));
            return true;
        }

        public void userUpdate(String empCode, String empName, String COMP_NUMBER, String AUTHORITY_CODE, String USE_YN, String userInfoCode, String PREPAID_CARD,
            String BUSINESS_CARD, String IDCARD_TYPE, String PRICE_GROUP, String ACCOUNT_CODE)
        {
            siksuRepository.userUpdate(empCode, empName, COMP_NUMBER, AUTHORITY_CODES(AUTHORITY_CODE), USE_YN, userInfoCode, PREPAID_CARDS(PREPAID_CARD),
             BUSINESS_CARDS(BUSINESS_CARD), IDCARD_TYPES(IDCARD_TYPE), groupConverter(PRICE_GROUP), ACCOUNT_CODES(ACCOUNT_CODE));
        }

        public DataTable getUserList(String USER_GROUP, String comCode, String comName, String empCode, String empName)
        {
            DataTable dataTable = new DataTable();
            String decode;
            USER_GROUP = groupConverter(USER_GROUP);
            dataTable = siksuRepository.getUserList(USER_GROUP, comCode, comName, empCode, empName);

            dataTable.Columns.Add("YARD_MORNING", typeof(String));
            dataTable.Columns.Add("YARD_NOON", typeof(String));
            dataTable.Columns.Add("YARD_EVENING", typeof(String));
            dataTable.Columns.Add("YARD_NIGHT", typeof(String));

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {//code ==> string
                decode = foodConverter("" + dataTable.Rows[i].ItemArray[20]);

                dataTable.Rows[i].SetField("USER_GROUP", groupConverter(("" + dataTable.Rows[i].ItemArray[0]).Trim()));
                dataTable.Rows[i].SetField("AUTHORITY_CODE", AUTHORITY_CODES("" + dataTable.Rows[i].ItemArray[14]));
                dataTable.Rows[i].SetField("ACCOUNT_CODE", ACCOUNT_CODES("" + dataTable.Rows[i].ItemArray[26]));
                dataTable.Rows[i].SetField("PREPAID_CARD", PREPAID_CARDS("" + dataTable.Rows[i].ItemArray[22]));
                dataTable.Rows[i].SetField("BUSINESS_CARD", BUSINESS_CARDS("" + dataTable.Rows[i].ItemArray[23]));
                dataTable.Rows[i].SetField("IDCARD_TYPE", IDCARD_TYPES("" + dataTable.Rows[i].ItemArray[24]));
                dataTable.Rows[i].SetField("PRICE_GROUP", groupConverter(("" + dataTable.Rows[i].ItemArray[25]).Trim()));

                dataTable.Rows[i].SetField("YARD_MORNING", decode[0]);//14
                dataTable.Rows[i].SetField("YARD_NOON", decode[1]);//15
                dataTable.Rows[i].SetField("YARD_EVENING", decode[2]);//16
                dataTable.Rows[i].SetField("YARD_NIGHT", decode[3]);//17
            }
            return dataTable;
        }

        public DataTable searchEmp(String empName, String empCode)
        {
            DataTable empTable = new DataTable();
            empName = "%" + empName + "%";
            empCode = "%" + empCode + "%";

            empTable = siksuRepository.searchEmp(empName, empCode);
            String decode;

            empTable.Columns.Add("YARD_MORNING", typeof(String));
            empTable.Columns.Add("YARD_NOON", typeof(String));
            empTable.Columns.Add("YARD_EVENING", typeof(String));
            empTable.Columns.Add("YARD_NIGHT", typeof(String));

            for (int i = 0; i < empTable.Rows.Count; i++)
            {
                decode = foodConverter("" + empTable.Rows[i].ItemArray[11]);

                empTable.Rows[i].SetField("AUTHORITY_CODE", AUTHORITY_CODES("" + empTable.Rows[i].ItemArray[5]));
                empTable.Rows[i].SetField("PREPAID_CARD", PREPAID_CARDS("" + empTable.Rows[i].ItemArray[13]));
                empTable.Rows[i].SetField("BUSINESS_CARD", BUSINESS_CARDS("" + empTable.Rows[i].ItemArray[14]));
                empTable.Rows[i].SetField("IDCARD_TYPE", IDCARD_TYPES("" + empTable.Rows[i].ItemArray[15]));
                empTable.Rows[i].SetField("ACCOUNT_CODE", ACCOUNT_CODES("" + empTable.Rows[i].ItemArray[17]));

                empTable.Rows[i].SetField("YARD_MORNING", decode[0]);//14
                empTable.Rows[i].SetField("YARD_NOON", decode[1]);//15
                empTable.Rows[i].SetField("YARD_EVENING", decode[2]);//16
                empTable.Rows[i].SetField("YARD_NIGHT", decode[3]);//17
            }

            return empTable;
        }

        public DataTable SearchCom(String comCode)
        {
            comCode = "%" + comCode + "%";
            return siksuRepository.SearchCom(comCode);
        }

        private String foodConverter(String codes)
        {//string==>int==>2진수코드 해석
            String conBineary = "";
            int numCode = int.Parse(codes);
            conBineary = Convert.ToString(numCode, 2);

            for (int j = 8; j > conBineary.Length; j--)
            {
                conBineary = "0" + conBineary;
            }
            return conBineary;
        }

        private String IDCARD_TYPES(String code)
        {
            if (code.Equals("001")) { code = "도급협력사 출입증"; }
            else if (code.Equals("002")) { code = "임시 출입증"; }

            else if (code.Equals("도급협력사 출입증")) { code = "001"; }
            else if (code.Equals("임시 출입증")) { code = "002"; }
            else { code = "%%"; }
            return code;
        }

        public String BUSINESS_CARDS(String code)
        {
            if (code.Equals("001")) { code = "개인용카드"; }
            else if (code.Equals("002")) { code = "법인카드"; }

            else if (code.Equals("개인용카드")) { code = "001"; }
            else if (code.Equals("법인카드")) { code = "002"; }
            else { code = "%%"; }
            return code;
        }

        public String PREPAID_CARDS(String code)
        {
            if (code.Equals("001")) { code = "선불카드"; }
            else if (code.Equals("002")) { code = "후불카드"; }

            else if (code.Equals("선불카드")) { code = "001"; }
            else if (code.Equals("후불카드")) { code = "002"; }
            else { code = "%%"; }
            return code;
        }

        public String ACCOUNT_CODES(String code)
        {
            if (code.Equals("001")) { code = "무상"; }
            else if (code.Equals("002")) { code = "기성"; }
            else if (code.Equals("003")) { code = "급여정산"; }
            else if (code.Equals("004")) { code = "식수 카운터"; }
            else if (code.Equals("005")) { code = "기타"; }

            else if (code.Equals("무상")) { code = "001"; }
            else if (code.Equals("기성")) { code = "002"; }
            else if (code.Equals("급여정산")) { code = "003"; }
            else if (code.Equals("식수 카운터")) { code = "004"; }
            else if (code.Equals("기타")) { code = "005"; }
            else { code = "%%"; }
            return code;
        }

        public String AUTHORITY_CODES(String code)
        {
            if (code.Equals("000")) { code = "출입"; }
            else if (code.Equals("001")) { code = "식사"; }
            else if (code.Equals("002")) { code = "출입식사겸용"; }

            else if (code.Equals("출입")) { code = "000"; }
            else if (code.Equals("식사")) { code = "001"; }
            else if (code.Equals("출입식사겸용")) { code = "002"; }
            else { code = "%%"; }
            return code;
        }

        public String groupConverter(String group)//식사그룹
        {
            if (group.Equals("임원")) { group = "0"; }
            else if (group.Equals("당사(일반)")) { group = "1"; }
            else if (group.Equals("당사(계약)")) { group = "2"; }
            else if (group.Equals("당사(파견)")) { group = "3"; }
            else if (group.Equals("당사(채용)")) { group = "4"; }
            else if (group.Equals("생산협력사(일반)")) { group = "5"; }
            else if (group.Equals("생산협력사(일괄)")) { group = "6"; }
            else if (group.Equals("생산협력사(채용)")) { group = "7"; }
            else if (group.Equals("생산협력사(일반)")) { group = "8"; }
            else if (group.Equals("설계협력사(채용)")) { group = "9"; }
            else if (group.Equals("도급사(제공-일반A)")) { group = "10"; }
            else if (group.Equals("도급사(제공-일반B)")) { group = "11"; }
            else if (group.Equals("도급사(제공-기타A)")) { group = "12"; }
            else if (group.Equals("도급사(제공-기타B)")) { group = "13"; }
            else if (group.Equals("도급사(비제공)")) { group = "14"; }
            else if (group.Equals("고객(선주)")) { group = "15"; }
            else if (group.Equals("고객(선급)")) { group = "16"; }
            else if (group.Equals("기술연수생")) { group = "17"; }
            else if (group.Equals("파견출장(제공-상주)")) { group = "18"; }
            else if (group.Equals("파견출장(제공-기타)")) { group = "19"; }
            else if (group.Equals("파견출장(비제공)")) { group = "20"; }

            else if (group.Equals("0")) { group = "임원"; }
            else if (group.Equals("1")) { group = "당사(일반)"; }
            else if (group.Equals("2")) { group = "당사(계약)"; }
            else if (group.Equals("3")) { group = "당사(파견)"; }
            else if (group.Equals("4")) { group = "당사(채용)"; }
            else if (group.Equals("5")) { group = "생산협력사(일반)"; }
            else if (group.Equals("6")) { group = "생산협력사(일괄)"; }
            else if (group.Equals("7")) { group = "생산협력사(채용)"; }
            else if (group.Equals("8")) { group = "생산협력사(일반)"; }
            else if (group.Equals("9")) { group = "설계협력사(채용)"; }
            else if (group.Equals("10")) { group = "도급사(제공-일반A)"; }
            else if (group.Equals("11")) { group = "도급사(제공-일반B)"; }
            else if (group.Equals("12")) { group = "도급사(제공-기타A)"; }
            else if (group.Equals("13")) { group = "도급사(제공-기타B)"; }
            else if (group.Equals("14")) { group = "도급사(비제공)"; }
            else if (group.Equals("15")) { group = "고객(선주)"; }
            else if (group.Equals("16")) { group = "고객(선급)"; }
            else if (group.Equals("17")) { group = "기술연수생"; }
            else if (group.Equals("18")) { group = "파견출장(제공-상주)"; }
            else if (group.Equals("19")) { group = "파견출장(제공-기타)"; }
            else if (group.Equals("20")) { group = "파견출장(비제공)"; }
            else { group = "%%"; }
            return group;
        }

        public Boolean chkReturn(String num)
        {
            if (num.Equals("1"))
            {
                return true;
            }
            return false;
        }

        public String checkConverter(Boolean ch1, Boolean ch2, Boolean ch3, Boolean ch4)
        {
            int Sch1, Sch2, Sch3, Sch4;

            if (ch1) { Sch1 = 128; } else { Sch1 = 0; }
            if (ch2) { Sch2 = 64; } else { Sch2 = 0; }
            if (ch3) { Sch3 = 32; } else { Sch3 = 0; }
            if (ch4) { Sch4 = 16; } else { Sch4 = 0; }
            return "" + (Sch1 + Sch2 + Sch3 + Sch4);
        }

        public void insertUser(String USER_GROUP, String COMP_CODE, String COMP_NAME,
                               String COMP_NUMBER, String CEO_NAME, String PHONE_NUMBER,
                               String empCode, String empName, String PRICE_GROUP,
                               String AUTHORITY_CODE, String ACCOUNT_CODE, String MANAGER_ID,
                               String PREPAID_CARD, String BUSINESS_CARD, String IDCARD_TYPE,
                               String USER_YN, String userINfoCode)
        {
            if (COMP_CODE.Equals("")) { MessageBox.Show("업체코드를 입력해 주세요"); return; }
            if (empCode.Equals("")) { MessageBox.Show("사번을 입력해 주세요"); return; }
            if (empName.Equals("")) { MessageBox.Show("성명을 입력해 주세요"); return; }
            if (COMP_NUMBER.Equals("")) { MessageBox.Show("전화번호를 입력해 주세요"); return; }
            if (AUTHORITY_CODE.Equals("")) { MessageBox.Show("출입권한을 입력해 주세요"); return; }
            if (PREPAID_CARD.Equals("")) { MessageBox.Show("선불여부를 입력해 주세요"); return; }
            if (BUSINESS_CARD.Equals("")) { MessageBox.Show("업무용 여부를 입력해 주세요"); return; }
            if (IDCARD_TYPE.Equals("")) { MessageBox.Show("카드구분을 입력해 주세요"); return; }
            if (PRICE_GROUP.Equals("") && USER_GROUP.Equals("")) { MessageBox.Show("식사그룹 혹은 정산그룹을 입력해 주세요"); return; }
            if (ACCOUNT_CODE.Equals("")) { MessageBox.Show("정산방법을 입력해 주세요"); return; }

            siksuRepository.insertUser(groupConverter(USER_GROUP), COMP_CODE, COMP_NAME,
                                       COMP_NUMBER, CEO_NAME, PHONE_NUMBER,
                                       empCode, empName, groupConverter(PRICE_GROUP),
                                       AUTHORITY_CODES(AUTHORITY_CODE), ACCOUNT_CODES(ACCOUNT_CODE), MANAGER_ID,
                                       PREPAID_CARDS(PREPAID_CARD), BUSINESS_CARDS(BUSINESS_CARD), IDCARD_TYPES(IDCARD_TYPE),
                                       USER_YN, userINfoCode);
        }
    }
}
