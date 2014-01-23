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
using System.Threading;

public partial class FormTE_FormInvitationApply : BasePage {

    private FormTEBLL _FormInvitationBLL;

    protected FormTEBLL FormInvitationBLL {
        get {
            if (this._FormInvitationBLL == null) {
                this._FormInvitationBLL = new FormTEBLL();
            }
            return this._FormInvitationBLL;
        }
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
                OpenForm(int.Parse(this.ViewState["ObjectId"].ToString()));
            } else {
                this.DeleteBtn.Visible = false;
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        base.OnLoadComplete(e);
        this.txtAmount.Attributes.Add("onblur", "updateTotalRMB()");
        this.txtExchageRate.Attributes.Add("onblur", "updateTotalRMB()");
        this.txtAttenderCount.Attributes.Add("onblur", "updateTotalRMB()");
    }

    protected void OpenForm(int formID) {
        QueryDS.FormViewRow rowForm = new FormQueryBLL().GetFormViewByID(formID);
        FormDS.FormInvitationApplyRow applyRow = this.FormInvitationBLL.GetByID(formID);
        AuthorizationDS.StuffUserRow applicant = new AuthorizationBLL().GetStuffUserById(rowForm.UserID);
        this.StuffNameCtl.Text = applicant.StuffName;
        this.PositionNameCtl.Text = new OUTreeBLL().GetPositionById(rowForm.PositionID).PositionName;
        if (new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID) != null) {
            this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID).OrganizationUnitName;
        }
        if (!applyRow.IsPeriodNull()) {
            this.dplPeriod.Items.FindByText(applyRow.Period.ToString("yyyy-MM")).Selected = true;
        }
        this.AttendDateCtl.Text = applicant.AttendDate.ToShortDateString();
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

    protected void SaveFormInvitationBLL(SystemEnums.FormStatus StatusID) {
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
        DateTime tt = DateTime.Now;
        string Remark = this.RemarkCtl.Text;
        DateTime? Period = null;
        if (dplPeriod.SelectedValue != null && dplPeriod.SelectedValue != "0") {
            Period = DateTime.Parse(this.dplPeriod.SelectedItem.Text.ToString() + "-1");
        }
        String CustomerName = this.txtCustomerName.Text;
        String AttenderNames = this.txtAttenderNames.Text;
        int AttenderCount = 0;
        if (this.txtAttenderCount.Text != "") {
            AttenderCount = int.Parse(this.txtAttenderCount.Text);
        }
        String BusinessRelation = this.txtBusinessRelation.Text;
        String Place = this.txtPlace.Text;
        DateTime? OccuredDate = null;
        if (this.UCOccuredDate.SelectedDate != null && this.UCOccuredDate.SelectedDate != "") {
            OccuredDate = DateTime.Parse(this.UCOccuredDate.SelectedDate);
        }
        String BusinessPurpose = this.txtBusinessPurpose.Text;
        int CurrencyID = int.Parse(this.dplCurrency.SelectedValue);
        string InvitationType = this.txtInvitationType.Text;
        Decimal? ExchangeRate = null;
        if (this.txtExchageRate.Text != "") {
            ExchangeRate = Decimal.Parse(this.txtExchageRate.Text);
        }
        Decimal? Amount = null;
        if (txtAmount.Text != "") {
            Amount = Decimal.Parse(this.txtAmount.Text);
        }

        decimal[] budgetInfo = null;
        if (StatusID == SystemEnums.FormStatus.Awaiting) {
            budgetInfo = new BudgetBLL().GetPersonalBudgetByParameter(PositionID, Period.GetValueOrDefault().Year);
            TotalBudget = budgetInfo[0];
            ApprovedAmount = budgetInfo[1];
            ApprovingAmount = budgetInfo[2];
            RemainBudget = budgetInfo[3];
            if (Amount * ExchangeRate > budgetInfo[3]) {
                PageUtility.ShowModelDlg(this, "超出预算，不能提交!", "Over budget, Can't submit!");
                return;
            }
        }
        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.FormInvitationBLL.AddFormInvitationApply(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID,
                    SystemEnums.FormType.FormInvitationApply, StatusID, Period, Remark, CustomerName, AttenderNames, AttenderCount, BusinessRelation, Place, OccuredDate, BusinessPurpose, InvitationType, CurrencyID, ExchangeRate, Amount, TotalBudget, ApprovingAmount, ApprovedAmount, RemainBudget);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.FormInvitationBLL.UpdateFormInvitationApply(RejectedFormID.GetValueOrDefault(), FormID, (int)SystemEnums.FormType.FormInvitationApply, StatusID, Period, Remark, CustomerName, AttenderNames, AttenderCount, BusinessRelation, Place, OccuredDate.GetValueOrDefault(), BusinessPurpose, InvitationType, CurrencyID, ExchangeRate, Amount, TotalBudget, ApprovingAmount, ApprovedAmount, RemainBudget);
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    #region Client_IDs

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

    #endregion

    #region 数据绑定及事件

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
        this.FormInvitationBLL.DeleteFormInvitationApply(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        SaveFormInvitationBLL(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        SaveFormInvitationBLL(SystemEnums.FormStatus.Awaiting);
    }

    #endregion
}