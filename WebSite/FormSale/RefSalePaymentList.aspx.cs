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

public partial class FormSale_RefSalePaymentList : BasePage {
    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            PageUtility.SetContentTitle(this, "相关报销单");

            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            this.odsPaymentList.SelectParameters["UserID"].DefaultValue = stuffuserID.ToString();
            this.odsPaymentList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();

            if (Request["FormSaleApplyID"] != null) {//如果是从申请单来的
                this.SettlementTR.Visible = false;
                int SaleApplyID = int.Parse(Request["FormSaleApplyID"].ToString());
                this.ViewState["FormSaleApplyID"] = Request["FormSaleApplyID"];
                QueryDS.FormSaleApplyViewRow applyRow = new FormQueryBLL().GetFormSaleApplyViewByID(SaleApplyID);
                this.txtApplyFormNo.Text = applyRow.FormNo;
                this.txtApplyCustomer.Text = applyRow.CustomerName;
                this.txtApplyCategory.Text = applyRow.ExpenseSubCategoryName;
                this.txtApplyProjectName.Text = applyRow.IsProjectNameNull() ? "" : applyRow.ProjectName;
                this.txtApplyAmount.Text = applyRow.AmountRMB.ToString("N");

                string filterStr = "Form.StatusId >=" + ((int)SystemEnums.FormStatus.Awaiting).ToString();
                filterStr += " And charindex('P" + applyRow.FormNo + "P',IsNull(FormSaleSettlement.FormApplyNos,'P'+ApplyForm.FormNo+'P'))>0";
                this.odsPaymentList.SelectParameters["queryExpression"].DefaultValue = filterStr;
            } else {//如果是从结案单来的
                this.ApplyTR.Visible = false;
                int SettlementID = int.Parse(Request["FormSaleSettlementID"].ToString());
                this.ViewState["FormSaleSettlementID"] = Request["FormSaleSettlementID"];
                QueryDS.FormSaleSettlementViewRow settlementRow = new FormQueryBLL().GetFormSaleSettlementViewByID(SettlementID);
                this.txtSettleFormNo.Text = settlementRow.FormNo;
                this.txtSettleCustomer.Text = settlementRow.CustomerName;
                this.txtSettleCategory.Text = settlementRow.ExpenseSubCategoryName;
                this.txtSettleBrand.Text = settlementRow.BrandName;
                this.txtSettleAmount.Text = settlementRow.AmountRMB.ToString("N");

                string filterStr = "Form.StatusId >=" + ((int)SystemEnums.FormStatus.Awaiting).ToString() + " And FormSaleSettlement.FormSaleSettlementID= " + SettlementID;
                this.odsPaymentList.SelectParameters["queryExpression"].DefaultValue = filterStr;
            }

        }
    }

    protected void gvPaymentList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormSalePaymentViewRow row = (QueryDS.FormSalePaymentViewRow)drvDetail.Row;

                LinkButton lblFormNo = (LinkButton)e.Row.FindControl("lblFormNo");
                if (this.ViewState["FormSaleApplyID"] != null) {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/FormSale/RefSalePaymentList.aspx?FormSaleApplyID=" + this.ViewState["FormSaleApplyID"].ToString() + "&startIndex=" + this.gvPaymentList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/FormSale/RefSalePaymentList.aspx?FormSaleSettlementID=" + this.ViewState["FormSaleSettlementID"].ToString() + "&startIndex=" + this.gvPaymentList.PageIndex));
                }

                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblStatus.Text = CommonUtility.GetStatusName(row.StatusID);

            }
        }
    }

}
