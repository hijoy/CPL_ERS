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

public partial class FormRD_RDApproval : BasePage {

    decimal Total = 0;
    decimal TotalRMB = 0;
    decimal DeliveryAmount = 0;
    decimal DeliveryQuantity = 0;

    private FormRDBLL _FormRDBLL;
    protected FormRDBLL FormRDBLL {
        get {
            if (this._FormRDBLL == null) {
                this._FormRDBLL = new FormRDBLL();
            }
            return this._FormRDBLL;
        }
    }

    private MasterDataBLL _MasterDataBLL;
    private MasterDataBLL MasterDataBLL {
        get {
            if (_MasterDataBLL == null) {
                return new MasterDataBLL();
            }
            return _MasterDataBLL;
        }
    }

    #region 页面初始化及事件处理

    protected void Page_Load(object sender, EventArgs e) {
        AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);

            MasterDataBLL mdBLL = new MasterDataBLL();
            int formID = int.Parse(Request["ObjectId"]);
            this.ViewState["ObjectId"] = formID;
            FormDS.FormRow rowForm = this.FormRDBLL.GetFormByID(formID)[0];
            FormDS.FormRDApplyRow rowFormApply = this.FormRDBLL.GetFormRDApplyByID(formID)[0];
            if (rowForm.IsProcIDNull()) {
                ViewState["ProcID"] = "";
            } else {
                ViewState["ProcID"] = rowForm.ProcID;
            }
            this.FormNoCtl.Text = rowForm.FormNo;
            AuthorizationDS.StuffUserRow applicant = new AuthorizationBLL().GetStuffUserById(rowForm.UserID);
            this.StuffNameCtl.Text =  CommonUtility.GetStaffFullName(applicant);
            this.PositionNameCtl.Text = new OUTreeBLL().GetPositionById(rowForm.PositionID).PositionName;
            if (new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID) != null) {
                this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowForm.OrganizationUnitID).OrganizationUnitName;
            }
            this.StuffNoCtl.Text = applicant.IsStuffNoNull() ? "" : applicant.StuffNo;
            this.AttendDateCtl.Text = applicant.AttendDate.ToShortDateString();

            this.PeriodCtl.Text = rowFormApply.FPeriod.ToString("yyyy-MM");
            this.CustomerNameCtl.Text = new MasterDataBLL().GetCustomerById(rowFormApply.CustomerID)[0].CustomerName;
            this.CustomerChannelCtl.Text = MasterDataBLL.GetCustomerChannelById(rowFormApply.CustomerChannelID)[0].CustomerChannelName;
            this.BrandCtl.Text = MasterDataBLL.GetBrandById(rowFormApply.BrandID)[0].BrandName;
            this.CurrencyCtl.Text = MasterDataBLL.GetCurrencyByID(rowFormApply.CurrencyID).CurrencyShortName;
            this.ExchangeRateCtl.Text = rowFormApply.ExchangeRate.ToString();
            if (!rowFormApply.IsProjectNameNull()) {
                this.ProjectNameCtl.Text = rowFormApply.ProjectName;
            }
            this.ExpenseSubCategoryCtl.Text = mdBLL.GetExpenseSubCategoryById(rowFormApply.ExpenseSubCategoryID).ExpenseSubCategoryName;

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                FormDS.FormRow rejectedForm = this.FormRDBLL.GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/SampleRequest/RDSampleRequestApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
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

            this.CostCenterCtl.Text = CommonUtility.GetMAACostCenterFullName(rowForm.CostCenterID);
            if (!rowFormApply.IsDeliveryAddressNull()) {
                this.txtDeliveryAddress.Text = rowFormApply.DeliveryAddress;
            }
            if (!rowFormApply.IsExpectDeliveryDateNull()) {
                this.txtExpectDeliveryDate.Text = rowFormApply.ExpectDeliveryDate.ToString("yyyy-MM-dd");
            }

            this.TotalBudgetCtl.Text = rowFormApply.TotalBudget.ToString("N");
            this.ApprovedAmountCtl.Text = rowFormApply.ApprovedAmount.ToString("N");
            this.ApprovingAmountCtl.Text = rowFormApply.ApprovingAmount.ToString("N");
            this.ReimbursedAmountCtl.Text = rowFormApply.ReimbursedAmount.ToString("N");
            this.RemainBudgetCtl.Text = rowFormApply.RemainBudget.ToString("N");

            //明细
            this.odsDetails.SelectParameters["FormRDApplyID"].DefaultValue = rowFormApply.FormRDApplyID.ToString();

            //按钮控制
            //审批页面处理&按钮处理
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            if (rowForm.InTurnUserIds.Contains("P" + stuffUser.StuffUserId + "P")) {
                this.SubmitBtn.Visible = true;
                this.ViewState["IsView"] = false;
            } else {
                this.SubmitBtn.Visible = false;
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

            FormDS.FormDeliveryGoodsDataTable tbDelivery = new FormSaleBLL().GetFormDeliveryGoodByFormID(formID);
            if (tbDelivery.Count > 0) {
                this.gvDeliveryInfo.DataSource = tbDelivery;
                this.gvDeliveryInfo.DataBind();
            } else {
                this.divDeliveryInfo.Visible = false;
                this.gvDeliveryInfo.Visible = false;
            }

            //是否显示报销完成按钮
            this.CloseBtn.Visible = false;
            if ((!rowFormApply.IsClose) && rowForm.StatusID == (int)SystemEnums.FormStatus.ApproveCompleted) {
                if (stuffUser.StuffUserId == rowForm.UserID || new AuthorizationBLL().GetProxyReimburseByParameter(rowForm.UserID, stuffUser.StuffUserId, rowForm.SubmitDate).Count > 0) {
                    this.CloseBtn.Visible = true;
                }
            }

            //发货完成按钮权限
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.DeliveryComplete, SystemEnums.OperateEnum.Other);
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool hasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            if (hasManageRight && (rowFormApply.IsIsDeliveryCompleteNull() || rowFormApply.IsDeliveryComplete == false)) {
                this.DeliveryCompleteBtn.Visible = true;
            } else {
                this.DeliveryCompleteBtn.Visible = false;
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
                            this.cwfAppCheck.GetApproveOrReject(), this.cwfAppCheck.GetComments(), ProxyStuffName, CommonUtility.CheckPeriod(cwfAppCheck.FormID));

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
        this.Response.Redirect("~/SampleRequest/RDSampleRequestApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormRDApplyDetailRow row = (FormDS.FormRDApplyDetailRow)drvDetail.Row;
                Total = decimal.Round((Total + row.DeliveryQuantity), 2);
                TotalRMB = decimal.Round((TotalRMB + row.AmountRMB), 2);
            }
        }

        this.ViewState["TotalApplyAmount"] = TotalRMB;

        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label lblTotalRMB = (Label)e.Row.FindControl("lblTotalRMB");
                lblTotalRMB.Text = TotalRMB.ToString("N");
            }
        }

    }

    protected void gvDeliveryInfo_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                FormDS.FormDeliveryGoodsRow row = (FormDS.FormDeliveryGoodsRow)drvDetail.Row;
                DeliveryAmount = decimal.Round((DeliveryAmount + row.AmountRMB), 2);
                DeliveryQuantity = decimal.Round((DeliveryQuantity + row.Quantity), 2);
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label lblAmountRMBTotal = (Label)e.Row.FindControl("lblAmountRMBTotal");
                lblAmountRMBTotal.Text = DeliveryAmount.ToString("N");
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

    public string GetVendorNameByID(object VendorID) {
        if (VendorID.ToString() != string.Empty) {
            int id = Convert.ToInt32(VendorID);
            return new MasterDataBLL().GetVendorByID(id).VendorName;
        } else {
            return null;
        }
    }

    protected void DeliveryCompleteBtn_Click(object sender, EventArgs e) {
        new FormSampleRequestBLL().UpdateRDApplyAfterDeliveryComplete(int.Parse(this.ViewState["ObjectId"].ToString()), ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void CloseBtn_Click(object sender, EventArgs e) {
        try {
            //关闭时检查费用期间是否不存，若不存在不允许关闭
            if (!MasterDataBLL.PeriodSaleExists()) {
                PageUtility.ShowModelDlg(this, "关账期间，不允许关闭！");
                return;
            }

            new FormRDBLL().CloseRDApplyByFormID(int.Parse(this.ViewState["ObjectId"].ToString()));
            if (this.Request["Source"] != null) {
                this.Response.Redirect(this.Request["Source"].ToString());
            } else {
                this.Response.Redirect("~/Home.aspx");
            }
        } catch (Exception exception) {
            PageUtility.DealWithException(this, exception);
        }
    }

}