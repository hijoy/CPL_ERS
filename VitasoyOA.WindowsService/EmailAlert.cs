using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

namespace VitasoyOA.WindowsService {
    public class EmailAlert {
        EmailTableAdapters.FormTableAdapter TAform = new EmailTableAdapters.FormTableAdapter();
        EmailTableAdapters.FormViewTableAdapter TAview = new EmailTableAdapters.FormViewTableAdapter();
        EmailTableAdapters.StuffUserTableAdapter TAstuff = new EmailTableAdapters.StuffUserTableAdapter();
        string style_td = "style='border:1px solid gray;width:200px;height:60px;text-align:center;'";
        string style_table = "style='border:1px solid gray;border-collapse:collapse;'";

        public static bool IsRunning = false;

        //the main func to send email to approvers
        public void SendAlertToApprovers() {

            DataTable dt = TAform.GetData();
            string userids = string.Empty;

            //先拼接成字符串
            foreach (DataRow dr in dt.Rows) {
                userids += dr[0];
            }
            userids = userids.Replace("PP", "P");


            List<string> list = new List<string>();
            //筛选出userid
            string[] strUser = userids.Split('P');
            for (int i = 0; i < strUser.Length; i++) {
                if (!list.Contains(strUser[i])) {
                    list.Add(strUser[i]);
                }
            }

            string strBody = string.Empty;
            string strTitle = ConfigurationManager.AppSettings["EmailAlert.Subject"];
            string strFrom = ConfigurationManager.AppSettings["EmailAlert.SendFrom"];
            string strEmailUser = ConfigurationManager.AppSettings["EmailAlert.EmailUser"];
            string pid = ConfigurationManager.AppSettings["EmailAlert.EmailPwd"];
            string strServer = ConfigurationManager.AppSettings["EmailAlert.EmailServer"];

            //根据userid发送邮件
            for (int n = 0; n < list.Count; n++) {
                if (string.IsNullOrEmpty(list[n])) {
                    continue;
                } else {
                    string strAddress = TAstuff.GetDataByID(int.Parse(list[n]))[0].EMail;
                    strBody = GetBody(list[n]);
                    Utility.SendMail(strAddress, "", strTitle, strBody, strFrom, strEmailUser, pid, strServer);
                }
            }


        }

        public string GetBody(string userid) {

            StringBuilder sb = new StringBuilder();


            DataTable dt = TAview.GetDataByUserID(userid);

            if (dt.Rows.Count > 0) {
                sb.Append("<div>");
                sb.Append("<div>等待您审批的单据<div>");
                sb.Append("<div>系统入口:<a href='" + ConfigurationManager.AppSettings["ERSEntry"] + "'>Campbell Swire 协同办公系统</a><div>");
                sb.Append("<table " + style_table + ">");
                sb.Append("<tr>");
                sb.Append("<td " + style_td + ">单据类型</td>");
                sb.Append("<td " + style_td + ">单据编号</td>");
                sb.Append("<td " + style_td + ">申请人</td>");
                sb.Append("<td " + style_td + ">申请时间</td>");
                sb.Append("</tr>");

                foreach (DataRow dr in dt.Rows) {
                    sb.Append("<tr>");
                    sb.Append("<td " + style_td + ">" + dr["FormTypeName"] + "</td>");
                    sb.Append("<td " + style_td + ">" + dr["FormNo"] + "</td>");
                    sb.Append("<td " + style_td + ">" + dr["StuffName"] + "</td>");
                    sb.Append("<td " + style_td + ">" + Convert.ToDateTime(dr["SubmitDate"]).ToString("yyyy-MM-dd") + "</td>");
                    sb.Append("</tr>");
                }





                sb.Append("</table>");
                sb.Append("</div>");

            }
            return sb.ToString();

        }



    }
}
