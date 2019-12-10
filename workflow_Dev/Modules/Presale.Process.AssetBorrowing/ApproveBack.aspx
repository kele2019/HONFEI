<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveBack.aspx.cs" Inherits="Presale.Process.AssetBorrowing.ApproveBack" %>


<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>IT Asset Borrowing Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>  
    <script type="text/javascript">
        function showTime(obj) {
            var time = new Date(obj.text().replace(/-/g, "/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        $(document).ready(function () {
            var receiveValue = $("#read_Receive").text();
            if (receiveValue == "getting once") {
                $("#receive1").attr("checked", true);

                $("#tableRevert").hide();
                $("#tableRevert").prev().hide();
            }
            if (receiveValue == "borrowing") {
                $("#receive3").attr("checked", true);
                $("#receive").attr("colspan", "3");
                $("#time").show();
             
            }
            $("#read_borrowTimeStart").text(showTime($("#read_borrowTimeStart")));
            $("#read_borrowTimeEnd").text(showTime($("#read_borrowTimeEnd")));

            if ($("#read_IAName").text() == "laptop") {
                $("#iano").show();
                $("#ianoValue").show();
            }
            var remoteEAValue = $("#read_IANo").text();
            if (remoteEAValue == "yes") {
                $("#rea1").attr("checked", "checked");
            }
            if (remoteEAValue == "no") {
                $("#rea2").attr("checked", "checked");
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="IT Asset Borrowing Request Process" processprefix="ITAB" tablename="PROC_AssetBorrowing"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_UserName" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require</p>
                <table class="table table-condensed table-bordered">
                    <%--<tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Phone</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_Phone"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Type</p>
                        </td>
                        <td class="td-content" colspan="7" id="receive">
                            <asp:CheckBox runat="server" ID="receive1" value="getting once" Enabled="false" />getting once &nbsp;&nbsp;
                            <%--<asp:CheckBox runat="server" ID="receive2" value="往复领取" disabled="disabled" />往复领取 &nbsp;&nbsp;--%>
                            <asp:CheckBox runat="server" ID="receive3" value="borrowing" Enabled="false"/>borrowing
                            <asp:Label runat="server" ID="read_Receive" style="display:none;"></asp:Label>
                        </td>
                        <td id="time" style="display:none;" colspan="4" class="td-content" >
                                From&nbsp;&nbsp;<asp:Label runat="server" ID="read_borrowTimeStart"></asp:Label>&nbsp;&nbsp;
                                <asp:Label runat="server" ID="read_startHour"></asp:Label>&nbsp;&nbsp;&nbsp;
                               To&nbsp;&nbsp;<asp:Label runat="server" ID="read_borrowTimeEnd"></asp:Label>&nbsp;&nbsp;
                               <asp:Label runat="server" ID="read_endHour"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">IT Assets Name</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <asp:Label runat="server" ID="read_IAName"></asp:Label>
                        </td>
                        <td class="td-label"> 
                            <div style="display:none" id="iano">
                           <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Remote Email Access</p>
                            </div>
                        </td>
                        <td class="td-content"  colspan="3">
                            <div style="display:none;" id="ianoValue">
                            <asp:RadioButton runat="server" ID="rea1" GroupName="RemoteEA" Enabled="false" />yes&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton runat="server" ID="rea2" GroupName="RemoteEA" Enabled="false"/>no
                            <asp:Label runat="server" ID="read_IANo" style="display:none;"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center">Reason</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label ID="read_ReceiveReason" runat="server"></asp:Label>
                        </td>
                    </tr>
                 </table>


                 <p style="font-weight:bold;">Revert Info</p>
                  <table class="table table-condensed table-bordered" id="tableRevert">
                    <tr>
                     <td class="td-label">
                    <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                     <p style="text-align:center;"> Revert Date</p></td>
                     <td colspan="3">
                     <asp:TextBox runat="server" ID="fld_RevertDate" CssClass="validate[required]" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                     </td>
                     </tr>
                     <tr>
                       <td class="td-label" >
                     <p style="text-align:center;margin-top:25%;">Revert Comments</p></td>
                     <td colspan="3">
                     <asp:TextBox runat="server" ID="fld_RevertComments" TextMode="MultiLine" Rows="5" Width="98%"></asp:TextBox>
                     </td>

                    </tr>
                    </table>
            </div>
            <div class="row" style="display:none;">
                <attach:attachments id="Attachments1" ReadOnly="true" runat="server"></attach:attachments>
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


