<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowChart.aspx.cs" Inherits="MobileClient.FlowChart" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="FormHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ultimus Mobile Client</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <link href="Css/CSS.css" rel="stylesheet" type="text/css" />
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="cont">
        <uc1:Header ID="Header1" runat="server" />
        <div class="well well-small" style="color: #f56b00;padding-bottom:0;margin-bottom:0;">
            <h4>
                建店流程</h4>
        </div>
        <div>
            <table class="table table-bordered">
                <tr>
                    <th>
                        申请单号
                    </th>
                    <td>
                        WF201301050001
                    </td>
                </tr>
                <tr>
                    <th>
                        申请人
                    </th>
                    <td>
                        张三
                    </td>
                </tr>
                <tr>
                    <th>
                        申请时间
                    </th>
                    <td>
                        2013-1-5
                    </td>
                </tr>
                <tr>
                    <th>
                        审批意见
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="images/approve.png" OnClientClick="return confirm('您确定提交吗？');" />
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/return.png" OnClientClick="return confirm('您确定提交吗？');" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
