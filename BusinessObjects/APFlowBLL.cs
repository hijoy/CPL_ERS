using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.FormDSTableAdapters;
using System.Data.SqlClient;
using System.Web;
using lib.wf;
using System.Net.Mail;
using System.Configuration;

namespace BusinessObjects {
    public class APFlowBLL {
        public string emailTo = //"chao.fan@open-groupe.com;" +
            "hui.gu@open-groupe.com;";// +
        //"fan_chao1990@163.com;" +
        //" timothy@unismart.net;" +
        //"Terence_huang@campbellswire.com;" +
        //"Jeson_wu@campbellswire.com;" +
        //"ken_mak@campbellsoup.com;" +
        //"huangsonghua66@126.com;" +
        //"huangsonghua66@gmail.com;" +
        //"huangsonghua66@hotmail.com;" +
        //"jeson.woo@gmail.com;";

        private APHelper _AP;
        public APHelper AP {
            get {
                if (_AP == null) {
                    _AP = new APHelper();
                }
                return _AP;
            }
        }

        private AuthorizationBLL _AuthorizationBLL;
        public AuthorizationBLL AuthorizationBLL {
            get {
                if (_AuthorizationBLL == null) {
                    _AuthorizationBLL = new AuthorizationBLL();
                }
                return _AuthorizationBLL;
            }
        }

        private FormDSTableAdapters.FormTableAdapter _TAForm;
        public FormDSTableAdapters.FormTableAdapter TAForm {
            get {
                if (_TAForm == null) {
                    _TAForm = new FormTableAdapter();
                }
                return _TAForm;
            }
        }

        string mailBody = "{0}<br>&nbsp;&nbsp;<b>系统入口：</b><a href='" + System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ERSEntry") + "'>Campbell Swire 协同办公系统</a>";

        public void ScrapForm(int FormID) {
            FormDS.FormRow formRow = new FormTableAdapter().GetDataByID(FormID)[0];
            formRow.StatusID = (int)SystemEnums.FormStatus.Scrap;
            new FormTableAdapter().Update(formRow);
        }

