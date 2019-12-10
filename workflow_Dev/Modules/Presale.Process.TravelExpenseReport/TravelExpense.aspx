<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravelExpense.aspx.cs" Inherits="Presale.Process.TravelExpenseReport.TravelExpense" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p style="text-align:center">Travel Expense Report</p>
        <table class="table table-condensed table-bordered">
            <tr>
                <td>
                    <p>Bussiness Purpose:</p>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="BussinessPurpose"></asp:TextBox>
                </td>
                <td>
                    <p>Total(RMB):</p>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="RMB"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <p>Employee Name:</p>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="EmployeeName"></asp:TextBox>
                </td>
                <td>
                    <p>Paid By Company:</p>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="PaidByCompany"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <p>Employee ID:</p>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="Employee"></asp:TextBox>
                </td>
                <td>
                    <p>Paid By Employee:<p>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="PaidByEmployee"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <p>Cost Center:<p>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="CostCenter"></asp:TextBox>
                </td>
                <td>
                    <p>Rate:<p>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="Rate"></asp:TextBox>
                </td>
            </tr>
            </table>
            <table class="table table-condensed table-bordered">
            <tr>
                <th width="2">NO.</th>
                <th width="9.8">Location</th>
                <th width="9.8">Date</th>
                <th width="9.8">Meal</th>
                <th width="9.8">BM/E</th>
                <th width="9.8">Airfare</th>
                <th width="9.8">Hotel</th>
                <th width="9.8">T/B</th>
                <th width="9.8">Laundry</th>
                <th width="9.8">TT</th>
                <th width="9.8">Others</th>
            </tr>
            <tbody id="detail">
                    <asp:Repeater runat="server" ID="travelDitail">
                        <ItemTemplate>
                            <tr>
                                <tr>
                                    <asp:Label ID="ID" runat="server"></asp:Label>
                                </tr>
                                <td>
                                    <asp:Label ID="Location" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Date" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Meal" runat="server"></asp:Label>
                                    <asp:Label ID="CurrencyMeal" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="BME" runat="server"></asp:Label>
                                    <asp:Label ID="CurrencyBME" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Airfare" runat="server"></asp:Label>
                                    <asp:Label ID="CurrencyAir" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Hotel" runat="server"></asp:Label>
                                    <asp:Label ID="CurrencyHotel" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="TB" runat="server"></asp:Label>
                                    <asp:Label ID="CurrencyTB" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Laundry" runat="server"></asp:Label>
                                    <asp:Label ID="CurrencyLau" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="TT" runat="server"></asp:Label>
                                    <asp:Label ID="CurrencyTT" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Others" runat="server"></asp:Label>
                                    <asp:Label ID="CurrencyOther" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
