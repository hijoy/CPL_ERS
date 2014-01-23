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

public partial class FormSale_NoActivitySettlementApply : BasePage {

    decimal ApplyAmountTotal = 0;
    decimal ApplyAmountRMBTotal = 0;
    decimal AdvancedAmountTotal = 0;

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
            this.StuffNoCtl.Text = stuffUser.IsStuffNoNull() ? "" : stuffUser.StuffNo;

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
                FormDS.FormRow rowForm = this.FormSaleBLL.GetFormByID(int.Parse(this.ViewState["ObjectId"].ToString()))[0];
                FormDS.FormSaleSettlementRow rowFormSettlement = this.FormSaleBLL.GetFormSaleSettlementByID(int.Parse(this.ViewState["ObjectId"].ToString()));
                //赋值
                this.ViewState["CustomerID"] = rowFormSettlement.CustomerID.ToString();
                this.ViewState["BrandID"] = rowFormSettlement.BrandID.ToString();
                this.ViewState["ExpenseSubCategoryID"] = rowFormSettlement.ExpenseSubCategoryID.ToString();
                this.ViewState["CurrencyID"] = rowFormSettlement.CurrencyID.ToString();
                this.ViewState["FormApplyNos"] = rowFormSettlement.FormApplyNos;
                this.ViewState["FormApplyIds"] = rowFormSettlement.FormApplyIds;
                this.ViewState["CostCenterID"] = rowForm.CostCenterID.ToString();

