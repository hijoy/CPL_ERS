using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using lib.wf;
using BusinessObjects;

partial class UserControls_APFlowNodes : System.Web.UI.UserControl {

    #region " Const "

    private const int strExchangeNum = 15;
    private const string DateFormatStr = "yyyy-MM-dd HH:mm";
    private const string strArrowHead = "<img style='width:50px' src=" + "" + "../images/ArrowHead1.png" + "" + ">";
    /*
    private const string submittedStatus = "提交审批";
    private const string waitingStatus = "等待审批";
    private const string rejectedStatus = "退回";
    private const string approvedStatus = "审批通过";
    private const string errorStatus = "发生错误";

    private const string strDraft = "申请";
    private const string strApproved = "已审批";
    private const string strApproving = "待审批";
    private const string strReturnModify = "退回";
    private const string strError = "错误";
    */

    private string submittedStatus = "";
    private string waitingStatus = "";
    private string rejectedStatus = "";
    private string approvedStatus = "";
    private string errorStatus = "";

    private string strDraft = "";
    private string strApproved = "";
    private string strApproving = "";
    private string strReturnModify = "";
    private string strError = "";
    
    #endregion

    #region " Private variables "

    private UpdatePanel cwfUpdatePanel = new UpdatePanel();
    private Table cwfTable = new Table();

    private APHelper AP = new APHelper();

    private RadioButton radApprove = new RadioButton();

    private RadioButton radReject = new RadioButton();

    private DropDownList ddlReject = new DropDownList();

    private TextBox txtComments = new TextBox();

    private Label lblErrorMsg = new Label();

    private Label lblRejectReason = new Label();

    private bool isLastIndex = false;

    #endregion

    #region " Public Properties "

    private string m_ProcID;
    public string ProcID {
        get {
            return m_ProcID;
        }
        set {
            m_ProcID = value;
        }
    }

    /// <summary> 
    /// 单据ID
    /// </summary> 
    /// <remarks></remarks> 
    private int m_FormID;
    public int FormID {
        get {
            return m_FormID;
        }
        set {
            m_FormID = value;
        }
    }

    /// <summary> 
    /// 是否可编辑 
    /// </summary> 
    /// <remarks></remarks> 
    private bool _isView = false;
    public bool IsView {
        get {
            return _isView;
        }
        set {
            _isView = value;
        }
    }


    /// <summary> 
    /// 审批顺序 
    /// </summary> 
    /// <remarks></remarks> 
    private int _priority = 0;
    public int Priority {
        get {
            return _priority;
        }
        set {
            _priority = value;
        }
    }

    //private string _formFlowTitle = "审批流程：";
    private string _formFlowTitle = "";
    /// <summary>
    /// 流程Title
    /// </summary>
    public string FormFlowTitle {
        get {
            return _formFlowTitle;
        }
        set {
            _formFlowTitle = value;
        }
    }

    public SystemEnums.FlowNodeStatus StatusValue {
        get {
            if (this.radApprove.Checked) {
                return SystemEnums.FlowNodeStatus.Pass;
            } else {
                return SystemEnums.FlowNodeStatus.Reject;
            }
        }
    }

    public string CommentsValue {
        get {
            return this.GetComments();
        }
    }

    private bool _allowSubmit = false;
    public bool AllowSubmit {
        get {
            return this._allowSubmit;
        }
        set {
            _allowSubmit = value;
        }
    }

    private bool _isAllowInput = false;
    public bool IsAllowInput {
        get {
            return this._isAllowInput;
        }
    }


    #endregion

    #region " Overrides Methods "

    /// <summary> 
    /// 
    /// </summary> 
    /// <param name="e"></param> 
    /// <remarks></remarks> 
    protected override void OnLoad(System.EventArgs e) {
        submittedStatus = GetLocalResourceObject("String_SubmitApproval").ToString();
        waitingStatus = GetLocalResourceObject("String_WaitingStatus").ToString();
        rejectedStatus = GetLocalResourceObject("String_Return").ToString();
        approvedStatus = GetLocalResourceObject("String_Approved").ToString();
        errorStatus = GetLocalResourceObject("String_ErrorStatus").ToString();

        strDraft = GetLocalResourceObject("String_Apply").ToString();
        strApproved = GetLocalResourceObject("String_Approved1").ToString();
        strApproving = GetLocalResourceObject("String_Approving").ToString();
        strReturnModify = GetLocalResourceObject("String_Return").ToString();
        strError = GetLocalResourceObject("String_Error").ToString();
        _formFlowTitle = GetLocalResourceObject("String_ApprovalFlow").ToString();
        initCtrl();
        base.OnLoad(e);
    }

