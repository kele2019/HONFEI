<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonInfo.aspx.cs" Inherits="Ultimus.UWF.OrgChart.PersonInfo" %>

<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <script type="text/javascript" src="../../Assets/js/common.js"></script>
    <script type="text/javascript">
        function openDetail(id) {
            window.location.href = "SecurityDetail.aspx";
            //returnValue = window.showModalDialog("PermissionDetail.aspx", "detail", "dialogWidth:800px;dialogHeight:600px");
            return false;
        }
    </script>
</head>
<body>
    
    <form id="form1" runat="server">
    <fieldset>
        <legend> <%=Lang.Get("PersonInfo_Title") %></legend>
        <div style="width:80%;padding-left:20px;">
        <table class="table  table-bordered table-condensed" >
            <tr>
                <td class="td-label">
                     <%=Lang.Get("PersonInfo_LoginName")%>：
                </td>
                <td class="td-content">
                    <asp:Label ID="lblAccount" runat="server" CssClass="  "></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td-label">
                      <%=Lang.Get("PersonInfo_Name")%>： 
                </td>
                <td class="td-content">
                    <asp:Label ID="lblName" runat="server" CssClass="  "></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td-label">
                      <%=Lang.Get("PersonInfo_JobFunction")%>： 
                </td>
                <td class="td-content">
                    <asp:Label ID="lblTitle" runat="server" CssClass="  "></asp:Label>
                </td>
            </tr>
            <tr class="hidden">
                <td class="td-label">
                     <%=Lang.Get("PersonInfo_Email")%>： 
                </td>
                <td class="td-content">
                    <asp:Label ID="lblEmail" runat="server" CssClass="  "></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td-label">
                     <%=Lang.Get("PersonInfo_Department")%>： 
                </td>
                <td class="td-content">
                    <asp:Label ID="lblDepartment" runat="server" CssClass="  "></asp:Label>
                </td>
            </tr>
            <tr class="hidden">
                <td class="td-label">
                     <%=Lang.Get("PersonInfo_DirectReport")%>：  
                </td>
                <td class="td-content">
                    <asp:Label ID="lblDirectReport" runat="server" CssClass="  "></asp:Label>
                </td>
            </tr>
            <tr  >
                <td class="td-label">
                     Email：  
                </td>
                <td class="td-content">
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr  >
                <td class="td-label">
                     密码：  
                </td>
                <td class="td-content">
                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr  >
                <td class="td-label">
                     重复密码：  
                </td>
                <td class="td-content">
                    <asp:TextBox ID="txtPwd2" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr align="center">
            <td colspan="2" align="center"><asp:Button
                ID="btnSearch" runat="server" Text="保存" CssClass="btn  btn-primary" 
                    onclick="btnSearch_Click"   />
                
                </td>
             
            </tr>
        </table></div>
    </fieldset>
    </form>
</body>
</html>
