<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentNewddApply.aspx.cs" Inherits="Ultimus.UWF.FunctionManager.Quality_document_management.DocumentModifyApply" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript" src="/Assets/js/common.js"></script>
        <script type="text/javascript">
          
            function beforeSubmmit() {
                $("#checkboxlist").find("tr").each(function (i, Etr) {
                    var substr = "";
                    $(Etr).find("td").each(function (j, Item) {
                        if ($(Item).children().attr("checked")) {
                            substr += $("#domain").text() + $(Item).children().text() + "|USER;";
                        }
                    });
                    $(Etr).find(".departlist").val(substr);
                });
            }
            function changetype(obj) {
                $(obj).next().val($(obj).val());
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
   <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Quality document management" processprefix="QD" tablename="PROC_QualityModifyApply"
                    runat="server"></ui:userinfo>
            </div>
    <div class="row">
    <table class="table table-condensed table-bordered">
    <tr>
    <td colspan="4" style="text-align: left;">Document Information （<span style=" background:red">&nbsp;</span> must write）</td>
    </tr>
    <tr>
    <td class="td-label">
    <span style=" background:red;float:left;">&nbsp;</span> 
    <p style="text-align:center">DOC Type </p>
    </td>
    <td>
        <asp:DropDownList ID="DropDownType" runat="server"  onchange="changetype(this)">
        <asp:ListItem>1、2level document</asp:ListItem>
        <asp:ListItem>3、4level document</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="fld_DOCtype" runat="server" Text='<%#Eval("DOCtype") %>'  Style="display: none"></asp:TextBox>
    </td>
     <td class="td-label">
     <span style=" background:red;float:left;">&nbsp;</span> 
    <p style="text-align:center">Oper Mode </p></td>
    <td>
        <asp:DropDownList ID="DropDownList1" runat="server"  onchange="changetype(this)">
        <asp:ListItem>create document</asp:ListItem>
        <asp:ListItem>Modify document</asp:ListItem>
        </asp:DropDownList>
 <asp:TextBox ID="fld_OperMode" runat="server" Text='<%#Eval("OperMode") %>' Style="display: none"></asp:TextBox>
    </td>
    </tr>
    <tr>
      <td class="td-label">
      <span style=" background:red;float:left;">&nbsp;</span> 
    <p style="text-align:center">DOC Name </p></td>
    <td colspan="3">
        <asp:TextBox ID="fld_Docname" runat="server" Width="80%" CssClass="validate[required]" ></asp:TextBox>
    </td>
    </tr>
    <tr>
     <td class="td-label">

    <span style=" background:red; float:left">&nbsp;</span> 
    <p style="text-align:center">DOC Description</p> </td>
    <td colspan="3">
        <asp:TextBox ID="fld_DOCDescription" runat="server" Width="80%"  CssClass="validate[required]" 
            TextMode="MultiLine" Height="50px"></asp:TextBox>
    </td>
    </tr>
        </table>
        <table class="table table-condensed table-bordered" >
    <tr>
    <td colspan="3" style="text-align: left;">Department of information sign  （<span style=" background:red">&nbsp;</span> must write）</td>
    </tr>
    <tbody id="tabletbodyDetail">
     <asp:Repeater ID="Repeaterlist" runat="server" >
        <ItemTemplate>
        <tr>
        <td><asp:CheckBox ID="CheckBox1" runat="server" Text='<%# Eval("USER1") %>' Width="120px" /></td>
         <td><asp:CheckBox ID="CheckBox2" runat="server" Text='<%# Eval("USER2") %>' Width="120px" /></td>
          <td><asp:CheckBox ID="CheckBox3" runat="server" Text='<%# Eval("USER3") %>' Width="120px" /></td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        </tbody>
        <tr>
        <td colspan="3">
        <asp:TextBox Class="departlist" ID="fld_departlist" runat="server"></asp:TextBox>
        </td>
        </tr>
</table>
    </div>
    
         
    </div>
<%--    <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>--%>
    <div style="display: none;">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
         <asp:HiddenField runat="server" ID="hdPrint"  Value="0" />
         <input type="hidden" id="hidDate" />
       <asp:HiddenField runat="server" ID="hdUrgeTask" />
    </div>
    </form>
</body>
</html>