    #endregion

    #region " Private Methods "

    private void initCtrl() {
        if (string.IsNullOrEmpty(this.ProcID)) {
            Label lblNull = new Label();
            lblNull.ID = "lblNull";
            //lblNull.Text = "该单据为自动审批通过！";
            lblNull.Text = GetLocalResourceObject("AutomaticApproval").ToString();
            lblNull.ForeColor = System.Drawing.Color.Red;
            this.Controls.Add(lblNull);
        } else {
            Panel pan = new Panel();
            Label lbltitle = new Label();
            lbltitle.ID = "lblTitle";
            lbltitle.Text = FormFlowTitle;
            pan.Controls.Add(lbltitle);
            pan.Attributes.Add("class", "title");
            pan.Width = 1258;
            this.Controls.Add(pan);
           
            cwfUpdatePanel.ID = "cwfUpdatePanel";
            cwfUpdatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
            cwfUpdatePanel.ContentTemplateContainer.Controls.Add(cwfTable);
          
            cwfTable.ID = "cwfTable";
            cwfTable.Attributes["runat"] = "server";
            cwfTable.Attributes.Add("cellpadding", "10px");
            this.Controls.Add(cwfUpdatePanel);

            createCheckWorkFlow();
        }
    }
    /// <summary> 
    /// 创建审批流程图 
    /// </summary> 
    /// <remarks></remarks> 
    private void createCheckWorkFlow() {
        DataSet ds = GetCheckWorkFlowData();
        if (ds.Tables.Count == 0) {
            return;
        }
        int cwfRecorder = 0;
        cwfRecorder = ds.Tables[0].Rows.Count;

        if (cwfRecorder > 0) {
            isLastIndex = false;

            int cwfRowCount = 0;
            cwfRowCount = cwfRecorder / 4;

            int modCount = 0;
            modCount = cwfRecorder % 4;

            int rowIndex = 0;

            for (int rowCount = 0; rowCount <= cwfRowCount - 1; rowCount++) {
                TableRow tr = new TableRow();

                for (int i = 0; i < 4; i++) {
                    tr.Cells.Add(createCheckWorkFlowTableCell(ds.Tables[0].Rows[rowIndex]));

                    rowIndex += 1;
                    if (rowIndex != cwfRecorder) {
                        tr.Cells.Add(createArrowHeadTableCell());
                    }
                }
                cwfTable.Rows.Add(tr);
            }

            if (modCount > 0) {
                TableRow tr = new TableRow();
                int cIndex = rowIndex;
                for (int n = 0; n < cwfRecorder - cIndex; n++) {
                    tr.Cells.Add(createCheckWorkFlowTableCell(ds.Tables[0].Rows[rowIndex]));

                    rowIndex += 1;
                    if (rowIndex != cwfRecorder) {
                        tr.Cells.Add(createArrowHeadTableCell());
                    }
                }
                cwfTable.Rows.Add(tr);
            }
        }
    }

    private TableCell createCheckWorkFlowTableCell(DataRow dr) {
        string strText = string.Empty;
        if (this.IsView) {
            strText = DBNullConverter.ToStr(dr["RegionCaption"])
                //+ "</br>" + "审批人:" + DBNullConverter.ToStr(dr["Approver"])
                //+ "</br>" + "单据状态:" + DBNullConverter.ToStr(dr["FlowStatus"]) + setHighLight(dr["IsOutTime"].ToString())
                //+ "</br>" + "日期:" + DBNullConverter.ToStr(dr["AuditDate"])
                //+ "</br>" + "审批意见:"
                + "</br>" + GetLocalResourceObject("Approver").ToString() + DBNullConverter.ToStr(dr["Approver"])
                + "</br>" + GetLocalResourceObject("FormStatus").ToString() + DBNullConverter.ToStr(dr["FlowStatus"]) + setHighLight(dr["IsOutTime"].ToString())
                + "</br>" + GetLocalResourceObject("Date").ToString() + DBNullConverter.ToStr(dr["AuditDate"])
                + "</br>" + GetLocalResourceObject("ApprovalComments").ToString()
                + "</br>" + exchangeString(DBNullConverter.ToStr(dr["Comment"]));
            if (DBNullConverter.ToInteger(dr["isPending"]) == 0) {
                return createViewTabelCell(dr);
            } else {
                return createIsPendingViewTableCell(dr);
            }
        } else {
            if (DBNullConverter.ToInteger(dr["isView"]) == 0) {
                strText = DBNullConverter.ToStr(dr["RegionCaption"]) +
                    //"</br>" + "审批人:" + DBNullConverter.ToStr(dr["Approver"]) +
                    //"</br>" + "单据状态:" + DBNullConverter.ToStr(dr["FlowStatus"]) + setHighLight(dr["IsOutTime"].ToString()) +
                    //"</br>" + "日期:" + DBNullConverter.ToStr(dr["AuditDate"]) +
                    //"</br>" + "审批意见:" + "</br>" + exchangeString(DBNullConverter.ToStr(dr["Comment"]));
                    "</br>" + GetLocalResourceObject("Approver").ToString() + DBNullConverter.ToStr(dr["Approver"]) +
                    "</br>" + GetLocalResourceObject("FormStatus").ToString() + DBNullConverter.ToStr(dr["FlowStatus"]) + setHighLight(dr["IsOutTime"].ToString()) +
                    "</br>" + GetLocalResourceObject("Date").ToString() + DBNullConverter.ToStr(dr["AuditDate"]) +
                    "</br>" + GetLocalResourceObject("ApprovalComments").ToString() + "</br>" + exchangeString(DBNullConverter.ToStr(dr["Comment"]));
                if (DBNullConverter.ToInteger(dr["isPending"]) == 0) {
                    return createViewTabelCell(dr);
                } else {
                    return createIsPendingViewTableCell(dr);
                }
            } else {
                this.AllowSubmit = true;
                if (isLastIndex) {
                    _isAllowInput = true;
                }
                return createEditTableCell(dr);
            }
        }
    }

