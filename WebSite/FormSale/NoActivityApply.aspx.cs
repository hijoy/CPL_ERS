﻿using System;
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

public partial class FormSale_NoActivityApply : BasePage {
    decimal Total = 0;
    decimal TotalRMB = 0;

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
            //this.StuffNoCtl.Text = stuffUser.IsStuffNoNull() ? "" : stuffUser.StuffNo;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();

            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());

            }
            //如果是草稿进行赋值
            MasterDataBLL MasterDataBLL = new MasterDataBLL();
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
                if (Request["PeriodSaleID"] != null) {
                    DateTime FPeriod = MasterDataBLL.GetPeriodSaleById(int.Parse(Request["PeriodSaleID"].ToString())).PeriodSale;
                    this.ViewState["FPeriod"] = MasterDataBLL.GetPeriodSaleById(int.Parse(Request["PeriodSaleID"].ToString())).PeriodSale.ToShortDateString();
                } else {
                    this.Session["ErrorInfor"] = "没有费用期间，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["CustomerID"] != null) {
                    this.ViewState["CustomerID"] = Request["CustomerID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到客户，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["BrandID"] != null) {
                    this.ViewState["BrandID"] = Request["BrandID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到Brand，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["ExpenseSubCategoryID"] != null) {
                    this.ViewState["ExpenseSubCategoryID"] = Request["ExpenseSubCategoryID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到费用小类，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["CurrencyID"] != null) {
                    this.ViewState["CurrencyID"] = Request["CurrencyID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到币种，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }

                this.PeriodCtl.Text = DateTime.Parse(this.ViewState["FPeriod"].ToString()).ToString("yyyy-MM");

                MasterData.CustomerRow customer = MasterDataBLL.GetCustomerById(int.Parse(this.ViewState["CustomerID"].ToString()))[0];
                this.txtCustomerNo.Text = customer.CustomerNo;
                this.CustomerNameCtl.Text = customer.CustomerName;
                ViewState["CustomerChannelID"] = customer.CustomerChannelID.ToString();
                this.CustomerChannelCtl.Text = MasterDataBLL.GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelName;
                this.KATypeCtl.Text = customer.IsKaTypeNull() ? "" : customer.KaType;
                this.CustomerRegionCtl.Text = MasterDataBLL.GetCustomerRegionById(customer.CustomerRegionID).CustomerRegionName;
                this.CityCtl.Text = customer.City;
                this.BrandCtl.Text = MasterDataBLL.GetBrandById(int.Parse(this.ViewState["BrandID"].ToString()))[0].BrandName;
                MasterData.ExpenseSubCategoryRow rowExpenseSubCategory = MasterDataBLL.GetExpenseSubCategoryById(int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString()));
                this.ExpenseCategoryCtl.Text = MasterDataBLL.GetExpenseCategoryById(rowExpenseSubCategory.ExpenseCategoryID).ExpenseCategoryName;
                this.ExpenseSubCategoryCtl.Text = rowExpenseSubCategory.ExpenseSubCategoryName;
                this.CurrencyCtl.Text = new CurrencyTableAdapter().GetDataByID(int.Parse(this.ViewState["CurrencyID"].ToString()))[0].CurrencyShortName;

            }
            this.ExpenseSubCategoryID.Value = this.ViewState["ExpenseSubCategoryID"].ToString();
            this.BrandID.Value = this.ViewState["BrandID"].ToString();
            this.ViewState["ExchangeRate"] = new MasterDataBLL().GetExchangeRateByPeriod(int.Parse(ViewState["CurrencyID"].ToString()), DateTime.Parse(ViewState["FPeriod"].ToString()));
            if (decimal.Parse(this.ViewState["ExchangeRate"].ToString()) == 0) {
                this.Session["ErrorInfor"] = "未找到汇率，请联系管理员";
                Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
            }
            this.ExchangeRateCtl.Text = this.ViewState["ExchangeRate"].ToString();

            //判断费用期间是否正确
            MasterDataBLL bll = new MasterDataBLL();
            if (!new MasterDataBLL().IsValidPeriodSale(DateTime.Parse(this.ViewState["FPeriod"].ToString()))) {
                this.SubmitBtn.Visible = false;
                PageUtility.ShowModelDlg(this, "不允许申请本月项目，请删除草稿并联系财务部!");
                return;
            }

            //预算信息
            this.ViewState["ExpenseCategoryID"] = new MasterDataBLL().GetExpenseSubCategoryById(int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString())).ExpenseCategoryID.ToString();
            decimal[] calculateAssistant = new decimal[6];
            calculateAssistant = new BudgetBLL().GetSalesBudgetByParameter(rowUserPosition.PositionId, DateTime.Parse(ViewState["FPeriod"].ToString()), int.Parse(this.ViewState["ExpenseCategoryID"].ToString()), int.Parse(ViewState["CustomerChannelID"].ToString()), int.Parse(this.ViewState["BrandID"].ToString()));
            this.TotalBudgetCtl.Text = calculateAssistant[0].ToString("N");
            this.ApprovedAmountCtl.Text = calculateAssistant[1].ToString("N");
            this.ApprovingAmountCtl.Text = calculateAssistant[2].ToString("N");
            this.CompletedAmountCtl.Text = calculateAssistant[3].ToString("N");
            this.ReimbursedAmountCtl.Text = calculateAssistant[4].ToString("N");
            this.RemainBudgetCtl.Text = calculateAssistant[5].ToString("N");

        }
        //查看预算权限
        int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ViewBudget, SystemEnums.OperateEnum.Manage);
        AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
        bool HasManageRight = new PositionRightBLL().CheckPositionRight(position.PositionId, opViewId);
        this.divBudgetInfo.Visible = HasManageRight;
        this.divBudgetInfoTitle.Visible = HasManageRight;

    }

    protected void OpenForm(int formID) {

        FormDS.FormRow rowForm = this.FormSaleBLL.GetFormByID(formID)[0];
        FormDS.FormSaleApplyRow rowFormApply = this.FormSaleBLL.GetFormSaleApplyByID(formID)[0];
        MasterDataBLL MasterDataBLL = new MasterDataBLL();
        //赋值
        this.ViewState["FPeriod"] = rowFormApply.FPeriod.ToShortDateString();
        this.PeriodCtl.Text = rowFormApply.FPeriod.ToString("yyyy-MM");
        this.ViewState["CustomerID"] = rowFormApply.CustomerID.ToString();
        this.ViewState["BrandID"] = rowFormApply.BrandID.ToString();
        this.ViewState["ExpenseSubCategoryID"] = rowFormApply.ExpenseSubCategoryID.ToString();
        this.ViewState["CurrencyID"] = rowFormApply.CurrencyID.ToString();

        MasterData.CustomerRow customer = MasterDataBLL.GetCustomerById(rowFormApply.CustomerID)[0];
        this.txtCustomerNo.Text = customer.CustomerNo;
        this.CustomerNameCtl.Text = customer.CustomerName;
        this.ViewState["CustomerChannelID"] = customer.CustomerChannelID.ToString();
        this.CustomerChannelCtl.Text = MasterDataBLL.GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelName;
        this.KATypeCtl.Text = customer.IsKaTypeNull() ? "" : customer.KaType;
        this.CustomerRegionCtl.Text = MasterDataBLL.GetCustomerRegionById(customer.CustomerRegionID).CustomerRegionName;
        this.CityCtl.Text = customer.City;
        this.BrandCtl.Text = MasterDataBLL.GetBrandById(rowFormApply.BrandID)[0].BrandName;
        MasterData.ExpenseSubCategoryRow rowExpenseSubCategory = MasterDataBLL.GetExpenseSubCategoryById(rowFormApply.ExpenseSubCategoryID);
        this.ExpenseCategoryCtl.Text = MasterDataBLL.GetExpenseCategoryById(rowExpenseSubCategory.ExpenseCategoryID).ExpenseCategoryName;
        this.ExpenseSubCategoryCtl.Text = rowExpenseSubCategory.ExpenseSubCategoryName;
        this.CurrencyCtl.Text = new CurrencyTableAdapter().GetDataByID(rowFormApply.CurrencyID)[0].CurrencyShortName;

        this.ShopNameCtl.Text = rowFormApply.IsShopNameNull() ? "" : rowFormApply.ShopName;
        this.ShopCountCtl.Text = rowFormApply.IsShopCountNull() ? "" : rowFormApply.ShopCount.ToString();
        this.ProjectNameCtl.Text = rowFormApply.IsProjectNameNull() ? "" : rowFormApply.ProjectName;
        if (!rowForm.IsCostCenterIDNull()) {
            this.CostCenterDDL.SelectedValue = rowForm.CostCenterID.ToString();
        }
        if (!rowFormApply.IsActivityBeginDateNull()) {
            this.UCActivityBegin.SelectedDate = rowFormApply.ActivityBeginDate.ToString("yyyy-MM-dd");
        }
        if (!rowFormApply.IsActivityEndDateNull()) {
            this.UCActivityEnd.SelectedDate = rowFormApply.ActivityEndDate.ToString("yyyy-MM-dd");
        }
        this.ProjectDescCtl.Text = rowFormApply.IsProjectDescNull() ? "" : rowFormApply.ProjectDesc;
        if (!rowFormApply.IsApplyFileNameNull())
            this.UCFileUpload.AttachmentFileName = rowFormApply.ApplyFileName;
        if (!rowFormApply.IsApplyRealFileNameNull())
            this.UCFileUpload.RealAttachmentFileName = rowFormApply.ApplyRealFileName;

        // 打开明细表
        new FormSaleExpenseDetailTableAdapter().FillByFormSaleApplyID(this.InnerDS.FormSaleExpenseDetail, formID);
    }

    #endregion

    protected void CancelBtn_Click(object sender, EventArgs e) {
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.FormSaleBLL.DeleteSaleActivityApplyByFormID(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveFormApply(SystemEnums.FormStatus StatusID) {

        decimal TotalBudget = 0;
        decimal ApprovedAmount = 0;
        decimal ApprovingAmount = 0;
        decimal CompletedAmount = 0;
        decimal ReimbursedAmount = 0;
        decimal RemainBudget = 0;

        if (StatusID == SystemEnums.FormStatus.Awaiting) {//提交时检查，保存草稿不检查

            decimal[] calculateAssistant = new decimal[6];
            calculateAssistant = new BudgetBLL().GetSalesBudgetByParameter((int)this.ViewState["PositionID"], DateTime.Parse(ViewState["FPeriod"].ToString()), int.Parse(this.ViewState["ExpenseCategoryID"].ToString()), int.Parse(ViewState["CustomerChannelID"].ToString()), int.Parse(this.ViewState["BrandID"].ToString()));
            if (calculateAssistant[5] < decimal.Parse(this.ViewState["TotalApplyAmount"].ToString())) {
                PageUtility.ShowModelDlg(this.Page, "本次申请金额超过可用预算，不能提交", "the amount of this application is more than remain budget");
                return;
            } else {
                TotalBudget = calculateAssistant[0];
                ApprovedAmount = calculateAssistant[1];
                ApprovingAmount = calculateAssistant[2];
                CompletedAmount = calculateAssistant[3];
                ReimbursedAmount = calculateAssistant[4];
                RemainBudget = calculateAssistant[5];
            }
        }

        this.FormSaleBLL.FormDataSet = this.InnerDS;
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
        DateTime FPeriod = DateTime.Parse(this.ViewState["FPeriod"].ToString());
        int CustomerID = int.Parse(this.ViewState["CustomerID"].ToString());
        int ExpenseSubCategoryID = int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString());
        int CurrencyID = int.Parse(this.ViewState["CurrencyID"].ToString());
        string ShopName = this.ShopNameCtl.Text;
        int? ShopCount = null;
        if (this.ShopCountCtl.Text != string.Empty) {
            ShopCount = int.Parse(this.ShopCountCtl.Text);
        }
        string ProjectName = this.ProjectNameCtl.Text;
        string ProjectDesc = this.ProjectDescCtl.Text;
        string ApplyFileName = this.UCFileUpload.AttachmentFileName;
        string ApplyRealFileName = this.UCFileUpload.RealAttachmentFileName;

        DateTime? ActivityBeginDate = null;
        DateTime? ActivityEndDate = null;

        if (this.UCActivityBegin.SelectedDate != string.Empty) {
            ActivityBeginDate = DateTime.Parse(this.UCActivityBegin.SelectedDate);
        }
        if (this.UCActivityEnd.SelectedDate != string.Empty) {
            ActivityEndDate = DateTime.Parse(this.UCActivityEnd.SelectedDate);
        }
        int? CostCenterID = null;
        if (this.CostCenterDDL.SelectedValue != "0") {
            CostCenterID = int.Parse(CostCenterDDL.SelectedValue);
        }
        int? DiscountTypeID = null;
        if (this.DiscountTypeDDL.SelectedValue != "0") {
            DiscountTypeID = int.Parse(DiscountTypeDDL.SelectedValue);
        }
        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.FormSaleBLL.AddSaleNoActivityApply(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.SaleApply, StatusID, FPeriod,
                        CustomerID, int.Parse(ViewState["BrandID"].ToString()), ExpenseSubCategoryID, CurrencyID, decimal.Parse(this.ViewState["ExchangeRate"].ToString()), ShopName, ShopCount, ProjectName, ProjectDesc,
                        ApplyFileName, ApplyRealFileName, ActivityBeginDate, ActivityEndDate, CostCenterID, DiscountTypeID, TotalBudget, ApprovedAmount, ApprovingAmount, CompletedAmount, ReimbursedAmount, RemainBudget);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.FormSaleBLL.UpdateSaleNoActivityApply(FormID, SystemEnums.FormType.SaleApply, StatusID, decimal.Parse(this.ViewState["ExchangeRate"].ToString()), ShopName, ShopCount, ProjectName, ProjectDesc,
                        ApplyFileName, ApplyRealFileName, ActivityBeginDate, ActivityEndDate, CostCenterID, DiscountTypeID, TotalBudget, ApprovedAmount, ApprovingAmount, CompletedAmount, ReimbursedAmount, RemainBudget);
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    public bool IsSubmitValid() {

        if (this.ShopNameCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入门店名称!", "please key in the name of shops");
            return false;
        }

        if (this.ShopCountCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入门店数量!", "please key in the number of shops");
            return false;
        } else {
            int ShopCount = 0;
            if (!int.TryParse(this.ShopCountCtl.Text, out ShopCount)) {
                PageUtility.ShowModelDlg(this.Page, "门店数量只能输入数字!", "Please Input Number");
            }
        }

        if (this.ProjectNameCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入方案名称!", "please key in project name");
            return false;
        }

        if (UCActivityBegin.SelectedDate == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入活动开始日期!", "please key in the begin date of activity");
            return false;
        }
        if (UCActivityEnd.SelectedDate == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入活动截止日期!", "please key in the end date of activity");
            return false;
        }

        DateTime activityBegin = DateTime.Parse(UCActivityBegin.SelectedDate);
        DateTime activityEnd = DateTime.Parse(UCActivityEnd.SelectedDate);
        if (activityBegin > activityEnd) {
            PageUtility.ShowModelDlg(this.Page, "活动结束日期必须大于开始日期!", "begin date should be earlier than end date");
            return false;
        }

        if (this.gvDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入费用明细信息", "please key in detail info");
            return false;
        }

        if (this.CostCenterDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this.Page, "请选择成本中心!", "please select profit center");
            return false;
        }

        if (decimal.Parse(this.ViewState["TotalApplyAmount"].ToString()) <= 0) {
            PageUtility.ShowModelDlg(this.Page, "申请费用必须大于零!", "amount should be more than zero");
            return false;
        }

        return true;
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {

        SaveFormApply(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {

        if (!IsSubmitValid())
            return;
        SaveFormApply(SystemEnums.FormStatus.Awaiting);
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormSaleExpenseDetailRow row = (FormDS.FormSaleExpenseDetailRow)drvDetail.Row;
                Total = decimal.Round((Total + row.Amount), 2);
                TotalRMB = decimal.Round((TotalRMB + row.AmountRMB), 2);
            }
        }

        this.ViewState["TotalApplyAmount"] = TotalRMB;

        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Text = Total.ToString("N");
                Label lblTotalRMB = (Label)e.Row.FindControl("lblTotalRMB");
                lblTotalRMB.Text = TotalRMB.ToString("N");
            }
        }

    }

    protected void odsDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {

        TextBox newAmountCtl = (TextBox)this.fvDetails.FindControl("newAmountCtl");
        decimal newAmount = decimal.Parse(newAmountCtl.Text);
        e.InputParameters["AmountRMB"] = decimal.Round(newAmount * decimal.Parse(this.ViewState["ExchangeRate"].ToString()), 2);
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormSaleApplyID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }
    }

    protected void odsDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        TextBox txtAmount = (TextBox)this.gvDetails.Rows[this.gvDetails.EditIndex].FindControl("txtAmount");
        decimal Amount = decimal.Parse(txtAmount.Text);
        e.InputParameters["AmountRMB"] = decimal.Round(Amount * decimal.Parse(this.ViewState["ExchangeRate"].ToString()), 2);
    }

    protected void odsDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormSaleBLL bll = (FormSaleBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    public string GetProductNameByID(object skuID) {
        if (skuID.ToString() != string.Empty) {
            int id = Convert.ToInt32(skuID);
            MasterData.SKURow sku = new SKUTableAdapter().GetDataByID(id)[0];
            return sku.SKUName + "-" + sku.SKUNo;
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

}