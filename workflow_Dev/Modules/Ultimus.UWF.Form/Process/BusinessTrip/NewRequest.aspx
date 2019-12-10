<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewRequest.aspx.cs" Inherits="BusinessTrip.NewRequest" %>
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
                tablenamedetail="PROC_BUSINESSTRIP_ITEM,PROC_BUSINESSTRIP_DESC" runat="server"></ui:userinfo>
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
							<span class="red">*</span>
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_Destination" runat="server" CssClass="validate[required]" ></asp:TextBox>
						</td>
						<td class="td-label">
							Reason：
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_Reason" runat="server" ></asp:TextBox>

							
						</td>
					 </tr>
					 <tr>
						<td class="td-label">
							RequestDate：
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_RequestDate" runat="server" OnClick="WdatePicker({dateFmt:'yyyy-MM-dd',readOnly:true})"
                                                CssClass="Wdate validate[custom[date]]"></asp:TextBox>
						</td>
						<td class="td-label">
							Applicant：
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_Applicant" runat="server" ></asp:TextBox>

							
						</td>
					 </tr>
					 <tr>
						<td class="td-label">
							Budget：
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_Budget" runat="server" ></asp:TextBox>
						</td>
						<td class="td-label">
							Url：
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_Url" runat="server" ></asp:TextBox>

							
						</td>
					 </tr>
					 <tr>
						<td class="td-label">
							Days：
							<span class="red">*</span>
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_Days" runat="server" CssClass="validate[required,custom[number]]" ></asp:TextBox>
						</td>
						<td class="td-label">
							aaaa：
							<span class="red">*</span>
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_aaaa" runat="server" CssClass="validate[required]" ></asp:TextBox>

							
						</td>
					 </tr>
					 <tr>
						<td class="td-label">
							Users：
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_Users" runat="server" ></asp:TextBox>
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
									<span class="red">*</span>
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
                                <th>
                                    <asp:Button ID="btn_ItemAdd" runat="server" Text="增加" CssClass="btn" CausesValidation="false"
                                        OnClick="btn_ItemAdd_Click" />
                                </th>
                            </tr>
                            <asp:Repeater ID="fld_detail_PROC_BusinessTrip_Item" runat="server" OnItemCommand="fld_detail_PROC_BusinessTrip_Item_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td class="hidden">
                                            <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server"/>
                                        </td>
                                        <td>
								 <asp:TextBox ID="fld_Name" runat="server" CssClass="validate[required]"  Text='<%#Eval("Name")%>'  Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
								 <asp:TextBox ID="fld_Qty" runat="server" CssClass="validate[ustom[number]]"  Text='<%#Eval("Qty")%>'  Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
								 <asp:TextBox ID="fld_Price" runat="server" CssClass="validate[ustom[number]]"  Text='<%#Eval("Price")%>'  Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
								 <asp:TextBox ID="fld_SumAmount" runat="server" CssClass="validate[ustom[number]]"  Text='<%#Eval("SumAmount")%>'  Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
								 <asp:TextBox ID="fld_ItemDate" runat="server" OnClick="WdatePicker({dateFmt:'yyyy-MM-dd',readOnly:true})"
                                                CssClass="Wdate validate[custom[date]]"  Text='<%#MyLib.ConvertUtil.ToShortDateString(Eval("ItemDate"))%>'  Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
								 <asp:TextBox ID="fld_bbb" runat="server"  Text='<%#Eval("bbb")%>'  Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="btn" CommandName="del"
                                                ClientIDMode="Static" OnClientClick="return confirm('确认删除？')" />
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
                                <th>
                                    <asp:Button ID="btn_DescAdd" runat="server" Text="增加" CssClass="btn" CausesValidation="false"
                                        OnClick="btn_DescAdd_Click" />
                                </th>
                            </tr>
                            <asp:Repeater ID="fld_detail_PROC_BusinessTrip_Desc" runat="server" OnItemCommand="fld_detail_PROC_BusinessTrip_Desc_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td class="hidden">
                                            <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server"/>
                                        </td>
                                        <td>
								 <asp:TextBox ID="fld_Name" runat="server"  Text='<%#Eval("Name")%>'  Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
								 <asp:TextBox ID="fld_Description" runat="server"  Text='<%#Eval("Description")%>'  Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="btn" CommandName="del"
                                                ClientIDMode="Static" OnClientClick="return confirm('确认删除？')" />
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
            <attach:attachments id="Attachments1" runat="server"></attach:attachments>
        </div>
        <div class="row">
            <ah:approvalhistory id="ApprovalHistory1" showaction="false" runat="server"></ah:approvalhistory>
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
