<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowChart.aspx.cs"
    Inherits="MobileClient.FlowChart" %>

<%@ Register Src="FormHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ultimus Mobile Client</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <link href="../Css/CSS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="cont">
        <uc1:Header ID="Header1" runat="server" />
       
        <div class="lt04">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td width="100%" class="lt6" align="left">
                       
                       <iframe id="rightframe" name="rightframe" hspace="0" vspace="0" src="GraphicalView.aspx?ProcessName=<%=Server.UrlEncode(Request.QueryString["ProcessName"]) %>&Incident=<%=Request.QueryString["Incident"] %>"
                frameborder="0" width="98%" height="800"></iframe>
                
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
