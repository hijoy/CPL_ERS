using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using BusinessObjects;

namespace Import_Console {
    class Program {
        static void Main(string[] args) {
            new Program().ImporterData();
        }

        public static string SpitVendorAddress(string Address) {
            byte[] addressBytes = Encoding.GetEncoding("utf-8").GetBytes(Address);
            if (addressBytes.Length <= 28) {
                return "";
            } else {
                return bSubstring(Address, 56).Replace(bSubstring(Address, 28), "");
            }
        }

        public static String bSubstring(string s, int length) {
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

        private void ImportTxtData(int importType, FileSystemInfo[] fsis) {
            foreach (FileSystemInfo fsi in fsis) {
                FormSampleRequestBLL formsamplerequestbll = new FormSampleRequestBLL();
                MasterDataBLL masterdatabll = new MasterDataBLL();
                LogBLL logbll = new LogBLL();
                FormSaleBLL formbll = new FormSaleBLL();
                int SuccessCount = 0;
                int FailCount = 0;
                string[] Content = File.ReadAllLines(fsi.FullName, Encoding.Default);
                //插入LOG主数据
                int logId = logbll.InsertImportLog(importType, fsi.Name, 27, Content.Length, SuccessCount, FailCount);
                for (int i = 0; i < Content.Length; i++) {
                    try {
                        //导入数据
                        #region 类型为SKU
                        if (importType == 0) {
                            string[] SKU = Content[i].Split('\t');
                            if (SKU.Length != 13) {
                                logbll.InsertImportLogDetail(logId, i + 1, "SKU数据COUNT错误！");
                                FailCount += 1;
                                continue;
                            }
                            //SKU = Content[i].Replace("\"", "").Split('\t');
                            Boolean active = false;
                            if (SKU[12].ToUpper().IndexOf("IM") >= 0) {
                                active = true;
                            } else if (SKU[12].ToUpper().IndexOf("IZ") >= 0) {
                                active = false;
                            } else {
                                logbll.InsertImportLogDetail(logId, i + 1, "active数据错误！");
                                FailCount += 1;
                                continue;
                            }

                            int BrandId = 0;
                            int SKUCategoryId = 0;
                            int SKUTypeID = 0;

                            MasterData.BrandDataTable Brand = masterdatabll.GetBrandByName(SKU[2]);
                            if (Brand.Rows.Count > 0) {
                                BrandId = Brand[0].BrandID;
                            } else {
                                logbll.InsertImportLogDetail(logId, i + 1, string.Format("编号为{0}的产品未找到Brand({1})！", SKU[0], SKU[2]));
                                FailCount += 1;
                                continue;
                            }
                            MasterData.SKUCategoryDataTable SKUCategory = masterdatabll.GetSKUCategoryByName(SKU[3]);
                            if (SKUCategory.Rows.Count > 0) {
                                SKUCategoryId = SKUCategory[0].SKUCategoryID;
                            } else {
                                logbll.InsertImportLogDetail(logId, i + 1, string.Format("编号为{0}的产品未找到SKU Category({1})！", SKU[0], SKU[3]));
                                FailCount += 1;
                                continue;
                            }
                            MasterData.SKUTypeDataTable SKUType = masterdatabll.GetSKUTypeByName(SKU[4]);
                            if (SKUType.Rows.Count > 0) {
                                SKUTypeID = SKUType[0].SKUTypeID;
                            }
                            MasterData.SKUDataTable dtSKU = masterdatabll.GetSKUBySKUNo(SKU[0]);
                            if (dtSKU.Rows.Count > 0) {
                                //Update
                                masterdatabll.UpdateSKU(dtSKU[0].SKUID, SKU[0], SKU[1], BrandId, SKUTypeID, SKUCategoryId, SKU[5], SKU[6], SKU[7], SKU[8], SKU[9], Decimal.Parse(SKU[10]), SKU[11], active);
                            } else if (active)//未找到数据  并且active 是ture的才做插入
                            {
                                //导入数据
                                masterdatabll.InsertSKU(SKU[0], SKU[1], BrandId, SKUTypeID, SKUCategoryId, SKU[5], SKU[6], SKU[7], SKU[8], SKU[9], Decimal.Parse(SKU[10]), SKU[11], active);
                            }
                        }
                        #endregion

                        #region 类型为Item
                        if (importType == 1) {
                            string[] Item = Content[i].Split('\t');
                            if (Item.Length != 6) {
                                logbll.InsertImportLogDetail(logId, i + 1, "Item数据COUNT错误！");
                                FailCount += 1;
                                continue;
                            }
                            //Item = Content[i].Replace("\"", "").Split('\t');

                            int ItemCategoryID = 0;
                            Boolean active = false;
                            if (Item[5].ToUpper().IndexOf("PC") >= 0) {
                                active = true;
                            } else if (Item[5].ToUpper().IndexOf("PZ") >= 0) {
                                active = false;
                            } else {
                                logbll.InsertImportLogDetail(logId, i + 1, "active数据错误！");
                                FailCount += 1;
                                continue;
                            }
                            MasterData.ItemCategoryDataTable ItemCategory = masterdatabll.GetItemCategoryByCode(Item[2]);
                            if (ItemCategory.Rows.Count > 0) {
                                ItemCategoryID = ItemCategory[0].ItemCategoryID;
                            } else {
                                logbll.InsertImportLogDetail(logId, i + 1, "未找到Item Category！");
                                FailCount += 1;
                                continue;
                            }
                            MasterData.ItemDataTable dtItem = masterdatabll.GetItemByCode(Item[0]);
                            if (dtItem.Rows.Count > 0) {
                                //Update
                                masterdatabll.UpdateItem(dtItem[0].ItemID, Item[0], Item[1], ItemCategoryID, Item[3], Item[4], active);
                            } else if (active)//未找到数据  并且active 是ture的才做插入
                            {
                                //导入数据
                                masterdatabll.InsertItem(Item[0], Item[1], ItemCategoryID, Item[3], Item[4], active);
                            }
                        }
                        #endregion

                        #region 类型为Customer
                        if (importType == 2) {
                            string[] Customer = Content[i].Split('\t');
                            if (Customer.Length != 14) {
                                logbll.InsertImportLogDetail(logId, i + 1, "Customer数据COUNT错误！");
                                FailCount += 1;
                                continue;
                            }
                            //Customer = Content[i].Replace("\"", "").Split('\t');
                            Boolean active = false;
                            if (Customer[12].ToUpper().IndexOf("CM") >= 0) {
                                active = true;
                            } else if (Customer[12].ToUpper().IndexOf("CZ") >= 0) {
                                active = false;
                            } else {
                                logbll.InsertImportLogDetail(logId, i + 1, "active数据错误！");
                                FailCount += 1;
                                continue;
                            }
                            int CustomerTypeID = 0;
                            int CustomerChannelID = 0;
                            int ProvinceID = 0;
                            int CustomerRegionID = 0;
                            //查询ID

                            MasterData.CustomerTypeDataTable CustomerType = masterdatabll.GetCustomerTypeByName(Customer[5]);
                            if (CustomerType.Rows.Count > 0) {
                                CustomerTypeID = CustomerType[0].CustomerTypeID;
                            } else {


                                logbll.InsertImportLogDetail(logId, i + 1, "未找到Customer Type！");
                                FailCount += 1;
                                continue;
                            }

                            MasterData.CustomerChannelDataTable CustomerChannel = masterdatabll.GetCustomerChanneByCode(Customer[6]);
                            if (CustomerChannel.Rows.Count > 0) {
                                CustomerChannelID = CustomerChannel[0].CustomerChannelID;
                            } else {
                                logbll.InsertImportLogDetail(logId, i + 1, "未找到Customer Channel！");
                                FailCount += 1;
                                continue;
                            }

                            MasterData.ProvinceDataTable Province = masterdatabll.GetProvinceByName(Customer[8]);
                            if (Province.Rows.Count > 0) {
                                ProvinceID = Province[0].ProvinceID;
                            } else {
                                logbll.InsertImportLogDetail(logId, i + 1, "未找到Province！");
                                FailCount += 1;
                                continue;
                            }

                            MasterData.CustomerRegionDataTable CustomerRegion = masterdatabll.GetCustomerRegionByName(Customer[9]);
                            if (CustomerRegion.Rows.Count > 0)
                                CustomerRegionID = CustomerRegion[0].CustomerRegionID;
                            else {
                                logbll.InsertImportLogDetail(logId, i + 1, "未找到Customer Region！");
                                FailCount += 1;
                                continue;
                            }
                            MasterData.CustomerDataTable dtCustomer = masterdatabll.GetCustomerByNo(Customer[0]);
                            if (dtCustomer.Rows.Count > 0) {
                                //Update
                                masterdatabll.UpdateCustomer(dtCustomer[0].CustomerID, Customer[0], Customer[1], Customer[2], Customer[3], Customer[4], Customer[4],
                                    CustomerTypeID, CustomerChannelID, Customer[7], ProvinceID, CustomerRegionID, Customer[10], Customer[11],
                                    "", "", "", "", active, Customer[13]);
                            } else if (active)//未找到数据  并且active 是ture的才做插入
                            {
                                //导入数据
                                masterdatabll.InsertCustomer(Customer[0], Customer[1], Customer[2], Customer[3], Customer[4], Customer[5],
                                    CustomerTypeID, CustomerChannelID, Customer[7], ProvinceID, CustomerRegionID, Customer[10], Customer[11],
                                    "", "", "", "", active, Customer[13]);
                            } else {
                                logbll.InsertImportLogDetail(logId, i + 1, "未激活的数据不导入！");
                                FailCount += 1;
                                continue;
                            }
                        }
                        #endregion

                        #region 类型为货补
                        if (importType == 3) {
                            string[] Delivery = Content[i].Split('\t');
                            if (Delivery.Length != 7) {
                                logbll.InsertImportLogDetail(logId, i + 1, "Delivery数据COUNT错误！");
                                FailCount += 1;
                                continue;
                            }
                            string l_strMaaNumber = Delivery[0].ToString();
                            string l_strSKUNo = Delivery[1].ToString();
                            string l_strPrice = Delivery[2].ToString();
                            string l_strDeliveryNo = Delivery[3].ToString();
                            string l_strQuantity = Delivery[4].ToString();
                            string l_strAmountRMB = Delivery[5].ToString();
                            string l_strDeliveryDate = Delivery[6].ToString();
                            int SkuID = 0;
                            int FormID = 0;
                            decimal Price = 0;
                            decimal AmountRMB = 0;
                            decimal Quantity = 0;
                            DateTime DeliveryDate = DateTime.Now;
                            if (string.IsNullOrEmpty(l_strMaaNumber) || string.IsNullOrEmpty(l_strSKUNo) || string.IsNullOrEmpty(l_strPrice) ||
                                string.IsNullOrEmpty(l_strDeliveryNo) || string.IsNullOrEmpty(l_strQuantity) || string.IsNullOrEmpty(l_strAmountRMB) ||
                                string.IsNullOrEmpty(l_strDeliveryDate)) {
                                logbll.InsertImportLogDetail(logId, i, "数据有空列，导入失败！");
                                FailCount++;
                                continue;
                            }
                            FormDS.FormDataTable l_dtForm = formbll.GetDataByFormNo(l_strMaaNumber);
                            if (l_dtForm.Rows.Count == 0) {
                                logbll.InsertImportLogDetail(logId, i, "未找到MAA号:" + l_strMaaNumber + "的单据，导入失败！");
                                FailCount++;
                                continue;
                            } else {
                                FormID = l_dtForm[0].FormID;
                            }
                            MasterData.SKUDataTable l_dtSKU = new MasterDataBLL().GetSKUBySKUNo(l_strSKUNo);
                            if (l_dtSKU.Rows.Count == 0) {
                                logbll.InsertImportLogDetail(logId, i, "未找到此SKU:" + l_strSKUNo + " NUMBER 下的SKU，导入失败！");
                                FailCount++;
                                continue;
                            } else {
                                SkuID = l_dtSKU[0].SKUID;
                            }
                            //价格是成本价，进入系统应该是
                            try {
                                Price = decimal.Round(decimal.Parse(l_strPrice) * (decimal)1.1 * (decimal)1.17, 2);
                            } catch {
                                logbll.InsertImportLogDetail(logId, i, "单价错误，导入失败！");
                                FailCount++;
                                continue;
                            }
                            try {
                                Quantity = decimal.Parse(l_strQuantity);
                            } catch {
                                logbll.InsertImportLogDetail(logId, i, "数量错误，导入失败！");
                                FailCount++;
                                continue;
                            }
                            try {
                                AmountRMB = Price * Quantity;
                            } catch {
                                logbll.InsertImportLogDetail(logId, i, "发货金额，导入失败！");
                                FailCount++;
                                continue;
                            }
                            try {
                                DeliveryDate = DateTime.Parse(l_strDeliveryDate);
                            } catch {
                                logbll.InsertImportLogDetail(logId, i, "日期错误，导入失败！");
                                FailCount++;
                                continue;
                            }
                            if (string.IsNullOrEmpty(l_strDeliveryNo)) {
                                logbll.InsertImportLogDetail(logId, i, "订单号为空，导入失败！");
                                FailCount++;
                                continue;
                            }
                            int result = 0;
                            FormDS.FormDeliveryGoodsDataTable l_dtFormDeliveryGoods = formbll.GetDataByDeliveryNoAndSkuID(l_strDeliveryNo, SkuID, l_strMaaNumber);
                            if (l_dtFormDeliveryGoods.Rows.Count > 0) {
                                result = formbll.UpdateFormDeliveryGoods(int.Parse(l_dtFormDeliveryGoods.Rows[0][0].ToString()), l_strDeliveryNo, DeliveryDate, Price, Quantity, AmountRMB);
                                if (result == 0) {
                                    logbll.InsertImportLogDetail(logId, i, "数据更新失败！");
                                    FailCount++;
                                    continue;
                                }
                            } else {
                                result = formbll.InsertFormDeliveryGoods(FormID, SkuID, l_strDeliveryNo, DeliveryDate, Price, Quantity, AmountRMB);
                                if (result == 0) {
                                    logbll.InsertImportLogDetail(logId, i, "数据插入失败！");
                                    FailCount++;
                                    continue;
                                }
                            }
                            formsamplerequestbll.UpdateFormAfterDeliveryComplete(FormID, 27);
                        }
                        #endregion

                        #region 类型为APH
                        if (importType == 4) {
                            string[] PaymentInfo = Content[i].Split('\t');
                            if (PaymentInfo.Length != 3) {
                                logbll.InsertImportLogDetail(logId, i + 1, "APH数据COUNT错误！");
                                FailCount += 1;
                                continue;
                            }
                            FormDS.FormDataTable l_dtform = new FormDS.FormDataTable();
                            l_dtform = formbll.GetDataByFormNo(PaymentInfo[0]);
                            if (l_dtform.Rows.Count == 0) {
                                logbll.InsertImportLogDetail(logId, i + 1, "未找到此Form单号：" + PaymentInfo[0] + "的单据");
                                FailCount += 1;
                                continue;
                            }
                            FormDS.FormRow l_drform = l_dtform[0];
                            if (l_drform.FormTypeID == (int)SystemEnums.FormType.FormMarketingPayment ||
                                l_drform.FormTypeID == (int)SystemEnums.FormType.PVApply ||
                                l_drform.FormTypeID == (int)SystemEnums.FormType.RDPayment ||
                                l_drform.FormTypeID == (int)SystemEnums.FormType.SalePayment ||
                                l_drform.FormTypeID == (int)SystemEnums.FormType.TravelReimburseApply ||
                                l_drform.FormTypeID == (int)SystemEnums.FormType.PersonalReimburseApply) {

                                try {
                                    if (!string.IsNullOrEmpty(PaymentInfo[1]))
                                        l_drform.PaymentDate = DateTime.Parse(PaymentInfo[1].Substring(0, 4) + "-" + PaymentInfo[1].Substring(4, 2) + "-" + PaymentInfo[1].Substring(6, 2));
                                    else {
                                        logbll.InsertImportLogDetail(logId, i + 1, "付款时间不能为空！");
                                        FailCount += 1;
                                        continue;
                                    }
                                } catch {
                                    logbll.InsertImportLogDetail(logId, i + 1, "时间格式不正确");
                                    FailCount += 1;
                                    continue;
                                }
                            } else {
                                logbll.InsertImportLogDetail(logId, i + 1, "单号：" + PaymentInfo[0] + " 不是正确的付款单号");
                                FailCount += 1;
                                continue;
                            }
                            try {
                                if (!string.IsNullOrEmpty(PaymentInfo[2])) {
                                    decimal PaymentAmount = decimal.Parse(PaymentInfo[2]);
                                    l_drform.PaymentAmount = PaymentAmount;
                                } else {
                                    logbll.InsertImportLogDetail(logId, i + 1, "付款金额不能为空！");
                                    FailCount += 1;
                                    continue;
                                }
                            } catch {
                                logbll.InsertImportLogDetail(logId, i + 1, "付款金额错误！");
                                FailCount += 1;
                                continue;
                            }
                            formbll.TAForm.Update(l_drform);
                        }
                        #endregion

                        SuccessCount += 1;
                    } catch (Exception ee) {
                        logbll.InsertImportLogDetail(logId, i + 1, ee.Message);
                        FailCount += 1;
                        continue;
                    }
                }
                //Update SuccessCount FailCount
                logbll.UpdateImportLog(logId, importType, fsi.Name, 1, Content.Length, SuccessCount, FailCount);
            }
        }

        private void ImporterData() {
            String FilePath = ConfigurationManager.AppSettings["ImporterService.FilePath"];
            String BakPath = ConfigurationManager.AppSettings["ImporterService.BakPath"];
            int hour = int.Parse(ConfigurationManager.AppSettings["ImporterService.hour"]);
            MasterDataBLL masterdatabll = new MasterDataBLL();
            LogBLL logbll = new LogBLL();
            DirectoryInfo mydir = new DirectoryInfo(FilePath);
            //0 SKU  1 Item 2 Customer 
            FileSystemInfo[] SKUfile = mydir.GetFileSystemInfos("IIM*");
            if (SKUfile.Length > 0) {
                ImportTxtData(0, SKUfile);
            } else {
                int logId = logbll.InsertImportLog(0, "未找到文件", 1, 0, 0, 1);
                logbll.InsertImportLogDetail(logId, 0, "未找到SKU文件！");
            }
            FileSystemInfo[] Itemfile = mydir.GetFileSystemInfos("HPC*");
            if (Itemfile.Length > 0) {
                ImportTxtData(1, Itemfile);
            } else {
                int logId = logbll.InsertImportLog(1, "未找到文件", 1, 0, 0, 1);
                logbll.InsertImportLogDetail(logId, 0, "未找到Item文件！");
            }
            FileSystemInfo[] Customerfile = mydir.GetFileSystemInfos("RCM*");
            if (Customerfile.Length > 0) {
                ImportTxtData(2, Customerfile);
            } else {
                int logId = logbll.InsertImportLog(2, "未找到文件", 1, 0, 0, 1);
                logbll.InsertImportLogDetail(logId, 0, "未找到Customer文件！");
            }

            //FileSystemInfo[] DeliveryGoods = mydir.GetFileSystemInfos("DeliveryGoods*");
            //if (DeliveryGoods.Length > 0) {
            //    ImportTxtData(3, Customerfile);
            //} else {
            //    int logId = logbll.InsertImportLog(3, "未找到文件", 1, 0, 0, 1);
            //    logbll.InsertImportLogDetail(logId, 0, "未找到Delivery Goods文件！");
            //}

            FileSystemInfo[] APHPayment = mydir.GetFileSystemInfos("APHPAY*");
            if (APHPayment.Length > 0) {
                ImportTxtData(4, APHPayment);
            } else {
                int logId = logbll.InsertImportLog(4, "未找到文件", 1, 0, 0, 1);
                logbll.InsertImportLogDetail(logId, 0, "未找到APHPAY文件！");
            }

            foreach (FileSystemInfo file in mydir.GetFileSystemInfos()) {
                File.Move(file.FullName, BakPath + file.Name);
            }
        }
    }
}
