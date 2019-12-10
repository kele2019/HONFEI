<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConnectionStringEncrypt.aspx.cs" Inherits="Ultimus.UWF.Common.ConnectionStringEncrypt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="146px" 
            Width="466px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="加密" />
&nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="解密" />
    </div>
    </form>
</body>
</html>