                if (!rowFormSettlement.IsRemarkNull()) {
                    this.RemarkCtl.Text = rowFormSettlement.Remark;
                }
                if (!rowFormSettlement.IsAttachedFileNameNull()) {
                    this.UCSettlementFile.AttachmentFileName = rowFormSettlement.AttachedFileName;
                }
                if (!rowFormSettlement.IsRealAttachedFileNameNull()) {
                    this.UCSettlementFile.RealAttachmentFileName = rowFormSettlement.RealAttachedFileName;
                }
                this.PaymentTypeDDL.SelectedValue = rowFormSettlement.PaymentTypeID.ToString();
                //处理明细
                new FormSettlementExpenseDetailTableAdapter().FillCurrentDataByFormSaleSettlementID(this.InnerDS.FormSettlementExpenseDetail, rowFormSettlement.FormSaleSettlementID);
            } else {
                this.DeleteBtn.Visible = false;
                if (Request["FormApplyIds"] != null) {
                    this.ViewState["FormApplyIds"] = Request["FormApplyIds"];
                } else {
                    this.Session["ErrorInfor"] = "没有选择申请单，请联系管理员";
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
                if (Request["CostCenterID"] != null) {
                    this.ViewState["CostCenterID"] = Request["CostCenterID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到成本中心，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["FormApplyNos"] != null) {
                    this.ViewState["FormApplyNos"] = Request["FormApplyNos"];
                } else {
                    this.Session["ErrorInfor"] = "没有选择申请单，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                //处理明细
                new FormSettlementExpenseDetailTableAdapter().FillByApplyIds(this.InnerDS.FormSettlementExpenseDetail, this.ViewState["FormApplyIds"].ToString());
            }
            MasterDataBLL mdBLL = new MasterDataBLL();
            MasterData.CustomerRow customer = mdBLL.GetCustomerById(int.Parse(this.ViewState["CustomerID"].ToString()))[0];
            this.CustomerNameCtl.Text = customer.CustomerName;
            ViewState["CustomerChannelID"] = customer.CustomerChannelID.ToString();
            this.CustomerChannelCtl.Text = mdBLL.GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelName;
            this.KATypeCtl.Text = customer.IsKaTypeNull() ? "" : customer.KaType;
            this.CustomerRegionCtl.Text = mdBLL.GetCustomerRegionById(customer.CustomerRegionID).CustomerRegionName;
            this.CityCtl.Text = customer.City;
            this.BrandCtl.Text = mdBLL.GetBrandById(int.Parse(this.ViewState["BrandID"].ToString()))[0].BrandName;
            MasterData.ExpenseSubCategoryRow rowExpenseSubCategory = mdBLL.GetExpenseSubCategoryById(int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString()));
            this.ExpenseCategoryCtl.Text = mdBLL.GetExpenseCategoryById(rowExpenseSubCategory.ExpenseCategoryID).ExpenseCategoryName;
            this.ExpenseSubCategoryCtl.Text = rowExpenseSubCategory.ExpenseSubCategoryName;
            this.CurrencyCtl.Text = mdBLL.GetCurrencyByID(int.Parse(this.ViewState["CurrencyID"].ToString())).CurrencyShortName;
        } 
    }

    protected override void OnLoadComplete(EventArgs e) {
        base.OnLoadComplete(e);
        decimal total = 0;
        if (this.gvExpenseDetails.Rows.Count > 0) {
            foreach (GridViewRow item in gvExpenseDetails.Rows) {
                if (item.RowType == DataControlRowType.DataRow) {
                    TextBox txtSettlementAmount = (TextBox)item.FindControl("txtSettlementAmount");
                    total += decimal.Parse(txtSettlementAmount.Text == string.Empty ? "0" : txtSettlementAmount.Text.ToString());
                }
            }
        }
        Label lblSettlementAmountTotal = (Label)this.gvExpenseDetails.FooterRow.FindControl("lblSettlementAmountTotal");
        lblSettlementAmountTotal.Text = total.ToString("N");
    }

    #endregion

    private bool FillDetail() {
        bool isValid = true;
        this.ViewState["SettlementAmountTotal"] = 0;
        //先填充费用明细
        foreach (GridViewRow row in this.gvExpenseDetails.Rows) {
            if (row.RowType == DataControlRowType.DataRow) {
                FormDS.FormSettlementExpenseDetailRow detailRow = this.InnerDS.FormSettlementExpenseDetail[row.RowIndex];
                TextBox txtSettlementAmount = (TextBox)row.FindControl("txtSettlementAmount");
                if (string.IsNullOrEmpty(txtSettlementAmount.Text.Trim())) {
                    txtSettlementAmount.Text = "0";
                }
                decimal SettlementAmount = decimal.Parse(txtSettlementAmount.Text.Trim());
                //if (SettlementAmount < 0) {
                //    PageUtility.ShowModelDlg(this.Page, "结案金额不能录入负数");
                //    isValid = false;
                //    break;
                //}
                if (SettlementAmount > detailRow.ApplyAmountRMB) {
                    PageUtility.ShowModelDlg(this.Page, "结案金额不能大于申请金额");
                    isValid = false;
                    break;
                }
                if (SettlementAmount < detailRow.AdvancedAmount) {
                    PageUtility.ShowModelDlg(this.Page, "结案金额不能小于预付款");
                    isValid = false;
                    break;
                }

                detailRow.AmountRMB = SettlementAmount;
                this.ViewState["SettlementAmountTotal"] = decimal.Parse(this.ViewState["SettlementAmountTotal"].ToString()) + SettlementAmount;

                TextBox txtSettlementRemark = (TextBox)row.FindControl("txtSettlementRemark");
                detailRow.Remark = txtSettlementRemark.Text;
            }
        }
        return isValid;
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {

        SaveFormApply(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {

        SaveFormApply(SystemEnums.FormStatus.Awaiting);
    }

    protected void SaveFormApply(SystemEnums.FormStatus StatusID) {

        this.FormSaleBLL.FormDataSet = this.InnerDS;
        if (this.PaymentTypeDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this.Page, "请选择支付方式");
            return;
        }
        if (FillDetail()) {
            if (StatusID == SystemEnums.FormStatus.Awaiting) {
                if (decimal.Parse(this.ViewState["SettlementAmountTotal"].ToString()) <= 0) {
                    PageUtility.ShowModelDlg(this.Page, "结案金额不能为零");
                    return;
                }
            }
            if (this.ViewState["FormApplyIds"] != null && this.PaymentTypeDDL.SelectedValue == ((int)SystemEnums.PaymentType.FreeGoods).ToString()) {
                if (this.ViewState["FormApplyIds"].ToString().IndexOf(',') > 0) {
                    PageUtility.ShowModelDlg(this.Page, "支付方式为货补时，只能有一张申请单！");
                    return;
                }
            }
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
            int CustomerID = int.Parse(this.ViewState["CustomerID"].ToString());
            int ExpenseSubCategoryID = int.Parse(this.ViewState["ExpenseSubCategoryID"].ToString());
            int CurrencyID = int.Parse(this.ViewState["CurrencyID"].ToString());
            int BrandID = int.Parse(this.ViewState["BrandID"].ToString());
            string AttachedFileName = this.UCSettlementFile.AttachmentFileName;
            string RealAttachedFileName = this.UCSettlementFile.RealAttachmentFileName;
            string Remark = this.RemarkCtl.Text;
            string FormApplyIds = this.ViewState["FormApplyIds"].ToString();
            string FormApplyNos = this.ViewState["FormApplyNos"].ToString();
            int PaymentTypeID = int.Parse(this.PaymentTypeDDL.SelectedValue);
            if (StatusID == SystemEnums.FormStatus.Awaiting) {//再检查一遍有没有结案过
                string SettledFormNo = this.FormSaleBLL.GetSettledFormNoByApplyFormIds(FormApplyIds);
                if (SettledFormNo != "") {
                    PageUtility.ShowModelDlg(this.Page, "申请单已经被结案过，结案单编号为：" + SettledFormNo);
                    return;
                }
            }

            try {
                if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                    this.FormSaleBLL.AddNoActivitySettlement(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.SaleSettlement, StatusID, CustomerID, BrandID,
                                ExpenseSubCategoryID, CurrencyID, AttachedFileName, RealAttachedFileName, Remark, FormApplyIds, FormApplyNos, int.Parse(this.ViewState["CostCenterID"].ToString()), PaymentTypeID);
                } else {
                    int FormID = (int)this.ViewState["ObjectId"];
                    this.FormSaleBLL.UpdateNoActivitySettlement(FormID, SystemEnums.FormType.SaleSettlement, StatusID, AttachedFileName, RealAttachedFileName, Remark, PaymentTypeID);
                }
                this.Page.Response.Redirect("~/Home.aspx");
            } catch (Exception ex) {
                PageUtility.DealWithException(this.Page, ex);
            }
        }
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
        this.FormSaleBLL.DeleteSaleSettlementByFormID(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void gvExpenseDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormSettlementExpenseDetailRow row = (FormDS.FormSettlementExpenseDetailRow)drvDetail.Row;
                ApplyAmountTotal = decimal.Round((ApplyAmountTotal + row.ApplyAmount), 2);
                ApplyAmountRMBTotal = decimal.Round((ApplyAmountRMBTotal + row.ApplyAmountRMB), 2);
                AdvancedAmountTotal = decimal.Round((AdvancedAmountTotal + row.AdvancedAmount), 2);

                TextBox txtSettlementAmount = (TextBox)e.Row.FindControl("txtSettlementAmount");
                txtSettlementAmount.Attributes.Add("onFocus", "MinusExpenseTotal(this)");
                txtSettlementAmount.Attributes.Add("onBlur", "PlusExpenseTotal(this)");

                HyperLink lblApplyFormNo = (HyperLink)e.Row.FindControl("lblApplyFormNo");
                lblApplyFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormSale/NoActivityApproval.aspx?ShowDialog=1&ObjectId=" + row.FormSaleApplyID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";

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
            }
        }
    }

    protected void odsSKUDetails_OnObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormSaleBLL bll = (FormSaleBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void odsExpenseDetails_OnObjectCreated(object sender, ObjectDataSourceEventArgs e) {
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


}