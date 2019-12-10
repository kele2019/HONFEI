<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewTask.aspx.cs" Inherits="MobileClient.NewTask" %>

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
        <div class="lt5">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="datalist">
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td class="lt6" align="left">
                                <table class="lt7" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="10%" style="padding-left: 20px;">
                                            <img src="images/btn_Gray.png" alt="" width="36" height="36" />
                                        </td>
                                        <td width="90%">
                                            <a href="NewTaskList.aspx?CategoryName=<%# Server.UrlEncode(Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].ToString()=="zh-CN"?Eval("CategoryCName").ToString():Eval("CategoryEName").ToString()) %>"
                                                class="nav01">
                                                <%# Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].ToString() == "zh-CN" ? Eval("CategoryCName").ToString() : Eval("CategoryEName").ToString()%>
                                                <span>(<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>) </span>
                                            </a>
                                            <asp:HiddenField runat="server" ID="CategoryName" Value='<%# Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].ToString() == "zh-CN" ? Eval("CategoryCName").ToString() : Eval("CategoryEName").ToString()%>' />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                
            </table>
        </div>
    </div>
    </form>
</body>
</html>
