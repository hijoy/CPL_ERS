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

public partial class FormSale_PaymentFreeGoodsApply : BasePage {

    decimal OrderAmountTotal = 0;
    decimal ActualOrderAmountTotal = 0;
    decimal ApplyAmountTotal = 0;
    decimal ApplyAmountRMBTotal = 0;
    decimal SettlementAmountTotal = 0;
    decimal PayedAmountTotal = 0;
    decimal FreeGoodsFeeTotal = 0;

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
                //赋值备注等
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

                new FormSalePaymentDetailTableAdapter().FillCurrentDataForNormal(this.InnerDS.FormSalePaymentDetail, rowFormPayment.FormSalePaymentID);
                new FormSalePaymentFreeGoodsTableAdapter().FillByFormSalePaymentID(this.InnerDS.FormSalePaymentFreeGoods, rowFormPayment.FormSalePaymentID);
            } else {
                this.DeleteBtn.Visible = false;
                FormSaleSettlementID = int.Parse(Request["FormSaleSettlementID"]);
                this.ViewState["FormSaleSettlementID"] = FormSaleSettlementID;
                new FormSalePaymentDetailTableAdapter().FillByFormSaleSettlementID(this.InnerDS.FormSalePaymentDetail, FormSaleSettlementID);
            }

            MasterDataBLL mdBLL = new MasterDataBLL();
            FormDS.FormSaleSettlementRow rowFormSettlement = this.FormSaleBLL.GetFormSaleSettlementByID(FormSaleSettlementID);
            FormDS.FormRow settlementForm = this.FormSaleBLL.GetFormByID(FormSaleSettlementID)[0];
            MasterData.CustomerRow customer = mdBLL.GetCustomerById(rowFormSettlement.CustomerID)[0];
            this.ViewState["CustomerID"] = customer.CustomerID;
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
        decimal total = 0;
        if (this.gvPaymentDetails.Rows.Count > 0) {
            foreach (GridViewRow item in gvPaymentDetails.Rows) {
                if (item.RowType == DataControlRowType.DataRow) {
                    TextBox txtAmountRMB = (TextBox)item.FindControl("txtAmountRMB");
                    total += decimal.Parse(txtAmountRMB.Text == string.Empty ? "0" : txtAmountRMB.Text.ToString());
                }
            }
        }
        Label lblAmountRMBTotal = (Label)this.gvPaymentDetails.FooterRow.FindControl("lblAmountRMBTotal");
        lblAmountRMBTotal.Text = total.ToString("N");
    }

    #endregion

    #region button

    private bool FillDetail(SystemEnums.FormStatus StatusID) {
        bool isValid = true;
        this.ViewState["AmountRMBTotal"] = 0;
        //先填充产品明细
        foreach (GridViewRow row in this.gvPaymentDetails.Rows) {
            if (row.RowType == DataControlRowType.DataRow) {
                FormDS.FormSalePaymentDetailRow detailRow = this.InnerDS.FormSalePaymentDetail[row.RowIndex];
                TextBox txtAmountRMB = (TextBox)row.FindControl("txtAmountRMB");
                if (string.IsNullOrEmpty(txtAmountRMB.Text.Trim())) {
                    txtAmountRMB.Text = "0";
                }
                decimal amountRMB = decimal.Parse(txtAmountRMB.Text.Trim());
                //if (amountRMB < 0) {
                //    PageUtility.ShowModelDlg(this.Page, "本次支付金额不能录入负数");
                //    isValid = false;
                //    break;
                //}
                //提交时再取一遍，防止漏洞
                detailRow.PayedAmount = this.FormSaleBLL.GetPayedAmountByFormSaleExpenseDetailID(detailRow.FormSaleExpenseDetailID);
                detailRow.RemainAmount = detailRow.SettlementAmount - detailRow.PayedAmount;
                if (amountRMB > detailRow.RemainAmount) {
                    PageUtility.ShowModelDlg(this.Page, "支付金额不能大于可用金额");
                    isValid = false;
                }

                detailRow.AmountRMB = amountRMB;
                this.ViewState["AmountRMBTotal"] = decimal.Parse(this.ViewState["AmountRMBTotal"].ToString()) + amountRMB;
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
            int InvoiceStatusID = (int)SystemEnums.InvoiceStatus.No;
            int PaymentTypeID = (int)SystemEnums.PaymentType.FreeGoods;
            string Remark = this.RemarkCtl.Text;
            string AttachedFileName = this.UCPaymentFile.AttachmentFileName;
            string RealAttachedFileName = this.UCPaymentFile.RealAttachmentFileName;
            int? VendorID = null;
            if (this.UCVendor.VendorID != string.Empty) {
                VendorID = int.Parse(this.UCVendor.VendorID);
            }

            if (StatusID == SystemEnums.FormStatus.Awaiting) {//提交时检查，保存草稿不检查
                //vendor
                if (this.UCVendor.VendorID == string.Empty) {
                    PageUtility.ShowModelDlg(this.Page, "请选择供应商!", "please select vendor");
                    return;
                }

                //判断是否录入了发货信息
                if (this.gvFreeGoods.Rows.Count == 0) {
                    PageUtility.ShowModelDlg(this.Page, "请录入货补信息!", "please key in free goods info");
                    return;
                } else {
                    decimal totalAmountRMB = decimal.Parse(this.ViewState["AmountRMBTotal"].ToString());
                    if (decimal.Parse(this.ViewState["FreeGoodsFeeTotal"].ToString()) > totalAmountRMB) {
                        PageUtility.ShowModelDlg(this.Page, "实物金额不得大于支付金额!", "the amount of free goods should not be more than the payment");
                        return;
                    }
                }
            }

            try {
                if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                    this.FormSaleBLL.AddPaymentFreeGoods(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.SalePayment, StatusID,
                            SystemEnums.PageType.PaymentFreeGoods, FormSaleSettlementID, InvoiceStatusID, PaymentTypeID, Remark, AttachedFileName, RealAttachedFileName, int.Parse(this.ViewState["CostCenterID"].ToString()), VendorID);
                } else {
                    int FormID = (int)this.ViewState["ObjectId"];
                    this.FormSaleBLL.UpdatePaymentFreeGoods(FormID, SystemEnums.FormType.SalePayment, StatusID, InvoiceStatusID, PaymentTypeID, Remark, AttachedFileName, RealAttachedFileName, VendorID);
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

    #endregion

    #region expense & SKU

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
                HyperLink lblApplyFormNo = (HyperLink)e.Row.FindControl("lblApplyFormNo");
                FormDS.FormRow rowApplyForm = this.FormSaleBLL.GetFormByID(row.FormSaleApplyID)[0];
                if (rowApplyForm.PageType == (int)SystemEnums.PageType.ActivityApply) {
                    lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/ActivityApproval.aspx?ShowDialog=1&ObjectId=" + row.FormSaleApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                } else {
                    lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/NoActivityApproval.aspx?ShowDialog=1&ObjectId=" + row.FormSaleApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                }
                TextBox txtAmountRMB = (TextBox)e.Row.FindControl("txtAmountRMB");
                txtAmountRMB.Attributes.Add("onFocus", "MinusExpenseTotal(this)");
                txtAmountRMB.Attributes.Add("onBlur", "PlusExpenseTotal(this)");

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

    #endregion

    #region Free Goods

    protected void odsFreeGoods_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormSaleBLL bll = (FormSaleBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void odsFreeGoods_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        UserControls_SKUControl NewUCSKU = (UserControls_SKUControl)this.fvFreeGoods.FindControl("NewUCSKU");
        MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById((int)this.ViewState["CustomerID"])[0];
        decimal MAAPrice = new MasterDataBLL().GetSKUPriceByParameter(int.Parse(NewUCSKU.SKUID), customer.CustomerTypeID, customer.CustomerChannelID);
        if (MAAPrice == 0) {
            PageUtility.ShowModelDlg(this.Page, "没有找到该产品价格，请联系管理员", "can't find the  price for this SKU");
            e.Cancel = true;
            return;
        } else {
            e.InputParameters["SKUID"] = NewUCSKU.SKUID;
            e.InputParameters["DeliveryPrice"] = MAAPrice;
        }
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormSalePaymentID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }
    }

    protected void odsFreeGoods_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        UserControls_SKUControl UCSKU = (UserControls_SKUControl)this.gvFreeGoods.Rows[this.gvFreeGoods.EditIndex].FindControl("UCSKU");
        MasterData.SKURow sku = new MasterDataBLL().GetSKUById(int.Parse(UCSKU.SKUID))[0];
        MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById((int)this.ViewState["CustomerID"])[0];
        decimal MAAPrice = new MasterDataBLL().GetSKUPriceByParameter(int.Parse(UCSKU.SKUID), customer.CustomerTypeID, customer.CustomerChannelID);
        if (MAAPrice == 0) {
            PageUtility.ShowModelDlg(this.Page, "没有找到该产品价格，请联系管理员", "can't find the  price for this SKU");
            e.Cancel = true;
            return;
        } else {
            e.InputParameters["SKUID"] = UCSKU.SKUID;
            e.InputParameters["DeliveryPrice"] = MAAPrice;
        }
    }

    protected void odsSKUDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormSaleBLL bll = (FormSaleBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void NewUCSKU_SelectedIndexChanged(object sender, EventArgs e) {
        UserControls_SKUControl NewUCSKU = (UserControls_SKUControl)this.fvFreeGoods.FindControl("NewUCSKU");
        TextBox NewPackPerCaseCtl = (TextBox)this.fvFreeGoods.FindControl("NewPackPerCaseCtl");
        TextBox NewDeliveryPriceCtl = (TextBox)this.fvFreeGoods.FindControl("NewDeliveryPriceCtl");
        TextBox NewQuantityCtl = (TextBox)this.fvFreeGoods.FindControl("NewQuantityCtl");
        TextBox NewAmountRMBCtl = (TextBox)this.fvFreeGoods.FindControl("NewAmountRMBCtl");
        if (NewUCSKU.SKUID != string.Empty) {
            MasterData.SKURow sku = new MasterDataBLL().GetSKUById(int.Parse(NewUCSKU.SKUID))[0];
            if (!sku.IsPackPerCaseNull()) {
                NewPackPerCaseCtl.Text = sku.PackPerCase;
            }
            MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById((int)this.ViewState["CustomerID"])[0];
            decimal MAAPrice = new MasterDataBLL().GetSKUPriceByParameter(int.Parse(NewUCSKU.SKUID), customer.CustomerTypeID, customer.CustomerChannelID);
            NewDeliveryPriceCtl.Text = MAAPrice.ToString("N");
            if (NewQuantityCtl.Text != string.Empty) {
                decimal quantity = decimal.Parse(NewQuantityCtl.Text);
                NewAmountRMBCtl.Text = decimal.Round(MAAPrice * quantity, 2).ToString();
            }
        } else {
            NewPackPerCaseCtl.Text = "";
            NewDeliveryPriceCtl.Text = "";
            NewAmountRMBCtl.Text = "";
        }
    }

    protected void UCSKU_SelectedIndexChanged(object sender, EventArgs e) {
        UserControls_SKUControl UCSKU = (UserControls_SKUControl)this.gvFreeGoods.Rows[gvFreeGoods.EditIndex].FindControl("UCSKU");
        TextBox PackPerCaseCtl = (TextBox)this.gvFreeGoods.Rows[gvFreeGoods.EditIndex].FindControl("PackPerCaseCtl");
        TextBox DeliveryPriceCtl = (TextBox)this.gvFreeGoods.Rows[gvFreeGoods.EditIndex].FindControl("DeliveryPriceCtl");
        TextBox QuantityCtl = (TextBox)this.gvFreeGoods.Rows[gvFreeGoods.EditIndex].FindControl("QuantityCtl");
        TextBox AmountRMBCtl = (TextBox)this.gvFreeGoods.Rows[gvFreeGoods.EditIndex].FindControl("AmountRMBCtl");

        if (UCSKU.SKUID != string.Empty) {
            MasterData.SKURow sku = new MasterDataBLL().GetSKUById(int.Parse(UCSKU.SKUID))[0];
            if (!sku.IsPackPerCaseNull()) {
                PackPerCaseCtl.Text = sku.PackPerCase;
            }
            MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById((int)this.ViewState["CustomerID"])[0];
            decimal MAAPrice = new MasterDataBLL().GetSKUPriceByParameter(int.Parse(UCSKU.SKUID), customer.CustomerTypeID, customer.CustomerChannelID);
            DeliveryPriceCtl.Text = MAAPrice.ToString("N");
            if (QuantityCtl.Text != string.Empty) {
                decimal quantity = decimal.Parse(QuantityCtl.Text);
                AmountRMBCtl.Text = decimal.Round(MAAPrice * quantity, 2).ToString();
            }
        } else {
            PackPerCaseCtl.Text = "";
            DeliveryPriceCtl.Text = "";
            AmountRMBCtl.Text = "";
        }
    }

    protected void gvFreeGoods_OnDataBound(object sender, EventArgs e) {

        if (this.gvFreeGoods.EditIndex >= 0) {
            TextBox DeliveryPriceCtl = (TextBox)this.gvFreeGoods.Rows[gvFreeGoods.EditIndex].FindControl("DeliveryPriceCtl");
            TextBox QuantityCtl = (TextBox)this.gvFreeGoods.Rows[gvFreeGoods.EditIndex].FindControl("QuantityCtl");
            TextBox AmountRMBCtl = (TextBox)this.gvFreeGoods.Rows[gvFreeGoods.EditIndex].FindControl("AmountRMBCtl");
            QuantityCtl.Attributes.Add("onchange", "ParameterChanged('" + DeliveryPriceCtl.ClientID + "','" + QuantityCtl.ClientID + "','" + AmountRMBCtl.ClientID + "')");
        }
    }

    protected void fvFreeGoods_OnDataBound(object sender, EventArgs e) {
        TextBox NewDeliveryPriceCtl = (TextBox)this.fvFreeGoods.FindControl("NewDeliveryPriceCtl");
        TextBox NewQuantityCtl = (TextBox)this.fvFreeGoods.FindControl("NewQuantityCtl");
        TextBox NewAmountRMBCtl = (TextBox)this.fvFreeGoods.FindControl("NewAmountRMBCtl");
        NewQuantityCtl.Attributes.Add("onchange", "ParameterChanged('" + NewDeliveryPriceCtl.ClientID + "','" + NewQuantityCtl.ClientID + "','" + NewAmountRMBCtl.ClientID + "')");
    }

    protected void gvFreeGoods_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormSalePaymentFreeGoodsRow row = (FormDS.FormSalePaymentFreeGoodsRow)drvDetail.Row;
                FreeGoodsFeeTotal = decimal.Round((FreeGoodsFeeTotal + row.AmountRMB), 2);
            }
        }

        this.ViewState["FreeGoodsFeeTotal"] = FreeGoodsFeeTotal;

        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label lblAmountRMBTotal = (Label)e.Row.FindControl("lblAmountRMBTotal");
                lblAmountRMBTotal.Text = FreeGoodsFeeTotal.ToString("N");
            }
        }

    }

    #endregion
}