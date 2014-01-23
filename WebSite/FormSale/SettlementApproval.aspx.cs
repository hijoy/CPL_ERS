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
using System.Text;

public partial class FormSale_SettlementApproval : BasePage {

    decimal OrderAmountTotal = 0;
    decimal ActualOrderAmountTotal = 0;
    decimal ApplyAmountTotal = 0;
    decimal ApplyAmountRMBTotal = 0;
    decimal AdvancedAmountTotal = 0;
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

    #region 页面初始化及事件处理

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);

            MasterDataBLL mdBLL = new MasterDataBLL();
            int formID = int.Parse(Request["ObjectId"]);
            this.ViewState["ObjectId"] = formID;
            FormDS.FormRow rowForm = this.FormSaleBLL.GetFormByID(formID)[0];
            FormDS.FormSaleSettlementRow rowFormSettlement = this.FormSaleBLL.GetFormSaleSettlementByID(formID);
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
            this.RemarkCtl.Text = rowFormSettlement.IsRemarkNull() ? "" : rowFormSettlement.Remark;
            if (!rowFormSettlement.IsAttachedFileNameNull())
                this.UCSettlementFile.AttachmentFileName = rowFormSettlement.AttachedFileName;
            if (!rowFormSettlement.IsRealAttachedFileNameNull())
                this.UCSettlementFile.RealAttachmentFileName = rowFormSettlement.RealAttachedFileName;

            this.CostCenterCtl.Text = CommonUtility.GetMAACostCenterFullName(rowForm.CostCenterID);
            this.PaymentTypeCtl.Text = mdBLL.GetPaymentTypeById(rowFormSettlement.PaymentTypeID).PaymentTypeName;

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                FormDS.FormRow rejectedForm = this.FormSaleBLL.GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/SettlementApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }
            //费用合计信息
            ApplyAmountRMBCtl.Text = rowFormSettlement.IsApplyAmountRMBNull() ? "" : rowFormSettlement.ApplyAmountRMB.ToString();
            ForecastOrderAmountCtl.Text = rowFormSettlement.IsForecastOrderAmountNull() ? "" : rowFormSettlement.ForecastOrderAmount.ToString();
            CostBenefitRateCtl.Text = rowFormSettlement.IsCostBenefitRateNull() ? "" : rowFormSettlement.CostBenefitRate.ToString();
            AmountRMBCtl.Text = rowFormSettlement.AmountRMB.ToString();
            ActualOrderAmountCtl.Text = rowFormSettlement.IsActualOrderAmountNull() ? "" : rowFormSettlement.ActualOrderAmount.ToString();
            ActualCostBenefitRateCtl.Text = rowFormSettlement.IsActualCostBenefitRateNull() ? "" : rowFormSettlement.ActualCostBenefitRate.ToString();

            //明细
            this.odsSKUDetails.SelectParameters["FormSaleSettlementID"].DefaultValue = rowFormSettlement.FormSaleSettlementID.ToString();
            this.odsExpenseDetails.SelectParameters["FormSaleSettlementID"].DefaultValue = rowFormSettlement.FormSaleSettlementID.ToString();
            //判断是Activity还是NoActivity，如果NoActivity那么需要隐藏
            if (rowForm.PageType == (int)SystemEnums.PageType.NoActivitySettlementApply) {
                this.FeeSumTR.Visible = false;
                this.SKUDiv.Visible = false;
                this.gvSKUDetails.Visible = false;
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

            //是否显示报销完成按钮
            this.CloseBtn.Visible = false;
            if ((!rowFormSettlement.IsClose) && rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted) {
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
            //查看报销单按钮
            if (rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted) {
                this.IsVisible = "";
            } else {
                this.IsVisible = "none";
            }

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
        FormDS.FormRow rowForm = this.FormSaleBLL.GetFormByID(int.Parse(this.ViewState["ObjectId"].ToString()))[0];
        if (rowForm.PageType == (int)SystemEnums.PageType.ActivitySettlementApply) {
            this.Response.Redirect("~/FormSale/ActivitySettlementApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
        } else {
            this.Response.Redirect("~/FormSale/NoActivitySettlementApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
        }
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void CloseBtn_Click(object sender, EventArgs e) {
        try {
            //关闭时检查费用期间是否不存，若不存在不允许关闭
            if (!new MasterDataBLL().PeriodSaleExists()) {
                PageUtility.ShowModelDlg(this, "关账期间，不允许关闭！");
                return;
            }

            int FormSaleSettlementID = int.Parse(this.ViewState["ObjectId"].ToString());
            string ProcessingPaymentNo = this.FormSaleBLL.GetProcessingPaymentNoByFormSaleSettlementID(FormSaleSettlementID);
            if (ProcessingPaymentNo != null) {
                PageUtility.ShowModelDlg(this.Page, "有没有处理完成的报销单，单号为:" + ProcessingPaymentNo);
                return;
            } else {
                this.FormSaleBLL.CloseFormSaleSettlement(FormSaleSettlementID);
            }
            if (this.Request["Source"] != null) {
                this.Response.Redirect(this.Request["Source"].ToString());
            } else {
                this.Response.Redirect("~/Home.aspx");
            }

        } catch (Exception exception) {
            PageUtility.DealWithException(this, exception);
        }
    }

    protected string GetShowWindowScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/FormSale/RefSalePaymentList.aspx?FormSaleSettlementID=" + this.ViewState["ObjectId"];
        script.Append(@"var url = '" + url + @"';window.open(url);");
        return script.ToString();
    }

    private string _isVisible = "";
    public string IsVisible {
        get {
            return _isVisible;
        }
        set {
            this._isVisible = value;
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

    protected void gvExpenseDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormSettlementExpenseDetailRow row = (FormDS.FormSettlementExpenseDetailRow)drvDetail.Row;
                ApplyAmountTotal = decimal.Round((ApplyAmountTotal + row.ApplyAmount), 2);
                ApplyAmountRMBTotal = decimal.Round((ApplyAmountRMBTotal + row.ApplyAmountRMB), 2);
                AdvancedAmountTotal = decimal.Round((AdvancedAmountTotal + row.AdvancedAmount), 2);
                AmountRMBTotal = decimal.Round((AmountRMBTotal + row.AmountRMB), 2);
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
                Label lblAdvancedAmountTotal = (Label)e.Row.FindControl("lblAdvancedAmountTotal");
                lblAdvancedAmountTotal.Text = AdvancedAmountTotal.ToString("N");
                Label lblSettlementAmountTotal = (Label)e.Row.FindControl("lblSettlementAmountTotal");
                lblSettlementAmountTotal.Text = AmountRMBTotal.ToString("N");
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

    public string GetDescriptionByID(object FormSettlementExpenseDetailID) {
        if (FormSettlementExpenseDetailID.ToString() != string.Empty) {
            int id = Convert.ToInt32(FormSettlementExpenseDetailID);
            FormDS.FormSettlementExpenseDetailRow row = this.FormSaleBLL.GetFormSettlementExpenseDetailByID(id);
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

}