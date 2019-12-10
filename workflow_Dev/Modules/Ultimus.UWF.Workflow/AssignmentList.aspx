<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignmentList.aspx.cs"
    Inherits="Ultimus.UWF.Workflow.AssignmentList" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>
        <%=Lang.Get("AssignmentList_Title")  %></title>
        <script type="text/javascript" src="/Assets/js/common.js"></script>
        <script type="text/javascript" src="/Assets/js/selectorNew.js"></script>

    <script src="../../Assets/layui/layui.js" type="text/javascript"></script>
    <link href="../../Assets/layui/css/layui.css" rel="stylesheet" type="text/css" />
     
     <style type="text/css">
        #cbxListParent input {vertical-align:middle;float:left;}
        #cbxListQuestionType input{vertical-align:middle;}
        .cb td{width:100px; border:0;} 
        .cb label{display:inline-block;width:80px;}
</style>

  <script type="text/javascript" language="javascript">

      //注意：选项卡 依赖 element 模块，否则无法进行功能性操作
      layui.use('element', function () {
          var element = layui.element();
      });
      function ChangeDepartmentProcess(obj) {
          $("#listbox1").find("option").remove();
          var IndexCount = 0;
          $("#divDept").find('input[type="checkbox"]').each(function () {
              // alert($(this).attr("checked"));
              if ($(this).attr("checked")) {
                  IndexCount++;
                  var ProcessID = $(this).attr("name");
                  //alert(ProcessID);
                  $.ajax({ url: 'AjaxClass.ashx',
                      data: { "ProcessID": ProcessID },
                      type: 'POST',
                      success: function (msg) {
                          if (msg != '') {
                              CheckDeptProcess(msg);
                          }
                      }
                  });
              }

          });
          if (IndexCount == 0) {
              $.ajax({ url: 'AjaxClass.ashx',
                  data: { "ProcessID": "" },
                  type: 'POST',
                  success: function (msg) {
                      if (msg != '') {
                          CheckDeptProcess(msg);
                      }
                  }
              });
          }
         
      }
      function CheckDeptProcess(StrValue)
      {
          var ProcessLength = StrValue.split('|').length;
         
          var listbox1HTML = "";
          for (var i = 0; i < ProcessLength; i++) {
          var NoSelect=StrValue.split('|')[i];
         if(!CheckListBox2Value(NoSelect))
              listbox1HTML += "<option>" + StrValue.split('|')[i] + "</option>";
          }
          $("#listbox1").append(listbox1HTML);
      }
      function CheckListBox2Value(ListValue) {
      var flag=false;
      $("#listbox2").find('option').each(function () {
          var ListBoxvalue = $(this).val();
          if (ListBoxvalue == ListValue) {
              flag = true;
              return;
          }
      });
      return flag;
      }

      $().ready(function () {
          $("input[type=radio][name=AssignType]").each(function (index) {

              $(this).click(function () {
                  if ($(this).attr("checked") == "checked") {

                      if ($(this).attr("id") == "RadioButton6") {
                          $("#trFuture").css("display", "block");
                          $("tr[idx=trprocessname]").css("display", "none");
                          $("#tableCTask").hide();
                      }
                      else if ($(this).attr("id") == "RadioButton7") {
                          $("#trFuture").css("display", "none");
                          $("tr[idx=trprocessname]").css("display", "block");
                          $("#tableCTask").hide();
                      }
                      else {
                          $("#trFuture").css("display", "none");
                          $("tr[idx=trprocessname]").css("display", "none");
                      }

                      if ($(this).attr("id") == "RadioButton5") {
                          $("#trFuture").css("display", "none");
                          $("tr[idx=trprocessname]").css("display", "none");
                          $("#tableCTask").show();
                      }
                  }
              });
          });
          if ($("#hidTab").val() == "2")
              $("#liRecall").click();
          else
              $("#liAssgin").click();
      });

      function GetSelectedValue(obj1, obj2) {
          var SelectedValue = "";
          $("#" + obj1 + " option:selected").each(function () {
              SelectedValue += "<option>" + $(this).val() + "</option>";
              $(this).remove();
          });
          if (SelectedValue != "")
              $("#" + obj2 + "").append(SelectedValue);
          else
              alert('没有选中任何选项');
      }
  </script>
