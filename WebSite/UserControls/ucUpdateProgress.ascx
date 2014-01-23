<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUpdateProgress.ascx.cs"
    Inherits="UserControls_ucUpdateProgress" %>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
    <ProgressTemplate>
        <div style="background: #FFFFFF; filter: alpha(opacity=10); opacity: 0.1; position: absolute;
            top: 0px; left: 0px; width: 100%; height: 3000px;">
        </div>
        <div id="divProcessing" usercontrol_updateprogressname="usercontrol_UpdateProgressName"
            runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;">
            <table style="height: 136px; width: 150px; z-index: 1002;" align="center" cellpadding="0"
                cellspacing="0" class="tdborder03">
                <tr>
                    <td align="center" style=" background-color:#ffffff;">
                        <asp:Image ID="imgProcess" runat="server" ImageUrl="~/images/loading.gif" AlternateText="Processing" />
                    </td>
                </tr>
                <tr>
                    <td align="center" class="smallblue2" style="font-weight: bold;background-color:#ffffff;">
                        <asp:Label ID="Waiting" runat="server" meta:resourcekey="Waiting" />
                    </td>
                </tr>
            </table>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<script type="text/javascript">
    function setDivPosition(divName) {
        //div
        var div = document.getElementById(divName);
        //窗口对象
        var doc = document;
        //如果是在iframe里面，则要取得父窗口的scroll和窗口高、宽。
        if (parent) {
            doc = parent.document;
        }
        div.style.top = (doc.documentElement.scrollTop + (doc.documentElement.clientHeight - parseInt(div.style.height)) / 2) - 120 + "px"; ;
        div.style.left = (doc.documentElement.scrollLeft + (doc.documentElement.clientWidth - parseInt(div.style.width)) / 2) - 300 * 2 + "px";
    }

    function setDivPosition_All() {
        var items = document.getElementsByTagName("Div");
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            var tname = item.getAttribute("usercontrol_UpdateProgressName");
            if (tname == "usercontrol_UpdateProgressName") {
                //alert(item.getAttribute("Id"));
                setDivPosition(item.getAttribute("Id"));
            }
        }
    }
    setDivPosition("<%=divProcessing_ClientID %>");
</script>
<script language="javascript" type="text/javascript" for="window" event="onresize">
    setDivPosition_All();
</script>
<script language="javascript" type="text/javascript" for="window" event="onscroll">
    setDivPosition_All();
</script>
<script language="javascript" type="text/javascript" for="document" event="onmouseover">
    setDivPosition_All();
</script>
