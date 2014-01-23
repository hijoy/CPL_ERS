﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Web;
using Ascentn.Workflow.Base;

namespace lib.wf {

    public class APHelper {

        public APHelper() {
            api = createAPI();
            if (api == null) {
                throw new Exception("init - api is null");
            }
            sysEntry = (String)ConfigurationManager.AppSettings["ERSEntry"];
        }

        //节点状态
        public class FlowNodeStatus {
            public const string APPROVED = "APPROVED";   //
            public const string CANCELLED = "CANCELLED";   //
            public const string ASSIGNED = "ASSIGNED";   //
            public const string ONERROR = "ONERROR";   //
            public const string Completed = "Completed";   //

        }

        //流程模板
        public class ProcessTemplateName {
            public const string Test = "Simple_Template";
            public const string ProjectFormApply = "方案申请";
            public const string ProjectReimburseFormApply = "方案报销";
            public const string ContractFormApply = "合同申请";
            public const string MaterialFormApply = "广宣物资申请";
            public const string PersonalReimburseFormApply = "个人费用报销";
            public const string BudgetAllocationFormApply = "预算调拨申请";
        }

        //流程属性
        public class ProcessAttributeName {
            internal const string InturnUserEmail = "ERS_PROC_VAR_INTURNUSEREMAIL";   //邮件地址
        }

        private const string errorMSGPrefix = "Agile Point :";

        private const string EmailTempName = "WorkAssigned";

        private WorkflowService api = null;

        private string sysEntry = "";

        private static WorkflowService createAPI() {
            try {
                string url = (String)ConfigurationManager.AppSettings["ServerUrl"];
                string admin = (String)ConfigurationManager.AppSettings["APAdmin"];
                string pass = (String)ConfigurationManager.AppSettings["APPWD"];
                string domain = (String)ConfigurationManager.AppSettings["APDomain"];
                WorkflowService api = new WorkflowService(url);
                System.Net.ICredentials credentials = new System.Net.NetworkCredential(admin, pass, domain);
                api.Credentials = credentials;
                api.CheckAuthenticated();
                api.SetClientAppName("ERS");
                return api;
            } catch (Exception e) {
                throw new Exception(errorMSGPrefix + e.Message, e);
            }
        }

        private WFEvent getFullEvent(WFEvent evt) {
            while (evt.Status == WFEvent.SENT) {
                System.Threading.Thread.Sleep(500);
                evt = api.GetEvent(evt.EventID);
            }
            return evt;
        }

        public string createProcess(string processName, string processTemplateName, Dictionary<string, Object> param) {
            try {
                if (String.IsNullOrEmpty(processName)) {
                    throw new Exception("createProcess - processName is empty");
                }
                if (String.IsNullOrEmpty(processTemplateName)) {
                    throw new Exception("createProcess - processTemplateName is empty");
                }

                string processTemplateID = api.GetReleasedPID(processTemplateName);
                if (processTemplateID == null) {
                    throw new Exception("createProcess - 流程模板" + processTemplateName + "未定义");
                }

                string PiID = Ascentn.Workflow.Base.UUID.GetID();
                string Woid = Ascentn.Workflow.Base.UUID.GetID();
                WFEvent evt = api.CreateProcInst(processTemplateID, PiID, processName, Woid, null, false);
                evt = getFullEvent(evt);
                if (!String.IsNullOrEmpty(evt.Error)) {
                    throw new Exception("createProcess - " + evt.Error);
                }

                if (param != null && param.Count > 0) {
                    ArrayList attrList = new ArrayList();
                    foreach (string key in param.Keys) {
                        attrList.Add(new NameValue(key, param[key]));
                    }
                    NameValue[] attributes = (NameValue[])attrList.ToArray(typeof(NameValue));
                    api.SetCustomAttrs(Woid, attributes);
                }
                return PiID;
            } catch (Exception e) {
                throw new Exception(errorMSGPrefix + e.Message, e);
            }
        }

