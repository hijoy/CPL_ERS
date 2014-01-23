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

public partial class FormRD_RDPaymentApply : BasePage {

    decimal ApplyAmountTotal = 0;
    decimal ApplyAmountRMBTotal = 0;
    decimal PayedAmountTotal = 0;
    decimal AmountRMBTotal = 0;
    decimal AmountBeforeTaxTotal = 0;
    decimal TaxAmountTotal = 0;
    decimal InvoiceFeeTotal = 0;

    private FormRDBLL _FormRDBLL;
    protected FormRDBLL FormRDBLL {
        get {
            if (this._FormRDBLL == null) {
                this._FormRDBLL = new FormRDBLL();
            }
            return this._FormRDBLL;
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
            VATTypeDDL.DataBind();
            PaymentTypeDDL.DataBind();
            PaymentTypeDDL_SelectedIndexChanged(null, null);
            InvoiceStatusDDL.DataBind();
            //如果是草稿进行赋值
            int formApplyID;
            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                FormDS.FormRDPaymentRow rowFormPayment = this.FormRDBLL.GetFormRDPaymentByID(int.Parse(this.ViewState["ObjectId"].ToString()));
                formApplyID = rowFormPayment.FormRDApplyID;
                this.ViewState["FormRDApplyID"] = formApplyID;
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
                //赋值备注等
                this.PaymentTypeDDL.SelectedValue = rowFormPayment.PaymentTypeID.ToString();
                PaymentTypeDDL_SelectedIndexChanged(null, null);
                InvoiceStatusDDL.DataBind();
                if (!rowFormPayment.IsVATTypeIDNull()) {
                    VATTypeDDL.SelectedValue = rowFormPayment.VATTypeID.ToString();
                }
                this.InvoiceStatusDDL.SelectedValue = rowFormPayment.InvoiceStatusID.ToString();
                if (!rowFormPayment.IsPaymentFileNameNull()) {
                    this.UCFliePayment.AttachmentFileName = rowFormPayment.PaymentFileName;
                }
                if (!rowFormPayment.IsPaymentRealFileNameNull()) {
                    this.UCFliePayment.RealAttachmentFileName = rowFormPayment.PaymentRealFileName;
                }
                if (!rowFormPayment.IsRemarkNull()) {
                    this.RemarkCtl.Text = rowFormPayment.Remark;
                }
                new FormRDPaymentDetailTableAdapter().FillCurrentData(this.InnerDS.FormRDPaymentDetail, rowFormPayment.FormRDPaymentID, rowFormPayment.FormRDApplyID);
                new FormInvoiceTableAdapter().FillByFormID(this.InnerDS.FormInvoice, rowFormPayment.FormRDPaymentID);
            } else {
                formApplyID = int.Parse(Request["FormRDApplyID"]);
                this.ViewState["FormRDApplyID"] = formApplyID;
                new FormRDPaymentDetailTableAdapter().FillByRDApplyID(this.InnerDS.FormRDPaymentDetail, formApplyID);
            }
            MasterDataBLL mdBLL = new MasterDataBLL();
            this.FormNoCtl.Text = this.FormRDBLL.GetFormByID(formApplyID)[0].FormNo;
            FormDS.FormRDApplyRow rowFormApply = this.FormRDBLL.GetFormRDApplyByID(formApplyID)[0];
            this.PeriodCtl.Text = rowFormApply.FPeriod.ToString("yyyy-MM");

            this.CustomerChannelCtl.Text = mdBLL.GetCustomerChannelById(rowFormApply.CustomerChannelID)[0].CustomerChannelName;
            this.BrandCtl.Text = mdBLL.GetBrandById(rowFormApply.BrandID)[0].BrandName;
            this.CurrencyCtl.Text = mdBLL.GetCurrencyByID(rowFormApply.CurrencyID).CurrencyShortName;
            this.ExchangeRateCtl.Text = rowFormApply.ExchangeRate.ToString();
            MasterData.ExpenseSubCategoryRow rowExpenseSubCategory = mdBLL.GetExpenseSubCategoryById(rowFormApply.ExpenseSubCategoryID);
            MasterData.ExpenseCategoryRow rowExpenseCategory = mdBLL.GetExpenseCategoryById(rowExpenseSubCategory.ExpenseCategoryID);
            this.ExpenseSubCategoryCtl.Text = rowExpenseSubCategory.ExpenseSubCategoryName;
            this.ViewState["NeedPO"] = rowExpenseCategory.NeedPO;
            if (!rowFormApply.IsProjectNameNull()) {
                this.ProjectNameCtl.Text = rowFormApply.ProjectName;
            }

            this.ProjectDescCtl.Text = rowFormApply.IsProjectDescNull() ? "" : rowFormApply.ProjectDesc;
            if (!rowFormApply.IsApplyFileNameNull())
                this.UCFileApply.AttachmentFileName = rowFormApply.ApplyFileName;
            if (!rowFormApply.IsApplyRealFileNameNull())
                this.UCFileApply.RealAttachmentFileName = rowFormApply.ApplyRealFileName;

            if (!rowFormApply.IsActivityBeginDateNull()) {
                this.ActivityBeginCtl.Text = rowFormApply.ActivityBeginDate.ToString("yyyy-MM-dd");
            }
            if (!rowFormApply.IsActivityEndDateNull()) {
                this.ActivityEndCtl.Text = rowFormApply.ActivityEndDate.ToString("yyyy-MM-dd");
            }
            this.ViewState["CostCenterID"] = this.FormRDBLL.GetFormByID(int.Parse(this.ViewState["FormRDApplyID"].ToString()))[0].CostCenterID;
            this.CostCenterCtl.Text = CommonUtility.GetMAACostCenterFullName(int.Parse(this.ViewState["CostCenterID"].ToString()));
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        base.OnLoadComplete(e);
        decimal totalAmountBeforeTax = 0;
        decimal totalTaxAmount = 0;
        if (this.gvPaymentDetails.Rows.Count > 0) {
            foreach (GridViewRow item in gvPaymentDetails.Rows) {
                if (item.RowType == DataControlRowType.DataRow) {
                    TextBox txtAmountBeforeTax = (TextBox)item.FindControl("txtAmountBeforeTax");
                    totalAmountBeforeTax += decimal.Parse(txtAmountBeforeTax.Text == string.Empty ? "0" : txtAmountBeforeTax.Text.ToString());
                    TextBox txtTaxAmount = (TextBox)item.FindControl("txtTaxAmount");
                    totalTaxAmount += decimal.Parse(txtTaxAmount.Text == string.Empty ? "0" : txtTaxAmount.Text.ToString());

                    Label txtAmountRMB = (Label)item.FindControl("lblAmountRMB");
                    txtAmountRMB.Text = (decimal.Parse(txtAmountBeforeTax.Text == string.Empty ? "0" : txtAmountBeforeTax.Text.ToString()) + decimal.Parse(txtTaxAmount.Text == string.Empty ? "0" : txtTaxAmount.Text.ToString())).ToString("N");
                }
            }
        }
        Label lblAmountBeforeTaxTotal = (Label)this.gvPaymentDetails.FooterRow.FindControl("lblAmountBeforeTaxTotal");
        lblAmountBeforeTaxTotal.Text = totalAmountBeforeTax.ToString("N");
        Label lblTaxAmountTotal = (Label)this.gvPaymentDetails.FooterRow.FindControl("lblTaxAmountTotal");
        lblTaxAmountTotal.Text = totalTaxAmount.ToString("N");

        Label lblApplyAmountTotal = (Label)this.gvPaymentDetails.FooterRow.FindControl("lblApplyAmountTotal");
        lblApplyAmountTotal.Text = ApplyAmountTotal.ToString("N");
        Label lblApplyAmountRMBTotal = (Label)this.gvPaymentDetails.FooterRow.FindControl("lblApplyAmountRMBTotal");
        lblApplyAmountRMBTotal.Text = ApplyAmountRMBTotal.ToString("N");
        Label lblPayedAmountTotal = (Label)this.gvPaymentDetails.FooterRow.FindControl("lblPayedAmountTotal");
        lblPayedAmountTotal.Text = PayedAmountTotal.ToString("N");
        Label lblAmountRMB = (Label)this.gvPaymentDetails.FooterRow.FindControl("lblAmountRMBTotal");
        lblAmountRMB.Text = (totalAmountBeforeTax + totalTaxAmount).ToString("N");

        this.UCPO.MAAID = int.Parse(ViewState["FormRDApplyID"].ToString());
    }

