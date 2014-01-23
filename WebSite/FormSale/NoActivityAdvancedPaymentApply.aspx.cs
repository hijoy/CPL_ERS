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

public partial class FormSale_NoActivityAdvancedPaymentApply : BasePage {

    decimal ApplyAmountTotal = 0;
    decimal ApplyAmountRMBTotal = 0;
    decimal PayedAmountTotal = 0;
    decimal InvoiceFeeTotal = 0;
    decimal AmountBeforeTaxTotal = 0;
    decimal TaxAmountTotal = 0;
    decimal AmountRMBTotal = 0;

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

            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow rowUserPosition = (AuthorizationDS.PositionRow)Session["Position"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = rowUserPosition.PositionId;

            this.StuffNameCtl.Text = CommonUtility.GetStaffFullName(stuffUser);
            this.PositionNameCtl.Text = rowUserPosition.PositionName;
            this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowUserPosition.OrganizationUnitId).OrganizationUnitName;
            this.ViewState["DepartmentID"] = rowUserPosition.OrganizationUnitId;
            this.StuffNoCtl.Text = stuffUser.IsStuffNoNull() ? "" : stuffUser.StuffNo;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();

            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());
            }

            //如果是草稿进行赋值
            int formApplyID;
            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                FormDS.FormSalePaymentRow rowFormPayment = this.FormSaleBLL.GetFormSalePaymentByID(int.Parse(this.ViewState["ObjectId"].ToString()));
                formApplyID = rowFormPayment.FormSaleApplyID;
                this.ViewState["FormSaleApplyID"] = formApplyID;
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
                //赋值备注等
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
                    this.VATTypeDDL.SelectedValue = rowFormPayment.VatTypeID.ToString();
                }

                new FormSalePaymentDetailTableAdapter().FillCurrentDataForAdvanced(this.InnerDS.FormSalePaymentDetail, rowFormPayment.FormSalePaymentID, rowFormPayment.FormSaleApplyID);
                new FormInvoiceTableAdapter().FillByFormID(this.InnerDS.FormInvoice, rowFormPayment.FormSalePaymentID);
            } else {
                this.DeleteBtn.Visible = false;
                formApplyID = int.Parse(Request["FormSaleApplyID"]);
                this.ViewState["FormSaleApplyID"] = formApplyID;
                new FormSalePaymentDetailTableAdapter().FillByFormSaleApplyID(this.InnerDS.FormSalePaymentDetail, formApplyID);
            }

            MasterDataBLL mdBLL = new MasterDataBLL();
            this.FormNoCtl.Text = this.FormSaleBLL.GetFormByID(formApplyID)[0].FormNo;
            FormDS.FormSaleApplyRow rowFormApply = this.FormSaleBLL.GetFormSaleApplyByID(formApplyID)[0];
            this.PeriodCtl.Text = rowFormApply.FPeriod.ToString("yyyy-MM");

            MasterData.CustomerRow customer = mdBLL.GetCustomerById(rowFormApply.CustomerID)[0];
            this.CustomerNameCtl.Text = customer.CustomerName;
            this.CustomerChannelCtl.Text = mdBLL.GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelName;
            this.KATypeCtl.Text = customer.IsKaTypeNull() ? "" : customer.KaType;
            this.CustomerRegionCtl.Text = mdBLL.GetCustomerRegionById(customer.CustomerRegionID).CustomerRegionName;
            this.CityCtl.Text = customer.City;
            this.BrandCtl.Text = mdBLL.GetBrandById(rowFormApply.BrandID)[0].BrandName;
            MasterData.ExpenseSubCategoryRow rowExpenseSubCategory = mdBLL.GetExpenseSubCategoryById(rowFormApply.ExpenseSubCategoryID);
            MasterData.ExpenseCategoryRow rowExpenseCategory = mdBLL.GetExpenseCategoryById(rowExpenseSubCategory.ExpenseCategoryID);
            this.ExpenseCategoryCtl.Text = rowExpenseCategory.ExpenseCategoryName;
            this.ExpenseSubCategoryCtl.Text = rowExpenseSubCategory.ExpenseSubCategoryName;
            this.ViewState["NeedPO"] = rowExpenseCategory.NeedPO;
            this.CurrencyCtl.Text = mdBLL.GetCurrencyByID(rowFormApply.CurrencyID).CurrencyShortName;
            this.ExchangeRateCtl.Text = rowFormApply.ExchangeRate.ToString();
            this.ShopNameCtl.Text = rowFormApply.IsShopNameNull() ? "" : rowFormApply.ShopName;
            this.ShopCountCtl.Text = rowFormApply.IsShopCountNull() ? "" : rowFormApply.ShopCount.ToString();
            this.ProjectNameCtl.Text = rowFormApply.IsProjectNameNull() ? "" : rowFormApply.ProjectName;
            this.ViewState["CostCenterID"] = this.FormSaleBLL.GetFormByID(formApplyID)[0].CostCenterID;
            this.CostCenterCtl.Text = CommonUtility.GetMAACostCenterFullName(int.Parse(this.ViewState["CostCenterID"].ToString()));

            this.ProjectDescCtl.Text = rowFormApply.IsProjectDescNull() ? "" : rowFormApply.ProjectDesc;
            if (!rowFormApply.IsApplyFileNameNull())
                this.UCFileUpload.AttachmentFileName = rowFormApply.ApplyFileName;
            if (!rowFormApply.IsApplyRealFileNameNull())
                this.UCFileUpload.RealAttachmentFileName = rowFormApply.ApplyRealFileName;

            if (!rowFormApply.IsActivityBeginDateNull()) {
                this.ActivityBeginCtl.Text = rowFormApply.ActivityBeginDate.ToString("yyyy-MM-dd");
            }
            if (!rowFormApply.IsActivityEndDateNull()) {
                this.ActivityEndCtl.Text = rowFormApply.ActivityEndDate.ToString("yyyy-MM-dd");
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
        this.UCPO.MAAID = int.Parse(this.ViewState["FormSaleApplyID"].ToString());
    }

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
                detailRow.RemainAmount = detailRow.ApplyAmountRMB - detailRow.PayedAmount;
                if (AmountRMB > detailRow.RemainAmount) {
                    PageUtility.ShowModelDlg(this.Page, "预付金额不能大于可用金额");
                    isValid = false;
                    break;
                }

                detailRow.AmountRMB = AmountRMB;
                this.ViewState["AmountRMBTotal"] = decimal.Parse(this.ViewState["AmountRMBTotal"].ToString()) + AmountRMB;
            }
        }
        if (isValid && StatusID == SystemEnums.FormStatus.Awaiting && decimal.Parse(this.ViewState["AmountRMBTotal"].ToString()) <= 0) {
            PageUtility.ShowModelDlg(this.Page, "预付金额必须大于零！");
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
            int FormSaleApplyID = int.Parse(this.ViewState["FormSaleApplyID"].ToString());
            int InvoiceStatusID = int.Parse(this.InvoiceStatusDDL.SelectedValue);
            int PaymentTypeID = (int)SystemEnums.PaymentType.Cash;
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

            if (StatusID == SystemEnums.FormStatus.Awaiting) {//提交时检查，保存草稿不检查
                //vendor
                if (this.UCVendor.VendorID == string.Empty && PaymentTypeID != (int)SystemEnums.PaymentType.Transfer) {
                    PageUtility.ShowModelDlg(this.Page, "请选择供应商!", "please select vendor");
                    return;
                }
                //判断是否录入了发票
                MasterData.InvoiceStatusRow row = new InvoiceStatusTableAdapter().GetDataByID(int.Parse(this.InvoiceStatusDDL.SelectedValue))[0];
                if (row.NeedInvoice) {
                    if (this.gvInvoice.Rows.Count == 0) {
                        PageUtility.ShowModelDlg(this.Page, "请录入发票信息!", "please key in invoice info");
                        return;
                    } else {
                        decimal AmountRMB = decimal.Parse(this.ViewState["AmountRMBTotal"].ToString());
                        if (decimal.Parse(this.ViewState["InvoiceFeeTotal"].ToString()) < AmountRMB) {
                            PageUtility.ShowModelDlg(this.Page, "发票金额不得小于支付金额!", "the amount of invoice should not be less than the payment");
                            return;
                        }
                    }
                }
                //再检查一遍有没有结案过
                string SettledFormNo = this.FormSaleBLL.GetSettledFormNoByApplyFormIds(FormSaleApplyID.ToString());
                if (SettledFormNo != "") {
                    PageUtility.ShowModelDlg(this.Page, "申请单已经被结案过，结案单编号为：" + SettledFormNo);
                    return;
                }
                //检查申请单是否有作废
                if (this.FormSaleBLL.GetFormByID(FormSaleApplyID)[0].StatusID != (int)SystemEnums.FormStatus.ApproveCompleted) {
                    PageUtility.ShowModelDlg(this.Page, "申请单已经作废，不能提交，请删除!");
                    return;
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
                    this.FormSaleBLL.AddAdvancedPayment(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.SaleAdvancedPayment, StatusID,
                            SystemEnums.PageType.NoActivityAdvancedPayment, FormSaleApplyID, InvoiceStatusID, PaymentTypeID, Remark, AttachedFileName, RealAttachedFileName, int.Parse(this.ViewState["CostCenterID"].ToString()), VendorID, FormPOID, VATTypeID);
                } else {
                    int FormID = (int)this.ViewState["ObjectId"];
                    this.FormSaleBLL.UpdateAdvancedPayment(FormID, SystemEnums.FormType.SaleAdvancedPayment, StatusID, InvoiceStatusID, PaymentTypeID, Remark, AttachedFileName, RealAttachedFileName, VendorID, FormPOID, VATTypeID);
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
        if (string.IsNullOrEmpty(this.RemarkCtl.Text)) {
            PageUtility.ShowModelDlg(this, "请输入备注", "Please key in remark");
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

    protected void gvPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormSalePaymentDetailRow row = (FormDS.FormSalePaymentDetailRow)drvDetail.Row;
                ApplyAmountTotal = decimal.Round((ApplyAmountTotal + row.ApplyAmount), 2);
                ApplyAmountRMBTotal = decimal.Round((ApplyAmountRMBTotal + row.ApplyAmountRMB), 2);
                PayedAmountTotal = decimal.Round((PayedAmountTotal + row.PayedAmount), 2);
                AmountBeforeTaxTotal = decimal.Round((AmountBeforeTaxTotal + row.AmountBeforeTax), 2);
                TaxAmountTotal = decimal.Round((TaxAmountTotal + row.TaxAmount), 2);
                AmountRMBTotal = decimal.Round((AmountRMBTotal + row.AmountRMB), 2);

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

}