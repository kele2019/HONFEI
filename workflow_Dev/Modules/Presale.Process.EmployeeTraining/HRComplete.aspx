<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRComplete.aspx.cs" Inherits="Presale.Process.EmployeeTraining.HRComplete" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Employee Training Management</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#fld_EmployeeName").val($("#EmployeeName").val());
            $("#fld_OBDepartment").val($("#OBDepartment").val());
            var stepname = request("StepName");

            if (request("type") == "myapproval") {
                $("#btnDiv").hide();
                $("#evaluationTable").hide();
                $("#btnComplete1").hide();
            }
            var EVDay = $("#read_EvaluationDays").val();
            if (EVDay != "") {
                $("#EVDay").text("Yes");
            }
            else {
                $("#EVDay").text("No");
            }
            // $("#btnComplete").attr("disabled", false);
        });
        function submitPageReview(obj) {
                $("#ButtonList1_btnSubmit").click();
        }
         
        function openPractise(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:800px;"
            var formId = $("#UserInfo1_fld_FORMID").val();
            var ReturnValue = window.showModalDialog("./Answer.aspx?FormID=" + formId, null, digStr);
            if (ReturnValue != null) {
                var throughAnswer1 = eval("(" + ReturnValue + ")");
                $("#throughAnswer").val(throughAnswer1[0].throughAnswer);
            }

            if ($("#throughAnswer").val() != "success") {

            } else {
                $("#PaperTest").text("Exam was Passed");
                $("#btnComplete").attr("disabled", false);
            }
        }
        function openEvaluation() {
            var digStr = "dialogHeight:500px;dialogWidth:800px;"
            var formId = $("#UserInfo1_fld_FORMID").val();
            var ReturnValue = window.showModalDialog("./TrainingPracticeAppraisal.aspx?FormID=" + formId, null, digStr);
            if (ReturnValue != null) {
                var CompleteEvalueation1 = eval("(" + ReturnValue + ")");
                $("#CompleteEvalueation").val(CompleteEvalueation1[0].CompleteEvalueation);
                $("#sapnEvalutaion").text("Evaluation Was Completed");
                $("#btnComplete").attr("disabled", false);
            }
        }
        function beforeSubmit() {
            return true;
        }
        

        function OpenreprorsPage(obj) {
            if (obj == "1") {
                var DocumentNo = $("#UserInfo1_fld_DOCUMENTNO").text();
                location.href = "TrainingReports.aspx?DocumentNo=" + DocumentNo;
            }
            if (obj == "2") {
                var FormID = $("#UserInfo1_fld_FORMID").val();
                location.href = "AnswerDetail.aspx?FormID=" + FormID;
            }
            if (obj == "3") {
                var FormID = $("#UserInfo1_fld_FORMID").val();
                location.href = "EvaluationDetailPage.aspx?FormID=" + FormID;
            }
        }
    </script>
     <style type="text/css">
        #menu {
font-size: 12px;
font-weight: bolder;
 
}
#menu li{
list-style-image: none;
list-style-type: none;
padding-right:20px;
margin-left:-20px;
height:30px;
float: left;
}

    
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Employee Training Management" processprefix="HRET" tablename="PROC_EmployeeTraining"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
               <%-- <p style="font-weight:bold;">Employee Information </p>--%>
                <table class="table table-condensed table-bordered" style="display:none">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Employee Name</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_EmployeeName" ReadOnly="true" style="border:0;background-color:White;"></asp:TextBox>
                            <asp:TextBox runat="server" ID="EmployeeName" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">On-boarding department</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:TextBox runat="server" ID="fld_OBDepartment" ReadOnly="true" style="border:0;background-color:White;" ></asp:TextBox>
                        <asp:TextBox runat="server" ID="OBDepartment" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;">Training Information（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Topic</p>
                        </td>
                        <td class="td-content" colspan="7" >
                        <asp:Label runat="server" ID="read_TrainingPurpose" ></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Trainer</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_TrainingTeacher"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Training type</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_TrainingType"></asp:Label>
                            <asp:Label runat="server" ID="read_havePapers" style="display:none;" ></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Date</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        From&nbsp;&nbsp;<asp:Label runat="server" ID="read_StartDate"></asp:Label>&nbsp;&nbsp;
                     To&nbsp;&nbsp;
                      <asp:Label runat="server" ID="read_EndDate"   ></asp:Label>
                        &nbsp;&nbsp;<asp:Label runat="server" ID="read_EndHour"></asp:Label> &nbsp;Hour
                        </td>
                    </tr>
                    <tr>
                        <%--<td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">End Date</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_EndDate"></asp:Label>
                        </td>--%>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Training Duration</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:Label runat="server" ID="read_TrainingDuration"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                       <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Location</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:Label runat="server" ID="read_TrainingLocation"></asp:Label>
                        </td>
                    </tr>


                      <tr>
                    <td class="td-label">
                    Training personnel
                    </td>
                      <td class="td-content" colspan="7">
                        <ul id="menu">
                <asp:Repeater runat="server" ID="RPList" onprerender="RPList_PreRender">
                <ItemTemplate>
                <li>
                   <span> <%#Eval("EXT04") %>;</span>
                </li>
                </ItemTemplate>
                </asp:Repeater>
                </ul>
                      </td>
                    </tr>


                      <tr>
                    <td class="td-label">
                    Is need attendee evaluation<br /> (your manager will evaluate your training effect after one month)
                    </td>
                      <td class="td-content" colspan="7">
                      <span id="EVDay"></span>
                      </td>
                      </tr>

                </table>
                
            </div>
            <div class="row">
                <attach:attachments id="Attachments1" ReadOnly="true" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div
        </div>
        <asp:TextBox runat="server" ID="fld_Signed" style="display:none;"></asp:TextBox>
        <asp:Label runat="server" ID="read_havePractise" style="display:none;"></asp:Label>
        <asp:TextBox runat="server" ID="CompleteEvalueation" style="display:none;"></asp:TextBox>
        <asp:TextBox runat="server" ID="throughAnswer" style="display:none;"></asp:TextBox>
        

        <div id="divPrint" style="text-align:center"  class="row" >

        
         <input type="button" id="btnComplete1"  class="btn" value="Complete" onclick="submitPageReview('0')" />
        <input type="button" value="Training Attendee Sheet" id="btnprint" name="ButtonList1$btnClose" onclick="OpenreprorsPage('1')" class="btn" />
        <input type="button" value="Exam" id="btnprint" name="ButtonList1$btnClose" onclick="OpenreprorsPage('2')" class="btn" />
        <input type="button" value="Evaluation" id="btnprint" name="ButtonList1$btnClose" onclick="OpenreprorsPage('3')" class="btn" />
        </div>

        <div style="display:none;">
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        </div>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        <asp:HiddenField runat="server" ID="hdFlag" />
        <asp:Label runat="server" ID="read_EvaluationDays"></asp:Label>
        </div>
    </form>
</body>
</html>