        private List<int> GetIDsByP(string p_strids) {
            List<int> result = new List<int>();
            string[] l_strids = p_strids.Split('P');
            foreach (string l_strid in l_strids) {
                if (!string.IsNullOrEmpty(l_strid)) {
                    result.Add(int.Parse(l_strid));
                }
            }
            return result;
        }
        //审批方法
        public void ApproveForm(int formID, int stuffUserId, string stuffName, bool pass, string comment, string ProxyStuffName,bool CheckPeriod = false) {
            string email = string.Empty;
            SqlTransaction transaction = null;
            try {
                string l_strtitle = "";
                StringBuilder l_strbody = new StringBuilder();
                FormTableAdapter TAMainForm = new FormTableAdapter();
                transaction = TableAdapterHelper.BeginTransaction(TAMainForm);

                FormDS.FormRow formRow = TAMainForm.GetDataByID(formID)[0];
                string returnValue = AP.approve(pass, comment, stuffUserId.ToString(), stuffName, formRow.ProcID, ProxyStuffName, formRow.OrganizationUnitID, ref email,CheckPeriod);
                // UtilityBLL ubll = new UtilityBLL();
                string[] approveinfo = AP.GetProcessApproveUser(formRow.ProcID);
                if (AP.GetProcessIsEnd(formRow.ProcID)) {

                    formRow.LastApprover = stuffUserId;
                    formRow.Comment = comment;
                    formRow.ApprovedDate = Convert.ToDateTime(approveinfo[0]);
                }
                formRow.ApproverIds = approveinfo[3];
                if (returnValue == null) {
                    formRow.InTurnUserIds = "P";
                    formRow.InTurnPositionIds = "P";
                    formRow.ApprovedDate = DateTime.Now;
                    if (pass) {
                        //如果审批通过且返回值为空则该流程结束
                        formRow.StatusID = (int)SystemEnums.FormStatus.ApproveCompleted;
                    } else {
                        //如果不通过则为驳回
                        formRow.StatusID = (int)SystemEnums.FormStatus.Rejected;
                    }
                } else {
                    string[] InTurn = returnValue.Split('&');//不同流程角色下的人员和职位
                    string ids = "";
                    string pids = "";
                    for (int a = 0; a < InTurn.Length; a++) {
                        ids += InTurn[a].Split('$')[0].ToString();///人员
                        pids += InTurn[a].Split('$')[1].ToString();//职位
                    }
                    formRow.InTurnUserIds = ids;//下一步的人员
                    formRow.InTurnPositionIds = pids;//下一步的人员职位
                }
                TAMainForm.Update(formRow);
                transaction.Commit();
                #region 发送邮件
                try {
                    QueryDS.FormViewRow l_drformView = new FormQueryBLL().GetFormViewByID(formID);
                    if (pass) {
                        if (!string.IsNullOrEmpty(returnValue)) {
                            l_strtitle = "有一份" + l_drformView.FormTypeName + "单据,编号为：" + l_drformView.FormNo + ",等待您的审批！";
                            l_strbody.Append("您好,<br>");
                            l_strbody.Append("&nbsp;&nbsp;有一份" + l_drformView.StuffName + "提交的" + l_drformView.FormTypeName + "单据,编号：" + l_drformView.FormNo + ",等待您的审批！");
                            l_strbody.Append("<br>此邮件请勿回复！");
                            mailBody = string.Format(mailBody, l_strbody.ToString());
                            sendMail(email, "", l_strtitle, mailBody);
                            //AP.sendMail(emailTo, "", l_strtitle, mailBody);
                        } else {
                            l_strtitle = "您的" + l_drformView.FormTypeName + "单据,编号为：" + l_drformView.FormNo + ",已审批完成！";
                            l_strbody.Append("您好,<br>");
                            l_strbody.Append("&nbsp;&nbsp;您于" + l_drformView.SubmitDate.ToString("yyyy-MM-dd") + "提交的" + l_drformView.FormTypeName + "单据,编号：" + l_drformView.FormNo + ",已经审批通过！");
                            l_strbody.Append("<br>此邮件请勿回复！");
                            mailBody = string.Format(mailBody, l_strbody.ToString());
                            sendMail(AuthorizationBLL.GetStuffUserById(formRow.UserID).EMail, "", l_strtitle, mailBody);
                            //AP.sendMail(emailTo, "", l_strtitle, mailBody);
                        }
                    } else {
                        l_strtitle = "您的" + l_drformView.FormTypeName + "单据,编号为：" + l_drformView.FormNo + ",被驳回！";
                        l_strbody.Append("您好,<br>");
                        l_strbody.Append("&nbsp;&nbsp;您于" + l_drformView.SubmitDate.ToString("yyyy-MM-dd") + "提交的" + l_drformView.FormTypeName + "单据,编号：" + l_drformView.FormNo + ",被" + stuffName + "驳回！");
                        l_strbody.Append("<br>此邮件请勿回复！");
                        mailBody = string.Format(mailBody, l_strbody.ToString());
                        sendMail(AuthorizationBLL.GetStuffUserById(formRow.UserID).EMail, "", l_strtitle, mailBody);
                        //AP.sendMail(emailTo, "", l_strtitle, mailBody);
                    }
                } catch (Exception e1) {

                }
                #endregion

            } catch (Exception ex) {
                if (transaction != null) {
                    transaction.Rollback();
                }
                throw ex;
            } finally {
                if (transaction != null) {
                    transaction.Dispose();
                }
            }
        }

