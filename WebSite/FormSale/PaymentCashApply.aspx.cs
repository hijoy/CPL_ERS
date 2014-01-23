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

public partial class FormSale_PaymentCashApply : BasePage {

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

    #region 页面初始化及事件处理

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);

            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow rowUserPosition = (AuthorizationDS.PositionRow)Session["Position"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = rowUserPosition.PositionId;

            this.StuffNameCtl.Text = CommonUtility.GetStaffFullName(stuffUser);
            this.PositionNameCtl.Text = rowUserPosition.PositionName;
            this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowUserPosition.OrganizationUnitId).OrganizationUnitName;
            this.ViewState["DepartmentID"] = rowUserPosition.OrganizationUnitId;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();

            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());
            }
            MasterDataBLL mdBLL = new MasterDataBLL();
            //赋值备注等
            this.PaymentTypeDDL.DataSource = mdBLL.GetPaymentTypeForDDL();
            this.PaymentTypeDDL.DataTextField = "PaymentTypeName";
            this.PaymentTypeDDL.DataValueField = "PaymentTypeID";
            this.PaymentTypeDDL.DataBind();
            this.VATTypeDDL.DataBind();
            PaymentTypeDDL_SelectedIndexChanged(null, null);
            //如果是草稿进行赋值
            int FormSaleSettlementID;
            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                FormDS.FormSalePaymentRow rowFormPayment = this.FormSaleBLL.GetFormSalePaymentByID(int.Parse(this.ViewState["ObjectId"].ToString()));
                FormSaleSettlementID = rowFormPayment.FormSaleSettlementID;
                this.ViewState["FormSaleSettlementID"] = FormSaleSettlementID;
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
                this.PaymentTypeDDL.SelectedValue = rowFormPayment.PaymentTypeID.ToString();
                PaymentTypeDDL_SelectedIndexChanged(null, null);
                this.InvoiceStatusDDL.SelectedValue = rowFormPayment.InvoiceStatusID.ToString();
                if (!rowFormPayment.IsRemarkNull()) {
                    this.RemarkCtl.Text = rowFormPayment.Remark;
                }
                if (!rowFormPayment.IsAttachedFileNameNull()) {
                    this.UCPaymentFile.AttachmentFileName = rowFormPayment.AttachedFileName;
                }
                if (!rowFormPayment.IsRealAttachedFileNameNull()) {
                    this.UCPaymentFile.RealAttachmentFileName = rowFormPayment.RealAttachedFileName;
                }
                if (!rowFormPayment.IsVendorIDNull()) {
                    this.UCVendor.VendorID = rowFormPayment.VendorID.ToString();
                }
                if (!rowFormPayment.IsFormPOIDNull()) {
                    this.UCPO.FormID = rowFormPayment.FormPOID.ToString();
                }
                if (!rowFormPayment.IsVatTypeIDNull()) {
                    VATTypeDDL.SelectedValue = rowFormPayment.VatTypeID.ToString();
                }

                new FormSalePaymentDetailTableAdapter().FillCurrentDataForNormal(this.InnerDS.FormSalePaymentDetail, rowFormPayment.FormSalePaymentID);
                new FormInvoiceTableAdapter().FillByFormID(this.InnerDS.FormInvoice, rowFormPayment.FormSalePaymentID);
            } else {
                this.DeleteBtn.Visible = false;
                FormSaleSettlementID = int.Parse(Request["FormSaleSettlementID"]);
                this.ViewState["FormSaleSettlementID"] = FormSaleSettlementID;
                new FormSalePaymentDetailTableAdapter().FillByFormSaleSettlementID(this.InnerDS.FormSalePaymentDetail, FormSaleSettlementID);
            }

            FormDS.FormSaleSettlementRow rowFormSettlement = this.FormSaleBLL.GetFormSaleSettlementByID(FormSaleSettlementID);
            FormDS.FormRow settlementForm = this.FormSaleBLL.GetFormByID(FormSaleSettlementID)[0];
            MasterData.CustomerRow customer = mdBLL.GetCustomerById(rowFormSettlement.CustomerID)[0];
            this.CustomerNameCtl.Text = customer.CustomerName;
            this.CustomerChannelCtl.Text = mdBLL.GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelName;
            this.CustomerRegionCtl.Text = mdBLL.GetCustomerRegionById(customer.CustomerRegionID).CustomerRegionName;
            this.CityCtl.Text = customer.City;
            this.BrandCtl.Text = mdBLL.GetBrandById(rowFormSettlement.BrandID)[0].BrandName;
            MasterData.ExpenseSubCategoryRow rowExpenseSubCategory = mdBLL.GetExpenseSubCategoryById(rowFormSettlement.ExpenseSubCategoryID);
            MasterData.ExpenseCategoryRow rowExpenseCategory = mdBLL.GetExpenseCategoryById(rowExpenseSubCategory.ExpenseCategoryID);
            this.ExpenseCategoryCtl.Text = rowExpenseCategory.ExpenseCategoryName;
            this.ExpenseSubCategoryCtl.Text = rowExpenseSubCategory.ExpenseSubCategoryName;
            this.ViewState["NeedPO"] = rowExpenseCategory.NeedPO;
            this.CurrencyCtl.Text = mdBLL.GetCurrencyByID(rowFormSettlement.CurrencyID).CurrencyShortName;
            this.ViewState["CostCenterID"] = this.FormSaleBLL.GetFormByID(FormSaleSettlementID)[0].CostCenterID;
            this.CostCenterCtl.Text = CommonUtility.GetMAACostCenterFullName(int.Parse(this.ViewState["CostCenterID"].ToString()));

            this.SettlementRemarkCtl.Text = rowFormSettlement.IsRemarkNull() ? "" : rowFormSettlement.Remark;
            if (!rowFormSettlement.IsAttachedFileNameNull())
                this.UCSettlementFile.AttachmentFileName = rowFormSettlement.AttachedFileName;
            if (!rowFormSettlement.IsRealAttachedFileNameNull())
                this.UCSettlementFile.RealAttachmentFileName = rowFormSettlement.RealAttachedFileName;
            this.SettlementFormNoCtl.Text = settlementForm.FormNo;
            this.SettlementFormNoCtl.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/SettlementApproval.aspx?ShowDialog=1&ObjectId=" + FormSaleSettlementID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";

            //费用合计信息
            ApplyAmountRMBCtl.Text = rowFormSettlement.IsApplyAmountRMBNull() ? "" : rowFormSettlement.ApplyAmountRMB.ToString();
            ForecastOrderAmountCtl.Text = rowFormSettlement.IsForecastOrderAmountNull() ? "" : rowFormSettlement.ForecastOrderAmount.ToString();
            CostBenefitRateCtl.Text = rowFormSettlement.IsCostBenefitRateNull() ? "" : rowFormSettlement.CostBenefitRate.ToString();
            AmountRMBCtl.Text = rowFormSettlement.AmountRMB.ToString();
            ActualOrderAmountCtl.Text = rowFormSettlement.IsActualOrderAmountNull() ? "" : rowFormSettlement.ActualOrderAmount.ToString();
            ActualCostBenefitRateCtl.Text = rowFormSettlement.IsActualCostBenefitRateNull() ? "" : rowFormSettlement.ActualCostBenefitRate.ToString();
            this.odsSKUDetails.SelectParameters["FormSaleSettlementID"].DefaultValue = rowFormSettlement.FormSaleSettlementID.ToString();

            //判断是Activity还是NoActivity，如果NoActivity那么需要隐藏
            if (settlementForm.PageType == (int)SystemEnums.PageType.NoActivitySettlementApply) {
                this.FeeSumTR.Visible = false;
                this.SKUDiv.Visible = false;
                this.gvSKUDetails.Visible = false;
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        base.OnLoadComplete(e);
        if (this.gvPaymentDetails.Rows.Count > 0) {
            foreach (GridViewRow item in gvPaymentDetails.Rows) {
                if (item.RowType == DataControlRowType.DataRow) {
                    TextBox txtAmountBeforeTax = (TextBox)item.FindControl("txtAmountBeforeTax");
                    AmountBeforeTaxTotal += decimal.Parse(txtAmountBeforeTax.Text == string.Empty ? "0" : txtAmountBeforeTax.Text.ToString());
                    TextBox txtTaxAmount = (TextBox)item.FindControl("txtTaxAmount");
                    TaxAmountTotal += decimal.Parse(txtTaxAmount.Text == string.Empty ? "0" : txtTaxAmount.Text.ToString());

                    Label txtAmountRMB = (Label)item.FindControl("lblAmountRMB");
                    txtAmountRMB.Text = (decimal.Parse(txtAmountBeforeTax.Text == string.Empty ? "0" : txtAmountBeforeTax.Text.ToString()) + decimal.Parse(txtTaxAmount.Text == string.Empty ? "0" : txtTaxAmount.Text.ToString())).ToString("N");
                }
            }
        }
        Label lblAmountBeforeTaxTotal = (Label)this.gvPaymentDetails.FooterRow.FindControl("lblAmountBeforeTaxTotal");
        lblAmountBeforeTaxTotal.Text = AmountBeforeTaxTotal.ToString("N");
        Label lblTaxAmountTotal = (Label)this.gvPaymentDetails.FooterRow.FindControl("lblTaxAmountTotal");
        lblTaxAmountTotal.Text = TaxAmountTotal.ToString("N");
        Label lblAmountRMBTotal = (Label)this.gvPaymentDetails.FooterRow.FindControl("lblAmountRMBTotal");
        lblAmountRMBTotal.Text = (AmountBeforeTaxTotal + TaxAmountTotal).ToString("N");

        this.UCPO.SettlementID = int.Parse(ViewState["FormSaleSettlementID"].ToString());
    }

    #endregion

    private bool FillDetail(SystemEnums.FormStatus StatusID) {
        bool isValid = true;
        this.ViewState["AmountRMBTotal"] = 0;
        //先填充产品明细
        foreach (GridViewRow row in this.gvPaymentDetails.Rows) {
            if (row.RowType == DataControlRowType.DataRow) {
                FormDS.FormSalePaymentDetailRow detailRow = this.InnerDS.FormSalePaymentDetail[row.RowIndex];
                TextBox txtAmountBeforeTax = (TextBox)row.FindControl("txtAmountBeforeTax");
                if (string.IsNullOrEmpty(txtAmountBeforeTax.Text.Trim())) {
                    detailRow.AmountBeforeTax = 0;
                } else {
                    decimal AmountBeforeTax = decimal.Parse(txtAmountBeforeTax.Text.Trim());
                    AmountBeforeTax = decimal.Round(AmountBeforeTax, 2);
                    detailRow.AmountBeforeTax = AmountBeforeTax;
                }

                decimal TaxAmount = 0;
                decimal.TryParse(((TextBox)row.FindControl("txtTaxAmount")).Text, out TaxAmount);
                if (StatusID == SystemEnums.FormStatus.Awaiting && detailRow.AmountBeforeTax > 0) {
                    //是否有税率
                    if (new MasterDataBLL().GetVatTypeById(int.Parse(this.VATTypeDDL.SelectedValue))[0].HasTax) {
                        if (TaxAmount <= 0) {
                            PageUtility.ShowModelDlg(this, "税率类型不为VAT0，必须填写税金！");
                            return false;
                        }
                    } else {
                        if (TaxAmount > 0) {
                            PageUtility.ShowModelDlg(this, "税率类型为VAT0，不能填写税金！");
                            return false;
                        }
                    }
                }
                TaxAmount = decimal.Round(TaxAmount, 2);
                detailRow.TaxAmount = TaxAmount;

                decimal AmountRMB = detailRow.AmountBeforeTax + detailRow.TaxAmount;

                detailRow.PayedAmount = this.FormSaleBLL.GetPayedAmountByFormSaleExpenseDetailID(detailRow.FormSaleExpenseDetailID);
                detailRow.RemainAmount = detailRow.SettlementAmount - detailRow.PayedAmount;
                if (AmountRMB > detailRow.RemainAmount) {
                    PageUtility.ShowModelDlg(this.Page, "支付金额不能大于可用金额");
                    isValid = false;
                    break;
                }
                detailRow.AmountRMB = AmountRMB;
                this.ViewState["AmountRMBTotal"] = decimal.Parse(this.ViewState["AmountRMBTotal"].ToString()) + AmountRMB;
            }
        }
        if (isValid && StatusID == SystemEnums.FormStatus.Awaiting && decimal.Parse(this.ViewState["AmountRMBTotal"].ToString()) <= 0) {
            PageUtility.ShowModelDlg(this.Page, "支付金额必须大于零！");
            isValid = false;
        }
        return isValid;
    }

    protected void SaveSalePayment(SystemEnums.FormStatus StatusID) {
        this.FormSaleBLL.FormDataSet = this.InnerDS;
        if (FillDetail(StatusID)) {
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
            int FormSaleSettlementID = int.Parse(this.ViewState["FormSaleSettlementID"].ToString());
            int InvoiceStatusID = int.Parse(this.InvoiceStatusDDL.SelectedValue);
            int PaymentTypeID = int.Parse(this.PaymentTypeDDL.SelectedValue);
            string Remark = this.RemarkCtl.Text;
            string AttachedFileName = this.UCPaymentFile.AttachmentFileName;
            string RealAttachedFileName = this.UCPaymentFile.RealAttachmentFileName;
            int? VendorID = null;
            if (this.UCVendor.VendorID != string.Empty) {
                VendorID = int.Parse(this.UCVendor.VendorID);
            }
            int? VATTypeID = null;
            if (this.VATTypeDDL.SelectedValue != "0") {
                VATTypeID = int.Parse(this.VATTypeDDL.SelectedValue);
            }

            //提交时检查，保存草稿不检查
            if (StatusID == SystemEnums.FormStatus.Awaiting) {
                //vendor
                if (this.UCVendor.VendorID == string.Empty && PaymentTypeID != (int)SystemEnums.PaymentType.Transfer) {
                    PageUtility.ShowModelDlg(this.Page, "请选择供应商!", "please select vendor");
                    return;
                }

                //发票
                MasterData.InvoiceStatusRow row = new InvoiceStatusTableAdapter().GetDataByID(int.Parse(this.InvoiceStatusDDL.SelectedValue))[0];
                if (row.NeedInvoice) {
                    if (this.gvInvoice.Rows.Count == 0) {
                        PageUtility.ShowModelDlg(this.Page, "请录入发票信息!", "please key in invoice info");
                        return;
                    } else {
                        decimal totalAmountRMB = decimal.Parse(this.ViewState["AmountRMBTotal"].ToString());
                        if (decimal.Parse(this.ViewState["InvoiceFeeTotal"].ToString()) < totalAmountRMB) {
                            PageUtility.ShowModelDlg(this.Page, "发票金额不得小于支付金额!", "the amount of invoice should not be less than the payment");
                            return;
                        }
                    }
                }
            }

            //PO
            int FormPOID = 0;
            int.TryParse(this.UCPO.FormID, out FormPOID);
            if (StatusID == SystemEnums.FormStatus.Awaiting && (bool)this.ViewState["NeedPO"] == true && decimal.Parse(this.ViewState["AmountRMBTotal"].ToString()) > 5000 && FormPOID <= 0) {
                PageUtility.ShowModelDlg(this, "请选择PO");
                return;
            }

            try {
                if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                    this.FormSaleBLL.AddPaymentCash(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.SalePayment, StatusID,
                            SystemEnums.PageType.PaymentCash, FormSaleSettlementID, InvoiceStatusID, PaymentTypeID, Remark, AttachedFileName, RealAttachedFileName, int.Parse(this.ViewState["CostCenterID"].ToString()), VendorID, FormPOID, VATTypeID);
                } else {
                    int FormID = (int)this.ViewState["ObjectId"];
                    this.FormSaleBLL.UpdatePaymentCash(FormID, SystemEnums.FormType.SalePayment, StatusID, InvoiceStatusID, PaymentTypeID, Remark, AttachedFileName, RealAttachedFileName, VendorID, FormPOID, VATTypeID);
                }
                this.Page.Response.Redirect("~/Home.aspx");
            } catch (Exception ex) {
                PageUtility.DealWithException(this.Page, ex);
            }
        }
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        SaveSalePayment(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        if (this.VATTypeDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this.Page, "请选择税率类型!", "please select VAT Rate");
            return;
        }
        if (string.IsNullOrEmpty(RemarkCtl.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请填写备注!", "please key in remark");
            return;
        }
        SaveSalePayment(SystemEnums.FormStatus.Awaiting);
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
        this.FormSaleBLL.DeleteFormSalePaymentByFormID(formID);
        this.Page.Response.Redirect("~/Home.aspx");
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
                PayedAmountTotal = decimal.Round((PayedAmountTotal + row.PayedAmount), 2);
                AmountBeforeTaxTotal = decimal.Round((AmountBeforeTaxTotal + row.AmountBeforeTax), 2);
                TaxAmountTotal = decimal.Round((TaxAmountTotal + row.TaxAmount), 2);
                AmountRMBTotal = decimal.Round((AmountRMBTotal + row.AmountRMB), 2);
                HyperLink lblApplyFormNo = (HyperLink)e.Row.FindControl("lblApplyFormNo");
                FormDS.FormRow rowApplyForm = this.FormSaleBLL.GetFormByID(row.FormSaleApplyID)[0];
                if (rowApplyForm.PageType == (int)SystemEnums.PageType.ActivityApply) {
                    lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/ActivityApproval.aspx?ShowDialog=1&ObjectId=" + row.FormSaleApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                } else {
                    lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/NoActivityApproval.aspx?ShowDialog=1&ObjectId=" + row.FormSaleApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                }

                TextBox txtAmountBeforeTax = (TextBox)e.Row.FindControl("txtAmountBeforeTax");
                if (!row.IsAmountBeforeTaxNull()) {
                    txtAmountBeforeTax.Text = row.AmountBeforeTax.ToString();
                } else {
                    txtAmountBeforeTax.Text = "0";
                }
                txtAmountBeforeTax.Attributes.Add("onFocus", "MinusExpenseTotal(this," + e.Row.DataItemIndex.ToString() + ",'lblAmountBeforeTaxTotal')");
                txtAmountBeforeTax.Attributes.Add("onBlur", "PlusExpenseTotal(this," + e.Row.DataItemIndex.ToString() + ",'lblAmountBeforeTaxTotal')");

                TextBox txtTaxAmount = (TextBox)e.Row.FindControl("txtTaxAmount");
                if (!row.IsTaxAmountNull()) {
                    txtTaxAmount.Text = row.TaxAmount.ToString();
                } else {
                    txtTaxAmount.Text = "0";
                }
                txtTaxAmount.Attributes.Add("onFocus", "MinusExpenseTotal(this," + e.Row.DataItemIndex.ToString() + ",'lblTaxAmountTotal')");
                txtTaxAmount.Attributes.Add("onBlur", "PlusExpenseTotal(this," + e.Row.DataItemIndex.ToString() + ",'lblTaxAmountTotal')");

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
                Label lblAmountRMB = (Label)e.Row.FindControl("lblAmountRMBTotal");
                lblAmountRMB.Text = AmountRMBTotal.ToString("N");
                Label lblTaxAmountTotal = (Label)e.Row.FindControl("lblTaxAmountTotal");
                lblTaxAmountTotal.Text = TaxAmountTotal.ToString("N");
                Label lblAmountBeforeTaxTotal = (Label)e.Row.FindControl("lblAmountBeforeTaxTotal");
                lblAmountBeforeTaxTotal.Text = AmountBeforeTaxTotal.ToString("N");
            }
        }
    }

    protected void odsPaymentDetails_OnObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormSaleBLL bll = (FormSaleBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
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

    protected void odsInvoice_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormSaleBLL bll = (FormSaleBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
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

        this.ViewState["InvoiceFeeTotal"] = InvoiceFeeTotal;

        if (e.Row.RowType == DataControlRowType.Footer) {
            Label lblInvoiceFeeTotal = (Label)e.Row.FindControl("lblInvoiceFeeTotal");
            lblInvoiceFeeTotal.Text = InvoiceFeeTotal.ToString("N");
        }
    }

    protected void PaymentTypeDDL_SelectedIndexChanged(object sender, EventArgs e) {
        int PaymentTypeID = int.Parse(this.PaymentTypeDDL.SelectedValue);
        if (PaymentTypeID == (int)SystemEnums.PaymentType.Cash || PaymentTypeID == (int)SystemEnums.PaymentType.Transfer) {
            this.VATTypeDDL.SelectedValue = VATTypeDDL.Items[0].Value;
            this.VATTypeDDL.Enabled = true;
        } else {
            this.VATTypeDDL.SelectedValue = VATTypeDDL.Items[1].Value;
            this.VATTypeDDL.Enabled = false;
        }
        if (PaymentTypeID == (int)SystemEnums.PaymentType.Transfer) {
            this.UCVendor.IsNoClear = "block";
        }
        string InvoiceStatusIds = new MasterDataBLL().GetPaymentTypeById(PaymentTypeID).InvoiceStatusIds;
        if (InvoiceStatusIds != "0") {
            this.odsInvoiceStatus.SelectCommand = string.Format("SELECT [InvoiceStatusID], [Name] FROM [InvoiceStatus] where InvoiceStatusID in ({0})", InvoiceStatusIds);
        } else {
            this.odsInvoiceStatus.SelectCommand = string.Format("SELECT [InvoiceStatusID], [Name] FROM [InvoiceStatus] ");
        }
        this.odsInvoiceStatus.DataBind();
    }
}