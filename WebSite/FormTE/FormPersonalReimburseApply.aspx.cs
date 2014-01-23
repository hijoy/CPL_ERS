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
using System.Text.RegularExpressions;

public partial class Form_FormPersonalReimburseApply : BasePage {
    decimal TotalFee = 0;
    decimal TotalRMBFee = 0;

    private FormTEBLL _FormTEBLL;
    protected FormTEBLL FormTEBLL {
        get {
            if (this._FormTEBLL == null) {
                this._FormTEBLL = new FormTEBLL();
            }
            return this._FormTEBLL;
        }
    }

    private FormDS m_InnerDS;
    public FormDS InnerDS {
        get {
            if (this.ViewState["InnerDS"] == null) {
                this.ViewState["InnerDS"] = new FormDS();
            }
            return (FormDS)this.ViewState["InnerDS"];
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);

        if (!this.IsPostBack) {
            String title = GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);
            this.Page.Title = title;

            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow UserPosition = (AuthorizationDS.PositionRow)Session["Position"];

            if (new StuffUserBLL().GetCostCenterIDByPositionID(UserPosition.PositionId) == 0) {
                this.Session["ErrorInfor"] = "未找到成本中心，请联系管理员";
                Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
            }
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = UserPosition.PositionId;
            this.ViewState["StaffLevelID"] = stuffUser.StaffLevelID;
            this.StuffNameCtl.Text = CommonUtility.GetStaffFullName(stuffUser);
            this.PositionNameCtl.Text = UserPosition.PositionName;
            this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(UserPosition.OrganizationUnitId).OrganizationUnitName;
            this.ViewState["DepartmentID"] = UserPosition.OrganizationUnitId;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();
            this.StuffNoCtl.Text = stuffUser.IsStuffNoNull() ? "" : stuffUser.StuffNo;

            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());
            }

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
            }
        }
    }

    protected void OpenForm(int formID) {
        FormDS.FormRow rowForm = this.FormTEBLL.GetFormByID(formID)[0];
        FormDS.FormPersonalReimburseRow rowFormPersonalReimburse = this.FormTEBLL.GetFormPersonalReimburseByID(formID)[0];
        //赋值
        PeriodDDL.DataBind();
        if (!rowFormPersonalReimburse.IsPeriodNull()) {
            ListItem item = this.PeriodDDL.Items.FindByText(rowFormPersonalReimburse.Period.ToString("yyyy-MM"));
            if (item != null) {
                this.PeriodDDL.SelectedValue = item.Value;
            }
        }
        if (!rowFormPersonalReimburse.IsRemarkNull()) {
            this.RemarkCtl.Text = rowFormPersonalReimburse.Remark;
        }
        if (!rowFormPersonalReimburse.IsAttachedFileNameNull())
            this.UCFileUpload.AttachmentFileName = rowFormPersonalReimburse.AttachedFileName;
        if (!rowFormPersonalReimburse.IsRealAttachedFileNameNull())
            this.UCFileUpload.RealAttachmentFileName = rowFormPersonalReimburse.RealAttachedFileName;

        // 打开明细表
        FormPersonalReimburseDetailTableAdapter taDetail = new FormPersonalReimburseDetailTableAdapter();
        taDetail.FillByFormPersonalReimburseID(this.InnerDS.FormPersonalReimburseDetail, formID);
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
        this.FormTEBLL.DeleteFormPersonalReimburse(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        SaveFormPersonal(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {

        if (!IsSubmitValid())
            return;
        SaveFormPersonal(SystemEnums.FormStatus.Awaiting);
    }

    public bool IsSubmitValid() {
        if (this.gvPersonalReimburseDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入明细项","please key in the detail info");
            return false;
        }
        if (string.IsNullOrEmpty(RemarkCtl.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请输入备注", "please key in remark");
            return false;
        }
        return true;
    }

    protected void SaveFormPersonal(SystemEnums.FormStatus StatusID) {
        if (this.PeriodDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this.Page, "请选择费用期间!","please select period");
            return;
        }
        decimal TotalBudget = 0;
        decimal ApprovedAmount = 0;
        decimal ApprovingAmount = 0;
        decimal RemainAmount = 0;
        DateTime Period = DateTime.Parse(this.PeriodDDL.SelectedItem.Text.ToString() + "-1");
        if (StatusID == SystemEnums.FormStatus.Awaiting) {
            decimal[] budgetInfo = new BudgetBLL().GetManagingBudgetByParameter((int)this.ViewState["PositionID"], Period,(int)SystemEnums.PurchaseBudgetType.Non_Capital);
            TotalBudget = budgetInfo[0];
            ApprovedAmount = budgetInfo[1];
            ApprovingAmount = budgetInfo[2];
            RemainAmount = budgetInfo[5];
            if (decimal.Parse(this.ViewState["TotalApplyAmount"].ToString()) > budgetInfo[5]) {
                PageUtility.ShowModelDlg(this.Page, "本次申请金额超过可用预算，不能提交", "the amount of this application is more than remain budget");
                return;
            }            
        }
        this.FormTEBLL.FormDataSet = this.InnerDS;
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

        string Remark = this.RemarkCtl.Text;
        string AttachedFileName = this.UCFileUpload.AttachmentFileName;
        string RealAttachedFileName = this.UCFileUpload.RealAttachmentFileName;

        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.FormTEBLL.AddFormPersonalReimburse(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID,
                    SystemEnums.FormType.PersonalReimburseApply, StatusID, Period, Remark, TotalBudget, ApprovedAmount, ApprovingAmount, RemainAmount, AttachedFileName, RealAttachedFileName);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.FormTEBLL.UpdateFormPersonalReimburse(FormID, StatusID, SystemEnums.FormType.PersonalReimburseApply, Period, Remark, TotalBudget, ApprovedAmount, ApprovingAmount, RemainAmount, AttachedFileName, RealAttachedFileName);
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    #region 数据绑定及事件

    protected void odsPersonalReimburseDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        TextBox txtExchangeRate = (TextBox)this.fvPersonalReimburseDetails.FindControl("txtExchangeRate");
        e.InputParameters["ExchangeRate"] = txtExchangeRate.Text;
    }

    protected void odsPersonalReimburseDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormTEBLL bll = (FormTEBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void gvPersonalReimburseDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormPersonalReimburseDetailRow row = (FormDS.FormPersonalReimburseDetailRow)drvDetail.Row;
                TotalRMBFee = decimal.Round((TotalRMBFee + row.RMB), 2);
                TotalFee = decimal.Round((TotalFee + row.ApplyAmount), 2);
                
                //超费用标准          
                Label lblRMB = (Label)e.Row.FindControl("lblRMB");
                decimal limit = new MasterDataBLL().GetLimitForOverStandard(0, int.Parse(ViewState["StaffLevelID"].ToString()), row.ManageExpenseItemID, row.RMB);
                if (limit!=0) {
                    lblRMB.Attributes.Add("title", PageUtility.TransferLanguage(this.Page, "超过费用标准 ￥"+limit, "Over Cost Standard ￥"+limit));
                    lblRMB.ForeColor = System.Drawing.Color.Red;
                }

            }
        }
        this.ViewState["TotalApplyAmount"] = TotalRMBFee;
        if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit) {
            TextBox txtApplyAmount = (TextBox)e.Row.FindControl("txtApplyAmount");
            TextBox txtExchangeRate = (TextBox)e.Row.FindControl("txtExchangeRate");
            TextBox txtRMB = (TextBox)e.Row.FindControl("txtRMB");
            txtApplyAmount.Attributes.Add("onchange", "ParameterChanged('" + txtExchangeRate.ClientID + "','" + txtApplyAmount.ClientID + "','" + txtRMB.ClientID + "')");
            txtExchangeRate.Attributes.Add("onchange", "ParameterChanged('" + txtExchangeRate.ClientID + "','" + txtApplyAmount.ClientID + "','" + txtRMB.ClientID + "')");
        }
        if (e.Row.RowType == DataControlRowType.Footer) {
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");
            lblTotal.Text = TotalFee.ToString("N");
            Label lblTotalRMB = (Label)e.Row.FindControl("lblTotalRMB");
            lblTotalRMB.Text = TotalRMBFee.ToString("N");
        }
    }

    public string GetManageExpenseItemNameByID(object ID) {
        return new MasterDataBLL().GetManageExpenseItemById((int)ID).ManageExpenseItemName;
    }

    public string GetCurrencyByID(object ID) {
        return new MasterDataBLL().GetCurrencyByID((int)ID).CurrencyFullName;
    }

    protected void fvPersonalReimburseDetails_DataBound(object sender, EventArgs e) {
        //自动计算
        TextBox txtApplyAmount = (TextBox)this.fvPersonalReimburseDetails.FindControl("txtApplyAmount");
        TextBox txtExchangeRate = (TextBox)this.fvPersonalReimburseDetails.FindControl("txtExchangeRate");
        TextBox txtRMB = (TextBox)this.fvPersonalReimburseDetails.FindControl("txtRMB");
        txtApplyAmount.Attributes.Add("onchange", "ParameterChanged('" + txtExchangeRate.ClientID + "','" + txtApplyAmount.ClientID + "','" + txtRMB.ClientID + "')");
        txtExchangeRate.Attributes.Add("onchange", "ParameterChanged('" + txtExchangeRate.ClientID + "','" + txtApplyAmount.ClientID + "','" + txtRMB.ClientID + "')");
        txtExchangeRate.Text = "1.0000";
    }

    #endregion


}