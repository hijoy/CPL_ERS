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

public partial class FormRD_RDApply : BasePage {
    decimal Total = 0;
    decimal TotalRMB = 0;

    private FormRDBLL _FormRDApplyBLL;
    protected FormRDBLL FormRDApplyBLL {
        get {
            if (this._FormRDApplyBLL == null) {
                this._FormRDApplyBLL = new FormRDBLL();
            }
            return this._FormRDApplyBLL;
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
            this.StuffNoCtl.Text = stuffUser.IsStuffNoNull() ? "" : stuffUser.StuffNo;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();

            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());
            }
            //如果是草稿进行赋值
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
                    DateTime FPeriod = new MasterDataBLL().GetPeriodSaleById(int.Parse(Request["PeriodSaleID"].ToString())).PeriodSale;
                    this.ViewState["FPeriod"] = new MasterDataBLL().GetPeriodSaleById(int.Parse(Request["PeriodSaleID"].ToString())).PeriodSale.ToShortDateString();
                } else {
                    this.Session["ErrorInfor"] = "没有费用期间，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["BrandID"] != null) {
                    this.ViewState["BrandID"] = Request["BrandID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到Brand，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["CustomerChannelID"] != null) {
                    this.ViewState["CustomerChannelID"] = Request["CustomerChannelID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到Brand，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["CurrencyID"] != null) {
                    this.ViewState["CurrencyID"] = Request["CurrencyID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到币种，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["ExpenseSubCategoryID"] != null) {
                    this.ViewState["ExpenseSubCategoryID"] = Request["ExpenseSubCategoryID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到费用大类，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }

                this.PeriodCtl.Text = DateTime.Parse(this.ViewState["FPeriod"].ToString()).ToString("yyyy-MM");
                this.CustomerChannelCtl.Text = new CustomerChannelTableAdapter().GetDataByID(int.Parse(ViewState["CustomerChannelID"].ToString()))[0].CustomerChannelName;
                this.BrandCtl.Text = new BrandTableAdapter().GetDataByID(int.Parse(this.ViewState["BrandID"].ToString()))[0].BrandName;
                this.CurrencyCtl.Text = new CurrencyTableAdapter().GetDataByID(int.Parse(this.ViewState["CurrencyID"].ToString()))[0].CurrencyShortName;
                this.ExpenseSubCategoryCtl.Text = new MasterDataBLL().GetExpenseSubCategoryById(int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString())).ExpenseSubCategoryName;
            }
            this.hdnBrandID.Value = this.ViewState["BrandID"].ToString();
            this.ExpenseSubCategoryID.Value = this.ViewState["ExpenseSubCategoryID"].ToString();
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
            decimal[] calculateAssistant = new decimal[6];
            calculateAssistant = new BudgetBLL().GetRDBudgetByParameter(rowUserPosition.PositionId, DateTime.Parse(ViewState["FPeriod"].ToString()), int.Parse(ViewState["CustomerChannelID"].ToString()), int.Parse(this.ViewState["BrandID"].ToString()), int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString()));
            this.TotalBudgetCtl.Text = calculateAssistant[0].ToString("N");
            this.ApprovedAmountCtl.Text = calculateAssistant[1].ToString("N");
            this.ApprovingAmountCtl.Text = calculateAssistant[2].ToString("N");
            this.ReimbursedAmountCtl.Text = calculateAssistant[3].ToString("N");
            this.RemainBudgetCtl.Text = calculateAssistant[4].ToString("N");
        }
        //隐藏预算信息,若需要则隐藏
    }

    protected void OpenForm(int formID) {

        FormDS.FormRow rowForm = this.FormRDApplyBLL.GetFormByID(formID)[0];
        FormDS.FormRDApplyRow rowFormApply = this.FormRDApplyBLL.GetFormRDApplyByID(formID)[0];
        //赋值
        this.ViewState["FPeriod"] = rowFormApply.FPeriod.ToShortDateString();
        this.PeriodCtl.Text = rowFormApply.FPeriod.ToString("yyyy-MM");
        this.ViewState["BrandID"] = rowFormApply.BrandID.ToString();
        this.ViewState["CurrencyID"] = rowFormApply.CurrencyID.ToString();
        this.ViewState["CustomerChannelID"] = rowFormApply.CustomerChannelID;
        this.ViewState["ExpenseSubCategoryID"] = rowFormApply.ExpenseSubCategoryID;
        this.CustomerChannelCtl.Text = new CustomerChannelTableAdapter().GetDataByID(rowFormApply.CustomerChannelID)[0].CustomerChannelName;
        this.BrandCtl.Text = new BrandTableAdapter().GetDataByID(rowFormApply.BrandID)[0].BrandName;
        this.CurrencyCtl.Text = new CurrencyTableAdapter().GetDataByID(rowFormApply.CurrencyID)[0].CurrencyShortName;
        this.ExpenseSubCategoryCtl.Text = new MasterDataBLL().GetExpenseSubCategoryById(rowFormApply.ExpenseSubCategoryID).ExpenseSubCategoryName;
        if (!rowFormApply.IsActivityBeginDateNull()) {
            this.UCActivityBegin.SelectedDate = rowFormApply.ActivityBeginDate.ToString("yyyy-MM-dd");
        }
        if (!rowFormApply.IsActivityEndDateNull()) {
            this.UCActivityEnd.SelectedDate = rowFormApply.ActivityEndDate.ToString("yyyy-MM-dd");
        }
        this.ProjectDescCtl.Text = rowFormApply.IsProjectDescNull() ? "" : rowFormApply.ProjectDesc;
        if (!rowFormApply.IsApplyFileNameNull()) {
            this.UCFileUpload.AttachmentFileName = rowFormApply.ApplyFileName;
        }
        if (!rowFormApply.IsApplyRealFileNameNull()) {
            this.UCFileUpload.RealAttachmentFileName = rowFormApply.ApplyRealFileName;
        }
        if (!rowFormApply.IsProjectNameNull()) {
            this.txtProjectName.Text = rowFormApply.ProjectName;
        }
        if (!rowForm.IsCostCenterIDNull()) {
            this.CostCenterDDL.SelectedValue = rowForm.CostCenterID.ToString();
        }

        // 打开明细表
        new FormRDApplyDetailTableAdapter().FillRDApplyID(this.InnerDS.FormRDApplyDetail, formID);
    }

    #endregion

    protected void CancelBtn_Click(object sender, EventArgs e) {
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.FormRDApplyBLL.DeleteRDApplyByFormID(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveFormApply(SystemEnums.FormStatus StatusID) {

        decimal TotalBudget = 0;
        decimal ApprovedAmount = 0;
        decimal ApprovingAmount = 0;
        decimal ReimbursedAmount = 0;
        decimal RemainBudget = 0;

        if (StatusID == SystemEnums.FormStatus.Awaiting) {//提交时检查，保存草稿不检查
            decimal[] calculateAssistant = new decimal[5];
            calculateAssistant = new BudgetBLL().GetRDBudgetByParameter((int)this.ViewState["PositionID"], DateTime.Parse(ViewState["FPeriod"].ToString()), int.Parse(ViewState["CustomerChannelID"].ToString()), int.Parse(this.ViewState["BrandID"].ToString()), int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString()));
            if (calculateAssistant[4] < decimal.Parse(this.ViewState["TotalApplyAmount"].ToString())) {
                PageUtility.ShowModelDlg(this.Page, "本次申请金额超过可用预算，不能提交", "the amount of this application is more than remain budget");
                return;
            } else {
                TotalBudget = calculateAssistant[0];
                ApprovedAmount = calculateAssistant[1];
                ApprovingAmount = calculateAssistant[2];
                ReimbursedAmount = calculateAssistant[3];
                RemainBudget = calculateAssistant[4];
            }
        }

        this.FormRDApplyBLL.FormDataSet = this.InnerDS;

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

        int CurrencyID = int.Parse(this.ViewState["CurrencyID"].ToString());
        string ProjectName = this.txtProjectName.Text;
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

        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.FormRDApplyBLL.AddRDApply(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.RDApply, StatusID, FPeriod,
                         int.Parse(ViewState["BrandID"].ToString()), int.Parse(ViewState["CustomerChannelID"].ToString()), CurrencyID, decimal.Parse(this.ViewState["ExchangeRate"].ToString()), ProjectName, ProjectDesc,
                        ApplyFileName, ApplyRealFileName, ActivityBeginDate, ActivityEndDate, CostCenterID, int.Parse(ViewState["ExpenseSubCategoryID"].ToString()), TotalBudget, ApprovedAmount, ApprovingAmount, ReimbursedAmount, RemainBudget);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.FormRDApplyBLL.UpdateRDApply(FormID, SystemEnums.FormType.RDApply, StatusID, decimal.Parse(this.ViewState["ExchangeRate"].ToString()), ProjectName, ProjectDesc,
                        ApplyFileName, ApplyRealFileName, ActivityBeginDate, ActivityEndDate, CostCenterID, TotalBudget, ApprovedAmount, ApprovingAmount, ReimbursedAmount, RemainBudget);
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    public bool IsSubmitValid() {
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
        if (this.txtProjectName.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入方案名称!", "please key in the project name");
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
                FormDS.FormRDApplyDetailRow row = (FormDS.FormRDApplyDetailRow)drvDetail.Row;
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
        UserControl UCVendor = (UserControl)this.fvDetails.FindControl("UCVendor");

        if (string.IsNullOrEmpty(((HiddenField)UCVendor.FindControl("VendorNameCtl")).Value)) {
            PageUtility.ShowModelDlg(this.Page, "请选择Vendor!", "Please select Vendor");
            e.Cancel = true;
            return;
        }
        decimal newAmount = decimal.Parse(newAmountCtl.Text);
        e.InputParameters["AmountRMB"] = decimal.Round(newAmount * decimal.Parse(this.ViewState["ExchangeRate"].ToString()), 2);
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormRDApplyID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }
    }

    protected void odsDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        TextBox txtAmount = (TextBox)this.gvDetails.Rows[this.gvDetails.EditIndex].FindControl("txtAmount");
        decimal Amount = decimal.Parse(txtAmount.Text);
        e.InputParameters["AmountRMB"] = decimal.Round(Amount * decimal.Parse(this.ViewState["ExchangeRate"].ToString()), 2);
    }

    protected void odsDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormRDBLL bll = (FormRDBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    public string GetSKUNameByID(object skuID) {
        if (skuID.ToString() != string.Empty) {
            int id = Convert.ToInt32(skuID);
            return new SKUTableAdapter().GetDataByID(id)[0].SKUName;
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

    public string GetVendorNameByID(object VendorID) {
        if (VendorID.ToString() != string.Empty) {
            int id = Convert.ToInt32(VendorID);
            return new MasterDataBLL().GetVendorByID(id).VendorName;
        } else {
            return null;
        }
    }
}