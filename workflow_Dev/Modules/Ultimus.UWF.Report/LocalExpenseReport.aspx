<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalExpenseReport.aspx.cs" Inherits="Ultimus.UWF.Report.LocalExpenseReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style media="print" type="text/css"> 
         .noprint{visibility:hidden} 
    </style> 
    <script type="text/javascript">
        function Print_click() {
            window.print();
        }
        function window_close() {
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="text-align:center;font-family:Helvetica Neue, Helvetica, Arial, sans-serif;font-size:12px;">
     <div class="row" style="width:90%;">
          <asp:TextBox runat="server" ID="formNumber" style="display:none;"></asp:TextBox>
          <asp:TextBox runat="server" ID="incidents" style="display:none;"></asp:TextBox>
          <div>
          <table align="center" style="width:100%;margin-top:0.5%;margin-bottom:0.5%;"  >
            <tr>
                <td >
                    <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/img/logo.gif" Height="30px" Visible="true" />
                </td>
                <td style="text-align: center; padding-top: 10px; " width="90%">
                    <strong>
                        <asp:Label ID="lblProcessName" Font-Size="20px" runat="server" Text="Local Expense Report"></asp:Label>
                    </strong>
                </td>
                <td align="right" style="padding-left:20;" width="250">
                    <asp:Literal ID="barcode" runat="server" Visible="false"  ></asp:Literal>
                </td>
            </tr>
          </table>
          </div>
          <hr />
          <table align="center">
            <tr>
                <td width="40%" style="text-align:left;padding-bottom:0;">
                    <p>Employee Name:<span><asp:Label runat="server" ID="EmployeeName"></asp:Label></span></p>
                </td>
                <td width="7%" style="padding-bottom:0;"></td>
                <td width="30%" style="text-align:left">
                    <p>Employee ID:<asp:Label runat="server" ID="Employee"></asp:Label></p>
                </td>
                <td width="23%"></td>
            </tr>
            <tr>
                 <td style="text-align:left" colspan="4">
                    <p>Cost Center:<asp:Label runat="server" ID="CostCenter"></asp:Label></p>
                </td>
            </tr>
            <tr>
                <td style="text-align:left">
                    <p>Project:<asp:Label runat="server" ID="Project"></asp:Label></p>
                </td>
                <td></td>
                <td style="text-align:left">
                    <p>Sub-total:<asp:Label runat="server" ID="Total"></asp:Label></p>
                </td>
                <td></td>
            </tr>
          </table>
          <table>
                <tr>
                    <th width="20%">ApproveName</th>
                    <th width="35%" colspan="2">Comments</th>
                    <th width="20%">ApproveDate</th>
                    <th width="25%">Signature</th>
                </tr>
                <tbody id="log">
                    <asp:Repeater runat="server" ID="logDetail">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("EXT04")%></p>
                                </td>
                                <td style="text-align:center" colspan="2">
                                    <p style="text-align:center"><%#Eval("COMMENTS")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Convert.ToDateTime(Eval("CREATEDATE")).ToString("yyyy/MM/dd")%></p>
                                </td>
                                <td style="text-align:center">
                                    <img style="width:80px;height:50px;" alt='<%# Eval("Signature")%>' src="/Modules/Ultimus.UWF.Form.ProcessControl/img/<%# Eval("Signature").ToString() %>.png" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
          <hr />
          <table>
                <tr>
                    <th width="4%">NO.</th>
                    <th width="14%">Date</th>
                    <th width="24%">Description</th>
                    <th width="44%">Comments</th>
                    <th width="14%">Amount</th>
                </tr>
                <asp:Repeater runat="server" ID="LocalExpDitail">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center">
                                <p style="text-align:center"><%#Eval("ROWID")%></p>
                            </td>
                            <td style="text-align:center">
                                <p style="text-align:center"><%#Convert.ToDateTime(Eval("Date")).ToString("yyyy/MM/dd")%></p>
                            </td>
                            <td style="text-align:center">
                                <p style="text-align:center"><%#Eval("SubjectName")%></p>
                            </td>
                            <td style="text-align:center">
                                <p style="text-align:center"><%#Eval("Comments")%></p>
                            </td>
                            <td style="text-align:center">
                                <p style="text-align:center"><%#Eval("RMB")%></p>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
    </div>
                                
    </form>
    <div style="text-align:center" class="noprint">
    <input type="button" class="btn" value="Close" onclick="window_close()" />
    <input type="button" class="btn" value="Print" onclick="Print_click()" />
    </div>
</body>
</html>
