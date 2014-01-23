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
using BusinessObjects;
using BusinessObjects.FormDSTableAdapters;

public partial class FormTE_FormInvitationApply : BasePage {

    private FormTEBLL _FormTEBLL;
    protected FormTEBLL FormTEBLL {
        get {
            if (this._FormTEBLL == null) {
                this._FormTEBLL = new FormTEBLL();
            }
            return this._FormTEBLL;
        }
    }

    private decimal[] _CostLimitInfo;

    protected decimal[] CostLimitInfo {
        get {
            if (_CostLimitInfo == null) {
                return new decimal[4];
            } else {
                return _CostLimitInfo;
            }
        }
        set { _CostLimitInfo = value; }
    }

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);
            this.Page.Title = title;

            this.dplPeriod.DataBind();
            this.dplCurrency.DataBind();

            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow UserPosition = (AuthorizationDS.PositionRow)Session["Position"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = UserPosition.PositionId;

            if (new StuffUserBLL().GetCostCenterIDByPositionID(UserPosition.PositionId) == 0) {
                this.Session["ErrorInfor"] = "未找到成本中心，请联系管理员";
                Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
            }
            this.StuffNameCtl.Text = stuffUser.StuffName;
            this.PositionNameCtl.Text = UserPosition.PositionName;
            this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(UserPosition.OrganizationUnitId).OrganizationUnitName;
            this.ViewState["DepartmentID"] = UserPosition.OrganizationUnitId;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();
            this.StuffNoCtl.Text = stuffUser.IsStuffNoNull() ? "" : stuffUser.StuffNo;

            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());
            }

            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
                OpenReimburseForm(int.Parse(this.ViewState["ObjectId"].ToString()));
            } else {
                if (Request["FormInvitationApplyID"] != null) {
                    this.ViewState["FormInvitationApplyID"] = int.Parse(Request["FormInvitationApplyID"]);
                    OpenApplyForm(int.Parse(this.ViewState["FormInvitationApplyID"].ToString()));
                }
                this.DeleteBtn.Visible = false;
            }
            //申请单编号
            if (this.ViewState["FormInvitationApplyID"] != null) {
                QueryDS.FormViewRow rowApplyForm = new FormQueryBLL().GetFormViewByID(int.Parse(this.ViewState["FormInvitationApplyID"].ToString()));
                this.ApplyFormNoCtl.Text = rowApplyForm.FormNo;
                this.ApplyFormNoCtl.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormTE/FormInvitationApproval.aspx?ShowDialog=1&ObjectId=" + rowApplyForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }
        }
    }

    protected void OpenApplyForm(int formID) {
        QueryDS.FormViewRow rowForm = new FormQueryBLL().GetFormViewByID(formID);
        FormDS.FormInvitationApplyRow applyRow = this.FormTEBLL.GetByID(formID);
        this.txtCustomerName.Text = applyRow.CustomerName;
        this.RemarkCtl.Text = applyRow.Remark;
        this.txtAttenderNames.Text = applyRow.AttenderNames;
        this.txtBusinessRelation.Text = applyRow.BusinessRelation;
        this.txtBusinessPurpose.Text = applyRow.Purpose;
        this.txtInvitationType.Text = applyRow.InvitationType;
        this.txtExchageRate.Text = applyRow.ExchangeRate.ToString();
        this.dplCurrency.SelectedValue = applyRow.CurrencyID.ToString();
        this.txtAmount.Text = applyRow.Amount.ToString();
        this.txtAmountRMB.Text = applyRow.AmountRMB.ToString("N");
        this.txtPlace.Text = applyRow.Place;
        if (!applyRow.IsOccuredDateNull()) {
            this.UCOccuredDate.SelectedDate = applyRow.OccuredDate.ToShortDateString();
        }
        this.txtAttenderCount.Text = applyRow.AttenderCount.ToString();

    }

    protected void OpenReimburseForm(int formID) {
        QueryDS.FormViewRow rowForm = new FormQueryBLL().GetFormViewByID(formID);
        FormDS.FormInvitationReimburseRow applyRow = this.FormTEBLL.GetFormInvitationReimburseRowByID(formID);
        if (!applyRow.IsPeriodNull()) {
            this.dplPeriod.Items.FindByText(applyRow.Period.ToString("yyyy-MM")).Selected = true;
        }
        this.txtCustomerName.Text = applyRow.CustomerName;
        this.RemarkCtl.Text = applyRow.Remark;
        this.txtAttenderNames.Text = applyRow.AttenderNames;
        this.txtBusinessRelation.Text = applyRow.BusinessRelation;
        this.txtBusinessPurpose.Text = applyRow.Purpose;
        this.txtInvitationType.Text = applyRow.InvitationType;
        this.txtExchageRate.Text = applyRow.ExchangeRate.ToString();
        this.dplCurrency.SelectedValue = applyRow.CurrencyID.ToString();
        this.txtAmount.Text = applyRow.Amount.ToString();
        this.txtAmountRMB.Text = applyRow.AmountRMB.ToString("N");
        this.txtPlace.Text = applyRow.Place;
        if (!applyRow.IsOccuredDateNull()) {
            this.UCOccuredDate.SelectedDate = applyRow.OccuredDate.ToShortDateString();
        }
        this.txtAttenderCount.Text = applyRow.AttenderCount.ToString();
        if ((!applyRow.IsFormInvitationApplyIDNull())) {
            this.ViewState["FormInvitationApplyID"] = applyRow.FormInvitationApplyID;
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        base.OnLoadComplete(e);
        this.txtAmount.Attributes.Add("onblur", "updateTotalRMB()");
    }

    protected String Amount_ClientID {
        get {
            return this.txtAmount.ClientID;
        }
    }

    protected String AmountRMB_ClientID {
        get {
            return this.txtAmountRMB.ClientID;
        }
    }

    protected String ExchangeRate_ClientID {
        get {
            return this.txtExchageRate.ClientID;
        }
    }

    protected String AttenderCount_ClientID {
        get {
            return this.txtAttenderCount.ClientID;
        }
    }

    protected void CancelBtn_Click(object sender, EventArgs e) {
        if (this.Request["Source"] != null) {
            this.Response.Redirect(this.Request["Source"].ToString());
        } else {
            this.Response.Redirect("~/Home.aspx");
        }
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.FormTEBLL.DeleteFormInvitationReimburseApply(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        SaveFormInvitationReimburseBLL(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        SaveFormInvitationReimburseBLL(SystemEnums.FormStatus.Awaiting);
    }

    protected void SaveFormInvitationReimburseBLL(SystemEnums.FormStatus StatusID) {
        decimal TotalBudget = 0;
        decimal ApprovedAmount = 0;
        decimal ApprovingAmount = 0;
        decimal RemainBudget = 0;

        int? RejectedFormID = null;
        if (this.ViewState["RejectedObjectID"] != null) {
            RejectedFormID = (int)this.ViewState["RejectedObjectID"];
        }

        int UserID = (int)this.ViewState["StuffUserID"];
        int? ProxyStuffUserId = null;
        if (Session["ProxyStuffUserId"] != null) {
            ProxyStuffUserId = int.Parse(Session["ProxyStuffUserId"].ToString());
        }
        int OrganizationUnitID = (int)this.ViewState["DepartmentID"];
        int PositionID = (int)this.ViewState["PositionID"];
        string Remark = this.RemarkCtl.Text;
        DateTime? Period = null;
        if (dplPeriod.SelectedValue != null && dplPeriod.SelectedValue != "0") {
            Period = DateTime.Parse(this.dplPeriod.SelectedItem.Text.ToString() + "-1");
        }
        String CustomerName = this.txtCustomerName.Text;
        String AttenderNames = this.txtAttenderNames.Text;
        int AttenderCount = this.txtAttenderCount.Text == "" ? 0 : int.Parse(this.txtAttenderCount.Text);
        String BusinessRelation = this.txtBusinessRelation.Text;
        String Place = this.txtPlace.Text;
        DateTime? OccuredDate = null;
        if (this.UCOccuredDate.SelectedDate != null && this.UCOccuredDate.SelectedDate != "") {
            OccuredDate = DateTime.Parse(this.UCOccuredDate.SelectedDate);
        }
        String BusinessPurpose = this.txtBusinessPurpose.Text;
        int CurrencyID = int.Parse(this.dplCurrency.SelectedValue);
        string InvitationType = this.txtInvitationType.Text;
        Decimal ExchangeRate = this.txtExchageRate.Text == "" ? 1 : Decimal.Parse(this.txtExchageRate.Text);
        Decimal Amount = this.txtAmount.Text == "" ? 0 : Decimal.Parse(this.txtAmount.Text);
        if (StatusID == SystemEnums.FormStatus.Awaiting) {
            if (ExchangeRate == 0) {
                PageUtility.ShowModelDlg(this, "汇率不能为0!", "exchange rate should not equal zero");
                return;
            }
            if (Amount == 0) {
                PageUtility.ShowModelDlg(this, "申请金额不能为0!", "amount should not equal zero");
                return;
            }
            decimal[] budgetInfo = new BudgetBLL().GetPersonalBudgetByParameter(PositionID, Period.GetValueOrDefault().Year);
            TotalBudget = budgetInfo[0];
            ApprovedAmount = budgetInfo[1];
            ApprovingAmount = budgetInfo[2];
            RemainBudget = budgetInfo[3];
            if (Amount * ExchangeRate > budgetInfo[3]) {
                PageUtility.ShowModelDlg(this, "超出预算，不能提交!", "Over budget, Can't submit!");
                return;
            }
        }
        int FormInvitationApplyID = this.ViewState["FormInvitationApplyID"] == null ? 0 : (int)this.ViewState["FormInvitationApplyID"];
        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.FormTEBLL.AddFormInvitationReimburseApply(FormInvitationApplyID, RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID,SystemEnums.FormType.FormInvitationReimburse, StatusID,
                    Period, Remark, CustomerName, AttenderNames, AttenderCount, BusinessRelation, Place, OccuredDate, BusinessPurpose, InvitationType, CurrencyID, ExchangeRate, Amount, TotalBudget, ApprovingAmount, ApprovedAmount, RemainBudget,int.Parse(this.ddlManageExpenseItem.SelectedValue));
            } else {
                this.FormTEBLL.UpdateFormInvitationReimburseApply(RejectedFormID.GetValueOrDefault(), (int)this.ViewState["ObjectId"], (int)SystemEnums.FormType.FormInvitationReimburse, StatusID, Period, Remark,
                    CustomerName, AttenderNames, AttenderCount, BusinessRelation, Place, OccuredDate, BusinessPurpose, InvitationType, CurrencyID, ExchangeRate, Amount, TotalBudget, ApprovingAmount, ApprovedAmount, RemainBudget,int.Parse(this.ddlManageExpenseItem.SelectedValue));
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

}