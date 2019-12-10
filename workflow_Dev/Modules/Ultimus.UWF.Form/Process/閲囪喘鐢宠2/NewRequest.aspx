<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewRequest.aspx.cs" Inherits="采购申请2.NewRequest" %>
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
                tablenamedetail="PROC_采购申请2_ITEM" runat="server"></ui:userinfo>
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
								 <asp:TextBox ID="fld_Applicant" runat="server" ></asp:TextBox>
						</td>
						<td class="td-label">
							RequestDate：
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_RequestDate" runat="server" OnClick="WdatePicker({dateFmt:'yyyy-MM-dd',readOnly:true})"
                                                CssClass="Wdate validate[custom[date]]"></asp:TextBox>

							
						</td>
					 </tr>
					 <tr>
						<td class="td-label">
							Amount：
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_Amount" runat="server" CssClass="validate[ustom[number]]" ></asp:TextBox>
						</td>
						<td class="td-label">
							Raason：
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_Raason" runat="server" ></asp:TextBox>

							
						</td>
					 </tr>
					 <tr>
						<td class="td-label">
							NextApprover：
						</td>
						<td class="td-content">
								 <asp:TextBox ID="fld_NextApprover" runat="server" ></asp:TextBox>
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
                                <th>
                                    <asp:Button ID="btn_ItemAdd" runat="server" Text="增加" CssClass="btn" CausesValidation="false"
                                        OnClick="btn_ItemAdd_Click" />
                                </th>
                            </tr>
                            <asp:Repeater ID="fld_detail_PROC_采购申请2_Item" runat="server" OnItemCommand="fld_detail_PROC_采购申请2_Item_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td class="hidden">
                                            <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server"/>
                                        </td>
                                        <td>
								 <asp:TextBox ID="fld_Name" runat="server"  Text='<%#Eval("Name")%>'  Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
								 <asp:TextBox ID="fld_Qty" runat="server" CssClass="validate[ustom[number]]"  Text='<%#Eval("Qty")%>'  Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
								 <asp:TextBox ID="fld_Price" runat="server" CssClass="validate[ustom[number]]"  Text='<%#Eval("Price")%>'  Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
								 <asp:TextBox ID="fld_SubAmount" runat="server" CssClass="validate[ustom[number]]"  Text='<%#Eval("SubAmount")%>'  Width="90%"></asp:TextBox>
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