        public string startProcess(string processInstanceID, int OrganizationUnitID, ref string email) {
            try {
                if (String.IsNullOrEmpty(processInstanceID)) {
                    throw new Exception("startProcess - processInstanceID is empty");
                }
                WFEvent evt = api.StartProcInst(processInstanceID);
                evt = getFullEvent(evt);
                if (!String.IsNullOrEmpty(evt.Error)) {
                    throw new Exception("startProcess - " + evt.Error);
                }
                return prepareNextNode(processInstanceID, "PROCESS START", OrganizationUnitID, ref email);
                //return "111";
            } catch (Exception e) {
                try {
                    api.CancelProcInst(processInstanceID);
                } catch (Exception ex) {
                }
                if (e is ApplicationException)
                    throw e;
                throw new Exception(errorMSGPrefix + e.Message, e);
            }
        }

        //得到审核的历史
        public APWorkFlow.NodeStatusDataTable getApprovalStatus(string processInstanceID) {
            try {
                if (String.IsNullOrEmpty(processInstanceID))
                    throw new Exception("getAprovalStatus - processInstanceID is empty");
                //string sql = "SELECT ASSIGNED_DATE, CANCELLED_DATE, COMPLETED_DATE, DUE_DATE, CREATED_DATE, STATUS, USER_ID, CLIENT_DATA,'' as " + StatusColumnName.APPROVED_BY + ", 0 as " + StatusColumnName.IS_APPROVED + ",'' as " + StatusColumnName.COMMENTS + " FROM WF_MANUAL_WORKITEMS where PROC_INST_ID='" + processInstanceID + "' order by ASSIGNED_DATE asc";
                string sql = "select CLIENT_DATA FROM WF_MANUAL_WORKITEMS where PROC_INST_ID='" + processInstanceID + "' order by ASSIGNED_DATE asc";
                String[] sqlRet = api.QueryDatabaseEx(sql);
                DataTable dt = getDataTableFromXML(sqlRet[0], sqlRet[1]);
                if (dt != null) {
                    APWorkFlow.NodeStatusDataTable result = new APWorkFlow.NodeStatusDataTable();
                    foreach (DataRow row in dt.Rows) {
                        string clientData;
                        try {
                            clientData = (string)row["CLIENT_DATA"];
                        } catch (Exception ee) {
                            clientData = null;
                        }
                        if (!String.IsNullOrEmpty(clientData)) {
                            APWorkFlow.NodeStatusDataTable cddt = new APWorkFlow.NodeStatusDataTable();
                            StringReader sr = new StringReader(clientData);
                            cddt.ReadXml(sr);
                            if (cddt != null && cddt.Rows.Count > 0) {
                                result.ImportRow(cddt[0]);
                            }
                        }
                    }
                    return result;
                } else {
                    return null;
                }
            } catch (Exception e) {
                throw new Exception(errorMSGPrefix + e.Message, e);
            }
        }

