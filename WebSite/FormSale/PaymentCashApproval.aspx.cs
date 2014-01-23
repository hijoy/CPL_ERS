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
using BusinessObjects.MasterDataTableAdapters;

public partial class FormSale_PaymentCashApproval : BasePage {

    decimal OrderAmountTotal = 0;
    decimal ActualOrderAmountTotal = 0;
    decimal ApplyAmountTotal = 0;
    decimal ApplyAmountRMBTotal = 0;
    decimal SettlementAmountTotal = 0;
    decimal PayedAmountTotal = 0;
    decimal AmountRMBTotal = 0;
    decimal AmountBeforeTaxTotal = 0;
    decimal TaxAmountTotal = 0;
    decimal InvoiceFeeTotal = 0;

    private FormSaleBLL m_FormSaleBLL;
    protected FormSaleBLL FormSaleBLL {
        get {
            if (this.m_FormSaleBLL == null) {
                this.m_FormSaleBLL = new FormSaleBLL();
            }
            return this.m_FormSaleBLL;
        }
    }

    private FormDS m_InnerDS;
    public FormDS InnerDS {
        get {
            if (this.ViewState["FormDS"] == null) {
                this.ViewState["FormDS"] = new FormDS();
            }
            return (FormDS)this.ViewState["FormDS"];
        }
    }


    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);

            MasterDataBLL mdBLL = new MasterDataBLL();
            int formID = int.Parse(Request["ObjectId"]);
            this.ViewState["ObjectId"] = formID;
            FormDS.FormSalePaymentRow rowFormPayment = this.FormSaleBLL.GetFormSalePaymentByID(int.Parse(this.ViewState["ObjectId"].ToString()));

            FormDS.FormRow rowForm = this.FormSaleBLL.GetFormByID(formID)[0];
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
            this.AttendDateCtl.Text = applicant.AttendDate.ToShortDateString();

            FormDS.FormSaleSettlementRow rowFormSettlement = this.FormSaleBLL.GetFormSaleSettlementByID(rowFormPayment.FormSaleSettlementID);
            FormDS.FormRow settlementForm = this.FormSaleBLL.GetFormByID(rowFormPayment.FormSaleSettlementID)[0];

            MasterData.CustomerRow customer = mdBLL.GetCustomerById(rowFormSettlement.CustomerID)[0];
            this.CustomerNameCtl.Text = customer.CustomerName;
            this.CustomerChannelCtl.Text = mdBLL.GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelName;
            this.CustomerRegionCtl.Text = mdBLL.GetCustomerRegionById(customer.CustomerRegionID).CustomerRegionName;
            this.CityCtl.Text = customer.City;
            this.BrandCtl.Text = mdBLL.GetBrandById(rowFormSettlement.BrandID)[0].BrandName;
            MasterData.ExpenseSubCategoryRow rowExpenseSubCategory = mdBLL.GetExpenseSubCategoryById(rowFormSettlement.ExpenseSubCategoryID);
            this.ExpenseCategoryCtl.Text = mdBLL.GetExpenseCategoryById(rowExpenseSubCategory.ExpenseCategoryID).ExpenseCategoryName;
            this.ExpenseSubCategoryCtl.Text = rowExpenseSubCategory.ExpenseSubCategoryName;
            this.CurrencyCtl.Text = mdBLL.GetCurrencyByID(rowFormSettlement.CurrencyID).CurrencyShortName;
            this.CostCenterCtl.Text = CommonUtility.GetMAACostCenterFullName(rowForm.CostCenterID);

            this.SettlementRemarkCtl.Text = rowFormSettlement.IsRemarkNull() ? "" : rowFormSettlement.Remark;
            this.PaymentAmountCtl.Text = rowForm.IsPaymentAmountNull() ? "" : rowForm.PaymentAmount.ToString("N");
            this.PaymentDateCtl.Text = rowForm.IsPaymentDateNull() ? "" : rowForm.PaymentDate.ToShortDateString();
            this.txtVatType.Text = mdBLL.GetVatTypeById(rowFormPayment.VatTypeID)[0].VatTypeName;

            if (!rowFormPayment.IsRemarkNull()) {
                this.RemarkCtl.Text = rowFormPayment.Remark;
            }
            if (!rowFormPayment.IsAttachedFileNameNull()) {
                this.UCPaymentFile.AttachmentFileName = rowFormPayment.AttachedFileName;
            }
            if (!rowFormPayment.IsRealAttachedFileNameNull()) {
                this.UCPaymentFile.RealAttachmentFileName = rowFormPayment.RealAttachedFileName;
            }
            this.PaymentTypeCtl.Text = new MasterDataBLL().GetPaymentTypeById(rowFormPayment.PaymentTypeID).PaymentTypeName;
            this.InvoiceStatusCtl.Text = new InvoiceStatusTableAdapter().GetDataByID(rowFormPayment.InvoiceStatusID)[0].Name;
            if (!rowFormPayment.IsVendorIDNull()) {
                MasterData.VendorRow vendor = new MasterDataBLL().GetVendorByID(rowFormPayment.VendorID);
                this.VendorCtl.Text = vendor.VendorName + "-" + vendor.VendorCode;
            }

            this.SettlementFormNoCtl.Text = settlementForm.FormNo;
            this.SettlementFormNoCtl.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/SettlementApproval.aspx?ShowDialog=1&ObjectId=" + rowFormPayment.FormSaleSettlementID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";

            //PO
            if (!rowFormPayment.IsFormPOIDNull()) {
                FormDS.FormRow rowFormPO = this.FormSaleBLL.GetFormByID(rowFormPayment.FormPOID)[0];
                this.hlPO.Text = rowFormPO.FormNo;
                this.hlPO.NavigateUrl = CommonUtility.GetPOPostBackUrl(rowFormPO.FormID);
            } else {
                this.hlPO.Text = "无";
            }

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                FormDS.FormRow rejectedForm = this.FormSaleBLL.GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/PaymentCashApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }

            //费用合计信息
            ApplyAmountRMBCtl.Text = rowFormSettlement.IsApplyAmountRMBNull() ? "" : rowFormSettlement.ApplyAmountRMB.ToString();
            ForecastOrderAmountCtl.Text = rowFormSettlement.IsForecastOrderAmountNull() ? "" : rowFormSettlement.ForecastOrderAmount.ToString();
            CostBenefitRateCtl.Text = rowFormSettlement.IsCostBenefitRateNull() ? "" : rowFormSettlement.CostBenefitRate.ToString();
            AmountRMBCtl.Text = rowFormSettlement.AmountRMB.ToString();
            ActualOrderAmountCtl.Text = rowFormSettlement.IsActualOrderAmountNull() ? "" : rowFormSettlement.ActualOrderAmount.ToString();
            ActualCostBenefitRateCtl.Text = rowFormSettlement.IsActualCostBenefitRateNull() ? "" : rowFormSettlement.ActualCostBenefitRate.ToString();

            //判断是Activity还是NoActivity，如果NoActivity那么需要隐藏
            if (settlementForm.PageType == (int)SystemEnums.PageType.NoActivitySettlementApply) {
                this.FeeSumTR.Visible = false;
                this.SKUDiv.Visible = false;
                this.gvSKUDetails.Visible = false;
            }

            //明细
            this.odsInvoice.SelectParameters["FormID"].DefaultValue = rowFormPayment.FormSalePaymentID.ToString();
            this.odsSKUDetails.SelectParameters["FormSaleSettlementID"].DefaultValue = rowFormPayment.FormSaleSettlementID.ToString();
            this.odsPaymentDetails.SelectParameters["FormSalePaymentID"].DefaultValue = rowFormPayment.FormSalePaymentID.ToString();

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

            //判断财务摘要的显示问题
            if (!rowForm.IsFinanceRemarkNull()) {
                this.FinanceRemarkCtl.Text = rowForm.FinanceRemark;
            } 
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FinanceRemark, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FinanceRemark, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow ViewerPosition = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool HasViewRight = positionRightBLL.CheckPositionRight(ViewerPosition.PositionId, opViewId);
            bool HasManageRight = positionRightBLL.CheckPositionRight(ViewerPosition.PositionId, opManageId);
            //如果没有权限则隐藏
            if (!HasViewRight && !HasManageRight) {
                this.FinanceRemarkTitleDIV.Visible = false;
                this.FinanceRemarkDIV.Visible = false;
                this.SaveBtn.Visible = false;
            }
            //如果没有填写的权限或者已经导出锁定了，则隐藏保存按钮
            if (HasViewRight && (!HasManageRight || (!rowForm.IsIsExportLockNull() && rowForm.IsExportLock))) {
                this.FinanceRemarkCtl.ReadOnly = true;
                this.SaveBtn.Visible = false;
            }
            //如果不是审批中或者审批完成不能修改
            if (rowForm.StatusID != 1 && rowForm.StatusID != 2) {
                this.FinanceRemarkCtl.ReadOnly = true;
                this.SaveBtn.Visible = false;
            }
            //单据打印
            this.ucPrint.FormID = rowForm.FormID;

            //判断发票是否收回选项           
            if (rowFormPayment.InvoiceStatusID == (int)SystemEnums.InvoiceStatus.Waiting) {
                this.InvoiceReturnTD.Visible = true;
            } else {
                this.InvoiceReturnTD.Visible = false;
                this.InvoiceReturnBtn.Visible = false;
            }
            int opInvoiceReturnId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.InvoiceReturn, SystemEnums.OperateEnum.Manage);
            bool invoiceReturned = rowForm.IsIsInvoiceReturnedNull() ? false : rowForm.IsInvoiceReturned;
            this.InvoiceReturnCtl.Checked = invoiceReturned;
            if (!positionRightBLL.CheckPositionRight(ViewerPosition.PositionId, opInvoiceReturnId) || invoiceReturned) {
                this.InvoiceReturnBtn.Visible = false;
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
        this.Response.Redirect("~/FormSale/PaymentCashApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
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

    protected void InvoiceReturnBtn_Click(object sender, EventArgs e) {

        try {
            new FormTEBLL().InvoiceReturned(int.Parse(this.ViewState["ObjectId"].ToString()));
            if (this.Request["Source"] != null) {
                this.Response.Redirect(this.Request["Source"].ToString());
            } else {
                this.Response.Redirect("~/Home.aspx");
            }
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    protected void gvSKUDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormSettlementSKUDetailRow row = (FormDS.FormSettlementSKUDetailRow)drvDetail.Row;
                OrderAmountTotal = decimal.Round((OrderAmountTotal + row.ForecastOrderAmount), 2);
                ActualOrderAmountTotal = decimal.Round((ActualOrderAmountTotal + row.ActualOrderAmount), 2);

                HyperLink lblApplyFormNo = (HyperLink)e.Row.FindControl("lblApplyFormNo");
                FormDS.FormRow rowApplyForm = this.FormSaleBLL.GetFormByID(row.FormSaleApplyID)[0];
                if (rowApplyForm.PageType == (int)SystemEnums.PageType.ActivityApply) {
                    lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/ActivityApproval.aspx?ShowDialog=1&ObjectId=" + row.FormSaleApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                } else {
                    lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/NoActivityApproval.aspx?ShowDialog=1&ObjectId=" + row.FormSaleApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label lblOrderAmountTotal = (Label)e.Row.FindControl("lblOrderAmountTotal");
                lblOrderAmountTotal.Text = OrderAmountTotal.ToString("N");
                Label lblActualOrderAmountTotal = (Label)e.Row.FindControl("lblActualOrderAmountTotal");
                lblActualOrderAmountTotal.Text = ActualOrderAmountTotal.ToString("N");
                Label lblActualRateTotal = (Label)e.Row.FindControl("lblActualRateTotal");
                if (OrderAmountTotal != null && OrderAmountTotal > 0) {
                    lblActualRateTotal.Text = decimal.Round(ActualOrderAmountTotal / OrderAmountTotal * 100, 2).ToString();
                } else {
                    lblActualRateTotal.Text = "0.00";
                }
            }
        }
    }

    protected void gvPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormSalePaymentDetailRow row = (FormDS.FormSalePaymentDetailRow)drvDetail.Row;
                ApplyAmountTotal = decimal.Round((ApplyAmountTotal + row.ApplyAmount), 2);
                ApplyAmountRMBTotal = decimal.Round((ApplyAmountRMBTotal + row.ApplyAmountRMB), 2);
                SettlementAmountTotal = decimal.Round((SettlementAmountTotal + row.SettlementAmount), 2);
                PayedAmountTotal = decimal.Round((PayedAmountTotal + (row.IsPayedAmountNull() ? 0 : row.PayedAmount)), 2);
                AmountRMBTotal = decimal.Round((AmountRMBTotal + row.AmountRMB), 2);
                if (!row.IsTaxAmountNull()) {
                    TaxAmountTotal = decimal.Round((TaxAmountTotal + row.TaxAmount), 2);
                }
                if (!row.IsAmountBeforeTaxNull()) {
                    AmountBeforeTaxTotal = decimal.Round((AmountBeforeTaxTotal + row.AmountBeforeTax), 2);
                }

                HyperLink lblApplyFormNo = (HyperLink)e.Row.FindControl("lblApplyFormNo");
                FormDS.FormRow rowApplyForm = this.FormSaleBLL.GetFormByID(row.FormSaleApplyID)[0];
                if (rowApplyForm.PageType == (int)SystemEnums.PageType.ActivityApply) {
                    lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/ActivityApproval.aspx?ShowDialog=1&ObjectId=" + row.FormSaleApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                } else {
                    lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/NoActivityApproval.aspx?ShowDialog=1&ObjectId=" + row.FormSaleApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label lblApplyAmountTotal = (Label)e.Row.FindControl("lblApplyAmountTotal");
                lblApplyAmountTotal.Text = ApplyAmountTotal.ToString("N");
                Label lblApplyAmountRMBTotal = (Label)e.Row.FindControl("lblApplyAmountRMBTotal");
                lblApplyAmountRMBTotal.Text = ApplyAmountRMBTotal.ToString("N");
                Label lblSettlementAmountTotal = (Label)e.Row.FindControl("lblSettlementAmountTotal");
                lblSettlementAmountTotal.Text = SettlementAmountTotal.ToString("N");
                Label lblPayedAmountTotal = (Label)e.Row.FindControl("lblPayedAmountTotal");
                lblPayedAmountTotal.Text = PayedAmountTotal.ToString("N");
                Label lblAmountRMBTotal = (Label)e.Row.FindControl("lblAmountRMBTotal");
                lblAmountRMBTotal.Text = AmountRMBTotal.ToString("N");
                Label lblTaxAmountTotal = (Label)e.Row.FindControl("lblTaxAmountTotal");
                lblTaxAmountTotal.Text = TaxAmountTotal.ToString("N");
                Label lblAmountBeforeTaxTotal = (Label)e.Row.FindControl("lblAmountBeforeTaxTotal");
                lblAmountBeforeTaxTotal.Text = AmountBeforeTaxTotal.ToString("N");
            }
        }
    }

    public string GetProductNameByID(object skuID) {
        if (skuID.ToString() != string.Empty) {
            int id = Convert.ToInt32(skuID);
            MasterData.SKURow sku = new SKUTableAdapter().GetDataByID(id)[0];
            return sku.SKUName + '-' + sku.SKUNo;
        } else {
            return null;
        }
    }

    public string GetExpenseItemNameByID(object expenseItemID) {
        if (expenseItemID.ToString() != string.Empty) {
            int id = Convert.ToInt32(expenseItemID);
            return new MasterDataBLL().GetExpenseItemById(id).ExpenseItemName;
        } else {
            return null;
        }
    }

    public string GetDescriptionByID(object FormSaleExpenseDetailID) {
        if (FormSaleExpenseDetailID.ToString() != string.Empty) {
            int id = Convert.ToInt32(FormSaleExpenseDetailID);
            FormDS.FormSaleExpenseDetailRow row = this.FormSaleBLL.GetFormSaleExpenseDetailByID(id);
            string desc = string.Empty;
            if (!row.IsShopNameNull()) {
                desc = row.ShopName;
            }
            if (!row.IsSKUIDNull()) {
                desc = new SKUTableAdapter().GetDataByID(row.SKUID)[0].SKUName;
            }
            return desc;
        } else {
            return null;
        }
    }

    public string GetApplyRemark(object FormSaleExpenseDetailID) {
        if (FormSaleExpenseDetailID.ToString() != string.Empty) {
            int id = Convert.ToInt32(FormSaleExpenseDetailID);
            FormDS.FormSaleExpenseDetailRow row = new FormSaleBLL().GetFormSaleExpenseDetailByID(id);
            if (row.IsRemarkNull()) {
                return null;
            } else {
                return row.Remark;
            }
        } else {
            return null;
        }
    }

    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormInvoiceRow row = (FormDS.FormInvoiceRow)drvDetail.Row;
                InvoiceFeeTotal = decimal.Round((InvoiceFeeTotal + row.InvoiceAmount), 2);
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer) {
            Label lblInvoiceFeeTotal = (Label)e.Row.FindControl("lblInvoiceFeeTotal");
            lblInvoiceFeeTotal.Text = InvoiceFeeTotal.ToString("N");

        }
    }

}