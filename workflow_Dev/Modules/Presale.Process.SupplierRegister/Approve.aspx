<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approve.aspx.cs" Inherits="Presale.Process.SupplierRegister.Approve" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta charset="UTF-8" http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <title>New Supplier Register Application </title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
      <script type="text/javascript">
          $(document).ready(function () {
               
          });
          function beforeSubmit() {
              var summary = "New Supplier Register Application";
              $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
              return true;
          }
      </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="New Supplier Register Application" processprefix="SR" tablename="PROC_Supplier"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
             <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                        <p style="text-align:center">Supplier Name</p>
                        </td>
                        <td class="td-content" >
                        <asp:Label runat="server" ID="readd_SupplierName"  ></asp:Label>
                        </td>

                         <td class="td-label">
                          <p style="text-align:center">Category of Supply</p>
                        </td>
                        <td class="td-content" >
                       <asp:TextBox runat="server" ID="read_SupplyCode" style="display:none" ></asp:TextBox>
                       <asp:Label runat="server" ID="read_SupplyValue"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                    
                     <td class="td-label">
                        <p style="text-align:center; margin-top:25%;">Description of Supply</p>
                        </td>
                        <td class="td-content"  colspan="3">
                         <asp:Label runat="server" ID="read_SupplierDescription"  ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td class="td-label">
                       <p style="text-align:center; margin-top:25%;"> Single supplier justification</p>
                    </td>
                        <td class="td-content"  colspan="3">
                        <asp:Label runat="server" ID="read_SinglesupplierRemark"   ></asp:Label>
                        </td>
                    </tr>

                     <tr>
                    <td class="td-label">
                       <p style="text-align:center; margin-top:25%;">Comments</p>
                    </td>
                        <td class="td-content"  colspan="3">
                        <asp:Label runat="server" ID="read_Comments"  ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="4">
                    <div>
                    Level 2: Provide direct material-Most important (parts will be on our product and send to our customer)  

                    <br />
                    Level 1: Provide indirect material or service-Medial important(indirect material or service or equipment, impact our product quality)  

                    <br />
                   Level 0: Provide indirect material or service-Normal purchase (indirect material or service or equipment, without impact our product) 

                    </div>
                    </td>
                    </tr>
                    </table>
                    
            </div>
    
            
            
            <div class="row" style="display:block;">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
           <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
  </div>
    <div style="display:none">
        <asp:TextBox runat="server" ID="read_TRSummary"></asp:TextBox>
        </div>
    </form>
</body>
</html>
