<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequestGoalPlan.aspx.cs" Inherits="Presale.Process.EmployeePerformanceReport.GoalPlan" ValidateRequest="false" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Employee Performance Report</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function getChangeCheck(obj) {
            $(obj).next().val($(obj).find("option:selected").text());
            var PYear = $("#fld_Year").val() - 0;
            $("#tablediv").find("table").each(function (i, Etr) {
                if (PYear > 2017) {
                    $(Etr).find(".Initiativesclass").find("label").eq(0).html("Culture")
                    $(Etr).find(".Initiativesclass").find("label").eq(1).html("Expand Margins")
                    $(Etr).find(".Initiativesclass").find("label").eq(2).html("Organic Growth")
                    $(Etr).find(".Initiativesclass").find("label").eq(3).html("Smart Investment")
                    $(Etr).find(".Initiativesclass").find("label").eq(4).html("Software Transformation")
                }
                else {
                    $(Etr).find(".Initiativesclass").find("label").eq(0).html("Growth")
                    $(Etr).find(".Initiativesclass").find("label").eq(1).html("Productivity")
                    $(Etr).find(".Initiativesclass").find("label").eq(2).html("Cash")
                    $(Etr).find(".Initiativesclass").find("label").eq(3).html("People")
                    $(Etr).find(".Initiativesclass").find("label").eq(4).html("Enablers")
                }
            });


        }
