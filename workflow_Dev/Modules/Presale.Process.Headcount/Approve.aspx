<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approve.aspx.cs" Inherits="Presale.Process.Headcount.Approve" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Headcount Request Application</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
//            if ($("#hdIncident").val() != "") {
//                $("#ButtonList1_btnSubmit").val("Submit");
//                $("#ButtonList1_btnBack").hide();
//                $("#ButtonList1_btnReject").show();
//            }
//            if ($("#hdUrgeTask").val() == "Yes") {
//                $("#ReturnBackTask").show();
//            }
            var EvalueatinValue = $("#read_HeadcountStatusCode").text();
            if (EvalueatinValue != "")
                $("input[name^='Headcountstatus']").eq((EvalueatinValue - 2) * (-1)).attr("checked", true);
        });
//        function DropChangeText(obj) {
//            var DropText = $(obj).find("option:selected").text();
//            $(obj).next().val(DropText);
//        }
//        function CheckedValue(flag, vindex) {
//            $('#fld_' + flag).val(vindex);
//            if (vindex == "1")
//                $("#fld_HeadcountComments").attr("class", "validate[required]");
//            else
//                $("#fld_HeadcountComments").removeClass("validate[required]");
//        }
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
                        Position Title
                        </td>
                        <td>
                        <asp:Label runat="server" ID="read_PositionTitle" ></asp:Label>
                        </td>
                        <td class="td-label">Budget</td>
                        <td>

                          <asp:Label runat="server" ID="read_BudgetInfo" ></asp:Label>
                        </td>
                        </tr>
                        <tr>
                        <td class="td-label">Primary location </td>
                        <td>
                        <asp:Label runat="server" ID="read_Primarylocation" ></asp:Label>
                        </td>

                       <td class="td-label">  Additional location  </td>
                       <td>
                       <asp:Label runat="server" ID="read_Additionallocation"></asp:Label>
                        </td>
                       
                        </tr>
                        <tr>
                        <td class="td-label">
                        Position Type 
                        </td>
                        <td>
                         <asp:Label runat="server" ID="read_PositionType"></asp:Label>

                     <%--    <asp:DropDownList runat="server" ID="fld_PositionType" Width="98%">
                        <asp:ListItem Text="Full time" Value="Full time"></asp:ListItem>
                        <asp:ListItem Text="Part time" Value="Part time"></asp:ListItem>
                        <asp:ListItem Text="Intern" Value="Intern"></asp:ListItem>
                        </asp:DropDownList>--%>
                        </td>

                        <td class="td-label">Band</td>
                        <td>
                        <asp:Label runat="server" ID="read_Band"></asp:Label>
                     <%--   <asp:DropDownList runat="server" ID="fld_Band" Width="98%">
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>--%>
                        </td>
                        </tr>
                        <tr>
                        <td class="td-label">Hiring manager</td>
                        <td>
                        <asp:Label runat="server" ID="read_HRManager"></asp:Label>
                        </td>
                        <td class="td-label">
                        Global Job Code 
                        </td>
                        <td>
                        <asp:Label runat="server" ID="read_GlobaljobCode" ></asp:Label>
                        </td>
                        </tr>
                        <tr>
                        <td class="td-label">Job Level </td>
                        <td>
                         <asp:Label runat="server" ID="read_JobLevel"  ></asp:Label>
                        </td>
                        <td class="td-label">Cost Center</td>
                        <td>
                        <%--<asp:DropDownList runat="server" ID="fld_CostCenterInfo" Width="98%" onchange="DropChangeText(this)"></asp:DropDownList>--%>
                        <asp:Label runat="server" ID="read_CostCenterText"  ></asp:Label>
                        </td>
                        </tr>
                        <tr>
                        <td class="td-label">Summary work <br />scope of position</td>
                        <td colspan="3">
                        <asp:Label runat="server" ID="read_SummaryworkInfo"></asp:Label>
                        </td>
                        </tr>
                        <tr>
                        <td  class="td-label">
                        Headcount status:  <!--1) Additional 2) Replacement (If Replacement, Reasons for : promotion, Resignation, Termination , Transfer )-->
                        </td>
                        <td colspan="3"> 
                        <input type="radio" name="Headcountstatus"    disabled="disabled" />Additional &nbsp;&nbsp;&nbsp;   <input type="radio" name="Headcountstatus" disabled="disabled"/>Replacement
                        <asp:Label runat="server" ID="read_HeadcountStatusCode" style="display:none"></asp:Label>
                        <br />
                          <asp:Label runat="server" ID="read_HeadcountComments"  ></asp:Label>
                        </td>
                        </tr>
                        <tr>
                        <td  class="td-label">Business Case</td>
                        <td colspan="3">
                        <asp:Label runat="server" ID="read_BusinessCase"  ></asp:Label>
                        </td>
                        </tr>
                        </table>
              </div>

             <div class="row" style="display:block;">
                <attach:attachments id="Attachments1" Readonly="true" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
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
