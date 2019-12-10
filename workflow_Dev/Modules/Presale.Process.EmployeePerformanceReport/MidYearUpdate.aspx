<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MidYearUpdate.aspx.cs" Inherits="Presale.Process.EmployeePerformanceReport.MidYearUpdate" %>

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
            //            $(obj).find("option:selected").each(function (i, Etr) {
            //                if ($(Etr).attr("Selected")) {
            //                    $(obj).next().val($(Etr).text());
            //                }
            //            });
        }
        $(document).ready(function () {
            //            if ($("#read_ReportType").text() == "年初绩效汇总") {
            //                $("#ReportType1").show();
            //            }
            //            if ($("#read_ReportType").text() == "年中绩效更新") {
            //                $("#ReportType2").show();
            //            }
            $("#tablediv").find("table").each(function (i, Etr) {
                $(Etr).find("tr").eq(2).find("td").eq(1).each(function (j, Item) {
                    alert($(Item).eq(5).html());
                    var value = $(Item).eq(5).val();
                    var values = value.split(",");
                    for (var e = 0; e <= values.length; e++) {
                        if (values[e] == "Growth") {
                            $(Item).eq(1).children().eq(0).attr("checked", true);
                        }
                        if (values[e] == "Productivity") {
                            $(Item).eq(1).children().eq(1).attr("checked", true);
                        }
                        if (values[e] == "Cash") {
                            $(Item).eq(1).children().eq(2).attr("checked", true);
                        }
                        if (values[e] == "People") {
                            $(Item).eq(1).children().eq(3).attr("checked", true);
                        }
                        if (values[e] == "Enablers") {
                            $(Item).eq(1).children().eq(4).attr("checked", true);
                        }
                    }
                });
            });

            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
        });
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
        function getButtonCheck(obj, index) {
            var str = "";
            var substr1 = "";
            var substr2 = "";
            var substr3 = "";
            var substr4 = "";
            var substr5 = "";
            if (index == "1") {
                if ($(obj).attr("checked")) {
                    substr1 = "," + "Growth";
                }
                else {
                    substr1 = "";
                }
            }
            if (index == "2") {
                if ($(obj).attr("checked")) {
                    substr2 = "," + "Productivity";
                }
                else {
                    substr2 = "";
                }
            }
            if (index == "3") {
                if ($(obj).attr("checked")) {
                    substr3 = "," + "Cash";
                }
                else {
                    substr3 = "";
                }
            }
            if (index == "4") {
                if ($(obj).attr("checked")) {
                    substr4 = "," + "People";
                }
                else {
                    substr4 = "";
                }
            }
            if (index == "5") {
                if ($(obj).attr("checked")) {
                    substr5 = "," + "Enablers";
                }
                else {
                    substr5 = "";
                }
            }
            str = substr1 + substr2 + substr3 + substr4 + substr5;
            str = str.substr(1);
            $(obj).parent().next().children().val(str);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Employee Performance Report" processprefix="HRRP" tablename="PROC_EmployeePerformance" tablenamedetail="PROC_EmployeePerformance_DT"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_ApplicantUser" style="display:none"></asp:TextBox>
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
                            <asp:DropDownList ID="dropYear" runat="server"></asp:DropDownList>
                            <asp:TextBox ID="fld_Year" runat="server" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Report Type</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:RadioButton ID="ReportType1" GroupName="ReportType" runat="server" value="Begin-Year goal plan" Checked="false" />Begin-Year goal plan&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="RadioButton1" GroupName="ReportType" runat="server" value="Mid-Year Update" />Mid-Year Update<br />
                            <asp:RadioButton ID="ReportType3" GroupName="ReportType" runat="server" value="End-Year Performance" />End-Year Performance&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="fld_ReportType" runat="server" style="display:none;"></asp:TextBox>
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
                <p id="reporttype2" style="font-weight:bold; display:none;">mid-year update（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <div style="border:0; margin-bottom:1%;">
                    <asp:Button runat="server" ID="btnAdd" Text="add" CssClass="btn"  CausesValidation="false" OnClick="btnAdd_Click" style="margin-right:1%;" />
                    <asp:Button runat="server" ID="btnDel" Text="delete" CssClass="btn" CausesValidation="false" OnClick="btnDel_Click" />
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
                                    <asp:DropDownList runat="server" ID="dropCompletionStatus" onchange="getChangeCheck(this)"></asp:DropDownList>
                                    <asp:TextBox runat="server" ID="fld_CompletionStatus" Text='<%#Eval("CompletionStatus") %>' style="display:none;" ></asp:TextBox>
                                </td>
                                <td width="5%" rowspan="5">
                                    <input type="checkbox" runat="server"  id="cb_SelectValue"  value='<%# Container.ItemIndex+1%>' style="float:right" />
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server" style="display:none;"></asp:Label>
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
                                    <asp:CheckBox runat="server" ID="Honfei5Initiatives1" Value="Growth"  onclick="getButtonCheck(this,1)" />Growth&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" ID="Honfei5Initiatives2" Value="Productivity" onclick="getButtonCheck(this,2)" />Productivity&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" ID="Honfei5Initiatives3" Value="Cash" onclick="getButtonCheck(this,3)" />Cash&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" ID="Honfei5Initiatives4" Value="People" onclick="getButtonCheck(this,4)" />People&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox runat="server" ID="Honfei5Initiatives5" Value="Enablers" onclick="getButtonCheck(this,5)"  />Enablers&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="display:none;">
                                    <asp:TextBox runat="server"  ID="fld_Honfei5Initiatives" style="display:none;" Text='<%#Eval("Honfei5Initiatives") %>'></asp:TextBox>
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
                                 <span style="background:red;height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Results</p>
                                </td>
                                <td class="td-content" colspan="6" >
                                    <asp:TextBox ID="fld_Results" Text='<%#Eval("Results") %>' Width="98%" runat="server" Rows="4" TextMode="MultiLine" CssClass="validate[required]" ></asp:TextBox>
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
        </div>
    </form>
</body>
</html>



