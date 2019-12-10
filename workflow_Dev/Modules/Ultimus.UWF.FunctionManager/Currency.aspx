<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Currency.aspx.cs" Inherits="Ultimus.UWF.FunctionManager.Currency" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>汇率更新</title>

    <style type="text/css">
        #currencylist tr td{
            border:1px solid black;
        }
        #currencylist{
            border:1px solid black;
            border-collapse: collapse;
        }
        td{
            height:30px;
        }
        thead tr{
            background-color:#cbcbcb;
        }
    </style>
</head>
<body>
    <img src="images/index/u0.png"/>
     <form id="form1" runat="server" style="text-align:center;">
     <div style="left:30px;width:757px;">
        <asp:Button ID="btnAdd"  runat="server" Text="新增" CssClass="btn btn-primary"  style="width: 100px;height: 25px; font-family:'Arial Normal', 'Arial'; font-weight: 400; font-style: normal; font-size: 13px;text-decoration: none; color: #000000; text-align: center;float:right;padding: 1px 0px 1px 0px;box-sizing: border-box; margin-bottom:1%;"             OnClick="btnAddCurrency_Click"  />
     </div>
     <div></div>
     <div id="currencyList" style="left:30px;width:757px;">      
        
        <table id="currencylist" style="left:30px;width:757px;text-align:center; ">
            <thead>
                <tr>
                    <td>编号</td>
                    <td>币种</td>
                    <td>汇率</td>
                    <td></td>
                </tr>
            </thead>
            <asp:Repeater ID="rptCurrency" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Container.ItemIndex + 1%>
                        </td>
                        <td>
                            <%#Eval("DicText")%>
                        </td>
                         <td>
                            <%#Eval("DicValue")%>
                        </td>
                        <td>
                            <a href='<%#GetUpdateCurrencyUrl(Eval("ID").ToString()) %>'
                            target="_self">编辑</a>
                        </td>
                    </tr>
                </ItemTemplate>
           </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>