        public string approve(bool isApproved, string approvalComment, string userID, string userName, string processInstanceID, string proxyUserName, int OrganizationUnitID, ref string email, bool CheckPeriod) {
            try {
                if (String.IsNullOrEmpty(processInstanceID)) {
                    throw new Exception("approve - processInstanceID is empty");
                }
                if (String.IsNullOrEmpty(userID)) {
                    throw new Exception("approve - userID is empty");
                }
                if (String.IsNullOrEmpty(userName)) {
                    throw new Exception("approve - userName is empty");
                }

                string where = "WF_MANUAL_WORKITEMS.PROC_INST_ID='" + processInstanceID + "' and WF_MANUAL_WORKITEMS.STATUS in ('" + WFManualWorkItem.ASSIGNED + "','" + WFManualWorkItem.OVERDUE + "','" + WFManualWorkItem.REASSIGNED + "','" + WFManualWorkItem.PSEUDO + "') and WF_MANUAL_WORKITEMS.USER_ID like '%P" + userID + "P%'";
                WFManualWorkItem[] wis = api.QueryWorkListEx(where);

                if (wis.Length > 0) {
                    WFManualWorkItem wi = wis[0];
                    APWorkFlow.NodeStatusDataTable dt = new APWorkFlow.NodeStatusDataTable();
                    StringReader sr = new StringReader(wi.ClientData);
                    dt.ReadXml(sr);
                    APWorkFlow.NodeStatusRow dr = dt[0];
                    if (String.IsNullOrEmpty(proxyUserName)) {
                        dr.APPROVED_BY = userName;
                    } else {
                        dr.APPROVED_BY = proxyUserName + " 代理 " + userName;
                    }
                    if (String.IsNullOrEmpty(approvalComment)) {
                        dr.COMMENTS = "";
                    } else {
                        dr.COMMENTS = approvalComment;
                    }
                    dr.COMPLETED_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    dr.NodeName = wi.DisplayName;
                    if (isApproved)//若状态为通过
                    {
                        //  首先判断是否为尝试修复的
                        if (dr.STATUS.Equals(FlowNodeStatus.ONERROR)) {
                            // recover this node
                            System.Collections.ArrayList attrList = new System.Collections.ArrayList();
                            attrList.Add(new NameValue("UserID", wi.OriginalUserID));
                            NameValue[] attributes = (NameValue[])attrList.ToArray(typeof(NameValue));
                            api.UpdateWorkItem(wi.WorkItemID, attributes);
                        } else {
                            if (CheckPeriod) {
                                throw new ApplicationException("该费用期间已关帐，请退回重新提交！");
                            }
                            dr.STATUS = FlowNodeStatus.APPROVED;//通过
                            //dr.AcceptChanges();
                            dt.AcceptChanges();
                            StringWriter sw = new StringWriter();
                            dt.WriteXml(sw);
                            string clientData = sw.ToString();//将信息写入XML并保存

                            WFEvent evt = api.CompleteWorkItemEx(wi.WorkItemID, clientData);//完成当前步骤
                            evt = getFullEvent(evt);
                            if (!String.IsNullOrEmpty(evt.Error)) {
                                throw new Exception("approve - " + evt.Error);
                            }
                        }
                    } else {
                        //直接取消流程
                        if (dr.STATUS.Equals(FlowNodeStatus.ONERROR)) {
                            dr.COMMENTS = dr.ERROR_MSG;
                        }
                        dr.STATUS = FlowNodeStatus.CANCELLED;
                        //dr.AcceptChanges();
                        dt.AcceptChanges();
                        StringWriter sw = new StringWriter();
                        dt.WriteXml(sw);
                        string clientData = sw.ToString();

                        ArrayList attrList = new ArrayList();
                        attrList.Add(new NameValue("CLIENT_DATA", clientData));
                        NameValue[] nv = (NameValue[])attrList.ToArray(typeof(NameValue));
                        api.UpdateWorkItem(wi.WorkItemID, nv);
                        WFEvent evt = api.CancelProcInst(processInstanceID);
                        evt = getFullEvent(evt);
                        if (!String.IsNullOrEmpty(evt.Error)) {
                            throw new Exception("approve - " + evt.Error);
                        }
                    }
                    //prepare next node 
                    string nextUser = prepareNextNode(processInstanceID, userID, OrganizationUnitID, ref email);
                    if (!String.IsNullOrEmpty(nextUser) && nextUser.IndexOf("P" + userID + "P") >= 0) {// 如果下一步包有且只有同一个人就自动审批(后面需要在审批历史里加入自动通过的标识)
                        return approve(isApproved, approvalComment, userID, userName, processInstanceID, proxyUserName, OrganizationUnitID, ref email, CheckPeriod);
                    } else {
                        return nextUser;
                    }
                } else {
                    throw new Exception("approve - 用户不能操作当前流程");
                }
            } catch (Exception e) {
                if (e is ApplicationException) {
                    throw e;
                }
                throw new Exception(errorMSGPrefix + e.Message, e);
            }
        }

