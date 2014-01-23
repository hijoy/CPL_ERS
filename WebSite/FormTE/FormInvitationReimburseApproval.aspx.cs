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

public partial class FormTE_FormInvitationApproval : BasePage {
    private FormTEBLL _FormTEBLL;
    protected FormTEBLL FormTEBLL {
        get {
            if (this._FormTEBLL == null) {
                this._FormTEBLL = new FormTEBLL();
            }
            return this._FormTEBLL;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);
            this.Page.Title = title;

            int formID = int.Parse(Request["ObjectID"]);
            this.ViewState["ObjectId"] = formID;
            QueryDS.FormViewRow rowForm = new FormQueryBLL().GetFormViewByID(formID);
            FormDS.FormInvitationReimburseRow applyRow = this.FormTEBLL.GetFormInvitationReimburseRowByID(formID);
            if (rowForm.IsProcIDNull()) {
                ViewState["ProcID"] = "";
            } else {
                ViewState["ProcID"] = rowForm.ProcID;
            }

            txtFormNo.Text = rowForm.FormNo;
            AuthorizationDS.StuffUserRow applicant = new AuthorizationBLL().GetStuffUserById(rowForm.UserID);
            this.StuffNameCtl.Text = applicant.StuffName;
            this.PositionNameCtl.Text = new OUTreeBLL().GetPositionById(rowForm.PositionID).PositionName;
            if (new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID) != null) {
                this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID).OrganizationUnitName;
            }
            if (!applicant.IsStuffNoNull()) {
                this.txtStuffID.Text = applicant.StuffNo;
            }
            this.AttendDateCtl.Text = applicant.AttendDate.ToShortDateString();
            if (!applyRow.IsPeriodNull()) {
                this.txtPeriod.Text = applyRow.Period.ToString("yyyy-MM");
            }
            this.txtCustomerName.Text = applyRow.CustomerName;
            this.RemarkCtl.Text = applyRow.Remark;
            this.txtAttenderNames.Text = applyRow.AttenderNames;
            this.txtBusinessRelation.Text = applyRow.BusinessRelation;
            this.txtBusinessPurpose.Text = applyRow.Purpose;
            this.txtInvitationType.Text = applyRow.InvitationType;
            this.txtExchageRate.Text = applyRow.ExchangeRate.ToString();
            this.txtCurrency.Text = new MasterDataBLL().GetCurrencyByID(applyRow.CurrencyID).CurrencyFullName;
            this.txtAmount.Text = applyRow.Amount.ToString();
            this.txtExchageRate.Text = applyRow.ExchangeRate.ToString();
            this.txtAmountRMB.Text = applyRow.AmountRMB.ToString("N");
            this.txtPlace.Text = applyRow.Place;
            if (!applyRow.IsOccuredDateNull()) {
                this.txtOccuredDate.Text = applyRow.OccuredDate.ToShortDateString();
            }
            this.txtAttenderCount.Text = applyRow.AttenderCount.ToString();
            if (!applyRow.IsManageExpenseItemIDNull()) {
                this.ExpenseItemCtl.Text = new MasterDataBLL().GetManageExpenseItemById(applyRow.ManageExpenseItemID).ManageExpenseItemName;
            }
            //预算信息,判断是否隐藏
            this.txtTotalBudget.Text = applyRow.TotalBudget.ToString("N");
            this.txtApprovedAmount.Text = applyRow.ApprovedAmount.ToString("N");
            this.txtApprovingAmount.Text = applyRow.ApprovingAmount.ToString("N");
            this.txtRemainAmount.Text = applyRow.RemainBudget.ToString("N");
            AuthorizationDS.StuffUserRow thisStaff = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            if (thisStaff.StuffUserId == rowForm.UserID) {
                this.budgetTitleDIV.Visible = false;
                this.budgetDIV.Visible = false;
            }
            //申请单编号
            if (!applyRow.IsFormInvitationApplyIDNull()) {
                QueryDS.FormViewRow rowApplyForm = new FormQueryBLL().GetFormViewByID(applyRow.FormInvitationApplyID);
                this.ApplyFormNoCtl.Text = rowApplyForm.FormNo;
                this.ApplyFormNoCtl.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormTE/FormInvitationApproval.aspx?ShowDialog=1&ObjectId=" + rowApplyForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }
            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                FormDS.FormRow rejectedForm = this.FormTEBLL.GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormTE/FormInvitationReimburseApproval.aspx?ShowDialog=1&ObjectID=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }

            //审批页面处理&按钮处理
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            if (rowForm.InTurnUserIds.Contains("P" + stuffUser.StuffUserId + "P")) {
                this.SubmitBtn.Visible = true;
                this.cwfAppCheck.IsView = false;
                this.ViewState["IsView"] = false;
            } else {
                this.SubmitBtn.Visible = false;
                this.cwfAppCheck.IsView = true;
                this.ViewState["IsView"] = true;
            }

            if (rowForm.StatusID == (int)SystemEnums.FormStatus.Rejected && (stuffUser.StuffUserId == rowForm.UserID)) {
                this.EditBtn.Visible = true;
                this.ScrapBtn.Visible = true;
            } else {
                this.EditBtn.Visible = false;
                this.ScrapBtn.Visible = false;
            }

            //如果是弹出,取消按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.upButton.Visible = false;
                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                }
            }
        }
        this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
        this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
        this.cwfAppCheck.IsView = (bool)this.ViewState["IsView"];
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        try {
            if (this.cwfAppCheck.CheckInputData()) {
                AuthorizationDS.StuffUserRow currentStuff = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
                string ProxyStuffName = null;
                if (Session["ProxyStuffUserId"] != null) {
                    ProxyStuffName = new StuffUserBLL().GetStuffUserById(int.Parse(Session["ProxyStuffUserId"].ToString()))[0].StuffName;
                }
                new APFlowBLL().ApproveForm(this.cwfAppCheck.FormID, currentStuff.StuffUserId, currentStuff.StuffName,
                            this.cwfAppCheck.GetApproveOrReject(), this.cwfAppCheck.GetComments(), ProxyStuffName);

                if (this.Request["Source"] != null) {
                    this.Response.Redirect(this.Request["Source"].ToString());
                } else {
                    this.Response.Redirect("~/Home.aspx");
                }
            }
        } catch (Exception exception) {
            this.cwfAppCheck.ReloadCtrl();
            PageUtility.DealWithException(this, exception);
        }
    }

    protected void CancelBtn_Click(object sender, EventArgs e) {
        if (this.Request["Source"] != null) {
            this.Response.Redirect(this.Request["Source"].ToString());
        } else {
            this.Response.Redirect("~/Home.aspx");
        }
    }

    protected void EditBtn_Click(object sender, EventArgs e) {
        this.Response.Redirect("~/FormTE/FormInvitationReimburseApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }
}