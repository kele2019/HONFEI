<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.Traval.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>出差申请流程/TravelRequestProcess</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            changeJDYD();
            changeYD();
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("提交/Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
                $("#ButtonList1_btnAsk").hide();
                //$("#ApprovalHistory1_trIdear").hide();
            }
            if ($("#hdPrint").val() != "0") {
                $("#divPrint").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
        });
        function beforeSubmit() {
            if ($(":input[name='TravalMD']:checked").size() == 0) {
                alert("差旅目的未选择/Purpose of the trip no select");
                return false;
            }
            var indexlen = $("#UserInfo1_fld_APPLICANT").text().indexOf('(');
            var UserName = $("#UserInfo1_fld_APPLICANT").text().substr(0, indexlen);
            var SAddress=$("#tabletbodyDetail").find("td").eq(1).find("input").val();
            var EAddress = $("#tabletbodyDetail").find("td").eq(2).find("input").val();
            var summary = UserName + " " + SAddress + "-" + EAddress + " Travel Request";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            $("#var_TravalVALUE").val($("#fld_TravalType option:selected").text());
            $("#var_FROMTO").val(SAddress + "-" + EAddress);
            return true;
        }
        function beforeSave() {
            var indexlen = $("#UserInfo1_fld_APPLICANT").text().indexOf('(');
            var UserName = $("#UserInfo1_fld_APPLICANT").text().substr(0, indexlen);
            var SAddress = $("#tabletbodyDetail").find("td").eq(1).find("input").val();
            var EAddress = $("#tabletbodyDetail").find("td").eq(2).find("input").val();
            var summary = UserName + " " + SAddress + "-" + EAddress + " Travel Request";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        function SetCityHotel(obj) {
            $("#fld_CityHotel").val($(obj).val());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="出差申请流程(Travel Request Form)" processprefix="TR" tablename="PROC_TRAVEL"
                    runat="server" tablenamedetail="PROC_Travel_DT"></ui:userinfo>
                 <%--tablenamedetail="PROC_TravelExpense_DT"--%>
            </div>
            <div class="row">
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="banner" colspan="6">详细信息Travel Application
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="width:100px">
                          差旅目的
                            <br /> Purpose of the trip：<span class="red">*</span>
                        </td>
                        <td class="td-content" colspan="3">
                        <asp:RadioButton runat="server" ID="fld_YWXY"  GroupName="TravalMD" />
                        <label>&nbsp;&nbsp;&nbsp;业务需要<br />&nbsp;&nbsp;&nbsp;Business imperative</label> 
                       &nbsp; &nbsp;&nbsp;&nbsp;
                       <asp:RadioButton runat="server" ID="fld_PX"      GroupName="TravalMD"   />
                          <label>&nbsp;&nbsp;&nbsp;培训<br />&nbsp;&nbsp;&nbsp;Training </label> 
                      &nbsp; &nbsp;&nbsp;&nbsp;  
                       <asp:RadioButton runat="server" ID="fld_QT"        GroupName="TravalMD"   />
                        <label>&nbsp;&nbsp;&nbsp;其他<br />&nbsp;&nbsp;&nbsp;Others</label> 
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                            请简明描述您的差旅目的 <br />
                            Please describe your intention of this business trip： <span class="red">*</span>
                        </td>
                        <td class="td-content" colspan="3">
                            <asp:TextBox runat="server" ID="fld_TravalComments"   CssClass="validate[required]" TextMode="MultiLine" Rows="5" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div  >
                <div class="row" style="width: 120%">

                    <table class="table table-condensed table-bordered tablerequired" style="width:83%" id="tbDetail">
                        <tr>
                            <td class="banner" colspan="9">出差详情 Traval Detail
                            </td>
                        </tr>
                        <tr>
                         
                            <th colspan="7">行程安排（将作为机票/火车票定购信息）<br />
Schedule(regarded as the information of tickets ordering)
                            </th>
                            <th align="center">请写明每次行程<br />
Please specify each trip</th>
   <th> </th>
                        </tr>
                        <tr>
                       
                            <th rowspan="2"> 序号<br />
                               No.
                            </th>
                            
                            <th colspan="2">目的地<br />
                                Destination
                                 <span class="red">*</span>
                            </th>

                            <th colspan="2">出发 <br />
                                Departure
                                  
                            </th>
                            <th colspan="2">到达 <br />
                                Arrival
                                
                            </th>
                            <th rowspan="2" align="center">将要拜访的客户或公司  <br />
Customers or companies <br />that you will visit
                            </th>
                             <th rowspan="2"></th>
                        </tr>
                        <tr>
                            <th width="12%">从<br />
From</th>
                            <th width="12%">
                                至<br />
To
                            </th>
                            <th width="12%">日期<br />
Date<span class="red">*</span></th>
                            <th  width="8%">时间<br />
Time</th>
                            <th width="12%">日期<br />
Date<span class="red">*</span></th>
                            <th width="8%">时间<br />
Time</th>
                        </tr>
                         <tbody id="tabletbodyDetail">
                        <asp:Repeater ID="fld_detail_PROC_Travel_DT" runat="server" OnItemCommand="fld_detail_PROC_Travel_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_Travel_DT_ItemDataBound" >
                            <ItemTemplate>
                                
                                <tr>
                              
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                        <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="fld_SAdress"  CssClass="validate[required]" Width="80%" Text='<%#Eval("SAdress") %>' runat="server"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="fld_EAdress" runat="server"  CssClass="validate[required]" Width="80%"  Text='<%#Eval("EAdress").ToString() %>'></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="fld_SDate" Text='<%# String.IsNullOrEmpty(Eval("SDate").ToString())? "":DateTime.Parse(Eval("SDate").ToString()).ToString("yyyy-MM-dd") %>' runat="server"
                                            Width="80%" CssClass="validate[required]"       onfocus="ShowTime(this,'1');" ></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                       <asp:TextBox ID="fld_ETime" Text='<%#Eval("ETime") %>' runat="server"   Width="80%"  ></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                      <asp:TextBox ID="fld_ASDate" Text='<%# String.IsNullOrEmpty(Eval("ASDate").ToString())? "":DateTime.Parse(Eval("ASDate").ToString()).ToString("yyyy-MM-dd") %>' runat="server"
                                            Width="85%" CssClass="validate[required]" onfocus="ShowTime(this,2);"         ></asp:TextBox> 
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="fld_AETime" Text='<%#Eval("AETime").ToString()%>' runat="server"
                                           Width="80%"      ></asp:TextBox>
                                    </td>
                                      <td style="text-align: center;">
                                        <asp:TextBox ID="fld_Comments" Text='<%# String.IsNullOrEmpty(Eval("Comments").ToString())? "":Eval("Comments").ToString() %>' runat="server"
                                            Width="90%"    ></asp:TextBox>
                                    </td>
                                       <td style="text-align:center">
                                            <asp:Button ID="btnDelete" runat="server" Text="删除/Del" CssClass="btn" CommandName="del"
                                                ClientIDMode="Static" OnClientClick="return confirm('确认删除/Confirm Del？')" />
                                        </td>
                                </tr>
                              
                            </ItemTemplate>
                        </asp:Repeater>
                          </tbody>
                          <tr>
                          <td colspan="9" style="text-align:left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Button ID="btnAdd" runat="server" Text="增加/Add" CssClass="btn" CausesValidation="false"
                OnClick="btnAdd_Click"     />
                          </td>
                          </tr>
                    </table>

                </div>

            </div>
            <div class="row">
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="banner" colspan="4"> 预定 Reservation
                        </td>
                    </tr>
                     <tr>
                        <td class="td-label">出差类别：<br />Traval Type</td>
                        <td><asp:DropDownList runat="server" ID="fld_TravalType" style=" width:97%">
                            <asp:ListItem Value="1">国内 Domestic</asp:ListItem>
                            <asp:ListItem Value="2">国外 Abroad</asp:ListItem>
                            </asp:DropDownList></td>
                        <td class="td-label"> </td>
                        <td><asp:TextBox runat="server" ID="fld_TravalCount" Visible="false" onfocus="this.blur()" style="width:97%"></asp:TextBox></td>
                    </tr>
                     <tr>
                         <td class="td-label">预订机票/火车票？<br />Ticket Reservation?</td>
                         <td  width="30%" >
                         <asp:RadioButton runat="server" onclick="changeYD()"  GroupName="RadioYDJP" ID="fld_JPYDYes"   />
                          是，公司预订<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Yes,company reserves  
                          </td>
                          <td>
                          <asp:RadioButton runat="server" GroupName="RadioYDJP"    onclick="changeYD()"  ID="fld_JPYDNo" />
                         否，理由<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No,reasons </td>
                         <td>
                         <asp:TextBox runat="server"     style="display:none;width:97%" ID="fld_YDNo"></asp:TextBox></td>
                     </tr>
                     <tr id="YDTR" style="display:none">
                     <td class="td-label">备注<br />Comments</td>
                     <td colspan="3"><asp:TextBox  runat="server" ID="fld_YDComments" width="99%"></asp:TextBox></td>
                     </tr>
                      <tr>
                         <td class="td-label">预订酒店？<br />Hotel Reservation?</td>
                         <td  >
                          <asp:RadioButton runat="server" GroupName="RadioYDJD" onclick="changeJDYD()"  ID="fld_YDJDYes" />
                         是，公司预订<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Yes,company reserves 
                         </td>
                         <td>
                          <asp:RadioButton runat="server" GroupName="RadioYDJD" onclick="changeJDYD()"  ID="fld_YDJDNo" />
                           否，理由<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No,reasons </td>
                           <td><asp:TextBox runat="server" ID="fld_JDNo" style="display:none;width:97%"></asp:TextBox></td>
                     </tr>
                     <tr id="JDTR" style="display:none">
                      <td class="td-label">备注<br />Comments</td>
                     <td colspan="3"><span><asp:TextBox  runat="server" ID="fld_YDJDComments"  style="margin-bottom:10px;"  width="99%"></asp:TextBox></span>
                     <br />城市 City： <asp:DropDownList runat="server" ID="drop_HotelCity" 
                             AutoPostBack="true"  onselectedindexchanged="fld_HotelCity_SelectedIndexChanged" Width="20%" ></asp:DropDownList>
                             <asp:TextBox runat="server" style="display:none" ID="fld_HotelCity"  onprerender="fld_HotelCity_PreRender" />
                             <asp:TextBox runat="server" style="display:none" ID="fld_HotelCityValue" />
                           酒店 Hotel：<asp:DropDownList runat="server" ID="drop_CityHotel" Width="55%" onchange="SetCityHotel(this)"></asp:DropDownList>
                             <asp:TextBox runat="server" style="display:none" ID="fld_CityHotel" />
                     </td>
                     </tr>
                   
                    </table>
            </div>
            <div class="row">
                <attach:attachments id="Attachments1" runat="server"></attach:attachments>
            </div>
            <div class="row">
                <ah:approvalhistory id="ApprovalHistory1" showaction="true" runat="server"></ah:approvalhistory>
            </div>
            <div id="ReturnBackTask" style="display:none; text-align:center">
        <asp:Button runat="server" ID="btnRevoke"  Visible="false" CssClass="btn" Text="Revoke" 
                onclick="btnRevoke_Click" />
        </div>
        </div>
        <div id="divPrint" style="display:none; text-align:center">
        
        <input type="button"  class="btn1" value="打印/Print" id="btnprint" onclick="preview('myDiv')" />
         
        </div>
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display: none;">
        <asp:TextBox runat="server"  ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
         <asp:HiddenField runat="server" ID="hdPrint"  Value="0" />
       <asp:TextBox runat="server" ID="var_TravalVALUE"></asp:TextBox>
       <asp:TextBox runat="server" ID="var_FROMTO"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdUrgeTask" />
           <%-- <asp:TextBox ID="fld_SupervisorLoginName" runat="server"></asp:TextBox>
            <asp:TextBox ID="fld_ManagerApproveType" runat="server"></asp:TextBox>--%>
            <%--   <asp:TextBox ID="fld_SelfDrivingDirector" runat="server" ></asp:TextBox>
         <asp:TextBox ID="fld_TravelerACCOUNT" runat="server" ></asp:TextBox>
         <asp:TextBox ID="fld_TravelerCOMPANYCODE" runat="server" ></asp:TextBox>--%>
        </div>
       
    </form>
</body>
<script type="text/javascript" language="javascript">
    
    function ShowTime(obj, index) {
        var mintime = $(obj).parent().prev().prev().find("input").val();
        var maxtime = $(obj).parent().next().next().find("input").val();
        var StartDate="";
        var EndDate="";
        if (index == 1 && maxtime!=undefined) {
            EndDate = maxtime
        }
        if (mintime!=undefined&&index== 2) {
          StartDate=mintime ;
        }
      if (index == 2) {
          WdatePicker({ dateFmt: 'yyyy-MM-dd', minDate: StartDate });
        }
        else {

            WdatePicker({dateFmt: 'yyyy-MM-dd', maxDate: EndDate });
        }
    }

    function DateChange(obj, index) {
//        var startDate = $(obj).val();
//        var mintime = $(obj).parent().prev().prev().find("input").val();
//        var maxtime = $(obj).parent().next().next().find("input").val();
    }
    function DateDiff(sDate1, sDate2) {    //sDate1和sDate2是2006-12-18格式  
        var aDate, oDate1, oDate2, iDays
        aDate = sDate1.split("-")
        oDate1 = new Date(aDate[1] + '-' + aDate[2] + '-' + aDate[0])    //转换为12-18-2006格式  
        aDate = sDate2.split("-")
        oDate2 = new Date(aDate[1] + '-' + aDate[2] + '-' + aDate[0])
        iDays = parseInt(Math.abs(oDate1 - oDate2) / 1000 / 60 / 60 / 24)    //把相差的毫秒数转换为天数
            iDays++;
        return iDays
    }  


    function changeYD() {
        var obj = $("#fld_JPYDYes").attr("checked");
        if (obj) {
            $("#fld_YDNo").hide();
            $("#YDTR").show();
            $("#fld_YDNo").css("");
        }
        else {
            $("#fld_YDNo").show();
            $("#YDTR").hide();
            $("#fld_YDNo").attr("class", "validate[required]");
        }
    }
    function changeJDYD() {
        var obj = $("#fld_YDJDYes").attr("checked");
        if (obj) {
            $("#fld_JDNo").hide();
            $("#JDTR").show();
            $("#fld_JDNo").css("");
        }
        else {
            $("#fld_JDNo").show();
            $("#JDTR").hide();
            $("#fld_JDNo").attr("class","validate[required]");
        }
    }
      
    // 处理浮点计算尾差问题
        function accMul(arg1, arg2) {
        //    var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
        //    try { m += s1.split(".")[1].length } catch (e) { }
        //         try { m += s2.split(".")[1].length } catch (e) { }
        //return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
        }
        //function GetCNYMoney(FROM_CURRENCY, MONEY_NUMBER, CNYMoneyObj) {
        //    var FROM_CURRENCY = FROM_CURRENCY;
        //    var MONEY_NUMBER = MONEY_NUMBER;
        //    var url = "Ajax/GetCNYMoney.ashx";
        //    $.ajaxSetup({ async: false });
        //    $.post(url, { FROM_CURRENCY: FROM_CURRENCY, MONEY_NUMBER: MONEY_NUMBER }, function (data) {
        //        if (data != null && data != "") {
        //            var parsedJson = $.parseJSON(data);
        //            for (var i = 0; i < parsedJson.T_blog.length; i++) {
        //                CNYMoneyObj.val(addKannma(parsedJson.T_blog[i].CHANGEMONEY));

        //            }
        //            getSumAmount('tbDetail', 'ACIVITYTOTAL');
        //        }
        //        else {

        //            CNYMoneyObj.val("");
        //            getSumAmount('tbDetail', 'ACIVITYTOTAL');
        //        }
        //    });
        //}
        ///处理数据千分位
        function addKannma(number) {
            
            var num = ToFixed(number, 2) + "";
            num = num.replace(new RegExp(",", "g"), "");
            // 正负号处理   
            var symble = "";
            if (/^([-+]).*$/.test(num)) {
                symble = num.replace(/^([-+]).*$/, "$1");
                num = num.replace(/^([-+])(.*)$/, "$2");
            }

            if (/^[0-9]+(\.[0-9]+)?$/.test(num)) {
                var num = num.replace(new RegExp("^[0]+", "g"), "");
                if (/^\./.test(num)) {
                    num = "0" + num;
                }

                var decimal = num.replace(/^[0-9]+(\.[0-9]+)?$/, "$1");
                var integer = num.replace(/^([0-9]+)(\.[0-9]+)?$/, "$1");

                var re = /(\d+)(\d{3})/;

                while (re.test(integer)) {
                    integer = integer.replace(re, "$1,$2");
                }

                return symble + integer + decimal;

            } else {
                return number;
            }
        }
        function ToFixed(value, digits) {
            var str = value.toString();
            var index = str.indexOf(".");
            var strInt = str;
            var strDec = "";

            if (index > 0) {
                strInt = str.substr(0, index);
                strDec = str.substr(index + 1, digits);
            }
            while (strDec.length < digits) {
                strDec += "0";
            }
            var integer = strInt + strDec;
            if (index > 0) {
                var nums = new Array();
                var dec = str.substr(index + 1 + digits);//取舍小数部分
              
                for (var i = 0; i < dec.length; i++) {
                    nums.push(dec.charAt(i));//拆分每个数字
                }
                var n1;
                var n2;
                while (nums.length > 1) {
                  n1= nums.pop();
                  if (n1 > 4) {
                      // js相加会自动认为是字符相加，修改成函数自加
                        n2 = nums[nums.length - 1]++;
                        nums[nums.length - 1] = n2;
                    }
                }
              
                if (nums.length && nums[nums.length - 1] > 4) {
                  integer++;
                   
                }

            }
            str = integer.toString();
            if (digits == 0)
                return str;
            while (str.length < strInt.length + strDec.length) {
                str = "0" + str;
            }
            return str.substr(0, str.length - digits) + "." + str.substr(str.length - digits);
        }
        function deletes() {
            var b = true;
            var num = 0;
            $("#tbDetail").find("tr:gt(0)").each(function (i, etr) {
                if ($(etr).find("td").eq(0).find("[id*=cb_SelectValue]").attr("checked") == "checked") {
                    num++;
                }
            });

            if (num == 0) {
                alert("Please select a list of data / 请选择一列数据！");
                b = false;
            }
            return b;
        }
        // 计算表格tableId 某列 totalCellIndex 的和 赋值给 totalControlId
        function getSumAmount(tableId, totalControlId) {
            var DutyMan_NM = 0;
            var DutyMan_Tm = 0;
            var findTableId = "#" + tableId + " tr";

            $(findTableId).find("td:eq(8)").each(function (i) {
                var textbox = $(this).find("input[type='text']");
                if (textbox.length > 0) {
                    re = new RegExp(",", "g");
                    DutyMan_Tm = parseFloat(textbox.val().replace(re, ""));

                    if (!isNaN(DutyMan_Tm)) {
                        DutyMan_NM = DutyMan_NM + DutyMan_Tm;
                    }
                }
            });
            if (!isNaN(DutyMan_NM)) {
                if ($("span[id$=" + totalControlId + "]") == null) return;

                var ActualPrediemOvernight = parseFloat($("input[id$=fld_ActualPrediemOvernightTotal]").val().replace(new RegExp(",", "g"), ""));
                if (!isNaN(ActualPrediemOvernight)) {
                    DutyMan_NM = DutyMan_NM + ActualPrediemOvernight;
                }
                var ActualPrediemMeals = parseFloat($("input[id$=fld_ActualPrediemMealsTotal]").val().replace(new RegExp(",", "g"), ""));
                if (!isNaN(ActualPrediemMeals)) {
                    DutyMan_NM = DutyMan_NM + ActualPrediemMeals;
                }
                var HomeCarMileageClaim = parseFloat($("input[id$=fld_HomeCarMileageClaim]").val().replace(new RegExp(",", "g"), ""));
                if (!isNaN(HomeCarMileageClaim)) {
                    DutyMan_NM = DutyMan_NM + HomeCarMileageClaim;
                }
                $("span[id$=" + totalControlId + "]").html(addKannma(ToFixed(DutyMan_NM, 2)));
                $("input[id$=" + totalControlId + "]").attr('value', addKannma(ToFixed(DutyMan_NM, 2)));


                $("input[id$=fld_TotalAmount]").val(ToFixed(DutyMan_NM, 2));
            }
        }

        // jack Add at 20140814  验证Remark 是否必填
        function CheckRemarkIsCanEmpty(FROM_DAY, TO_DAY) {

            var id = $(FROM_DAY).attr('id');
            var remarkId = id;


            FROM_DAY = $(FROM_DAY).val();
            if (TO_DAY == 'ToDate') {
                id = id.replace('Date', 'ToDate');
                remarkId = remarkId.replace('Date', 'Note');
                TO_DAY = $('#' + id).val();

            } else {
                id = id.replace('ToDate', 'Date');
                remarkId = remarkId.replace('ToDate', 'Note');
                TO_DAY = FROM_DAY;
                FROM_DAY = $('#' + id).val();
            }

            var url = "Ajax/GetHolidays.ashx";
            $.ajaxSetup({ async: false });
            $.post(url, { FROM_DAY: FROM_DAY, TO_DAY: TO_DAY }, function (data) {
                if (data != null && data != "") {


                    if (data == 'False') {

                        $('#' + remarkId).removeClass("validate[required]");
                    } else {

                        $('#' + remarkId).addClass("validate[required]");
                        alert("该日期为假日费用，请在备注栏注明原因");
                    }
                }
                else {
                    alert(0);
                }
            });
        }

</script>
</html>
