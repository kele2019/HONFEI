<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs" Inherits="Ultimus.UWF.Form.ProcessControl.UserInfo" %>

<script src="/Modules/Ultimus.UWF.Form.ProcessControl/js/loading.js" type="text/javascript"></script>
<link href="/Modules/Ultimus.UWF.Form.ProcessControl/css/loading.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript" src="/Assets/js/jquery.js"></script>
    <script type="text/javascript" src="/Assets/js/jquery.validationEngine.js"></script>
    <script type="text/javascript" src="/Assets/js/languages/jquery.validationEngine-zh_CN.js"></script>
<script type="text/javascript" language="javascript">
    self.moveTo(0, 0);
    self.resizeTo(screen.availWidth, screen.availHeight);
    function request(paras) {
        var url = location.href;
        var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
        var paraObj = {}
        for (i = 0; j = paraString[i]; i++) {
            paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
        }
        var returnValue = paraObj[paras.toLowerCase()];
        if (typeof (returnValue) == "undefined") {
            return "";
        } else {
            return returnValue;
        }
    }
    jQuery(document).ready(function () {

        $("input[money=money]").each(function () {
            $(this).keyup(function () {
                //this.value = this.value.replace(/[^0-9.]/g, '');
                this.value = this.value.replace(/[^0-9.+(\d{2})?$]/g, '');

            });
        });
        $("input[money=int]").each(function () {
            $(this).keyup(function () {
                this.value = this.value.replace(/[^0-9]/g, '');
            });
        });
        $("#ButtonList1_btnSubmit").click(function () {
            form_validation();
        });

        $("span[id^=fld_detail]").each(function () {
            $(this).css("text-align", "center");

        });
        $(".money").each(function () {
            var totalCount = $(this).text();
            var ThoudsData = formatNumber(totalCount, 2, 1);
            $(this).text(ThoudsData);

        });
        if (request("type").toLocaleLowerCase() == "myrequest") {
            DisableWebForm();
        }
    });

    function DisableWebForm() {

        $(":input[type!=submit][type!=radio][type!=checkbox][type!=button]:visible").each(function () {
            var jQueryObject = $(this); //取jQuery对象，实际上它是一个数组，只不过里面只有一个元素，因为这里选择器是id 
            var domObject = jQueryObject[0]; //从jQuery对象中得到原生的DOM对象 
            if (domObject.tagName.toLocaleLowerCase() == "select") {
                //$(this).parent().append("<span style=\"word-break:break-all;word-wrap:break-word;width:" + $(this).css("width") + ";text-align:center;\">" + $(this).text() + "</span>");
                $(this).attr("disabled", "disabled");
            }
            else {
                $(this).parent().append("<span style=\"word-break:break-all;word-wrap:break-word;width:" + $(this).css("width") + ";text-align:center;\">" + $(this).val() + "</span>");
                $(this).css("display", "none");
            }
        })
        $(":input[type=radio]").each(function () {
            $(this).attr("disabled", "disabled");
        });

        $(":button[id!=btnprint]").each(function () {
            $(this).css("display", "none");
        })
        $(".btn").each(function () {
            if ($(this)[0].name != "ButtonList1$btnClose" && $(this)[0].name != "btnRevoke") {
                // $(this).css("display", "none");
                $(this).attr("disabled", "disabled");
            }
        });
    }

    function form_validation() {
        jQuery("#form1").validationEngine('attach', {
            onValidationComplete: function (form, status) {
                if (status == false) {
                    submitTimes = 0;
                   // closeDiv();
                }
            }
        });

    }

</script>
<table align="center" style="width:100%;margin-top:0.5%;margin-bottom:0.5%;"  >
    <tr>
        <td width="30%" >
            <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/img/logo.gif" Height="50px"
                Visible="true" />
        </td>
        <td style="text-align: center; padding-top: 10px; "  >
            <strong>
                <asp:Label ID="lblProcessName" Font-Size="20px" runat="server" Text="流程标题"></asp:Label>
            </strong>
        </td>
       
        <td align="right" style="padding-left:20;" width="30%">
        <asp:Literal ID="barcode" runat="server" Visible="false"  ></asp:Literal>
        </td>
    </tr>
    <tr>
     <td colspan="3">
        <p style="height:1px;">&nbsp;</p>
     </td>
       
    </tr>
    <tr class="hidden">
        <td style="text-align:right">
        实例号：
        </td>
        <td>
            <asp:Label ID="incident" runat="server" Text=""></asp:Label>
        </td>
         <td style="text-align: right; ">
            <asp:Label ID="lblDocumentNo" runat="server" Text="编号：" ></asp:Label>
        </td>
        <td width="150">
          
        </td>
    </tr>
