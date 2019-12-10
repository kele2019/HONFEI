<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentReport.aspx.cs" Inherits="Presale.Process.PaymentRequestForm.PaymentReport" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Report</title>
     <script type="text/javascript" src="/Assets/js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <div id="myDiv" class="container" style="width:1500px;">
    
     <div class="row">
       <table align="center" style="width:100%;margin-top:0.5%;margin-bottom:0.5%;"  >
       <tr>
        <td width="30%" >
            <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/img/logo.gif" Height="50px"
                Visible="true" />
        </td>
        <td style="text-align: center; padding-top: 10px; " width="450">
            <h3> Payment Report  </h3>
        </td>
        <td align="right" style="padding-left:20;" width="250">
        </td>
    </tr>
    </table>
    </div>

    
      <table class="table table-condensed table-bordered">
       <tr>
       <td  class="td-label">RequstDate:</td>
       <td class="td-content">
        <asp:TextBox runat="server" ID="txtStartDate" Width="40%"  onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox> to
        <asp:TextBox runat="server" ID="txtEndDate" Width="40%"   onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
       </td>
        <td  class="td-label">Vendor:</td>
       <td class="td-content">
        <asp:TextBox runat="server" ID="txtVenderinfo"></asp:TextBox> 

       
       </td>
       </tr>
       <tr>
         <td  class="td-label">CONFIDENTIAL:</td>
       <td class="td-content">
       <asp:DropDownList runat="server" ID="dropConfidential">
       <asp:ListItem Value="">--Pls select--</asp:ListItem>
       <asp:ListItem Value="0">No</asp:ListItem>
       <asp:ListItem Value="1">Yes</asp:ListItem>
       </asp:DropDownList>
       </td>
       <td colspan="2">
        <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
       </td>
       </tr>
       
       </table>

    
     <div class="row">
        <table class="table table-condensed table-bordered">
       <%--  <tr>
        <td class="banner" colspan="15">Payment Report</td>
        </tr>--%>
        <thead>
        <tr>
        <th>No</th>
        <th>Document No.</th>
        <th style="width:200px">Payment Desc</th>
        <th>Applicant</th>
        <th>Request Date</th>
        <th>Vendor Code</th>
        <th style="width:200px">Vendor</th>
        <th>Currency</th>
        <th>Downpayment</th>
        <th>Total Invoice Amount</th>
     <%--   <th>Total Payment</th>--%>
        <th>Contract Number</th>
        <th>PO Number</th>
        <th>CONFIDENTIAL</th>
        <th style="width:200px">Remark</th>
        </tr>
        </thead>
        
        <tbody>
        <asp:Repeater runat="server" ID="RPlist">
        <ItemTemplate>
       <tr>
       <td><%#Container.ItemIndex + 1 %></td>
       <td><a href='Approval.aspx?Incident=<%#Eval("INCIDENT") %>&Type=myapproval&ProcessName=Payment%20Request' target="_blank"><%#Eval("DOCUMENTNO")%></a></td>
       <td><%#Eval("PaymentDescription")%></td>
       <td><%#Eval("APPLICANT")%></td>
       <td><%#Eval("REQUESTDATE")%></td>
       <td><%#Eval("vendorcode")%></td>
       <td><%#Eval("vendorname")%></td>
       <td><%#Eval("Currency")%></td>
       <td><%#Eval("DownPaymentAmount")%></td>
       <td><%#Eval("totalamountofpayment")%></td>
       <td><%#Eval("Contract")%></td>
       <td><%#Eval("PO") %></td>
       <td><%#Eval("Emergency").ToString()=="1"?"Y":"N"%></td>
       <td><%#Eval("Remark")%></td>
       </tr>
        </ItemTemplate>
        </asp:Repeater>
        </tbody>
         <tfoot>
                    <tr>
                    <td colspan="15">
                      <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="Count %RecordCount%"
                        horizontalalign="right" width="100%" cssclass="aspNetPager" onpagechanged="AspNetPager1_PageChanged"
                        pagesize="30" alwaysshow="true" submitbuttonstyle="display:none" inputboxstyle="display:none"
                        nextpagetext="Next" firstpagetext="Home" lastpagetext="Last" prevpagetext="Prev">
                    </webdiyer:aspnetpager>
                    </td>
                    </tr>
         </tfoot>


        </table>
        </div>



    </div>
    </form>
</body>
</html>