</head>
<body>
    <form id="form1" runat="server">
  
     <div class="layui-tab">
  <ul class="layui-tab-title">
    <li class="layui-this" onclick="ChekedLi('1')" id="liAssgin">Assign</li>
    <li  onclick="ChekedLi('2')" id="liRecall">Recall Assign</li>
  </ul>
  <div class="layui-tab-content">
    <div class="layui-tab-item layui-show">
    <div>
        <table border="1" class="table table-border" width="98%">
            <tr class="banner">
                <td colspan="3" class="well">
                    <%=Lang.Get("Assign_Type")%>
                </td>
            </tr>
            <tr style="text-align: left; height: 20px; display:none;">
                <td  >
                </td>
                <td >
                 <asp:RadioButton ID="RadioButton2" runat="server" GroupName="AssignType" Checked="true" />
                    <label for="RadioButton2" style="cursor: pointer;">
                        <%=Lang.Get("Assign_SelectTaskAssign")%></label>
                </td>
                <td>
                </td>
            </tr>
            <tr class="TableDataRow" style="text-align: left; height: 20px;">
                <td colspan="3">
                  <asp:RadioButton ID="RadioButton5" runat="server" style=" float:left" GroupName="AssignType" />
                    <label for="RadioButton5" style="cursor: pointer; margin-left:20px;">Current tasks assign </label>
                </td>
            </tr>
            <tr class="TableDataRow" style="text-align: left; height: 20px;">
                
                <td colspan="3">
                 <asp:RadioButton ID="RadioButton6" runat="server" style=" float:left" GroupName="AssignType" />
                    <label for="RadioButton6" style="cursor: pointer; margin-left:20px;">Future all tasks assign</label>
                </td>
                
            </tr>
            <tr id="trFuture" style="height: 20px; display: none;">
                <td colspan="3">
                    <div style="border: 1px;">
                      <%--  <%=Lang.Get("Assign_FutureTaskAssignDate")%>--%>
                      Date interval 
                        <span class="red">*</span>
                        <asp:TextBox ID="txtFutureTaskDate" runat="server" Width="150" CssClass="Wdate" Height="20"
                            onclick="WdatePicker()"></asp:TextBox>
                             &nbsp;&nbsp;-
                    <asp:TextBox ID="txtFutureTaskDateEnd" runat="server" CssClass="Wdate" Height="20px" Width="150"
                        onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtFutureTaskDate\')}'})"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr class="TableDataRow" style="text-align: left; height: 20px; display:block">
                <td colspan="3">
                  <asp:RadioButton ID="RadioButton7" runat="server" style=" float:left" GroupName="AssignType" />
                    <label for="RadioButton7" style="cursor: pointer; margin-left:20px;">
                        <%=Lang.Get("Assign_ProcessAssign")%></label>
                </td>
            </tr>
            <tr idx="trprocessname" class="TableDataRow" style="text-align: left; height: 20px;
                display: none;">
                
                <td   colspan="3" >
                   <%-- <%=Lang.Get("frm_Queue_process")%><span class="red">*</span>--%>
                    <asp:DropDownList ID="dropProcessName1" Visible="false"  runat="server" Width="250">
                    </asp:DropDownList>
                     <asp:HiddenField runat="server" ID="hidProcessName" />
                      <%-- Department Process<hr />--%>
                     <%--  <asp:CheckBoxList runat="server" CssClass="cb"     RepeatLayout="Table"   CellPadding="5" CellSpacing="5" Width="100%"  RepeatColumns="8" RepeatDirection="Horizontal" ID="cbListProcess">
                       </asp:CheckBoxList>--%>
                       <div id="divDept" runat="server" >
                     
 
                       </div>
   <table>
    <tr>
    <td style="border-top:0px" rowspan="2">
    From:<br />
     <asp:ListBox Width="300px" Height="300px" SelectionMode="Multiple" runat="server" ID="listbox1">
    <asp:ListItem>1</asp:ListItem>
    <asp:ListItem>2</asp:ListItem>
    <asp:ListItem>3</asp:ListItem>
    <asp:ListItem>4</asp:ListItem>
    <asp:ListItem>5</asp:ListItem>
    </asp:ListBox>
    </td>
   <td style="border-top:0px; vertical-align:bottom;"><input type="button" class="btn" value=">>" onclick="GetSelectedValue('listbox1','listbox2')" /></td>
    
    <td style="border-top:0px" rowspan="2"> To:<br />
     <asp:ListBox Width="300px" Height="300px" SelectionMode="Multiple" runat="server" ID="listbox2">
    
    </asp:ListBox>
    </td>
     </tr>
    
     <tr>
     <td style="border-top:0px"> <input type="button" value="<<"  class="btn"  onclick="GetSelectedValue('listbox2','listbox1')" /></td>
     </tr>
    </table>
      </td>
              
            </tr>


            <tr idx="trprocessname" class="TableDataRow" style="text-align: left; height: 20px;
                display: none;">
                
                <td colspan="3">
                   <%=Lang.Get("Assign_Date")%> <span class="red">*</span>
                    <asp:TextBox ID="txtBegin" runat="server" CssClass="Wdate" Height="20px" Width="100"
                        onclick="WdatePicker()"></asp:TextBox>
                    &nbsp;&nbsp;-
                    <asp:TextBox ID="txtEnd" runat="server" CssClass="Wdate" Height="20px" Width="100"
                        onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtBegin\')}'})"></asp:TextBox>
                </td>
            </tr>

            <tr class="banner">
                <td colspan="3" class="well" >
                   <%=Lang.Get("Assign_Touser")%> 
                </td>
            </tr>
            <tr>
                
                <td colspan="3">
                    <%=Lang.Get("Assign_AssignUser")%><span class="red">*</span>&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="AssignUserName" style="width: 200px;" runat="server" class="TextSearch" />
                    <input type="button" value="..." class="btn Button" onclick="OpenSelectUser()" />
                    <script type="text/javascript" language="javascript">
                        function OpenSelectUser() {
                            //                            SelectUser({ type: '1', txtName: 'AssignUserName', txtId: 'AssignUserAccount' });
                            //selectUser('','');
                            //selectUser(0, 'AssignUserName', 'AssignUserAccount');

                            var returnJson = window.showModalDialog("../Ultimus.UWF.OrgChart/SelectUser.aspx", "javascript", "dialogHeight=450px;dialogWidth=800px;scroll=no;");
                            if (returnJson != null) {
                                var json = eval('(' + returnJson + ')');
                                $("#AssignUserName").val(json[0].Name);
                                $("#AssignUserAccount").val(json[0].ID);
                            }
                        }
                    </script>
                    <input id="AssignUserAccount" type="hidden" runat="server" />
                </td>
            </tr>
            
            <tr class="TableDataRow"   >
                <td colspan="2" style="text-align:center">
                    <asp:Button ID="Button2" runat="server" CssClass="btn  Button" Text="Assign" OnClientClick="return CheckPage1()"
                        OnClick="Button2_Click" />
                    <script type="text/javascript" language="javascript">
                        function CheckPage1() {
                             
                            var flag = true;
                            if ($("#AssignUserAccount").val() == "") {
                                flag = false;
                                alert('<%=Lang.Get("Assign_SelectUserMsg") %>');
                                return false;
                            } else if ($("#RadioButton5").attr("checked")) {
                                var TaskIDs = "";
                            $("#tbody2").find("tr").each(function () {
                                if ($(this).find("td:eq(0)").children().attr("checked")) {
                                    TaskIDs = TaskIDs + $(this).find("td:eq(0)").children().val() + ",";
                                }
                            });
                            $("#TaskIDs").val(TaskIDs.substr(0, (TaskIDs.length - 1)));
                                if ($("#TaskIDs").val() == "") {
                                    flag = false;
                                    alert('<%=Lang.Get("Assign_SelectTaskMsg") %>');
                                }
                            } else if ($("#RadioButton7").attr("checked")) {
//                                if ($("#dropProcessName1 option:selected").val() == "") {
//                                    flag = false;
//                                    alert('<%=Lang.Get("Assign_SelectProcessMsg") %>');
                                    var SelectedProcess="";
                                    $("#listbox2").find("option").each(function(){
                                 SelectedProcess=SelectedProcess+$(this).text()+",";
                                    });
                                 $("#hidProcessName").val(SelectedProcess);
                                    if (SelectedProcess== "") {
                                        flag = false;
                                     
                                         alert('Pls Select Process');
                                } else if ($("#txtBegin").val() == "") {
                                    flag = false;
                                    alert('<%=Lang.Get("Assign_SelectBeginDateMsg") %>');
                                } else if ($("#txtEnd").val() == "") {
                                    flag = false;
                                    alert('<%=Lang.Get("Assign_SelectEndDateMsg") %>');
                                }
                            }
                            if ($("#RadioButton6").attr("checked")) {
                                if ($("#txtFutureTaskDate").val() == "") {
                                    flag = false;
                                    alert('Please Input Close Date');
                                 }
                            }
                            return flag;
                        }
                    </script>
                    <input type="button" class="btn Button" value='<%=Lang.Get("Assign_CloseButton") %>'
                        onclick="window.close()" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="TaskIDs" runat="server" />
     <table id="tableCTask" class="table table-border" style="display:none">
                <tr class="TableHeader">
                    <th style="text-align:left">
                    <input type="checkbox"  onclick="changeListChecked(this,'tbody2')" />
                    Option
                    </th>
                    <th>
                       Process Name
                    </th>
                    <th>
                       DocumentNo
                    </th>
                    <th>
                        Step Name
                    </th>
                </tr>
                <tbody id="tbody2">
                    <asp:Repeater ID="RPCTask" runat="server">
                        <ItemTemplate>
                            <tr class="TableDataRow">
                                <td>
                                    <input type="checkbox" runat="server" id="Task_checkbox" value='<%# Eval("TASKID") %>' />
                                </td>
                                <td>
                                    <%# Eval("PROCESSNAME")%>
                                </td>
                                <td>
                                    <%# Eval("SUMMARY")%>
                                </td>
                                <td>
                                    <%# Eval("STEPLABEL")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
    
    </div>
    <div class="layui-tab-item">
    
      <table border="1"  style="height:140px" class="table table-border">
            <tr class="banner">
                <td rowspan="1" colspan="4" style="text-align: left;" class="well">
                    <%=Lang.Get("AssignmentList_Title")   %>
                </td>
            </tr>
            <tr align="center" valign="middle">
                <td rowspan="2" class="td-label" valign="middle">
                    <%=Lang.Get("AssignmentList_AssignType")   %>
                </td>
                <td rowspan="2" style="height:140px">
                   
                        <table border="0">
                            <tr style="border: 0;">
                                <td style="border: 0;">
                                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="AssignType" Checked="true"  onclick="CheckSearch()" />
                                </td>
                                <td style="border: 0;">
                                    <label for="RadioButton1">
                                        <%= Lang.Get("AssignmentList_SelectTaskAssign") %></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 0;">
                                    <asp:RadioButton ID="RadioButton3" runat="server" GroupName="AssignType" onclick="CheckSearch()" />
                                </td>
                                <td style="border: 0;">
                                    <label for="RadioButton3">
                                        <%=Lang.Get("AssignmentList_FutureTasksAssign")  %></label>
                                </td>
                            </tr>
                            <tr class="<%=EnableProcessAssign%>">
                                <td style="border: 0;">
                                    <asp:RadioButton ID="RadioButton4" runat="server" GroupName="AssignType" />
                                </td>
                                <td style="border: 0;">
                                    <label for="RadioButton4">
                                        <%=Lang.Get("AssignmentList_ProcessAssign")  %></label>
                                </td>
                            </tr>
                        </table>
                   
                </td>
            
               
            </tr>
            <tr style="display:none">
                <td style="display:none" rowspan="1">
                    <%=Lang.Get("frm_Queue_process")   %>
                </td>
             <td style="display:none" rowspan="1">
                    <asp:DropDownList ID="dropProcessName" runat="server">
                    </asp:DropDownList>
                </td>

                <td rowspan="1">
                    <%=Lang.Get("Assign_AssignUser")  %>
                </td>
                <td rowspan="1">
                    <input id="txtAssignUser" type="text" runat="server" class="inputborder160" readonly="readonly" />
                    <input class="btn" type="button" value="..." onclick="SelectUser({ type: '1', txtName: 'txtAssignUser', txtId: 'txtAssignUserAccount' });" />
                    <input id="txtAssignUserAccount" type="hidden" runat="server" />
                    <script type="text/javascript" language="javascript">
                        function ChooseUser() {
                         
                        }
                    </script>
                </td>
            </tr>
            <tr>
                <td colspan="2" rowspan="2" style=" text-align:center; vertical-align:middle;">
                    <asp:Button ID="Button1"  style="display:none" runat="server" CssClass="btn Button" OnClick="Button1_Click"   />
                    <asp:Button ID="Button3" runat="server" CssClass="btn Button" Visible="false" OnClientClick="return ResetSearchValue()" />
                    <script type="text/javascript" language="javascript">
                        function ResetSearchValue() {
                            document.getElementById("RadioButton1").setAttribute("checked", "checked");
                            var options = document.getElementById("dropProcessName").children;
                            options[0].setAttribute("selected", "selected");
                            document.getElementById("txtAssignUser").value = "";
                            document.getElementById("txtAssignUserAccount").value = "";
                            document.getElementById("Button4").click();
                            return false;
                        }
                        function CheckSearch() {
                            if ($("#RadioButton1").attr("checked") || $("#RadioButton3").attr("checked")) {
                                // return true;
                                $("#Button1").click();
                            }
                            else {
                                alert("Please Select Assign Type");
                                return false;
                            }

                        }
                    </script>
                    <asp:Button ID="Button4" runat="server"   CssClass="btn Button"  OnClientClick="return CheckPage()"
                        OnClick="Button4_Click" />
                    <script type="text/javascript" language="javascript">
                        function CheckPage() {
                            var isCheck = false;
                            var table;
                            if (document.getElementById("RadioButton1").getAttribute("checked") != "") {
                                table = $("#tbody1");
                            }
                            if (document.getElementById("RadioButton3").getAttribute("checked") != "") {
                                table = $("#tbody3");
                            }
                            if (document.getElementById("RadioButton4").getAttribute("checked") != "") {
                                table = $("#tbody4");
                            }
                            $(table).find("tr").each(function () {
                                if ($(this).find("td:eq(0)").children().attr("checked")) {
                                    isCheck = true;
                                }
                            });
                            return isCheck;
                        }
                    </script>
                    <asp:Button ID="Button5"  runat="server" CssClass="btn Button"  Visible="false" OnClientClick="return GoBack()" />
                    <script type="text/javascript" language="javascript">
                        function GoBack() {
                            window.opener.iframeAutoFit();
                            window.close();
                             
                            return false;
                        }
                    </script>
                      <input type="button" class="btn Button" value='<%=Lang.Get("Assign_CloseButton") %>'
                        onclick="window.close()" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="task" runat="server" Visible="false">
            <table class="table table-border">
                <tr class="TableHeader">
                    <th style="text-align:left">
                    <input type="checkbox"  onclick="changeListChecked(this,'tbody1')" />
                    Option
                    </th>
                    <th>
                       Process Name
                    </th>
                    <th>
                       DocumentNo
                    </th>
                    <th>
                        Step Name
                    </th>
                    <th>
                      Assignee
                    </th>
                </tr>
                <tbody id="tbody1">
                    <asp:Repeater ID="TaskList" runat="server">
                        <ItemTemplate>
                            <tr class="TableDataRow">
                                <td>
                                    <input type="checkbox" runat="server" id="Task_checkbox" value='<%# Eval("TASKID") %>' />
                                </td>
                                <td>
                                    <%# Eval("PROCESSNAME")%>
                                </td>
                                <td>
                                    <%# Eval("SUMMARY")%>
                                </td>
                                <td>
                                    <%# Eval("STEPLABEL")%>
                                </td>
                                <td>
                                    <%# Eval("ASSIGNEDTOUSER")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </asp:Panel>
        <asp:Panel ID="FutureTasks" runat="server" Visible="false">
            <table class="table table-border">
                <tr class="TableHeader">
                    <th>
                    </th>
                    <th>
                         Process Name
                    </th>
                    <th>
                         Step Name
                    </th>
                    <th>
                       Assignee
                    </th>
                    <th>
                       Due Time
                    </th>
                </tr>
                <tbody id="tbody3">
                    <asp:Repeater ID="FutureTasksList" runat="server">
                        <ItemTemplate>
                            <tr class="TableDataRow">
                                <td>
                                    <input type="checkbox" runat="server" id="FutureTasksList_checkbox" />
                                </td>
                                <td>
                                    <asp:Label ID="FutureTasksList_ProcessName" runat="server" Text='<%# Eval("PROCESSNAME")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="FutureTasksList_StepName" runat="server" Text='<%# Eval("STEPLABEL")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="FutureTasksList_AssignedToUser" runat="server" Text='<%# Eval("ASSIGNEDTOUSER")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="FutureTasksList_Assignuntil" runat="server" Text='<%# Eval("ASSIGNUNTIL")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </asp:Panel>
        <asp:Panel ID="Processes" runat="server" Visible="false">
            <table class="table table-condensed table-bordered">
                <tr>
                    <th>
                    </th>
                    <th>
                        流程名 Process Name
                    </th>
                    <th>
                        代理人 Assignee
                    </th>
                    <th>
                        开始时间 Begin Date
                    </th>
                    <th>
                        结束时间 End date
                    </th>
                </tr>
                <tbody id="tbody4">
                    <asp:Repeater ID="ProcessesList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <input id="Processes_checkbox" type="checkbox" runat="server" value='<%# Eval("ID") %>' />
                                </td>
                                <td>
                                    <%# Eval("ProcessName")%>
                                </td>
                                <td>
                                    <%# Eval("TskAssignUser")%>
                                </td>
                                <td>
                                    <%# Eval("BgDate")%>
                                </td>
                                <td>
                                    <%# Eval("EdDate")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </asp:Panel>
    </div>
    
  </div>
</div>


    <div style="display:none">
    <asp:HiddenField runat="server" ID="hidTab" />
      <script type="text/javascript">
          function changeListChecked(obj, elem) {
              if ($(obj).attr("checked"))
                  $("#" + elem).find('input[type="checkbox"]').attr("checked", true);
              else
                  $("#" + elem).find('input[type="checkbox"]').attr("checked", false);
          }

          function ChekedLi(obj) {
          $("#hidTab").val(obj);
          }
      </script>
    </div>
    </form>
</body>
</html>
