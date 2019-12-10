<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="Ultimus.UWF.Workflow.History" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=GB2312" />
    <style type="text/css">
        table
        {
            font-size: 12px;
	        font-family:����,΢���ź�;
	        
        }
        td{padding:3px;}
    </style>
</head>
<body style="padding-top:0px;">
    <form id="form1" runat="server">
    <div>
        <asp:gridview id="gvApproval" runat="server" autogeneratecolumns="False" onrowdatabound="gvApproval_RowDataBound"
            >
            <HeaderStyle Height="23" BackColor="#DBEAED" Font-Bold="true" />
            <RowStyle BorderColor="#DBEAED" Font-Size="12px" BorderWidth="1" BorderStyle="Double" />
            <Columns>
                <asp:TemplateField ItemStyle-Width="40px">
                    <HeaderTemplate>���</HeaderTemplate>
                    <ItemTemplate> <%#Container.DataItemIndex+1%></ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="120px">
                    <HeaderTemplate>��������</HeaderTemplate>
                    <ItemTemplate> <%#Eval("stepname") %></ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField  ItemStyle-Width="140px">
                    <HeaderTemplate>����ʱ��</HeaderTemplate>
                    <ItemTemplate> <%#Eval("createdate") %></ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField  ItemStyle-Width="80px">
                    <HeaderTemplate>������</HeaderTemplate>
                    <ItemTemplate> <%#Eval("approvername")%></ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField  ItemStyle-Width="150px">
                    <HeaderTemplate>����״̬</HeaderTemplate>
                    <ItemTemplate> <%#Eval("action") %></ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField  ItemStyle-Width="300px" FooterStyle-Wrap="true">
                    <HeaderTemplate>�������</HeaderTemplate>
                    <ItemTemplate> <%#Eval("comments").ToString().Replace("\r\n","<br/>") %></ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <table cellspacing="0" rules="all" border="1" id="gvApproval" style="border-collapse: collapse;">
                    <tr style="background-color: #DBEAED; font-weight: bold; height: 23px;">
                        <th scope="col">
                            ���
                        </th>
                        <th scope="col">
                            ��������
                        </th>
                        <th scope="col">
                            ����ʱ��
                        </th>
                        <th scope="col">
                            ������
                        </th>
                        <th scope="col">
                            ����״̬
                        </th>
                        <th scope="col">
                            �������
                        </th>
                    </tr>
                    <tr style="border-color: #DBEAED; border-width: 1px; border-style: Double; font-size: 12px;">
                        <td align="center" style="width: 40px;">
                             
                        </td>
                        <td align="center" style="width: 120px;">
                             
                        </td>
                        <td align="center" style="width: 140px;">
                             
                        </td>
                        <td align="center" style="width: 80px;">
                             
                        </td>
                        <td align="center" style="width: 150px;">
                             
                        </td>
                        <td align="center" style="width: 300px;">
                        </td>
                    </tr>
                </table>

            </EmptyDataTemplate>
        </asp:gridview>
    </div>
    </form>
</body>
</html>