</table>

<p style="font-weight:bold;">Basic Info<span style="text-decoration:underline; float:right;color:Blue;">Request Operation </span></p>
<table class="table table-condensed table-bordered ">
    <%--<tr>
        <td class="banner" colspan="6">
            <%=Ultimus.UWF.Common.Logic.Lang.Get("Form_BasicInfo")%>
        </td>
    </tr>--%>
    <tr>
        <td class="td-label">
            <%--<%=Ultimus.UWF.Common.Logic.Lang.Get("Form_Applicant")%>：--%>
            <p style="text-align:center">Applicant</p>
        </td>
        <td class="td-content">
            <asp:Label ID="fld_APPLICANT" runat="server" Text=""></asp:Label>
        </td>
        <td class="td-label">
            <%-- <%=Ultimus.UWF.Common.Logic.Lang.Get("Form_RequestDate")%>：--%>
            <p style="text-align:center;">Department</p>
        </td>
        <td class="td-content">
            <asp:Label ID="fld_DEPARTMENT" runat="server" Text=""></asp:Label>
            
        </td>
    </tr>
    <tr  >
        <td class="td-label">
          <%--  <%=Ultimus.UWF.Common.Logic.Lang.Get("Form_Department")%>：--%>
           <p style="text-align:center;">Employee ID</p>
        </td>
        <td class="td-content"  >
            
            <asp:DropDownList ID="fld_COMPANY" runat="server"  Visible="false"    DataTextField="Name" DataValueField="Value">
            </asp:DropDownList>
            <asp:Label ID="lblCOMPANY" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="fld_USERCODE" runat="server" Text=""></asp:Label>
        </td>
        <td class="td-label ">
          <%--  <%=Ultimus.UWF.Common.Logic.Lang.Get("Form_Company")%>：--%>
          <p style="text-align:center;">Request Date</p>
        </td>
        <td class="td-content">
            <asp:Label ID="fld_REQUESTDATE" runat="server" Text=""></asp:Label>
          
        </td>
    </tr>
    <tr style="block">
        <td class="td-label">
        <p style="text-align:center;"> Document Number</p>
           <%-- <%=Ultimus.UWF.Common.Logic.Lang.Get("Form_Summary")%>：<span class="red">*</span>--%>
        </td>
        <td class="td-content" colspan="3">
        <asp:Label ID="fld_DOCUMENTNO" runat="server" Text=""    ></asp:Label>
            <asp:TextBox ID="fld_PROCESSSUMMARY"  runat="server" Text=""  STYLE="display:none"  Width="87%"></asp:TextBox>
            <asp:Label ID="lblSummary" runat="server" Visible="false" Text=""></asp:Label>
        </td>
    </tr>
</table>
<div class="hidden">
    <asp:TextBox ID="fld_Status" runat="server" Text="1"></asp:TextBox>
    <asp:TextBox ID="fld_PROCESSNAME" runat="server"></asp:TextBox>
    <asp:TextBox ID="fld_INCIDENT" runat="server" Text="0"></asp:TextBox>
    <asp:TextBox ID="txtStepName" runat="server"></asp:TextBox>
    <asp:TextBox ID="fld_FORMID" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtProcessPrefix" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtReadOnly" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtTableName" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtTableNameDetail" runat="server"></asp:TextBox>
    
    <asp:TextBox ID="fld_APPLICANTACCOUNT" runat="server" Text=""></asp:TextBox><%--申请人账号--%>
    <asp:TextBox ID="var_DepartmentId" runat="server" Text=""></asp:TextBox><%--本部门Id--%>
  
    <asp:TextBox ID="var_AttachmentPath" runat="server" Text=""></asp:TextBox> 
    <asp:TextBox ID="var_AttachmentName" runat="server" Text=""></asp:TextBox> 

    <asp:TextBox runat="server" ID="var_REMAIL"></asp:TextBox>
    <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtTaskId" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtApplicantAccount" runat="server"></asp:TextBox>
</div>
