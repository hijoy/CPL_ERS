using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.IO;
using BusinessObjects;
using System.Threading;

namespace ExportWindowsService {
    public partial class ExportService : ServiceBase {

        private DateTime ExportDate;

        public ExportService() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            TimerStart();
        }
        private void TimerStart() {
            int interval = Convert.ToInt32(ConfigurationManager.AppSettings["ImporterService.TimeInterval"]);
            System.Timers.Timer timer1 = new System.Timers.Timer(1000 * interval * 60);//每隔 n * 0.5小时做一次
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(ExportData);
            timer1.Enabled = true;
            timer1.Start();
        }

        #region 导出主方法
        private string path = "";
        LogBLL logbll = new LogBLL();
        private void ExportData(object source, System.Timers.ElapsedEventArgs e) {
            string FileName = ConfigurationManager.AppSettings["ExportService.FileName"];
            string ExportHour = ConfigurationManager.AppSettings["ExportService.hour"];
            string BakPath = ConfigurationManager.AppSettings["ExportService.BakPath"];

            ExportDate = DateTime.Now;

            path = ConfigurationManager.AppSettings["ExportService.Path"].ToString();
            bool Export = false;
            //File.AppendAllText(path + FileName, "Co,Vendor,Ref,Invoice,Inv Date,Inv Amt,Cur,Rate,Distribution Line Desc,Dist PC,Dist Account,Dist Amt,PO Number,PO Line Number,Comments,Application Form ID,Tax Amt,Tax Rate Code", Encoding.UTF8);
            if (ExportHour.IndexOf(',') >= 0) {
                for (int i = 0; i < ExportHour.Split(',').Length; i++) {
                    if (ExportHour.Split(',')[i].ToString() == DateTime.Now.Hour.ToString()) {
                        Export = true;
                        break;
                    }
                }
            } else {
                if (ExportHour.ToString() == DateTime.Now.Hour.ToString()) {
                    Export = true;
                }
            }
            if (Export) {
                FormRDBLL formrdbll = new FormRDBLL();
                FormPurchaseBLL formPurchaseBLL = new FormPurchaseBLL();
                FormQueryBLL formquerybll = new FormQueryBLL();
                FormMarketingBLL formmarkbll = new FormMarketingBLL();
                FormSaleBLL formsalebll = new FormSaleBLL();
                QueryDS.FormViewDataTable l_dtformview = formquerybll.GetPagedFormView(" Form.StatusID=2 and (Form.IsExportLock='false' or Form.IsExportLock is null) AND Form.FormTypeID=4 ", 0, Int32.MaxValue, "Form.SubmitDate");
                //个人
                int logId = logbll.InsertExportLog(1, FileName, 0, 0, 0, 0);
                int SuccessCount = 0;
                int FailCount = 0;
                int TotalCount = l_dtformview.Rows.Count;
                foreach (QueryDS.FormViewRow l_drformview in l_dtformview) {
                    if (ExportPersonalReimburseDataByFormID(l_drformview.FormID, logId)) {
                        UpdateFormbyID(l_drformview.FormID, true);
                        SuccessCount++;
                    } else {
                        UpdateFormbyID(l_drformview.FormID, false);
                        FailCount++;
                    }
                }
                logbll.UpdateExportLog(logId, TotalCount, SuccessCount, FailCount);



                //差旅费
                logId = logbll.InsertExportLog(2, FileName, 0, 0, 0, 0);
                SuccessCount = 0;
                FailCount = 0;
                l_dtformview = formquerybll.GetPagedFormView("Form.StatusID=2 and (Form.IsExportLock='false' or Form.IsExportLock is null) AND Form.FormTypeID =1 ", 0, Int32.MaxValue, "SubmitDate");
                TotalCount = l_dtformview.Rows.Count;
                foreach (QueryDS.FormViewRow l_drformview in l_dtformview) {
                    if (ExportTravelReimburseDataByFormID(l_drformview.FormID, logId)) {
                        UpdateFormbyID(l_drformview.FormID, true);
                        SuccessCount++;
                    } else {
                        UpdateFormbyID(l_drformview.FormID, false);
                        FailCount++;
                    }
                }
                logbll.UpdateExportLog(logId, TotalCount, SuccessCount, FailCount);


                //pv
                logId = logbll.InsertExportLog(3, FileName, 0, 0, 0, 0);
                SuccessCount = 0;
                FailCount = 0;
                l_dtformview = formquerybll.GetPagedFormView("Form.StatusID=2 and (Form.IsExportLock='false' or Form.IsExportLock is null) AND Form.FormTypeID =24 ", 0, Int32.MaxValue, "SubmitDate");
                TotalCount = l_dtformview.Rows.Count;
                foreach (QueryDS.FormViewRow l_drformview in l_dtformview) {
                    if (ExportPVDataByFormID(l_drformview.FormID, logId)) {
                        UpdateFormbyID(l_drformview.FormID, true);
                        SuccessCount++;
                    } else {
                        UpdateFormbyID(l_drformview.FormID, false);
                        FailCount++;
                    }
                }
                logbll.UpdateExportLog(logId, TotalCount, SuccessCount, FailCount);

                //sale
                logId = logbll.InsertExportLog(4, FileName, 0, 0, 0, 0);
                SuccessCount = 0;
                FailCount = 0;
                l_dtformview = formquerybll.GetPagedFormView("Form.StatusID=2 and (Form.IsExportLock='false' or Form.IsExportLock is null)  AND Form.FormTypeID in (13,14) and Form.FormID  in (select FormSalePaymentID from FormSalePayment where PaymentTypeID = 1)", 0, Int32.MaxValue, "SubmitDate");
                TotalCount = l_dtformview.Rows.Count;
                foreach (QueryDS.FormViewRow l_drformview in l_dtformview) {
                    if (ExportSaleDataByFormID(l_drformview.FormID, logId)) {
                        UpdateFormbyID(l_drformview.FormID, true);
                        SuccessCount++;
                    } else {
                        UpdateFormbyID(l_drformview.FormID, false);
                        FailCount++;
                    }
                }
                logbll.UpdateExportLog(logId, TotalCount, SuccessCount, FailCount);


                //market
                logId = logbll.InsertExportLog(5, FileName, 0, 0, 0, 0);
                SuccessCount = 0;
                FailCount = 0;
                l_dtformview = formquerybll.GetPagedFormView(" (Form.IsExportLock='false' or Form.IsExportLock is null)  AND Form.StatusID=2 AND Form.FormTypeID=42 AND  Form.FormID  in (select FormMarketingPaymentID from FormMarketingPayment where PaymentTypeID = 1)", 0, Int32.MaxValue, "SubmitDate");
                TotalCount = l_dtformview.Rows.Count;
                foreach (QueryDS.FormViewRow l_drformview in l_dtformview) {
                    if (ExportMarketingDataByFormID(l_drformview.FormID, logId)) {
                        UpdateFormbyID(l_drformview.FormID, true);
                        SuccessCount++;
                    } else {
                        UpdateFormbyID(l_drformview.FormID, false);
                        FailCount++;
                    }
                }
                logbll.UpdateExportLog(logId, TotalCount, SuccessCount, FailCount);

                //RD
                logId = logbll.InsertExportLog(6, FileName, 0, 0, 0, 0);
                SuccessCount = 0;
                FailCount = 0;
                l_dtformview = formquerybll.GetPagedFormView(" Form.StatusID=2 and (Form.IsExportLock='false' or Form.IsExportLock is null)  AND Form.FormTypeID=32 AND Form.FormID in (select FormRDPaymentID from FormRDPayment where PaymentTypeID = 1)", 0, Int32.MaxValue, "SubmitDate");
                TotalCount = l_dtformview.Rows.Count;
                foreach (QueryDS.FormViewRow l_drformview in l_dtformview) {
                    if (ExportRDDataByFormID(l_drformview.FormID, logId)) {
                        UpdateFormbyID(l_drformview.FormID, true);
                        SuccessCount++;
                    } else {
                        UpdateFormbyID(l_drformview.FormID, false);
                        FailCount++;
                    }
                }
                logbll.UpdateExportLog(logId, TotalCount, SuccessCount, FailCount);
                if (File.Exists(path + FileName)) {
                    File.Copy(path + FileName, BakPath + FileName.Split('.')[0] + DateTime.Now.ToString("yyyyMMdd") + "." + FileName.Split('.')[1], true);
                } else {
                    File.AppendAllText(path + FileName, "", Encoding.Default);
                }

                //Vendor
                FileName = ConfigurationManager.AppSettings["ExportService.VendorAVMFileName"];
                logId = logbll.InsertExportLog(8, FileName, 0, 0, 0, 0);
                SuccessCount = 0;
                FailCount = 0;
                MasterData.VendorDataTable tbVendor = new MasterDataBLL().GetVendorToExport();
                TotalCount = tbVendor.Rows.Count;
                foreach (MasterData.VendorRow vendor in tbVendor) {
                    if (ExportVendorAVM(vendor, logId)) {
                        SuccessCount++;
                    } else {
                        FailCount++;
                    }
                }
                logbll.UpdateExportLog(logId, TotalCount, SuccessCount, FailCount);
                if (File.Exists(path + FileName))
                    File.Copy(path + FileName, BakPath + FileName.Split('.')[0] + DateTime.Now.ToString("yyyyMMdd") + "." + FileName.Split('.')[1], true);
                else
                    File.AppendAllText(path + FileName, "", Encoding.Default);
                //Vendor
                FileName = ConfigurationManager.AppSettings["ExportService.VendorAVMXFileName"];
                logId = logbll.InsertExportLog(8, FileName, 0, 0, 0, 0);
                SuccessCount = 0;
                FailCount = 0;
                tbVendor = new MasterDataBLL().GetAllVendor();
                TotalCount = tbVendor.Rows.Count;
                foreach (MasterData.VendorRow vendor in tbVendor) {
                    if (ExportVendorAVMX(vendor, logId)) {
                        SuccessCount++;
                    } else {
                        FailCount++;
                    }
                }
                logbll.UpdateExportLog(logId, TotalCount, SuccessCount, FailCount);
                if (File.Exists(path + FileName))
                    File.Copy(path + FileName, BakPath + FileName.Split('.')[0] + DateTime.Now.ToString("yyyyMMdd") + "." + FileName.Split('.')[1], true);


                //PO
                string POFileName = ConfigurationManager.AppSettings["ExportService.POFileName"];
                string POLineFileName = ConfigurationManager.AppSettings["ExportService.POLineFileName"];
                logId = logbll.InsertExportLog(9, POFileName + " And " + POLineFileName, 0, 0, 0, 0);
                SuccessCount = 0;
                FailCount = 0;
                l_dtformview = formquerybll.GetPagedFormView(" Form.StatusID=2 and (Form.IsExportLock='false' or Form.IsExportLock is null)  AND Form.FormTypeID=23", 0, Int32.MaxValue, "SubmitDate");
                TotalCount = l_dtformview.Rows.Count;
                foreach (QueryDS.FormViewRow l_drformview in l_dtformview) {
                    PurchaseDS.FormPORow formPO = formPurchaseBLL.GetFormPOByID(l_drformview.FormID);
                    if (ExportPO(l_drformview.FormID, logId)) {
                        UpdateFormbyID(l_drformview.FormID, true);
                        SuccessCount++;
                    } else {
                        UpdateFormbyID(l_drformview.FormID, false);
                        FailCount++;
                    }
                }
                logbll.UpdateExportLog(logId, TotalCount, SuccessCount, FailCount);
                if (File.Exists(path + POFileName))
                    File.Copy(path + POFileName, BakPath + POFileName.Split('.')[0] + DateTime.Now.ToString("yyyyMMdd") + "." + POFileName.Split('.')[1], true);
                else
                    File.AppendAllText(path + POFileName, "", Encoding.Default);


                if (File.Exists(path + POLineFileName))
                    File.Copy(path + POLineFileName, BakPath + POLineFileName.Split('.')[0] + DateTime.Now.ToString("yyyyMMdd") + "." + POLineFileName.Split('.')[1], true);
                else
                    File.AppendAllText(path + POLineFileName, "", Encoding.Default);

            }
        }
        #endregion

