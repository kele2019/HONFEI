<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leavetest.aspx.cs" Inherits="Ultimus.UWF.Report.Leavetest" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Assets/js/common.js" type="text/javascript"></script>
    <script src="/JS/jquery.min.js" type="text/javascript"></script>
    <script src="jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="/JS/jquery.table2excel.js" type="text/javascript"></script>
    <script type="text/javascript">

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
            $(".trLeaveinfo").each(function () {
                var NoODays = 0;
                $(this).find('tbody[class="bodyleaveinfo"]').find("tr").each(function (i, Etr) {
                    var Applying = $(Etr).find(".Applying").html();
                    if (Applying != "Annual Leave" && Applying != "Full Pay Sick Leave" && Applying != "AnnualCreate" && Applying != "AnnualClear")
                        NoODays += $(Etr).find(".NoODays").html() - 0;
                });
               // console.log(NoODays);
                $(this).prev().find("td").eq(9).html(NoODays);
            });


            $("#btnExport").click(function () {
                $("#ExcelTable").table2excel({
                    exclude: ".noExl", //过滤位置的 css 类名
                    filename: "Leave Info" + new Date().getTime() + ".xls", //文件名称
                    name: "Excel Document Name.xlsx",
                    exclude_img: true,
                    exclude_links: true,
                    exclude_inputs: true

                });
            });

        });

    </script>
</head>
<body>
    <form id="form1" runat="server" style="margin-left:20%;" align="center">
    <div style="width:80%;">
      <div class="row" >

       <h3>Leave Report</h3>
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
                <tr>
                 <td class="td-label" >Year</td>
                    <td class="td-content">
                    <asp:DropDownList ID="dropYear" runat="server">
                     <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                     <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                    </asp:DropDownList></td>

                    <td style="text-align:center" colspan="2">
                        <asp:Button runat="server" CssClass="btn" ID="btnSearch" Text="Search" onclick="btnSearch_Click" style="float:right;margin-left:10px;"/>
                    <input type="button" value="ExportExcel" class="btn" id="btnExport" style="float:right" />    
                    </td>
                 </tr>
            </table >
                    <table class="table table-condensed table-bordered tablerequired" style="width:100%" id="tbDetail">
                        <tr>
                        <th style="width:6%">NO.</th>
                        <th style="width:10%">Employee Name</th>
                        <th style="width:10%">Department Name</th>
                        <th style="width:10%">This Year Annual Leave</th>
                        <th style="width:10%">Last Year Annual Leave</th>
                        <th style="width:10%">Used Annual Leave</th>
                        <th style="width:10%">Remaining last Year Annual Leave</th>
                        <th style="width:10%">Remaining Annual Leave</th>
                        <th style="width:10%">Full Pay Sick Leave</th>
                        <th  >（Personal Leave/No Pay Sick Leave）</th>
                       </tr>
                       <asp:Repeater runat="server" ID="rpList">
                       <ItemTemplate>
                        <tr>
                         <td style="text-align:center">
                          <img src="/Assets/images/icon_t_o3.gif" onclick="ToggleBudegetDetail(this)" />
                         <%# Container.ItemIndex+1%>
                         </td>
                         <td style="text-align:center">
                          <%#Eval("EXT04") %>
                         </td>
                         <td style="text-align:center">
                          <%#Eval("DEPARTMENTNAME")%>
                         </td>
                         <td style="text-align:center">
                          <%# Eval("LeaveCount")%>
                             </td>
                             <td style="text-align:center">
                           <%#Eval("CountLastLeave")%>
                             </td>

                              <td style="text-align:center">
                           <%#Eval("ALeavecount")%>
                             </td>

                                <td style="text-align:center">
                           <%#Eval("LeaveLastYearCount")%>
                             </td>

                              <td style="text-align:center">
                           <%#Eval("EnableLeaveCount")%>
                             </td>

                         <td style="text-align:center">
                            <%#Eval("FuallpaySick") %>
                             </td>
                             <td>
                             
                             </td>
                        </tr>


              <tr class="trLeaveinfo" style="display:none">
              <td colspan="10">
              <asp:HiddenField runat="server" ID="hdUserID" Value='<%#Eval("UserAccount") %>' />
              <table class="table table-condensed table-bordered">
                 <tr>
                 <th>No.</th>
                 <th>DocumentNo</th>
                 <th>Requst Date</th>
                 <th>Applying for</th>
                 <th>Date</th>
                 <th>Absence days</th>
                 <%--<th colspan="9"></th>--%>
                 </tr>
                 <tbody class="bodyleaveinfo">
                 <asp:Repeater runat="server" ID="RPLeaveDetail">
                 <ItemTemplate>
                 <tr>
                 <td><%# Container.ItemIndex+1%></td>
                 <td>
                 <a href='../Presale.Process.Leave/Approval.aspx?Incident=<%#Eval("INCIDENT") %>&Type=myapproval&ProcessName=Leave%20Application' target="_blank"><%#Eval("DOCUMENTNO")%></a>
                </td>
                 <td><%#Convert.ToDateTime(Eval("REQUESTDATE").ToString()).ToShortDateString()%></td>
                 <td class="Applying"><%#Eval("Applying") %></td>
                 <td><%#Eval("CreateDate")%></td>
                 <td class="NoODays"><%#Eval("NoODays") %></td>
                 </tr>
                 </ItemTemplate>
                 </asp:Repeater>
                   </tbody>
                 </table>
                  
              </td>
              </tr>
                 </ItemTemplate>
                       </asp:Repeater>
                       <tfoot>
                   <%-- <tr>
                    <td colspan="7">
                      <webdiyer:aspnetpager id="AspNetPager1" runat="server" custominfohtml="Count %RecordCount%"
                        horizontalalign="right" width="100%" cssclass="aspNetPager" onpagechanged="AspNetPager1_PageChanged"
                        pagesize="10" alwaysshow="true" submitbuttonstyle="display:none" inputboxstyle="display:none"
                        nextpagetext="Next" firstpagetext="Home" lastpagetext="Last" prevpagetext="Prev">
                    </webdiyer:aspnetpager>
                    </td>
                    </tr>--%>
                    </tfoot>
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
                 <th>Absence days</th>
                 </tr>
                 <tbody class="bodyleaveinfo">
                 <asp:Repeater runat="server" ID="ExcelRPLeaveDetail">
                 <ItemTemplate>
                 <tr>
                 <td><%# Container.ItemIndex+1%></td>
                 <td><%#Eval("APPLICANT")%></td>
                 <td><%#Eval("DOCUMENTNO")%></td>
                 <td><%#Convert.ToDateTime(Eval("REQUESTDATE").ToString()).ToShortDateString()%></td>
                 <td class="Applying"><%#Eval("Applying") %></td>
                 <td><%#Eval("CreateDate")%></td>
                 <td class="NoODays"><%#Eval("NoODays") %></td>
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
