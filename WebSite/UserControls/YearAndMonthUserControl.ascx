<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YearAndMonthUserControl.ascx.cs" Inherits="UserControls_YearAndMonthUserControl" %>
<script type="text/javascript" >
function CheckExpensePeriodDatePicker(obj)
{
    strDate=obj.value;
    strDate=strDate.replace(/^(\s)*|(\s)*$/g,"");//ȥ���ַ������ߵĿո�
    
   //��֤���򣺳����ڸ�ʽ��������0���룬��2003-09-01
    var newPar=/^\d{4}?(?:0[1-9]|1[0-2])$/
    if (strDate.length>0 && newPar.test(strDate)==false)
    {
        obj.focus();
		alert("���ڸ�ʽ���ԣ����������룮");
		obj.value="";	
	    return false;
	}
	else
	{
	   return true;
	}
}

// -->
</script>

    
<asp:TextBox ID="txtDate" runat="server" Width="80px" ToolTip="��ʽ:yyyymm" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
<asp:ImageButton
    ID="ibtDate" runat="server" ImageUrl="~/images/Calendar.gif" BorderColor="Silver"
    BorderStyle="None" BorderWidth="0px" ImageAlign="AbsMiddle" Height="19px" Width="19px" TabIndex="-1" AccessKey="?" />
<!--<span style="color:Red">(��ʽ:yyyymm)</span>-->