        //找到下个节点
        private string prepareNextNode(string processInstanceID, string lastUserID, int OrganizationUnitID, ref string email) {
            WFManualWorkItem nextNode = getNextNode(processInstanceID);
            if (nextNode == null) {
                return null; //process finished
            } else {
                string participant = "";
                string participantEmail = "";
                nextNode.UserID = nextNode.UserID.Split('\\')[1].ToString();

                //translate participant(防止多个流程角色)
                string[] ids = nextNode.OriginalUserID.Split('\\')[1].ToString().Split("|".ToCharArray());
                for (int i = 0; i < ids.Length; i++) {
                    string uid = string.Empty;
                    string PositionTypeCode = ids[i].ToString();
                    getUserIDByIdentifier(PositionTypeCode, OrganizationUnitID, ref uid, ref email);

                    //当前角色下找不到人
                    if (uid == "none") {
                        continue;
                    } else {
                        participant += uid + "&";
                        participantEmail += email;
                    }
                }
                participant = participant.TrimEnd('&');

                // update workitem
                APWorkFlow.NodeStatusDataTable dt = new APWorkFlow.NodeStatusDataTable();
                APWorkFlow.NodeStatusRow dr = dt.NewNodeStatusRow();
                if (!String.IsNullOrEmpty(participant) && participant != "none") { //user found
                    dr.STATUS = FlowNodeStatus.ASSIGNED;
                    dr.PARTICIPANT = participant;
                    //dr.AcceptChanges();
                    dt.AddNodeStatusRow(dr);
                    dt.AcceptChanges();
                    StringWriter sw = new StringWriter();
                    dt.WriteXml(sw);
                    string clientData = sw.ToString();

                    System.Collections.ArrayList attrList = new System.Collections.ArrayList();
                    attrList.Add(new NameValue("UserID", dr.PARTICIPANT));//更新该步骤的userid
                    attrList.Add(new NameValue("CLIENT_DATA", clientData));
                    NameValue[] attributes = (NameValue[])attrList.ToArray(typeof(NameValue));
                    api.UpdateWorkItem(nextNode.WorkItemID, attributes);
                    //update email
                    if (!String.IsNullOrEmpty(participantEmail)) {
                        email = participantEmail;
                    }
                    return dr.PARTICIPANT;
                } else { // 没有找到人员
                    dr.STATUS = FlowNodeStatus.ONERROR;
                    dr.PARTICIPANT = "P" + lastUserID + "P";
                    dr.ERROR_MSG = "错误代码：Invalid UserId (" + nextNode.ProcInstName + "," + nextNode.OriginalUserID + "," + nextNode.UserID + ")";
                    //dr.AcceptChanges();
                    dt.AddNodeStatusRow(dr);
                    dt.AcceptChanges();
                    StringWriter sw = new StringWriter();
                    dt.WriteXml(sw);
                    string clientData = sw.ToString();

                    System.Collections.ArrayList attrList = new System.Collections.ArrayList();
                    attrList.Add(new NameValue("UserID", dr.PARTICIPANT));
                    attrList.Add(new NameValue("CLIENT_DATA", clientData));
                    NameValue[] attributes = (NameValue[])attrList.ToArray(typeof(NameValue));
                    api.UpdateWorkItem(nextNode.WorkItemID, attributes);

                    // sendMail(getUserEmailByID(int.Parse(lastUserID)), "", "您提交的单据发生错误", "单据编号为：" + nextNode.ProcInstName);

                    throw new ApplicationException(dr.ERROR_MSG);
                }
            }
        }

        //找到下个结点
        private WFManualWorkItem getNextNode(string processInstanceID) {
            if (String.IsNullOrEmpty(processInstanceID))
                throw new Exception("getCurrentParticipant - processInstanceID is empty");
            WFBaseProcessInstance pi = api.GetProcInst(processInstanceID);
            int times = 0;
            while (times < 30) { // wait for 30 times, 
                if (pi.Status.Equals(WFProcessInstance.COMPLETED) || pi.Status.Equals(WFProcessInstance.CANCELLED)) {
                    return null;
                } else {
                    string where = "WF_MANUAL_WORKITEMS.PROC_INST_ID='" + processInstanceID + "' and WF_MANUAL_WORKITEMS.STATUS in ('" + WFManualWorkItem.ASSIGNED + "','" + WFManualWorkItem.OVERDUE + "','" + WFManualWorkItem.REASSIGNED + "','" + WFManualWorkItem.PSEUDO + "')";
                    WFManualWorkItem[] wis = api.QueryWorkListEx(where);
                    if (wis.Length > 0) {
                        return wis[0];
                    } else {
                        System.Threading.Thread.Sleep(1000); // wait time unit
                        pi = api.GetProcInst(processInstanceID);
                    }
                }
                times++;
            }
            throw new Exception("FLOWNODE NOT FOUND(" + pi.ProcInstName + ")");
        }

        private DataTable getDataTableFromXML(string xml, string schema) {
            if (String.IsNullOrEmpty(schema))
                throw new Exception("getDataTableFromXML - schema is empty");
            DataSet ds = new DataSet();
            StringReader sr = new StringReader(schema);
            ds.ReadXmlSchema(sr);
            sr = new StringReader(xml);
            ds.ReadXml(sr);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                return ds.Tables[0];
            } else {
                return null;
            }
        }

