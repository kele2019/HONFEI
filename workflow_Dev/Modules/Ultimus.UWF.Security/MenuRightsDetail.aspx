<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuRightsDetail.aspx.cs"
    Inherits="Ultimus.UWF.Security.MenuRightsDetail" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Expires" content="0">
    <base target="_self" />
    
    <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath()%>/Assets/js/common.js"></script>
    <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath()%>/Assets/js/selectorNew.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <fieldset>
        <legend><%=Lang.Get("SecurityDetail_Detail")%> <span class="right">
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="保存" 
                onclick="btnSave_Click" />
                <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="返回" onclick="btnBack_Click" Visible="false" 
                />
        </span></legend>
        <div class="row-fluid">
            <div class="span3">
                <%=Lang.Get("SecurityDetail_Name")%>  *</div>
            <div class="span9">
                <asp:TextBox ID="txtName" runat="server" Width="500"></asp:TextBox></div>
        </div>
        <div class="row-fluid">
            <div class="span3">
                <%=Lang.Get("SecurityDetail_Members")%>  *</div>
            <div class="span9">
                <asp:TextBox ID="txtMember" runat="server" TextMode="MultiLine" Width="500px" 
                    Height="66px"></asp:TextBox>
                <button type="button" class="btn" onclick="SelectUser({ type: '0', txtName: 'txtMember', txtId: 'txtMemberId' });">
                    ...</button>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span6">
                <strong><%=Lang.Get("SecurityDetail_Menu")%>  </strong></div>
            <div class="span6 hidden">
                <strong class="pr10"><%=Lang.Get("SecurityDetail_DataObject")%>  </strong><asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true"
                    >
                </asp:DropDownList>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span6" >
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="All">
                </asp:TreeView>
            </div>
            <div class="span6 hidden">
                <asp:TreeView ID="tvData" runat="server" ShowCheckBoxes="All">
                </asp:TreeView>
            </div>
        </div>
    </fieldset>
    <div class="hidden">
        <asp:TextBox ID="txtMemberId" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtResourceId" runat="server"></asp:TextBox>
    </div>

    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            parent.document.getElementById('frmContent').height = 800;
        });
    </script>
    
</body>
</html>
