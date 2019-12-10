<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Approval.aspx.cs" Inherits="采购申请2.Approval" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>采购申请2</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <ui:userinfo id="UserInfo1" processtitle="采购申请2" processprefix="FP" tablename="PROC_采购申请2"
                tablenamedetail="PROC_采购申请2_ITEM" ReadOnly="true" runat="server"></ui:userinfo>
        </div>
        <div class="row">
            <table class="table table-condensed table-bordered">
                <tr>
                    <td class="banner" colspan="4">
                        详细信息
                    </td>
                </tr>
					 <tr>
						<td class="td-label">
							Applicant：
						</td>
						<td class="td-content">
							<asp:Label ID="read_Applicant" runat="server" CssClass="validate[ustom[number]]" />
						</td>
						<td class="td-label">
							RequestDate：
						</td>
						<td class="td-content">
							<asp:Label ID="read_RequestDate" runat="server" CssClass="validate[ustom[number]]" />
						</td>
					 </tr>
					 <tr>
						<td class="td-label">
							Amount：
						</td>
						<td class="td-content">
							<asp:Label ID="read_Amount" runat="server" CssClass="validate[ustom[number]]" />
						</td>
						<td class="td-label">
							Raason：
						</td>
						<td class="td-content">
							<asp:Label ID="read_Raason" runat="server" CssClass="validate[ustom[number]]" />
						</td>
					 </tr>
					 <tr>
						<td class="td-label">
							NextApprover：
						</td>
						<td class="td-content">
							<asp:Label ID="read_NextApprover" runat="server" CssClass="validate[ustom[number]]" />
						</td>
					<td class="td-label">
						</td>
						<td class="td-content">
						</td>
					 </tr>
				<tr>
                    <td colspan="10">
                        <table class="table table-condensed table-bordered tablerequired">
                            <tr>
                                <th class="hidden">
                                    FORMID
                                </th>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Qty
                                </th>
                                <th>
                                    Price
                                </th>
                                <th>
                                    SubAmount
                                </th>
                            </tr>
                            <asp:Repeater ID="read_detail_PROC_采购申请2_Item" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="hidden">
                                            <asp:TextBox ID="read_FORMID" Text='<%#Eval("FORMID") %>' runat="server"/>
                                        </td>
                                        <td>
								<asp:Label ID="read_Name" runat="server" 
                                                Text='<%#Eval("Name")%>'/>
                                        </td>
                                        <td>
								<asp:Label ID="read_Qty" runat="server" 
                                                Text='<%#Eval("Qty")%>'/>
                                        </td>
                                        <td>
								<asp:Label ID="read_Price" runat="server" 
                                                Text='<%#Eval("Price")%>'/>
                                        </td>
                                        <td>
								<asp:Label ID="read_SubAmount" runat="server" 
                                                Text='<%#Eval("SubAmount")%>'/>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>

            </table>
        </div>
        <div class="row">
            <attach:attachments id="Attachments1" runat="server" readonly="true"></attach:attachments>
        </div>
        <div class="row">
            <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
        </div>
        <div class="row">
        </div>
    </div>
    <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
    <div style="display: none;">
        
    </div>
    </form>
</body>
</html>
