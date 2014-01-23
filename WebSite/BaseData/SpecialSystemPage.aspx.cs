using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

public partial class BaseData_SpecialSystemPage : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }
    protected void btnGenearate_Click(object sender, EventArgs e) {
        FormSaleBLL saleBLL = new FormSaleBLL();
        FormDS.FormDataTable tbForm = null;

        string FormNoStr = this.txtFormNo.Text;
        if (!string.IsNullOrEmpty(FormNoStr)) {
            string[] FormNos = FormNoStr.Split(',');
            if (FormNos.Length > 0) {
                foreach (string FormNo in FormNos) {
                    tbForm = saleBLL.GetDataByFormNo(FormNo);
                    if (tbForm != null && tbForm.Count > 0) {
                        try {
                            saleBLL.GenerateSettlement(tbForm[0].FormID);
                            this.lblError.Text += "申请单"+FormNo+ "生成结案单成功，结案单号为<br />";
                        } catch (Exception) {
                            this.lblError.Text+=e.ToString()+"<br />";
                            throw;
                        }
                    }
                }
            }
        }
    }
}