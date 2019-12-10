<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTAndDayOffReport.aspx.cs" Inherits="Ultimus.UWF.Report.OTAndDayOffReport1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>OT And DayOff Report</title>
    <script src="/Assets/js/common.js" type="text/javascript"></script>
    <script src="JS/jquery.min.js" type="text/javascript"></script>
    <script src="jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="JS/jquery.table2excel.js" type="text/javascript"></script>
    <script type="text/javascript">
        function getSelectBy_onclick(index) {
            if (index == "1") {
                $("#select").val("dept");
            }
            if (index == "2") {
                $("#select").val("employee");
            }
        }
        function ToggleBudegetDetail(obj) {

            if ($(obj).attr("value") == "" || $(obj).attr("value") == undefined) {
                $(obj).attr("value", "1");
                $(obj).attr("src", "/Assets/images/icon_t_o1.gif");
                $(obj).parent().parent().next().show("slow");
            }
            else {
                $(obj).attr("value", "");
                $(obj).attr("src", "/Assets/images/icon_t_o3.gif");
                $(obj).parent().parent().next().hide("slow");
            }
        }
        $(document).ready(function () {
            $("#btnExport").click(function () {
                $("#ExcelTable").table2excel({
                    exclude: ".noExl", //过滤位置的 css 类名
                    filename: "OT And DayOff Info" + new Date().getTime() + ".xls", //文件名称
                    name: "Excel Document Name.xlsx",
                    exclude_img: true,
                    exclude_links: true,
                    exclude_inputs: true

                });
            });
           var SelectYear=$("#DropYear").val();
            $("#OTTH").html(SelectYear+"OT (hour)");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" style="margin-left:20%;" align="center">
    <div style="width:80%;">
        <div class="row">

        <h3>OT And DayOff Report</h3>

            <table class="table table-condensed table-bordered" >
                <tr>
                    <td class="td-label">Dept</td>
                    <td class="td-content"><asp:DropDownList runat="server" 
                            ID="dropDepartment" 
                            onselectedindexchanged="dropDepartment_SelectedIndexChanged" 
                            AutoPostBack="True" ></asp:DropDownList></td>
                    <td class="td-label" >Employee Name</td>
                    <td class="td-content"><asp:DropDownList ID="dropEmployee" runat="server"></asp:DropDownList></td>
                </tr>
               <%-- <tr>
                    <td class="td-label" >From</td>
                    <td>
                        <asp:TextBox runat="server" ID="from" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'to\')}'})"></asp:TextBox>
                    </td>
                    <td class="td-label" >To</td>
                    <td>
                        <asp:TextBox runat="server" ID="to" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',minDate:'#F{$dp.$D(\'from\')}'})"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td class="td-label" >Year</td>
                    <td class="td-content">
                    <asp:DropDownList ID="DropYear" runat="server"></asp:DropDownList>
                    </td>

                    <td style="text-align:center" colspan="2">
                        <asp:Button runat="server" CssClass="btn" ID="btnSearch" Text="Search" onclick="btnSearch_Click" style="float:right;margin-left:10px;" />
                    <input type="button" value="ExportExcel" class="btn" id="btnExport" style="float:right" />   
                    </td>
                 </tr>
            </table>
     </div>
     <div class="row" >
                    <table class="table table-condensed table-bordered tablerequired" style="width:100%" id="tbDetail">
                        <tr>
                        <th style="width:5%">NO.</th>
                        <th style="width:12%">Employee Name</th>
                        
                        <th style="width:15%">Department Name</th>
                         <th style="width:14%">Last year Initial OT(hour)</th>
                        <th style="width:14%">Last Year Remaind OT(hour)</th>
                        <th  style="width:14%" id="OTTH">OT (hour)</th>
                        <th  style="width:14%">DayOff (hour)</th>
                        <th  style="width:14%" >Remaind OT (hour)</th>
                       </tr>
                       <asp:Repeater runat="server" ID="rpList">
                       <ItemTemplate>
                       <tr>
                        <td style="text-align:center">
                          <img src="/Assets/images/icon_t_o3.gif" onclick="ToggleBudegetDetail(this)" />
                         <%# Container.ItemIndex+1%>
                         </td>
                       <td style="text-align:center">
                       <%#Eval("EXT04")%>
                       </td>
                       <td style="text-align:center">
                       <%#Eval("DEPARTMENTNAME")%>
                       </td>
                        <td style="text-align:center">
                       <%#Eval("TotalHour")%>
                       </td>
                       <td  style="text-align:center"> 
                        <%# String.IsNullOrEmpty(Eval("LastYearHourCount").ToString()) ? "0" : Eval("LastYearHourCount")%>
                       </td>
                        <td style="text-align:center">
                       <%# String.IsNullOrEmpty(Eval("SumHour").ToString()) ? "0" : Eval("SumHour")%>
                       </td>
                       <td style="text-align:center">
                       <%# String.IsNullOrEmpty(Eval("DayOff").ToString()) ? "0" : Eval("DayOff")%>
                       </td>
                       <td style="text-align:center">
                       <%# String.IsNullOrEmpty(Eval("RemainOT").ToString()) ? "0" : Eval("RemainOT")%>
                       </td>
                       </tr>
                        
                  <tr class="trLeaveinfo" style="display:none">
                 <td colspan="8">
                 <asp:HiddenField runat="server" ID="hdUserID" Value='<%#Eval("UserAccount") %>' />
                 <table class="table table-condensed table-bordered">
                 <tr>
                 <th>No.</th>
                 <th>DocumentNo</th>
                 <th>Requst Date</th>
                 <th>Applying for</th>
                 <th>Date</th>
                 <th>Sum Hours</th>
                 </tr>
                 <tbody class="bodyleaveinfo">
                 <asp:Repeater runat="server" ID="RPOTDayOffDetail">
                 <ItemTemplate>
                 <tr>
                   <td><%# Container.ItemIndex+1%></td>
                    <td><%#Eval("DOCUMENTNO")%></td>
                 <td><%#Convert.ToDateTime(Eval("REQUESTDATE").ToString()).ToShortDateString()%></td>
                 <td class="Applying"><%#Eval("Applying") %></td>
                 <td><%#Eval("CreateDate")%></td>
                 <td class="SumHour"><%#Eval("SumHour")%></td>
                 </tr>
                </ItemTemplate>
                 </asp:Repeater>
                   </tbody>
                 </table>
                 </td>
                 </tr>
                       </ItemTemplate>
                       </asp:Repeater>
                      <%-- <tfoot>
                    <tr>
                    <td colspan="6">
                      <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="Count %RecordCount%"
                        horizontalalign="right" width="100%" cssclass="aspNetPager" onpagechanged="AspNetPager1_PageChanged"
                        pagesize="10" alwaysshow="true" submitbuttonstyle="display:none" inputboxstyle="display:none"
                        nextpagetext="Next" firstpagetext="Home" lastpagetext="Last" prevpagetext="Prev">
                    </webdiyer:aspnetpager>
                    </td>
                    </tr>
                    </tfoot>--%>
                       </table>
                       </div>


  <div class="row" style="display:none" >
         <table class="table table-condensed table-bordered" id="ExcelTable">
                 <tr>
                 <th>No.</th>
                   <th>Requestor</th>
                 <th>DocumentNo</th>
                 <th>Requst Date</th>
                 <th>Applying for</th>
                 <th>Date</th>
                 <th>Sum Hours</th>
                 </tr>
                 <tbody class="bodyleaveinfo">
                 <asp:Repeater runat="server" ID="RPOTDayOffDetail">
                 <ItemTemplate>
                 <tr>
                   <td><%# Container.ItemIndex+1%></td>
                    <td><%#Eval("APPLICANT")%></td>
                    <td><%#Eval("DOCUMENTNO")%></td>
                 <td><%#Convert.ToDateTime(Eval("REQUESTDATE").ToString()).ToShortDateString()%></td>
                 <td class="Applying"><%#Eval("Applying") %></td>
                 <td><%#Eval("CreateDate")%></td>
                 <td class="SumHour"><%#Eval("SumHour")%></td>
                 </tr>
                </ItemTemplate>
                 </asp:Repeater>
                   </tbody>
                 </table>

</div>

    </div>
    </form>
</body>
</html>