//        function getStatusValue(year, applierLogin, reportType) {
//            var StatusValue;
//            $.ajax({ url: 'getStatus.ashx',
//                data: { "year": year, "applierLogin": applierLogin, "reportType": reportType },
//                type: 'POST',
//                success: function (value) {
//                    $("#status").val(value);
//                }
//            });
//        }
//        function getReportType(obj, index) {
//            if (index == "1" && $(obj).attr("checked")) {
//                $("#fld_ReportType").val("Begin-Year goal plan");
//                $("#beginOrMidYear").show();
//                $("#ReportType1").show();
//                $("#ReportType2").hide();
//            }
//            if (index == "2" && $(obj).attr("checked")) {
//                $("#fld_ReportType").val("Mid-Year Update");
//                $("#beginOrMidYear").show();
//                $("#ReportType1").hide();
//                $("#ReportType2").show();
//            }
//            if (index == "3" && $(obj).attr("checked")) {
//                $("#fld_ReportType").val("End-Year Performance");
//                $("#beginOrMidYear").hide();
//                $("#ReportType1").hide();
//                $("#ReportType2").hide();
//            }
//            getStatusValue($("#fld_Year").val(), $("#fld_ApplicantUserLogin").val(), $("#fld_ReportType").val());
        //        }
        function StatusBeginYearChange() {
            $("#tablediv").find("table").each(function (i, Etr) {
                alert($(Etr).find("tr eq(4)").find("td .reaultLabel").html());
                $(Etr).find("tr eq(4)").find(".reaultLabel").hide();
                $(Etr).find("tr eq(4)").find(".results").attr("CssClass", "");
            }); 
        }
        function StatusMidYearChange() {
            $("#tablediv").find("table").each(function (i, Etr) {
                alert($(Etr).find("tr eq(4)").find("td .reaultLabel").html());
                $(Etr).find("tr eq(4)").find(".reaultLabel").show();
                $(Etr).find("tr eq(4)").find(".results").attr("CssClass", "validate[required]");
            });
        }
        function StatusEndYearChange() {
            $("#tablediv").find("table").each(function (i, Etr) {
                alert($(Etr).find("tr eq(4)").find("td .reaultLabel").html());
                $(Etr).find("tr eq(4)").find(".reaultLabel").show();
                $(Etr).find("tr eq(4)").find(".results").attr("CssClass", "validate[required]");
            });
        }
        $(document).ready(function () {
            $("#tablediv").find("table").each(function (i, Etr) {
                var CompletionStatus = $(Etr).find("tr:eq(0)").find("td:eq(3)").find("input").val();
                $(Etr).find("tr:eq(0)").find("td:eq(3)").find("select").val(CompletionStatus);
                //$(Etr).find("tr:eq(0)").find(".dropCompletionStatusClass").val($(Etr).find("tr:eq(0)").find(".dropCompletionStatusClass").next().val());
            });
            if ($("#fld_ReportType").val() == "Begin-Year goal plan") {
                $("#hdReporttype").val('1');
                $("#BeginYear").attr("checked", true);
                $("#beginOrMidYear").show();
                $("#ReportType1").show();
                $("#ReportType2").hide();
            }
            if ($("#fld_ReportType").val() == "Mid-Year Update") {
                $("#hdReporttype").val('2');
                $("#MidYear").attr("checked", true);
                $("#beginOrMidYear").show();
                $("#ReportType1").hide();
                $("#ReportType2").show();
            }
            if ($("#fld_ReportType").val() == "End-Year Performance") {
                $("#hdReporttype").val('3');
                $("#EndYear").attr("checked", true);
                $("#beginOrMidYear").show();
            }
            var PYear = $("#fld_Year").val()-0;
            $("#tablediv").find("table").each(function (i, Etr) {
                if (PYear>2017) {
                    $(Etr).find(".Initiativesclass").find("label").eq(0).html("Culture")
                    $(Etr).find(".Initiativesclass").find("label").eq(1).html("Expand Margins")
                    $(Etr).find(".Initiativesclass").find("label").eq(2).html("Organic Growth")
                    $(Etr).find(".Initiativesclass").find("label").eq(3).html("Smart Investment")
                    $(Etr).find(".Initiativesclass").find("label").eq(4).html("Software Transformation")
               
                }
                var values = $(Etr).find(".Honfei5InitiativesValue").val().split(",");
                for (var j = 0; j < values.length; j++) {
                    if (values[j] == "Growth" || values[j] == "Culture") {
                        $(Etr).find(".Honfei5Initiatives").eq(0).children().attr("checked", true);
                    }
                    if (values[j] == "Productivity" || values[j]=="Expand Margins") {
                        $(Etr).find(".Honfei5Initiatives").eq(1).children().attr("checked", true);
                    }
                    if (values[j] == "Cash" || values[j] == "Organic Growth") {
                        $(Etr).find(".Honfei5Initiatives").eq(2).children().attr("checked", true);
                    }
                    if (values[j] == "People" || values[j] == "Smart Investment") {
                        $(Etr).find(".Honfei5Initiatives").eq(3).children().attr("checked", true);
                    }
                    if (values[j] == "Enablers" || values[j] == "Software Transformation") {
                        $(Etr).find(".Honfei5Initiatives").eq(4).children().attr("checked", true);
                    }
                }
            });
            $("#dropYear").val($("#fld_Year").val());
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }

            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
        });
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
        function getButtonCheck(obj, index) {
            var str = "";
            $(obj).parent().parent().each(function (i, Etr) {
                $(Etr).find(".Honfei5Initiatives").each(function (j, Item) {
                    if ($(Item).children().attr("checked")) {
                        str += "," + $(Item).children().last().text();
                    }
                });
            });
            $(obj).parent().parent().parent().find(".Honfei5InitiativesValue").val(str.substr(1));
       }
       function beforeSubmit() {
           $("#dropYear").next().val($("#dropYear option:selected").text());
           $("#tablediv").find("table").each(function (i, Etr) {
               $(Etr).find("tr").eq(0).find(".dropCompletionStatusClass").next().val($(Etr).find("tr").eq(0).find(".dropCompletionStatusClass option:selected").text());
           });
           var status = $("#employeePerformanceStatus").val();
           if ($("#BeginYear").attr("checked")) {
               $("#fld_ReportType").val("Begin-Year goal plan");
               
           }
           if ($("#MidYear").attr("checked")) {
               $("#fld_ReportType").val("Mid-Year Update");
           }
           if ($("#EndYear").attr("checked")) {
               $("#fld_ReportType").val("End-Year Performance");
           }
           var value = true;
//           if (status == "1") {
//               alert("You have been submitted and are under examination and approval.Cannot commit second times");
//               value = false;
//           }
//           if (status == "2") {
//               alert("You should have passed the audit.Cannot commit second times");
//               value = false;
//           }
           var summary = "Employee Performance Report";
           $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
           return value;
       }
       function beforeSave() {
           var summary = "Employee Performance Report";
           $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
           $("#fld_TRSummary").val(summary);
           return true;
       }
       function funchangeReportType(obj) {
           $("#hdReporttype").val(obj);
           $("#btnChangeReportType").click();
       }
       function changeDrop(obj) {
           $(obj).next().val($(obj).val());
       }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Employee Performance Report" processprefix="HRRP" tablename="PROC_EmployeePerformance" tablenamedetail="PROC_EmployeePerformance_DT"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_ApplicantUserLogin" style="display:none"></asp:TextBox>
                <asp:TextBox runat="server" ID="employeePerformanceStatus" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_EmployeePost" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_GM" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Report information</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center"> Year</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:DropDownList ID="dropYear" runat="server" onchange="getChangeCheck(this)"></asp:DropDownList>
                            <%--<asp:Button runat="server" ID="dropYearButton" onclick="dropYearButton_Click" style="display:none;"/>--%>
                            <asp:TextBox ID="fld_Year" runat="server" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Report Type</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:RadioButton ID="BeginYear" GroupName="ReportType" runat="server" 
                                value="Begin-Year goal plan" onclick="funchangeReportType('1')"  />Begin-Year goal plan&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <%--<asp:Button runat="server" ID="beginYearButton" onclick="beginYearButton_Click"  style="display:none"/>--%>
                            <asp:RadioButton ID="MidYear" GroupName="ReportType" runat="server" 
                                value="Mid-Year Update"   onclick="funchangeReportType('2')" />Mid-Year Update<br />
                            <%--<asp:Button runat="server" ID="midYearButton" onclick="midYearButton_Click"  style="display:none"/>--%>
                            <asp:RadioButton ID="EndYear" GroupName="ReportType" runat="server" 
                                value="End-Year Performance"  onclick="funchangeReportType('3')"  />End-Year Performance&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           <%-- <asp:Button runat="server" ID="endYearButton" onclick="endYearButton_Click"  style="display:none"/>--%>
                            <asp:TextBox ID="fld_ReportType" runat="server" style="display:none;"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="hdReporttype" />
                            <asp:Button runat="server" ID="btnChangeReportType" Text="reporttypebutton" OnClick="BtnChangeReportType_Click"  style="display:none" />
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;">Employee Information </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Employee Name</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox ID="fld_EmployeeName" ReadOnly="true" runat="server" style="border:0;background-color:White;"></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">On-boarding department</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:TextBox ID="fld_OnBoardingDepartment" ReadOnly="true" runat="server" style="border:0;background-color:White;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div id="beginOrMidYear" style="display:none">
                <%--<p id="ReportType1" style="font-weight:bold; display:none;">Goal Plan（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <p id="ReportType2" style="font-weight:bold; display:none;">Mid-Year Update（"<span style=" background:red;">&nbsp;</span>" must write） </p>--%>
                <asp:Label runat="server" ID="ReportLabel" Text=""></asp:Label>
                <div style="border:0; margin-bottom:1%;">
                    <asp:Button runat="server" ID="btnAdd" Text="add" CssClass="btn"  CausesValidation="false" OnClick="btnAdd_Click" style="margin-right:1%;" />
                    <%--<asp:Button runat="server" ID="btnDel" Text="delete" CssClass="btn" CausesValidation="false" OnClick="btnDel_Click" />--%>
                </div>
                <div id="tablediv">
                <asp:Repeater ID="fld_detail_PROC_EmployeePerformance_DT" OnItemCommand="fld_detail_PROC_EmployeePerformance_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_EmployeePerformance_DT_ItemDataBound" runat="server" >
                    <ItemTemplate>
                        <table class="table table-condensed table-bordered">
                            <tr>
                                <td class="td-label">
                                 <span style="background:red;height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Goal Name</p>
                                </td>
                                <td class="td-content" colspan="3" >
                                    <asp:TextBox ID="fld_GoalName" Text='<%#Eval("GoalName") %>' runat="server" CssClass="validate[required]" Width="96%"></asp:TextBox>
                                </td>
                                <td class="td-label"> 
                               <span style="background:red;margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Completion Status</p>
                                </td>
                                <td class="td-content"  colspan="2" >
                                    <asp:DropDownList runat="server" CssClass="validate[required]" class="dropCompletionStatusClass"  onchange="changeDrop(this)" ID="dropCompletionStatus" >
                                        <asp:ListItem Selected="True" Value="">--Select Please--</asp:ListItem>
                                        <asp:ListItem>Not Started</asp:ListItem>
                                        <asp:ListItem>Started</asp:ListItem>
                                        <asp:ListItem>Complete</asp:ListItem>
                                        <asp:ListItem>In Process</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox runat="server" ID="fld_CompletionStatus" Text='<%#Eval("CompletionStatus") %>' style="display:none;" ></asp:TextBox>
                                </td>
                                <td width="5%" rowspan="5">
                                    <asp:Button ID="btnDelete" runat="server" Text="delete" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                                    <%--<input type="checkbox" runat="server"  id="cb_SelectValue"  value='<%# Container.ItemIndex+1%>' style="float:right" />
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server" style="display:none;"></asp:Label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label" style="vertical-align:middle;">
                                 <span style="background:red;height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Goal Details</p>
                                </td>
                                <td class="td-content" colspan="6" >
                                    <asp:TextBox ID="fld_GoalDetails" Text='<%#Eval("GoalDetails") %>' runat="server" Rows="4" TextMode="MultiLine" CssClass="validate[required]" Width="98%" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label">
                                 <span style="background:red;height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Honfei 5 Initiatives</p>
                                </td>
                                <td class="td-content Initiativesclass" colspan="6" >
                                    <asp:CheckBox runat="server" class="Honfei5Initiatives" ID="Honfei5Initiatives1" Text="Growth"  onclick="getButtonCheck(this,1)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" class="Honfei5Initiatives" ID="Honfei5Initiatives2" Text="Productivity" onclick="getButtonCheck(this,2)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" class="Honfei5Initiatives" ID="Honfei5Initiatives3" Text="Cash" onclick="getButtonCheck(this,3)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" class="Honfei5Initiatives" ID="Honfei5Initiatives4" Text="People" onclick="getButtonCheck(this,4)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" class="Honfei5Initiatives" ID="Honfei5Initiatives5" Text="Enablers" onclick="getButtonCheck(this,5)"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="display:none">
                                    <asp:TextBox runat="server" class="Honfei5InitiativesValue" ID="fld_Honfei5Initiatives"  Text='<%#Eval("Honfei5Initiatives") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label">
                                 <span style="background:red;height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Assigned Date</p>
                                </td>
                                <td class="td-content" colspan="3" >
                                    <asp:TextBox runat="server" ID="fld_AssignedDate" Width="96%" Text='<%# String.IsNullOrEmpty(Eval("AssignedDate").ToString())? "":DateTime.Parse(Eval("AssignedDate").ToString()).ToString("yyyy-MM-dd") %>' CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                                <td class="td-label">
                                 <span style="background:red;height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Due Date</p>
                                </td>
                                <td class="td-content" colspan="2" >
                                    <asp:TextBox runat="server" ID="fld_DueDate" Width="96%" Text='<%# String.IsNullOrEmpty(Eval("DueDate").ToString())? "":DateTime.Parse(Eval("DueDate").ToString()).ToString("yyyy-MM-dd") %>' CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label" style="vertical-align:middle;">
                                 <span class="reaultLabel" style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Results</p>
                                </td>
                                <td class="td-content" colspan="6" >
                                    <asp:TextBox class="results" ID="fld_Results" Text='<%#Eval("Results") %>' Width="98%" runat="server" Rows="4" TextMode="MultiLine" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
                </div>
            </div>
            <div class="row" style="display:none;">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
        </div>
        </div>
       <%-- <div id="btnDiv" runat="server"   >
            <table style="width: 100%;" >
                <tr>
                    <td align="center"  >
                        <table>
                            <tr>
                                <td> 
                                <input type="button"  class="btn" value="Submit" onclick="submitPageReview('0')" />
                                </td>
                            </tr>
                       </table>
                    </td>
                 </tr>
            </table>
        </div>--%>
          <div id="ReturnBackTask" style="display:none; text-align:center">
        <asp:Button runat="server" ID="btnRevoke"  Visible="false" CssClass="btn" Text="Revoke" 
                onclick="btnRevoke_Click" />
        </div>
        <div style="display:block;">
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        </div>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        <asp:HiddenField runat="server" ID="hdUrgeTask" />
        </div>
    </form>
</body>
</html>



