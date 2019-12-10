<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Ultimus.UWF.Home2.List" %>
<%@ Import Namespace="Ultimus.UWF.Home2.Code" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 <html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
<%--    <link rel="Stylesheet" type="text/css" href="css/list.css" />--%>
    <link href="css/workflow.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" type="text/css" href="css/jquery-ui.css" />
    <script src="../../Assets/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery-ui.js"></script>
 <%--   <script src="js/iframescroll.js"></script>--%>
    <%
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();
    %>
    <script type="text/javascript">
        $(document).ready(function () {
            var language = '<%=LanguageHelper.Get("Language")%>';
            if (language == "zh-cn") {
                initDatePicker_zh();
            }
            var URLType = GetQueryString('Type');
            if (URLType == "myrequest") {
                $(".Sender").hide();
            }

            //            $("#txtDateStart").datepicker();
            //            $("#txtDateEnd").datepicker();
            //            $("#txtDateStart").datepicker("option", "dateFormat", "yy-mm-dd");
            //            $("#txtDateEnd").datepicker("option", "dateFormat", "yy-mm-dd");
        });
        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        } 

        function initDatePicker_zh() {
            $.datepicker.regional['zh-CN'] = {
                closeText: 'Close',
                prevText: '<LastMonth',
                nextText: 'NextMonth>',
                currentText: 'Today',
                monthNames: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                dayNames: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
                dayNamesShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
                dayNamesMin: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
                weekHeader: '',
                dateFormat: 'yy-mm-dd',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: true,
                yearSuffix: 'year'
            };
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
        }
        function openForm(taskId, type, ele) {
            var sheight = screen.height - 150;
            var swidth = screen.width - 10;
            var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
            s = window.open('../Ultimus.UWF.workflow/OpenForm.aspx?TaskId=' + taskId + '&Type=' + type + '', '', winoption);

            s.focus();
            if ($('#txtType').val() == '') {
                // ele.parentNode.parentNode.style.display = 'none';
            }
        }
        function iframeAutoFit() {

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
           <div class="contant_list">

         <div class="screenbox">
         <div class="screen">
    <dl>
      <%--<dt style="text-align:right">  <%=LanguageHelper.Get("Text_ToolBar_Flow")%></dt>--%>
      <dt>Title</dt>
       <dd>
         <asp:DropDownList ID="ddlFlow" runat="server" CssClass="search_input" style="width:180px; "></asp:DropDownList>
       </dd>
    </dl>
    <dl>
      <dt> <%--<%=LanguageHelper.Get("Text_ToolBar_Title")%>--%>No.</dt>
      <%--<dt>Step</dt>--%>
      <dd> 
        <%--<asp:DropDownList ID="ddlStep" runat="server" CssClass="search_input" style="width:180px; "></asp:DropDownList>--%>
       <asp:TextBox ID="txtTitle" runat="server" CssClass="search_input" style="width:80px;  " />
      </dd>
    </dl>

     <dl>
      <dt> Status</dt>
      <dd> 
        <asp:DropDownList ID="dropStatus" runat="server" CssClass="search_input" style="width:110px; ">
        <asp:ListItem>--Please Select--</asp:ListItem>
        <asp:ListItem Value="3">Completed</asp:ListItem>
        <asp:ListItem Value="1">No Completed</asp:ListItem>
        </asp:DropDownList>
      </dd>
    </dl>

    <dl>
      <dt><%=LanguageHelper.Get("Text_ToolBar_Date")%></dt>
      <dd class="nd">
      <asp:TextBox ID="txtDateStart" runat="server" CssClass="time_input" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'txtDateEnd\')}'})" style="width:80px;  " />
      </dd>
      <dd class="nd">-</dd>
      <dd>
       <asp:TextBox ID="txtDateEnd" runat="server" CssClass="time_input" onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtDateStart\')}'})" style="width:80px;" />
      </dd>
    </dl>
    <div class="btnbox" style="display:block"> 
     <asp:ImageButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" style="height:26px;  " />
    </div>
    <div class="clear"></div>
  </div>
        </div>


        <div  class="listpage_list">
            <table border="0" cellspacing="0" cellpadding="0" class="listtable">
                    <tr class="tblListHeaderTr">
                        <td class="type" width="5%" style="text-align:center"><%--<%=LanguageHelper.Get("Text_GridHeader_No")%>--%>Item</td>
                        <td width="10%" class="type" style="text-align:center"><%=LanguageHelper.Get("Text_ToolBar_Monitor")%></td>
                        <td class="type" width="20%">No.</td>
                        <td class="type" style="text-align:center"><%=LanguageHelper.Get("Text_GridHeader_Title")%></td>
                        <td class="type" width="15%" style="text-align:center"><%=LanguageHelper.Get("Text_GridHeader_Step")%></td>
                        <td class="type" style="display:none;"><%=LanguageHelper.Get("Text_GridHeader_Flow")%></td>
                        <td  class="type Sender" width="10%" style="text-align:center">Sender</td>
                        <td class="type" width="10%" style="text-align:center"><%--<%=LanguageHelper.Get("Text_GridHeader_CurrentBy")%>--%>Approver</td>
                        <td class="type" width="12%" style="text-align:center"><%=LanguageHelper.Get("Text_GridHeader_SendDate")%></td>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center" style="text-align:center"><%#AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + Container.ItemIndex + 1%></td>
                                 <td  style="text-align:center">   <a href="../Ultimus.UWF.workflow/TaskStatus.aspx?ProcessName=<%#Server.UrlEncode(Eval("PROCESSNAME").ToString().Trim()) %>&Incident=<%#Eval("INCIDENT") %>"
                                target="_blank"   style="color:#8b8b89">
                                <img src="images/control.png" /></a>
                              </td>
                                 <td style="text-align:center">
                                 <a onclick="javascript:openForm('<%#Eval("TaskID") %>','<%=Request.QueryString["Type"] %>',this);"
                                style="cursor: hand; color:Blue;" href="#">
                              <%#Eval("SUMMARY").ToString().IndexOf("◆") > 0 ? Eval("SUMMARY").ToString().Split('◆')[0] : Eval("SUMMARY")%></a></td>
                               <td style="text-align:center"><div class="dot"><%#UIHelper.ChangeProcessName(Eval("PROCESSNAME").ToString())%></div></td>
                              <td  style="text-align:center"><%#CurrenctUser(Eval("PROCESSNAME").ToString(), Eval("INCIDENT").ToString())==""?"Complete":Eval("STEPLABEL")%></td>
                              
                              <%--<td >
                                 <a onclick="javascript:openForm('<%#Eval("TaskID") %>','<%=Request.QueryString["Type"] %>',this);"
                                style="cursor: hand; color:Blue;" href="#">
                              <%#Eval("SUMMARY").ToString().IndexOf("◆")>0?Eval("SUMMARY").ToString().Split('◆')[1]:""%></a></td>--%>
                               
                           <td class="dot Sender"" style="text-align:center"><%#Eval("INITIATOR").ToString().IndexOf('/') > 0 ? Eval("INITIATOR").ToString().Split('/')[1] : ""%></td>
                                <td  style="text-align:center"><%#CurrenctUser(Eval("PROCESSNAME").ToString(), Eval("INCIDENT").ToString())%></td>
                                <td  style="text-align:center"><%#Eval("STARTTIME", "{0:yyyy-MM-dd}")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="plcEmpty" runat="server" Visible="false">
                        <tr>
                            <td colspan="8" align="center">
                                <%=LanguageHelper.Get("Grid_EmptyData_Text")%>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
            </table>
        </div>
        <div class="pagelist"  style="border:0">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="Count %RecordCount%"
                Width="100%" CurrentPageButtonClass="pageBtn" OnPageChanged="AspNetPager1_PageChanged"
                AlwaysShow="true" PageSize="10" CssClass="dPager">
            </webdiyer:AspNetPager>
        </div>
        <div style="display:none">
        <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
        </div>
        </div>
    </form>
</body>
</html>
