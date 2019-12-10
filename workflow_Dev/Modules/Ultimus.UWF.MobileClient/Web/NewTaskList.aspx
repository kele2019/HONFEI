<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewTaskList.aspx.cs" Inherits="MobileClient.NewTaskList" %>

<%@ Register src="Header.ascx" tagname="Header" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ultimus Mobile Client</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <link href="Css/CSS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="cont">
               <uc1:Header ID="Header1" runat="server" />

        <div class="lt5" style="border: 0px;">
            <table class="lt7" border="0" cellpadding="0" cellspacing="0" align="center">
                <tr>
                    <td colspan="2">
                        <div style="color: #f56b00; padding-bottom: 30px;">
                            【<%= Request.QueryString["CategoryName"].ToString() %>】 
                            <%= Resources.Resource.NewTaskList_Remark2 %>  
                            <asp:Label ID="lbProcessCount" runat="server" Text="5"></asp:Label>
                            <%= Resources.Resource.NewTaskList_Remark3 %></div>
                    </td>
                </tr>
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td width="10%" style="padding-left: 20px;">
                                        <img src="images/btn_blue.png" alt="" width="36" height="36" />
                                    </td>
                                    <td width="90%">
                                        <a href="OpenForm.aspx?ProcessName=<%# Eval("ProcessName")%>&StepName=<%# Eval("Beginstepname") %>">
                                            <%# Eval("ProcessName")%></a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