        //启动流程
        public APResult CreateProcess(APParameter app) {
            APResult result = new APResult();
            string email = string.Empty;
            string l_strtitle = "";
            StuffUserBLL stuffuserbll = new StuffUserBLL();
            StringBuilder l_strbody = new StringBuilder();
            OUTreeBLL outreebll = new OUTreeBLL();
            string Deptment = outreebll.GetFlowParameter(app.OrganizationUnitID);
            Dictionary<string, object> Dictionary = app.Dic;
            Dictionary["Department"] = Deptment;
            AuthorizationDS.PositionRow position = outreebll.GetPositionById(app.PositionID);
            if (position.IsFlowLevelNull()) {
                Dictionary["FlowLevel"] = "";
            } else {
                Dictionary["FlowLevel"] = position.FlowLevel;
            }
            string ProcID = AP.createProcess(app.FormNo + ":" + DateTime.Now.ToString(), new AuthorizationBLL().GetFlowTemplateNameByFormTypeAndUserID(app.FormTypeID, app.UserID), Dictionary);
            result.ProcID = ProcID;

            string[] InTurn = AP.startProcess(ProcID, app.OrganizationUnitID, ref email).Split('&');//不同流程角色下的人员和职位
            string ids = "";
            string pids = "";
            for (int a = 0; a < InTurn.Length; a++) {
                ids += InTurn[a].Split('$')[0].ToString();///人员
                pids += InTurn[a].Split('$')[1].ToString();//职位
            }
            result.ApprovedDate = DateTime.Now;
            result.InTurnUserIds = ids;//下一步的人员
            result.InTurnPositionIds = pids;//下一步的人员职位
            result.StatusID = (int)SystemEnums.FormStatus.Awaiting;
            //自动审批下一个节点是自己的
            if (!(ids.IndexOf("27") >= 0)) {
                if (ids.IndexOf(app.UserID.ToString()) > 0) {
                    APResult approveresult = ApproveForm(app, app.FormID, app.UserID, stuffuserbll.GetStuffUserById(app.UserID)[0].StuffName, true, "", "", ProcID);
                    result.InTurnUserIds = approveresult.InTurnUserIds;
                    result.InTurnPositionIds = approveresult.InTurnPositionIds;
                }
            }

            try {
                if (!string.IsNullOrEmpty(email)) {
                    //QueryDS.FormViewRow l_drformView = new FormQueryBLL().GetFormViewByID(app.FormID);
                    l_strtitle = "有一份单据编号为：" + app.FormNo + ",等待您的审批！";
                    l_strbody.Append("您好,<br>");
                    l_strbody.Append("&nbsp;&nbsp;您有一份" + new StuffUserBLL().GetStuffUserById(app.UserID)[0].StuffName + "提交的单据,编号为：" + app.FormNo + ",等待您的审批！");

                    l_strbody.Append("<br>此邮件请勿回复！");
                    mailBody = string.Format(mailBody, l_strbody.ToString());
                    sendMail(email, "", l_strtitle, mailBody);
                    //AP.sendMail(emailTo, "", l_strtitle, mailBody);
                }
            } catch {
            }
            return result;
        }

        public APResult ApproveForm(APParameter app, int formID, int stuffUserId, string stuffName, bool pass, string comment, string ProxyStuffName, string proctID) {
            APResult result = new APResult();
            string email = string.Empty;
            try {
                string l_strtitle = "";
                StringBuilder l_strbody = new StringBuilder();
                FormTableAdapter TAMainForm = new FormTableAdapter();
                //FormDS.FormRow formRow = TAMainForm.GetDataByID(formID)[0];

                string returnValue = AP.approve(pass, comment, stuffUserId.ToString(), stuffName, proctID, ProxyStuffName, app.OrganizationUnitID, ref email,false);
                // UtilityBLL ubll = new UtilityBLL();
                string[] approveinfo = AP.GetProcessApproveUser(proctID);
                if (returnValue != null) {
                    string[] InTurn = returnValue.Split('&');//不同流程角色下的人员和职位
                    string ids = "";
                    string pids = "";
                    for (int a = 0; a < InTurn.Length; a++) {
                        ids += InTurn[a].Split('$')[0].ToString();///人员
                        pids += InTurn[a].Split('$')[1].ToString();//职位
                    }
                    result.InTurnUserIds = ids;//下一步的人员
                    result.InTurnPositionIds = pids;//下一步的人员职位
                }
                #region 发送邮件
                try {
                    QueryDS.FormViewRow l_drformView = new FormQueryBLL().GetFormViewByID(formID);
                    if (pass) {
                        if (!string.IsNullOrEmpty(returnValue)) {
                            l_strtitle = "有一份" + l_drformView.FormTypeName + "单据,编号为：" + l_drformView.FormNo + ",等待您的审批！";
                            l_strbody.Append("您好,<br>");
                            l_strbody.Append("&nbsp;&nbsp;有一份" + l_drformView.StuffName + "提交的" + l_drformView.FormTypeName + "单据,编号：" + l_drformView.FormNo + ",等待您的审批！");
                            l_strbody.Append("<br>此邮件请勿回复！");
                            mailBody = string.Format(mailBody, l_strbody.ToString());
                            sendMail(email, "", l_strtitle, mailBody);
                            //AP.sendMail(emailTo, "", l_strtitle, mailBody);
                        } else {
                            l_strtitle = "您的" + l_drformView.FormTypeName + "单据,编号为：" + l_drformView.FormNo + ",已审批完成！";
                            l_strbody.Append("您好,<br>");
                            l_strbody.Append("&nbsp;&nbsp;您于" + l_drformView.SubmitDate.ToString("yyyy-MM-dd") + "提交的" + l_drformView.FormTypeName + "单据,编号：" + l_drformView.FormNo + ",已经审批通过！");
                            l_strbody.Append("<br>此邮件请勿回复！");
                            mailBody = string.Format(mailBody, l_strbody.ToString());
                            sendMail(AuthorizationBLL.GetStuffUserById(app.UserID).EMail, "", l_strtitle, mailBody);
                            //AP.sendMail(emailTo, "", l_strtitle, mailBody);
                        }
                    } else {
                        l_strtitle = "您的" + l_drformView.FormTypeName + "单据,编号为：" + l_drformView.FormNo + ",被驳回！";
                        l_strbody.Append("您好,<br>");
                        l_strbody.Append("&nbsp;&nbsp;您于" + l_drformView.SubmitDate.ToString("yyyy-MM-dd") + "提交的" + l_drformView.FormTypeName + "单据,编号：" + l_drformView.FormNo + ",被" + stuffName + "驳回！");
                        l_strbody.Append("<br>此邮件请勿回复！");
                        mailBody = string.Format(mailBody, l_strbody.ToString());
                        sendMail(AuthorizationBLL.GetStuffUserById(app.UserID).EMail, "", l_strtitle, mailBody);
                        //AP.sendMail(emailTo, "", l_strtitle, mailBody);
                    }
                } catch {

                }
                #endregion

                return result;
            } catch (Exception ex) {
                throw ex;
            }
        }

