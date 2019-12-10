<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPage.aspx.cs" Inherits="Ultimus.UWF.Common.SelectPage" %>

<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>
        <asp:Literal ID="litTitle" runat="server"></asp:Literal></title>
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/common.js"></script>
    <script type="text/javascript">
        function ReturnValue(rtnValue) {
            window.returnValue = rtnValue;
            try {
                self.opener.setValue(rtnValue);
            }
            catch (e) {
            }
            window.close();
        }

        function Ok(rtnValue) {
            window.returnValue = rtnValue;
            window.close();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-left: 5px; padding-right: 5px;">
        <div class="pt10 pb10">
            <asp:TextBox ID="txtSearch" runat="server" Width="80%"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></div>
        <asp:DataGrid ID="dgResult" runat="server" AutoGenerateColumns="False" CssClass="table  table-hover table-bordered table-condensed"
            Width="100%" AllowSorting="True" AllowPaging="true">
            <PagerStyle HorizontalAlign="Right" Mode="NumericPages" Visible="False" />
            <HeaderStyle Font-Bold="true" />
        </asp:DataGrid>
        <span class="left"><font color="red"> </font></span> <span class="left">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="Count %RecordCount%"
                Width="100%" CssClass="aspNetPager" OnPageChanged="AspNetPager1_PageChanged"
                AlwaysShow="true" FirstPageText="首页" PrevPageText="前页" NextPageText="下页" LastPageText="末页">
            </webdiyer:AspNetPager>
        </span>
    </div>
    <div  class="right" align="right">
    <asp:Button ID="btnOk" runat="server" Text="确定" CssClass="btn btn-primary" 
            onclick="btnOk_Click"  />
    <asp:Button ID="Button2" runat="server" Text="取消" CssClass="btn " OnClientClick="window.close();return false;" />
    </div>
    <div class="hidden">
        <asp:HiddenField ID="hidQuery" runat="server" />
        <asp:HiddenField ID="hidCaption" runat="server" />
        <asp:HiddenField ID="hidWidth" runat="server" />
        <asp:HiddenField ID="hidOrder" runat="server" />
        <asp:HiddenField ID="hidDBName" runat="server" />
        <asp:HiddenField ID="hidSql" runat="server" />
    </div>
    </form>
</body>
</html>
