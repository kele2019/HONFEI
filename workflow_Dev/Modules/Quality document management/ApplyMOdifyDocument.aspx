<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyMOdifyDocument.aspx.cs" Inherits="Ultimus.UWF.FunctionManager.Quality_document_management.ApplyMOdifyDocument" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quality document management</title>
      <script type="text/javascript" src="/Assets/js/common.js"></script>
      <script type="text/javascript">
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
    <td colspan="4" style="text-align: left;">Document Information</td>
    </tr>
    <tr>
    <td class="td-label"><p style="text-align:center">DOC Type </p></td>
    <td><asp:Label ID="read_DOCtype" runat="server" Text='<%#Eval("DOCtype") %>'></asp:Label> </td>
     <td class="td-label"> <p style="text-align:center">Oper Mode </p></td>
    <td> <asp:Label ID="read_OperMode" runat="server" Text='<%#Eval("OperMode") %>'></asp:Label></td>
    </tr>
    <tr>
      <td class="td-label"><p style="text-align:center">DOC Name </p></td>
    <td colspan="3">
        <asp:Label ID="read_Docname" runat="server" Width="80%" Text='<%#Eval("Docname") %>'></asp:Label></td>
    </tr>
    <tr>
     <td class="td-label"><p style="text-align:center">major change</p> </td>
    <td colspan="3">
       
        <asp:TextBox ID="read_majorchange" runat="server" Width="80%" Text='<%#Eval("DOCDescription") %>' 
            TextMode="MultiLine" Height="50px" ReadOnly="True"></asp:TextBox>
    </td>
    </tr>
    <tr>
      <td class="td-label"><p style="text-align:center">Select document</p></td>
    <td colspan="3">
        <asp:DropDownList ID="DropDOC" runat="server" Width="80%" onchange="changetype(this)">
        </asp:DropDownList>
 <asp:TextBox ID="read_selectdocument" runat="server" Text='<%#Eval("selectdocument") %>' Style="display: none"></asp:TextBox>
        <asp:Button ID="Button1" CssClass="btn validate-skip"  runat="server" Text="Down" />
    </td>
    </tr>
    <tr>
    <td style="text-align:left">Modified Document(<span style="background:red"> </span> must write)</td>
    </tr>
    <tr>
    <td class="td-label" > <span style=" background:red; float:left">&nbsp;</span> <span style="text-align:center">Select document</span> </td>
     <td colspan="3">
      <attach:attachments id="Attachments1" runat="server"></attach:attachments
    </td>
    </tr>
    </table>
    <div class="row">
                <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
        </div>

        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
    </div>

     

    </form>
</body>
</html>
