<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent.aspx.cs" Inherits="Ultimus.UWF.FunctionManager.Agent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>切换用户</title>
    <style type="text/css">
        #agentlist tr td{
            border:1px solid black;
        }
        #agentlist{
            border:1px solid black;
            border-collapse: collapse;
        }
        td{
            height:30px;
        }
        thead tr{
            background-color:#cbcbcb;
        }
        .auto-style2 {
            height: 29px;
        }
        .btn-primary {}
    </style>
</head>
<body>
    <img src="images/index/u0.png"/>
     <form id="form1" runat="server" style="text-align:center;">
    <div id="agentList" style="left:30px;width:757px;">
         <asp:Button ID="btnAdd"  runat="server" Text="新增" CssClass="btn btn-primary"  style="width: 100px;height: 25px; font-family:'Arial Normal', 'Arial'; font-weight: 400; font-style: normal; font-size: 13px;text-decoration: none; color: #000000; text-align: center;float:right;padding: 1px 0px 1px 0px;box-sizing: border-box; margin-bottom:1%;" 
            OnClick="btnAddAgent_Click"  />
        <table id="agentlist" style="width:757px;text-align:center;">
            <thead >
                <tr>
                    <td>编号</td>
                    <td>代理账户</td>
                    <td>代理账号</td>
                    <td style="width:294px;"></td>
                </tr>
            </thead>
            <asp:Repeater ID="rptAgent" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Container.ItemIndex + 1%>
                        </td>
                        <td>
                            <%#Eval("USERNAME")%>
                        </td>
                         <td>
                            <%#Eval("LOGINNAME").ToString().Substring(8)%>
                        </td>
                        <td>
                            <a href='<%#GetUpdateAgentUrl(Eval("USERID").ToString()) %>'target="_self">设置密码</a>
                            <a href='<%#DeleteAgent(Eval("USERID").ToString()) %>'target="_self" onclick="javascript:return confirm('确认删除吗？')">删除</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>