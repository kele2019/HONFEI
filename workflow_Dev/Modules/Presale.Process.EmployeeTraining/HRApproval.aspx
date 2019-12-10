<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRApproval.aspx.cs" Inherits="Presale.Process.EmployeeTraining.HRApproval" %>
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
        function showTime(obj) {
            var time = new Date(obj.replace(/-/g, "/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        $(document).ready(function () {
            $("#fld_EmployeeName").val($("#EmployeeName").val());
            $("#fld_OBDepartment").val($("#OBDepartment").val());

            $("#papersTable").hide();
            $("#evaluationTable").hide();
            if (request("type") == "myapproval") {
                $("#btnComplete").hide();
            }
            var EVDay = $("#read_EvaluationDays").val();
            if (EVDay != "") {
                $("#EVDay").text("Yes");
            }
            else {
                $("#EVDay").text("No");
            }
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
            //$("#ButtonList1_btnSubmit").val('Are you sure close this Training?');
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
              <%--  <p style="font-weight:bold;">Employee Information </p>--%>
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
                      <asp:Label runat="server" ID="read_EndDate" ReadOnly="true" style="border:0;background-color:White;"  ></asp:Label>
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
                    Is need Attender Evaluation
                    </td>
                      <td class="td-content" colspan="7">
                      <span id="EVDay"></span>
                      </td>
                      </tr>


                </table>
                
                <table id="evaluationTable" class="table table-condensed table-bordered"  style="display:none;">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Evaluation table</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <input type="button" value="Evaluation" class="btn" onclick="openEvaluation(this)" />
                             <span id="sapnEvalutaion"></span>
                            <%--<asp:Button ID="btnEvaluation"  runat="server" Text="test" CssClass="btn btn-primary"  style="width: 100px;height: 25px; color:Black;background-color:White; font-weight: 400; font-style: normal; font-size: 13px;text-decoration: none; text-align: center;float:left;padding: 1px 0px 1px 0px; margin-bottom:1%;" onclick="return openEvaluation()"  />--%>
                        </td>
                    </tr>
                </table>
                <table id="papersTable" class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">papers</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <input type="button" value="test" class="btn" onclick="openPractise(this)" />
                            <span id="PaperTest"></span>
                              <%--<asp:Button ID="btntest"  runat="server" Text="test" CssClass="btn btn-primary"  style="width: 100px;height: 25px; color:Black;background-color:White; font-weight: 400; font-style: normal; font-size: 13px;text-decoration: none; text-align: center;float:left;padding: 1px 0px 1px 0px; margin-bottom:1%;" onclick="openPractise(this)"  />--%>
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
        <%--<div id="btnDiv"  >
            <table style="width: 100%;" >
                <tr>
                    <td align="center"  >
                        <table>
                            <tr>
                                <td> 
                                <input type="button" id="btnComplete" class="btn" value="Approve" onclick="submitPageReview('0')" />
                                </td>
                            </tr>
                       </table>
                    </td>
                 </tr>
            </table>
        </div>--%>

        <div id="divPrint" style="text-align:center"  class="row" >
       <%--  <input type="button" id="btnComplete1"  class="btn" value="Complete" onclick="submitPageReview('0')" />
        <input type="button" value="Training Attendee Sheet" id="btnprint" name="ButtonList1$btnClose" onclick="OpenreprorsPage('1')" class="btn" />
        <input type="button" value="Exam" id="btnprint" name="ButtonList1$btnClose" onclick="OpenreprorsPage('2')" class="btn" />
        <input type="button" value="Evaluation" id="btnprint" name="ButtonList1$btnClose" onclick="OpenreprorsPage('3')" class="btn" />--%>
        </div>

        <div style="display:block;">
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        </div>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
         <asp:TextBox runat="server" ID="read_EvaluationDays"></asp:TextBox>
        </div>
    </form>
</body>
</html>
