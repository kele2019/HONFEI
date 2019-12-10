<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.Headcount.NewRequest" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Headcount Request Application</title>
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
            var EvalueatinValue = $("#fld_HeadcountStatusCode").val();
            if (EvalueatinValue != "")
                $("input[name^='Headcountstatus']").eq((EvalueatinValue -2) * (-1)).attr("checked", true);
        });
        function DropChangeText(obj) {
            var DropText = $(obj).find("option:selected").text();
            $(obj).next().val(DropText);
        }
        function CheckedValue(flag, vindex) {
            $('#fld_' + flag).val(vindex);
//            if (vindex == "1")
//                $("#fld_HeadcountComments").attr("class", "validate[required]");
//                else
//                    $("#fld_HeadcountComments").removeClass("validate[required]");
        }
        function beforeSave() {
            var indexlen = $("#UserInfo1_fld_APPLICANT").text().indexOf('(');
            var UserName = $("#UserInfo1_fld_APPLICANT").text().substr(0, indexlen);
            var summary ="Headcount Request Application";

            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
    <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Headcount Request Application" processprefix="HC" tablename="PROC_HeadCount"
                    runat="server"  ></ui:userinfo>
                    
            </div>

              <div class="row">
              <span style="color:Red;">Note: "Band","Job Level","Global Job Code"  will provided by HR, Please confirm with HR before submit.</span>
               <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                          <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Position Title
                        </td>
                        <td>
                        <asp:TextBox runat="server" ID="fld_PositionTitle" MaxLength="100" Width="95%" CssClass="validate[required]" ></asp:TextBox>
                        </td>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Budget</td>
                        <td>
                        <asp:DropDownList runat="server" ID="fld_BudgetInfo" Width="98%" CssClass="validate[required]" >
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        </asp:DropDownList>
                        </td>
                        </tr>
                        <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Primary location </td>
                        <td>
                        <asp:TextBox runat="server" ID="fld_Primarylocation" MaxLength="100" Width="95%" CssClass="validate[required]" ></asp:TextBox>
                        </td>

                       <td class="td-label"> 
                        <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                         Additional location  </td>
                       <td>
                       <asp:TextBox runat="server" ID="fld_Additionallocation"  MaxLength="100" Width="95%" CssClass="validate[required]" ></asp:TextBox>
                        </td>
                       
                        </tr>
                        <tr>

                         <td class="td-label">
                          <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Position Type 
                        </td>
                        <td>
                         <asp:DropDownList runat="server" ID="fld_PositionType" Width="98%" CssClass="validate[required]" >
                        <asp:ListItem Text="Full time" Value="Full time"></asp:ListItem>
                        <asp:ListItem Text="Part time" Value="Part time"></asp:ListItem>
                        <asp:ListItem Text="Intern" Value="Intern"></asp:ListItem>
                        </asp:DropDownList>
                        </td>

                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Band</td>
                        <td>
                        <asp:DropDownList runat="server" ID="fld_Band" Width="98%" CssClass="validate[required]" >
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        </td>
                        </tr>
                        <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Hiring manager</td>
                        <td>
                        <asp:TextBox runat="server" ID="fld_HRManager" MaxLength="100" Width="95%" CssClass="validate[required]" ></asp:TextBox>
                        </td>
                        <td class="td-label">
                        Global Job Code
                        </td>
                        <td>
                        <asp:TextBox runat="server" ID="fld_GlobaljobCode" MaxLength="50" Width="95%"  ></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Job Level </td>
                        <td>
                         <asp:TextBox runat="server" ID="fld_JobLevel" MaxLength="50" Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Cost Center</td>
                        <td>
                        <asp:DropDownList runat="server" ID="fld_CostCenterInfo" Width="98%" onchange="DropChangeText(this)" CssClass="validate[required]" ></asp:DropDownList>
                        <asp:TextBox runat="server" ID="fld_CostCenterText" style="display:none"></asp:TextBox>

                        </td>
                        </tr>
                        <tr>
                        <td class="td-label">
                        <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Summary work <br />scope of position</td>
                        <td colspan="3">
                        <asp:TextBox runat="server" ID="fld_SummaryworkInfo" TextMode="MultiLine" Width="98%"  Rows="5" CssClass="validate[required]" ></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                        <td  class="td-label">
                        <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Headcount status:  <!--1) Additional 2) Replacement (If Replacement, Reasons for : promotion, Resignation, Termination , Transfer )-->
                        </td>
                        <td colspan="3"> 
                        <input type="radio" name="Headcountstatus" onclick="CheckedValue('HeadcountStatusCode','2')" />Additional &nbsp;&nbsp;&nbsp;   <input type="radio" name="Headcountstatus" onclick="CheckedValue('HeadcountStatusCode','1')" />Replacement
                        <asp:TextBox runat="server" ID="fld_HeadcountStatusCode" style="display:none"></asp:TextBox>
                        <br />
                          <asp:TextBox runat="server" ID="fld_HeadcountComments" TextMode="MultiLine" Width="98%"   Rows="5" CssClass="validate[required]"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                        <td  class="td-label">
                        <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Business Case</td>
                        <td colspan="3">
                        <asp:TextBox runat="server" ID="fld_BusinessCase" TextMode="MultiLine" Width="98%"  Rows="5" CssClass="validate[required]" ></asp:TextBox>
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
    </div>

       <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
       <div style="display:none">
       <asp:HiddenField runat="server" ID="hdIncident" />
       <asp:HiddenField runat="server"  ID="hdUrgeTask" />
       </div>
    </form>
</body>
</html>
