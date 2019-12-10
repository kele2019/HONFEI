<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToDoList.aspx.cs" Inherits="Ultimus.UWF.Home2.ToDoList" %>
<%@ Import Namespace="Ultimus.UWF.Home2.Code" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--<link rel="Stylesheet" type="text/css" href="css/list.css" />--%>
 
     <script src="../../Assets/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript"  src="js/jquery.min.js"></script>
    <script type="text/javascript"  src="js/jquery-ui.js"></script>

        <link href="css/workflow.css" rel="stylesheet" type="text/css" />
    <%--<script src="js/iframescroll.js"></script>--%>
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
//            $("#txtDateStart").datepicker();
//            $("#txtDateEnd").datepicker();
//            $("#txtDateStart").datepicker("option", "dateFormat", "yy-mm-dd");
//            $("#txtDateEnd").datepicker("option", "dateFormat", "yy-mm-dd");

            //var DBRWStr = parent.document.getElementById("td_DBRW").innerHTML;
            var DBRWStr = $(window.parent.$("#td_DBRW"));
            var StrIndex = $(DBRWStr).html().indexOf('(') - 0;
            var Comstr = "";
            if (StrIndex > 0) {
                Comstr = $(DBRWStr).html().substr(0, StrIndex);
            }
            else {
                Comstr = $(DBRWStr).html();
            }
            var trCount = $("#txtType").val();
            if (trCount != "0") {
                $(DBRWStr).html(Comstr + "(" + trCount + ")");
            }
            else {
               
                $(DBRWStr).html(Comstr);
            }


            var DBCGX = $(window.parent.$("#td_CGX"));
            var StrIndex = $(DBCGX).html().indexOf('(') - 0;
            var Comstr = "";
            if (StrIndex > 0) {
                Comstr = $(DBCGX).html().substr(0, StrIndex);
            }
            else {
                Comstr = $(DBCGX).html();
            }
            var trCount = $("#hiddraftcount").val();
            if (trCount != "0") {
                $(DBCGX).html(Comstr + "(" + trCount + ")");
            }
            else {

                $(DBCGX).html(Comstr);
            }
            


        });

        function initDatePicker_zh() {
            $.datepicker.regional['zh-CN'] = {
                closeText: 'Close',
                prevText: '<Last Month',
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
            //Trele.parentNode.parentNode.style.display = 'none';
            location.href = "../Ultimus.UWF.Home2/ToDoList.aspx?Type=mytask";
        }
        function LoadAssignTask() {
            var sheight = 300; // screen.height - 150;
            var swidth = 600;// screen.width - 10;
            var winoption = "left=500px,top=300px,height=" + sheight + ",width=" + swidth + ",toolbar=no,menubar=no,location=no,status=no,scrollbars=no,resizable=yes";
            window.open("../Ultimus.UWF.Workflow/Assign.aspx", "", winoption);
        }

        function RecallAssignTask() {
            var sheight = 600; // screen.height - 150;
            var swidth = 1000; // screen.width - 10;
            var winoption = "left=500px,top=300px,height=" + sheight + ",width=" + swidth + ",toolbar=no,menubar=no,location=no,status=no,scrollbars=yes,resizable=yes";
            window.open("../Ultimus.UWF.Workflow/AssignmentList.aspx", "", winoption);
        }
         
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="tBar" style="display:none">
            <table cellpadding="0" cellspacing="0" border="0" width="100%" style="table-layout: fixed;">
                <tr>
                    <td width="40px" align="right">
                        <%=LanguageHelper.Get("Text_ToolBar_Flow")%>
                    </td>
                    <td width="190px">
                    </td>
                    <td width="80px" align="right">
                        <%--<%=LanguageHelper.Get("Text_ToolBar_Title")%>--%>
                        No.
                    </td>
                    <td width="130px">
                    </td>
                    <td width="70px" align="right">
                        <%=LanguageHelper.Get("Text_ToolBar_Date")%>
                    </td>
                    <td width="210px">
                        -
                    </td>
                    <td>
                       
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>

           <div class="contant_list">
                 
        <%--     <div style="padding-bottom:10px;">
            <input type="button" value="Assign"  class="inputbtn" onclick="LoadAssignTask()" />
            <input type="button" value="RecallAssign"  class="inputbtn" onclick="RecallAssignTask()" />
             </div>--%>
        <div class="screenbox" >
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
        <asp:ListItem Value="2">Completed</asp:ListItem>
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
        <div class="listpage_list">
            <table  border="0" cellspacing="0" cellpadding="0" class="listtable">
                    <tr >
                        <td class="type" width="5%"><%--<%=LanguageHelper.Get("Text_GridHeader_No")%>--%>Item</td>
                        <td width="10%" class="type"><%=LanguageHelper.Get("Text_ToolBar_Monitor")%></td>
                        <td class="type" width="20%">No.</td>
                        <td class="type"><%=LanguageHelper.Get("Text_GridHeader_Title")%></td>
                        <td class="type" width="15%" style="text-align:center"><%=LanguageHelper.Get("Text_GridHeader_CurrentStep")%></td>
                       <%-- <td class="type"><%=LanguageHelper.Get("Text_GridHeader_Flow")%></td>--%>
                       
                        <%--<td width="40px" class="type"><%=LanguageHelper.Get("Text_GridHeader_RequestNo")%></td>--%>
                        <td class="type" width="10%" style="text-align:center"><%--<%=LanguageHelper.Get("Text_GridHeader_SendBy")%>--%>Sender</td>
                        <td class="type" width="12%" style="text-align:center"><%=LanguageHelper.Get("Text_GridHeader_SendDate")%></td>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                               <td align="center" style="text-align:center"><%#AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + Container.ItemIndex + 1%></td>
                               <td  style="text-align:center">   <a href="../Ultimus.UWF.workflow/TaskStatus.aspx?ProcessName=<%#Server.UrlEncode(Eval("PROCESSNAME").ToString().Trim()) %>&Incident=<%#Eval("INCIDENT") %>"
                                target="_blank" style="color:#8b8b89"  ><img alt="Monitor" src="images/control.png"  />
                              </a>  
                              </td>
                               <td style="text-align:center" >
                                 <a onclick="javascript:openForm('<%#Eval("TaskID") %>','<%=Request.QueryString["Type"] %>',this);"
                                style="cursor: hand;color:Blue;" href="#">
                              <%#Eval("SUMMARY").ToString().IndexOf("◆") > 0 ? Eval("SUMMARY").ToString().Split('◆')[0] : Eval("SUMMARY")%></a></td>
                               <td style="text-align:center"><div class="dot"><%#UIHelper.ChangeProcessName(Eval("PROCESSNAME").ToString())%></div></td>
                               <td style="text-align:center" ><%#Eval("STEPLABEL")%></td>
                              
                               <%--
                               <td  >
                                 <a onclick="javascript:openForm('<%#Eval("TaskID") %>','<%=Request.QueryString["Type"] %>',this);"
                                style="cursor: hand; color:Blue;" href="#">
                              <%#Eval("SUMMARY").ToString().IndexOf("◆")>0?Eval("SUMMARY").ToString().Split('◆')[1]:""%></a></td>--%>
                                
                                
                                <td class="dot" style="text-align:center"><%#Eval("INITIATOR").ToString().IndexOf('/') > 0 ? Eval("INITIATOR").ToString().Split('/')[1] : ""%></td>
                                <td style="text-align:center"><%#Eval("STARTTIME", "{0:yyyy-MM-dd}")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="plcEmpty" runat="server" Visible="false">
                        <tr>
                            <td colspan="7" align="center">
                                <%=LanguageHelper.Get("Grid_EmptyData_Text")%>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
               
            </table>
        </div>
        <div  class="pagelist"  style="border:0">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="Count %RecordCount%"
                Width="100%" CurrentPageButtonClass="pageBtn" OnPageChanged="AspNetPager1_PageChanged"
                AlwaysShow="true" PageSize="10" CssClass="dPager">
            </webdiyer:AspNetPager>
        </div>
        <div style="display:none">
        <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hiddraftcount" />
        </div>
     
        </div>
        
    </form>
</body>
</html>
