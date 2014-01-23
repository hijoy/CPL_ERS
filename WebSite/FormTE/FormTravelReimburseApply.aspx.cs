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

public partial class Form_FormTravelReimburseApply : BasePage {

    decimal TotalFee = 0;

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
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
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
        FormDS.FormTravelReimburseRow rowFormTravelReimburse = this.FormTEBLL.GetFormTravelReimburseByID(formID)[0];
        //赋值
        PeriodDDL.DataBind();
        if (!rowFormTravelReimburse.IsPeriodNull()) {
            ListItem item = this.PeriodDDL.Items.FindByText(rowFormTravelReimburse.Period.ToString("yyyy-MM"));
            if (item != null) {
                this.PeriodDDL.SelectedValue = item.Value;
            }
        }

        if (!rowFormTravelReimburse.IsRemarkNull()) {
            this.RemarkCtl.Text = rowFormTravelReimburse.Remark;
        }
        if (!rowFormTravelReimburse.IsAttachedFileNameNull())
            this.UCFileUpload.AttachmentFileName = rowFormTravelReimburse.AttachedFileName;
        if (!rowFormTravelReimburse.IsRealAttachedFileNameNull())
            this.UCFileUpload.RealAttachmentFileName = rowFormTravelReimburse.RealAttachedFileName;