    private TableCell createViewTabelCell(DataRow dr) {
        TableCell tcTemp = new TableCell();
        if ((dr["RegionCaption"].ToString() != "已完成")) {
            tcTemp.Attributes.Add("class", "FlowBg_old");//此处为审批通过的节点
            Label lblRegionCaption = new Label();
            lblRegionCaption.Text = DBNullConverter.ToStr(dr["RegionCaption"]);
            lblRegionCaption.Attributes.Add("class", "TxtB");
            tcTemp.Controls.Add(lblRegionCaption);
            tcTemp.Controls.Add(new LiteralControl("<br><br>"));

            Label lblApproverEx = new Label();
            //lblApproverEx.Text = "审批人:";
            lblApproverEx.Text = GetLocalResourceObject("Approver").ToString();
            if ((dr["RegionCaption"].ToString() == strDraft)) {
                //lblApproverEx.Text = "申请人:";
                lblApproverEx.Text = GetLocalResourceObject("Applicant").ToString();
            }
            lblApproverEx.Attributes.Add("class", "TxtB");
            tcTemp.Controls.Add(lblApproverEx);

            Label lblApprover = new Label();
            lblApprover.Text = DBNullConverter.ToStr(dr["Approver"]);
            tcTemp.Controls.Add(lblApprover);
            tcTemp.Controls.Add(new LiteralControl("<br><br>"));

            Label lblStatusEx = new Label();
            //lblStatusEx.Text = "单据状态:";
            lblStatusEx.Text = GetLocalResourceObject("FormStatus").ToString();
            lblStatusEx.Attributes.Add("class", "TxtB");
            tcTemp.Controls.Add(lblStatusEx);

            Label lblStatus = new Label();
            lblStatus.Text = DBNullConverter.ToStr(dr["FlowStatus"]) + setHighLight(dr["IsOutTime"].ToString());
            tcTemp.Controls.Add(lblStatus);
            tcTemp.Controls.Add(new LiteralControl("<br><br>"));

            Label lblApproveDateEx = new Label();
            //lblApproveDateEx.Text = "日期:";
            lblApproveDateEx.Text = GetLocalResourceObject("Date").ToString();
            lblApproveDateEx.Attributes.Add("class", "TxtB");
            tcTemp.Controls.Add(lblApproveDateEx);

            Label lblApproveDate = new Label();
            lblApproveDate.Text = DBNullConverter.ToStr(dr["AuditDate"]);
            tcTemp.Controls.Add(lblApproveDate);
            tcTemp.Controls.Add(new LiteralControl("<br><br>"));

            Label lblCommentsEx = new Label();
            //lblCommentsEx.Text = "审批意见:";
            lblCommentsEx.Text = GetLocalResourceObject("ApprovalComments").ToString();
            lblCommentsEx.Attributes.Add("class", "TxtB");
            tcTemp.Controls.Add(lblCommentsEx);
            tcTemp.Controls.Add(new LiteralControl("<br><br>"));

            TextBox txtCommentsEx = new TextBox();
            txtCommentsEx.Width = 200;
            txtCommentsEx.Height = 50;
            txtCommentsEx.TextMode = TextBoxMode.MultiLine;
            txtCommentsEx.Rows = 5;
            txtCommentsEx.Columns = 20;
            txtCommentsEx.Text = DBNullConverter.ToStr(dr["Comment"]);
            txtCommentsEx.ReadOnly = true;
            txtCommentsEx.Attributes.Add("class", "FlowCom_checked");
            tcTemp.Controls.Add(txtCommentsEx);
        } else {
            tcTemp.Attributes.Add("class", "FlowBg_old");
            tcTemp.Attributes.Add("style", "vertical-align:top");

            Label lblRegionCaption = new Label();
            lblRegionCaption.Text = DBNullConverter.ToStr(dr["RegionCaption"]);
            lblRegionCaption.Attributes.Add("class", "TxtB");
            tcTemp.Controls.Add(lblRegionCaption);
            tcTemp.Controls.Add(new LiteralControl("<br><br>"));
        }
        return tcTemp;
    }

