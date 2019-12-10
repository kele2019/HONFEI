<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravelExpenseReport.aspx.cs" Inherits="Ultimus.UWF.Report.TravelExpenseReport2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--<script type="text/javascript" src="/Assets/js/common.js"></script>--%>
    <style media="print" type="text/css"> 
         .noprint{visibility:hidden} 
    </style> 
    <script type="text/javascript">
        function Print_click() {
////            function preview() {
//                //获取页面内容
//                var bdhtml = document.body.innerHTML;
//                var beginstr = "<!--startprint-->";
//                var endstr = "<!--endprint-->";
//                //获取要打印的内容
//                var prnhtml = bdhtml.substr(bdhtml.indexOf(beginstr) + 17);
//                prnhtml = prnhtml.substr(0, prnhtml.indexOf(endstr));
//                //预览
//                window.document.body.innerHTML = prnhtml
//                //打印
            window.print();
//            }
        }
        function window_close() {
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="text-align:center;font-family:Helvetica Neue, Helvetica, Arial, sans-serif;font-size:12px;">
    <!--startprint-->
     <div class="row" style="width:90%;">
          <asp:TextBox runat="server" ID="formNumber" style="display:none;"></asp:TextBox>
          <asp:TextBox runat="server" ID="incidents" style="display:none;"></asp:TextBox>
          <div>
          <table align="center" style="width:100%;margin-top:0.5%;margin-bottom:0.5%;"  >
            <tr>
                <td >
                    <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/img/logo.gif" Height="30px"
                        Visible="true" />
                </td>
                <td style="text-align: center; padding-top: 10px; " width="90%">
                    <strong>
                        <asp:Label ID="lblProcessName" Font-Size="20px" runat="server" Text="Travel Expense Report"></asp:Label>
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
                <td style="text-align:left" colspan="4">
                    <p>Bussiness Purpose:<span><asp:Label runat="server" ID="BussinessPurpose"></asp:Label></span></p>
                </td>
                
            </tr>
            <tr>
                <td style="text-align:left" width="40%">
                    <p>Employee Name:<span><asp:Label runat="server" ID="EmployeeName"></asp:Label></span></p>
                </td>
                <td width="7%"></td>
                <td width="30%" style="text-align:left" colspan="2">
                    <p>Paid By Company:<asp:Label runat="server" ID="PaidByCompany"></asp:Label></p>
                </td>
                <td width="23%"></td>
            </tr>
            <tr>
                <td style="text-align:left">
                    <p>Employee ID:<span><asp:Label runat="server" ID="Employee"></asp:Label></span></p>
                </td>
                <td></td>
                <td style="text-align:left" colspan="2">
                    <p>Paid By Employee:<asp:Label runat="server" ID="PaidByEmployee"></asp:Label></p>
                </td>
            </tr>
            <tr>
                <td style="text-align:left" colspan="2">
                    <p>Cost Center:<span><asp:Label runat="server" ID="CostCenter"></asp:Label></span></p>
                </td>
                <td  style="text-align:left" colspan="2">
                    <p>Total(RMB):<asp:Label runat="server" ID="RMB"></asp:Label></p>
                </td>
            </tr>
            <tr>
                <td style="text-align:left">
                    <p>Project:<asp:Label runat="server" ID="Project"></asp:Label></p>
                </td>
                <td></td>
                <td  style="text-align:left">
                    <p>Rate:<asp:Label runat="server" ID="Rate"></asp:Label></p>
                </td>
                <td></td>
            </tr>
            </table>
            <table>
                <tr>
                    <th width="20%">ApproveName</th>
                    <th width="35%">Comments</th>
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
                                    <td style="text-align:center">
                                        <p style="text-align:center"><%#Eval("COMMENTS")%></p>
                                    </td>
                                    <td style="text-align:center">
                                        <p style="text-align:center"><%#Convert.ToDateTime(Eval("CREATEDATE")).ToString("yyyy/MM/dd")%></p>
                                    </td>
                                    <td style="text-align:center">
                                        <img style="width:80px;height:50px;text-align:center" alt='<%# Eval("Signature")%>' src='/Modules/Ultimus.UWF.Form.ProcessControl/img/<%# Eval("Signature").ToString() %>.png' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                </tbody>
            </table>
            <hr />
            <table>
                <tr>
                    <td width="30%">
                        <table>
                            <tr>
                                <th width="4%">NO.</th>
                                <th width="16%">Location</th>
                                <th width="16%">Date</th>
                                <th width="16%">Description</th>
                                <th width="16%">Amount</th>
                                <th width="16%">Currency</th>
                                <th width="16%">Type</th>
                            </tr>
                           <%-- <tr>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("ROWID")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("TravelFrom")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Convert.ToDateTime(Eval("ItemValue")).ToString("yyyy/MM/dd")%></p>
                                </td>
                                <td colspan="4">
                                <asp:Repeater runat="server" ID="travelDitail">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td style="text-align:center">
                                    <p style="text-align:center">Meal</p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("Meal")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("CurrencyMeal")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("payMeal")%></p>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center">
                                    <p style="text-align:center">BM/E</p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("BME")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("CurrencyBME")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("payBME")%></p>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center">
                                    <p style="text-align:center">Airfare</p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("Air")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("CurrencyAir")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("payBME")%></p>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center">
                                    <p style="text-align:center">Hotel</p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("Hotel")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("CurrencyHotel")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("payHotel")%></p>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center">
                                    <p style="text-align:center">T/B</p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("TB")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("CurrencyTB")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("payTB")%></p>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center">
                                    <p style="text-align:center">Laundry</p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("Lau")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("CurrencyLau")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("payLau")%></p>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center">
                                    <p style="text-align:center">TT</p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("TT")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("CurrencyTT")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("payTT")%></p>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center">
                                    <p style="text-align:center">Other</p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("Others")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("CurrencyOther")%></p>
                                </td>
                                <td style="text-align:center">
                                    <p style="text-align:center"><%#Eval("payOthers")%></p>
                                </td>
                            </tr>
                    </tbody>
                </ItemTemplate>
            </asp:Repeater>
                                </td>
                            </tr>--%>
                            <asp:Label runat="server" ID="lbHmtl"></asp:Label>
                        </table>
                    </td>
                    
                </tr>
                
            </table>
    </div>
   <!--endprint-->
                                
    </form>
    <div style="text-align:center" class="noprint">
    <input type="button" class="btn" value="Close" onclick="window_close()" />
    <input type="button" class="btn" value="Print" onclick="Print_click()" />
    </div>
</body>
</html>

