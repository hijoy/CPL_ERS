using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.Data;
using BusinessObjects.FormDSTableAdapters;

public partial class Form_FormTravelReimburseApproval : BasePage {
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
            String title = GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);
            this.Page.Title = title;

            int formID = int.Parse(Request["ObjectId"]);
            this.ViewState["ObjectId"] = formID;
            FormDS.FormRow rowForm = this.FormTEBLL.GetFormByID(formID)[0];
            FormDS.FormTravelReimburseRow rowTravelReimburse = this.FormTEBLL.GetFormTravelReimburseByID(formID)[0];
            if (rowForm.IsProcIDNull()) {
                ViewState["ProcID"] = "";
            } else {
                ViewState["ProcID"] = rowForm.ProcID;
            }

            //对控件进行赋值
            this.txtFormNo.Text = rowForm.FormNo;
            this.ApplyDateCtl.Text = rowForm.SubmitDate.ToShortDateString();
            AuthorizationDS.StuffUserRow applicant = new AuthorizationBLL().GetStuffUserById(rowForm.UserID);
            this.StuffNameCtl.Text = CommonUtility.GetStaffFullName(applicant);
            this.PositionNameCtl.Text = new OUTreeBLL().GetPositionById(rowForm.PositionID).PositionName;
            if (new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID) != null) {
                this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID).OrganizationUnitName;
            }
            this.AttendDateCtl.Text = applicant.AttendDate.ToShortDateString();
            this.StuffNoCtl.Text = applicant.IsStuffNoNull() ? "" : applicant.StuffNo;
            this.ViewState["StaffLevelID"] = applicant.StaffLevelID;

            this.txtPeriod.Text = rowTravelReimburse.Period.ToString("yyyyMM");

            this.txtTotalBudget.Text = rowTravelReimburse.TotalBudget.ToString();
            this.txtApprovedAmount.Text = rowTravelReimburse.ApprovedAmount.ToString();
            this.txtApprovingAmount.Text = rowTravelReimburse.ApprovingAmount.ToString();
            this.txtRemainAmount.Text = rowTravelReimburse.RemainAmount.ToString();
            AuthorizationDS.StuffUserRow thisStaff = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            if (thisStaff.StuffUserId == rowForm.UserID) {
                this.budgetTitleDIV.Visible = false;
                this.budgetDIV.Visible = false;
            }
            this.RemarkCtl.Text = rowTravelReimburse.IsRemarkNull() ? "" : rowTravelReimburse.Remark;
            if (!rowTravelReimburse.IsAttachedFileNameNull())
                this.UCFileUpload.AttachmentFileName = rowTravelReimburse.AttachedFileName;
            if (!rowTravelReimburse.IsRealAttachedFileNameNull())
                this.UCFileUpload.RealAttachmentFileName = rowTravelReimburse.RealAttachedFileName;

            this.odsTravelReimburseDetails.SelectParameters["FormTravelReimburselID"].DefaultValue = rowTravelReimburse.FormTravelReimburseID.ToString();

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                FormDS.FormRow rejectedForm = this.FormTEBLL.GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormTE/FormTravelReimburseApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
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

            if (rowForm.StatusID == (int)SystemEnums.FormStatus.Rejected && ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId == rowForm.UserID) {
                this.EditBtn.Visible = true;
                this.ScrapBtn.Visible = true;
            } else {
                this.EditBtn.Visible = false;
                this.ScrapBtn.Visible = false;
            }

            //如果是弹出,取消按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {

                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                }
            }

            //判断财务摘要的显示问题
            if (!rowForm.IsFinanceRemarkNull()) {
                this.FinanceRemarkCtl.Text = rowForm.FinanceRemark;
            }
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FinanceRemark, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FinanceRemark, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow ViewerPosition = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool HasViewRight = positionRightBLL.CheckPositionRight(ViewerPosition.PositionId, opViewId);
            bool HasManageRight = positionRightBLL.CheckPositionRight(ViewerPosition.PositionId, opManageId);
            //如果没有权限则隐藏
            if (!HasViewRight && !HasManageRight) {
                this.FinanceRemarkTitleDIV.Visible = false;
                this.FinanceRemarkDIV.Visible = false;
                this.SaveBtn.Visible = false;
            }
            //如果没有填写的权限或者已经导出锁定了，则隐藏保存按钮
            if (HasViewRight && (!HasManageRight || (!rowForm.IsIsExportLockNull() && rowForm.IsExportLock))) {
                this.FinanceRemarkCtl.ReadOnly = true;
                this.SaveBtn.Visible = false;
            }
            //如果不是审批中或者审批完成不能修改
            if (rowForm.StatusID != 1 && rowForm.StatusID != 2) {
                this.FinanceRemarkCtl.ReadOnly = true;
                this.SaveBtn.Visible = false;
            }

            //修改费用期间
            int opModifyPeriodId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.PeriodModify, SystemEnums.OperateEnum.Manage);
            bool HasModifyPeriodRight = positionRightBLL.CheckPositionRight(ViewerPosition.PositionId, opModifyPeriodId);
            if ((rowForm.StatusID == 1 || rowForm.StatusID == 2) && HasModifyPeriodRight) {
                this.UCNewPeriod.Visible = true;
                this.ModifyPeriodBtn.Visible = true;
            } else {
                this.UCNewPeriod.Visible = false;
                this.ModifyPeriodBtn.Visible = false;
            }

            //单据打印
            this.ucPrint.FormID = rowForm.FormID;
        }
        this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
        this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
        this.cwfAppCheck.IsView = (bool)this.ViewState["IsView"];
    }

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
        this.Response.Redirect("~/FormTE/FormTravelReimburseApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        if (FinanceRemarkCtl.Text.Trim() == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入财务摘要", "please key in finance remark");
            return;
        }
        try {
            new FormTEBLL().AddFinanceRemark(int.Parse(this.ViewState["ObjectId"].ToString()), this.FinanceRemarkCtl.Text.Trim());
            PageUtility.ShowModelDlg(this.Page, "已保存", "Successfully saved");
            //if (this.Request["Source"] != null) {
            //    this.Response.Redirect(this.Request["Source"].ToString());
            //} else {
            //    this.Response.Redirect("~/Home.aspx");
            //}
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    protected void ModifyPeriodBtn_Click(object sender, EventArgs e) {
        string stringPeriod = ((TextBox)(this.UCNewPeriod.FindControl("txtDate"))).Text.Trim();
        if (stringPeriod == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请选择要修改的费用期间", "please select period");
            return;
        }
        DateTime Period = DateTime.Parse(stringPeriod.Substring(0, 4) + "-" + stringPeriod.Substring(4, 2) + "-01");
        try {
            new FormTEBLL().ModifyPeriodForTravelReimburse(int.Parse(this.ViewState["ObjectId"].ToString()), Period);
            PageUtility.ShowModelDlg(this.Page, "已修改成功", "Successfully saved");
            this.txtPeriod.Text = Period.ToString("yyyyMM");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    protected void gvTravelReimburseDetails_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
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
                    e.Row.Cells[6].Attributes.Add("title", PageUtility.TransferLanguage(this.Page, "超过费用标准 ￥" + limit, "Over Cost Standard ￥" + limit));
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
        }


        if (e.Row.RowType == DataControlRowType.Footer) {
            Label lblTotalRMB = (Label)e.Row.FindControl("lblTotalRMB");
            lblTotalRMB.Text = TotalFee.ToString("N");

        }
    }

    public string GetPayMan(object PayMan) {
        string payman = "员工支付";
        if (PayMan.ToString() == "1") {
            payman = "公司支付";
        }
        return payman;
    }

    public string GetManageExpenseItemNameByID(object ID) {
        return new MasterDataBLL().GetManageExpenseItemById((int)ID).ManageExpenseItemName;
    }

    public string GetCurrencyByID(object ID) {
        return new MasterDataBLL().GetCurrencyByID((int)ID).CurrencyFullName;
    }

}