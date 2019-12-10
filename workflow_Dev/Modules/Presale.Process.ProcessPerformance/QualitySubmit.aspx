<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QualitySubmit.aspx.cs" Inherits="Presale.Process.ProcessPerformance.QualitySubmit" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>KPI Submit Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
        });
        function submitPageReview(obj) {
            $("#ButtonList1_btnSubmit").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="KPI Submit Process" processprefix="KPI" tablename="PROC_ProcessPerformance"
                    runat="server"  ></ui:userinfo>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Summary time</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Year</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_Year"></asp:Label>
                        </td>
                        <td class="td-label"> 
                       <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Month</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_Month"></asp:Label>
                        </td>
                    </tr>
                </table>
                <p style="font-weight:bold;">Summary Description</p>
                <asp:Repeater ID="RepeaterDetail" runat="server">
                    <ItemTemplate>
                        <table class="table table-condensed table-bordered">
                            <tr style="display:none;">
                                <asp:Label runat="server" ID="ROWID" Text='<%# Eval("ROWID")%>'></asp:Label>
                                <asp:Label runat="server" ID="DEPTMENTCODE" Text='<%# Eval("DEPTMENTCODE")%>'></asp:Label>
                            </tr>
                            <tr>
                                <td class="td-label"> 
                               <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Process</p>
                                </td>
                                <td class="td-content"  colspan="3" >
                                    <asp:Label runat="server" ID="PROCESS" Text='<%# Eval("PROCESS")%>'></asp:Label>
                                </td>
                                <td class="td-label"> 
                               <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Process Measurement</p>
                                </td>
                                <td class="td-content"  colspan="3" >
                                    <asp:Label runat="server" ID="PROCESSMEA" Text='<%# Eval("PROCESSMEA")%>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label"> 
                               <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Pass standard</p>
                                </td>
                                <td class="td-content"  colspan="3" >
                                    <asp:Label runat="server" ID="STANDARD" Text='<%# Eval("STANDARD")%>'></asp:Label>
                                </td>
                                <td class="td-label"> 
                               <span style="margin-top:5px;  height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Status</p>
                                </td>
                                <td style="width:80px"    >
                                    <asp:Label runat="server" ID="StatusValue" Text='<%# Eval("StatusValue")%>'></asp:Label>
                                </td>

                                 <td class="td-label"> YTD
                                  </td>
                                  <td style="width:80px" >
                                    <asp:Label runat="server" ID="YTD" Text='<%# Eval("YTD")%>' Width="85%"></asp:Label>
                                  </td>
                            </tr>
                            <tr>
                                <td class="td-label" style="vertical-align:middle;"> 
                               <span style="height:30px; float:left;">&nbsp;</span>
                                <p style="text-align:center">Action Need</p>
                                </td>
                                <td class="td-content"  colspan="7" >
                                    <asp:Label runat="server" ID="ActionNeed" Text='<%# Eval("ActionNeed")%>'></asp:Label>
                                </td>
                            </tr>
                    </table>
                </ItemTemplate>
             </asp:Repeater>
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






