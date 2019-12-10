<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DraftList.aspx.cs" Inherits="Ultimus.UWF.Home2.DraftList" %>
<%@ Import Namespace="Ultimus.UWF.Home2.Code" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <%-- <link rel="Stylesheet" type="text/css" href="css/list.css" />--%>
     <link href="css/workflow.css" rel="stylesheet" type="text/css" />
     <%--link rel="Stylesheet" type="text/css" href="css/jquery-ui.css" />--%>
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="js/iframescroll.js"></script>
    <script type="text/javascript">
        function delConfirm() {
            return confirm('确认删除？Delete?');
        }
//        function openForm(taskId, type, ele) {
//            var sheight = screen.height - 150;
//            var swidth = screen.width - 10;
//            var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
//            s = window.open('../Ultimus.UWF.workflow/OpenForm.aspx?TaskId=' + taskId + '&Type=' + type + '', '', winoption);
//            s.focus();
        //        }
        function openForm(taskId, processName, formId, incident, type, ele) {
            var sheight = screen.height - 150;
            var swidth = screen.width - 10;
            var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
            s = window.open('../Ultimus.UWF.workflow/OpenForm.aspx?TaskId=' + taskId + '&Type=' + type + '&ProcessName=' + escape(processName) + '&FORMID=' + formId + "&incident=" + incident, '', winoption);
            s.focus();

        }
        function iframeAutoFit() {
            location.href = "../Ultimus.UWF.Home2/DraftList.aspx";
        }
    </script>
    <%
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();
    %>
</head>
<body>
    <form id="form1" runat="server">
      <div class="contant_list" style="height:800px">
        <div class="tBar" style="display:none">
            <table cellpadding="0" cellspacing="0" border="0" width="100%" style="table-layout: fixed;">
                <tr>
                    <td width="40px" align="right">
                        <%=LanguageHelper.Get("Text_ToolBar_Flow")%>
                    </td>
                    <td width="190px">
                        <asp:DropDownList ID="ddlFlow" runat="server" CssClass="tb" style="width:180px; margin-left:8px;"></asp:DropDownList>
                    </td>
                    <td width="40px" align="right">
                        <%=LanguageHelper.Get("Text_ToolBar_Title")%>
                    </td>
                    <td width="130px">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="tb" style="width:120px; margin-left:8px;" />
                    </td>
                    <td width="70px" align="right">
                        <%=LanguageHelper.Get("Text_ToolBar_Date")%>
                    </td>
                    <td width="210px">
                        <asp:TextBox ID="txtDateStart" runat="server" CssClass="tb" style="width:80px; margin-left:8px;" />
                        -
                        <asp:TextBox ID="txtDateEnd" runat="server" CssClass="tb" style="width:80px;" />
                    </td>
                    <td>
                        <asp:ImageButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" style="height:26px; margin-left:10px;" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
         
        <div class="listpage_list" >
            <table border="0" cellspacing="0" cellpadding="0" class="listtable">
                    <tr class="tblListHeaderTr">
                        <td width="10%" class="type" style="text-align:center"><%--<%=LanguageHelper.Get("Text_GridHeader_No")%>--%>Item</td>
                        <td width="30%" class="type" >No.</td>
                        <td  class="type" style="text-align:center"><%=LanguageHelper.Get("Text_GridHeader_Title")%></td>
                        <td width="15%" class="type" style="display:none;"><%=LanguageHelper.Get("Text_GridHeader_Flow")%></td>
                        <td width="15%" class="type" style="text-align:center"><%=LanguageHelper.Get("Text_ToolBar_Del")%></td>
                    </tr>
                
                <tbody>
                    <asp:Repeater ID="rptList" runat="server"  OnItemCommand="rptList_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td align="center" style="text-align:center"><%#AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + Container.ItemIndex + 1%></td>
                                <td class="dot" style="text-align:center"> 
                                <a onclick="javascript:openForm('<%#Eval("TASKID") %>','<%#Eval("ProcessName") %>','<%#Eval("FORMID") %>','<%#Eval("Incident") %>','Draft',this);" style="cursor:pointer; color:Blue;">
                                    <%#Eval("SUMMARY")%></a>
                                </td>
                                <td class="dot" style="text-align:center"><div class="dot"><%#UIHelper.ChangeProcessName(Eval("PROCESSNAME").ToString())%></div></td>
                                <td style="text-align:center">
                                 <asp:ImageButton ID="btnDelete" runat="server"  ImageUrl="../Ultimus.UWF.Home2/images/delect.jpg"   CommandName="del"
                                    CommandArgument='<%#Eval("FormID") %>' ClientIDMode="Static" OnClientClick='return delConfirm();' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="plcEmpty" runat="server" Visible="false">
                        <tr>
                            <td colspan="4" align="center">
                                <%=LanguageHelper.Get("Grid_EmptyData_Text")%>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </tbody>
            </table>
        </div>
        <div  class="pagelist"  style="border:0">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="Count %RecordCount%"
                Width="100%" CurrentPageButtonClass="pageBtn" OnPageChanged="AspNetPager1_PageChanged"
                AlwaysShow="true" PageSize="10" CssClass="dPager">
            </webdiyer:AspNetPager>
        </div>
       </div>
    </form>
</body>
</html>
