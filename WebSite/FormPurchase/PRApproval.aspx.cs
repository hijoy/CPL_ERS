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
using BusinessObjects.PurchaseDSTableAdapters;
using BusinessObjects.MasterDataTableAdapters;

public partial class FormPurchase_PRApproval : BasePage {


    decimal AmountTotal = 0;
    decimal AmountRMBTotal = 0;

    private FormPurchaseBLL m_FormPurchaseBLL;
    protected FormPurchaseBLL FormPurchaseBLL {
        get {
            if (this.m_FormPurchaseBLL == null) {
                this.m_FormPurchaseBLL = new FormPurchaseBLL();
            }
            return this.m_FormPurchaseBLL;
        }
    }



    #region 页面初始化及事件处理

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);

        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);

            MasterDataBLL mdBLL = new MasterDataBLL();
            int formID = int.Parse(Request["ObjectId"]);
            this.ViewState["ObjectId"] = formID;
            PurchaseDS.FormRow rowForm = this.FormPurchaseBLL.GetFormByID(formID)[0];
            PurchaseDS.FormPRRow rowFormPR = this.FormPurchaseBLL.GetFormPRByID(formID);
            if (rowForm.IsProcIDNull()) {
                ViewState["ProcID"] = "";
            } else {
                ViewState["ProcID"] = rowForm.ProcID;
            }

            this.FormNoCtl.Text = rowForm.FormNo;
            AuthorizationDS.StuffUserRow applicant = new AuthorizationBLL().GetStuffUserById(rowForm.UserID);
            this.StuffNameCtl.Text = CommonUtility.GetStaffFullName(applicant);
            this.PositionNameCtl.Text = new OUTreeBLL().GetPositionById(rowForm.PositionID).PositionName;
            if (new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID) != null) {
                this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID).OrganizationUnitName;
            }
            this.StuffNoCtl.Text = applicant.IsStuffNoNull() ? "" : applicant.StuffNo;
            this.AttendDateCtl.Text = applicant.AttendDate.ToShortDateString();

            MasterData.VendorRow vendor = new VendorTableAdapter().GetDataByID(rowFormPR.VendorID)[0];
            this.VendorCodeCtl.Text = vendor.VendorCode;
            this.VendorNameCtl.Text = vendor.VendorName;
            this.VendorAddressCtl.Text = vendor.VendorAddress;
            this.ItemCategoryCtl.Text = new ItemCategoryTableAdapter().GetDataByID(rowFormPR.ItemCategoryID)[0].ItemCategoryName;
            this.CurrencyCtl.Text = new CurrencyTableAdapter().GetDataByID(rowFormPR.CurrencyID)[0].CurrencyShortName;
            this.ExchangeRateCtl.Text = rowFormPR.ExchangeRate.ToString();

            this.PeriodCtl.Text = rowFormPR.FPeriod.ToString("yyyy-MM");
            this.PurchaseBudgetTypeCtl.Text = new PurchaseBudgetTypeTableAdapter().GetDataByID(rowFormPR.PurchaseBudgetTypeID)[0].PurchaseBudgetTypeName;
            this.PurchaseTypeCtl.Text = new PurchaseTypeTableAdapter().GetDataByID(rowFormPR.PurchaseTypeID)[0].PurchaseTypeName;
            this.ShippingTermCtl.Text = new ShippingTermTableAdapter().GetDataByID(rowFormPR.ShippingTermID)[0].ShippingTermName;
            this.PaymentTermCtl.Text = rowFormPR.IsPaymentTermsNull() ? "" : rowFormPR.PaymentTerms;
            this.DeliveryAddressCtl.Text = rowFormPR.IsDeliveryAddressNull() ? "" : rowFormPR.DeliveryAddress;
            this.RemarkCtl.Text = rowFormPR.IsRemarkNull() ? "" : rowFormPR.Remark;
            if (!rowFormPR.IsAttachedFileNameNull())
                this.UCFileUpload.AttachmentFileName = rowFormPR.AttachedFileName;
            if (!rowFormPR.IsRealAttachedFileNameNull())
                this.UCFileUpload.RealAttachmentFileName = rowFormPR.RealAttachedFileName;
            this.RealDeliveryAddressCtl.Text = rowFormPR.IsRealDeliveryAddressNull() ? "" : rowFormPR.RealDeliveryAddress;
            this.IsCloseCtl.Checked = rowFormPR.IsClose;
            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                PurchaseDS.FormRow rejectedForm = this.FormPurchaseBLL.GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormPurchase/PRApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }

            this.TotalBudgetCtl.Text = rowFormPR.TotalBudget.ToString("N");
            this.ApprovedAmountCtl.Text = rowFormPR.ApprovedAmount.ToString("N");
            this.ApprovingAmountCtl.Text = rowFormPR.ApprovingAmount.ToString("N");
            this.ReimbursedAmountCtl.Text = rowFormPR.ReimbursedAmount.ToString("N");
            this.NonReimbursedAmountCtl.Text = rowFormPR.NonReimbursedAmount.ToString("N");
            this.RemainBudgetCtl.Text = rowFormPR.RemainBudget.ToString("N");

            //明细
            this.odsDetails.SelectParameters["FormPRID"].DefaultValue = rowFormPR.FormPRID.ToString();

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

            if (rowForm.StatusID == (int)SystemEnums.FormStatus.Rejected && stuffUser.StuffUserId == rowForm.UserID) {
                this.EditBtn.Visible = true;
                this.ScrapBtn.Visible = true;
            } else {
                this.EditBtn.Visible = false;
                this.ScrapBtn.Visible = false;
            }

            //是否显示报销完成按钮
            this.CloseBtn.Visible = false;
            if ((!rowFormPR.IsClose) && rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted) {
                if (stuffUser.StuffUserId == rowForm.UserID || new AuthorizationBLL().GetProxyReimburseByParameter(rowForm.UserID, stuffUser.StuffUserId, rowForm.SubmitDate).Count > 0) {
                    this.CloseBtn.Visible = true;
                }
            }

            //如果是弹出,取消按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.upButton.Visible = false;
                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                }
            }
            //单据打印
            this.ucPrint.FormID = rowForm.FormID;
        }
        this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
        this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
        this.cwfAppCheck.IsView = (bool)this.ViewState["IsView"];
    }

    #endregion

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
        this.Response.Redirect("~/FormPurchase/PRApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void CloseBtn_Click(object sender, EventArgs e) {
        try {
            int FormPRID = int.Parse(this.ViewState["ObjectId"].ToString());
            string ProcessingPVNo = this.FormPurchaseBLL.GetProcessingPVNoByFormPRID(FormPRID);
            string ProcessingPONo = this.FormPurchaseBLL.GetProcessingPONoByFormPRID(FormPRID);
            if (ProcessingPVNo != null) {
                PageUtility.ShowModelDlg(this.Page, "有没有处理完成的PV，单号为:" + ProcessingPVNo);
                return;
            }
            if (ProcessingPONo != null) {
                PageUtility.ShowModelDlg(this.Page, "有没有处理完成的PO，单号为:" + ProcessingPONo);
                return;
            }
            this.FormPurchaseBLL.CloseFormPR(FormPRID);
            if (this.Request["Source"] != null) {
                this.Response.Redirect(this.Request["Source"].ToString());
            } else {
                this.Response.Redirect("~/Home.aspx");
            }

        } catch (Exception exception) {
            PageUtility.DealWithException(this, exception);
        }
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                PurchaseDS.FormPRDetailRow row = (PurchaseDS.FormPRDetailRow)drvDetail.Row;
                AmountTotal = decimal.Round((AmountTotal + row.Amount), 2);
                AmountRMBTotal = decimal.Round((AmountRMBTotal + row.AmountRMB), 2);
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label lblAmountTotal = (Label)e.Row.FindControl("lblAmountTotal");
                lblAmountTotal.Text = AmountTotal.ToString("N");
                Label lblAmountRMBTotal = (Label)e.Row.FindControl("lblAmountRMBTotal");
                lblAmountRMBTotal.Text = AmountRMBTotal.ToString("N");
            }
        }
    }
}