    private TableCell createIsPendingViewTableCell(string strText) {

        TableCell tcTemp = new TableCell();
        tcTemp.Attributes.Add("class", "FlowBg_Pending");
        tcTemp.Text = strText;
        return tcTemp;
    }

    private TableCell createIsPendingViewTableCell(DataRow dr) {
        TableCell tcTemp = new TableCell();
        tcTemp.Attributes.Add("class", "FlowBg_Pending");

        Label lblRegionCaption = new Label();
        lblRegionCaption.Text = DBNullConverter.ToStr(dr["RegionCaption"]);
        lblRegionCaption.Attributes.Add("class", "TxtB");
        tcTemp.Controls.Add(lblRegionCaption);
        tcTemp.Controls.Add(new LiteralControl("<br><br>"));

        Label lblApproverEx = new Label();
        if (DBNullConverter.ToInt32(dr["OnError"]) == 1)
            //lblApproverEx.Text = "操作人:";
            lblApproverEx.Text = GetLocalResourceObject("Operator").ToString();
        else
            //lblApproverEx.Text = "审批人:";
            lblApproverEx.Text = GetLocalResourceObject("Approver").ToString();
        lblApproverEx.Attributes.Add("class", "TxtB");
        tcTemp.Controls.Add(lblApproverEx);

        Label lblApprover = new Label();
        lblApprover.Text = DBNullConverter.ToStr(dr["Approver"]);
        tcTemp.Controls.Add(lblApprover);
        tcTemp.Controls.Add(new LiteralControl("<br><br>"));

        Label lblStatusEx = new Label();
        //lblStatusEx.Text = "单据状态:";
        lblStatusEx.Text = GetLocalResourceObject("FormStatus").ToString();
        lblStatusEx.Attributes.Add("class", "TxtB");
        tcTemp.Controls.Add(lblStatusEx);

        Label lblStatus = new Label();
        lblStatus.Text = DBNullConverter.ToStr(dr["FlowStatus"]) + setHighLight(dr["IsOutTime"].ToString());
        tcTemp.Controls.Add(lblStatus);
        tcTemp.Controls.Add(new LiteralControl("<br><br>"));

        Label lblApproveDateEx = new Label();
        //lblApproveDateEx.Text = "日期:";
        lblApproveDateEx.Text = GetLocalResourceObject("Date").ToString();
        lblApproveDateEx.Attributes.Add("class", "TxtB");
        tcTemp.Controls.Add(lblApproveDateEx);

        Label lblApproveDate = new Label();
        lblApproveDate.Text = DBNullConverter.ToStr(dr["AuditDate"]);
        tcTemp.Controls.Add(lblApproveDate);
        tcTemp.Controls.Add(new LiteralControl("<br><br>"));

        Label lblCommentsEx = new Label();
        //lblCommentsEx.Text = "审批意见:";
        lblCommentsEx.Text = GetLocalResourceObject("ApprovalComments").ToString();
        lblCommentsEx.Attributes.Add("class", "TxtB");
        tcTemp.Controls.Add(lblCommentsEx);
        tcTemp.Controls.Add(new LiteralControl("<br><br>"));

        TextBox txtCommentsEx = new TextBox();
        txtCommentsEx.Width = 200;
        txtCommentsEx.Height = 50;
        txtCommentsEx.TextMode = TextBoxMode.MultiLine;
        txtCommentsEx.Rows = 5;
        txtCommentsEx.Columns = 20;
        txtCommentsEx.Text = DBNullConverter.ToStr(dr["Comment"]);
        txtCommentsEx.ReadOnly = true;
        txtCommentsEx.Attributes.Add("class", "FlowCom_Pending");
        tcTemp.Controls.Add(txtCommentsEx);

        return tcTemp;
    }

