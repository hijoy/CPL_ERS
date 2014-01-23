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
using System.IO;
using System.Text;

public partial class FormPurchase_VendorManage : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);

            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Vendor, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Vendor, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool hasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            this.ViewState["hasManageRight"] = hasManageRight;
            if (!positionRightBLL.CheckPositionRight(position.PositionId, opViewId)&&(!hasManageRight)) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }

    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        bool hasManageRight = (bool)this.ViewState["hasManageRight"];
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                MasterData.VendorRow row = (MasterData.VendorRow)drvDetail.Row;

                HyperLink hlDetail = (HyperLink)e.Row.FindControl("hlDetail");
                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                LinkButton lbtnActive = (LinkButton)e.Row.FindControl("lbtnReactive");
                if ((!hasManageRight)||(!row.IsActive)) {
                    lbtnEdit.Visible = false;
                    lbtnDelete.Visible = false;
                }
                if (hasManageRight && !row.IsActive) {
                    lbtnActive.Visible = true;
                } else {
                    lbtnActive.Visible = false;
                }

                if (this.ViewState["SearchCondition"] != null) {
                    hlDetail.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/Dialog/VendorDetail.aspx?ShowDialog=1&VendorID=" + row.VendorID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                    lbtnEdit.PostBackUrl = "~/FormPurchase/FormVendorApply.aspx?VendorId=" + row.VendorID + "&ActionType=" + (int)SystemEnums.VendorActionType.Edit +
                    "&Source=" + HttpUtility.UrlEncode("~/FormPurchase/VendorManage.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvVendorList.PageIndex);
                    lbtnDelete.PostBackUrl = "~/FormPurchase/FormVendorApply.aspx?VendorId=" + row.VendorID + "&ActionType=" + (int)SystemEnums.VendorActionType.Delete +
                        "&Source=" + HttpUtility.UrlEncode("~/FormPurchase/VendorManage.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvVendorList.PageIndex);
                    lbtnActive.PostBackUrl = "~/FormPurchase/FormVendorApply.aspx?VendorId=" + row.VendorID + "&ActionType=" + (int)SystemEnums.VendorActionType.Reactive +
                        "&Source=" + HttpUtility.UrlEncode("~/FormPurchase/VendorManage.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvVendorList.PageIndex);

                } else {
                    hlDetail.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/Dialog/VendorDetail.aspx?ShowDialog=1&VendorID=" + row.VendorID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                    lbtnEdit.PostBackUrl = "~/FormPurchase/FormVendorApply.aspx?VendorId=" + row.VendorID + "&ActionType=" + (int)SystemEnums.VendorActionType.Edit;
                    lbtnDelete.PostBackUrl = "~/FormPurchase/FormVendorApply.aspx?VendorId=" + row.VendorID + "&ActionType=" + (int)SystemEnums.VendorActionType.Delete;
                    lbtnActive.PostBackUrl = "~/FormPurchase/FormVendorApply.aspx?VendorId=" + row.VendorID + "&ActionType=" + (int)SystemEnums.VendorActionType.Reactive;
                }
            }
        }
    }

    protected void gvHistory_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormVendorViewRow row = (QueryDS.FormVendorViewRow)drvDetail.Row;

                HyperLink lbtnFormNo = (HyperLink)e.Row.FindControl("lblFormNo");
                lbtnFormNo.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormPurchase/FormVendorApproval.aspx?ShowDialog=1&ObjectId=" + row.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";

                Label lblActionType = (Label)e.Row.FindControl("lblActionType");
                lblActionType.Text = CommonUtility.GetVendorActionTypeName(row.ActionType);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (this.Request["StartIndex"] != null) {
            string start = this.Request["StartIndex"].ToString();
            this.gvVendorList.PageIndex = int.Parse(start);
        }
        this.odsVendorList.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
        this.gvVendorList.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e) {
        try {
            StringWriter swCSV = new StringWriter();

            QueryDS.Export_VendorDataTable tbVendorView = new MasterDataBLL().GetVendorViewByExport(this.getSearchCondition());

            StringBuilder sbText = new StringBuilder();
            sbText = AppendCSVFields(sbText, "VNSTAT");
            sbText = AppendCSVFields(sbText, "VMID");
            sbText = AppendCSVFields(sbText, "VENDOR");
            sbText = AppendCSVFields(sbText, "VNDNAM");
            sbText = AppendCSVFields(sbText, "VNDAD1");
            sbText = AppendCSVFields(sbText, "VNDAD2");
            sbText = AppendCSVFields(sbText, "VCITY");
            sbText = AppendCSVFields(sbText, "VSTATE");
            sbText = AppendCSVFields(sbText, "VPOST");
            sbText = AppendCSVFields(sbText, "VTERMS");
            sbText = AppendCSVFields(sbText, "VTYPE");
            sbText = AppendCSVFields(sbText, "VPAYTO");
            sbText = AppendCSVFields(sbText, "VDTLPD	");
            sbText = AppendCSVFields(sbText, "VDAYCL");
            sbText = AppendCSVFields(sbText, "VGL");
            sbText = AppendCSVFields(sbText, "V1099");
            sbText = AppendCSVFields(sbText, "V1099C	");
            sbText = AppendCSVFields(sbText, "VPHONE");
            sbText = AppendCSVFields(sbText, "VCMPNY");
            sbText = AppendCSVFields(sbText, "VCURR");
            sbText = AppendCSVFields(sbText, "VPAYTY");
            sbText = AppendCSVFields(sbText, "V1TIME");
            sbText = AppendCSVFields(sbText, "VCORPV");
            sbText = AppendCSVFields(sbText, "VHOLD");
            sbText = AppendCSVFields(sbText, "VHOLDT");
            sbText = AppendCSVFields(sbText, "VPYTYR");
            sbText = AppendCSVFields(sbText, "VDSCAV");
            sbText = AppendCSVFields(sbText, "VDSCTK");
            sbText = AppendCSVFields(sbText, "VDPURS");
            sbText = AppendCSVFields(sbText, "VNNEXT");
            sbText = AppendCSVFields(sbText, "VNGEN");
            sbText = AppendCSVFields(sbText, "VNALPH	");
            sbText = AppendCSVFields(sbText, "VNUNAL");
            sbText = AppendCSVFields(sbText, "VCON");
            sbText = AppendCSVFields(sbText, "VCOUN");
            sbText = AppendCSVFields(sbText, "V1099S");
            sbText = AppendCSVFields(sbText, "VPAD1");
            sbText = AppendCSVFields(sbText, "VPAD2");
            sbText = AppendCSVFields(sbText, "VPCTY");
            sbText = AppendCSVFields(sbText, "VPSTE");
            sbText = AppendCSVFields(sbText, "VPPST");
            sbText = AppendCSVFields(sbText, "VPCON");
            sbText = AppendCSVFields(sbText, "VPCOU");
            sbText = AppendCSVFields(sbText, "VMFRM");
            sbText = AppendCSVFields(sbText, "VMMAT");
            sbText = AppendCSVFields(sbText, "VTAX");
            sbText = AppendCSVFields(sbText, "VPPHN");
            sbText = AppendCSVFields(sbText, "VMFSCD");
            sbText = AppendCSVFields(sbText, "VMIDNM");
            sbText = AppendCSVFields(sbText, "VTAXCD");
            sbText = AppendCSVFields(sbText, "VMXNBR");
            sbText = AppendCSVFields(sbText, "VMXCRT");
            sbText = AppendCSVFields(sbText, "VMXDTE");
            sbText = AppendCSVFields(sbText, "VMXMAX");
            sbText = AppendCSVFields(sbText, "VFOB");
            sbText = AppendCSVFields(sbText, "VMCLAS");
            sbText = AppendCSVFields(sbText, "VMRATE");
            sbText = AppendCSVFields(sbText, "VMDCPX");
            sbText = AppendCSVFields(sbText, "VMSRCC");
            sbText = AppendCSVFields(sbText, "VMSRNO");
            sbText = AppendCSVFields(sbText, "VMPREQ");
            sbText = AppendCSVFields(sbText, "VXMID");
            sbText = AppendCSVFields(sbText, "VXNUMB");
            sbText = AppendCSVFields(sbText, "VXCUR");
            sbText = AppendCSVFields(sbText, "VXBKCD");
            sbText = AppendCSVFields(sbText, "VXTYP");
            sbText = AppendCSVFields(sbText, "VXBNKN");
            sbText = AppendCSVFields(sbText, "VXACT");
            sbText = AppendCSVFields(sbText, "VXB2BA");
            sbText = AppendCSVFields(sbText, "VXCCB");
            sbText = AppendCSVFields(sbText, "VXCCBB");

            //去掉尾部的逗号
            sbText.Remove(sbText.Length - 1, 1);

            //写datatable的一行
            swCSV.WriteLine(sbText.ToString());

            //遍历datatable导出数据
            foreach (DataRow drTemp in tbVendorView.Rows) {
                sbText = new StringBuilder();

                sbText = AppendCSVFields(sbText, drTemp["VNSTAT"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMID"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VENDOR"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VNDNAM"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VNDAD1"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VNDAD2"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VCITY"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VSTATE"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPOST"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VTERMS"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VTYPE"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPAYTO"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VDTLPD"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VDAYCL"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VGL"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["V1099"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["V1099C"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPHONE"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VCMPNY"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VCURR"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPAYTY"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["V1TIME"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VCORPV"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VHOLD"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VHOLDT"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPYTYR"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VDSCAV"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VDSCTK"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VDPURS"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VNNEXT"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VNGEN"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VNALPH"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VNUNAL"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VCON"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VCOUN"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["V1099S"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPAD1"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPAD2"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPCTY"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPSTE"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPPST"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPCON"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPCOU"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMFRM"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMMAT"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VTAX"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VPPHN"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMFSCD"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMIDNM"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VTAXCD"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMXNBR"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMXCRT"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMXDTE"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMXMAX"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VFOB"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMCLAS"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMRATE"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMDCPX"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMSRCC"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMSRNO"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMPREQ"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VMID"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VXNUMB"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VXCUR"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VXBKCD"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VXTYP"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VXBNKN"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VXACT"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VXB2BA"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VXCCB"].ToString());
                sbText = AppendCSVFields(sbText, drTemp["VXCCBB"].ToString());

                //去掉尾部的逗号
                sbText.Remove(sbText.Length - 1, 1);

                //写datatable的一行
                swCSV.WriteLine(sbText.ToString());
            }
            //下载文件
            DownloadFile(Response, swCSV.GetStringBuilder(), "Vendor_Export_" + DateTime.Now.ToString("yyyyMMdd") + ".csv");

            swCSV.Close();
            Response.End();

        } catch (Exception ex) {
            throw ex;
        }
    }

    /// <summary>
    /// csv添加逗号 用来区分列
    /// </summary>
    /// <param name="argFields">字段</param>
    /// <returns>添加后内容</returns>
    private StringBuilder AppendCSVFields(StringBuilder argSource, string argFields) {
        return argSource.Append(argFields.Replace(",", " ").Trim()).Append(",");
    }

    /// <summary>
    /// 弹出下载框
    /// </summary>
    /// <param name="argResp">弹出页面</param>
    /// <param name="argFileStream">文件流</param>
    /// <param name="strFileName">文件名</param>
    public static void DownloadFile(HttpResponse argResp, StringBuilder argFileStream, string strFileName) {
        try {
            string strResHeader = "attachment; filename=" + Guid.NewGuid().ToString() + ".csv";
            if (!string.IsNullOrEmpty(strFileName)) {
                strResHeader = "inline; filename=" + strFileName;
            }
            argResp.AppendHeader("Content-Disposition", strResHeader);//attachment说明以附件下载，inline说明在线打开
            argResp.ContentType = "application/ms-excel";
            argResp.ContentEncoding = Encoding.GetEncoding("GB2312"); // Encoding.UTF8;//
            argResp.Write(argFileStream);

        } catch (Exception ex) {
            throw ex;
        }
    }

    private string getSearchCondition() {
        string filterStr = " 1=1";
        this.ViewState["SearchCondition"] = "Search=true";
        String temp;

        temp = this.txtVendorCode.Text.Trim();
        if (temp != string.Empty) {
            filterStr += " and VendorCode like '%" + temp + "%'";
            this.ViewState["SearchCondition"] += "&VendorCode=" + temp;
        }

        temp = this.txtVendorName.Text.Trim();
        if (temp != string.Empty) {
            filterStr += " and VendorName like '%" + temp + "%'";
            this.ViewState["SearchCondition"] += "&VendorName=" + temp;
        }

        temp = this.VendorTypeControl.VendorTypeID;
        if (temp != null && temp != string.Empty && temp != "0") {
            filterStr += " and VendorTypeID =" + temp;
            this.ViewState["SearchCondition"] += "&VendorTypeID=" + temp;
        }

        temp = this.ddlPaymentTerm.SelectedValue;
        if (temp != null && temp != string.Empty && temp != "0") {
            filterStr += " and PaymentTermID = " + temp;
            this.ViewState["SearchCondition"] += "&PaymentTermID=" + temp;
        }
        return filterStr;
    }

    protected override void OnLoadComplete(EventArgs e) {
        if (!IsPostBack) {
            if (Request["Search"] != null) {
                if (Request["VendorCode"] != null) {
                    this.txtVendorCode.Text = Request["VendorCode"];
                }
                if (Request["VendorName"] != null) {
                    this.txtVendorName.Text = Request["VendorName"];
                }
                if (Request["VendorTypeID"] != null) {
                    this.VendorTypeControl.VendorTypeID = Request["VendorTypeID"];
                }
                if (Request["PaymentTermID"] != null) {
                    this.ddlPaymentTerm.SelectedValue = Request["PaymentTermID"];
                }
                btnSearch_Click(null, null);
            }
        }
    }

    public string getVendorTypeNameByID(object ID) {
        return new MasterDataBLL().GetVendorTypeById((int)ID).VendorTypeName;
    }

    public string getMethodPaymentNameByID(object ID) {
        return new MasterDataBLL().GetMethodPaymentById((int)ID)[0].MethodPaymentName;
    }

    public string getPaymentTermNameByID(object ID) {
        return new MasterDataBLL().GetPaymentTermById((int)ID)[0].PaymentTermName;
    }

    protected void gvVendorList_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.gvVendorList.SelectedIndex >= 0) {
            this.odsHistory.SelectParameters[0].DefaultValue = this.gvVendorList.SelectedValue.ToString();
            this.gvHistory.Visible = true;
            this.gvHistory.DataBind();
        }
    }
}