    private bool FillDetail(SystemEnums.FormStatus StatusID) {
        bool isValid = true;
        this.ViewState["AmountRMBTotal"] = 0;
        //先填充产品明细
        foreach (GridViewRow row in this.gvPaymentDetails.Rows) {
            if (row.RowType == DataControlRowType.DataRow) {
                FormDS.FormRDPaymentDetailRow detailRow = this.InnerDS.FormRDPaymentDetail[row.RowIndex];
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
                detailRow.TaxAmount = TaxAmount;
                TaxAmount = decimal.Round(TaxAmount, 2);
                UserControls_VendorControl ucVendor = (UserControls_VendorControl)row.FindControl("UCVendor");
                if (!string.IsNullOrEmpty(ucVendor.VendorID)) {
                    detailRow.VendorID = int.Parse(ucVendor.VendorID);
                }

                decimal AmountRMB = detailRow.AmountBeforeTax + TaxAmount;

                //提交时再取一遍，防止漏洞
                detailRow.PaiedAmount = this.FormRDBLL.GetPaidAmountByFormRDDetailID(detailRow.FormRDApplyDetailID);
                detailRow.RemainAmount = detailRow.ApplyAmountRMB - detailRow.PaiedAmount;
                if (AmountRMB > detailRow.RemainAmount) {
                    PageUtility.ShowModelDlg(this.Page, "报销金额不能大于可用金额");
                    isValid = false;
                    break;
                }

                detailRow.AmountRMB = AmountRMB;
                this.ViewState["AmountRMBTotal"] = decimal.Parse(this.ViewState["AmountRMBTotal"].ToString()) + AmountRMB;
            }
        }
        if (isValid && StatusID == SystemEnums.FormStatus.Awaiting && decimal.Parse(this.ViewState["AmountRMBTotal"].ToString()) <= 0) {
            PageUtility.ShowModelDlg(this.Page, "报销金额必须大于零！");
            isValid = false;
        }
        return isValid;
    }

