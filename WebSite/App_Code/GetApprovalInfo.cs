using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BusinessObjects;
using System.Xml;
using BusinessObjects.MasterDataTableAdapters;
using System.Text;
using System.Data;
using lib.wf;
using lib;

/// <summary>
///GetApprovalInfo 的摘要说明
/// </summary>
[WebService(Namespace = "http://ccampbellsoup.com.cn")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class GetApprovalInfo : System.Web.Services.WebService {

    public GetApprovalInfo() {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public XmlDocument GetApprovalInfoByFormID(int FormID) {
        MasterData myDS = new MasterData();
        MasterData.ApprovalInfoDataTable tbApprovalInfo = myDS.ApprovalInfo;
        AuthorizationDS.PositionDataTable tbPosition = null;
        int index = 1;

        AuthorizationBLL authBLL = new AuthorizationBLL();
        OUTreeBLL ouBLL = new OUTreeBLL();

        MasterData.ApprovalInfoRow rowApprovalInfo = tbApprovalInfo.NewApprovalInfoRow();

        FormDS.FormRow rowForm = new FormSaleBLL().GetFormByID(FormID)[0];
        rowApprovalInfo.ApprovalInfoID = index++;
        rowApprovalInfo.StuffName = authBLL.GetStuffUserById(rowForm.UserID).StuffName;
        rowApprovalInfo.ApprovalDate = rowForm.SubmitDate.ToString("yyyy-MM-dd HH:mm");
        rowApprovalInfo.Position = ouBLL.GetPositionById(rowForm.PositionID).PositionName;
        rowApprovalInfo.Comment = "";
        rowApprovalInfo.Status = "Submit";
        tbApprovalInfo.AddApprovalInfoRow(rowApprovalInfo);

        if (!rowForm.IsProcIDNull()) {
            APWorkFlow.NodeStatusDataTable nodeTable = new APHelper().getApprovalStatus(rowForm.ProcID);
            foreach (APWorkFlow.NodeStatusRow item in nodeTable) {
                if ((!item.IsSTATUSNull()) && item.STATUS != "ASSIGNED") {
                    rowApprovalInfo = tbApprovalInfo.NewApprovalInfoRow();
                    item.PARTICIPANT = item.PARTICIPANT.Replace("PP", "P");
                    rowApprovalInfo.ApprovalInfoID = index++;
                    int i = 0;
                    foreach (var userid in item.PARTICIPANT.Split('$')[0].Split('P')) {

                        if ((!string.IsNullOrEmpty(userid)) && authBLL.GetStuffUserById(int.Parse(userid)).StuffName.Trim().Equals(item.APPROVED_BY)) {
                            tbPosition = ouBLL.GetPositionsByID(int.Parse(item.PARTICIPANT.Split('$')[1].Split('P')[i]));
                            rowApprovalInfo.Position = tbPosition.Count > 0 ? tbPosition[0].PositionName : "";
                            break;
                        }
                        i++;
                    }
                    rowApprovalInfo.StuffName = item.APPROVED_BY;
                    rowApprovalInfo.ApprovalDate = item.IsCOMPLETED_DATENull() ? "" : item.COMPLETED_DATE;

                    rowApprovalInfo.Comment = item.IsCOMMENTSNull() ? "" : item.COMMENTS;
                    rowApprovalInfo.Status = item.IsSTATUSNull() ? "" : item.STATUS;
                    tbApprovalInfo.AddApprovalInfoRow(rowApprovalInfo);
                }
            }
        }

        return new XmlDataDocument(myDS);
    }
}
