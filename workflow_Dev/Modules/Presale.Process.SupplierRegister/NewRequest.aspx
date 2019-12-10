<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.SupplierRegister.NewRequest" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="UTF-8" http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>

    <title>New Supplier Register Application </title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
      <script type="text/javascript">
          $(document).ready(function () {
              if ($("#hdIncident").val() != "") {
                  $("#ButtonList1_btnSubmit").val("Submit");
                  $("#ButtonList1_btnBack").hide();
                  $("#ButtonList1_btnReject").show();
              }
              if ($("#hdUrgeTask").val() == "Yes") {
                  $("#ReturnBackTask").show();
              }
              var Code = $("#fld_SupplyCode").val();
              SelectSupplyType(Code);
          });
          function beforeSubmit() {
              if ($("#fld_SupplyCode").val() == "")
              {alert("Pls Select Category of Supply");return false;}
              $("#Attachments1_txtMust").val("1");
              var summary = "New Supplier Register Application";
              $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
              return true;
          }
          function beforeSave() {
              var summary = "New Supplier Register Application";
              $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
              $("#fld_TRSummary").val(summary);
              return true;
          }
          function SelectSupplyType(obj) {
          var Code="";
          var Value="";
              switch (obj) {
                  case "1":
                      Value = "Level 0";
                      break;
              case "2":
                  Value = "Level 1";
              break;
              case "3":
                  Value = "Level 2";
              break;
                  default:
                  break;
          }
             $("#SType" + obj).attr("checked", true);
              $("#fld_SupplyCode").val(obj);
              $("#fld_SupplyValue").val(Value);
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
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Supplier Name</p>
                        </td>
                        <td class="td-content" >
                        <asp:TextBox runat="server" ID="fld_SupplierName" Width="96%" MaxLength="100"  CssClass="validate[required]"></asp:TextBox>
                        </td>

                         <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Category of Supply</p>
                        </td>
                        <td class="td-content" >
                       <input type="radio"  name="SupplyType" id="SType1" onclick="SelectSupplyType('1')" style="margin-left:25px;" />Level 0
                       <input type="radio"  name="SupplyType" id="SType2"  onclick="SelectSupplyType('2')" style="margin-left:25px;" />Level 1
                       <input type="radio"  name="SupplyType" id="SType3"  onclick="SelectSupplyType('3')" style="margin-left:25px;" />Level 2
                       <asp:TextBox runat="server" ID="fld_SupplyCode" style="display:none" ></asp:TextBox>
                       <asp:TextBox runat="server" ID="fld_SupplyValue" style="display:none" ></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                    
                     <td class="td-label">
                        <p style="text-align:center; margin-top:25%;">Description of Supply</p>
                        </td>
                        <td class="td-content"  colspan="3">
                        
                        <asp:TextBox runat="server" ID="fld_SupplierDescription" TextMode="MultiLine" Rows="5" Width="98%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    <td class="td-label">
                       <p style="text-align:center; margin-top:25%;"> Single supplier justification</p>
                    </td>
                        <td class="td-content"  colspan="3">
                        <asp:TextBox runat="server" ID="fld_SinglesupplierRemark" TextMode="MultiLine" Rows="5" Width="98%" ></asp:TextBox>
                        </td>
                    </tr>

                     <tr>
                    <td class="td-label">
                       <p style="text-align:center; margin-top:25%;">Comments</p>
                    </td>
                        <td class="td-content"  colspan="3">
                        <asp:TextBox runat="server" ID="fld_Comments" TextMode="MultiLine" Rows="5" Width="98%" ></asp:TextBox>
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
              <div id="ReturnBackTask" style="display:none; text-align:center">
              <asp:Button runat="server" ID="btnRevoke"  Visible="false" CssClass="btn" Text="Revoke" 
                onclick="btnRevoke_Click" />
          </div>
           <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
  </div>
    <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
          <asp:HiddenField runat="server" ID="hdUrgeTask" />
        </div>
    </form>
</body>
</html>
