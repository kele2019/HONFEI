<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Approval.aspx.cs" Inherits="BusinessTrip.Approval" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>BusinessTrip</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <ui:userinfo id="UserInfo1" processtitle="BusinessTrip" processprefix="FP" tablename="PROC_BUSINESSTRIP"
                tablenamedetail="PROC_BUSINESSTRIP_ITEM,PROC_BUSINESSTRIP_DESC" ReadOnly="true" runat="server"></ui:userinfo>
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
							Destination：
						</td>
						<td class="td-content">
							<asp:Label ID="read_Destination" runat="server" CssClass="validate[ustom[number]]" />
						</td>
						<td class="td-label">
							Reason：
						</td>
						<td class="td-content">
							<asp:Label ID="read_Reason" runat="server" CssClass="validate[ustom[number]]" />
						</td>
					 </tr>
					 <tr>
						<td class="td-label">
							RequestDate：
						</td>
						<td class="td-content">
							<asp:Label ID="read_RequestDate" runat="server" CssClass="validate[ustom[number]]" />
						</td>
						<td class="td-label">
							Applicant：
						</td>
						<td class="td-content">
							<asp:Label ID="read_Applicant" runat="server" CssClass="validate[ustom[number]]" />
						</td>
					 </tr>
					 <tr>
						<td class="td-label">
							Budget：
						</td>
						<td class="td-content">
							<asp:Label ID="read_Budget" runat="server" CssClass="validate[ustom[number]]" />
						</td>
						<td class="td-label">
							Url：
						</td>
						<td class="td-content">
							<asp:Label ID="read_Url" runat="server" CssClass="validate[ustom[number]]" />
						</td>
					 </tr>
					 <tr>
						<td class="td-label">
							Days：
						</td>
						<td class="td-content">
							<asp:Label ID="read_Days" runat="server" CssClass="validate[ustom[number]]" />
						</td>
						<td class="td-label">
							aaaa：
						</td>
						<td class="td-content">
							<asp:Label ID="read_aaaa" runat="server" CssClass="validate[ustom[number]]" />
						</td>
					 </tr>
					 <tr>
						<td class="td-label">
							Users：
						</td>
						<td class="td-content">
							<asp:Label ID="read_Users" runat="server" CssClass="validate[ustom[number]]" />
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
                                    SumAmount
                                </th>
                                <th>
                                    ItemDate
                                </th>
                                <th>
                                    bbb
                                </th>
                            </tr>
                            <asp:Repeater ID="read_detail_PROC_BusinessTrip_Item" runat="server">
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
								<asp:Label ID="read_SumAmount" runat="server" 
                                                Text='<%#Eval("SumAmount")%>'/>
                                        </td>
                                        <td>
								 <asp:Label ID="read_ItemDate" runat="server" 
                                                Text='<%#MyLib.ConvertUtil.ToShortDateString(Eval("ItemDate"))%>'/>
                                        </td>
                                        <td>
								<asp:Label ID="read_bbb" runat="server" 
                                                Text='<%#Eval("bbb")%>'/>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
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
                                    Description
                                </th>
                            </tr>
                            <asp:Repeater ID="read_detail_PROC_BusinessTrip_Desc" runat="server">
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
								<asp:Label ID="read_Description" runat="server" 
                                                Text='<%#Eval("Description")%>'/>
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
