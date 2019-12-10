<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MobileClient.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ultimus Mobile Client</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            background: url(images/bg.jpg);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <img src="images/logo.png" />
        </div>
    </div>
    <br />
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span4">
                <span style="color: #FFF; font-family: 黑体; font-size: 22px;">
                    <%= Resources.Resource.Login_UserName %></span></div>
            <div class="span8" >
                <asp:TextBox ID="txtAccount" CssClass="span10" runat="server"></asp:TextBox></div>
        </div>
        <div class="row-fluid">
            <div class="span4">
                <span style="color: #FFF; font-family: 黑体; font-size: 24px;">
                    <%= Resources.Resource.Login_PassWord%></span></div>
            <div class="span8">
                <asp:TextBox ID="txtPassWord" runat="server" MaxLength="15" TextMode="Password" CssClass="span10"></asp:TextBox>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span4">
                <span style="color: #FFF; font-family: 黑体; font-size: 24px;">
                    <%= Resources.Resource.Login_Domain%></span></div>
            <div class="span8">
                <asp:DropDownList ID="dropDomain" CssClass="span10" runat="server">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span4">
                <span style="color: #FFF; font-family: 黑体; font-size: 24px;">
                    <%= Resources.Resource.LoginLanguages %></span>
            </div>
            <div class="span8">
                <asp:Button ID="btncn" runat="server" 
                    Text="<%$ Resources:Resource,LoginLanguages_ZH %>" onclick="btncn_Click"/>
                <asp:Button ID="btnen" runat="server" 
                    Text="<%$ Resources:Resource,LoginLanguages_EN %>" onclick="btnen_Click"/>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <br />
                <a href="#">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/Login.png" OnClick="ImageButton1_Click" />
                </a>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
