<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.EmployeeTraining.NewRequest" %>

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
        function onselectall_click(obj) {
            if ($(obj).attr("checked")) {
                $("#selectManager").attr("checked", true);
                $("#selectIT").attr("checked", true);
                $("#selectHR").attr("checked", true);
                $("#selectPM").attr("checked", true);
                $("#selectADM").attr("checked", true);
                $("#selectFIN").attr("checked", true);
                $("#selectQA").attr("checked", true);
                $("#selectENG").attr("checked", true);
                $("#selectPUR").attr("checked", true);
                $("#selectHSEF").attr("checked", true);
                $("#selectMarketing").attr("checked", true);
                $("#ManagerList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
                $("#ITList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
                $("#HRList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
                $("#PMList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
                $("#ADMList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
                $("#FINList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
                $("#QAList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
                $("#ENGList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
                $("#MarketingList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
                $("#HSEFList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
                $("#PURList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
            }
            else {
                $("#selectManager").attr("checked", false);
                $("#selectIT").attr("checked", false);
                $("#selectHR").attr("checked", false);
                $("#selectPM").attr("checked", false);
                $("#selectADM").attr("checked", false);
                $("#selectFIN").attr("checked", false);
                $("#selectQA").attr("checked", false);
                $("#selectENG").attr("checked", false);
                $("#selectPUR").attr("checked", false);
                $("#selectMarketing").attr("checked", false);
                $("#selectHSEF").attr("checked", false);
                $("#ManagerList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
                $("#ITList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
                $("#HRList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
                $("#PMList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
                $("#ADMList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
                $("#FINList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
                $("#QAList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
                $("#ENGList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
                $("#PURList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
                $("#HSEFList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
                $("#MarketingList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
            }
        }
        function onselectManager_click(obj){
            if ($(obj).attr("checked")) {
                $("#ManagerList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
            }
            else {
                $("#ManagerList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
            }
        }
        function onselectIT_click(obj) {
            if ($(obj).attr("checked")) {
                $("#ITList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
            }
            else {
                $("#ITList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
            }
        }
        function onselectHR_click(obj) {
            if ($(obj).attr("checked")) {
                $("#HRList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
            }
            else {
                $("#HRList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
            }
        }
        function onselectPM_click(obj) {
            if ($(obj).attr("checked")) {
                $("#PMList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
            }
            else {
                $("#PMList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
            }
        }
        function onselectADM_click(obj) {
            if ($(obj).attr("checked")) {
                $("#ADMList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
            }
            else {
                $("#ADMList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
            }
        }
        function onselectFIN_click(obj) {
            if ($(obj).attr("checked")) {
                $("#FINList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
            }
            else {
                $("#FINList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
            }
        }
        function onselectQA_click(obj) {
            if ($(obj).attr("checked")) {
                $("#QAList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
            }
            else {
                $("#QAList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
            }
        }
        function onselectENG_click(obj) {
            if ($(obj).attr("checked")) {
                $("#ENGList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
            }
            else {
                $("#ENGList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
            }
        }
        function onselectPUR_click(obj) {
            if ($(obj).attr("checked")) {
                $("#PURList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
            }
            else {
                $("#PURList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
            }
        }
        function onselectHSEF_click(obj) {
            if ($(obj).attr("checked")) {
                $("#HSEFList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", true);
                });
            }
            else {
                $("#HSEFList").find("tr").each(function (i, Etr) {
                    $(Etr).find(".user input").attr("checked", false);
                });
            }
        }
        function showTime(obj) {
            var time = new Date(obj.replace(/-/g,"/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        function TrainingType_click(obj, index) {
            if ($(obj).attr("checked") && (index == "1")) {
                //$("#fld_TrainingType").val("on-line");
                $("#fld_TrainingType").val("Self-study");

                $("#addexamred").show();
            }
            if ($(obj).attr("checked") && (index == "2")) {
                //$("#fld_TrainingType").val("off-line");
                $("#fld_TrainingType").val("Face to Face");
                $("#addexamred").hide();
            }
        }
        function beforeSubmit() {
//            $("#fld_StartHour").val($("#DropStartHour").val());
//            $("#fld_EndHour").val($("#DropEndHour").val());
            $("#formId").val($("#UserInfo1_fld_FORMID").val());
            $("#fld_TrainingTeacher").val($("#dropTrainingTeacher option:selected").text());
            if ($("#TrainingType1").attr("checked")) {
                //$("#fld_TrainingType").val("on-line");
                $("#fld_TrainingType").val("Self-study");
            }
            if ($("#TrainingType2").attr("checked")) {
                // $("#fld_TrainingType").val("off-line");
                $("#fld_TrainingType").val("Face to Face");
            }
            var ChexboxCount=0;
            $("#myDiv").find("input[type='checkbox']").each(function () {
                if ($(this).attr("checked"))
                    ChexboxCount++;
            });
            if (ChexboxCount <= 0) {
                alert("Please select Training personnel");
                return false;
            }

            if ($("#fld_TrainingType").val() == "on-line" || $("#fld_TrainingType").val() == "Self-study") {
                $("#Attachments1_txtMust").val("1");
                if ($("#fld_havePapers").val() != "have") {
                    alert("You have never added the examination.");
                    return false;
                }
            }
            else {
                $("#Attachments1_txtMust").val("0");
            }
            var summary = "Employee Training Management";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        $(document).ready(function () {

            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
            $(".container").attr("style", "width:1200px");
            $(".td-label").attr("style", "width:15%");
            $(".td-content").attr("style", "width:35%");

            $("#DropStartHour").val($("#fld_StartHour").val());
            $("#DropEndHour").val($("#fld_EndHour").val());
            $("#fld_StartDate").val(showTime($("#fld_StartDate").val()));
            $("#fld_EndDate").val(showTime($("#fld_EndDate").val()));
            if ($("#fld_TrainingType").val() == "on-line" || $("#fld_TrainingType").val() == "Self-study") {
                $("#TrainingType1").attr("checked", true);
            }
            if ($("#fld_TrainingType").val() == "off-line" || $("#fld_TrainingType").val() == "Face to Face") {
                $("#TrainingType2").attr("checked", true);
            }
            $("#dropTrainingTeacher").val($("#fld_TrainingTeacher").val());

            $("#ManagerList").find("tr").each(function (i, Etr) {
                $(Etr).find("td").each(function (j, Item) {
                    if ($(Item).children().text() == "") {
                        $(Item).hide();
                    }
                });
            });
            $("#ITList").find("tr").each(function (i, Etr) {
                $(Etr).find("td").each(function (j, Item) {
                    if ($(Item).children().text() == "") {
                        $(Item).hide();
                    }
                });
            });
            $("#HRList").find("tr").each(function (i, Etr) {
                $(Etr).find("td").each(function (j, Item) {
                    if ($(Item).children().text() == "") {
                        $(Item).hide();
                    }
                });
            });
            $("#ADMList").find("tr").each(function (i, Etr) {
                $(Etr).find("td").each(function (j, Item) {
                    if ($(Item).children().text() == "") {
                        $(Item).hide();
                    }
                });
            });
            $("#FINList").find("tr").each(function (i, Etr) {
                $(Etr).find("td").each(function (j, Item) {
                    if ($(Item).children().text() == "") {
                        $(Item).hide();
                    }
                });
            });
            $("#QAList").find("tr").each(function (i, Etr) {
                $(Etr).find("td").each(function (j, Item) {
                    if ($(Item).children().text() == "") {
                        $(Item).hide();
                    }
                });
            });
            $("#ENGList").find("tr").each(function (i, Etr) {
                $(Etr).find("td").each(function (j, Item) {
                    if ($(Item).children().text() == "") {
                        $(Item).hide();
                    }
                });
            });
            $("#PURList").find("tr").each(function (i, Etr) {
                $(Etr).find("td").each(function (j, Item) {
                    if ($(Item).children().text() == "") {
                        $(Item).hide();
                    }
                });
            });
            $("#PMList").find("tr").each(function (i, Etr) {
                $(Etr).find("td").each(function (j, Item) {
                    if ($(Item).children().text() == "") {
                        $(Item).hide();
                    }
                });

                $("#HSEFList").find("tr").each(function (i, Etr) {
                    $(Etr).find("td").each(function (j, Item) {
                        if ($(Item).children().text() == "") {
                            $(Item).hide();
                        }
                    });
                });

                $("#MarketingList").find("tr").each(function (i, Etr) {
                    $(Etr).find("td").each(function (j, Item) {
                        if ($(Item).children().text() == "") {
                            $(Item).hide();
                        }
                    });
                });
            });
            $("#Attachments1_lblTitle").hide();
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if (request("type") == "myrequest")
                $("#divPrint").show();
            else
                $("#divPrint").hide();

            CheckeTrainingUser('ManagerList');
            CheckeTrainingUser('ITList');
            CheckeTrainingUser('HRList');
            CheckeTrainingUser('PMList');
            CheckeTrainingUser('ADMList');
            CheckeTrainingUser('FINList');
            CheckeTrainingUser('QAList');
            CheckeTrainingUser('ENGList');
            CheckeTrainingUser('PURList');
            CheckeTrainingUser('HSEFList');
            CheckeTrainingUser('MarketingList');


            ToggleEvaluation();
        });

        function ToggleEvaluation() {
            var EVDay = $("#fld_EvaluationDays").val();
            if (EVDay != "") {
            $("#radioYTraining").attr("checked", true);
//                if (EVDay == "3") {
//                    $("#cbtday").attr("checked", true);
//                }
//                if (EVDay == "6") {
//                    $("#cbsday").attr("checked", true);
//                }
            }
            else {
                $("#fld_EvaluationDays").val("");
                $("#radioNTraining").attr("checked", true);
            }
        }
         function CheckedEvaluation(obj)
        {
            if (obj == "1") {
//                $("#trEvaluation").show();
//                $("#cbtday").attr("checked", true);
                $("#fld_EvaluationDays").val("1");
            }
            else {
//                $("#trEvaluation").hide();
//                $("#cbtday").attr("checked", false);
//                $("#cbsday").attr("checked", false);
                $("#fld_EvaluationDays").val("");
            }
        }
        

        function CheckeTrainingUser(obj) {
            var TrainingUserEn = $("#fld_TrainingUser").val();
            if (TrainingUserEn != "") {
                $("#" + obj).find("label").each(function () {

                    for (var i = 0; i < TrainingUserEn.split(',').length; i++) {
                        if ($(this).text() == TrainingUserEn.split(',')[i])
                            $(this).prev().attr("checked", true);
                    }
                });
            }
            }

        function openPractise(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:800px;"
            var formId = $("#UserInfo1_fld_FORMID").val();
            var ReturnValue = window.showModalDialog("./Practise.aspx?FormID=" + formId, null, digStr);
          
            if (ReturnValue != null) {
                var havePapers1 = eval("(" + ReturnValue + ")");
               // var havePapers1 = eval(havePapers);
                $("#fld_havePapers").val(havePapers1[0].havePapers);
            }
            if ($("#fld_havePapers").val() == "have"&&ReturnValue!=undefined) {
                $("#PractiseInfo").prev().val("Update");
                alert("You have added the examination successfully.");
                $("#PractiseInfo").text("You have added the examination successfully");
            }
        }
        function beforeSave() {
            var summary = "Employee Training Management";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        function OpenreprorsPage(obj) {
            if (obj == "1") {
                var DocumentNo = $("#UserInfo1_fld_DOCUMENTNO").text();
                location.href = "TrainingReports.aspx?DocumentNo=" + DocumentNo;
            }
            if (obj == "2") {
                var FormID =$("#UserInfo1_fld_FORMID").val();
                location.href = "AnswerDetail.aspx?FormID="+FormID;
            }
            if (obj == "3") {
                var FormID = $("#UserInfo1_fld_FORMID").val();
                location.href = "EvaluationDetailPage.aspx?FormID=" + FormID;
            }
        }


        function openForm(taskId, type) {
            var sheight = screen.height - 150;
            var swidth = screen.width - 10;
            var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
            s = window.open('../Ultimus.UWF.workflow/OpenForm.aspx?TaskId=' + taskId + '&Type=' + type + '', '', winoption);
            s.focus();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <asp:TextBox runat="server" ID="formId" style="display:none;"></asp:TextBox>
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Employee Training Management" processprefix="HRET" tablename="PROC_EmployeeTraining" tablenamedetail="PROC_EmployeeTraining_DT"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Training Information（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Topic</p>
                        </td>
                        <td class="td-content" colspan="7" >
                        <asp:TextBox runat="server" ID="fld_TrainingPurpose" Width="98%"  CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Trainer</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:DropDownList runat="server" ID="dropTrainingTeacher"  CssClass="validate[required]"></asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_TrainingTeacher" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                       <span style=" background:red; margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Training type</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:RadioButton runat="server" ID="TrainingType1" GroupName="TrainingType" Checked="true" Text="Self-study" onclick="TrainingType_click(this,1)"/>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton runat="server" ID="TrainingType2" GroupName="TrainingType" Text="Face to Face" onclick="TrainingType_click(this,2)"/>
                            <asp:TextBox runat="server" ID="fld_TrainingType" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Date</p>
                        </td>
                        <td class="td-content" colspan="7" >
                        from&nbsp;&nbsp;<span><asp:TextBox runat="server" ID="fld_StartDate" Width="30%"  CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'fld_EndDate\')}'})"></asp:TextBox></span>&nbsp;&nbsp;
                        <asp:DropDownList runat="server" Width="10%" ID="DropStartHour">
                                <asp:ListItem Selected="True">00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_StartHour" style="display:none;"></asp:TextBox>
                        to&nbsp;&nbsp;<span><asp:TextBox runat="server" ID="fld_EndDate" Width="30%"  CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',minDate:'#F{$dp.$D(\'fld_StartDate\')}'})"></asp:TextBox></span>&nbsp;&nbsp;
                        <asp:DropDownList runat="server" Width="10%" ID="DropEndHour">
                                <asp:ListItem Selected="True">00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_EndHour" style="display:none;"></asp:TextBox>
                        </td>
                        <%--<td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">End Time</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:TextBox runat="server" ID="fld_EndDate" Width="96%"  CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',minDate:'#F{$dp.$D(\'fld_StartDate\')}'})"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Training Duration</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:TextBox runat="server" ID="fld_TrainingDuration"  CssClass="validate[required]" Width="96%" money="money"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Location</p>
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:TextBox runat="server" ID="fld_TrainingLocation"  CssClass="validate[required]" Width="96%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="vertical-align:middle" >
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Training personnel</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <table style="width:100%;">
                                <tr>
                                <td style="border-left:none;"><asp:CheckBox runat="server" ID="selectall" onclick="onselectall_click(this)" />Select All</td>
                                <td style="border-right:none;border-top:none;border-bottom:none;"></td>
                            </tr>
                            <tr>
                                <td style="border-left:none;padding-bottom:0;padding-top:0;">
                                    <asp:CheckBox runat="server" ID="selectManager" onclick="onselectManager_click(this)" />
                                    <label for="selectManager">Management</label></td>
                                <td style="border-bottom: medium none; border-top: medium none; border-right: medium none;">
                                    <table id="ManagerList" style="border:0;">
                                        <asp:Repeater runat="server" ID="RepeaterManager">
                                            <ItemTemplate> 
                                                <tr style="border:0;">
                                                    <td style="border:0;width:20%;">
                                                        <asp:CheckBox ID="User1" class="user" Text='<%#Eval("USER1") %>' runat="server" />
                                                    </td>
                                                    <td style="border:0;width:20%;">
                                                        <asp:CheckBox ID="User2" class="user" Text='<%#Eval("USER2") %>' runat="server" />
                                                    </td>
                                                    <td style="border:0;width:20%;">
                                                        <asp:CheckBox ID="User3" class="user" Text='<%#Eval("USER3") %>' runat="server" />
                                                    </td>
                                                    <td style="border:0;width:20%;">
                                                        <asp:CheckBox ID="User4" class="user" Text='<%#Eval("USER4") %>' runat="server" />
                                                    </td>
                                                    <td style="border:0;width:20%;">
                                                        <asp:CheckBox ID="User5" class="user" Text='<%#Eval("USER5") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <!--- IT-->
                            <tr>
                                <td style="border-left:none;padding-bottom:0;padding-top:0;"><asp:CheckBox runat="server" ID="selectIT" onclick="onselectIT_click(this)" />IT</td>
                                <td  style="border-bottom: medium none; border-top: medium none; border-right: medium none;">
                                    <table id="ITList"   style="border:0;" >
                                        <asp:Repeater runat="server" ID="RepeaterIT">
                                            <ItemTemplate> 
                                                <tr style="border:0;">
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User1" class="user" Text='<%#Eval("USER1") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User2" class="user" Text='<%#Eval("USER2") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User3" class="user" Text='<%#Eval("USER3") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User4" class="user" Text='<%#Eval("USER4") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User5" class="user" Text='<%#Eval("USER5") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                             <!--HR-->
                            <tr>
                                <td style="border-left:none;padding-bottom:0;padding-top:0;"><asp:CheckBox runat="server" ID="selectHR" onclick="onselectHR_click(this)" />HR</td>
                                <td style="border-bottom: medium none; border-top: medium none; border-right: medium none;">
                                    <table id="HRList"   style="border:0;"  >
                                        <asp:Repeater runat="server" ID="RepeaterHR">
                                            <ItemTemplate> 
                                                <tr  style="border:0;">
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User1" class="user" Text='<%#Eval("USER1") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User2" class="user" Text='<%#Eval("USER2") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User3" class="user" Text='<%#Eval("USER3") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User4" class="user" Text='<%#Eval("USER4") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User5" class="user" Text='<%#Eval("USER5") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                             <!--PMList-->
                            <tr>
                                <td style="border-left:none;padding-bottom:0;padding-top:0;"><asp:CheckBox runat="server" ID="selectPM" onclick="onselectPM_click(this)" />PM</td>
                                <td style="border-bottom: medium none; border-top: medium none; border-right: medium none;">
                                    <table id="PMList" style="border:0;" >
                                        <asp:Repeater runat="server" ID="RepeaterPM">
                                            <ItemTemplate> 
                                                <tr  style="border:0;">
                                                    <td style="border:0;width:20%">
                                                        <asp:CheckBox ID="User1" class="user" Text='<%#Eval("USER1") %>' runat="server" />
                                                    </td>
                                                    <td style="border:0;width:20%">
                                                        <asp:CheckBox ID="User2" class="user" Text='<%#Eval("USER2") %>' runat="server" />
                                                    </td>
                                                    <td style="border:0;width:20%">
                                                        <asp:CheckBox ID="User3" class="user" Text='<%#Eval("USER3") %>' runat="server" />
                                                    </td>
                                                    <td style="border:0;width:20%">
                                                        <asp:CheckBox ID="User4" class="user" Text='<%#Eval("USER4") %>' runat="server" />
                                                    </td>
                                                    <td style="border:0;width:20%">
                                                        <asp:CheckBox ID="User5" class="user" Text='<%#Eval("USER5") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                              <!--ADMList-->
                            <tr>
                                <td style="border-left:none;padding-bottom:0;padding-top:0;"><asp:CheckBox runat="server" ID="selectADM" onclick="onselectADM_click(this)" />Admin</td>
                                <td style="border-bottom: medium none; border-top: medium none; border-right: medium none;">
                                    <table id="ADMList" style="border:0; width:100%" >
                                        <asp:Repeater runat="server" ID="RepeaterADM">
                                            <ItemTemplate> 
                                                <tr style="border:0; width:100%">
                                                    <td style="border:0; width:25%">
                                                        <asp:CheckBox ID="User1" class="user" Text='<%#Eval("USER1") %>' runat="server" />
                                                    </td>
                                                    <td style="border:0; width:25%">
                                                        <asp:CheckBox ID="User2" class="user" Text='<%#Eval("USER2") %>' runat="server" />
                                                    </td>
                                                    <td style="border:0; width:25%">
                                                        <asp:CheckBox ID="User3" class="user" Text='<%#Eval("USER3") %>' runat="server" />
                                                    </td>
                                                    <td style="border:0; width:25%">
                                                        <asp:CheckBox ID="User4" class="user" Text='<%#Eval("USER4") %>' runat="server" />
                                                    </td>
                                                    <td style="border:0; width:25%">
                                                        <asp:CheckBox ID="User5" class="user" Text='<%#Eval("USER5") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                             <!--FINList-->
                            <tr>
                                <td style="border-left:none;padding-bottom:0;padding-top:0;"><asp:CheckBox runat="server" ID="selectFIN" onclick="onselectFIN_click(this)" />Finance</td>
                                <td style="border-bottom: medium none; border-top: medium none; border-right: medium none;">
                                    <table id="FINList" style="border:0;" >
                                        <asp:Repeater runat="server" ID="RepeaterFIN">
                                            <ItemTemplate> 
                                                <tr>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User1" class="user" Text='<%#Eval("USER1") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User2" class="user" Text='<%#Eval("USER2") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User3" class="user" Text='<%#Eval("USER3") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User4" class="user" Text='<%#Eval("USER4") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User5" class="user" Text='<%#Eval("USER5") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                             <!--QAList-->
                            <tr>
                                <td style="border-left:none;padding-bottom:0;padding-top:0;"><asp:CheckBox runat="server" ID="selectQA" onclick="onselectQA_click(this)" />Quality</td>
                                <td style="border-bottom: medium none; border-top: medium none; border-right: medium none;">
                                    <table id="QAList" style="border:0;" >
                                        <asp:Repeater runat="server" ID="RepeaterQA" >
                                            <ItemTemplate> 
                                                <tr>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User1" class="user" Text='<%#Eval("USER1") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User2" class="user" Text='<%#Eval("USER2") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User3" class="user" Text='<%#Eval("USER3") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User4" class="user" Text='<%#Eval("USER4") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User5" class="user" Text='<%#Eval("USER5") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                             <!--ENGList-->
                            <tr>
                                <td style="border-left:none;padding-bottom:0;padding-top:0;"><asp:CheckBox runat="server" ID="selectENG" onclick="onselectENG_click(this)" />Engineering</td>
                                <td style="border-bottom: medium none; border-top: medium none; border-right: medium none;">
                                    <table id="ENGList" style="border:0;" >
                                        <asp:Repeater runat="server" ID="RepeaterENG">
                                            <ItemTemplate> 
                                                <tr>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User1" class="user" Text='<%#Eval("USER1") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User2" class="user" Text='<%#Eval("USER2") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User3" class="user" Text='<%#Eval("USER3") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User4" class="user" Text='<%#Eval("USER4") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User5" class="user" Text='<%#Eval("USER5") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <!--PURList-->
                            <tr>
                                <td style="border-left:none;padding-bottom:0;padding-top:0;"><asp:CheckBox runat="server" ID="selectPUR" onclick="onselectPUR_click(this)" />PUR</td>
                                <td style="border-bottom: medium none; border-top: medium none; border-right: medium none;">
                                    <table id="PURList" style="border:0;" >
                                        <asp:Repeater runat="server" ID="RepeaterPUR">
                                            <ItemTemplate> 
                                                <tr>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User1" class="user" Text='<%#Eval("USER1") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User2" class="user" Text='<%#Eval("USER2") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User3" class="user" Text='<%#Eval("USER3") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User4" class="user" Text='<%#Eval("USER4") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User5" class="user" Text='<%#Eval("USER5") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <!--HSEF-->
                             <tr>
                                <td style="border-left:none;padding-bottom:0;padding-top:0;"><asp:CheckBox runat="server" ID="selectHSEF" onclick="onselectHSEF_click(this)" />HSEF</td>
                                <td style="border-bottom: medium none; border-top: medium none; border-right: medium none;">
                                    <table id="HSEFList" style="border:0;" >
                                        <asp:Repeater runat="server" ID="RepeaterHSEF">
                                            <ItemTemplate> 
                                                <tr>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User1" class="user" Text='<%#Eval("USER1") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User2" class="user" Text='<%#Eval("USER2") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User3" class="user" Text='<%#Eval("USER3") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User4" class="user" Text='<%#Eval("USER4") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User5" class="user" Text='<%#Eval("USER5") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                              <!--Markting-->
                              <tr>
                                <td style="border-left:none;padding-bottom:0;padding-top:0;"><asp:CheckBox runat="server" ID="selectMarketing" onclick="onselectHSEF_click(this)" />Marketing</td>
                                <td style="border-bottom: medium none; border-top: medium none; border-right: medium none;">
                                    <table id="MarketingList" style="border:0;" >
                                        <asp:Repeater runat="server" ID="Marketing">
                                            <ItemTemplate> 
                                                <tr>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User1" class="user" Text='<%#Eval("USER1") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User2" class="user" Text='<%#Eval("USER2") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User3" class="user" Text='<%#Eval("USER3") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User4" class="user" Text='<%#Eval("USER4") %>' runat="server" />
                                                    </td>
                                                    <td style="border-left:none;width:20%">
                                                        <asp:CheckBox ID="User5" class="user" Text='<%#Eval("USER5") %>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>



                            </table>
                            <asp:Label runat="server" ID="domain" style="display:none;" ></asp:Label>
                            <asp:TextBox runat="server" ID="fld_ApprovalArr_TrainingPersonnel" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
              
                <table id="addPapers" class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span id="addexamred" style="background-color:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Add examination</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <%--<asp:Button ID="btnAdd"  runat="server" Text="Add" CssClass="btn btn-primary"  style="width: 100px;height: 25px; color:Black;background-color:White; font-weight: 400; font-style: normal; font-size: 13px;text-decoration: none; text-align: center;float:left;padding: 1px 0px 1px 0px; margin-bottom:1%;" OnClick="btnPapers_Click"  />--%>
                            <input type="button" value="add" class="btn" onclick="openPractise(this)" />
                           <span id="PractiseInfo"></span>
                                                       <asp:TextBox ID="fld_havePapers" runat="server" style="display:none;" ></asp:TextBox>
                          <%--<asp:Button ID="btn" runat="server" Text="ADD" onclick="btn_Click" />--%>
                        </td>
                    </tr>
                </table>


                  <table id="Table1" class="table table-condensed table-bordered">
         <tr >
         <td class="td-label">
          Is need Attender Evaluation
            </td>
            <td>
            <input type="radio"  name="IsTraining" id="radioYTraining" onclick="CheckedEvaluation('1')" />Yes
            <input type="radio"  name="IsTraining"  id="radioNTraining" onclick="CheckedEvaluation('0')" />No
            </td>
         </tr>
                  <%--  <tr id="trEvaluation" >
                        <td class="td-label">
                         Training Evaluation Duration
                        </td>
                        <td>
                        <asp:RadioButton runat="server"   GroupName="Traininggroup" ID="cbtday" />3 Month
                        <asp:RadioButton runat="server" GroupName="Traininggroup" ID="cbsday" />6 Month
                        </td>
                    </tr>--%>
            </table>
              
            </div>
            <div class="row">
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
        <div id="divPrint" style="text-align:center"  class="row" >
        <input type="button" value="Training Attendee Sheet" id="btnprint" name="ButtonList1$btnClose" onclick="OpenreprorsPage('1')" class="btn" />
        <input type="button" value="Exam" id="btnprint" name="ButtonList1$btnClose" onclick="OpenreprorsPage('2')" class="btn" />
        <input type="button" value="Evaluation" id="btnprint" name="ButtonList1$btnClose" onclick="OpenreprorsPage('3')" class="btn" />
        </div>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
    <%--    <input type="button" value="testProcess"  onclick="openForm('S0710171ba6f99baca654e7c45838fd','NEWREQUEST')" />--%>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        <asp:HiddenField runat="server" ID="hdUserID" />
        <asp:TextBox runat="server" ID="fld_TrainingUser"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdUrgeTask" />
        <asp:TextBox runat="server" ID="fld_EvaluationDays"></asp:TextBox>
        </div>
    </form>
</body>
</html>



