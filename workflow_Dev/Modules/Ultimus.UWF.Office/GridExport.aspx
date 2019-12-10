<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridExport.aspx.cs" Inherits="Ultimus.UWF.Office.GridExport" %>

<html>
<head>
        <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath() %>/Assets/js/common.js"></script>
     
    <script>
        $(document).ready(function () {
            var str = window.opener.GetExportString();
            $("#<%=hfExportString.ClientID %>").val(str);
            $("#<%=btnDownload.ClientID %>").click();
        }); 
    </script>
</head>
<body>
    <form runat="server" id="form1">
        <asp:Button ID="btnDownload" style="display:none;" runat="server" Text="下载" 
            onclick="btnDownload_Click" />
    <asp:HiddenField ID="hfFileName" runat="server" />
    <asp:HiddenField ID="hfExportString" runat="server" />
    </form>
</body>
</html>
