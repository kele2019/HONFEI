<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.OT.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Time Record Application Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function beforeSubmit() {
            var date = new Date($("#fld_StartDate").val().replace(/-/g, "/"));
            $("#fld_OTYear").val(date.getFullYear());
            $("#fld_OTMonth").val(date.getMonth() + 1);
            if ($("#applyingFor1").attr("checked")) {
                $("#fld_ApplyingFor").val("Working Date & Holiday");
            }
            if ($("#applyingFor2").attr("checked")) {
                $("#fld_ApplyingFor").val("Public Holiday");
            }
            var SumHour = $("#fld_SumHour").val() - 0;
            var Msg = "";
            if (SumHour % 1 != 0) {
               Msg="Your Applying time is wrong.Please input again!";
               
            }
            if (Msg == "") {
                var summary = "OT Application";
                $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
                return true;
            }
            else {
                alert(Msg);
                return false;
            }
        }
        function beforeSave() {
            var summary = "OT Application";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        function showTime(obj) {
            var time = new Date(obj.replace(/-/g, "/"));
            var year = time.getFullYear();
            var month = time.getMonth() + 1;
            var date = time.getDate();
            return (year + "-" + month + "-" + date) == "NaN-NaN-NaN" ? "" : (year + "-" + month + "-" + date);
        }
        $(document).ready(function () {
            $("#starthours").val($("#fld_StartHours").val());
            $("#startminutes").val($("#fld_StartMinutes").val());
            $("#endhours").val($("#fld_EndHours").val());
            $("#endminutes").val($("#fld_EndMinutes").val());
            if ($("#fld_ApplyingFor").val() == "Working Date & Holiday") { $("#applyingFor1").attr("checked", true); }
            if ($("#fld_ApplyingFor").val() == "Public Holiday") { $("#applyingFor2").attr("checked", true); }
            $("#fld_StartDate").val(showTime($("#fld_StartDate").val()));
            $("#fld_EndDate").val(showTime($("#fld_EndDate").val()));
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
        });
        function getHours(startDate, startHours, startMinutes, endDate, endHours, endMinutes) {
            var date1 = new Date(startDate.replace(/-/g, "/"));
            date1.setHours(parseFloat(startHours));
            date1.setMinutes(parseFloat(startMinutes));
            var date2 = new Date(endDate.replace(/-/g, "/"));
            date2.setHours(parseFloat(endHours));
            date2.setMinutes(parseFloat(endMinutes));
            var time1 = date1.getTime();
            var time2 = date2.getTime();
            return ((time2 - time1) / (1000 * 60 * 60)).toFixed(2);
        }
        function changeOption(obj) {
            var times = $(obj).find("option:selected").text();
            $(obj).next().val(times);
            changeTime(obj);
        }
        function changeTime(obj) {
            var startdate = $("#fld_StartDate").val();
            var starthours = $("#fld_StartHours").val();
            var startminutes = $("#fld_StartMinutes").val();
            var enddate = $("#fld_EndDate").val();
            var endhours = $("#fld_EndHours").val();
            var endminutes = $("#fld_EndMinutes").val();
            var hours = getHours(startdate, starthours, startminutes, enddate, endhours, endminutes) == "NaN" ? "" : getHours(startdate, starthours, startminutes, enddate, endhours, endminutes);
            $("#fld_SumHour").val(hours);
            if (hours < 0) {
                alert("You enter the time less than zero.Please input again");
                $("#fld_SumHour").val("");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Time Record Application Process" processprefix="HROT" tablename="PROC_OT"
                    runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_ApplierLogin" style="display:none"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_ApplicantUser" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_OTMonth" style="display:none;" ></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_OTYear" style="display:none;" ></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_HR" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_HRLogin" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                         <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Applying for</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:RadioButton runat="server" ID="applyingFor1" GroupName="applyingFor" value="Working Date & Holiday"/>Working Date & Holiday &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton runat="server" ID="applyingFor2" GroupName="applyingFor" value="Public Holiday" />Public Holiday
                            <asp:TextBox runat="server" ID="fld_ApplyingFor" style="display:none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label" style="vertical-align:middle"> 
                       <span style=" background:red; height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Overtime Reason</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <asp:TextBox runat="server"  ID="fld_Reason" TextMode="MultiLine"  CssClass="validate[required]"   Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">From</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <span><asp:TextBox runat="server" style="float:left"  ID="fld_StartDate" onchange="changeTime(this)" CssClass="validate[required]" Width="45%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'fld_EndDate\')}'})"></asp:TextBox></span>
                            <asp:DropDownList runat="server" Width="20%" ID="starthours" onchange="changeOption(this)">
                                <asp:ListItem Selected="True">00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                            </asp:DropDownList>:
                            <asp:TextBox runat="server" ID="fld_StartHours" value="00" style="display:none;"></asp:TextBox>
                            <asp:DropDownList runat="server" Width="20%" ID="startminutes" onchange="changeOption(this)">
                                <asp:ListItem>00</asp:ListItem>
                                <%--<asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>--%>
                                <asp:ListItem>15</asp:ListItem>
                                <%--<asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>--%>
                                <asp:ListItem>30</asp:ListItem>
                                <%--<asp:ListItem>31</asp:ListItem>
                                <asp:ListItem>32</asp:ListItem>
                                <asp:ListItem>33</asp:ListItem>
                                <asp:ListItem>34</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>36</asp:ListItem>
                                <asp:ListItem>37</asp:ListItem>
                                <asp:ListItem>38</asp:ListItem>
                                <asp:ListItem>39</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>41</asp:ListItem>
                                <asp:ListItem>42</asp:ListItem>
                                <asp:ListItem>43</asp:ListItem>
                                <asp:ListItem>44</asp:ListItem>--%>
                                <asp:ListItem>45</asp:ListItem>
                                <%--<asp:ListItem>46</asp:ListItem>
                                <asp:ListItem>47</asp:ListItem>
                                <asp:ListItem>48</asp:ListItem>
                                <asp:ListItem>49</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>51</asp:ListItem>
                                <asp:ListItem>52</asp:ListItem>
                                <asp:ListItem>53</asp:ListItem>
                                <asp:ListItem>54</asp:ListItem>
                                <asp:ListItem>55</asp:ListItem>
                                <asp:ListItem>56</asp:ListItem>
                                <asp:ListItem>57</asp:ListItem>
                                <asp:ListItem>58</asp:ListItem>
                                <asp:ListItem>59</asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_StartMinutes" value="00" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label"> 
                       <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">To</p>
                        </td>
                        <td class="td-content"  colspan="3" >
                            <span><asp:TextBox runat="server" style="float:left" ID="fld_EndDate" CssClass="validate[required]" onchange="changeTime(this)"  Width="45%" onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd',minDate:'#F{$dp.$D(\'fld_StartDate\')}'})"></asp:TextBox></span>
                            <asp:DropDownList runat="server" Width="20%" ID="endhours" onchange="changeOption(this)">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                            </asp:DropDownList>:
                            <asp:TextBox runat="server" ID="fld_EndHours" value="00" style="display:none;"></asp:TextBox>
                            <asp:DropDownList runat="server" Width="20%" ID="endminutes" onchange="changeOption(this)">
                                <asp:ListItem>00</asp:ListItem>
                                <%--<asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>--%>
                                <asp:ListItem>15</asp:ListItem>
                                <%--<asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>--%>
                                <asp:ListItem>30</asp:ListItem>
                                <%--<asp:ListItem>31</asp:ListItem>
                                <asp:ListItem>32</asp:ListItem>
                                <asp:ListItem>33</asp:ListItem>
                                <asp:ListItem>34</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>36</asp:ListItem>
                                <asp:ListItem>37</asp:ListItem>
                                <asp:ListItem>38</asp:ListItem>
                                <asp:ListItem>39</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>41</asp:ListItem>
                                <asp:ListItem>42</asp:ListItem>
                                <asp:ListItem>43</asp:ListItem>
                                <asp:ListItem>44</asp:ListItem>--%>
                                <asp:ListItem>45</asp:ListItem>
                                <%--<asp:ListItem>46</asp:ListItem>
                                <asp:ListItem>47</asp:ListItem>
                                <asp:ListItem>48</asp:ListItem>
                                <asp:ListItem>49</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>51</asp:ListItem>
                                <asp:ListItem>52</asp:ListItem>
                                <asp:ListItem>53</asp:ListItem>
                                <asp:ListItem>54</asp:ListItem>
                                <asp:ListItem>55</asp:ListItem>
                                <asp:ListItem>56</asp:ListItem>
                                <asp:ListItem>57</asp:ListItem>
                                <asp:ListItem>58</asp:ListItem>
                                <asp:ListItem>59</asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="fld_EndMinutes" value="00" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-label"> 
                       <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">Sum</p>
                        </td>
                        <td class="td-content"  colspan="7" >
                            <span><asp:TextBox runat="server" ID="fld_SumHour"  onfocus="this.blur()" style="background-color:white;" CssClass="validate[required]" Width="38%"></asp:TextBox></span>hours
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row" style="display:none;">
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
        <btn:buttonlist id="ButtonList1" runat="server"></btn:buttonlist>
        <div style="display:none">
        <asp:TextBox runat="server" ID="fld_TRSummary"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdIncident" />
        <asp:HiddenField runat="server" ID="hdPrint" />
          <asp:HiddenField runat="server" ID="hdUrgeTask" />
        </div>
    </form>
</body>
</html>