    private TableCell createEditTableCell(DataRow dr) {
        TableCell tcTemp = new TableCell();
        tcTemp.Attributes.Add("class", "FlowBg_now");//此处添加的是当前步骤的cell

        Label lblRegionCaption = new Label();
        lblRegionCaption.Text = DBNullConverter.ToStr(dr["RegionCaption"]);
        lblRegionCaption.ID = "lblRegionCaption";
        lblRegionCaption.Attributes.Add("class", "TxtB");
        tcTemp.Controls.Add(lblRegionCaption);
        tcTemp.Controls.Add(new LiteralControl("<br><br>"));

        Label lblApproverEx = new Label();
        if (DBNullConverter.ToInt32(dr["OnError"]) == 1)
            //lblApproverEx.Text = "操作人:";
            lblApproverEx.Text = GetLocalResourceObject("Operator").ToString();
        else
            //lblApproverEx.Text = "审批人:";
            lblApproverEx.Text = GetLocalResourceObject("Approver").ToString();
        lblApproverEx.Attributes.Add("class", "TxtB");
        tcTemp.Controls.Add(lblApproverEx);

        Label lblApprover = new Label();
        lblApprover.ID = "lblApprover";
        lblApprover.Text = DBNullConverter.ToStr(dr["Approver"]);
        tcTemp.Controls.Add(lblApprover);
        tcTemp.Controls.Add(new LiteralControl("<br><br>"));

        Label lblrequiredLable = new Label();
        lblrequiredLable.Text = "*";
        lblrequiredLable.ForeColor = System.Drawing.Color.Red;
        tcTemp.Controls.Add(lblrequiredLable);
        Label lblStatus = new Label();
        lblStatus.ID = "lblStatus";
        //lblStatus.Text = "单据状态:" + setHighLight(dr["IsOutTime"].ToString());
        lblStatus.Text = GetLocalResourceObject("FormStatus").ToString() + setHighLight(dr["IsOutTime"].ToString());
        lblStatus.Attributes.Add("class", "TxtB");
        tcTemp.Controls.Add(lblStatus);
        tcTemp.Controls.Add(new LiteralControl("<br><br>"));

        radApprove.ID = "radApprove";
        if (DBNullConverter.ToInt32(dr["OnError"]) == 1)
            //radApprove.Text = "尝试恢复";
            radApprove.Text = GetLocalResourceObject("TryToRecover").ToString();
        else {
            //radApprove.Text = "同意";
            radApprove.Text = GetLocalResourceObject("Agree").ToString();
            radApprove.CheckedChanged += new EventHandler(radApprove_CheckedChanged);
        }
        radApprove.GroupName = "gnStatus";
        radApprove.Checked = true;
        radApprove.AutoPostBack = true;
        tcTemp.Controls.Add(radApprove);
        tcTemp.Controls.Add(new LiteralControl("<br><br>"));

        radReject.ID = "radReject";
        if (DBNullConverter.ToInt32(dr["OnError"]) == 1)
            //radReject.Text = "退回单据";
            radReject.Text = GetLocalResourceObject("RejectForm").ToString();
        else {
            //radReject.Text = "拒绝";
            radReject.Text = GetLocalResourceObject("Reject").ToString();
            radReject.CheckedChanged += new EventHandler(radApprove_CheckedChanged);
        }
        radReject.GroupName = "gnStatus";
        radReject.AutoPostBack = true;
        tcTemp.Controls.Add(radReject);
        tcTemp.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

        ddlReject.ID = "ddlReject";
        ddlReject.Width = new Unit("142px");
        ddlReject.Visible = false;
        ddlReject.DataValueField = "RejectReasonId";
        ddlReject.DataTextField = "RejectReasonTitle";
        ddlReject.AutoPostBack = true;

        if (DBNullConverter.ToInt32(dr["OnError"]) == 1) {
            tcTemp.Controls.Add(new LiteralControl("<br><br>"));
        } else {
            MasterDataBLL bll = new MasterDataBLL();
            ddlReject.DataSource = bll.GetRejectReason();
            ddlReject.DataBind();
            ddlReject.Items.Insert(0, new ListItem("---Please select---", ""));
            ddlReject.SelectedIndexChanged += new EventHandler(ddlReject_SelectedIndexChanged);
            tcTemp.Controls.Add(ddlReject);
            tcTemp.Controls.Add(new LiteralControl("<br><br>"));

            Label lblComments = new Label();
            //lblComments.Text = "审批意见:";
            lblComments.Text = GetLocalResourceObject("ApprovalComments").ToString();
            lblComments.ID = "lblComments";
            lblComments.Attributes.Add("class", "TxtB");
            tcTemp.Controls.Add(lblComments);
            tcTemp.Controls.Add(new LiteralControl("<br><br>"));
        }

        txtComments.ID = "txtComments";
        txtComments.Width = 200;
        txtComments.Height = 50;
        txtComments.TextMode = TextBoxMode.MultiLine;
        txtComments.Rows = 5;
        txtComments.Columns = 20;
        if (DBNullConverter.ToInt32(dr["OnError"]) == 1) {
            txtComments.Text = DBNullConverter.ToStr(dr["Comment"]);
            txtComments.ReadOnly = true;
        } else {
            txtComments.Attributes.Add("onchange", "javascript:return textLimit(this,100);");
        }
        tcTemp.Controls.Add(txtComments);
        return tcTemp;
    }

