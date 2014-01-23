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

public partial class FormPurchase_FormVendorApproval : BasePage {


    private FormVendorBLL m_FormVendorBLL;
    protected FormVendorBLL FormVendorBLL {
        get {
            if (this.m_FormVendorBLL == null) {
                this.m_FormVendorBLL = new FormVendorBLL();
            }
            return this.m_FormVendorBLL;
        }
    }

    private MasterDataBLL _MasterDataBLL;
    protected MasterDataBLL MasterDataBLL {
        get {
            if (this._MasterDataBLL == null) {
                this._MasterDataBLL = new MasterDataBLL();
            }
            return this._MasterDataBLL;
        }
    }

    #region 页面初始化及事件处理
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);

            int formID = int.Parse(Request["ObjectId"]);
            this.ViewState["ObjectId"] = formID;
            PurchaseDS.FormRow rowForm = this.FormVendorBLL.GetFormByID(formID)[0];
            PurchaseDS.FormVendorRow rowForVendor = this.FormVendorBLL.GetFormVendorByID(formID)[0];
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

            //历史单据
            if (rowForm.IsRejectedFormIDNull()) {
                lblRejectFormNo.Text = "无";
            } else {
                PurchaseDS.FormRow rejectedForm = this.FormVendorBLL.GetFormByID(rowForm.RejectedFormID)[0];
                this.lblRejectFormNo.Text = rejectedForm.FormNo;
                this.lblRejectFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormPurchase/FormVendorApproval.aspx?ShowDialog=1&ObjectId=" + rejectedForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            }

            PurchaseDS.FormVendorRow FormVendorTR = FormVendorBLL.GetFormVendorByID(formID)[0];
            if (!FormVendorTR.IsVendorNameNull()) {
                txtVendorName.Text = FormVendorTR.VendorName;
            }
            if (!FormVendorTR.IsVendorAddressNull()) {
                txtVendorAddress.Text = FormVendorTR.VendorAddress;
            }
            if (!FormVendorTR.IsCityNull()) {
                txtCity.Text = FormVendorTR.City;
            }
            if (!FormVendorTR.IsPostalNull()) {
                txtPostal.Text = FormVendorTR.Postal;
            }
            if (!FormVendorTR.IsContactNameNull()) {
                txtContactName.Text = FormVendorTR.ContactName;
            }
            MasterDataBLL masterDataBLL = new MasterDataBLL();
            if (!FormVendorTR.IsVendorTypeIDNull()) {
                this.VendorTypeControl.VendorTypeID = FormVendorTR.VendorTypeID.ToString();
                MasterData.VendorTypeRow VendorTypeRow = masterDataBLL.GetVendorTypeById(FormVendorTR.VendorTypeID);
                txtCurrency.Text = masterDataBLL.GetCurrencyByID(VendorTypeRow.CurrencyID).CurrencyFullName;
                txtCompany.Text = masterDataBLL.GetCompanyById(VendorTypeRow.CompanyID).CompanyName;
                txtCompanyCode.Text = masterDataBLL.GetCompanyById(VendorTypeRow.CompanyID).CompanyCode;
            }
            if (!FormVendorTR.IsPhoneNumberNull()) {
                this.txtPhoneNumber.Text = FormVendorTR.PhoneNumber;
            }
            if (!FormVendorTR.IsOneTimeVendorNull()) {
                this.txtOneTimeVendor.Text = FormVendorTR.OneTimeVendor ? "Y" : "N";
            }
            if (!FormVendorTR.IsHoldVendorNull()) {
                this.txtHoldVendor.Text = FormVendorTR.HoldVendor ? "Y" : "N";
            }
            if (!FormVendorTR.IsPurchaseingPostalCodeNull()) {
                this.txtPurchaseingPostalCode.Text = FormVendorTR.PurchaseingPostalCode;
            }
            if (!FormVendorTR.IsAlphaSearchKeyNull()) {
                this.txtAlphaSearchKey.Text = FormVendorTR.AlphaSearchKey;
            }
            if (!FormVendorTR.IsPurchasingCityNull()) {
                this.txtPurchasingCity.Text = FormVendorTR.PurchasingCity;
            }
            if (!FormVendorTR.IsPurchasingContactNull()) {
                this.txtPurchasingContact.Text = FormVendorTR.PurchasingContact;
            }
            if (!FormVendorTR.IsPurchasingAddressNull()) {
                this.txtPurchasingAddress.Text = FormVendorTR.PurchasingAddress;
            }
            if (!FormVendorTR.IsPurchasePhoneNumberNull()) {
                this.txtPurchasePhoneNumber.Text = FormVendorTR.PurchasePhoneNumber;
            }
            if (!FormVendorTR.IsBankCodeNull()) {
                this.txtBankCode.Text = FormVendorTR.BankCode;
            }
            if (!FormVendorTR.IsMethodPaymentIDNull()) {
                this.txtMethodPayment.Text = masterDataBLL.GetMethodPaymentById(FormVendorTR.MethodPaymentID)[0].MethodPaymentName;
            }
            if (!FormVendorTR.IsPaymentTermIDNull()) {
                this.txtPaymentTerm.Text = masterDataBLL.GetPaymentTermById(FormVendorTR.PaymentTermID)[0].PaymentTermName;
            }
            if (!FormVendorTR.IsTransTypeIDNull()) {
                this.txtTransType.Text = masterDataBLL.GetTransTypeById(FormVendorTR.TransTypeID)[0].TransTypeName;
            }
            if (!FormVendorTR.IsVATTypeIDNull()) {
                txtVATRate.Text = masterDataBLL.GetVatTypeById(FormVendorTR.VATTypeID)[0].VatTypeName;
            }
            if (!FormVendorTR.IsBankNameNull()) {
                txtBankName.Text = FormVendorTR.BankName.ToString();
            }
            if (!FormVendorTR.IsAccountNoNull()) {
                txtAccountNo.Text = FormVendorTR.AccountNo;
            }
            if (!FormVendorTR.IsBankNoNull()) {
                this.txtBankNo.Text = FormVendorTR.BankNo;
            }
            if (!FormVendorTR.IsACTypeIDNull()) {
                this.txtACType.Text = masterDataBLL.GetACTypeById(FormVendorTR.ACTypeID)[0].ACTypeName;
            }
            if (!FormVendorTR.IsAttachmentFileNameNull()) {
                this.UCFileUpload.AttachmentFileName = FormVendorTR.AttachmentFileName;
                this.UCFileUpload.RealAttachmentFileName = FormVendorTR.RealAttachmentFileName;
            }
            if (!FormVendorTR.IsModifyReasonNull()) {
                this.txtModifyReason.Text = FormVendorTR.ModifyReason;
            }
            if (!FormVendorTR.IsRemarkNull()) {
                this.txtRemark.Text = FormVendorTR.Remark;
            }
            //通过判断FormVendor有没有VendorID，判断是新增还是修改Vendor
            if (!FormVendorTR.IsVendorIDNull()) {
                this.ViewState["VendorID"] = FormVendorTR.VendorID;
            }
            if (FormVendorTR.ActionType != (int)SystemEnums.VendorActionType.Add) {
                this.trModifyReason.Visible = true;
            }

            //是新增还是修改
            this.ViewState["ActionType"] = FormVendorTR.ActionType;
            this.txtActionType.Text = CommonUtility.GetVendorActionTypeName(FormVendorTR.ActionType);

            //Vendor
            if (!FormVendorTR.IsVendorIDNull()) {
                hlVendor.Text = new MasterDataBLL().GetVendorByID(FormVendorTR.VendorID).VendorCode;
                hlVendor.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/Dialog/VendorDetail.aspx?ShowDialog=1&VendorID=" + FormVendorTR.VendorID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            } else {
                hlVendor.Text = "无";
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

            //如果是弹出,取消按钮不可见
            if (this.Request["ShowDialog"] != null) {
                if (this.Request["ShowDialog"].ToString() == "1") {
                    this.upButton.Visible = false;
                    this.Master.FindControl("divMenu").Visible = false;
                    this.Master.FindControl("tbCurrentPage").Visible = false;
                }
            }

            //是否有修改VendorType的权限
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.VendorTypeModify, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            if (rowForm.StatusID == (int)SystemEnums.FormStatus.Awaiting && positionRightBLL.CheckPositionRight(position.PositionId, opManageId)) {
                this.VendorTypeControl.IsVisible = "inline";
                this.btnSave.Visible = true;
            } else {
                this.VendorTypeControl.IsVisible = "none";
                this.btnSave.Visible = false;
            }
        }
        this.cwfAppCheck.FormID = (int)this.ViewState["ObjectId"];
        this.cwfAppCheck.ProcID = this.ViewState["ProcID"].ToString();
        this.cwfAppCheck.IsView = (bool)this.ViewState["IsView"];
    }

    #endregion

    //提交时要判断是不是审批通过，若通过则更新vendor表
    protected void SubmitBtn_Click(object sender, EventArgs e) {
        //审批完成时将vendor信息插入vendor表（待修改）
        try {
            if (this.cwfAppCheck.CheckInputData()) {
                AuthorizationDS.StuffUserRow currentStuff = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
                string ProxyStuffName = null;
                if (Session["ProxyStuffUserId"] != null) {
                    ProxyStuffName = new StuffUserBLL().GetStuffUserById(int.Parse(Session["ProxyStuffUserId"].ToString()))[0].StuffName;
                }
                new APFlowBLL().ApproveForm(this.cwfAppCheck.FormID, currentStuff.StuffUserId, currentStuff.StuffName,
                            this.cwfAppCheck.GetApproveOrReject(), this.cwfAppCheck.GetComments(), ProxyStuffName);

                PurchaseDS.FormRow formRow = this.FormVendorBLL.GetFormByID(this.cwfAppCheck.FormID)[0];
                if (formRow.StatusID == 2) {
                    if (this.ViewState["ActionType"] != null) {
                        if (int.Parse(this.ViewState["ActionType"].ToString()) != 1) {
                            MasterDataBLL.UpdateVendor((int)this.ViewState["ObjectId"]);
                        } else {
                            new MasterDataBLL().InsertVendor((int)this.ViewState["ObjectId"]);
                        }
                    }
                }
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
        this.Response.Redirect("~/FormPurchase/FormVendorApply.aspx?ObjectId=" + this.ViewState["ObjectId"].ToString() + "&RejectObjectID=" + this.ViewState["ObjectId"].ToString());
    }

    protected void ScrapBtn_Click(object sender, EventArgs e) {
        new APFlowBLL().ScrapForm((int)this.ViewState["ObjectId"]);
        this.Response.Redirect("~/Home.aspx");
    }

    protected void txtVendorTypeName_TextChanged(object sender, EventArgs e) {
        MasterDataBLL ma = new MasterDataBLL();
        if (this.VendorTypeControl.VendorTypeID != string.Empty) {
            txtCurrency.Text = ma.GetCurrencyByID(ma.GetVendorTypeById(int.Parse(VendorTypeControl.VendorTypeID)).CurrencyID).CurrencyFullName;
            txtCompany.Text = ma.GetCompanyById(ma.GetVendorTypeById(int.Parse(VendorTypeControl.VendorTypeID)).CompanyID).CompanyName;
        } else {
            txtCurrency.Text = "";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e) {
        new FormVendorBLL().UpdateFormVendorApply((int)this.ViewState["ObjectId"], int.Parse(this.VendorTypeControl.VendorTypeID));
        PageUtility.ShowModelDlg(this, "保存成功!");
    }
}