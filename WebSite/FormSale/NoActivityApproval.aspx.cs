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

public partial class FormSale_NoActivityApproval : BasePage {

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
            FormDS.FormSaleApplyRow rowFormApply = this.FormSaleBLL.GetFormSaleApplyByID(formID)[0];
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

            this.PeriodCtl.Text = rowFormApply.FPeriod.ToString("yyyy-MM");

            MasterData.CustomerRow customer = mdBLL.GetCustomerById(rowFormApply.CustomerID)[0];
            this.txtCustomerCode.Text = customer.CustomerNo;
            this.CustomerNameCtl.Text = customer.CustomerName;
            this.CustomerChannelCtl.Text = mdBLL.GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelName;
            this.KATypeCtl.Text = customer.IsKaTypeNull() ? "" : customer.KaType;
            this.CustomerRegionCtl.Text = mdBLL.GetCustomerRegionById(customer.CustomerRegionID).CustomerRegionName;
            this.CityCtl.Text = customer.City;
            this.BrandCtl.Text = mdBLL.GetBrandById(rowFormApply.BrandID)[0].BrandName;
            MasterData.ExpenseSubCategoryRow rowExpenseSubCategory = mdBLL.GetExpenseSubCategoryById(rowFormApply.ExpenseSubCategoryID);
            this.ExpenseCategoryCtl.Text = mdBLL.GetExpenseCategoryById(rowExpenseSubCategory.ExpenseCategoryID).ExpenseCategoryName;
            this.ExpenseSubCategoryCtl.Text = rowExpenseSubCategory.ExpenseSubCategoryName;
            this.CurrencyCtl.Text = new CurrencyTableAdapter().GetDataByID(rowFormApply.CurrencyID)[0].CurrencyShortName;
            this.ExchangeRateCtl.Text = rowFormApply.ExchangeRate.ToString();
            this.ShopNameCtl.Text = rowFormApply.IsShopNameNull() ? "" : rowFormApply.ShopName;
            this.ShopCountCtl.Text = rowFormApply.IsShopCountNull() ? "" : rowFormApply.ShopCount.ToString();
            this.ProjectNameCtl.Text = rowFormApply.IsProjectNameNull() ? "" : rowFormApply.ProjectName;
            this.CostCenterCtl.Text = CommonUtility.GetMAACostCenterFullName(rowForm.CostCenterID);
            if (!rowFormApply.IsDiscountTypeIDNull()) {
                this.DiscountTypeCtl.Text = new MasterDataBLL().GetDiscountTypeById(rowFormApply.DiscountTypeID).DiscountTypeName;
            }

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                FormDS.FormRow rejectedForm = this.FormSaleBLL.GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/NoActivityApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }
            //结案单编号
            int SaleSettlementID = this.FormSaleBLL.GetValidSaleSettlementIDBySaleApplyID(formID);
            if (SaleSettlementID == 0) {
                this.hlSettlementFormNo.Text = "未结案";
            } else {
                FormDS.FormRow settledForm = this.FormSaleBLL.GetFormByID(SaleSettlementID)[0];
                this.hlSettlementFormNo.Text = settledForm.FormNo;
                this.hlSettlementFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/SettlementApproval.aspx?ShowDialog=1&ObjectId=" + settledForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }

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

            this.TotalBudgetCtl.Text = rowFormApply.TotalBudget.ToString("N");
            this.ApprovedAmountCtl.Text = rowFormApply.ApprovedAmount.ToString("N");
            this.ApprovingAmountCtl.Text = rowFormApply.ApprovingAmount.ToString("N");
            this.CompletedAmountCtl.Text = rowFormApply.CompletedAmount.ToString("N");
            this.ReimbursedAmountCtl.Text = rowFormApply.ReimbursedAmount.ToString("N");
            this.RemainBudgetCtl.Text = rowFormApply.RemainBudget.ToString("N");

            //明细
            this.odsDetails.SelectParameters["FormSaleApplyID"].DefaultValue = rowFormApply.FormSaleApplyID.ToString();

            //按钮控制
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

            //查看报销单按钮
            if (rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted) {
                this.IsVisible = "";
            } else {
                this.IsVisible = "none";
            }
        }

        //查看预算权限
        int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ViewBudget, SystemEnums.OperateEnum.Manage);
        AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
        bool HasManageRight = new PositionRightBLL().CheckPositionRight(position.PositionId, opViewId);
        this.divBudgetInfo.Visible = HasManageRight;
        this.divBudgetInfoTitle.Visible = HasManageRight;

        //流程控件赋值
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
                            this.cwfAppCheck.GetApproveOrReject(), this.cwfAppCheck.GetComments(), ProxyStuffName, CommonUtility.CheckPeriod(cwfAppCheck.FormID));
                FormDS.FormRow formRow = FormSaleBLL.GetFormByID(cwfAppCheck.FormID)[0];
                if (formRow.StatusID == 2) {
                    FormSaleBLL.GenerateSettlement(formRow.FormID);
                }
                if (this.Request["Source"] != null) {
                    this.Response.Redirect(this.Request["Source"].ToString());
                } else {
                    this.Response.Redirect("~/Home.aspx");
                }
            }
        } catch (ApplicationException ex) {
            PageUtility.DealWithException(this, ex);
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
        this.Response.Redirect("~/FormSale/NoActivityApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected string GetShowWindowScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/FormSale/RefSalePaymentList.aspx?FormSaleApplyID=" + this.ViewState["ObjectId"];
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

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormSaleExpenseDetailRow row = (FormDS.FormSaleExpenseDetailRow)drvDetail.Row;
                Total = decimal.Round((Total + row.Amount), 2);
                TotalRMB = decimal.Round((TotalRMB + row.AmountRMB), 2);
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Text = Total.ToString("N");
                Label lblTotalRMB = (Label)e.Row.FindControl("lblTotalRMB");
                lblTotalRMB.Text = TotalRMB.ToString("N");
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


}