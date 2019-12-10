<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Ultimus.UWF.Home.Error" %>

<%@ Import Namespace="Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath()%>/assets/js/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <br />
        <strong  ><%=Lang.Get("Error_SystemError")%> ！</strong>
        <hr />
        <strong><%=Lang.Get("Error_info")%> :</strong>
        <asp:Literal ID="ltError" runat="server"></asp:Literal>
        <div  class="hidden">
        <asp:Literal ID="ltStack" runat="server"></asp:Literal>
        </div>
    </div>
    </form>
</body>
</html>