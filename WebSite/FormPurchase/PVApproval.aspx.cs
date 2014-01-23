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

public partial class FormPurchase_PVApproval : BasePage {

    decimal InvoiceFeeTotal = 0;
    decimal InvoiceReverseFeeTotal = 0;

    private FormPurchaseBLL m_FormPurchaseBLL;
    protected FormPurchaseBLL FormPurchaseBLL {
        get {
            if (this.m_FormPurchaseBLL == null) {
                this.m_FormPurchaseBLL = new FormPurchaseBLL();
            }
            return this.m_FormPurchaseBLL;
        }
    }

    private PurchaseDS m_InnerDS;
    public PurchaseDS InnerDS {
        get {
            if (this.ViewState["PurchaseDS"] == null) {
                this.ViewState["PurchaseDS"] = new PurchaseDS();
            }
            return (PurchaseDS)this.ViewState["PurchaseDS"];
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
            PurchaseDS.FormPVRow rowFormPV = this.FormPurchaseBLL.GetFormPVByID(formID);
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

            MasterData.VendorRow vendor = new VendorTableAdapter().GetDataByID(rowFormPV.VendorID)[0];
            this.VendorCodeCtl.Text = vendor.VendorCode;
            this.VendorNameCtl.Text = vendor.VendorName;
            this.VendorAddressCtl.Text = vendor.VendorAddress;
            this.ItemCategoryCtl.Text = new ItemCategoryTableAdapter().GetDataByID(rowFormPV.ItemCategoryID)[0].ItemCategoryName;
            this.CurrencyCtl.Text = new CurrencyTableAdapter().GetDataByID(rowFormPV.CurrencyID)[0].CurrencyShortName;
            this.ExchangeRateCtl.Text = rowFormPV.ExchangeRate.ToString();
            this.PeriodCtl.Text = rowFormPV.FPeriod.ToString("yyyy-MM");
            this.PurchaseBudgetTypeCtl.Text = new PurchaseBudgetTypeTableAdapter().GetDataByID(rowFormPV.PurchaseBudgetTypeID)[0].PurchaseBudgetTypeName;
            this.PurchaseTypeCtl.Text = new PurchaseTypeTableAdapter().GetDataByID(rowFormPV.PurchaseTypeID)[0].PurchaseTypeName;
            this.ShippingTermCtl.Text = new ShippingTermTableAdapter().GetDataByID(rowFormPV.ShippingTermID)[0].ShippingTermName;
            this.PaymentTermCtl.Text = rowFormPV.PaymentTerms;
            this.ParentFormNoCtl.Text = rowFormPV.ParentFormNo;
            this.RealDeliveryAddressCtl.Text = rowFormPV.IsRealDeliveryAddressNull() ? "" : rowFormPV.RealDeliveryAddress;
            MasterData.MethodPaymentRow MethodPayment = new MasterDataBLL().GetMethodPaymentById(rowFormPV.MethodPaymentID)[0];
            this.MethodPaymentCtl.Text = MethodPayment.MethodPaymentName + "-" + MethodPayment.Description;
            if (!rowFormPV.IsExpectPaymentDateNull()) {
                this.ExpectPaymentDateCtl.Text = rowFormPV.ExpectPaymentDate.ToString("yyyy-MM-dd");
            }
            this.IsUrgentCtl.Text = rowFormPV.IsUrgent ? "Yes" : "No";
            this.IsPublicCtl.Text = rowFormPV.IsPublic ? "Yes" : "No";
            this.ApplyAmountCtl.Text = rowFormPV.ApplyAmount.ToString("N");
            this.PayedAmountCtl.Text = rowFormPV.PayedAmount.ToString("N");
            this.AmountCtl.Text = rowFormPV.Amount.ToString("N");
            this.AmountRMBCtl.Text = rowFormPV.AmountRMB.ToString("N");
            this.InvoiceStatusCtl.Text = new InvoiceStatusTableAdapter().GetDataByID(rowFormPV.InvoiceStatusID)[0].Name;
            //payment term label名称
            if (!rowFormPV.IsIsPTChangedNull() && rowFormPV.IsPTChanged) {
                this.Form_PaymentTerms.Text = Resources.Common.Form_PaymentTerms + "(Changed)";
                this.Form_PaymentTerms.ForeColor = System.Drawing.Color.Red;
            } else {
                this.Form_PaymentTerms.Text = Resources.Common.Form_PaymentTerms;
            }
            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                PurchaseDS.FormRow rejectedForm = this.FormPurchaseBLL.GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormPurchase/PVApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }
            this.RemarkCtl.Text = rowFormPV.IsRemarkNull() ? "" : rowFormPV.Remark;
            if (!rowFormPV.IsAttachedFileNameNull())
                this.UCFileUpload.AttachmentFileName = rowFormPV.AttachedFileName;
            if (!rowFormPV.IsRealAttachedFileNameNull())
                this.UCFileUpload.RealAttachmentFileName = rowFormPV.RealAttachedFileName;

            this.VATRateCtl.Text = new MasterDataBLL().GetVatTypeById(rowFormPV.VatRateID)[0].VatTypeName;
            this.AMTBeforeTaxCtl.Text = rowFormPV.AMTBeforeTax.ToString("N");
            this.AMTTaxCtl.Text = rowFormPV.AMTTax.ToString("N");
            this.PaymentAmountCtl.Text = rowForm.IsPaymentAmountNull() ? "" : rowForm.PaymentAmount.ToString("N");
            this.PaymentDateCtl.Text = rowForm.IsPaymentDateNull() ? "" : rowForm.PaymentDate.ToShortDateString();

            //发票明细
            this.odsInvoice.SelectParameters["FormID"].DefaultValue = rowFormPV.FormPVID.ToString();
            //PR或PO明细
            if (rowFormPV.PVType == (int)SystemEnums.PVType.PR) {
                this.ParentFormNoCtl.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormPurchase/PRApproval.aspx?ShowDialog=1&ObjectId=" + rowFormPV.FormPRID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                new FormPRPODetailViewTableAdapter().FillByPRID(this.InnerDS.FormPRPODetailView, rowFormPV.FormPRID);
            } else {
                this.ParentFormNoCtl.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormPurchase/POApproval.aspx?ShowDialog=1&ObjectId=" + rowFormPV.FormPOID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                new FormPRPODetailViewTableAdapter().FillByPOID(this.InnerDS.FormPRPODetailView, rowFormPV.FormPOID);
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

            if (rowForm.StatusID == (int)SystemEnums.FormStatus.Rejected && stuffUser.StuffUserId == rowForm.UserID) {
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

            //冲抵情况
            if (rowFormPV.InvoiceStatusID != (int)SystemEnums.InvoiceStatus.Yes && rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted) {
                this.odsInvoiceReverse.SelectParameters["FormID"].DefaultValue = rowForm.FormID.ToString();
                if (((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId != rowForm.UserID) {
                    this.fvInvoiceReverse.Visible = false;
                }
            } else {
                this.reverseDIV.Visible = false;
                this.upReverse.Visible = false;
            }
            this.ViewState["ApplicantID"] = rowForm.UserID;
            int opInvoiceReverseManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.InvoiceReverse, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow ViewerPosition = (AuthorizationDS.PositionRow)this.Session["Position"];
            this.HasApproveRight = new PositionRightBLL().CheckPositionRight(ViewerPosition.PositionId, opInvoiceReverseManageId);

            //判断财务摘要和ItemCategory的显示问题
            if (!rowForm.IsFinanceRemarkNull()) {
                this.FinanceRemarkCtl.Text = rowForm.FinanceRemark;
            }
            this.UCItemCategory.ItemCategoryID = rowFormPV.FinalItemCategoryID.ToString();
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FinanceRemark, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FinanceRemark, SystemEnums.OperateEnum.Manage);
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool HasViewRight = positionRightBLL.CheckPositionRight(ViewerPosition.PositionId, opViewId);
            bool HasManageRight = positionRightBLL.CheckPositionRight(ViewerPosition.PositionId, opManageId);
            //如果没有权限则隐藏
            if (!HasViewRight && !HasManageRight) {
                this.FinanceRemarkTitleDIV.Visible = false;
                this.FinanceRemarkDIV.Visible = false;
                this.SaveBtn.Visible = false;
                this.UCItemCategory.IsVisible = "display:none";
            }
            //如果没有填写的权限或者已经导出锁定了，则隐藏保存按钮
            if (HasViewRight && (!HasManageRight || (!rowForm.IsIsExportLockNull() && rowForm.IsExportLock))) {
                this.FinanceRemarkCtl.ReadOnly = true;
                this.SaveBtn.Visible = false;
                this.UCItemCategory.IsVisible = "display:none";
            }
            //如果不是审批中或者审批完成不能修改
            if (rowForm.StatusID != 1 && rowForm.StatusID != 2) {
                this.FinanceRemarkCtl.ReadOnly = true;
                this.SaveBtn.Visible = false;
                this.UCItemCategory.IsVisible = "display:none";
            }
            
            //单据打印
            this.ucPrint.FormID = rowForm.FormID;
        }
        this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
        this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
        this.cwfAppCheck.IsView = (bool)this.ViewState["IsView"];
    }

    protected bool HasApproveRight {
        get {
            return (bool)this.ViewState["HasManageRight"];
        }
        set {
            this.ViewState["HasManageRight"] = value;
        }
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
        this.Response.Redirect("~/FormPurchase/PVApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        if (FinanceRemarkCtl.Text.Trim() == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入财务摘要", "please key in finance remark");
            return;
        }
        try {
            new FormTEBLL().AddFinanceRemark(int.Parse(this.ViewState["ObjectId"].ToString()), this.FinanceRemarkCtl.Text.Trim());
            new FormPurchaseBLL().UpdatePVItemCategory(int.Parse(this.ViewState["ObjectId"].ToString()),int.Parse(this.UCItemCategory.ItemCategoryID));
            PageUtility.ShowModelDlg(this.Page, "已保存", "Successfully saved");
            //if (this.Request["Source"] != null) {
            //    this.Response.Redirect(this.Request["Source"].ToString());
            //} else {
            //    this.Response.Redirect("~/Home.aspx");
            //}
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    protected void odsDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormPurchaseBLL bll = (FormPurchaseBLL)e.ObjectInstance;
        bll.PurchaseDataSet = this.InnerDS;
    }

    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                PurchaseDS.FormInvoiceRow row = (PurchaseDS.FormInvoiceRow)drvDetail.Row;
                InvoiceFeeTotal = decimal.Round((InvoiceFeeTotal + row.InvoiceAmount), 2);
            }
        }