        //根据FormID获取审批信息 审批人（审批时间）> ...
        //public string GetApprovalInfoByFormID(int FormID) {
        //    string Result = String.Empty;

        //    FormDS.FormRow rowForm = new FormTEBLL().GetFormByID(FormID)[0];
        //    Result += new AuthorizationBLL().GetStuffUserById(rowForm.UserID).StuffName + "(" + rowForm.SubmitDate.ToString("yyyy-MM-dd HH:mm") + ") > ";
        //    if (!rowForm.IsProcIDNull()) {
        //        Result += AP.GetApproverNamesByProcID(rowForm.ProcID);
        //    }

        //    if (Result.Length > 3) {
        //        Result = Result.Substring(0, Result.Length - 3);
        //    }

        //    return Result;
        //}
        //发送邮件
        public void sendMail(string to, string cc, string title, string body) {
            string strFrom = ConfigurationManager.AppSettings["EmailAlert.SendFrom"];
            string strEmailUser = ConfigurationManager.AppSettings["EmailAlert.EmailUser"];
            string pid = ConfigurationManager.AppSettings["EmailAlert.EmailPwd"];
            string strServer = ConfigurationManager.AppSettings["EmailAlert.EmailServer"];
            string strStatus = ConfigurationManager.AppSettings["EmailAlert.Status"];

            if (string.IsNullOrEmpty(strStatus) || !strStatus.ToLower().Equals("on")) {
                return;
            }

            MasterData.EmailHistoryDataTable tbHistory = new MasterData.EmailHistoryDataTable();
            MasterData.EmailHistoryRow row = null;

            if (String.IsNullOrEmpty(to)) {
                to = "";
            }
            if (String.IsNullOrEmpty(cc)) {
                cc = "";
            }
            if (String.IsNullOrEmpty(title)) {
                title = "";
            }
            if (String.IsNullOrEmpty(body)) {
                body = "";
            }

            try {
                if (!string.IsNullOrEmpty(to) || !string.IsNullOrEmpty(cc)) {
                    MailMessage mail = new MailMessage();
                    string[] strs = to.Split(";；,，".ToCharArray());
                    for (int i = 0; i < strs.Length; i++) {
                        if (strs[i] != null && !"".Equals(strs[i].Trim()) && strs[i].IndexOf("@") > 0) {
                            mail.To.Add(strs[i]);
                        }
                    }

                    if (!string.IsNullOrEmpty(cc)) {
                        strs = cc.Split(";；,，".ToCharArray());
                        for (int i = 0; i < strs.Length; i++) {
                            if (strs[i] != null && !"".Equals(strs[i].Trim()) && strs[i].IndexOf("@") > 0) {
                                mail.CC.Add(strs[i]);
                            }
                        }
                    }

                    row = tbHistory.NewEmailHistoryRow();
                    row.SentTo = to;
                    row.EmailContent = body;
                    row.SendDate = DateTime.Now;
                    row.Result = "Success send mail:" + title;
                    row.ResultType = 1;

                    mail.From = new MailAddress(strFrom);

                    mail.Subject = title;
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = body;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential(strEmailUser, pid);
                    smtpClient.Host = strServer;

                    smtpClient.Send(mail);
                }
            } catch (Exception e) {
                row.Result = "Failed:" + e.ToString();
                row.ResultType = 0;
                Console.WriteLine(e.Message);
            }
            tbHistory.AddEmailHistoryRow(row);
            new MasterDataTableAdapters.EmailHistoryTableAdapter().Update(row);
        }
    }
}
