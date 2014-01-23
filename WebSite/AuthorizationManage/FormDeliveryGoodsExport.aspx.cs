using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessObjects;
using System.Data.OleDb;

public partial class AuthorizationManage_FormDeliveryGoodsExport : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            PageUtility.SetContentTitle(this, "发货信息导入", "Delivery Goods Import");
        }
    }
    protected void btnImport_Click(object sender, EventArgs e) {
        if (this.fileUpLoad.Value.Equals("") | this.fileUpLoad.Value == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择文件!", "Please select file");
            return;
        }
        ReadDataFromClient();
    }

    private void ReadDataFromClient() {

        string filename = this.fileUpLoad.PostedFile.FileName.ToString();

        if (filename.IndexOf(".") > 0) {

            if (filename.ToLower().EndsWith("xls") || filename.ToLower().EndsWith("xlsx")) {
                string tmpFile = filename.Remove(0, filename.LastIndexOf("\\") + 1);
                filename = tmpFile;
                tmpFile = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + tmpFile;
                string tmpfilename = tmpFile;
                string pathName = CommonUtility.GetPathName();
                string path = Server.MapPath(@"~/" + pathName);
                string fullName = path + @"\" + tmpFile;
                this.fileUpLoad.PostedFile.SaveAs(fullName);
                this.LoadFile(fullName, tmpfilename, filename);
            } else {
                PageUtility.ShowModelDlg(this, "请选择excel文件", "Please select excel file!");
                return;
            }
        }

    }
    public string GetStaffNameByID(object UserID) {
        return new AuthorizationBLL().GetStuffUserById((int)UserID).StuffName;
    }
    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!string.IsNullOrEmpty(UCStartDate.SelectedDate) && !string.IsNullOrEmpty(ucEnddate.SelectedDate)) {
            if (DateTime.Parse(UCStartDate.SelectedDate) > DateTime.Parse(ucEnddate.SelectedDate)) {
                PageUtility.ShowModelDlg(this.Page, "开始时间不能大于结束时间！");
                return;
            }
        }
        gvFaillist.Visible = false;
        this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = GetWhere();
        gvlogList.DataBind();
    }
    private string GetWhere() {
        string where = " 1=1 ";
        if (!string.IsNullOrEmpty(UCStartDate.SelectedDate)) {
            where += " and ImportDate>='" + UCStartDate.SelectedDate + "'";
        }
        if (!string.IsNullOrEmpty(ucEnddate.SelectedDate)) {
            where += " and ImportDate-1<='" + ucEnddate.SelectedDate + "'";
        }
        if (!string.IsNullOrEmpty(txtUser.Text.Trim())) {
            where += " and ImportUserID in (select StuffUserID from StuffUser where stuffname like '%" + txtUser.Text.Trim() + "%' or EnglishName like '%" + txtUser.Text.Trim() + "%') ";
        }

        where += " and ImportType ='3' ";

        return where;
    }
    protected void lblErrorDetail_Click(object sender, EventArgs e) {
        //获取LinkButton对象
        LinkButton linkbtn = (LinkButton)sender;
        //引用imgbtnDelete控件的父控件上一级控件
        GridViewRow gvr = (GridViewRow)linkbtn.Parent.Parent;
        //获取ID
        string id = gvlogList.DataKeys[gvr.RowIndex].Value.ToString();
        gvFaillist.Visible = true;
        this.odsFailList.SelectParameters["logId"].DefaultValue = id;
        gvFaillist.DataBind();
    }
    public DataSet GetDataSet(string filepath) {
        try {
            string oldebConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"";
            if (filepath.EndsWith("xlsx")) {
                oldebConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath
                   + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
            }

            //excel 
            System.Data.OleDb.OleDbConnection oledbcon = new System.Data.OleDb.OleDbConnection(oldebConnString);
            oledbcon.Open();
            DataTable dtSheetName = oledbcon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
            string sheetName = dtSheetName.Rows[0]["TABLE_NAME"].ToString();
            //select 
            System.Data.OleDb.OleDbDataAdapter oledbAdaptor = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [" + sheetName + "] ", oledbcon);
            DataSet ds = new DataSet();
            oledbAdaptor.Fill(ds);
            oledbcon.Close();
            return ds;
        } catch (Exception ex) {

            throw ex;

        }
    }
    private void LoadFile(string FullPath, string FileName, string RealFileName) {

        SaveDataToDB(FullPath, FileName, RealFileName);

    }
    public void SaveDataToDB(string FullPath, string FileName, string RealFileName) {
        try {
            DataTable l_dt = null;
            LogBLL logbll = new LogBLL();
            FormSaleBLL Formsalebll = new FormSaleBLL();
            FormSampleRequestBLL formsamplerequestbll = new FormSampleRequestBLL();
            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            l_dt = this.GetDataSet(FullPath).Tables[0];
            if (l_dt.Rows.Count < 1) {
                PageUtility.ShowModelDlg(this.Page, "文件中没有任何记录，请重新选择");
                return;
            }
            List<int> FormIDs = new List<int>();
            int logId = logbll.InsertImportLog(3, RealFileName, stuffuserID, 0, 0, 0);
            int SuccessCount = 0;
            int FailCount = 0;
            int TotalCount = l_dt.Rows.Count - 1;
            for (int i = 1; i < l_dt.Rows.Count; i++) {
                try {
                    string l_strMaaNumber = l_dt.Rows[i][0].ToString();
                    string l_strSKUNo = l_dt.Rows[i][1].ToString();
                    string l_strPrice = l_dt.Rows[i][2].ToString();
                    string l_strDeliveryNo = l_dt.Rows[i][3].ToString();
                    string l_strQuantity = l_dt.Rows[i][4].ToString();
                    string l_strAmountRMB = l_dt.Rows[i][5].ToString();
                    string l_strDeliveryDate = l_dt.Rows[i][6].ToString();
                    int SkuID = 0;
                    int FormID = 0;
                    decimal Price = 0;
                    decimal AmountRMB = 0;
                    decimal Quantity = 0;
                    DateTime DeliveryDate = DateTime.Now;
                    if (string.IsNullOrEmpty(l_strMaaNumber) && string.IsNullOrEmpty(l_strSKUNo) && string.IsNullOrEmpty(l_strPrice) &&
                        string.IsNullOrEmpty(l_strDeliveryNo) && string.IsNullOrEmpty(l_strQuantity) && string.IsNullOrEmpty(l_strAmountRMB) &&
                        string.IsNullOrEmpty(l_strDeliveryDate)) {
                        TotalCount--;
                        continue;
                    }
                    if (string.IsNullOrEmpty(l_strMaaNumber) || string.IsNullOrEmpty(l_strSKUNo) || string.IsNullOrEmpty(l_strPrice) ||
                        string.IsNullOrEmpty(l_strDeliveryNo) || string.IsNullOrEmpty(l_strQuantity) || string.IsNullOrEmpty(l_strAmountRMB) ||
                        string.IsNullOrEmpty(l_strDeliveryDate)) {
                        logbll.InsertImportLogDetail(logId, i, "数据有空列，导入失败！");
                        FailCount++;
                        continue;
                    }
                    FormDS.FormDataTable l_dtForm = Formsalebll.GetDataByFormNo(l_strMaaNumber);
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
                    FormDS.FormDeliveryGoodsDataTable l_dtFormDeliveryGoods = Formsalebll.GetDataByDeliveryNoAndSkuID(l_strDeliveryNo, SkuID, l_strMaaNumber);
                    if (l_dtFormDeliveryGoods.Rows.Count > 0) {
                        result = Formsalebll.UpdateFormDeliveryGoods(int.Parse(l_dtFormDeliveryGoods.Rows[0][0].ToString()), l_strDeliveryNo, DeliveryDate, Price, Quantity, AmountRMB);
                        if (result == 0) {
                            logbll.InsertImportLogDetail(logId, i, "数据更新失败！");
                            FailCount++;
                            continue;
                        }
                    } else {
                        result = Formsalebll.InsertFormDeliveryGoods(FormID, SkuID, l_strDeliveryNo, DeliveryDate, Price, Quantity, AmountRMB);
                        if (result == 0) {
                            logbll.InsertImportLogDetail(logId, i, "数据插入失败！");
                            FailCount++;
                            continue;
                        }
                    }
                    FormIDs.Add(FormID);
                    SuccessCount++;
                } catch (Exception ee) {
                    logbll.InsertImportLogDetail(logId, i, ee.Message);
                    FailCount++;
                    continue;
                }
            }
            logbll.UpdateImportLog(logId, 3, RealFileName, stuffuserID, TotalCount, SuccessCount, FailCount);
            this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = string.Format(" LogId='{0}'", logId);
            gvFaillist.Visible = true;
            this.odsFailList.SelectParameters["logId"].DefaultValue = logId.ToString();
            gvFaillist.DataBind();
            string returnString = "成功导入" + TotalCount.ToString() + "条信息，其中成功" + SuccessCount.ToString() + "条，失败" + FailCount.ToString() + "条";
            foreach (int formID in FormIDs) {
                formsamplerequestbll.UpdateFormAfterDeliveryComplete(formID, stuffuserID);
            }
            PageUtility.ShowModelDlg(this.Page, returnString);
        } catch (Exception e) {
            throw e;
        }
    }

}