        // 打开明细表
        FormTravelReimburseDetailTableAdapter taDetail = new FormTravelReimburseDetailTableAdapter();
        taDetail.FillByFormTravelReimburseID(this.InnerDS.FormTravelReimburseDetail, formID);
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
        this.FormTEBLL.DeleteFormTravelReimburse(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {

        SaveFormTravel(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        if (!IsSubmitValid())
            return;
        SaveFormTravel(SystemEnums.FormStatus.Awaiting);
    }

    public bool IsSubmitValid() {
        if (this.gvTravelReimburseDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入明细项","please key in the detail info");
            return false;
        }
        if (string.IsNullOrEmpty(RemarkCtl.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请填写备注!", "please key in remark");
            return false;
        }
        return true;
    }

    protected void SaveFormTravel(SystemEnums.FormStatus StatusID) {
        if (this.PeriodDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this.Page, "请选择费用期间!");
            return;
        }
        decimal TotalBudget = 0;
        decimal ApprovedAmount = 0;
        decimal ApprovingAmount = 0;
        decimal RemainAmount = 0;
        DateTime Period = DateTime.Parse(this.PeriodDDL.SelectedItem.Text.ToString() + "-1");
        if (StatusID == SystemEnums.FormStatus.Awaiting) {
            decimal[] budgetInfo = new BudgetBLL().GetManagingBudgetByParameter((int)this.ViewState["PositionID"], Period, (int)SystemEnums.PurchaseBudgetType.Non_Capital);
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
                this.FormTEBLL.AddFormTravelReimburse(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID,
                    SystemEnums.FormType.TravelReimburseApply, StatusID, Period, Remark, TotalBudget, ApprovedAmount, ApprovingAmount, RemainAmount, AttachedFileName, RealAttachedFileName);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.FormTEBLL.UpdateFormTravelReimburse(FormID, StatusID, SystemEnums.FormType.TravelReimburseApply, Period, Remark, TotalBudget, ApprovedAmount, ApprovingAmount, RemainAmount, AttachedFileName, RealAttachedFileName);
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    #region 数据绑定及事件

    protected void odsTravelReimburseDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        DropDownList ddlManageExpenseItem = (DropDownList)this.fvTravelReimburseDetails.FindControl("ddlManageExpenseItem");
        if (ddlManageExpenseItem.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this.Page, "请选择费用项！","please select expense item");
            e.Cancel = true;
        }
    }

    protected void odsTravelReimburseDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormTEBLL bll = (FormTEBLL)e.ObjectInstance;
        bll.FormDataSet = this.InnerDS;
    }

    protected void gvTravelReimburseDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormTravelReimburseDetailRow row = (FormDS.FormTravelReimburseDetailRow)drvDetail.Row;
                TotalFee = decimal.Round((TotalFee + row.Cost), 2);
                Label lblUnitPrice = (Label)e.Row.FindControl("lblUnitPrice");
                //超费用标准
                int CityTypeID = new MasterDataBLL().GetCityById(row.CityID).CityTypeID;
                decimal limit = new MasterDataBLL().GetLimitForOverStandard(CityTypeID, int.Parse(ViewState["StaffLevelID"].ToString()), row.ManageExpenseItemID, row.UnitPrice * row.ExchangeRate);
                if (limit != 0) {
                    e.Row.Cells[6].Attributes.Add("title",PageUtility.TransferLanguage(this.Page, "超过费用标准 ￥"+ limit,"Over Cost Standard ￥"+limit));
                    lblUnitPrice.ForeColor = System.Drawing.Color.Red;
                }

                //费用项是机票
                if (new MasterDataBLL().GetManageExpenseItemById(row.ManageExpenseItemID).IsTicket == true) {
                    //如果是员工支付
                    if (row.PayMan == 0) {
                        Label lblPayMan = (Label)e.Row.FindControl("lblPayMan");
                        e.Row.Cells[9].Attributes.Add("title", PageUtility.TransferLanguage(this.Page, "机票费的支付人是员工！", "is paid by employee"));
                        lblPayMan.ForeColor = System.Drawing.Color.Red;
                    }
                    //是否允许坐飞机
                    if (new AuthorizationBLL().GetStaffLevelById(int.Parse(ViewState["StaffLevelID"].ToString())).IsPlane == false) {
                        Label lblManageExpenseItem = (Label)e.Row.FindControl("lblManageExpenseItem");
                        e.Row.Cells[3].Attributes.Add("title", PageUtility.TransferLanguage(this.Page, "该级别员工不允许坐飞机！", "this staff should not take a plane"));
                        lblManageExpenseItem.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            this.ViewState["TotalApplyAmount"] = TotalFee;
            if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit) {
                DropDownList ddlCity = (DropDownList)e.Row.FindControl("ddlCity");
                DropDownList ddlManageExpenseItem = (DropDownList)e.Row.FindControl("ddlManageExpenseItem");
                DropDownList ddlPayMan = (DropDownList)e.Row.FindControl("ddlPayMan");
                TextBox txtDestination = (TextBox)e.Row.FindControl("txtDestination");
                if (ddlCity.SelectedValue == "0") {
                    txtDestination.Text = "";

                } else if (new MasterDataBLL().GetCityById(int.Parse(ddlCity.SelectedValue)).IsAutoComplete == true) {
                    txtDestination.ReadOnly = true;
                    txtDestination.Text = ddlCity.SelectedItem.Text;
                } else {
                    txtDestination.ReadOnly = false;
                }

                if (new MasterDataBLL().GetManageExpenseItemById(int.Parse(ddlManageExpenseItem.SelectedValue)).IsTicket == true) {
                    ddlPayMan.Enabled = true;

                }

                //自动计算
                TextBox txtFrequency = (TextBox)e.Row.FindControl("txtFrequency");
                TextBox txtUnitPrice = (TextBox)e.Row.FindControl("txtUnitPrice");
                TextBox txtExchangeRate = (TextBox)e.Row.FindControl("txtExchangeRate");
                TextBox txtCost = (TextBox)e.Row.FindControl("txtCost");
                txtExchangeRate.Attributes.Add("onchange", "ParameterChanged('" + txtExchangeRate.ClientID + "','" + txtUnitPrice.ClientID + "','" + txtFrequency.ClientID + "','" + txtCost.ClientID + "')");
                txtUnitPrice.Attributes.Add("onchange", "ParameterChanged('" + txtExchangeRate.ClientID + "','" + txtUnitPrice.ClientID + "','" + txtFrequency.ClientID + "','" + txtCost.ClientID + "')");
                txtFrequency.Attributes.Add("onchange", "ParameterChanged('" + txtExchangeRate.ClientID + "','" + txtUnitPrice.ClientID + "','" + txtFrequency.ClientID + "','" + txtCost.ClientID + "')");
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer) {
            Label lblTotalRMB = (Label)e.Row.FindControl("lblTotalRMB");
            lblTotalRMB.Text = TotalFee.ToString("N");
        }
    }

    public string GetCityByID(object ID) {
        return new MasterDataBLL().GetCityById(int.Parse(ID.ToString())).CityName;
    }

    public string GetManageExpenseItemNameByID(object ID) {
        return new MasterDataBLL().GetManageExpenseItemById((int)ID).ManageExpenseItemName;
    }

    public string GetCurrencyByID(object ID) {
        return new MasterDataBLL().GetCurrencyByID((int)ID).CurrencyFullName;
    }

    public string GetPayMan(object PayMan) {
        //string payman = "员工支付";
        string payman = GetLocalResourceObject("TravelReimburseDetailsGridView_TemplateField_ddlPayMan0.Text").ToString();
        if (PayMan.ToString() == "1") {
            //payman = "公司支付";
            payman = GetLocalResourceObject("TravelReimburseDetailsGridView_TemplateField_ddlPayMan1.Text").ToString();
        }
        return payman;
    }

    #endregion

    protected void ddlManageExpenseItem_SelectedIndexChanged(object sender, EventArgs e) {
        DropDownList ddlManageExpenseItem = (DropDownList)sender;
        DropDownList ddlPayMan = new DropDownList();
        if (ddlManageExpenseItem.NamingContainer.ID == "fvTravelReimburseDetails") {
            ddlPayMan = (DropDownList)fvTravelReimburseDetails.FindControl("ddlPayMan");
        } else {
            ddlPayMan = (DropDownList)gvTravelReimburseDetails.Rows[gvTravelReimburseDetails.EditIndex].FindControl("ddlPayMan");
        }
        if (ddlManageExpenseItem.SelectedValue != "0") {
            if (new MasterDataBLL().GetManageExpenseItemById(int.Parse(ddlManageExpenseItem.SelectedValue)).IsTicket == true) {
                ddlPayMan.Enabled = true;
                ddlPayMan.SelectedValue = "1";
            } else {
                ddlPayMan.Enabled = false;
                ddlPayMan.SelectedValue = "0";
            }
        } else {
            ddlPayMan.Enabled = false;
            ddlPayMan.SelectedValue = "0";
        }

    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e) {
        DropDownList ddlCity = (DropDownList)sender;
        TextBox txtDestination = new TextBox();
        if (ddlCity.NamingContainer.ID == "fvTravelReimburseDetails") {
            txtDestination = (TextBox)fvTravelReimburseDetails.FindControl("txtDestination");
        } else {
            txtDestination = (TextBox)gvTravelReimburseDetails.Rows[gvTravelReimburseDetails.EditIndex].FindControl("txtDestination");
        }
        if (ddlCity.SelectedValue == "0") {
            txtDestination.Text = "";
            txtDestination.ReadOnly = true;
        } else if (new MasterDataBLL().GetCityById(int.Parse(ddlCity.SelectedValue)).IsAutoComplete == true) {
            txtDestination.ReadOnly = true;
            txtDestination.Text = ddlCity.SelectedItem.Text;
        } else {
            txtDestination.Text = "";
            txtDestination.ReadOnly = false;
        }
    }

    protected void fvTravelReimburseDetails_DataBound(object sender, EventArgs e) {
        //自动计算
        TextBox txtFrequency = (TextBox)this.fvTravelReimburseDetails.FindControl("txtFrequency");
        TextBox txtUnitPrice = (TextBox)this.fvTravelReimburseDetails.FindControl("txtUnitPrice");
        TextBox txtExchangeRate = (TextBox)this.fvTravelReimburseDetails.FindControl("txtExchangeRate");
        TextBox txtCost = (TextBox)this.fvTravelReimburseDetails.FindControl("txtCost");
        txtExchangeRate.Attributes.Add("onchange", "ParameterChanged('" + txtExchangeRate.ClientID + "','" + txtUnitPrice.ClientID + "','" + txtFrequency.ClientID + "','" + txtCost.ClientID + "')");
        txtUnitPrice.Attributes.Add("onchange", "ParameterChanged('" + txtExchangeRate.ClientID + "','" + txtUnitPrice.ClientID + "','" + txtFrequency.ClientID + "','" + txtCost.ClientID + "')");
        txtFrequency.Attributes.Add("onchange", "ParameterChanged('" + txtExchangeRate.ClientID + "','" + txtUnitPrice.ClientID + "','" + txtFrequency.ClientID + "','" + txtCost.ClientID + "')");
        txtExchangeRate.Text = "1.0000";
    }
}