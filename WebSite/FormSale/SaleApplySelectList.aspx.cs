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
using System.Collections.Generic;

public partial class FormSale_SaleApplySelectList : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);

            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            this.ViewState["DefaultFilter"] = "(UserID=" + stuffuserID + " or exists (select * from ProxyReimburse where ProxyReimburse.UserID = FormSaleApplyView.UserID and ProxyReimburse.ProxyUserID =" + stuffuserID + " and ProxyReimburse.EndDate>FormSaleApplyView.SubmitDate) )"
                + " And IsClose = 0 and StatusID = 2 and PageType<>27"//û�б��᰸�ĵ���
                //+ "And FormSaleApplyView.FormID not in (select FormSettlementExpenseDetail.FormSaleApplyID from FormSettlementExpenseDetail join FormSaleSettlement on FormSettlementExpenseDetail.FormSaleSettlementID = FormSaleSettlement.FormSaleSettlementID  join Form SettlementForm on  FormSettlementExpenseDetail.FormSaleSettlementID = SettlementForm.FormID)";
            + " And FormSaleApplyView.FormID not in (select FormSettlementExpenseDetail.FormSaleApplyID from Form join FormSettlementExpenseDetail on FormSettlementExpenseDetail.FormSaleSettlementID = Form.FormID where Form.StatusID in (0,1,2,3)  )"+
            " And FormSaleApplyView.FormID not in (select FormSalePayment.FormSaleApplyID from Form Join FormSalePayment on FormSalePayment.FormSalePaymentID = Form.FormID where Form.StatusID in (0,1,3) and FormSalePayment.FormSaleApplyID is not null )";//û������������Ԥ����
            if (Request["Search"] == null) {
                this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = this.ViewState["DefaultFilter"].ToString();
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        if (!IsPostBack) {
            if (Request["Search"] != null) {
                if (this.Request["FormNo"] != null)
                    this.txtFormNo.Text = this.Request["FormNo"].ToString();
                if (this.Request["StuffName"] != null)
                    this.txtStuffUser.Text = this.Request["StuffName"].ToString();
                if (this.Request["CustomerID"] != null)
                    this.UCCustomer.CustomerID = this.Request["CustomerID"].ToString();
                this.ExpenseSubCategoryDDL.DataBind();
                if (this.Request["ExpenseSubCategoryID"] != null)
                    this.ExpenseSubCategoryDDL.SelectedValue = this.Request["ExpenseSubCategoryID"].ToString();
                this.BrandDDL.DataBind();
                if (this.Request["BrandID"] != null)
                    this.BrandDDL.SelectedValue = this.Request["BrandID"].ToString();
                if (this.Request["PeriodStart"] != null)
                    this.UCPeriodBegin.SelectedDate = this.Request["PeriodStart"].ToString();
                if (this.Request["PeriodEnd"] != null)
                    this.UCPeriodEnd.SelectedDate = this.Request["PeriodEnd"].ToString();
                if (this.Request["SubmitDateStart"] != null)
                    this.UCDateInputBeginDate.SelectedDate = this.Request["SubmitDateStart"].ToString();
                if (this.Request["SubmitDateEnd"] != null)
                    this.UCDateInputEndDate.SelectedDate = this.Request["SubmitDateEnd"].ToString();
                btnSearch_Click(null, null);
            }
        }
    }

    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // �������н��и�ֵ
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormSaleApplyViewRow row = (QueryDS.FormSaleApplyViewRow)drvDetail.Row;

                Label lblProjectName = (Label)e.Row.FindControl("lblProjectName");
                if (!row.IsProjectNameNull()) {
                    if (row.ProjectName.Length >= 20) {
                        lblProjectName.Text = row.ProjectName.Substring(0, 20) + "...";
                    } else {
                        lblProjectName.Text = row.ProjectName;
                    }
                }

                LinkButton lblFormNo = (LinkButton)e.Row.FindControl("lblFormNo");
                if (this.ViewState["SearchCondition"] != null) {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/FormSale/SaleApplySelectList.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/FormSale/SaleApplySelectList.aspx");
                }

            }
        }
    }

    protected void CreateBtn_Click(object sender, EventArgs e) {
        string strFormID = string.Empty;
        string strFormNo = string.Empty;
        bool isSameCustID = true;
        bool isSameBrandID = true;
        bool isSameYear = true;
        bool isSameSubCategoryID = true;
        bool isSameCurrencyID = true;
        bool isSameCostCenter = true;
        int custID = 0;
        int brandID = 0;
        int year = 0;
        int subCategoryID = 0;
        int currencyID = 0;
        int pageType = 0;
        decimal NeedCreatePOAmount = Convert.ToDecimal(ConfigurationManager.AppSettings["NeedCreatePOAmount"]);
        int costCenterID = 0;
        String NeedPOFormNo = string.Empty;

        MasterDataBLL masterBLL = new MasterDataBLL();
        FormPurchaseBLL purchaseBLL = new FormPurchaseBLL();
        string intFormID = string.Empty;
        foreach (GridViewRow row in this.gvApplyList.Rows) {
            CheckBox CheckCtl = (CheckBox)row.FindControl("CheckCtl");
            if (CheckCtl.Checked) {
                QueryDS.FormSaleApplyViewRow formApply = new FormQueryBLL().GetFormSaleApplyViewByID((int)this.gvApplyList.DataKeys[row.RowIndex].Value);
                FormDS.FormRow form = new FormSaleBLL().GetFormByID((int)this.gvApplyList.DataKeys[row.RowIndex].Value)[0];
                intFormID = formApply.FormID.ToString();
                pageType = formApply.PageType;
                if (custID == 0) {
                    custID = formApply.CustomerID;
                } else {
                    if (custID != formApply.CustomerID) {
                        isSameCustID = false;
                        break;
                    }
                }
                if (brandID == 0) {
                    brandID = formApply.BrandID;
                } else {
                    if (brandID != formApply.BrandID) {
                        isSameBrandID = false;
                        break;
                    }
                }
                if (year == 0) {
                    year = formApply.FPeriod.AddMonths(5).Year;
                } else {
                    if (year != formApply.FPeriod.AddMonths(5).Year) {
                        isSameYear = false;
                        break;
                    }
                }
                if (subCategoryID == 0) {
                    subCategoryID = formApply.ExpenseSubCategoryID;
                } else {
                    if (subCategoryID != formApply.ExpenseSubCategoryID) {
                        isSameSubCategoryID = false;
                        break;
                    }
                }
                if (currencyID == 0) {
                    currencyID = formApply.CurrencyID;
                } else {
                    if (currencyID != formApply.CurrencyID) {
                        isSameCurrencyID = false;
                        break;
                    }
                }
                if (strFormID == string.Empty) {
                    strFormID = this.gvApplyList.DataKeys[row.RowIndex].Value.ToString();
                } else {
                    strFormID = strFormID + "," + this.gvApplyList.DataKeys[row.RowIndex].Value.ToString();
                }
                if (strFormNo == string.Empty) {
                    strFormNo = "P" + form.FormNo + "P";
                } else {
                    strFormNo = strFormNo + "P" + form.FormNo + "P";
                }
                if (costCenterID == 0) {
                    costCenterID = formApply.CostCenterID;
                } else {
                    if (costCenterID != formApply.CostCenterID) {
                        isSameCostCenter = false;
                        break;
                    }
                }

                //�жϽ�����X����Ҫ�½�PO
                MasterData.ExpenseCategoryRow ecr = masterBLL.GetExpenseCategoryById(masterBLL.GetExpenseSubCategoryById(formApply.ExpenseSubCategoryID).ExpenseCategoryID);
                if (formApply.AmountRMB > NeedCreatePOAmount && ecr.NeedPO) {
                    if (purchaseBLL.QueryPOCountByParentFormID(formApply.FormID) <= 0) {
                        NeedPOFormNo += formApply.FormNo + ",";
                    }
                }
            }
        }

        if (strFormID == string.Empty) {
            PageUtility.ShowModelDlg(this, "��ѡ�����뵥!");
            return;
        }

        if (!isSameCustID) {
            PageUtility.ShowModelDlg(this, "��ѡ����ͬ�ͻ������뵥!");
            return;
        }
        if (!isSameBrandID) {
            PageUtility.ShowModelDlg(this, "��ѡ����ͬBrand�����뵥!");
            return;
        }
        if (!isSameYear) {
            PageUtility.ShowModelDlg(this, "��ѡ��ͬһ������뵥!");
            return;
        }
        if (!isSameSubCategoryID) {
            PageUtility.ShowModelDlg(this, "��ѡ��ͬһ����С������뵥!");
            return;
        }
        if (!isSameCurrencyID) {
            PageUtility.ShowModelDlg(this, "��ѡ��ͬһ���ֵ����뵥!");
            return;
        }
        if (!isSameCostCenter) {
            PageUtility.ShowModelDlg(this, "��ѡ��ͬһ�ɱ����ĵ����뵥!");
            return;
        }
        if (NeedPOFormNo != string.Empty) {
            PageUtility.ShowModelDlg(this, string.Format("���뵥{0}������{1}����Ҫ����PO��", NeedPOFormNo, NeedCreatePOAmount));
            return;
        }

        string url = string.Empty;

        if (pageType == (int)SystemEnums.PageType.ActivityApply) {
            if (this.ViewState["SearchCondition"] != null) {
                url = "~/FormSale/ActivitySettlementApply.aspx?FormApplyIds=" + strFormID +
                     "&CustomerID=" + custID.ToString() + "&BrandID=" + brandID.ToString() + "&ExpenseSubCategoryID=" + subCategoryID + "&CurrencyID=" + currencyID+"&CostCenterID="+costCenterID + "&FormApplyNos=" + strFormNo + "&Source=" + HttpUtility.UrlEncode("~/FormSale/SaleApplySelectList.aspx?" + this.ViewState["SearchCondition"].ToString());
            } else {
                url = "~/FormSale/ActivitySettlementApply.aspx?FormApplyIds=" + strFormID +
                     "&CustomerID=" + custID.ToString() + "&BrandID=" + brandID.ToString() + "&ExpenseSubCategoryID=" + subCategoryID + "&CurrencyID=" + currencyID + "&CostCenterID=" + costCenterID + "&FormApplyNos=" + strFormNo + "&Source=" + HttpUtility.UrlEncode("~/FormSale/SaleApplySelectList.aspx");
            }
        } else {
            if (this.ViewState["SearchCondition"] != null) {
                url = "~/FormSale/NoActivitySettlementApply.aspx?FormApplyIds=" + strFormID +
                     "&CustomerID=" + custID.ToString() + "&BrandID=" + brandID.ToString() + "&ExpenseSubCategoryID=" + subCategoryID + "&CurrencyID=" + currencyID + "&CostCenterID=" + costCenterID + "&FormApplyNos=" + strFormNo + "&Source=" + HttpUtility.UrlEncode("~/FormSale/SaleApplySelectList.aspx?" + this.ViewState["SearchCondition"].ToString());
            } else {
                url = "~/FormSale/NoActivitySettlementApply.aspx?FormApplyIds=" + strFormID +
                     "&CustomerID=" + custID.ToString() + "&BrandID=" + brandID.ToString() + "&ExpenseSubCategoryID=" + subCategoryID + "&CurrencyID=" + currencyID + "&CostCenterID=" + costCenterID + "&FormApplyNos=" + strFormNo + "&Source=" + HttpUtility.UrlEncode("~/FormSale/SaleApplySelectList.aspx");
            }
        }
        Response.Redirect(url);
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            if (this.Request["StartIndex"] != null) {
                string start = this.Request["StartIndex"].ToString();
                this.gvApplyList.PageIndex = int.Parse(start);
            }
            this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
            this.gvApplyList.DataBind();
        }
    }

    private string getSearchCondition() {
        int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
        string filterStr = this.ViewState["DefaultFilter"].ToString();
        this.ViewState["SearchCondition"] = "Search=true";
        //���뵥���
        if (txtFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND FormNo like '%" + this.txtFormNo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormNo=" + this.txtFormNo.Text.Trim();
        }

        //����������
        if (txtStuffUser.Text.Trim() != string.Empty) {
            filterStr += " AND StuffName like '%" + this.txtStuffUser.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&StuffName=" + this.txtStuffUser.Text.Trim();
        }

        //�ͻ�
        if (this.UCCustomer.CustomerID != string.Empty && this.UCCustomer.CustomerID != null) {
            filterStr += " AND CustomerID = " + this.UCCustomer.CustomerID;
            this.ViewState["SearchCondition"] += "&CustomerID=" + this.UCCustomer.CustomerID;
        }

        //����С��
        if (!this.ExpenseSubCategoryDDL.SelectedValue.Equals("0")) {
            filterStr += " AND ExpenseSubCategoryID = " + ExpenseSubCategoryDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&ExpenseSubCategoryID=" + ExpenseSubCategoryDDL.SelectedValue;
        }

        //֧����ʽ
        if (!this.BrandDDL.SelectedValue.Equals("0")) {
            filterStr += " AND BrandID = " + BrandDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&BrandID=" + BrandDDL.SelectedValue;
        }

        //�����ڼ�
        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        if (startPeriod != null && startPeriod != string.Empty) {
            string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND FPeriod >='" + startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01'" +
                " AND FPeriod<='" + endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01'";
            this.ViewState["SearchCondition"] += "&PeriodStart=" + startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01"
               + "&PeriodEnd=" + endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01";
        }

        //��������
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND SubmitDate >='" + start + "' AND dateadd(day,-1,SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
        }

        return filterStr;
    }

    protected bool checkSearchConditionValid() {
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();

        if (start == null || start == string.Empty) {
            if (end != null && end != string.Empty) {
                PageUtility.ShowModelDlg(this, "��ѡ�������ύ��ʼ����!");
                return false;
            }
        } else {
            if (end == null || end == string.Empty) {
                PageUtility.ShowModelDlg(this, "��ѡ�������ύ��ֹ����!");
                return false;
            } else {
                DateTime dtStart = DateTime.Parse(start);
                DateTime dtEnd = DateTime.Parse(end);
                if (dtStart > dtEnd) {
                    PageUtility.ShowModelDlg(this, "��ʼ���ڴ��ڽ�ֹ���ڣ�");
                    return false;
                }
            }
        }

        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();

        if (startPeriod == null || startPeriod == string.Empty) {
            if (endPeriod != null && endPeriod != string.Empty) {
                PageUtility.ShowModelDlg(this, "��ѡ����ʼ�����ڼ�!");
                return false;
            }
        } else {
            if (endPeriod == null || endPeriod == string.Empty) {
                PageUtility.ShowModelDlg(this, "��ѡ���ֹ�����ڼ�!");
                return false;
            } else {
                DateTime dtstartPeriod = DateTime.Parse(startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01");
                DateTime dtendPeriod = DateTime.Parse(endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01");
                if (dtstartPeriod > dtendPeriod) {
                    PageUtility.ShowModelDlg(this, "��ʼ�����ڼ���ڽ�ֹ�����ڼ䣡");
                    return false;
                }
            }
        }

        return true;
    }

}
