<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QualityApproval.aspx.cs" Inherits="Presale.Process.NewEmployee.QualityApproval" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>New Employee On-boardIng</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function beforeSubmit() {
            if ($("#RadioButton1").attr("checked")) {
                $("#fld_QualityAdmin").val($("#read_QM").text());
            }
            if ($("fld_QualityAdmin").val() == "") {
                alert("please select admin");
                return false;
            }
            return true;
        }
        $(document).ready(function () {
            $("#read_OBDate").text(showTime($("#read_OBDate").text()));
            $("#fld_QualityTrainDate").val(showTime($("#fld_QualityTrainDate").val()));
            if ($("#fld_QualityAdmin").val() == $("#read_QM").text()) {
                $("#RadioButton1").attr("checked", true);
            }
        });
        function showTime(obj) {
            var time = new Date(obj.replace(/-/g, "/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="New Employee On-boardIng" processprefix="HREO" tablename="PROC_NewEmployee"
                    runat="server"  ></ui:userinfo>
            </div>
            <asp:TextBox runat="server" ID="domin" style="display:none;" ></asp:TextBox>
            <div class="row">
                <p style="font-weight:bold;">Employee Information </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Employee Name</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_EmployeeName"></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">On-boarding date</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_OBDate"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">On-boarding department</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_OBDepartment"></asp:Label>
                        </td>
                    
                        <%--<td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Post</p>
                        </td>
                        <td class="td-content" colspan="3" >
                        <asp:Label runat="server" ID="read_Post"></asp:Label>
                        </td>--%>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">Training Place</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_TrainPlace"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Training Content</p>
                        </td>
                        <td class="td-content" colspan="7" >
                        <asp:Label runat="server" ID="read_TrainContent"></asp:Label>
                        </td>
                    </tr>
                    
                </table>
                <p style="font-weight:bold;">Agenda of information（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Quality</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:CheckBox runat="server" ID="RadioButton1" Checked="true"/><asp:Label runat="server" ID="read_QM" ></asp:Label>
                            <asp:TextBox runat="server" ID="fld_QualityAdmin" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Training Date</p>
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_QualityTrainDate" Width="96%" CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
        </div>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>