        private void getUserIDByIdentifier(string PositionTypeCode, int OrganizationUnitID, ref string userid, ref string email) {
            string Pid = string.Empty;//职位
            APWorkFlowTableAdapters.StuffUserTableAdapter TAUser = new APWorkFlowTableAdapters.StuffUserTableAdapter();
            APWorkFlow.StuffUserDataTable dt = TAUser.GetDataByNode(PositionTypeCode, OrganizationUnitID);
            if (dt.Rows.Count < 1) {
                userid = "none";//此处给出找不到人时的标识
                email = "";
            } else {
                foreach (APWorkFlow.StuffUserRow dataRow in dt.Rows) {
                    //人员
                    if (!string.IsNullOrEmpty(dataRow.StuffUserId.ToString())) {
                        userid += "P" + dataRow.StuffUserId + "P";
                    }
                    //职位
                    if (!string.IsNullOrEmpty(dataRow.PositionId.ToString())) {
                        Pid += "P" + dataRow.PositionId + "P";
                    }
                    //email
                    if (!string.IsNullOrEmpty(dataRow.EMail)) {
                        email += dataRow.EMail + ";";
                    }
                }
                userid = userid + "$" + Pid;
            }
        }

        //得到用户邮箱
        //public string getUserEmailByID(int userid) {
        //    APWorkFlowTableAdapters.StuffUserinfoTableAdapter TAUser = new APWorkFlowTableAdapters.StuffUserinfoTableAdapter();
        //    APWorkFlow.StuffUserDataTable dt = TAUser.ge(userid);
        //    return dt[0].EMail;
        //}

        //得倒绑定的属性值
        public object getValue(string pid, string name) {
            WFManualWorkItem thisNode = getNextNode(pid);
            object a = api.GetCustomAttr(thisNode.WorkObjectID, name);
            return a;
        }

        //设定绑定的属性值
        public void setValue(string pid, string name, object value) {
            WFManualWorkItem thisNode = getNextNode(pid);
            api.SetCustomAttr(thisNode.WorkObjectID, name, value);
        }

        //得到当前步骤的名称
        public string getDisplayName(string processInstanceID) {
            WFManualWorkItem thisNode = getNextNode(processInstanceID);
            return thisNode.DisplayName;
        }