    protected void ddlReject_SelectedIndexChanged(object sender, EventArgs e) {
        SetComments();
        cwfUpdatePanel.Update();
    }

    protected void radApprove_CheckedChanged(object sender, EventArgs e) {
        if (radApprove.Checked) {
            ddlReject.Visible = false;
            txtComments.Text = string.Empty;
        } else {
            ddlReject.Visible = true;
            SetComments();
        }
        cwfUpdatePanel.Update();
    }

    private void SetComments() {
        if (this.ddlReject.SelectedValue != string.Empty) {
            int RejectReasonId = int.Parse(this.ddlReject.SelectedValue);

            MasterDataBLL bll = new MasterDataBLL();
            MasterData.RejectReasonDataTable rejectReasonDT = bll.GetRejectReasonById(RejectReasonId);
            if (rejectReasonDT != null && rejectReasonDT.Rows.Count > 0) {
                MasterData.RejectReasonRow rejectReasonDr = rejectReasonDT[0];
                txtComments.Text = rejectReasonDr.RejectReasonContent;
            } else {
                txtComments.Text = string.Empty;
            }
        }
    }

    /// <summary> 
    /// 创建箭头 
    /// </summary> 
    /// <returns></returns> 
    /// <remarks></remarks> 
    private TableCell createArrowHeadTableCell() {
        TableCell tcTemp = new TableCell();
        tcTemp.Text = strArrowHead;
        tcTemp.Attributes.Add("style", "width:50px");
        return tcTemp;
    }

    //好像没用到
    private TableCell createPyViewTabelCell(string strText) {
        TableCell tcTemp = new TableCell();
        tcTemp.Attributes.Add("class", "FlowBg_checked");
        tcTemp.Text = strText;
        return tcTemp;
    }

    //好像没用到
    private TableCell createPyIsPendingViewTabelCell(string strText) {
        TableCell tcTemp = new TableCell();
        tcTemp.Attributes.Add("class", "FlowBg_Pending");
        tcTemp.Text = strText;

        return tcTemp;
    }

    /// <summary> 
    /// 转换字符串长度 
    /// </summary> 
    /// <param name="str"></param> 
    /// <returns></returns> 
    /// <remarks></remarks> 
    private string exchangeString(string str) {

        if (str.Length > strExchangeNum) {
            int strCount = 0;
            strCount = str.Length / strExchangeNum;
            int strMod = 0;
            strMod = str.Length % strExchangeNum;
            string exchangeStr = string.Empty;

            if (strMod == 0) {
                for (int i = 0; i <= strCount - 1; i++) {
                    if (i != strCount - 1) {
                        exchangeStr = exchangeStr + str.Substring(i * strExchangeNum, strExchangeNum) + "</br>";
                    } else {
                        exchangeStr = exchangeStr + str.Substring(i * strExchangeNum, strExchangeNum);
                    }
                }
            } else {
                for (int i = 0; i <= strCount - 1; i++) {
                    exchangeStr = exchangeStr + str.Substring(i * strExchangeNum, strExchangeNum) + "</br>";
                }
                exchangeStr = exchangeStr + str.Substring(strCount * strExchangeNum, strMod);
            }
            return exchangeStr;
        } else {
            return str;
        }
    }

    private string GetApproveName(int stuffuserID, int positionID, bool isApprove) {
        AuthorizationBLL authorizationBLL = new AuthorizationBLL();
        AuthorizationDS.StuffUserRow stuffuserDr = authorizationBLL.GetStuffUserById(stuffuserID);
        string stuffUserName = stuffuserDr.StuffName;
        AuthorizationDS.PositionRow PositionRow = new OUTreeBLL().GetPositionById(positionID);
        string positionName = PositionRow.PositionName;
        if (isApprove) {
            return positionName + "[" + stuffUserName + "]";
        } else {
            return stuffUserName;
        }
    }