        this.ViewState["InvoiceFeeTotal"] = InvoiceFeeTotal;

        if (e.Row.RowType == DataControlRowType.Footer) {
            Label lblInvoiceFeeTotal = (Label)e.Row.FindControl("lblInvoiceFeeTotal");
            lblInvoiceFeeTotal.Text = InvoiceFeeTotal.ToString("N");

        }
    }

    protected void gvReverse_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                PurchaseDS.FormInvoiceReverseRow row = (PurchaseDS.FormInvoiceReverseRow)drvDetail.Row;
                InvoiceReverseFeeTotal = decimal.Round((InvoiceReverseFeeTotal + row.InvoiceAmount), 2);

                LinkButton lbAgree = (LinkButton)e.Row.FindControl("lbAgree");
                LinkButton lbReject = (LinkButton)e.Row.FindControl("lbReject");
                lbAgree.CommandArgument = row.FormInvoiceReverseID.ToString();
                lbReject.CommandArgument = row.FormInvoiceReverseID.ToString();

                //按钮隐藏
                AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
                LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
                if (stuffUser.StuffUserId != int.Parse(this.ViewState["ApplicantID"].ToString()) || row.Status == 2) {
                    lbDelete.Visible = false;
                }
                if (row.Status == 2 || row.Status == 3 || !this.HasApproveRight) {
                    lbAgree.Visible = false;
                    lbReject.Visible = false;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer) {
            Label lblInvoiceReverseFeeTotal = (Label)e.Row.FindControl("lblInvoiceReverseFeeTotal");
            lblInvoiceReverseFeeTotal.Text = InvoiceReverseFeeTotal.ToString("N");
        }
    }

    protected void lbAgree_Click(object sender, EventArgs e) {
        LinkButton btn = (LinkButton)sender;
        AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
        this.FormPurchaseBLL.ApproveFormInvoiceReverse(int.Parse(btn.CommandArgument), stuffUser.StuffUserId);
        this.gvReverse.DataBind();
        this.upReverse.Update();
    }

    protected void lbReject_Click(object sender, EventArgs e) {
        LinkButton btn = (LinkButton)sender;
        AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
        this.FormPurchaseBLL.RejectFormInvoiceReverse(int.Parse(btn.CommandArgument), stuffUser.StuffUserId);
        this.gvReverse.DataBind();
        this.upReverse.Update();
    }

    public string GetStatus(object status) {
        int id = (int)status;
        string statusName = string.Empty;
        if (id == 1) {
            statusName = "waiting";
        } else if (id == 2) {
            statusName = "approved";
        } else if (id == 3) {
            statusName = "reject";
        }
        return statusName;
    }

    public string GetApprover(object ApproverID) {
        if (ApproverID.ToString() != string.Empty) {
            int id = (int)ApproverID;
            return new AuthorizationBLL().GetStuffUserById(id).StuffName;
        } else {
            return string.Empty;
        }
    }

    protected void odsInvoiceReverse_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["FormID"] = this.ViewState["ObjectId"];
    }

}