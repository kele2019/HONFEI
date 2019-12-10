<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="olePage4.aspx.cs" Inherits="BPM.popup.olePage4" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<META HTTP-EQUIV="Content-Type"  CONTENT="text/html; CHARSET=gb2312"/>
<HTML>
	<HEAD>
		<title>Support_CompanyList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="Content-Type" content="text/html; charset=GB2312" />
		<base target="_self"/>
    <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath()%>/Assets/js/jquery.js"></script>
		<LINK href="../css/css.css" type="text/css" rel="stylesheet"/>
		<script language="JavaScript" src="../images/msg.js" type="text/JavaScript"></script>
		<script>

		    function ReturnValue(rtnValue) {
		        window.returnValue = rtnValue;
		        window.close();
		    }
		    function SelectAll() {

		        if ($("#<%=btnSelectAll.ClientID %>").val() == "全选") {
		            if ($("#<%=dgResult.ClientID %>").find("tr[sel='true']").size() < $("#<%=dgResult.ClientID %>").find("tr:gt(0)").size())
		                $("#<%=dgResult.ClientID %>").find("tr[sel='true']").click();
		            $("#<%=btnSelectAll.ClientID %>").val("取消");
		            $("#<%=dgResult.ClientID %>").find("tr:gt(0)").click();
		        } else {
		            if ($("#<%=dgResult.ClientID %>").find("tr[sel='false']").size() < $("#<%=dgResult.ClientID %>").find("tr:gt(0)").size())
		                $("#<%=dgResult.ClientID %>").find("tr[sel='false']").click();
		            $("#<%=dgResult.ClientID %>").find("tr:gt(0)").click();
		            $("#<%=btnSelectAll.ClientID %>").val("全选");
		        }
		        return false;
		    }
		    var multiValue = "";
		    function getMulti(rtnValue, ctl) {
		        var pColor = ctl.style.backgroundColor;
		        //alert(pColor);
		        if (pColor != "white") {
		            $(ctl).attr("sel", false);
		            ctl.style.backgroundColor = "white";
		            ctl.style.color = "black";
		            multiValue = multiValue.replace(rtnValue + "|", "");
		        } else {
		            $(ctl).attr("sel", true);
		            ctl.style.backgroundColor = "#C0C0FF";
		            ctl.style.color = "white";
		            multiValue += rtnValue + "|";
		        }
		    }

		    function MouseEvent(ctl) {
		        var pColor = ctl.style.backgroundColor;
		        ctl.style.cursor = "hand";
		        if (pColor != "white") {
		            ctl.style.backgroundColor = "white";
		            ctl.style.color = "black";
		        }
		        else {
		            ctl.style.backgroundColor = "#C0C0FF";
		            ctl.style.color = "white";
		        }

		    }
		</script>
	</HEAD>
	<body topmargin="0" leftmargin="0" style="text-align: left">
		<form id="Form1" method="post" runat="server">
        <%--<div style="width:800px">--%>
		    <hr style="color: white" />
		        <asp:TextBox ID="txtSearch" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Width="80px" Text="查询" BackColor="#DBEAED" />
                <asp:Button ID="btnConfirm" runat="server" Width="80px" Text="选择并关闭" BackColor="#DBEAED"
            OnClientClick="ReturnValue(multiValue)" />
                <asp:Button ID="btnSelectAll" runat="server" Width="80px" Text="全选" BackColor="#DBEAED"
            OnClientClick="return SelectAll();" />
                <hr style="color: white" />
                <FONT face="宋体"><asp:DataGrid ID="dgResult" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#CCCCFF" BorderStyle="None" CellPadding="0" CssClass="table"
                        PageSize="100" ToolTip="双击行直接选择!" Width="100%" AllowSorting="True">
                        <FooterStyle BackColor="#8080FF" ForeColor="Red" HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <ItemStyle BackColor="White" CssClass="TableItem" ForeColor="Black"
                            VerticalAlign="Middle" Width="100px" Font-Bold="False" Font-Italic="False" Height="22px" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Names="宋体" Font-Size="Smaller" />
                        <HeaderStyle BackColor="#DBEAED" CssClass="TableHead" Font-Bold="True" Font-Size="Smaller" Height="23px" HorizontalAlign="Center" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <PagerStyle BackColor="White" ForeColor="Red" HorizontalAlign="Left"
                            NextPageText="Pre." PageButtonCount="30" PrevPageText="Next" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Visible="False" />
                    </asp:DataGrid></FONT>            
                <asp:Label ID="lblSQL" runat="server" Text="Label" Visible="False"></asp:Label><asp:Label
                    ID="lblName" runat="server" Text="Label" Visible="False"></asp:Label><asp:Label ID="lblWidth"
                        runat="server" Text="Label" Visible="False"></asp:Label><asp:Label ID="lblConn" runat="server"
                            Text="Label" Visible="False"></asp:Label><asp:Label ID="lblOrder" runat="server"
                                Text="Label" Visible="False"></asp:Label><asp:Label ID="lblTitle" runat="server"
                                    Text="Label" Visible="False"></asp:Label><asp:Label ID="lblisMulti" runat="server"
                                    Text="Label" Visible="False"></asp:Label>

		    </form>
        <%--</div>--%>
	</body>
</HTML>

