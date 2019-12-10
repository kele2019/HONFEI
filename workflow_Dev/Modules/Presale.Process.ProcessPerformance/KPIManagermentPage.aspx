<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPIManagermentPage.aspx.cs" Inherits="Presale.Process.ProcessPerformance.KPIManagermentPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>KPI Management</title>
      <link href="../../Assets/layui/css/layui.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript" src="/Assets/js/common.js"></script>
      <script src="../../Assets/layui/layui.js" type="text/javascript"></script>
      <script type="text/javascript">
          layui.use(['laydate', 'laypage', 'layer', 'table', 'carousel', 'upload', 'element', 'slider'], function () {

          });
          function AddVersionData() {
              layer.open({
                  type: 1 //Page层类型
               , area: ['400px', '180px']
               , title: 'Add Version'
               , shade: 0.6 //遮罩透明度
               , maxmin: true //允许全屏最小化
               , anim: 1 //0-6的动画形式，-1不开启
               , content: $("#divAddVersion")
              });
          }
          function OpenTargetData() {
              layer.open({
                 type: 1 //Page层类型
               , area: ['700px', '350px']
               , title: 'Add Target'
               , shade: 0.6 //遮罩透明度
               , maxmin: true //允许全屏最小化
               , anim: 1 //0-6的动画形式，-1不开启
               , content: $("#divTarget")
              });
          }

          function AddTargetData() {
              $("#divTarget").find('input[type=text]').val("");
              $("#hdFlag").val("");
              OpenTargetData();
          }

          function CheckVersionName() {
              var VersionName = $("#txtVersionname").val();
              $.ajax({
                  type: "post", //要用post方式                 
                  url: "HandlerData.ashx?Method=CheckName&VersionName=" + VersionName, //方法所在页面和方法名
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function (data) {
                      if (data.Code == 1) {
                          layer.msg("VersionName exists", { time: 2000 });
                          $("#txtVersionname").val("")
                      }
                      //alert(data.d); //返回的数据用data.d获取内容
                  },
                  error: function (err) {
                      layer.msg(err, { time: 2000 });
                  }
              });
          }
          function AddVersionInfo() {
              var VersionName = $("#txtVersionname").val();
              if (VersionName == "") {
                  layer.msg("VersionName is Empty");
              }
              else {
                  $.ajax({
                      type: "post", //要用post方式                 
                      url: "HandlerData.ashx?Method=AddVersionName&VersionName=" + VersionName, //方法所在页面和方法名
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (data) {
                          if (data.Code == 1) {
                              layer.msg("VersionName Faild:" + data.Msg, { time: 2000 });
                          }
                          if (data.Code == 0) {
                              layer.msg('Save Sucessfully', { icon: 1 }, function () {
                                  window.location.reload();
                              });
                          }
                          //alert(data.d); //返回的数据用data.d获取内容
                      },
                      error: function (err) {
                          layer.msg(err, { time: 2000 });
                      }
                  });
              }
          }
          function EditData(obj,code,index) {
              var txtTarget = $(index).parent().parent().find("td").eq(3).text();
              var txtStandard = $(index).parent().parent().find("td").eq(4).text();
              var txtDescription = $(index).parent().parent().find("td").eq(5).text();
              var txtCalculation = $(index).parent().parent().find("td").eq(6).text();

              $("#hdFlag").val(obj);
              $("#dropDepartment").val(code);
              $("#dropDepartment").val(code);
              $("#txtTarget").val(txtTarget);
              $("#txtStandard").val(txtStandard);
              $("#txtDescription").val(txtDescription);
              $("#txtCalculation").val(txtCalculation);
              OpenTargetData();
          }

          function AddTarget() {
              var hdFlagID = $("#hdFlag").val();
              var DeptCode = $("#dropDepartment").val();
              var StrTextIndex = $("#dropDepartment option:selected").text().indexOf('-');
              var DeptText = $("#dropDepartment option:selected").text().substring(0,StrTextIndex);
              var txtTarget = $("#txtTarget").val();
              var txtStandard = $("#txtStandard").val();
              var txtDescription = $("#txtDescription").val();
              var txtCalculation = $("#txtCalculation").val();
//              console.log(DeptText);
//              console.log(DeptCode);
              if (txtTarget == "" || txtStandard == "" || txtDescription == "" || txtCalculation == "") {
                  layer.msg("Sorry,Input text is empty, don't submit");
              }
              else {
                  $.ajax({
                      type: "post", //要用post方式                 
                      url: "HandlerData.ashx?Method=AddTarget&Flag=" + hdFlagID + "&DeptCode=" + DeptCode + "&DeptText=" + DeptText + "&Target=" + txtTarget + "&Standard=" + txtStandard + "&Description=" + txtDescription + "&Calculation=" + txtCalculation + "", //方法所在页面和方法名
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (data) {
                          if (data.Code == 1) {
                              layer.msg("Save Faild:" + data.Msg, { time: 2000 });
                          }
                          if (data.Code == 0) {
                              layer.msg('Save Sucessfully', { icon: 1 }, function () {
                                  window.location.reload();
                              });
                          }
                          //alert(data.d); //返回的数据用data.d获取内容
                      },
                      error: function (err) {
                          layer.msg(err, { time: 2000 });
                      }
                  });
              }
          }
          function DeleteData(obj,index) {
              layer.confirm('Are you sure delete?', { icon: 3, title: 'Tip' }, function (index) {
                  $.ajax({
                      type: "post", //要用post方式                 
                      url: "HandlerData.ashx?Method=DeleteTarget&Flag=" + obj + "",
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (data) {
                          if (data.Code == 1) {
                              layer.msg("Delete Faild:" + data.Msg, { time: 2000 });
                          }
                          if (data.Code == 0) {
                              layer.msg('Delete Sucessfully', { icon: 1 }, function () {
                                  window.location.reload();
                              });
                              //$(index).parent().parent().remove();
                          }
                      },
                      error: function (err) {
                          layer.msg(err, { time: 2000 });
                      }
                  });
                  layer.close(index);
              });

          }

      </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">

      <div style="text-align:center">
          <p style="font-size:xx-large; margin:20px;">KPI Management</p>
      </div>
     <div>
       <table class="table table-condensed table-bordered">
                    <tr>
                    <th>Search：</th>
                    <td>
                   <asp:DropDownList runat="server" ID="dropYear">
                   </asp:DropDownList>
                    <asp:Button runat="server" ID="btnSeachNew" Text="Search" CssClass="btn" onclick="btnSeachNew_Click"   />

                    <input type="button" id="btnAdd" class="btn" value="Add Version" onclick="AddVersionData()" style="margin-left:30%;"   />
                    <input type="button" id="btnAddTarget" class="btn btn-primary" value="Add Target" onclick="AddTargetData()" style="margin-left:2%;"   />
                     </td>
                    </tr>
      </table>
    </div>

     <table class="table table-condensed table-bordered">
                    <thead>
                    <tr>
                     <th>No</th>
                    <th colspan="2" width="25%">Process</th>
                    <th> Process Measurement </th>
                    <th>Goal</th>
                    <th>Definition</th>
                    <th>Calculationmethod</th>
                    <th width="8%">Option</th>
                    </tr>
                    </thead>

                    <tbody>
                    <asp:Repeater runat="server" ID="RPList">
                    <ItemTemplate>
                    <tr>
                    <td><%#Container.ItemIndex+1%></td>
                    <td  width="10%"><%#Eval("DEPTMENTCODE")%></td>
                     <td><%#Eval("PROCESS")%></td>
                    <td><%#Eval("PROCESSMEA")%></td>
                    <td><%#Eval("STANDARD")%></td>
                    <td><%#Eval("Definition")%></td>
                    <td><%#Eval("Calculationmethod")%></td>
                    <td style="text-align:center">
                    <a href="javascript:void(0)" onclick="EditData('<%#Eval("ID")%>','<%#Eval("DEPTMENTCODE") %>',this)" >Edit </a> 
                    <a href="javascript:void(0)" onclick="DeleteData('<%#Eval("ID")%>',this)" > Delete </a>
                    </td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                    </table>

    <div id="divAddVersion" style="display:none; margin-top:10px; ">
                    <table class="table table-condensed table-bordered">
                    <tr>
                    <th>VersionName：</th>
                    <td><input type="text" id="txtVersionname" onchange="CheckVersionName()" />
                    </td>
                    </tr>
                    <tr>
                    <td colspan="2">
                     Version name: Year+ V(version)+ Series Number(1,2,3….)
                    </td></tr>
                    </table>
               
                    <div style="width:100%; text-align:center;">
                    <%--<input type="button"  class="layui-btn layui-btn-normal" value="Save" onclick="AddVersionInfo()" />--%>
                    <input type="button"  class="btn btn-primary" value="Save" onclick="AddVersionInfo()" />
                    <input type="button"  class="btn" value="Cancel" onclick="layer.closeAll()" />
                    
                    </div>

    </div>

    
      <div id="divTarget" style="display:none; margin-top:10px; ">
     <table class="table table-condensed table-bordered">
                    <tr>
                    <th>Process：</th>
                    <td>
                    <asp:DropDownList runat="server"  style="width:99%;" ID="dropDepartment">
                    
                    </asp:DropDownList>
                    </td>
                    </tr>

                     <tr>
                    <th> Process Measurement ：</th>
                    <td>
                    <input type="text"    style="width:95%;"  id="txtTarget" />
                    </td>
                    </tr>

                    <tr>
                    <th>Goal：</th>
                    <td>
                    <input type="text"     style="width:95%;"   id="txtStandard" />
                    </td>
                    </tr>
                    
                    <tr>
                    <th>Definition：</th>
                    <td>
                    <input type="text"      style="width:95%;" id="txtDescription" />
                    </td>
                    </tr>

                    <tr>
                    <th>Calculationmethod：</th>
                    <td>
                    <input type="text"      style="width:95%;" id="txtCalculation" />
                    </td>
                    </tr>

                    </table>

                    <div style="width:100%; text-align:center;">
                    <%--<input type="button"  class="layui-btn layui-btn-normal" value="Save" onclick="AddVersionInfo()" />--%>
                    <input type="hidden" id="hdFlag" />
                    <input type="button"  class="btn btn-primary" value="Save" onclick="AddTarget()" />
                    <input type="button"  class="btn" value="Cancel" onclick="layer.closeAll()" />
                    
                    </div>

    </div>




    </div>
    </form>
</body>
</html>