    /// <summary> 
    /// 
    /// </summary> 
    /// <returns></returns> 
    /// <remarks></remarks> 
    protected DataSet GetCheckWorkFlowData() {
        DataSet ds = new DataSet();
        DataTable dt;

        //创建数据表 
        dt = new DataTable();
        dt.Columns.Add(new DataColumn("RegionCaption", typeof(string)));
        dt.Columns.Add(new DataColumn("Approver", typeof(string)));
        dt.Columns.Add(new DataColumn("FlowStatus", typeof(string)));
        dt.Columns.Add(new DataColumn("AuditDate", typeof(string)));
        dt.Columns.Add(new DataColumn("Comment", typeof(string)));
        dt.Columns.Add(new DataColumn("IsView", typeof(int)));
        dt.Columns.Add(new DataColumn("IsPending", typeof(int)));
        dt.Columns.Add(new DataColumn("OnError", typeof(int)));
        dt.Columns.Add(new DataColumn("RejectReason", typeof(string)));
        dt.Columns.Add(new DataColumn("IsOutTime", typeof(int)));//是否超时

        //HU
        lib.APWorkFlow.NodeStatusDataTable APTable = AP.getApprovalStatus(this.ProcID);
        if (APTable == null) {
            return ds;
        }
        AuthorizationBLL authorizationBLL = new AuthorizationBLL();
        QueryDS.FormViewRow formRow = new FormQueryBLL().GetFormViewByID(this.FormID);
        AuthorizationDS.StuffUserRow stuffuserDr = authorizationBLL.GetStuffUserById(formRow.UserID);

        //创建节点，先创建申请节点
        DataRow dr;
        dr = dt.NewRow();
        dr["RegionCaption"] = strDraft;
        dr["Approver"] = stuffuserDr.StuffName;
        dr["AuditDate"] = formRow.SubmitDate.ToString("yyyy-MM-dd HH:mm");
        dr["FlowStatus"] = submittedStatus;

        dr["Comment"] = string.Empty;
        dr["IsView"] = 0;
        dr["IsPending"] = 0;
        dr["OnError"] = 0;
        dr["IsOutTime"] = 0;
        dt.Rows.Add(dr);
        //再创建审批节点
        foreach (lib.APWorkFlow.NodeStatusRow drwfh in APTable.Rows) {
            switch (DBNullConverter.ToStr(drwfh.STATUS)) {
                case APHelper.FlowNodeStatus.ONERROR:
                    if (this.IsView) {
                        generaAddErrorPendingRowToDt(ref dt, drwfh, strError);
                    } else {
                        generaAddErrorInTurnRowToDt(ref dt, drwfh, strError, errorStatus);
                    }
                    break;
                case APHelper.FlowNodeStatus.ASSIGNED:
                    if (this.IsView) {
                        generaAddPendingRowToDt(ref dt, drwfh, strApproving);
                    } else {
                        generaAddInTurnRowToDt(ref dt, drwfh, strApproving, approvedStatus);
                    }
                    break;
                case APHelper.FlowNodeStatus.APPROVED:
                    generaAddRowToDt(ref dt, drwfh, strApproved, approvedStatus);
                    break;
                case APHelper.FlowNodeStatus.CANCELLED:
                    generaAddRowToDt(ref dt, drwfh, strReturnModify, rejectedStatus);
                    break;
                default:
                    break;
            }
        }
        for (int n = 0; n < dt.Rows.Count; n++) {

            DataRow dr1 = dt.Rows[n];
            DataRow dr2 = dt.Rows[n + 1];//后一个节点
            if (string.IsNullOrEmpty(dr2["AuditDate"].ToString())) {
                dr2["AuditDate"] = DateTime.Now.ToString();
            }
            TimeSpan ts = Convert.ToDateTime(dr2["AuditDate"]) - Convert.ToDateTime(dr1["AuditDate"]);
            if (ts.Days >= 2) {
                dr2["IsOutTime"] = 1;
            } else {
                dr2["IsOutTime"] = 0;
            }
            if (n + 1 == dt.Rows.Count - 1) {
                break;
            }
        }
        ds.Tables.Add(dt);
        return ds;
    }

    private void generaAddPendingRowToDt(ref DataTable dt, lib.APWorkFlow.NodeStatusRow drwfh, string regionCaption) {
        AuthorizationBLL auBLL = new AuthorizationBLL();
        string approvalNames = auBLL.GetApprovalNamesByUserIds(DBNullConverter.ToStr(drwfh.PARTICIPANT));

        DataRow dr = dt.NewRow();
        dr["RegionCaption"] = regionCaption;
        dr["Approver"] = approvalNames;
        dr["FlowStatus"] = waitingStatus;
        dr["AuditDate"] = string.Empty;
        dr["Comment"] = null;
        dr["IsView"] = 0;
        dr["IsPending"] = 1;

        dt.Rows.Add(dr);
    }

