<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRReview.aspx.cs" Inherits="Presale.Process.OT.HRReview" %>


<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Time Record Application Process</title>
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
            $("#read_StartDate").text(showTime($("#read_StartDate").text()));
            $("#read_EndDate").text(showTime($("#read_EndDate").text()));
            if (request("type") == "myapproval") {
                $("#btnDiv").hide();
            }
        });

        
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").val("Confirm Review?");
            $("#ApprovalHistory1_txtSpecialAction").val("Review");
            $("#ButtonList1_btnSubmit").click();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Time Record Application Process" processprefix="HROT" tablename="PROC_OT"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_UserName" style="display:none;"></asp:TextBox>
                <asp:Label runat="server" ID="read_ApplierLogin" style="display:none"></asp:Label>
                <asp:Label runat="server" ID="read_OTMonth" style="display:none;" ></asp:Label>
                <asp:Label runat="server" ID="read_OTYear" style="display:none;" ></asp:Label>
                <asp:Label runat="server" ID="read_HR" style="display:none;"></asp:Label>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"  style="width:100px">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Applying for</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_ApplyingFor"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Overtime Reason</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <asp:Label runat="server"  ID="read_Reason"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">From</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <span><asp:Label runat="server"  ID="read_StartDate"></asp:Label></span>&nbsp;&nbsp;
                            <asp:Label runat="server" ID="read_StartHours"></asp:Label>:
                            <asp:Label runat="server" ID="read_StartMinutes"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">To</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <span><asp:Label runat="server"  ID="read_EndDate" ></asp:Label></span>&nbsp;&nbsp;
                            <asp:Label runat="server" ID="read_EndHours"></asp:Label>:
                            <asp:Label runat="server" ID="read_EndMinutes"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Sum</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <span><asp:Label runat="server"  ID="read_SumHour" ></asp:Label></span>hours
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row" style="display:none;">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
             <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>

               <div class="row" id="btnDiv" style="margin-left:50%">
            <input type="button" value="Review" class="btn" onclick="submitPageReview()"  />
             </div>

        </div>
        <div style="display:none">
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
        </div>
    </form>
</body>
</html>