    protected void SaveRDPayment(SystemEnums.FormStatus StatusID) {
        this.FormRDBLL.FormDataSet = this.InnerDS;
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

            int FormRDApplyID = int.Parse(this.ViewState["FormRDApplyID"].ToString());
            FormDS.FormRow formRow = FormRDBLL.GetFormByID(FormRDApplyID)[0];
            if (formRow.StatusID != 2) {
                PageUtility.ShowModelDlg(this, "不能提交，对应的方案申请单不是审批完成状态！");
                return;
            }

            int InvoiceStatusID = int.Parse(this.InvoiceStatusDDL.SelectedValue);
            int PaymentTypeID = int.Parse(this.PaymentTypeDDL.SelectedValue);
            string Remark = this.RemarkCtl.Text;
            int? VATTypeID = null;
            if (this.VATTypeDDL.SelectedValue != "0") {
                VATTypeID = int.Parse(this.VATTypeDDL.SelectedValue);
            }

            //判断是否录入了发票
            if (StatusID == SystemEnums.FormStatus.Awaiting) {//提交时检查，保存草稿不检查
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
                    this.FormRDBLL.AddRDPayment(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.RDPayment, StatusID,
                            SystemEnums.PageType.RDPayment, FormRDApplyID, InvoiceStatusID, PaymentTypeID, this.UCFliePayment.AttachmentFileName, this.UCFliePayment.RealAttachmentFileName, Remark, int.Parse(this.ViewState["CostCenterID"].ToString()), VATTypeID, FormPOID);
                } else {
                    int FormID = (int)this.ViewState["ObjectId"];
                    this.FormRDBLL.UpdateRDPayment(FormID, SystemEnums.FormType.RDPayment, StatusID, InvoiceStatusID, PaymentTypeID, this.UCFliePayment.AttachmentFileName, this.UCFliePayment.RealAttachmentFileName, Remark, VATTypeID, FormPOID);
                }
                this.Page.Response.Redirect("~/Home.aspx");
            } catch (Exception ex) {
                PageUtility.DealWithException(this.Page, ex);
            }
        }
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        SaveRDPayment(SystemEnums.FormStatus.Draft);
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
        SaveRDPayment(SystemEnums.FormStatus.Awaiting);
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
        this.FormRDBLL.DeleteFormRDPaymentByFormID(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void gvPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormRDPaymentDetailRow row = (FormDS.FormRDPaymentDetailRow)drvDetail.Row;
                ApplyAmountTotal = decimal.Round((ApplyAmountTotal + row.ApplyAmount), 2);
                ApplyAmountRMBTotal = decimal.Round((ApplyAmountRMBTotal + row.ApplyAmountRMB), 2);
                PayedAmountTotal = decimal.Round((PayedAmountTotal + row.PaiedAmount), 2);
                AmountRMBTotal = decimal.Round((AmountRMBTotal + row.AmountRMB), 2);
                if (!row.IsAmountBeforeTaxNull()) {
                    AmountBeforeTaxTotal = decimal.Round((AmountBeforeTaxTotal + row.AmountBeforeTax), 2);
                }
                if (!row.IsTaxAmountNull()) {
                    TaxAmountTotal = decimal.Round((TaxAmountTotal + row.TaxAmount), 2);
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
        FormRDBLL bll = (FormRDBLL)e.ObjectInstance;
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

    public string GetApplyRemark(object FormRDApplyDetailID) {
        if (FormRDApplyDetailID.ToString() != string.Empty) {
            int id = Convert.ToInt32(FormRDApplyDetailID);
            FormDS.FormRDApplyDetailRow row = new FormRDBLL().GetFormRDApplyDetailByID(id);
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
        FormRDBLL bll = (FormRDBLL)e.ObjectInstance;
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

    public string GetVendorNameByID(object VendorID) {
        if (VendorID.ToString() != string.Empty) {
            int id = Convert.ToInt32(VendorID);
            return new MasterDataBLL().GetVendorByID(id).VendorName;
        } else {
            return null;
        }
    }
    protected void PaymentTypeDDL_SelectedIndexChanged(object sender, EventArgs e) {
        string InvoiceStatusIds = new MasterDataBLL().GetPaymentTypeById(int.Parse(this.PaymentTypeDDL.SelectedValue)).InvoiceStatusIds;
        if (InvoiceStatusIds != "0") {
            this.odsInvoiceStatus.SelectCommand = string.Format("SELECT [InvoiceStatusID], [Name] FROM [InvoiceStatus] where InvoiceStatusID in ({0})", InvoiceStatusIds);
        } else {
            this.odsInvoiceStatus.SelectCommand = string.Format("SELECT [InvoiceStatusID], [Name] FROM [InvoiceStatus] ");
        }
        this.odsInvoiceStatus.DataBind();
    }
}