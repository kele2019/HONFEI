<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Presale.Process.HongFeiJVAccess.Approval" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>IT Access Request Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>  
    <script type="text/javascript">
        function showTime(obj) {
            var time = new Date(obj.replace(/-/g, "/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            var value = year + "-" + month + "-" + date
            return value == "NaN-NaN-NaN" ? "" : value;
        }
        $(document).ready(function () {
            $("#read_Expiration").text(showTime($("#read_Expiration").text()));
            var role = $("#read_Role").eq(0).text();
            if (role == "Employee") {
                $("#Role1").attr("checked", "checked");
            }
            if (role == "Contractor") {
                $("#Role2").attr("checked", "checked");
            }
            if (role == "Other") {
                $("#Role3").attr("checked", "checked");
            }
            if ($("#createId").eq(0).next().text() == "Create a new ID") {
                $("#createId").eq(0).attr("checked", true);
            }
            if ($("#existId").eq(0).next().text() == "Disable existing ID") {
                $("#existId").eq(0).attr("checked", true);
            }

            var systemAccessOrg = $("#systemAccessOrg1").eq(0).next().text();
            var items = systemAccessOrg.split(",");
            for (var i = 0; i < items.length; i++) {
                if (items[i] == "Network") {
                    $("#systemAccessOrg1").eq(0).attr("checked", "checked");
                }
                if (items[i] == "Outlook") {
                    $("#systemAccessOrg2").eq(0).attr("checked", "checked");
                }
                if (items[i] == "Daptiv") {
                    $("#systemAccessOrg3").eq(0).attr("checked", "checked");
                }
                if (items[i] == "Engineering") {
                    $("#systemAccessOrg4").eq(0).attr("checked", "checked");
                }
                if (items[i] == "ERP") {
                    $("#systemAccessOrg5").eq(0).attr("checked", "checked");
                }
                if (items[i] == "Other") {
                    $("#systemAccessOrg6").eq(0).attr("checked", "checked");
                }
            }
            if ($("#PrivilegedAccess2").prev().text() == "System Administrator") {
                $("#PrivilegedAccess1").eq(0).attr("checked", "checked");
                $("#read_PAOther").hide();

            }
            if ($("#PrivilegedAccess2").prev().text() == "Other") {
                $("#PrivilegedAccess2").eq(0).attr("checked", "checked");
                $("#read_PrivilegedAccess").hide();
            }
            if ($("#AUP").next().next().text() == "Server Room") {
                $("#AUP").attr("checked", "checked");
            }
            $("#tbodyParent").find("tbody").each(function (i, Etr) {
                $(Etr).find("tr").eq(1).find("td").each(function (j, Item) {
                    var value = $(Item).find("input").eq(2).next().text();
                    if (value == "Read Only") {
                        $(Item).find("input").eq(0).attr("checked", "checked");
                    }
                    if (value == "Read/Write") {
                        $(Item).find("input").eq(1).attr("checked", "checked");
                    }
                    if (value == "Add/Remove") {
                        $(Item).find("input").eq(2).attr("checked", "checked");
                    }
                });
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="IT Access Request Process" processprefix="ITAR" tablename="PROC_HongFeiJVAccess" tablenamedetail="PROC_HongFeiJVAccess_DT"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_appeovalPost" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <P style="font-weight:bold;">SECTION 1: Request Information</P>
                 <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" colspan="1" style="width:13%;">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Purpose</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:Label runat="server" ID="read_Title"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="width:13%;" align="center">
                           <span style="height:30px; float:left;">&nbsp;</span>
                           <input id="Role1" type="checkbox" value="Employee" disabled="disabled" />Employee
                        </td>
                        <td class="td-label" style="border-left:0;width:13%;" align="center">
                           <input id="Role2" type="checkbox" value="Contractor" disabled="disabled" />Contractor
                        </td>
                        <td class="td-label" style="border-left:0;width:13%;" align="center">
                           <input id="Role3" type="checkbox" value="Other" disabled="disabled"/>Other 
                        </td>
                        <td class="td-label" id="role" style="border-left:0;width:11%;" align="center">
                            <asp:Label runat="server" ID="read_Role" style="display:none;"></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                           <p style="text-align:center;">Expiration Date</p> 
                        </td>
                        <td class="td-content" colspan="1">
                            <asp:Label runat="server" ID="read_Expiration"></asp:Label>
                        </td>
                    </tr>
                    </table>

                    <p style="font-weight:bold;">SECTION 2: Access Information</p>
                    <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-content" colspan="4" style="width:50%">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <input id="createId" type="checkbox" value="Create a new ID" disabled="disabled" />Create a new ID &nbsp;&nbsp;&nbsp;
                            <asp:Label runat="server" ID="read_CreateId" style="display:none" ></asp:Label>
                            <asp:Label runat="server" ID="read_CreateIdNo"></asp:Label>
                        </td>
                        <td class="td-label">
                         <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">UID Name</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:Label runat="server" ID="read_SetUpUID"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-content" colspan="4">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <input id="existId" type="checkbox" value="Disable existing ID" disabled="disabled" />Disable existing ID &nbsp;&nbsp;&nbsp;
                            <asp:Label runat="server" ID="read_ExistIdOrg" style="display:none" ></asp:Label>
                            <asp:Label runat="server" ID="read_ExistIdNo"></asp:Label>
                        </td>
                        <td class="td-label">
                            <span style=" height:30px; float:left;">&nbsp;</span>
                        </td>
                        <td class="td-content" colspan="3">
                            <span style=" height:30px; float:left;">&nbsp;</span>
                        </td>
                        <%--<td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        Explain
                        </td>
                        <td class="td-content" colspan="3" >
                            <asp:TextBox runat="server" ID="fld_Explain" Width="95%"  CssClass="validate[required]"></asp:TextBox>
                        </td>--%>
                    </tr>

                    <tr>
                        <td class="td-label" style="vertical-align:middle;width:13%;"> 
                       <span style="height:30px; float:left;">&nbsp;</span>
                       <p style="text-align:center;">A.System Access</p> 
                        </td>
                        <td class="td-content"  colspan="7" >
                            <table style="border:0;">
                                <tr style="border:0;">
                                    
                                    <td style="border:0;">
                                        <input id="systemAccessOrg1" type="checkbox" value="Network" disabled="disabled"/>Network &nbsp;&nbsp;&nbsp;
                                        <asp:Label runat="server" ID="read_SystemAccess" style="display:none"></asp:Label>
                                    </td>
                                    <td style="border:0;">
                                        <input id="systemAccessOrg2" type="checkbox" value="Outlook" disabled="disabled" />Outlook &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td style="border:0;">
                                        <input id="systemAccessOrg3" type="checkbox" value="Daptiv" disabled="disabled" />Daptiv &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr style="border:0;">
                                    <td style="border:0;">
                                        <input id="systemAccessOrg4" type="checkbox" value="HRM" disabled="disabled" />Engineering &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td style="border:0;">
                                        <input id="systemAccessOrg5" type="checkbox" value="ERP" disabled="disabled" />ERP &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td style="border:0;">
                                        <input id="systemAccessOrg6" type="checkbox" value="Other"  disabled="disabled"/>Other&nbsp;&nbsp;
                                        <asp:Label runat="server" ID="read_SAOther"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td class="td-label" colspan="8">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            <p align="center">ERP & HRM require Application Owner Authorization</p>
                        </td>
                    </tr>--%>
                    </table>
                    <table id="tbodyParent" class="table table-condensed table-bordered">
                    <thead>
                    <tr>
                        <td class="td-label" colspan="8">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="float:left;margin-left:1%;">B.Shared Folder Access</p>
                        </td>
                    </tr>
                    </thead>
                    <asp:Repeater  ID="fld_detail_PROC_HongFeiJVAccess_DT" runat="server">
                        <ItemTemplate>
                            <tbody>
                            <tr>
                                <td class="td-label" style="vertical-align:middle; width:17%;">
                                 <span style="height:100%; float:left;">&nbsp;</span>
                                <p style="text-align:center;">Directory Folder or Path</p>
                                </td>
                                
                                <td class="td-content" colspan="6" >
                                    <asp:Label runat="server" ID="read_DirFOP" Text='<%#Eval("DirFOP") %>'>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label" style="vertical-align:middle;">
                                    <span style="height:30px; float:left;">&nbsp;</span>
                                </td>
                                <td class="td-content" colspan="7">
                                    <input  type="checkbox" value="Read Only" disabled="disabled" />Read Only
                                    <input  type="checkbox" value="Read/Write" disabled="disabled" />Read/Write
                                    <input  type="checkbox" value="Add/Remove" disabled="disabled" />Add/Remove
                                    <asp:Label ID="read_ReadOrWrite" Text='<%#Eval("ReadOrWrite") %>' runat="server" style="display:none;"></asp:Label>
                                </td>
                            </tr>
                            </tbody>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    </table>
                    <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="vertical-align:middle; width:17%;">
                         <span style="height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center;">C.Privileged Access</p>
                        </td>
                        <td class="td-content" colspan="3">
                            <input id="PrivilegedAccess1" type="checkbox" value="System Administrator" disabled="disabled" />System Administrator: &nbsp;&nbsp;&nbsp;
                            <asp:Label runat="server" ID="read_PrivilegedAccess"></asp:Label><br />
                            <asp:Label runat="server" ID="read_PrivilegedAccessOrg" style="display:none"></asp:Label>
                            <input id="PrivilegedAccess2" type="checkbox" value="Other" disabled="disabled" />Other
                            <asp:Label runat="server" ID="read_PAOther"></asp:Label>
                        </td>
                    </tr>

                    <%--<tr>
                        <td class="td-label">
                            <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                            E.Other:Provide Additional Information below:
                        </td>
                        <td class="td-content" colspan="7">
                            <asp:TextBox runat="server" ID="fld_PAIB" Width="98%" TextMode="MultiLine" CssClass="validate[required]"></asp:TextBox>
                        </td>
                    </tr>--%>
                    </table>

                    <p style="font-weight:bold;">SECTION 3: Policy Acceptance for New Access</P>
                    <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" colspan="4" style="width:36%;">
                            <input id="AUP" type="checkbox" value="Server Room" style="float:left;" disabled="disabled"/><p style="float:left;">Acceptable Use Policy has been reviewed and accepted</p>
                            <asp:Label runat="server" ID="read_AUP" style="display:none" ></asp:Label>
                        </td>
                        <td class="td-content" colspan="4" style="text-align:center">
                            <p>see \\192.168.101.64\Public\IT_P\IT policy</p>
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



