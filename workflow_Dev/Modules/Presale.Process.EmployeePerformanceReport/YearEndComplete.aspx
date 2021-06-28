<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YearEndComplete.aspx.cs" Inherits="Presale.Process.EmployeePerformanceReport.YearEndApproval" %>

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
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
        $(document).ready(function () {

            var PYear = $("#read_Year").text() - 0;
            $("#tablediv").find("table").each(function (i, Etr) {
                if (PYear > 2017) {
                    $(Etr).find(".Initiativesclass").find("label").eq(0).html("Culture")
                    $(Etr).find(".Initiativesclass").find("label").eq(1).html("Expand Margins")
                    $(Etr).find(".Initiativesclass").find("label").eq(2).html("Organic Growth")
                    $(Etr).find(".Initiativesclass").find("label").eq(3).html("Smart Investment")
                    $(Etr).find(".Initiativesclass").find("label").eq(4).html("Software Transformation")

                }
                var values = $(Etr).find(".Honfei5InitiativesValue").text().split(",");
                for (var j = 0; j < values.length; j++) {
                    if (values[j] == "Growth" || values[j] == "Culture") {
                        $(Etr).find(".Honfei5Initiatives").eq(0).children().attr("checked", true);
                    }
                    if (values[j] == "Productivity" || values[j] == "Expand Margins") {
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
            if ($("#read_NeedOrNo").val() == "No")
                $("#NeedOrNo").hide();

            $(".ResultGrade").each(function () {
                var ResultGrade = $(this).val();
                if (ResultGrade == "1")
                    $(this).prev().prev().prev().attr("checked", true);
                if (ResultGrade == "2")
                    $(this).prev().prev().attr("checked", true);
                if (ResultGrade == "3")
                    $(this).prev().prev().attr("checked", true);
            });
            $("input[type='radio']").attr("disabled", true);
            if (request('Type') == "myapproval") {
                $("#btnDiv").hide();
            }

            LoadBehavior($("#read_Behavior1Score"), 'Behavior1Score');
            LoadBehavior($("#read_Behavior2Score"), 'Behavior2Score');
            LoadBehavior($("#read_Behavior3Score"), 'Behavior3Score');
            LoadBehavior($("#read_Behavior4Score"), 'Behavior4Score');
            LoadBehavior($("#read_Behavior5Score"), 'Behavior5Score');
            LoadBehavior($("#read_Behavior6Score"), 'Behavior6Score');
            LoadBehavior($("#read_Behavior7Score"), 'Behavior7Score');
            LoadBehavior($("#read_Behavior8Score"), 'Behavior8Score');
            LoadBehavior($("#read_Behavior9Score"), 'Behavior9Score');
            LoadBehavior($("#read_Behavior10Score"));
            LoadBehavior($("#read_Behavior11Score"));
            LoadBehavior($("#read_Behavior12Score"));


        });



        function LoadBehavior(obj, flag) {
            var RDate = $("#UserInfo1_fld_REQUESTDATE").text();
            var RYear = new Date(RDate).getFullYear() - 0;
            var value = $(obj).text();
            if (RYear < 2017) {
                $("#behaviortable1").show();
                $("#behaviortable2").hide();
                $("#behaviortable3").hide();
                if (value == "Exceed")
                    $(obj).parent().prev().prev().find("input").attr("checked", true);
                if (value == "At")
                    $(obj).parent().prev().find("input").attr("checked", true);
                if (value == "Below")
                    $(obj).prev().attr("checked", true);
                if (value == "Rarely")
                    $(obj).prev().attr("checked", true);
            }
            else {
                if (RYear >= 2021) {
                    $("#behaviortable1").hide();
                    $("#behaviortable2").hide();
                    $("#behaviortable3").show();

                    if (flag != undefined) {
                        if (value == "Exceed")
                            $("input[name^='" + flag + "']").eq(7).attr("checked", true);
                        if (value == "At")
                            $("input[name^='" + flag + "']").eq(8).attr("checked", true);
                        if (value == "Below")
                            $("input[name^='" + flag + "']").eq(9).attr("checked", true);
                        if (value == "Rarely")
                            $("input[name^='" + flag + "']").eq(10).attr("checked", true);

                    }
                }
                else {
                    $("#behaviortable1").hide();
                    $("#behaviortable2").show();
                    $("#behaviortable3").hide();
               
                
                if (flag != undefined) {
                    if (value == "Exceed")
                        $("input[name^='" + flag + "']").eq(3).attr("checked", true);
                    if (value == "At")
                        $("input[name^='" + flag + "']").eq(4).attr("checked", true);
                    if (value == "Below")
                        $("input[name^='" + flag + "']").eq(5).attr("checked", true);
                    if (value == "Rarely")
                        $("input[name^='" + flag + "']").eq(6).attr("checked", true);

                }
            }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Employee Performance Report" processprefix="HRRP" tablename="PROC_EmployeePerformance" tablenamedetail="PROC_EmployeePerformance_DT"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Report information</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center"> Year</p>
                        </td>
                        <td class="td-content">
                            <asp:Label ID="read_Year" runat="server"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Report Type</p>
                        </td>
                        <td class="td-content">
                            <asp:Label ID="read_ReportType" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;">Employee Information</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Employee Name</p>
                        </td>
                        <td class="td-content" colspan="1" >
                            <asp:Label ID="read_EmployeeName" runat="server"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">On-boarding department</p>
                        </td>
                        <td class="td-content">
                            <asp:Label ID="read_OnBoardingDepartment" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;">Results Assessment</p>
                <div id="tablediv">
                <asp:Repeater ID="fld_detail_PROC_EmployeePerformance_DT" runat="server" >
                    <ItemTemplate>
                        <table class="table table-condensed table-bordered">
                            <tr>
                                <td class="td-label">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Goal Name</p>
                                </td>
                                <td class="td-content" colspan="3" >
                                    <asp:Label ID="read_GoalName" Text='<%#Eval("GoalName") %>' runat="server"></asp:Label>
                                </td>
                                <td class="td-label"> 
                               <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Completion Status</p>
                                </td>
                                <td class="td-content"  colspan="2" >
                                    <asp:Label runat="server" ID="read_CompletionStatus" Text='<%#Eval("CompletionStatus") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Goal Details</p>
                                </td>
                                <td class="td-content" colspan="6" >
                                    <asp:Label ID="read_GoalDetails" Text='<%#Eval("GoalDetails") %>' runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Honfei 5 Initiatives</p>
                                </td>
                                <td class="td-content Initiativesclass" colspan="6" >
                                    <asp:CheckBox runat="server" Enabled="false" class="Honfei5Initiatives" ID="Honfei5Initiatives1" Text="Growth"  onclick="getButtonCheck(this,1)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" Enabled="false" class="Honfei5Initiatives" ID="Honfei5Initiatives2" Text="Productivity" onclick="getButtonCheck(this,2)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" Enabled="false" class="Honfei5Initiatives" ID="Honfei5Initiatives3" Text="Cash" onclick="getButtonCheck(this,3)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" Enabled="false" class="Honfei5Initiatives" ID="Honfei5Initiatives4" Text="People" onclick="getButtonCheck(this,4)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" Enabled="false" class="Honfei5Initiatives" ID="Honfei5Initiatives5" Text="Enablers" onclick="getButtonCheck(this,5)"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="display:none">
                                    <asp:Label runat="server" class="Honfei5InitiativesValue" ID="read_Honfei5Initiatives"  Text='<%#Eval("Honfei5Initiatives") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Assigned Date</p>
                                </td>
                                <td class="td-content" colspan="3" >
                                    <asp:Label runat="server" ID="read_AssignedDate" Text='<%# String.IsNullOrEmpty(Eval("AssignedDate").ToString())? "":DateTime.Parse(Eval("AssignedDate").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                                </td>
                                <td class="td-label">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Due Date</p>
                                </td>
                                <td class="td-content" colspan="2" >
                                    <asp:Label runat="server" ID="read_DueDate" Text='<%# String.IsNullOrEmpty(Eval("DueDate").ToString())? "":DateTime.Parse(Eval("DueDate").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label">
                                 <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Results</p>
                                </td>
                                <td class="td-content" colspan="6" >
                                    <asp:Label ID="read_Results" Text='<%#Eval("Results") %>' runat="server"></asp:Label>
                                </td>
                            </tr>
                          <%-- <tr>
                           <td class="td-label" style="text-align:center;vertical-align:middle;">
                           Comments
                           </td>
                           <td colspan="6">
                            <asp:Label ID="read_Result1Goals"  Text='<%#Eval("Result1Goals") %>'  runat="server"  ></asp:Label>

                           </td>
                           </tr>--%>
                           <tr>
                             <td class="td-label" style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Grade</p>
                             </td>
                           <td colspan="6">
                           <input type="radio" name='Result1Score<%# Container.ItemIndex+1%>'  disabled="disabled"  />Exceed
                           <input type="radio" name='Result1Score<%# Container.ItemIndex+1%>'  disabled="disabled"   style="margin-left:20px;"/>At
                           <input type="radio" name='Result1Score<%# Container.ItemIndex+1%>'  disabled="disabled" style="margin-left:20px;" />Below
                           <asp:TextBox runat="server" class="ResultGrade" ID="read_ResultGrade"  Text='<%#Eval("ResultGrade") %>' style="display:none"></asp:TextBox>
                           </td>
                           </tr>

                        </table>
                    </ItemTemplate>
                </asp:Repeater>
                </div>
          
                <p style="font-weight:bold;">Behavior Assessment</p>
                <table class="table table-condensed table-bordered"  id="behaviortable1">
                    <tr>
                        <th width="4%">
                            <p style="text-align:center" >No</p>
                        </th>
                         <th width="54%">
                            <p style="text-align:center">Goals</p>
                        </th>
                         <th width="14%">
                            <p style="text-align:center" >Exceed</p>
                        </th>
                        <th width="14%">
                            <p style="text-align:center">At</p>
                        </th>
                         <th width="14%">
                            <p style="text-align:center">Below</p>
                        </th>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">1</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Global Mindset</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton25" runat="server" GroupName="Behavior1Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton26" runat="server" GroupName="Behavior1Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton27" runat="server" GroupName="Behavior1Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Behavior1Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">2</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Leadership Impact</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton28" runat="server" GroupName="Behavior2Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton29" runat="server" GroupName="Behavior2Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton30" runat="server" GroupName="Behavior2Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior2Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">3</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Gets Results</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton31" runat="server" GroupName="Behavior3Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton32" runat="server" GroupName="Behavior3Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton33" runat="server" GroupName="Behavior3Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior3Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">4</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Makes People Better</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton34" runat="server" GroupName="Behavior4Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton35" runat="server" GroupName="Behavior4Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton36" runat="server" GroupName="Behavior4Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior4Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">5</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Integrative Thinker</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton37" runat="server" GroupName="Behavior5Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton38" runat="server" GroupName="Behavior5Score" Value="At" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton39" runat="server" GroupName="Behavior5Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Behavior5Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">6</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Fosters Teamwork and Diversity</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton40" runat="server" GroupName="Behavior6Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton41" runat="server" GroupName="Behavior6Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton42" runat="server" GroupName="Behavior6Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior6Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">7</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Champions Change and 6 Sigma</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton43" runat="server" GroupName="Behavior7Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton44" runat="server" GroupName="Behavior7Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton45" runat="server" GroupName="Behavior7Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior7Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">8</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Intelligent Risk Taking</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton46" runat="server" GroupName="Behavior8Score" Value="Exceed" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton47" runat="server" GroupName="Behavior8Score" Value="At" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton48" runat="server" GroupName="Behavior8Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Behavior8Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">9</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Self-Aware/Learner</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton49" runat="server" GroupName="Behavior9Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton50" runat="server" GroupName="Behavior9Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton51" runat="server" GroupName="Behavior9Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Behavior9Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">10</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Effective Communicator</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton52" runat="server" GroupName="Behavior10Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton53" runat="server" GroupName="Behavior10Score" Value="At" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton54" runat="server" GroupName="Behavior10Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior10Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">11</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Growth and Customer Focus</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton55" runat="server" GroupName="Behavior11Score" Value="Exceed" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton56" runat="server" GroupName="Behavior11Score" Value="At" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton57" runat="server" GroupName="Behavior11Score" Value="Below" ReadOnly="true"/>
                            <asp:Label runat="server" ID="read_Behavior11Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">12</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Technical or Functional Excellence</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton58" runat="server" GroupName="Behavior12Score" Value="Exceed" ReadOnly="true" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton59" runat="server" GroupName="Behavior12Score" Value="At" ReadOnly="true"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton60" runat="server" GroupName="Behavior12Score" Value="Below" ReadOnly="true" />
                            <asp:Label runat="server" ID="read_Behavior12Score" style="display:none;"></asp:Label>
                        </td>
                    </tr>
                </table>


                   <table class="table table-condensed table-bordered" id="behaviortable2">
                    <tr>
                        <th width="4%">
                            <p style="text-align:center" >No</p>
                        </th>
                         <th width="40%">
                            <p style="text-align:center">Behaviors</p>
                        </th>
                         <th width="14%">
                            <p style="text-align:center" >Always</p>
                        </th>
                        <th width="14%">
                            <p style="text-align:center">Frequently</p>
                        </th>
                         <th width="14%">
                            <p style="text-align:center">Sometimes</p>
                        </th>
                          <th width="14%">
                            <p style="text-align:center">Rarely</p>
                        </th>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">1</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Have a Passion for Winning</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton101" runat="server" GroupName="Behavior1Score" Value="Exceed"   Enabled="false"  />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton102" runat="server" GroupName="Behavior1Score" Value="At"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton103" runat="server" GroupName="Behavior1Score" Value="Below"  Enabled="false"/>
                        </td>
                         <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton68" runat="server" GroupName="Behavior1Score" Value="Below"  Enabled="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">2</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Be a Zealot for Growth</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton4" runat="server" GroupName="Behavior2Score" Value="Exceed"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton5" runat="server" GroupName="Behavior2Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton6" runat="server" GroupName="Behavior2Score" Value="Below"  Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton67" runat="server" GroupName="Behavior2Score" Value="Below"  Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">3</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Think Big … Then Make It Happen</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton7" runat="server" GroupName="Behavior3Score" Value="Exceed"   Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton8" runat="server" GroupName="Behavior3Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton9" runat="server" GroupName="Behavior3Score" Value="Below"   Enabled="false" />
                        </td>
                          <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton66" runat="server" GroupName="Behavior3Score" Value="Below"   Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">4</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Act with Urgency</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton10" runat="server" GroupName="Behavior4Score" Value="Exceed"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton11" runat="server" GroupName="Behavior4Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton12" runat="server" GroupName="Behavior4Score" Value="Below"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton65" runat="server" GroupName="Behavior4Score" Value="Below"   Enabled="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">5</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Be Courageous</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton13" runat="server" GroupName="Behavior5Score" Value="Exceed"   Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton14" runat="server" GroupName="Behavior5Score" Value="At"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton15" runat="server" GroupName="Behavior5Score" Value="Below"   Enabled="false" />
                        </td>

                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton64" runat="server" GroupName="Behavior5Score" Value="Below"  Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">6</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Go Beyond</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton16" runat="server" GroupName="Behavior6Score" Value="Exceed"   Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton17" runat="server" GroupName="Behavior6Score" Value="At"   Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton18" runat="server" GroupName="Behavior6Score" Value="Below"   Enabled="false"/>
                        </td>

                         <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton63" runat="server" GroupName="Behavior6Score" Value="Below"   Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">7</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Inspire Greatness</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton19" runat="server" GroupName="Behavior7Score" Value="Exceed"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton20" runat="server" GroupName="Behavior7Score" Value="At"  Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton21" runat="server" GroupName="Behavior7Score" Value="Below"  Enabled="false"/>
                        </td>

                          <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton62" runat="server" GroupName="Behavior7Score" Value="Below"  Enabled="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">8</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Become Your Best</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Behavior8Score" Value="Exceed"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="Behavior8Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton3" runat="server" GroupName="Behavior8Score" Value="Below"  Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton61" runat="server" GroupName="Behavior8Score" Value="Below"   Enabled="false" />
                        </td>
                    </tr>
                    
                      <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">9</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Be Committed</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton88" runat="server" GroupName="Behavior9Score" Value="Exceed"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton89" runat="server" GroupName="Behavior9Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton90" runat="server" GroupName="Behavior9Score" Value="Below"  Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton91" runat="server" GroupName="Behavior9Score" Value="Below"   Enabled="false" />
                        </td>
                    </tr>
                  
                </table>

                  <table class="table table-condensed table-bordered" id="behaviortable3">
                    <tr>
                        <th width="4%">
                            <p style="text-align:center" >No</p>
                        </th>
                         <th width="40%">
                            <p style="text-align:center">Behaviors</p>
                        </th>
                         <th width="14%">
                            <p style="text-align:center" >Always</p>
                        </th>
                        <th width="14%">
                            <p style="text-align:center">Frequently</p>
                        </th>
                         <th width="14%">
                            <p style="text-align:center">Sometimes</p>
                        </th>
                          <th width="14%">
                            <p style="text-align:center">Rarely</p>
                        </th>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">1</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Have a Passion for Winning</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton92" runat="server" GroupName="Behavior1Score" Value="Exceed"   Enabled="false"  />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton93" runat="server" GroupName="Behavior1Score" Value="At"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton94" runat="server" GroupName="Behavior1Score" Value="Below"  Enabled="false"/>
                        </td>
                         <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton95" runat="server" GroupName="Behavior1Score" Value="Below"  Enabled="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">2</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Be a Zealot for Growth</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton96" runat="server" GroupName="Behavior2Score" Value="Exceed"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton97" runat="server" GroupName="Behavior2Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton98" runat="server" GroupName="Behavior2Score" Value="Below"  Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton99" runat="server" GroupName="Behavior2Score" Value="Below"  Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style="background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">3</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Think Big … Then Make It Happen</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton100" runat="server" GroupName="Behavior3Score" Value="Exceed"   Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton104" runat="server" GroupName="Behavior3Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton105" runat="server" GroupName="Behavior3Score" Value="Below"   Enabled="false" />
                        </td>
                          <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton106" runat="server" GroupName="Behavior3Score" Value="Below"   Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">4</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Act with Urgency</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton107" runat="server" GroupName="Behavior4Score" Value="Exceed"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton108" runat="server" GroupName="Behavior4Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton109" runat="server" GroupName="Behavior4Score" Value="Below"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton110" runat="server" GroupName="Behavior4Score" Value="Below"   Enabled="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">5</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Be Courageous</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton111" runat="server" GroupName="Behavior5Score" Value="Exceed"   Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton112" runat="server" GroupName="Behavior5Score" Value="At"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton113" runat="server" GroupName="Behavior5Score" Value="Below"   Enabled="false" />
                        </td>

                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton114" runat="server" GroupName="Behavior5Score" Value="Below"  Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">6</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Become your best</p><%--Go Beyond--%>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton115" runat="server" GroupName="Behavior6Score" Value="Exceed"   Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton116" runat="server" GroupName="Behavior6Score" Value="At"   Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton117" runat="server" GroupName="Behavior6Score" Value="Below"   Enabled="false"/>
                        </td>

                         <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton118" runat="server" GroupName="Behavior6Score" Value="Below"   Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">7</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Be Committed</p><%--Inspire Greatness--%>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton119" runat="server" GroupName="Behavior7Score" Value="Exceed"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton120" runat="server" GroupName="Behavior7Score" Value="At"  Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton121" runat="server" GroupName="Behavior7Score" Value="Below"  Enabled="false"/>
                        </td>

                          <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton122" runat="server" GroupName="Behavior7Score" Value="Below"  Enabled="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;vertical-align:middle;">
                         <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">8</p>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                            <p style="text-align:center">Build Exceptional Talent</p><%--Become Your Best--%>
                        </td>
                        <td style="text-align:center;vertical-align:middle;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton123" runat="server" GroupName="Behavior8Score" Value="Exceed"  Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton124" runat="server" GroupName="Behavior8Score" Value="At"   Enabled="false"/>
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton125" runat="server" GroupName="Behavior8Score" Value="Below"  Enabled="false" />
                        </td>
                        <td style="text-align:center;vertical-align:middle;">
                       <span style="height:30px; float:left;">&nbsp;</span>
                            <asp:RadioButton ID="RadioButton126" runat="server" GroupName="Behavior8Score" Value="Below"   Enabled="false" />
                        </td>
                    </tr>
                  
                </table>



                 <table class="table table-condensed table-bordered" style="display:none">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Describe behaviros employees demonstrates well</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_DBEDW" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered" style="display:none">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Describe behaviors employee can improve</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_DBECI" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">9 Block rating</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_BRating" ></asp:Label>
                              <img src="standar.png" width="50px" height="50px" />
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Overall Summary</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_OverallSummary" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Development Plan</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_DevelopmentPlan" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Potential Next Moves</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_PNMoves" runat="server" ></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="NeedOrNo">
                <p style="font-weight:bold;">Second Level Manager Feedback</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Feedback</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_Feedback" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                </div>
            </div>
            <div class="row" style="display:none;">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
        </div>
        <div id="btnDiv" runat="server"   >
            <table style="width: 100%;" >
                <tr>
                    <td align="center"  >
                        <table>
                            <tr>
                                <td> 
                                <input type="button"  class="btn" value="Complete" onclick="submitPageReview('0')" />
                                </td>
                            </tr>
                       </table>
                    </td>
                 </tr>
            </table>
        </div>
        <div style="display:none;">
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        </div>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
         <asp:TextBox ID="read_NeedOrNo" runat="server" style="display:none;"></asp:TextBox>
        </div>
    </form>
</body>
</html>


