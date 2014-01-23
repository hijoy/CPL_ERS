using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessObjects;
using System.Text;
using lib.wf;

/// <summary>
/// Summary description for CommonUtility
/// </summary>
public class CommonUtility {
    public static string GetPathName() {
        return System.Configuration.ConfigurationManager.AppSettings["UploadDirectory"];
    }

    public static string GetStatusName(int statusID) {
        string returnStr = string.Empty;
        switch (statusID) {
            case (int)SystemEnums.FormStatus.Awaiting:
                returnStr = "待审批";
                break;
            case (int)SystemEnums.FormStatus.ApproveCompleted:
                returnStr = "审批完成";
                break;
            case (int)SystemEnums.FormStatus.Rejected:
                returnStr = "退回待修改";
                break;
            case (int)SystemEnums.FormStatus.Scrap:
                returnStr = "作废";
                break;
        }
        return returnStr;
    }

    public static string GetVendorActionTypeName(int actionTypeID) {
        string returnStr = string.Empty;
        switch (actionTypeID) {
            case (int)SystemEnums.VendorActionType.Delete:
                returnStr = "删除";
                break;
            case (int)SystemEnums.VendorActionType.Add:
                returnStr = "增加";
                break;
            case (int)SystemEnums.VendorActionType.Edit:
                returnStr = "修改";
                break;
            case (int)SystemEnums.VendorActionType.Reactive:
                returnStr = "重新激活";
                break;
        }
        return returnStr;
    }

    public static string GetFormPostBackUrl(int FormID, int PageType, int StatusID, string SourceUrl) {
        string returnStr = string.Empty;
        if (StatusID == (int)SystemEnums.FormStatus.Draft) {
            switch (PageType) {
                case (int)SystemEnums.PageType.TravelReimburseApply:
                    returnStr = "~/FormTE/FormTravelReimburseApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.FormInvitationApply:
                    returnStr = "~/FormTE/FormInvitationApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.FormInvitationReimburse:
                    returnStr = "~/FormTE/FormInvitationReimburseApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.FormPersonalReimburse:
                    returnStr = "~/FormTE/FormPersonalReimburseApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.ActivityApply:
                    returnStr = "~/FormSale/ActivityApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.NoActivityApply:
                    returnStr = "~/FormSale/NoActivityApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.ActivitySettlementApply:
                    returnStr = "~/FormSale/ActivitySettlementApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.NoActivitySettlementApply:
                    returnStr = "~/FormSale/NoActivitySettlementApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.VendorApply:
                    returnStr = "~/FormPurchase/FormVendorApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.ActivityAdvancedPayment:
                    returnStr = "~/FormSale/ActivityAdvancedPaymentApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.NoActivityAdvancedPayment:
                    returnStr = "~/FormSale/NoActivityAdvancedPaymentApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.PaymentCash:
                    returnStr = "~/FormSale/PaymentCashApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.PaymentFreeGoods:
                    returnStr = "~/FormSale/PaymentFreeGoodsApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.RDApply:
                    returnStr = "~/FormRD/RDApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.RDPayment:
                    returnStr = "~/FormRD/RDPaymentApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.PRApply:
                    returnStr = "~/FormPurchase/PRApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.POApply:
                    returnStr = "~/FormPurchase/POApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.PVApply:
                    returnStr = "~/FormPurchase/PVApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.SpecialPV:
                    returnStr = "~/FormPurchase/PVSpecialApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.SpecialPOApply:
                    returnStr = "~/FormPurchase/POSpecialApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.FormMarketingApply:
                    returnStr = "~/FormMarketing/MarketingApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.FormMArketingPayment:
                    returnStr = "~/FormMarketing/MarketingPaymentApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.SaleSampleRequest:
                    returnStr = "~/SampleRequest/SaleSampleRequestApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.MarketingSampleRequest:
                    returnStr = "~/SampleRequest/MarketingSampleRequestApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.RDSampleRequest:
                    returnStr = "~/SampleRequest/RDSampleRequestApply.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
            }
        } else {
            switch (PageType) {
                case (int)SystemEnums.PageType.TravelReimburseApply:
                    returnStr = "~/FormTE/FormTravelReimburseApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.FormInvitationApply:
                    returnStr = "~/FormTE/FormInvitationApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.FormInvitationReimburse:
                    returnStr = "~/FormTE/FormInvitationReimburseApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.FormPersonalReimburse:
                    returnStr = "~/FormTE/FormPersonalReimburseApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.ActivityApply:
                    returnStr = "~/FormSale/ActivityApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.NoActivityApply:
                    returnStr = "~/FormSale/NoActivityApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.ActivitySettlementApply:
                    returnStr = "~/FormSale/SettlementApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.NoActivitySettlementApply:
                    returnStr = "~/FormSale/SettlementApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.VendorApply:
                    returnStr = "~/FormPurchase/FormVendorApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.ActivityAdvancedPayment:
                    returnStr = "~/FormSale/ActivityAdvancedPaymentApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.NoActivityAdvancedPayment:
                    returnStr = "~/FormSale/NoActivityAdvancedPaymentApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.PaymentCash:
                    returnStr = "~/FormSale/PaymentCashApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.PaymentFreeGoods:
                    returnStr = "~/FormSale/PaymentFreeGoodsApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.RDApply:
                    returnStr = "~/FormRD/RDApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.RDPayment:
                    returnStr = "~/FormRD/RDPaymentApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.PRApply:
                    returnStr = "~/FormPurchase/PRApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.POApply:
                    returnStr = "~/FormPurchase/POApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.PVApply:
                    returnStr = "~/FormPurchase/PVApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.SpecialPV:
                    returnStr = "~/FormPurchase/PVSpecialApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.SpecialPOApply:
                    returnStr = "~/FormPurchase/POSpecialApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.FormMarketingApply:
                    returnStr = "~/FormMarketing/MarketingApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.FormMArketingPayment:
                    returnStr = "~/FormMarketing/MarketingPaymentApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.SaleSampleRequest:
                    returnStr = "~/SampleRequest/SaleSampleRequestApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.MarketingSampleRequest:
                    returnStr = "~/SampleRequest/MarketingSampleRequestApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
                case (int)SystemEnums.PageType.RDSampleRequest:
                    returnStr = "~/SampleRequest/RDSampleRequestApproval.aspx?ObjectId=" + FormID + SourceUrl;
                    break;
            }
        }
        return returnStr;
    }

