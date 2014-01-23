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
using BusinessObjects.ReportDSTableAdapters;
using System.Collections.Generic;

public partial class FormQuery_FormPaymentList : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            PageUtility.SetContentTitle(this, "数据导出查询", "Form Payment List");
            this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = "1!=1";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e) {
        string l_strwhere = "";
        if (!string.IsNullOrEmpty(txtFormNo.Text)) {
            l_strwhere += string.Format(" and Form.FormNo like '%{0}%'", txtFormNo.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtStuffUser.Text)) {
            l_strwhere += string.Format(" and StuffName like '%{0}%'", txtStuffUser.Text.Trim());
        }
        if (!string.IsNullOrEmpty(UCDateInputBeginDate.SelectedDate)) {
            l_strwhere += string.Format(" and Form.SubmitDate >= '{0}'", UCDateInputBeginDate.SelectedDate);
        }
        if (!string.IsNullOrEmpty(UCDateInputEndDate.SelectedDate)) {
            l_strwhere += string.Format(" and Form.SubmitDate <= '{0} 23:59:59:999' ", UCDateInputEndDate.SelectedDate);
        }
        if (!string.IsNullOrEmpty(UCDateInputBeginCreateVoucherDate.SelectedDate)) {
            l_strwhere += string.Format(" and Form.CreateVoucherDate >= '{0}'", UCDateInputBeginCreateVoucherDate.SelectedDate);
        }
        if (!string.IsNullOrEmpty(UCDateInputEndCreateVoucherDate.SelectedDate)) {
            l_strwhere += string.Format(" and Form.CreateVoucherDate <= '{0}  23:59:59:999'", UCDateInputEndCreateVoucherDate.SelectedDate);
        }
        if (ddlFormType.SelectedValue != "0") {
            l_strwhere += string.Format(" and Form.FormTypeID = '{0}'", ddlFormType.SelectedValue);
        }
        this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = "1=1 and Form.StatusID=2 and Form.FormTypeID in (32,42,13,14,24,1,4,23) " + l_strwhere;
    }

    public string GetStaffNameByID(object UserID) {
        return new AuthorizationBLL().GetStuffUserById((int)UserID).StuffName;
    }
    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowIndex > -1) {
                Label lblIsExportLock = (Label)e.Row.FindControl("lblIsExportLock");
                Label lblIsCreateVoucher = (Label)e.Row.FindControl("lblIsCreateVoucher");
                if (lblIsExportLock.Text.ToLower() == "true")
                {
                    lblIsExportLock.Text = "是";
                }
                else if (lblIsExportLock.Text.ToLower() == "false")
                {
                    lblIsExportLock.Text = "否";
                }
                if (lblIsCreateVoucher.Text.ToLower() == "true")
                {
                    lblIsCreateVoucher.Text = "是";
                }
                else if (lblIsCreateVoucher.Text.ToLower() == "false")
                {
                    lblIsCreateVoucher.Text = "否";
                }
        }
    }
    protected void btnUnLock_Click(object sender, EventArgs e)
    {
        FormPurchaseBLL formbll = new FormPurchaseBLL();
        
        for (int i = 0; i < gvApplyList.Rows.Count; i++)
        {
            int formId =Convert.ToInt32(gvApplyList.DataKeys[i].Value);
            CheckBox cbIsUnLock = (CheckBox)gvApplyList.Rows[i].FindControl("cbIsUnLock");
            if (cbIsUnLock.Checked)
            {
                PurchaseDS.FormDataTable l_dtform = formbll.GetFormByID(formId);
                if (l_dtform.Rows.Count > 0)
                {
                    l_dtform[0].IsExportLock = false;
                    formbll.TAForm.Update(l_dtform);
                }
            }
        }
        this.gvApplyList.DataBind();

    }
}