        //得到所有流程模板的名称
        public DataTable GetDefNames() {
            WFBaseProcessDefinition[] wf = api.GetProcDefs();
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("DefName");
            dt.Columns.Add(dc);
            for (int i = 0; i < wf.Length; i++) {
                if (wf[i].Status == "Released") {
                    DataRow dr = dt.NewRow();
                    dr[0] = wf[i].DefName;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        //判断节点是否是最后一个节点
        public Boolean GetProcessIsEnd(string processInstanceID) {
            return api.GetProcInst(processInstanceID).Status == WFProcessInstance.COMPLETED || api.GetProcInst(processInstanceID).Status == WFProcessInstance.CANCELLED;
        }

        //
        /// <summary>
        /// ApprovedDate【0】，LastApprover【1】，Comment【2】，ApproverIds【3】
        /// </summary>
        /// <param name="processInstanceID"></param>
        /// <returns>ApprovedDate【0】，LastApprover【1】，Comment【2】，ApproverIds【3】</returns>
        public string[] GetProcessApproveUser(string processInstanceID) {

            DateTime ApprovedDate = DateTime.MinValue;
            string LastApprover = "";
            string Comment = "";
            string ApproverIds = "";
            string[] ApproveInfo = new string[4];
            APWorkFlowTableAdapters.StuffUserinfoTableAdapter TAStuffUserinfo = new APWorkFlowTableAdapters.StuffUserinfoTableAdapter();
            if (String.IsNullOrEmpty(processInstanceID))
                throw new Exception("GetProcessApproveUser - processInstanceID is empty");
            string sql = "select CLIENT_DATA FROM WF_MANUAL_WORKITEMS where CLIENT_DATA like '%</APPROVED_BY>%' and PROC_INST_ID='" + processInstanceID + "' order by ASSIGNED_DATE asc";
            String[] sqlRet = api.QueryDatabaseEx(sql);
            DataTable l_dtclient = getDataTableFromXML(sqlRet[0], sqlRet[1]);
            APWorkFlow.NodeStatusDataTable l_dtapprove = new APWorkFlow.NodeStatusDataTable();
            int i = 0;
            foreach (DataRow l_dr in l_dtclient.Rows) {
                StringReader sr = new StringReader(l_dr["CLIENT_DATA"].ToString());
                l_dtapprove.ReadXml(sr);
                if (Convert.ToDateTime(l_dtapprove[0].COMPLETED_DATE) > ApprovedDate) {
                    ApprovedDate = Convert.ToDateTime(l_dtapprove[0].COMPLETED_DATE);
                    LastApprover = TAStuffUserinfo.GetDataByStuffName(l_dtapprove[0].APPROVED_BY)[0].StuffUserId.ToString();
                    Comment = l_dtapprove[0].COMMENTS;
                }
                if (!string.IsNullOrEmpty(l_dtapprove[i].APPROVED_BY))
                    ApproverIds += ("P" + TAStuffUserinfo.GetDataByStuffName(l_dtapprove[i].APPROVED_BY)[0].StuffUserId + "P");
                i++;
            }
            ApproveInfo[0] = ApprovedDate.ToString();
            ApproveInfo[1] = LastApprover;
            ApproveInfo[2] = Comment;
            ApproveInfo[3] = ApproverIds;
            return ApproveInfo;
        }

        //如是最后一个节点 则创建一个任务
        public bool LastCreateWorkItem(string processInstanceID, int positionID, ref string approve) {
            try {
                string admin = (String)ConfigurationManager.AppSettings["APAdmin"];
                APWorkFlowTableAdapters.StuffUserTableAdapter TAStuffUser = new APWorkFlowTableAdapters.StuffUserTableAdapter();
                APWorkFlowTableAdapters.StuffUserAndPositionTableAdapter TAUserAndPosition = new APWorkFlowTableAdapters.StuffUserAndPositionTableAdapter();
                APWorkFlowTableAdapters.ProcessLastNameTableAdapter TAProcessLastName = new APWorkFlowTableAdapters.ProcessLastNameTableAdapter();
                APWorkFlow.StuffUserAndPositionDataTable dtStuffUserAndPosition = TAUserAndPosition.GetDataByPositionId(positionID);
                if (dtStuffUserAndPosition.Rows.Count == 0) {
                    return false;
                }
                string lastName = "";
                WFBaseProcessInstance pi = api.GetProcInst(processInstanceID);
                WFManualWorkItem wi = getNextNode(processInstanceID);
                WFBaseActivityInstance ai = api.GetActivityInst(wi.ActivityInstID);
                APWorkFlow.ProcessLastNameDataTable dtProcessLastName = TAProcessLastName.GetDataByTName(pi.DefName);
                if (dtProcessLastName.Rows.Count > 0) {
                    lastName = dtProcessLastName[0].lastName;
                    if (ai.DisplayName == lastName) {
                        WFTimeDuration td = new WFTimeDuration();
                        td.BusinessTime = false;
                        td.Length = "7";
                        td.Unit = WFTimeUnit.DAY;
                        foreach (APWorkFlow.StuffUserAndPositionRow drStuffUserAndPosition in dtStuffUserAndPosition.Rows) {
                            //获取审批人
                            approve += "P" + drStuffUserAndPosition.StuffUserId + "P";
                            //APWorkFlow.StuffUserDataTable dtUser=TAStuffUser.GetDataByID(drStuffUserAndPosition.StuffUserId);  
                        }
                        WFEvent evt = api.CreateLinkedWorkItem(wi.WorkItemID, wi.Name, approve, td, wi.ClientData);
                        evt = getFullEvent(evt);
                        APWorkFlow.NodeStatusDataTable dt = new APWorkFlow.NodeStatusDataTable();
                        APWorkFlow.NodeStatusRow dr = dt.NewNodeStatusRow();

                        dr.STATUS = FlowNodeStatus.ASSIGNED;
                        dr.PARTICIPANT = approve;
                        //dr.AcceptChanges();
                        dt.AddNodeStatusRow(dr);
                        dt.AcceptChanges();
                        StringWriter sw = new StringWriter();
                        dt.WriteXml(sw);
                        string clientData = sw.ToString();

                        System.Collections.ArrayList attrList = new System.Collections.ArrayList();
                        attrList.Add(new NameValue("UserID", dr.PARTICIPANT));//更新该步骤的userid
                        attrList.Add(new NameValue("CLIENT_DATA", clientData));
                        NameValue[] attributes = (NameValue[])attrList.ToArray(typeof(NameValue));
                        api.UpdateWorkItem(evt.WorkItemID, attributes);
                        api.GetWorkItem(evt.WorkItemID).DisplayName = "总经理审批";
                        if (string.IsNullOrEmpty(evt.Error))
                            return true;
                        return false;
                    }
                }
                return false;
            } catch {
                return false;
            }
        }

        //public int GetApproveTimeByUser(string UserId, DateTime stratDate, DateTime endDate)
        //{
        //    try
        //    {
        //        string sql = string.Format("WF_MANUAL_WORKITEMS.LAST_MODIFIED_BY like '%{0}%' and WF_MANUAL_WORKITEMS.LastModifiedDate between '{1}' and  '{2}' and WF_MANUAL_WORKITEMS.STATUS ='{3}' ", UserId, stratDate, endDate, FlowNodeStatus.Completed);
        //        WFManualWorkItem[] WorkItems = api.QueryWorkListEx(sql);
        //        //api.GetProcInst(WorkItems[0].ProcInstID).DefName//流程图名称

        //        return WorkItems.Length;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

        public WFManualWorkItem[] GetAllWorkItem(String defname) {
            string sql = string.Format("WF_MANUAL_WORKITEMS.STATUS='{0}' and  [WF_PROC_INSTS].DEF_NAME=N'{1}'", WFManualWorkItem.ASSIGNED, defname);
            WFManualWorkItem[] WorkItems = api.QueryWorkListEx(sql);
            return WorkItems;
        }

        public WFManualWorkItem approvetest(WFManualWorkItem WorkItem, int OrganizationUnitID) {
            string participant = "";
            api.CompleteWorkItem(WorkItem.WorkItemID);
            WorkItem = getNextNode(WorkItem.ProcInstID);
            if (WorkItem != null) {
                string[] ids = WorkItem.OriginalUserID.Split('\\')[1].ToString().Split("|".ToCharArray());
                for (int i = 0; i < ids.Length; i++) {
                    string uid = string.Empty;
                    string PositionTypeCode = ids[i].ToString();
                    string email = "";

                    getUserIDByIdentifier(PositionTypeCode, OrganizationUnitID, ref uid, ref email);
                    if (uid == "none") {
                        participant = "P27P&";
                    } else {
                        participant += uid + "&";
                    }
                }
                participant = participant.TrimEnd('&');
                System.Collections.ArrayList attrList = new System.Collections.ArrayList();
                attrList.Add(new NameValue("UserID", participant));//更新该步骤的userid
                NameValue[] attributes = (NameValue[])attrList.ToArray(typeof(NameValue));
                api.UpdateWorkItem(WorkItem.WorkItemID, attributes);
                WorkItem = getNextNode(WorkItem.ProcInstID);
            }
            return WorkItem;
        }

        public string startProcessTest(string processInstanceID, int OrganizationUnitID, ref string email) {
            try {
                if (String.IsNullOrEmpty(processInstanceID)) {
                    throw new Exception("startProcessTest - processInstanceID is empty");
                }
                WFEvent evt = api.StartProcInst(processInstanceID);
                evt = getFullEvent(evt);
                if (!String.IsNullOrEmpty(evt.Error)) {
                    throw new Exception("startProcessTest - " + evt.Error);
                } else {
                    System.Collections.ArrayList attrList = new System.Collections.ArrayList();
                    attrList.Add(new NameValue("ProcInstName", processInstanceID + OrganizationUnitID.ToString()));
                    NameValue[] attributes = (NameValue[])attrList.ToArray(typeof(NameValue));
                    api.UpdateProcInst(processInstanceID, attributes);
                }
                return prepareNextNodeTest(processInstanceID, "PROCESS START", OrganizationUnitID, ref email);
                //return "111";
            } catch (Exception e) {
                try {
                    api.CancelProcInst(processInstanceID);
                } catch (Exception ex) {
                }
                if (e is ApplicationException)
                    throw e;
                throw new Exception(errorMSGPrefix + e.Message, e);
            }
        }

        private string prepareNextNodeTest(string processInstanceID, string lastUserID, int OrganizationUnitID, ref string email) {
            WFManualWorkItem nextNode = getNextNode(processInstanceID);
            if (nextNode == null) {
                return null; //process finished
            } else {
                string participant = "";
                string participantEmail = "";
                nextNode.UserID = nextNode.UserID.Split('\\')[1].ToString();

                //translate participant(防止多个流程角色)
                string[] ids = nextNode.OriginalUserID.Split('\\')[1].ToString().Split("|".ToCharArray());
                for (int i = 0; i < ids.Length; i++) {
                    string uid = string.Empty;
                    string PositionTypeCode = ids[i].ToString();
                    getUserIDByIdentifier(PositionTypeCode, OrganizationUnitID, ref uid, ref email);

                    //当前角色下找不到人
                    if (uid == "none") {
                        participant = "P27P&";
                    } else {
                        participant += uid + "&";
                        participantEmail += email;
                    }
                }
                participant = participant.TrimEnd('&');

                // update workitem
                APWorkFlow.NodeStatusDataTable dt = new APWorkFlow.NodeStatusDataTable();
                APWorkFlow.NodeStatusRow dr = dt.NewNodeStatusRow();
                if (!String.IsNullOrEmpty(participant) && participant != "none") { //user found
                    dr.STATUS = FlowNodeStatus.ASSIGNED;
                    dr.PARTICIPANT = participant;
                    //dr.AcceptChanges();
                    dt.AddNodeStatusRow(dr);
                    dt.AcceptChanges();
                    StringWriter sw = new StringWriter();
                    dt.WriteXml(sw);
                    string clientData = sw.ToString();

                    System.Collections.ArrayList attrList = new System.Collections.ArrayList();
                    attrList.Add(new NameValue("UserID", dr.PARTICIPANT));//更新该步骤的userid
                    attrList.Add(new NameValue("CLIENT_DATA", clientData));
                    NameValue[] attributes = (NameValue[])attrList.ToArray(typeof(NameValue));
                    api.UpdateWorkItem(nextNode.WorkItemID, attributes);
                    //update email
                    if (!String.IsNullOrEmpty(participantEmail)) {
                        email = participantEmail;
                    }
                    return dr.PARTICIPANT;
                } else { // 没有找到人员
                    dr.STATUS = FlowNodeStatus.ONERROR;
                    dr.PARTICIPANT = "P" + lastUserID + "P";
                    dr.ERROR_MSG = "错误代码：Invalid UserId (" + nextNode.ProcInstName + "," + nextNode.OriginalUserID + "," + nextNode.UserID + ")";
                    //dr.AcceptChanges();
                    dt.AddNodeStatusRow(dr);
                    dt.AcceptChanges();
                    StringWriter sw = new StringWriter();
                    dt.WriteXml(sw);
                    string clientData = sw.ToString();

                    System.Collections.ArrayList attrList = new System.Collections.ArrayList();
                    attrList.Add(new NameValue("UserID", dr.PARTICIPANT));
                    attrList.Add(new NameValue("CLIENT_DATA", clientData));
                    NameValue[] attributes = (NameValue[])attrList.ToArray(typeof(NameValue));
                    api.UpdateWorkItem(nextNode.WorkItemID, attributes);

                    // sendMail(getUserEmailByID(int.Parse(lastUserID)), "", "您提交的单据发生错误", "单据编号为：" + nextNode.ProcInstName);

                    throw new ApplicationException(dr.ERROR_MSG);
                }
            }
        }

        public WFBaseProcessInstance getprocessinstance(string ProcInstID) {
            return api.GetProcInst(ProcInstID);
        }

        //根据流程ID获取审批历史 审批人（节点名称）
        public string GetApproverNamesByProcID(string ProcID) {
            string ApproverNames = string.Empty;
            APWorkFlow.NodeStatusDataTable nodeTable = getApprovalStatus(ProcID);
            foreach (APWorkFlow.NodeStatusRow item in nodeTable) {
                if (!item.IsAPPROVED_BYNull()) {
                    ApproverNames += item.APPROVED_BY + "(" + item.COMPLETED_DATE + ") > ";
                }
            }
            return ApproverNames;
        }
    }
}