    public static string ByteSubString(string strInput, int startIndex, int length) {
        strInput = strInput.Trim();
        int byteLen = Encoding.Default.GetByteCount(strInput);
        if (byteLen > length) {
            string resultStr = String.Empty;
            for (int i = startIndex / 2; i < strInput.Length; i++) {
                if (Encoding.Default.GetByteCount(resultStr) < length) {
                    resultStr += strInput.Substring(i, 1);
                } else {
                    break;
                }
            }
            return resultStr;
        } else {
            return strInput;
        }
    }

    public static string GetStaffFullName(AuthorizationDS.StuffUserRow Staff) {
        if (Staff.IsEnglishNameNull()) {
            return Staff.StuffName;
        } else {
            return Staff.StuffName + "(" + Staff.EnglishName + ")";
        }
    }

    public static string GetMAACostCenterFullName(int CostCenterID) {
        MasterData.CostCenterRow cc = new MasterDataBLL().GetCostCenterById(CostCenterID);
        return new MasterDataBLL().GetCompanyById(cc.CompanyID).CompanyCode + "-" + cc.CostCenterName + "-" + cc.CostCenterCode;
    }

    public static string GetReportPostBackUrl(int FormID, int PageType) {
        string returnStr = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/Reports/DocumentPrint.aspx?FormID=" + FormID + "&";
        switch (PageType) {
            case (int)SystemEnums.PageType.FormPersonalReimburse:
                returnStr += "ReportName=DocumentPrintTE";
                break;
            case (int)SystemEnums.PageType.TravelReimburseApply:
                returnStr += "ReportName=DocumentPrintTravel";
                break;
            case (int)SystemEnums.PageType.FormMArketingPayment:
                returnStr += "ReportName=DocumentPrintMarketingPayment";
                break;
            case (int)SystemEnums.PageType.PVApply:
                returnStr += "ReportName=DocumentPrintPV";
                break;
            case (int)SystemEnums.PageType.PRApply:
                returnStr += "ReportName=DocumentPrintPR";
                break;
            case (int)SystemEnums.PageType.POApply:
                returnStr += "ReportName=DocumentPrintPO";
                break;
            case (int)SystemEnums.PageType.SpecialPOApply:
                returnStr += "ReportName=DocumentPrintPO";
                break;
            case (int)SystemEnums.PageType.SpecialPV:
                returnStr += "ReportName=DocumentPrintPV";
                break;
            case (int)SystemEnums.PageType.RDPayment:
                returnStr += "ReportName=DocumentPrintRD";
                break;
            case (int)SystemEnums.PageType.PaymentCash:
                returnStr += "ReportName=DocumentPrintSaleCash";
                break;
            case (int)SystemEnums.PageType.PaymentFreeGoods:
                returnStr += "ReportName=DocumentPrintSaleFreeGoods";
                break;
        }
        return returnStr;
    }

    public static string GetPOPostBackUrl(int FormID) {
        PurchaseDS.FormPORow rowPO = new FormPurchaseBLL().GetFormPOByID(FormID);
        string returnStr = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormPurchase/{0}?ShowDialog=1&ObjectId=" + FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
        switch (rowPO.POType) {
            case (int)SystemEnums.POType.Marketing:
            case (int)SystemEnums.POType.RD:
            case (int)SystemEnums.POType.Sale:
                returnStr = string.Format(returnStr, "POSpecialApproval.aspx");
                break;
            case (int)SystemEnums.POType.Purchase:
                returnStr = string.Format(returnStr, "POApproval.aspx");
                break;
        }
        return returnStr;
    }

    public static bool CheckPeriod(int FormID) {
        FormDS.FormRow rowForm = new FormSaleBLL().GetFormByID(FormID)[0];
        DateTime Period = DateTime.Now;
        switch (rowForm.FormTypeID) {
            case (int)SystemEnums.FormType.FormMarketingApply:
                Period = new FormMarketingBLL().GetFormMarketingApplyByID(rowForm.FormID)[0].FPeriod;
                break;
            case (int)SystemEnums.FormType.RDApply:
                Period = new FormRDBLL().GetFormRDApplyByID(rowForm.FormID)[0].FPeriod;
                break;
            case (int)SystemEnums.FormType.SaleApply:
                Period = new FormSaleBLL().GetFormSaleApplyByID(rowForm.FormID)[0].FPeriod;
                break;
        }
        MasterData.PeriodSaleDataTable tbPeriodSale = new MasterDataBLL().GetPeriodSale();
        foreach (var rowPeriodSale in tbPeriodSale) {
            if (rowPeriodSale.PeriodSale == Period) {
                return false;
            }
        }
        return true;
    }

}