        #region 截取byte
        public String bSubstring(string s, int length) {
            if (!string.IsNullOrEmpty(s)) {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(s);
                int n = 0;  //  表示当前的字节数
                int i = 0;  //  要截取的字节数
                for (; i < bytes.GetLength(0) && n < length; i++) {

                    //  偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节

                    if (i % 2 == 0) {
                        n++;      //  在UCS2第一个字节时n加1
                    } else {

                        //  当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节

                        if (bytes[i] > 0) {
                            n++;
                        }
                    }

                }

                //  如果i为奇数时，处理成偶数
                if (i % 2 == 1) {


                    //  该UCS2字符是汉字时，去掉这个截一半的汉字 
                    if (bytes[i] > 0)

                        i = i - 1;

                     //  该UCS2字符是字母或数字，则保留该字符

                    else
                        i = i + 1;
                }
                return System.Text.Encoding.Unicode.GetString(bytes, 0, i);
            } else {
                return string.Empty;
            }
        }
        #endregion

        #region 导出数据至文件
        private void ExportByFormView(ExportDataDS.ExportDataDataTable l_dtExportData) {
            string FileName = ConfigurationManager.AppSettings["ExportService.FileName"];
            string BakPath = ConfigurationManager.AppSettings["ExportService.BakPath"];
            string spit = "\t";
            foreach (ExportDataDS.ExportDataRow l_drExportData in l_dtExportData) {
                string Reference = "";
                string Invoice = "";
                string Invoicedate = "";
                if (!l_drExportData.IsReferenceNull()) {
                    Reference = bSubstring(l_drExportData.Reference, 10);
                }
                if (!l_drExportData.IsInvoiceNull()) {
                    Invoice = bSubstring(l_drExportData.Invoice, 10);
                }
                if (!l_drExportData.IsInvoicedateNull()) {
                    Invoicedate = bSubstring(l_drExportData.Invoicedate, 10);
                }
                string content = bSubstring(l_drExportData.CompanyCode, 2) + spit
                      + bSubstring(l_drExportData.VendorCode, 5) + spit
                      + bSubstring(Reference, 10) + spit
                      + bSubstring(Invoice, 10) + spit
                      + bSubstring(Invoicedate, 10) + spit
                      + l_drExportData.TotalMoney + spit
                      + "RMB" + spit
                      + "1.00" + spit
                      + bSubstring(l_drExportData.ManageExpenseItem, 20) + spit
                      + bSubstring(l_drExportData.CostCenter, 10) + spit
                      + bSubstring(l_drExportData.AccountCode, 20) + spit
                      + l_drExportData.DetailMoney + spit
                      + bSubstring(l_drExportData.PONumber.ToString(), 6) + spit
                      + bSubstring(l_drExportData.POLineNumber, 6) + spit
                      + bSubstring(l_drExportData.FinanceRemark, 60) + spit
                      + bSubstring(l_drExportData.FormNo, 60) + spit
                      + l_drExportData.TaxAmt + spit
                      + bSubstring(l_drExportData.TaxRateCode, 5) + spit
                      + ExportDate.ToString("yyyyMMddhhmmss");
                content = content.Replace("\r", "").Replace("\n", "") + "\r\n";
                File.AppendAllText(path + FileName, content, Encoding.Default);
                File.AppendAllText(BakPath + DateTime.Now.ToString("yyyyMMddhhmmss") + FileName, content, Encoding.Default);
            }

        }
        #endregion