    private void generaAddInTurnRowToDt(ref DataTable dt, lib.APWorkFlow.NodeStatusRow drwfh, string regionCaption, string status) {
        string userName = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffName;
        DataRow dr;
        dr = dt.NewRow();
        dr["RegionCaption"] = regionCaption;
        dr["Approver"] = userName;
        dr["FlowStatus"] = status;
        dr["AuditDate"] = string.Empty;
        dr["Comment"] = string.Empty;
        dr["IsView"] = 1;
        dr["IsPending"] = 0;
        dt.Rows.Add(dr);
    }

    private void generaAddErrorPendingRowToDt(ref DataTable dt, lib.APWorkFlow.NodeStatusRow drwfh, string regionCaption) {
        AuthorizationBLL auBLL = new AuthorizationBLL();
        string approvalNames = auBLL.GetApprovalNamesByUserIds(DBNullConverter.ToStr(drwfh.PARTICIPANT));

        DataRow dr = dt.NewRow();
        dr["RegionCaption"] = regionCaption;
        dr["Approver"] = approvalNames;
        dr["AuditDate"] = string.Empty;
        dr["IsView"] = 0;
        dr["IsPending"] = 1;
        dr["OnError"] = 1;
        dr["Comment"] = DBNullConverter.ToStr(drwfh.ERROR_MSG);
        dr["FlowStatus"] = errorStatus;

        dt.Rows.Add(dr);
    }

    private void generaAddErrorInTurnRowToDt(ref DataTable dt, lib.APWorkFlow.NodeStatusRow drwfh, string regionCaption, string status) {
        string userName = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffName;
        DataRow dr;
        dr = dt.NewRow();
        dr["RegionCaption"] = regionCaption;
        dr["Approver"] = userName;
        dr["FlowStatus"] = status;
        dr["AuditDate"] = string.Empty;
        dr["IsView"] = 1;
        dr["IsPending"] = 0;
        dr["OnError"] = 1;
        dr["Comment"] = DBNullConverter.ToStr(drwfh.ERROR_MSG);
        dt.Rows.Add(dr);
    }

    private void generaAddRowToDt(ref DataTable dt, lib.APWorkFlow.NodeStatusRow drwfh, string regionCaption, string status) {
        DataRow dr;
        dr = dt.NewRow();
        dr["RegionCaption"] = regionCaption;
        dr["Approver"] = DBNullConverter.ToStr(drwfh.APPROVED_BY);
        dr["FlowStatus"] = status;
        dr["AuditDate"] = DBNullConverter.ToStr(drwfh.COMPLETED_DATE);
        dr["Comment"] = DBNullConverter.ToStr(drwfh.COMMENTS);
        dr["IsView"] = 0;
        dr["IsPending"] = 0;
        dt.Rows.Add(dr);
    }

    public bool GetApproveOrReject() {
        if (this.radApprove.Checked == true) {
            return true;
        } else {
            return false;
        }
    }

    public string GetComments() {
        return this.txtComments.Text;
    }

    #endregion

    #region " Public Method "

    public bool CheckInputData() {
        if (this.radApprove.Checked == false) {
            if (this.txtComments.Text == string.Empty) {
                //PageUtility.ShowModelDlg(this.Page, "请输入Comments！");
                PageUtility.ShowModelDlg(this.Page, GetLocalResourceObject("Comments").ToString());
                return false;
            }
        }

        if (this.txtComments.Text.Length > 100) {
            //PageUtility.ShowModelDlg(this.Page, "输入的Comments超过100个字！");
            PageUtility.ShowModelDlg(this.Page, GetLocalResourceObject("CommentsLong").ToString());
            return false;
        }
        return true;
    }

    public void ReloadCtrl() {
        //base.Dispose();
        while (this.cwfTable.Rows.Count > 0)
            this.cwfTable.Rows.RemoveAt(0);
        createCheckWorkFlow();
    }

    public string setHighLight(string IsOutTime) {
        string temp = string.Empty;
        if (IsOutTime == "1") {
            //temp = "<span style='color:Red'>(审批时间超过48小时！)</span>";
            temp = "<span style='color:Red'>(" + GetLocalResourceObject("TimeOut").ToString() + ")</span>";
        }
        return temp;

    }
    #endregion

}