        #region 个人费用报销
        /// <summary>
        /// 个人费用报销
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        private bool ExportPersonalReimburseDataByFormID(int FormID, int logId) {
            ExportDataDS.ExportDataDataTable l_dtExportData = new ExportDataDS.ExportDataDataTable();
            FormDS.FormPersonalReimburseDetailDataTable l_dtPersonalReimburseDetail = new FormDS.FormPersonalReimburseDetailDataTable();
            FormMarketingBLL formmarkbll = new FormMarketingBLL();
            FormTEBLL formtebll = new FormTEBLL();
            StuffUserBLL stuffuserdll = new StuffUserBLL();
            MasterDataBLL masterdatabll = new MasterDataBLL();
            FormDS.FormDataTable l_dtform = formmarkbll.GetFormByID(FormID);
            FormDS.FormRow l_drform = l_dtform.NewFormRow();
            try {
                if (l_dtform.Rows.Count > 0) {
                    l_drform = l_dtform[0];
                    if (l_drform.IsFinanceRemarkNull()) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：财务摘要为空，导出失败！");
                        return false;
                    }
                    l_dtPersonalReimburseDetail = formtebll.GetFormPersonalReimburseDetailByFormPersonalReimburseID(l_drform.FormID);
                    int i = 0;
                    FormDS.FormPersonalReimburseDataTable l_dtPersonalReimburse = formtebll.GetFormPersonalReimburseByID(l_drform.FormID);
                    foreach (FormDS.FormPersonalReimburseDetailRow l_drtemp in l_dtPersonalReimburseDetail) {
                        ExportDataDS.ExportDataRow l_drExportData = l_dtExportData.NewExportDataRow();
                        l_drExportData.FormID = FormID;
                        l_drExportData.VendorCode = stuffuserdll.GetStuffUserById(l_drform.UserID)[0].VendorCode;
                        l_drExportData.CompanyCode = masterdatabll.GetCompanyById(masterdatabll.GetCostCenterById(l_drform.CostCenterID).CompanyID).CompanyCode;
                        if (l_drExportData.IsVendorCodeNull() || string.IsNullOrEmpty(l_drExportData.VendorCode)) {
                            logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：Vendor Code为空，无法导出！");
                            return false;
                        }
                        //如果Vendor Code的公司和成本中心的公司不一致，则报错
                        MasterData.VendorDataTable tbVendor = new BusinessObjects.MasterDataTableAdapters.VendorTableAdapter().GetDataByVendorCode(l_drExportData.VendorCode);
                        if (tbVendor.Rows.Count > 0) {
                            string vendorCompanyCode = masterdatabll.GetCompanyById(tbVendor[0].CompanyID).CompanyCode;
                            if (l_drExportData.CompanyCode != vendorCompanyCode) {
                                logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：Company Code不符，请检查！");
                                return false;
                            }
                        }
                        l_drExportData.Reference = "";
                        l_drExportData.Invoice = l_drform.FormNo;
                        l_drExportData.Invoicedate = l_drform.SubmitDate.ToString("yyyyMMdd");
                        l_drExportData.TotalMoney = l_dtPersonalReimburse[0].Amount;
                        //l_drExportData.Currency = masterdatabll.GetCurrencyByID(l_drtemp.CurrencyID).CurrencyShortName;
                        //l_drExportData.ExchangeRate = l_drtemp.ExchangeRate;
                        l_drExportData.ManageExpenseItem = masterdatabll.GetManageExpenseItemById(l_drtemp.ManageExpenseItemID).ManageExpenseItemName;
                        l_drExportData.CostCenter = masterdatabll.GetCostCenterById(l_drform.CostCenterID).CostCenterCode;
                        l_drExportData.AccountCode = masterdatabll.GetAccountingCodeByExpenseItemAndCostCenter(l_drtemp.ManageExpenseItemID, l_drform.CostCenterID);
                        l_drExportData.DetailMoney = l_drtemp.RMB;
                        l_drExportData.PONumber = 0;
                        l_drExportData.POLineNumber = "0";
                        l_drExportData.FinanceRemark = l_drform.FinanceRemark;
                        l_drExportData.FormNo = l_drform.FormNo;
                        l_drExportData.TaxRateCode = masterdatabll.GetVatTypeById(1)[0].VatTypeName;
                        l_drExportData.TaxAmt = 0;
                        l_dtExportData.AddExportDataRow(l_drExportData);
                        i++;

                    }
                    if (l_dtExportData.Rows.Count == 0) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：无明细数据，无法导出！");
                        return false;
                    }
                    if (l_dtExportData.Rows.Count == l_dtPersonalReimburseDetail.Rows.Count) {
                        ExportByFormView(l_dtExportData);
                    } else {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：导出数据与明细数据数量不一致！");
                        return false;
                    }
                }


            } catch (Exception e) {
                logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：" + e.Message);
                return false;
            }
            return true;
        }
        #endregion

        #region 差旅费报销
        /// <summary>
        /// 差旅费报销
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        private bool ExportTravelReimburseDataByFormID(int FormID, int logId) {
            ExportDataDS.ExportDataDataTable l_dtExportData = new ExportDataDS.ExportDataDataTable();
            FormDS.FormTravelReimburseDetailDataTable l_dtTravelReimburseDetail = new FormDS.FormTravelReimburseDetailDataTable();
            FormMarketingBLL formmarkbll = new FormMarketingBLL();
            FormTEBLL formtebll = new FormTEBLL();
            StuffUserBLL stuffuserdll = new StuffUserBLL();
            MasterDataBLL masterdatabll = new MasterDataBLL();
            FormDS.FormDataTable l_dtform = formmarkbll.GetFormByID(FormID);
            FormDS.FormRow l_drform = l_dtform.NewFormRow();
            try {
                if (l_dtform.Rows.Count > 0) {
                    l_drform = l_dtform[0];
                    if (l_drform.IsFinanceRemarkNull()) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：财务摘要为空，导出失败！");
                        return false;
                    }
                    l_dtTravelReimburseDetail = formtebll.GetFormTravelReimburseDetailByFormTravelReimburseID(l_drform.FormID);
                    int i = 0;
                    FormDS.FormTravelReimburseDataTable l_dtTravelReimburse = formtebll.GetFormTravelReimburseByID(l_drform.FormID);
                    decimal TotalMoney = l_dtTravelReimburse[0].Amount;
                    foreach (FormDS.FormTravelReimburseDetailRow l_drtemp in l_dtTravelReimburseDetail) {
                        if (l_drtemp.PayMan == 1) {
                            TotalMoney = TotalMoney - l_drtemp.Cost;
                        }
                    }
                    foreach (FormDS.FormTravelReimburseDetailRow l_drtemp in l_dtTravelReimburseDetail) {
                        if (l_drtemp.PayMan != 1) {
                            ExportDataDS.ExportDataRow l_drExportData = l_dtExportData.NewExportDataRow();
                            l_drExportData.FormID = FormID;
                            l_drExportData.CompanyCode = masterdatabll.GetCompanyById(masterdatabll.GetCostCenterById(l_drform.CostCenterID).CompanyID).CompanyCode;
                            l_drExportData.VendorCode = stuffuserdll.GetStuffUserById(l_drform.UserID)[0].VendorCode;
                            if (l_drExportData.IsVendorCodeNull() || string.IsNullOrEmpty(l_drExportData.VendorCode)) {
                                logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：Vendor Code为空，无法导出！");
                                return false;
                            }
                            //如果Vendor Code的公司和成本中心的公司不一致，则报错
                            MasterData.VendorDataTable tbVendor = new BusinessObjects.MasterDataTableAdapters.VendorTableAdapter().GetDataByVendorCode(l_drExportData.VendorCode);
                            if (tbVendor.Rows.Count > 0) {
                                string vendorCompanyCode = masterdatabll.GetCompanyById(tbVendor[0].CompanyID).CompanyCode;
                                if (l_drExportData.CompanyCode != vendorCompanyCode) {
                                    logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：Company Code不符，请检查！");
                                    return false;
                                }
                            }
                            l_drExportData.Reference = "";
                            l_drExportData.Invoice = l_drform.FormNo;
                            l_drExportData.Invoicedate = l_drform.SubmitDate.ToString("yyyyMMdd");
                            l_drExportData.TotalMoney = TotalMoney;
                            //l_drExportData.Currency = masterdatabll.GetCurrencyByID(l_drtemp.CurrencyID).CurrencyShortName;
                            //l_drExportData.ExchangeRate = l_drtemp.ExchangeRate;
                            l_drExportData.ManageExpenseItem = masterdatabll.GetManageExpenseItemById(l_drtemp.ManageExpenseItemID).ManageExpenseItemName;
                            l_drExportData.CostCenter = masterdatabll.GetCostCenterById(l_drform.CostCenterID).CostCenterCode;
                            l_drExportData.AccountCode = masterdatabll.GetAccountingCodeByExpenseItemAndCostCenter(l_drtemp.ManageExpenseItemID, l_drform.CostCenterID);
                            //公司支付的金额为0
                            l_drExportData.DetailMoney = l_drtemp.Cost;
                            l_drExportData.PONumber = 0;
                            l_drExportData.POLineNumber = "0";
                            l_drExportData.FinanceRemark = l_drform.FinanceRemark;
                            l_drExportData.FormNo = l_drform.FormNo;
                            l_drExportData.TaxRateCode = masterdatabll.GetVatTypeById(1)[0].VatTypeName;
                            l_drExportData.TaxAmt = 0;
                            l_dtExportData.AddExportDataRow(l_drExportData);
                            i++;
                        }
                    }
                    if (l_dtExportData.Rows.Count == 0) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：无明细数据，无法导出！");
                        return false;
                    }
                    if (l_dtExportData.Rows.Count == i) {
                        ExportByFormView(l_dtExportData);
                    } else {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：导出数据与明细数据数量不一致！");
                        return false;
                    }
                }


            } catch (Exception e) {
                logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：" + e.Message);
                return false;
            }
            return true;
        }
        #endregion

        #region PV
        /// <summary>
        /// PV
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        private bool ExportPVDataByFormID(int FormID, int logId) {
            ExportDataDS.ExportDataDataTable l_dtExportData = new ExportDataDS.ExportDataDataTable();
            PurchaseDS.FormPVDetailDataTable l_dtFormPVDetail = new PurchaseDS.FormPVDetailDataTable();
            PurchaseDS.FormPVRow l_drFormPV = new PurchaseDS.FormPVDataTable().NewFormPVRow();
            FormMarketingBLL formmarkbll = new FormMarketingBLL();
            FormPurchaseBLL formPurchaseBLL = new FormPurchaseBLL();
            MasterDataBLL masterdatabll = new MasterDataBLL();
            FormDS.FormDataTable l_dtform = formmarkbll.GetFormByID(FormID);
            FormDS.FormRow l_drform = l_dtform.NewFormRow();
            try {
                if (l_dtform.Rows.Count > 0) {
                    l_drform = l_dtform[0];
                    if (l_drform.IsFinanceRemarkNull()) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：财务摘要为空，导出失败！");
                        return false;
                    }
                    l_drFormPV = formPurchaseBLL.GetFormPVByID(FormID);
                    //PurchaseDS.FormInvoiceDataTable l_dtFormInvoice = formPurchaseBLL.GetFormInvoiceByFormID(FormID);
                    ExportDataDS.ExportDataRow l_drExportData = l_dtExportData.NewExportDataRow();
                    l_drExportData.FormID = FormID;
                    l_drExportData.CompanyCode = masterdatabll.GetCompanyById(masterdatabll.GetVendorTypeById(masterdatabll.GetVendorByID(l_drFormPV.VendorID).VendorTypeID).CompanyID).CompanyCode;
                    //验证公司是否和成本中心对应的一致
                    string CCCompanyCode = masterdatabll.GetCompanyById(masterdatabll.GetCostCenterById(l_drform.CostCenterID).CompanyID).CompanyCode;
                    if (l_drExportData.CompanyCode != CCCompanyCode) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：Company Code不一致，无法导出！");
                        return false;
                    }
                    l_drExportData.VendorCode = masterdatabll.GetVendorByID(l_drFormPV.VendorID).VendorCode;
                    if (l_drExportData.IsVendorCodeNull() || string.IsNullOrEmpty(l_drExportData.VendorCode)) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：Vendor Code为空，无法导出！");
                        return false;
                    }
                    //发票号码改为单据编号，单据编号变为10位
                    l_drExportData.Invoice = l_drform.FormNo;
                    //l_drExportData.Invoicedate = l_drform.SubmitDate.ToString("yyyyMMdd");
                    l_drExportData.Invoicedate = getInvoiceDate(l_drform.FormID, l_drform.SubmitDate);

                    //if (l_dtFormInvoice.Rows.Count > 1)
                    //    l_drExportData.Reference = l_dtFormInvoice[1].InvoiceNo;
                    //if (l_dtFormInvoice.Rows.Count > 0) {
                    //    l_drExportData.Invoice = l_dtFormInvoice[0].InvoiceNo;
                    //    l_drExportData.Invoicedate = l_dtFormInvoice[0].InvoiceDate.ToString("yyyyMMdd");
                    //} else {
                    //    l_drExportData.Invoice = l_drform.FormNo;
                    //    l_drExportData.Invoicedate = l_drform.SubmitDate.ToString("yyyyMMdd");
                    //}
                    l_drExportData.TotalMoney = l_drFormPV.AMTBeforeTax;
                    //l_drExportData.Currency = masterdatabll.GetCurrencyByID(l_drFormPV.CurrencyID).CurrencyShortName;
                    //l_drExportData.ExchangeRate = l_drFormPV.ExchangeRate;
                    l_drExportData.ManageExpenseItem = masterdatabll.GetItemCategoryById(l_drFormPV.FinalItemCategoryID).AccountingName;
                    l_drExportData.CostCenter = masterdatabll.GetCostCenterById(l_drform.CostCenterID).CostCenterCode;
                    l_drExportData.AccountCode = masterdatabll.GetItemCategoryById(l_drFormPV.FinalItemCategoryID).AccountingCode;
                    l_drExportData.DetailMoney = l_drFormPV.AMTBeforeTax;
                    if (!l_drFormPV.IsFormPOIDNull()) {
                        PurchaseDS.FormPORow formPO = formPurchaseBLL.GetFormPOByID(l_drFormPV.FormPOID);
                        l_drExportData.PONumber = formPO.BPCSPONo;
                        PurchaseDS.FormPODetailDataTable formPODetail = formPurchaseBLL.GetPODetailByFormPOID(l_drFormPV.FormPOID);
                        l_drExportData.POLineNumber = formPODetail.Rows.Count.ToString();
                    } else {
                        l_drExportData.PONumber = 0;
                        l_drExportData.POLineNumber = "0";
                    }
                    l_drExportData.FinanceRemark = l_drform.FinanceRemark;
                    l_drExportData.FormNo = l_drform.FormNo;
                    l_drExportData.TaxAmt = l_drFormPV.AMTTax;
                    l_drExportData.TaxRateCode = masterdatabll.GetVatTypeById(l_drFormPV.VatRateID)[0].VatTypeName;
                    l_dtExportData.AddExportDataRow(l_drExportData);
                    if (l_dtExportData.Rows.Count == 0) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：无明细数据，无法导出！");
                        return false;
                    }
                    ExportByFormView(l_dtExportData);
                }
            } catch (Exception e) {
                logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：" + e.Message);
                return false;
            }
            return true;
        }
        #endregion

        #region 销售
        /// <summary>
        ///  销售
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        private bool ExportSaleDataByFormID(int FormID, int logId) {
            ExportDataDS.ExportDataDataTable l_dtExportData = new ExportDataDS.ExportDataDataTable();
            FormDS.FormSalePaymentRow l_drFormSalePayment = new FormDS.FormSalePaymentDataTable().NewFormSalePaymentRow();
            FormMarketingBLL formmarkbll = new FormMarketingBLL();
            FormTEBLL formtebll = new FormTEBLL();
            FormSaleBLL formsalebll = new FormSaleBLL();
            MasterDataBLL masterdatabll = new MasterDataBLL();
            FormPurchaseBLL formPurchaseBLL = new FormPurchaseBLL();
            int Count = 0;
            FormDS.FormDataTable l_dtform = formmarkbll.GetFormByID(FormID);
            FormDS.FormRow l_drform = l_dtform.NewFormRow();
            try {
                if (l_dtform.Rows.Count > 0) {
                    l_drform = l_dtform[0];
                    if (l_drform.IsFinanceRemarkNull()) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：财务摘要为空，导出失败！");
                        return false;
                    }
                    l_drFormSalePayment = formsalebll.GetFormSalePaymentByID(l_drform.FormID);
                    //if (l_drFormSalePayment.PaymentTypeID == (int)SystemEnums.PaymentType.FreeGoods)
                    //{
                    //    logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：货补的数据不导出！");
                    //    return false;
                    //}
                    //if (l_drFormSalePayment.PaymentTypeID == (int)SystemEnums.PaymentType.Transfer)
                    //{
                    //    logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：帐扣的数据不导入！");
                    //    return false;
                    //}
                    //if (l_drFormSalePayment.PaymentTypeID == (int)SystemEnums.PaymentType.PiaoKou)
                    //{
                    //    logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：票扣的数据不导入！");
                    //    return false;
                    //}
                    //if (l_drFormSalePayment.PaymentTypeID == 5)
                    //{
                    //    logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：调整因子的数据不导入！");
                    //    return false;
                    //}
                    FormDS.FormSalePaymentDetailDataTable l_dtFormSalePaymentDetail = formsalebll.GetFormSalePaymentDetailByPaymentID(FormID);
                    for (int i = 0; i < l_dtFormSalePaymentDetail.Rows.Count; i++) {
                        if (l_dtFormSalePaymentDetail[i].AmountRMB == Decimal.Zero) {
                            Count++;
                            continue;
                        }
                        //PurchaseDS.FormInvoiceDataTable l_dtFormInvoice = formPurchaseBLL.GetFormInvoiceByFormID(FormID);
                        ExportDataDS.ExportDataRow l_drExportData = l_dtExportData.NewExportDataRow();
                        l_drExportData.FormID = FormID;
                        l_drExportData.CompanyCode = l_drExportData.CompanyCode = masterdatabll.GetCompanyById(masterdatabll.GetVendorTypeById(masterdatabll.GetVendorByID(l_drFormSalePayment.VendorID).VendorTypeID).CompanyID).CompanyCode;
                        l_drExportData.VendorCode = masterdatabll.GetVendorByID(l_drFormSalePayment.VendorID).VendorCode;
                        if (l_drExportData.IsVendorCodeNull() || string.IsNullOrEmpty(l_drExportData.VendorCode)) {
                            logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：Vendor Code为空，无法导出！");
                            return false;
                        }
                        //发票号码改为单据编号，单据编号变为10位
                        l_drExportData.Invoice = l_drform.FormNo;
                        l_drExportData.Invoicedate = l_drform.SubmitDate.ToString("yyyyMMdd");

                        //if (l_dtFormInvoice.Rows.Count > 1)
                        //    l_drExportData.Reference = l_dtFormInvoice[1].InvoiceNo;
                        //if (l_dtFormInvoice.Rows.Count > 0) {
                        //    l_drExportData.Invoice = l_dtFormInvoice[0].InvoiceNo;
                        //    l_drExportData.Invoicedate = l_dtFormInvoice[0].InvoiceDate.ToString("yyyyMMdd");
                        //} else {
                        //    l_drExportData.Invoice = l_drform.FormNo;
                        //    l_drExportData.Invoicedate = l_drform.SubmitDate.ToString("yyyyMMdd");
                        //}

                        l_drExportData.TotalMoney = l_drFormSalePayment.AmountBeforeTax;
                        //l_drExportData.Currency = masterdatabll.GetCurrencyByID(formsalebll.GetFormSaleApplyByID(l_dtFormSalePaymentDetail[i].FormSaleApplyID)[0].CurrencyID).CurrencyShortName;
                        //l_drExportData.ExchangeRate = formsalebll.GetFormSaleApplyByID(l_dtFormSalePaymentDetail[i].FormSaleApplyID)[0].ExchangeRate;
                        l_drExportData.ManageExpenseItem = masterdatabll.GetExpenseItemById(l_dtFormSalePaymentDetail[i].ExpenseItemID).ExpenseItemName;
                        //profit center取AccrualCode，如果没有报错
                        if (masterdatabll.GetCostCenterById(l_drform.CostCenterID).IsAccrualCodeNull()) {
                            logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：Profit Center Accrual Code为空，无法导出！");
                            return false;
                        } else {
                            l_drExportData.CostCenter = masterdatabll.GetCostCenterById(l_drform.CostCenterID).AccrualCode;
                        }

                        if (l_dtFormSalePaymentDetail[i].ApplyPeriod.AddMonths(5).Year == l_drform.SubmitDate.AddMonths(5).Year)
                            l_drExportData.AccountCode = masterdatabll.GetExpenseItemById(l_dtFormSalePaymentDetail[i].ExpenseItemID).AccrualAccountingCode;
                        else
                            l_drExportData.AccountCode = masterdatabll.GetExpenseItemById(l_dtFormSalePaymentDetail[i].ExpenseItemID).LastAccountingCode;

                        l_drExportData.DetailMoney = l_dtFormSalePaymentDetail[i].AmountBeforeTax;
                        //PO No 变为取报销单的PO的No 
                        if (l_drFormSalePayment.IsFormPOIDNull()) {
                            //PurchaseDS.FormPODataTable formPO = formPurchaseBLL.GetFormPOByParentFormID(l_dtFormSalePaymentDetail[i].FormSaleApplyID);
                            //if (formPO.Rows.Count > 0) {
                            //    l_drExportData.PONumber = formPO[0].BPCSPONo;
                            //    PurchaseDS.FormPODetailDataTable formPODetail = formPurchaseBLL.GetPODetailByFormPOID(formPO[0].FormPOID);
                            //    l_drExportData.POLineNumber = formPODetail.Rows.Count.ToString();
                            //} else {
                            l_drExportData.PONumber = 0;
                            l_drExportData.POLineNumber = "0";
                            //}
                        } else {
                            //若已选PO，取所选PO信息
                            PurchaseDS.FormPORow formPO = formPurchaseBLL.GetFormPOByID(l_drFormSalePayment.FormPOID);
                            PurchaseDS.FormPODetailDataTable formPODetail = formPurchaseBLL.GetPODetailByFormPOID(l_drFormSalePayment.FormPOID);
                            l_drExportData.PONumber = formPO.BPCSPONo;
                            l_drExportData.POLineNumber = formPODetail.Count.ToString();
                        }

                        l_drExportData.FormNo = l_drform.FormNo;
                        l_drExportData.FinanceRemark = l_drform.FinanceRemark;
                        l_drExportData.TaxRateCode = masterdatabll.GetVatTypeById(l_drFormSalePayment.VatTypeID)[0].VatTypeName;
                        l_drExportData.TaxAmt = l_dtFormSalePaymentDetail[i].TaxAmount;
                        l_dtExportData.AddExportDataRow(l_drExportData);
                        Count++;
                    }
                    if (l_dtExportData.Rows.Count == 0) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：无明细数据，无法导出！");
                        return false;
                    }
                    if (l_dtFormSalePaymentDetail.Rows.Count == Count)
                        ExportByFormView(l_dtExportData);
                    else {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：明细数据数量不一致，无法导出！");
                        return false;
                    }
                }
            } catch (Exception e) {

                logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：" + e.Message);
                return false;
            }
            return true;
        }
        #endregion

        #region  市场
        /// <summary>
        ///  市场
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        private bool ExportMarketingDataByFormID(int FormID, int logId) {
            ExportDataDS.ExportDataDataTable l_dtExportData = new ExportDataDS.ExportDataDataTable();
            FormDS.FormMarketingPaymentRow l_drMarketingPayment = new FormDS.FormMarketingPaymentDataTable().NewFormMarketingPaymentRow();
            FormMarketingBLL formmarkbll = new FormMarketingBLL();
            FormTEBLL formtebll = new FormTEBLL();
            FormSaleBLL formsalebll = new FormSaleBLL();
            MasterDataBLL masterdatabll = new MasterDataBLL();
            FormPurchaseBLL formPurchaseBLL = new FormPurchaseBLL();
            FormDS.FormDataTable l_dtform = formmarkbll.GetFormByID(FormID);
            FormDS.FormRow l_drform = l_dtform.NewFormRow();
            int Count = 0;
            try {
                if (l_dtform.Rows.Count > 0) {
                    l_drform = l_dtform[0];
                    if (l_drform.IsFinanceRemarkNull()) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：财务摘要为空，导出失败！");
                        return false;
                    }
                    l_drMarketingPayment = formmarkbll.GetFormMarketingPaymentByID(l_drform.FormID);
                    FormDS.FormMarketingPaymentDetailDataTable l_dtFormMarketingPaymentDetail = formmarkbll.GetFormMarketingPaymentDetailByPaymentID(FormID);
                    for (int i = 0; i < l_dtFormMarketingPaymentDetail.Rows.Count; i++) {
                        if (l_dtFormMarketingPaymentDetail[i].AmountBeforeTax == Decimal.Zero) {
                            Count++;
                            continue;
                        }
                        //PurchaseDS.FormInvoiceDataTable l_dtFormInvoice = formPurchaseBLL.GetFormInvoiceByFormID(FormID);
                        ExportDataDS.ExportDataRow l_drExportData = l_dtExportData.NewExportDataRow();
                        l_drExportData.FormID = FormID;
                        l_drExportData.CompanyCode = masterdatabll.GetCompanyById(masterdatabll.GetVendorTypeById(masterdatabll.GetVendorByID(l_dtFormMarketingPaymentDetail[i].VendorID).VendorTypeID).CompanyID).CompanyCode;
                        l_drExportData.VendorCode = masterdatabll.GetVendorByID(l_dtFormMarketingPaymentDetail[i].VendorID).VendorCode;
                        if (l_drExportData.IsVendorCodeNull() || string.IsNullOrEmpty(l_drExportData.VendorCode)) {
                            logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + "，错误信息：Vendor Code为空，无法导出！");
                            return false;
                        }
                        //发票号码改为单据编号，单据编号变为10位
                        l_drExportData.Invoice = l_drform.FormNo;
                        l_drExportData.Invoicedate = l_drform.SubmitDate.ToString("yyyyMMdd");

                        //if (l_dtFormInvoice.Rows.Count > 1)
                        //    l_drExportData.Reference = l_dtFormInvoice[1].InvoiceNo;
                        //if (l_dtFormInvoice.Rows.Count > 0) {
                        //    l_drExportData.Invoice = l_dtFormInvoice[0].InvoiceNo;
                        //    l_drExportData.Invoicedate = l_dtFormInvoice[0].InvoiceDate.ToString("yyyyMMdd");
                        //} else {
                        //    l_drExportData.Invoice = l_drform.FormNo;
                        //    l_drExportData.Invoicedate = l_drform.SubmitDate.ToString("yyyyMMdd");
                        //}
                        l_drExportData.TotalMoney = l_drMarketingPayment.AmountBeforeTaxRMB;
                        //l_drExportData.Currency = masterdatabll.GetCurrencyByID(formmarkbll.GetFormMarketingApplyByID(l_drMarketingPayment.FormMarketingApplyID)[0].CurrencyID).CurrencyShortName;
                        //l_drExportData.ExchangeRate = formmarkbll.GetFormMarketingApplyByID(l_drMarketingPayment.FormMarketingApplyID)[0].ExchangeRate;
                        l_drExportData.ManageExpenseItem = masterdatabll.GetExpenseItemById(l_dtFormMarketingPaymentDetail[i].ExpenseItemID).ExpenseItemName;
                        //profit center取AccrualCode，如果没有报错
                        if (masterdatabll.GetCostCenterById(l_drform.CostCenterID).IsAccrualCodeNull()) {
                            logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：Profit Center Accrual Code为空，无法导出！");
                            return false;
                        } else {
                            l_drExportData.CostCenter = masterdatabll.GetCostCenterById(l_drform.CostCenterID).AccrualCode;
                        }

                        if (formmarkbll.GetFormMarketingApplyByID(l_drMarketingPayment.FormMarketingApplyID)[0].FPeriod.AddMonths(5).Year == l_drform.SubmitDate.AddMonths(5).Year)
                            l_drExportData.AccountCode = masterdatabll.GetExpenseItemById(l_dtFormMarketingPaymentDetail[i].ExpenseItemID).AccrualAccountingCode;
                        else
                            l_drExportData.AccountCode = masterdatabll.GetExpenseItemById(l_dtFormMarketingPaymentDetail[i].ExpenseItemID).LastAccountingCode;

                        l_drExportData.DetailMoney = l_dtFormMarketingPaymentDetail[i].AmountBeforeTax;
                        //取Detail中的PO NO，否则就是0
                        //PurchaseDS.FormPODataTable formPO = formPurchaseBLL.GetFormPOByParentFormID(l_drMarketingPayment.FormMarketingApplyID);
                        //if (formPO.Rows.Count > 0)
                        //{
                        //    l_drExportData.PONumber = formPO[0].BPCSPONo;
                        //    PurchaseDS.FormPODetailDataTable formPODetail = formPurchaseBLL.GetPODetailByFormPOID(formPO[0].FormPOID);
                        //    l_drExportData.POLineNumber = formPODetail.Rows.Count.ToString();
                        //}
                        //else
                        //{
                        //    l_drExportData.PONumber = 0;
                        //    l_drExportData.POLineNumber = "0";
                        //}
                        if (!l_dtFormMarketingPaymentDetail[i].IsPOBPCSNoNull()) {
                            l_drExportData.PONumber = l_dtFormMarketingPaymentDetail[i].POBPCSNo;
                            PurchaseDS.FormPODetailDataTable formPODetail = formPurchaseBLL.GetPODetailByFormPOID(l_dtFormMarketingPaymentDetail[i].POFormID);
                            l_drExportData.POLineNumber = formPODetail.Rows.Count.ToString();
                        } else {
                            l_drExportData.PONumber = 0;
                            l_drExportData.POLineNumber = "0";
                        }

                        l_drExportData.FinanceRemark = l_drform.FinanceRemark;
                        l_drExportData.FormNo = l_drform.FormNo;
                        l_drExportData.TaxAmt = l_dtFormMarketingPaymentDetail[i].TaxAmount;
                        l_drExportData.TaxRateCode = masterdatabll.GetVatTypeById(l_drMarketingPayment.VATTypeID)[0].VatTypeName;
                        l_dtExportData.AddExportDataRow(l_drExportData);
                        Count++;
                    }
                    if (l_dtExportData.Rows.Count == 0) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：无明细数据，无法导出！");
                        return false;
                    }
                    if (l_dtFormMarketingPaymentDetail.Rows.Count == Count)
                        ExportByFormView(l_dtExportData);
                    else {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：无导出数据！");
                        return false;
                    }
                }
            } catch (Exception e) {
                logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：" + e.Message);
                return false;
            }
            return true;
        }
        #endregion

        #region  R&D
        /// <summary>
        /// MAA R&D
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        private bool ExportRDDataByFormID(int FormID, int logId) {
            ExportDataDS.ExportDataDataTable l_dtExportData = new ExportDataDS.ExportDataDataTable();
            FormDS.FormRDPaymentRow l_drFormRDPayment = new FormDS.FormRDPaymentDataTable().NewFormRDPaymentRow();
            MasterDataBLL masterdatabll = new MasterDataBLL();
            FormRDBLL formrdbll = new FormRDBLL();
            FormPurchaseBLL formPurchaseBLL = new FormPurchaseBLL();
            int Count = 0;
            FormDS.FormDataTable l_dtform = formrdbll.GetFormByID(FormID);
            FormDS.FormRow l_drform = l_dtform.NewFormRow();
            try {

                if (l_dtform.Rows.Count > 0) {
                    l_drform = l_dtform[0];
                    if (l_drform.IsFinanceRemarkNull()) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + "，错误信息：财务摘要为空，导出失败！");
                        return false;
                    }
                    l_drFormRDPayment = formrdbll.GetFormRDPaymentByID(l_drform.FormID);
                    FormDS.FormRDPaymentDetailDataTable l_dtFormRDPaymentDetail = formrdbll.GetFormRDPaymentDetailByPaymentID(FormID);
                    for (int i = 0; i < l_dtFormRDPaymentDetail.Rows.Count; i++) {
                        if (l_dtFormRDPaymentDetail[i].AmountBeforeTax == Decimal.Zero) {
                            Count++;
                            continue;
                        }
                        //PurchaseDS.FormInvoiceDataTable l_dtFormInvoice = formPurchaseBLL.GetFormInvoiceByFormID(FormID);
                        ExportDataDS.ExportDataRow l_drExportData = l_dtExportData.NewExportDataRow();
                        l_drExportData.FormID = FormID;
                        l_drExportData.CompanyCode = masterdatabll.GetCompanyById(masterdatabll.GetVendorTypeById(masterdatabll.GetVendorByID(l_dtFormRDPaymentDetail[i].VendorID).VendorTypeID).CompanyID).CompanyCode;
                        l_drExportData.VendorCode = masterdatabll.GetVendorByID(l_dtFormRDPaymentDetail[i].VendorID).VendorCode;

                        if (l_drExportData.IsVendorCodeNull() || string.IsNullOrEmpty(l_drExportData.VendorCode)) {
                            logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + "，错误信息：Vendor Code为空，无法导出！");
                            return false;
                        }
                        //发票号码改为单据编号，单据编号变为10位
                        l_drExportData.Invoice = l_drform.FormNo;
                        l_drExportData.Invoicedate = l_drform.SubmitDate.ToString("yyyyMMdd");

                        //if (l_dtFormInvoice.Rows.Count > 1)
                        //    l_drExportData.Reference = l_dtFormInvoice[1].InvoiceNo;
                        //if (l_dtFormInvoice.Rows.Count > 0) {
                        //    l_drExportData.Invoice = l_dtFormInvoice[0].InvoiceNo;
                        //    l_drExportData.Invoicedate = l_dtFormInvoice[0].InvoiceDate.ToString("yyyyMMdd");
                        //} else {
                        //    l_drExportData.Invoice = l_drform.FormNo;
                        //    l_drExportData.Invoicedate = l_drform.SubmitDate.ToString("yyyyMMdd");
                        //}
                        l_drExportData.TotalMoney = l_drFormRDPayment.AmountBeforeTaxRMB;
                        //l_drExportData.Currency = masterdatabll.GetCurrencyByID(formmarkbll.GetFormMarketingApplyByID(l_drMarketingPayment.FormMarketingApplyID)[0].CurrencyID).CurrencyShortName;
                        //l_drExportData.ExchangeRate = formmarkbll.GetFormMarketingApplyByID(l_drMarketingPayment.FormMarketingApplyID)[0].ExchangeRate;
                        l_drExportData.ManageExpenseItem = masterdatabll.GetExpenseItemById(l_dtFormRDPaymentDetail[i].ExpenseItemID).ExpenseItemName;
                        //profit center取AccrualCode，如果没有报错
                        if (masterdatabll.GetCostCenterById(l_drform.CostCenterID).IsAccrualCodeNull()) {
                            logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + ",错误信息：Profit Center Accrual Code为空，无法导出！");
                            return false;
                        } else {
                            l_drExportData.CostCenter = masterdatabll.GetCostCenterById(l_drform.CostCenterID).AccrualCode;
                        }

                        if (formrdbll.GetFormRDApplyByID(l_drFormRDPayment.FormRDApplyID)[0].FPeriod.AddMonths(5).Year == l_drform.SubmitDate.AddMonths(5).Year)
                            l_drExportData.AccountCode = masterdatabll.GetExpenseItemById(l_dtFormRDPaymentDetail[i].ExpenseItemID).AccrualAccountingCode;
                        else
                            l_drExportData.AccountCode = masterdatabll.GetExpenseItemById(l_dtFormRDPaymentDetail[i].ExpenseItemID).LastAccountingCode;

                        l_drExportData.DetailMoney = l_dtFormRDPaymentDetail[i].AmountBeforeTax;

                        if (l_drFormRDPayment.IsFormPOIDNull()) {
                            //PurchaseDS.FormPODataTable formPO = formPurchaseBLL.GetFormPOByParentFormID(l_drFormRDPayment.FormRDApplyID);
                            //if (formPO.Rows.Count > 0) {
                            //    l_drExportData.PONumber = formPO[0].BPCSPONo;
                            //    PurchaseDS.FormPODetailDataTable formPODetail = formPurchaseBLL.GetPODetailByFormPOID(formPO[0].FormPOID);
                            //    l_drExportData.POLineNumber = formPODetail.Rows.Count.ToString();
                            //} else {
                            l_drExportData.PONumber = 0;
                            l_drExportData.POLineNumber = "0";
                            //}
                        } else {
                            //若已选择PO，取所选PO信息
                            PurchaseDS.FormPORow formPO = formPurchaseBLL.GetFormPOByID(l_drFormRDPayment.FormPOID);
                            PurchaseDS.FormPODetailDataTable formPODetail = formPurchaseBLL.GetPODetailByFormPOID(l_drFormRDPayment.FormPOID);
                            l_drExportData.PONumber = formPO.BPCSPONo;
                            l_drExportData.POLineNumber = formPODetail.Count.ToString();
                        }
                        l_drExportData.FinanceRemark = l_drform.FinanceRemark;
                        l_drExportData.FormNo = l_drform.FormNo;
                        l_drExportData.TaxAmt = l_dtFormRDPaymentDetail[i].TaxAmount;
                        l_drExportData.TaxRateCode = masterdatabll.GetVatTypeById(l_drFormRDPayment.VATTypeID)[0].VatTypeName;
                        l_dtExportData.AddExportDataRow(l_drExportData);
                        Count++;
                    }
                    if (l_dtExportData.Rows.Count == 0) {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + "，错误信息：无明细数据，无法导出！");
                        return false;
                    }
                    if (l_dtFormRDPaymentDetail.Rows.Count == Count)
                        ExportByFormView(l_dtExportData);
                    else {
                        logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + "，错误信息：明细数据与导出数据数量不一致！");
                        return false;
                    }
                }
            } catch (Exception e) {
                logbll.InsertExportLogDetail(logId, "单据编号：" + l_drform.FormNo + ",提交人：" + new StuffUserBLL().GetStuffUserById(l_drform.UserID)[0].StuffName + "，错误信息：" + e.Message);
                return false;
            }
            return true;


        }
        #endregion

        #region 更新form表
        private void UpdateFormbyID(int FormID, bool IsCreateVoucher) {
            FormMarketingBLL formmarkbll = new FormMarketingBLL();
            FormDS.FormDataTable l_dtform = formmarkbll.GetFormByID(FormID);
            if (l_dtform.Rows.Count > 0) {
                FormDS.FormRow l_drform = l_dtform[0];
                l_drform.CreateVoucherDate = DateTime.Now;
                l_drform.IsExportLock = true;
                l_drform.IsCreateVoucher = IsCreateVoucher;
                formmarkbll.TAForm.Update(l_drform);
            }
        }
        #endregion

        #region Vendor导出
        /// <summary>
        /// Vendor导出
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        private bool ExportVendorAVM(MasterData.VendorRow vendor, int logId) {
            string BakPath = ConfigurationManager.AppSettings["ExportService.BakPath"];
            FormVendorBLL formvendorbll = new FormVendorBLL();
            MasterDataBLL masterdatabll = new MasterDataBLL();
            try {
                string FileName = ConfigurationManager.AppSettings["ExportService.VendorAVMFileName"];
                string spit = "\t";
                MasterData.PaymentTermRow paymentTerm = null;
                MasterData.VendorTypeRow vendorType = null;
                MasterData.CompanyRow company = null;
                MasterData.CurrencyRow currency = null;
                MasterData.MethodPaymentRow paymentType = null;
                MasterData.VatTypeRow VatType = null;
                MasterData.BankCodeRow bankCode = null;
                MasterData.TransTypeRow transType = null;
                MasterData.ACTypeRow acType = null;

                paymentTerm = masterdatabll.GetPaymentTermById(vendor.PaymentTermID)[0];
                vendorType = masterdatabll.GetVendorTypeById(vendor.VendorTypeID);
                company = masterdatabll.GetCompanyById(vendor.CompanyID);
                currency = masterdatabll.GetCurrencyByID(vendorType.CurrencyID);
                paymentType = masterdatabll.GetMethodPaymentById(vendor.MethodPaymentID)[0];
                VatType = masterdatabll.GetVatTypeById(vendor.VATTypeID)[0];
                bankCode = masterdatabll.GetBankCodeById(vendor.BankCodeID)[0];
                transType = masterdatabll.GetTransTypeById(vendor.TransTypeID)[0];
                acType = masterdatabll.GetACTypeById(vendor.ACTypeID)[0];
                string ActionName = "";
                string content = "";
                switch (vendor.ActionType) {
                    case 0:
                        ActionName = "D";
                        #region 删除
                        content =
                           "" + spit +
                           ActionName + spit +
                           "" + spit +
                           "VZ" + spit +
                           bSubstring(vendor.VendorCode, 5) + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           "" + spit +
                          "" + spit +
                           "" + spit +
                           0 + spit +
                           "" + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + "\r\n";
                        #endregion
                        break;
                    case 1:
                        ActionName = "A";
                        #region 新增
                        content =
                           "" + spit +
                           ActionName + spit +
                           (vendor.IsActive ? "A" : "D") + spit +
                           (vendor.IsActive ? "VM" : "VZ") + spit +
                           bSubstring(vendor.VendorCode, 5) + spit +
                           bSubstring(vendor.VendorName, 28) + spit +
                           bSubstring(vendor.VendorAddress, 28) + spit +
                           SpitVendorAddress(vendor.VendorAddress) + spit +
                           bSubstring(vendor.City, 17) + spit +
                           "" + spit +
                           bSubstring(vendor.Postal, 9) + spit +
                           bSubstring(paymentTerm.PaymentTermName, 2) + spit +
                           bSubstring(vendorType.VendorTypeName, 4) + spit +
                           bSubstring(vendor.VendorCode, 5) + spit +
                           0 + spit +
                           0 + spit +
                           "" + spit +
                           "N" + spit +
                           "" + spit +
                           bSubstring(vendor.PhoneNumber, 15) + spit +
                           bSubstring(company.CompanyCode, 2) + spit +
                           bSubstring(currency.CurrencyShortName, 3) + spit +
                           bSubstring(paymentType.MethodPaymentName, 1) + spit +
                           (vendor.OneTimeVendor ? "Y" : "N") + spit +
                           0 + spit +
                           (vendor.HoldVendor ? "Y" : "N") + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           "N" + spit +
                           bSubstring(vendor.AlphaSearchKey, 10) + spit +
                           0 + spit +
                           bSubstring(vendor.ContactName, 30) + spit +
                           "" + spit +
                           0 + spit +
                           bSubstring(vendor.PurchasingAddress, 28) + spit +
                           SpitVendorAddress(vendor.PurchasingAddress) + spit +
                           bSubstring(vendor.PurchasingCity, 28) + spit +
                           "" + spit +
                           bSubstring(vendor.PurchaseingPostalCode, 9) + spit +
                           bSubstring(vendor.PurchasingContact, 30) + spit +
                           "" + spit +
                           0 + spit +
                           0 + spit +
                           "N" + spit +
                           bSubstring(vendor.PurchasePhoneNumber, 15) + spit +
                           "" + spit +
                           "" + spit +
                           bSubstring(VatType.VatTypeName, 5) + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           0 + "\r\n";
                        #endregion
                        break;
                    case 2:
                        ActionName = "U";
                        #region 修改
                        QueryDS.FormVendorViewDataTable l_dtformvendor = formvendorbll.GetApproveFormVendorByVendorID(vendor.VendorID);
                        if (l_dtformvendor.Rows.Count > 1) {

                            QueryDS.FormVendorViewRow l_drformvendor = l_dtformvendor[1];
                            content =
                               "" + spit +
                               ActionName + spit +
                               "" + spit +
                               (vendor.IsActive ? "VM" : "VZ") + spit +
                               bSubstring(vendor.VendorCode, 5) + spit +
                               (bSubstring(vendor.VendorName, 28) == bSubstring(l_drformvendor.VendorName, 28) ? "" : bSubstring(vendor.VendorName, 28)) + spit +
                               (bSubstring(vendor.VendorAddress, 28) == bSubstring(l_drformvendor.VendorAddress, 28) ? "" : vendor.VendorAddress) + spit +
                               (SpitVendorAddress(vendor.VendorAddress) == SpitVendorAddress(l_drformvendor.VendorAddress) ? "" : SpitVendorAddress(vendor.VendorAddress)) + spit +
                               (bSubstring(vendor.City, 17) == bSubstring(l_drformvendor.City, 17) ? "" : bSubstring(vendor.City, 17)) + spit +
                               "" + spit +
                               (bSubstring(vendor.Postal, 9) == bSubstring(l_drformvendor.Postal, 9) ? "" : bSubstring(vendor.Postal, 9)) + spit +
                              bSubstring(l_drformvendor.MethodPaymentID == vendor.MethodPaymentID ? "" : paymentTerm.PaymentTermName, 2) + spit +
                               bSubstring(vendor.VendorTypeID == l_drformvendor.VendorTypeID ? "" : vendorType.VendorTypeName, 4) + spit +
                               0 + spit +
                               0 + spit +
                               0 + spit +
                               "" + spit +
                               "" + spit +
                               "" + spit +
                               (bSubstring(vendor.PhoneNumber, 15) == bSubstring(l_drformvendor.PhoneNumber, 15) ? "" : bSubstring(vendor.PhoneNumber, 15)) + spit +
                               (bSubstring(company.CompanyCode, 2) == bSubstring(l_drformvendor.CompanyCode, 2) ? "0" : bSubstring(company.CompanyCode, 2)) + spit +
                               (bSubstring(currency.CurrencyShortName, 3) == bSubstring(l_drformvendor.CurrencyShortName, 3) ? "" : bSubstring(currency.CurrencyShortName, 3)) + spit +
                               bSubstring(l_drformvendor.MethodPaymentID == vendor.MethodPaymentID ? "" : paymentType.MethodPaymentName, 1) + spit +
                               (vendor.OneTimeVendor == l_drformvendor.OneTimeVendor ? "" : (vendor.OneTimeVendor ? "Y" : "N")) + spit +
                               0 + spit +
                               (vendor.HoldVendor == l_drformvendor.HoldVendor ? "" : (vendor.HoldVendor ? "Y" : "N")) + spit +
                               0 + spit +
                               0 + spit +
                               0 + spit +
                               0 + spit +
                               0 + spit +
                               0 + spit +
                               "" + spit +
                               (bSubstring(vendor.AlphaSearchKey, 10) == bSubstring(l_drformvendor.AlphaSearchKey, 10) ? "" : bSubstring(vendor.AlphaSearchKey, 10)) + spit +
                               0 + spit +
                               (bSubstring(vendor.ContactName, 30) == bSubstring(l_drformvendor.ContactName, 30) ? "" : bSubstring(vendor.ContactName, 30)) + spit +
                               "" + spit +
                               0 + spit +
                               (bSubstring(vendor.PurchasingAddress, 28) == bSubstring(l_drformvendor.PurchasingAddress, 28) ? "" : bSubstring(vendor.PurchasingAddress, 28)) + spit +
                               (SpitVendorAddress(vendor.PurchasingAddress) == SpitVendorAddress(l_drformvendor.PurchasingAddress) ? "" : SpitVendorAddress(vendor.PurchasingAddress)) + spit +
                              (bSubstring(vendor.PurchasingCity, 28) == bSubstring(l_drformvendor.PurchasingCity, 28) ? "" : bSubstring(vendor.PurchasingCity, 28)) + spit +
                               "" + spit +
                               (bSubstring(vendor.PurchaseingPostalCode, 9) == bSubstring(l_drformvendor.PurchaseingPostalCode, 9) ? "" : bSubstring(vendor.PurchaseingPostalCode, 9)) + spit +
                               (bSubstring(vendor.PurchasingContact, 30) == bSubstring(l_drformvendor.PurchasingContact, 30) ? "" : bSubstring(vendor.PurchasingContact, 30)) + spit +
                               "" + spit +
                               0 + spit +
                               0 + spit +
                               "" + spit +
                               (bSubstring(vendor.PurchasePhoneNumber, 15) == bSubstring(l_drformvendor.PurchasePhoneNumber, 15) ? "" : bSubstring(vendor.PurchasePhoneNumber, 15)) + spit +
                               "" + spit +
                               "" + spit +
                               bSubstring(l_drformvendor.VATTypeID == vendor.VATTypeID ? "" : VatType.VatTypeName, 5) + spit +
                               "" + spit +
                               "" + spit +
                               0 + spit +
                               0 + spit +
                               "" + spit +
                               "" + spit +
                               "" + spit +
                               "" + spit +
                               "" + spit +
                               "" + spit +
                               "" + "\r\n";
                        } else {
                            logbll.InsertExportLogDetail(logId, "未找到Vendor历史数据！");
                        }
                        #endregion
                        break;
                    case 3:
                        ActionName = "R";
                        #region 重新激活
                        content =
                           "" + spit +
                           ActionName + spit +
                           "" + spit +
                           "VM" + spit +
                           bSubstring(vendor.VendorCode, 5) + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           "" + spit +
                          "" + spit +
                           "" + spit +
                           0 + spit +
                           "" + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           0 + spit +
                           0 + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + spit +
                           "" + "\r\n";
                        #endregion
                        break;
                }
                content = content.Replace("\r", "").Replace("\n", "") + "\r\n";
                File.AppendAllText(path + FileName, content, Encoding.Default);
                File.AppendAllText(BakPath + DateTime.Now.ToString("yyyyMMddhhmmss") + FileName, content, Encoding.Default);
                UpdateVendorbyID(vendor.VendorID);
            } catch (Exception e) {
                logbll.InsertExportLogDetail(logId, "VendorCode:" + vendor.VendorCode + "，VendorName:" + vendor.VendorName + "" + ",错误信息：" + e.Message);
                return false;
            }
            return true;
        }

        private void UpdateVendorbyID(int VendorId) {
            MasterDataBLL masterdatabll = new MasterDataBLL();
            MasterData.VendorRow l_drVendor = masterdatabll.GetVendorByID(VendorId);
            l_drVendor.IsExport = true;
            masterdatabll.TAVendor.Update(l_drVendor);
        }

        private bool ExportVendorAVMX(MasterData.VendorRow vendor, int logId) {
            string BakPath = ConfigurationManager.AppSettings["ExportService.BakPath"];
            try {
                string FileName = ConfigurationManager.AppSettings["ExportService.VendorAVMXFileName"];
                string spit = "\t";
                MasterDataBLL masterdatabll = new MasterDataBLL();
                MasterData.CurrencyRow currency = null;
                MasterData.VendorTypeRow vendorType = null;
                MasterData.TransTypeRow transType = null;
                MasterData.BankCodeRow bankCode = null;
                MasterData.ACTypeRow acType = null;
                vendorType = masterdatabll.GetVendorTypeById(vendor.VendorTypeID);
                currency = masterdatabll.GetCurrencyByID(vendorType.CurrencyID);

                if (!vendor.IsBankCodeIDNull()) {
                    MasterData.BankCodeDataTable tbBankCode = masterdatabll.GetBankCodeById(vendor.BankCodeID);
                    bankCode = tbBankCode[0];
                    if (bankCode.BankCode.Equals("999")) {
                        throw new Exception("BankCode为999不导出！");
                    }
                }

                if (!vendor.IsTransTypeIDNull()) {
                    MasterData.TransTypeDataTable tbTransType = masterdatabll.GetTransTypeById(vendor.TransTypeID);
                    transType = tbTransType[0];
                }

                if (!vendor.IsACTypeIDNull()) {
                    MasterData.ACTypeDataTable tbACType = masterdatabll.GetACTypeById(vendor.ACTypeID);
                    acType = masterdatabll.GetACTypeById(vendor.ACTypeID)[0];
                }
                if (vendor.IsAccountNoNull()) {
                    throw new Exception("银行账号为空的不导出！");
                }

                string content =
                   (vendor.IsActive ? "VM" : "VZ") + spit +
                       bSubstring(vendor.VendorCode, 5) + spit +
                       bSubstring(currency.CurrencyShortName, 3) + spit +
                       bSubstring(bankCode.BankCode, 3) + spit +
                       bSubstring(transType == null ? "" : transType.TransTypeName, 2) + spit +
                       bSubstring(vendor.IsBankNameNull() ? "" : vendor.BankName, 68) + spit +
                       bSubstring(vendor.IsAccountNoNull() ? "" : vendor.AccountNo, 32) + spit +
                       bSubstring(vendor.VendorName, 58) + spit +
                       bSubstring(acType == null ? "" : acType.ACTypeName, 1) + spit +
                       0 + spit +
                       bSubstring(vendor.IsBankNoNull() ? "" : vendor.BankNo, 11);
                content = content.Replace("\r", "").Replace("\n", "") + "\r\n";
                File.AppendAllText(path + FileName, content, Encoding.Default);
                File.AppendAllText(BakPath + DateTime.Now.ToString("yyyyMMddhhmmss") + FileName, content, Encoding.Default);
            } catch (Exception e) {
                logbll.InsertExportLogDetail(logId, "VendorCode:" + vendor.VendorCode + "，VendorName:" + vendor.VendorName + "" + ",错误信息：" + e.Message);
                return false;
            }
            return true;
        }

        public string SpitVendorAddress(string Address) {
            byte[] addressBytes = Encoding.GetEncoding("utf-8").GetBytes(Address);
            if (addressBytes.Length <= 28) {
                return "";
            } else {
                return bSubstring(Address, 56).Replace(bSubstring(Address, 28), "");
            }
        }

        #endregion

        #region PO导出

        private bool ExportPO(int formid, int logId) {
            string BakPath = ConfigurationManager.AppSettings["ExportService.BakPath"];
            AuthorizationBLL authorizationbll = new AuthorizationBLL();
            MasterDataBLL masterdatabll = new MasterDataBLL();
            FormQueryBLL formQueryBLL = new FormQueryBLL();
            FormPurchaseBLL formPurchaseBLL = new FormPurchaseBLL();
            OUTreeBLL outreebll = new OUTreeBLL();
            PurchaseDS.FormDataTable l_dtform = new PurchaseDS.FormDataTable();
            PurchaseDS.FormRow form = l_dtform.NewFormRow();
            try {
                PurchaseDS.FormPORow formPO = formPurchaseBLL.GetFormPOByID(formid);

                PurchaseDS.FormPODetailDataTable formPODetail = formPurchaseBLL.GetPODetailByFormPOID(formPO.FormPOID);
                form = formPurchaseBLL.GetFormByID(formPO.FormPOID)[0];
                string POFileName = ConfigurationManager.AppSettings["ExportService.POFileName"];
                string POLineFileName = ConfigurationManager.AppSettings["ExportService.POLineFileName"];
                string spit = "\t";
                string CompanyCode = masterdatabll.GetCompanyById(masterdatabll.GetVendorTypeById(masterdatabll.GetVendorByID(formPO.VendorID).VendorTypeID).CompanyID).CompanyCode;
                string VendorCode = masterdatabll.GetVendorByID(formPO.VendorID).VendorCode;
                string POcontent = (
                   "PH" + spit +
                    "" + spit +
                   formPO.BPCSPONo + spit +
                    "0" + spit +
                    "" + spit +
                    "0" + spit +
                     CompanyCode + spit +
                    "" + spit +
                     CompanyCode + spit +
                    VendorCode + spit +
                    "0" + spit +
                    "4" + spit +
                    "" + spit +
                    masterdatabll.GetCompanyById(formPO.CompanyID).CompanyName + spit +
                    authorizationbll.GetStuffUserById(form.UserID).UserName.ToUpper() + spit +
                    bSubstring(formPO.DeliveryAddress, 30) + spit +
                    SpitAddress(formPO.DeliveryAddress, 60) + spit +
                    SpitAddress(formPO.DeliveryAddress, 90) + spit +
                    "" + spit +
                    "" + spit +
                    "" + spit +
                    form.SubmitDate.ToString("yyyyMMdd") + spit +
                    "0" + spit +
                    "0" + spit +
                    "0" + spit +
                    formPODetail[0].DeliveryDate.ToString("yyyyMMdd") + spit +
                    "0" + spit +
                    "0" + spit +
                    bSubstring(formPO.Remark, 30) + spit +
                    masterdatabll.GetPaymentTermById(masterdatabll.GetVendorByID(formPO.VendorID).PaymentTermID)[0].PaymentTermName + spit +
                    "1" + spit +
                    "" + spit +
                    outreebll.GetOrganizationUnitById(form.OrganizationUnitID).OrganizationUnitCode + spit +
                    masterdatabll.GetShippingTermById(formPO.ShippingTermID).ShippingTermCode + spit +
                    "" + spit +
                    masterdatabll.GetCurrencyByID(formPO.CurrencyID).CurrencyShortName + spit +
                    formPO.ExchangeRate + spit +
                    formPO.ExchangeRate + spit +
                    formPODetail.Rows.Count + spit +
                    formPO.ExchangeRate + spit +
                    authorizationbll.GetStuffUserById(form.UserID).UserName.ToUpper() + spit +
                    "" + spit +
                     authorizationbll.GetStuffUserById(form.UserID).UserName.ToUpper() + spit +
                   form.ApprovedDate.ToString("yyyyMMdd") + spit +
                    "" + spit +
                    "0" + spit +
                    "" + spit +
                    "0" + spit +
                    "" + spit +
                    "" + spit +
                    "" + spit +
                    "" + spit +
                    "").Replace("\r", "").Replace("\n", "");
                int order = 0;
                string[] PODetailcontent = new string[formPODetail.Rows.Count];
                foreach (PurchaseDS.FormPODetailRow l_drformPODetail in formPODetail) {
                    order++;
                    PODetailcontent[order - 1] = (
                        "PO" + spit +
                         formPO.BPCSPONo + spit +
                         order + spit +
                         l_drformPODetail.ItemCode.ToUpper() + spit +
                         VendorCode + spit +
                         l_drformPODetail.Quantity + spit +
                         l_drformPODetail.Quantity + spit +
                         l_drformPODetail.DeliveryDate.ToString("yyyyMMdd") + spit +
                         "" + spit +
                         "0" + spit +
                         l_drformPODetail.AmountRMB.ToString() + spit +
                         "0" + spit +
                         "0" + spit +
                         "EA" + spit +
                         "0" + spit +
                         "" + spit +
                         "0" + spit +
                         "0" + spit +
                         "" + spit +
                         "" + spit +
                         "" + spit +
                         DateTime.Now.ToString("yyyyMMdd") + spit +
                         masterdatabll.GetPaymentTermById(masterdatabll.GetVendorByID(formPO.VendorID).PaymentTermID)[0].PaymentTermName + spit +
                         "" + spit +
                         "0" + spit +
                         "1" + spit +
                         "1" + spit +
                         "" + spit +
                         CompanyCode + spit +
                         "0" + spit +
                         "0" + spit +
                         "0" + spit +
                         "0" + spit +
                         l_drformPODetail.DeliveryDate.ToString("yyyyMMdd") + spit +
                         "" + spit +
                         "0" + spit +
                         "0" + spit +
                         "" + spit +
                         masterdatabll.GetCurrencyByID(formPO.CurrencyID).CurrencyShortName + spit +
                         formPO.ExchangeRate + spit +
                         formPO.ExchangeRate + spit +
                         "0" + spit +
                         l_drformPODetail.AmountRMB.ToString() + spit +
                         "0" + spit +
                         "1" + spit +
                         "" + spit +
                          "0" + spit +
                         "0" + spit +
                         "0" + spit +
                        "" + spit +
                        "" + spit +
                        "" + spit +
                        "01" + spit +
                        "4" + spit +
                       masterdatabll.GetCompanyById(formPO.CompanyID).CompanyName + spit +//Ship To Company
                       authorizationbll.GetStuffUserById(form.UserID).UserName.ToUpper() + spit +
                        bSubstring(formPO.DeliveryAddress, 30) + spit +
                    SpitAddress(formPO.DeliveryAddress, 60) + spit +
                    SpitAddress(formPO.DeliveryAddress, 90) + spit +
                       "" + spit +
                       "" + spit +
                       "" + spit +
                       "0" + spit +
                       "0" + spit +
                       l_drformPODetail.DeliveryDate.ToString("yyyyMMdd") + spit +
                       "0" + spit +
                       l_drformPODetail.DeliveryDate.ToString("yyyyMMdd") + spit +
                       l_drformPODetail.DeliveryDate.ToString("yyyyMMdd") + spit +
                       "" + spit +
                       outreebll.GetOrganizationUnitById(form.OrganizationUnitID).OrganizationUnitCode + spit +
                       "" + spit +
                       "" + spit +
                       masterdatabll.GetCostCenterById(form.CostCenterID).CostCenterCode + spit +
                       l_drformPODetail.ItemCode.ToUpper() + spit +
                       l_drformPODetail.ItemDescription + spit +
                       "1" + spit +
                       "0" + spit +
                       "0" + spit +
                       "3" + spit +
                       "0" + spit +
                       "0" + spit +
                       "" + spit +
                       "" + spit +
                       "" + spit +
                       "" + spit +
                       "" + spit +
                       "" + spit +
                       "" + spit +
                       l_drformPODetail.Quantity).Replace("\r", "").Replace("\n", ""); ;
                }

                File.AppendAllLines(path + POLineFileName, PODetailcontent, Encoding.Default);
                File.AppendAllLines(BakPath + DateTime.Now.ToString("yyyyMMddhhmmss") + POLineFileName, PODetailcontent, Encoding.Default);

                File.AppendAllText(path + POFileName, POcontent + "\r\n", Encoding.Default);
                File.AppendAllText(BakPath + DateTime.Now.ToString("yyyyMMddhhmmss") + POFileName, POcontent, Encoding.Default);


            } catch (Exception e) {
                logbll.InsertExportLogDetail(logId, "单据编号：" + form.FormNo + "，提交人：" + new StuffUserBLL().GetStuffUserById(form.UserID)[0].StuffName + "，错误信息：" + e.Message);
                return false;
            }
            return true;
        }

        public string SpitAddress(string Address, int Number) {
            byte[] addressBytes = Encoding.GetEncoding("utf-8").GetBytes(Address);
            if (addressBytes.Length > Number - 30) {
                return bSubstring(Address, Number).Replace(bSubstring(Address, Number - 30), "");
            }
            return "";
        }
        #endregion

        protected override void OnStop() {

        }
        //设置并返回发票日期
        private string getInvoiceDate(int FormID, DateTime SubmitDate) {
            FormPurchaseBLL formPurchaseBLL = new FormPurchaseBLL();
            //get PV Object
            PurchaseDS.FormPVRow l_drFormPV = formPurchaseBLL.GetFormPVByID(FormID);
            //如有发票，取最近发票日期
            if (l_drFormPV.InvoiceStatusID == 1) {
                //有发票
                //get Invoice ObjectList
                PurchaseDS.FormInvoiceDataTable l_dtFormInvoice = formPurchaseBLL.GetFormInvoiceByFormID(FormID);
                //获取付款申请单上最晚发票的日期，默认第一条发票信息，如大于一条，循环遍历最近发票日期
                DateTime invoiceTime = l_dtFormInvoice[0].InvoiceDate;
                //遍历发票日期
                for (int i = 0; i < l_dtFormInvoice.Rows.Count; i++) {
                    for (int j = i + 1; j < l_dtFormInvoice.Rows.Count; j++) {
                        //发票日期比较
                        if (DateTime.Compare(l_dtFormInvoice[i].InvoiceDate, l_dtFormInvoice[j].InvoiceDate) > 0) {
                            //取到的最近日期和默认值或前一次最近日期比较
                            if (DateTime.Compare(l_dtFormInvoice[i].InvoiceDate, invoiceTime) > 0) {
                                //设置日期
                                invoiceTime = l_dtFormInvoice[i].InvoiceDate;
                            }
                        } else {
                            //取到的最近日期和默认值或前一次最近日期比较
                            if (DateTime.Compare(l_dtFormInvoice[j].InvoiceDate, invoiceTime) > 0) {
                                //设置日期
                                invoiceTime = l_dtFormInvoice[j].InvoiceDate;
                            }
                        }
                    }
                }
                //发票日期
                TimeSpan invoiceDate = new TimeSpan(invoiceTime.Ticks);
                //提交日期
                TimeSpan _submitDate = new TimeSpan(SubmitDate.Ticks);
                //日期获取间隔天数
                int intervalDays = invoiceDate.Subtract(_submitDate).Duration().Days;
                if (intervalDays < 14) {
                    //如未到14天，则为提交日期
                    return invoiceTime.ToString("yyyyMMdd");
                } else {
                    //如该付款申请单上最晚发票日期大于提交日期14天
                    return SubmitDate.ToString("yyyyMMdd");
                }
            }
            return SubmitDate.ToString("yyyyMMdd");
        }
    }
}
