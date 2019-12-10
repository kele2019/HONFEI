<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.LocalExpense.NewRequest" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Local Expense Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function employee_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("/Modules/Presale.Process.TravelExpenseReport/Employee.aspx", "", digStr);
            if (ReturnValue != null) {
                var Employeedetail = eval("(" + ReturnValue + ")");
                var VendorCode = Employeedetail[0].EmployeeCode;
                var VendorName = Employeedetail[1].EmployeeName;
                $(obj).val(VendorCode + "-" + VendorName);
                $(obj).next().val(VendorCode);
                $(obj).next().next().val(VendorName);
            }
        }
        function subject_onclick(obj) {
            var digStr = "dialogHeight:660px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("Subject.aspx", "", digStr);
            if (ReturnValue != null) {
                var SubjectDetail = eval("(" + ReturnValue + ")");
                var SubjectName = SubjectDetail[0].SubjectName;
                var SubjectCode = SubjectDetail[1].SubjectCode;
                var ItemValue = SubjectDetail[2].ItemValue;
                $(obj).val(SubjectName);
                $(obj).next().val(SubjectCode);
                $(obj).next().next().val(ItemValue);
            }
            $(obj).parent().prev().children().val($("#UserInfo1_fld_REQUESTDATE").text());
        }
        function getTotalRMB() {
            var value = 0;
            $("#detailtable").find("tr").each(function (i, Etr) {
                var eachvalue = $(Etr).find("td").eq(3).children().val() - 0;
                $(Etr).find("td").eq(3).children().val(eachvalue.toFixed(2));
                value += (eachvalue == "" ? "0.00" : eachvalue) - 0;
            });
            $("#fld_RMB").val(value.toFixed(2));
            $("#lb_RMB").text(formatNumber(value.toFixed(2), 2, 1));
        }
        function beforeSubmit() {
            $("#Attachments1_txtMust").val("1");
            $("#fld_USD").val((($("#fld_RMB").val() - 0) / ($("#fld_Rate").val() - 0)).toFixed(2));
            var summary = "Local Expense Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            var GMCode = $("#fld_GM").val().toUpperCase();//GM
            var CurrencyCode = $("#fld_DGM").val().toUpperCase();//当前用户
            var VendorCode = $("#fld_VendorCode").val().toUpperCase();//报销用户
            $("#fld_GMLogin").val("GM");
            if (GMCode == VendorCode)
                $("#fld_GMLogin").val("GM1"); //代替提交
            if (CurrencyCode == VendorCode && GMCode == VendorCode)
                $("#fld_GMLogin").val("GM1"); //自己提交

            var SpecialCodeflag= "";
            $("#detailtable").find('input[Title="fld_SubjectCode"]').each(function (i, Etr) {

                if ($(this).val() == "7360000000") {
                    SpecialCodeflag = "1";
                    return;
                }
            });

            if (SpecialCodeflag == "1") {
                if (($("#SpecialTable").find("tr").length - 0) <= 0) {
                    alert("Please Input ENTERTAINMENT");
                    return false;
                }
            }
             return true;
        }
        function descriptionItemChange(obj) {
            $(obj).next().val($(obj).find("option:selected").text());
            $(obj).next().next().val($(obj).find("option:selected").val());
        }
        $(document).ready(function () {
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }
            getTotalRMB();
        });
        function ShowTime(obj, index) {
            var mintime = $(obj).parent().prev().find("input").val();
            var maxtime = $(obj).parent().next().find("input").val();
            var StartDate = "";
            var EndDate = "";
            if (index == 1 && maxtime != undefined) {
                EndDate = maxtime
            }
            if (mintime != undefined && index == 2) {
                StartDate = mintime;
            }
            if (index == 2) {
                WdatePicker({ dateFmt: 'yyyy-MM-dd', minDate: StartDate });
            }
            else {
                WdatePicker({ dateFmt: 'yyyy-MM-dd'});
            }
        }
        function beforeSave() {
            var summary = "Local Expense Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        function showPurchaseOrder() {
            var digStr = "dialogHeight:500px;dialogWidth:850px;"
            var employee = $("#fld_VendorCode").val();
            var ReturnValue = window.showModalDialog("./Purchase.aspx?employee=" + employee, null, digStr);
            if (ReturnValue != null) {
                var purchaseNo = eval("(" + ReturnValue + ")");
                // var havePapers1 = eval(havePapers);
                var value = $("#fld_PurchaseOrderNo").val();
                value += "," + purchaseNo[0].PurchaseNo;
                if (value.substr(0, 1) == ',')
                    value = value.substr(1);
                $("#fld_PurchaseOrderNo").val(value);
                var applicant = $("#Allpplicant").val();
                applicant += purchaseNo[1].Applicant;
                $("#Allpplicant").val(applicant);
            }
        }
        function project_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./ProjectList1.aspx", "", digStr);
            if (ReturnValue != null) {
                var Project = eval("(" + ReturnValue + ")");
                var ProjectValue = Project[0].ProjectValue;
                var ProjectDisplayValue = Project[1].Description;
                $(obj).val(ProjectValue + "-" + ProjectDisplayValue);
                $(obj).next().val(ProjectValue);
            }
        }
        function costcenter_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./CostCenter.aspx", "", digStr);
            if (ReturnValue != null) {
                var cosetcenterDetail = eval("(" + ReturnValue + ")");
                var CostCenter = cosetcenterDetail[0].cosetcenter;
                var Description = cosetcenterDetail[1].Description;
                $(obj).val(CostCenter + "-" + Description);
                $(obj).next().val(CostCenter);
            }
            $("#fld_Project").val("");
            if ($("#fld_CostCenterValue").val() == "50806200") {
                $("#fld_Project").val("EC919CA003");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="Local Expense Process" processprefix="FINLE" tablename="PROC_LocalExpense"
                    tablenamedetail = "PROC_LocalExpense_DT,PROC_LocalExpenseThird_DT" runat="server"  ></ui:userinfo>
                <asp:TextBox runat="server" ID="fld_Rate" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_CompareValue" value="1500" style="display:none;"></asp:TextBox>
                <asp:TextBox runat="server" ID="fld_DGM" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_GM" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_FinLogin" style="display:none;"></asp:TextBox>
                 <asp:TextBox runat="server" ID="fld_deptLogin" style="display:none;"></asp:TextBox>
            </div>
            <div class="row">
                <p style="font-weight:bold;">Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label">
                            <span style=" background:red;float:left">&nbsp;</span>
                            <p style="text-align:center">Employee</p>
                          </td>
                        
                        <td class="td-content">
                            <asp:TextBox runat="server" ID ="fld_VendorDisplay" onclick="employee_onclick(this)" Width="90%"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_VendorCode" style="display:none"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_VendorName" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label"> <span style=" background:red;float:left">&nbsp;</span><p style="text-align:center">Cost Center</p></td>
                        <td class="td-content">
                            <asp:TextBox runat="server" ID="fld_CostCenterSubject" CssClass="validate[required]" Width="92%" onclick="costcenter_onclick(this)"></asp:TextBox>
                            <asp:TextBox runat="server" ID="fld_CostCenterValue" style="display:none;"></asp:TextBox>
                        </td>
                        <td class="td-label"><p style="text-align:center">Project</p></td>
                        <td class="td-content">
                            <asp:TextBox runat="server" ID="fld_Project" width="92%"></asp:TextBox>
                        </td>
                    </tr>
                        <tr>
                        <td class="td-label" colspan="2">
                        <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center">Purchase Request NO</p>
                        </td>
                        <td class="td-content" colspan="6">
                            <asp:TextBox runat="server" ID="fld_PurchaseOrderNo" Width="90%" style="background-color:White;" onfocus="this.blur()"></asp:TextBox>
                            <input type="button" value="..." class="btn" onclick="return showPurchaseOrder()" />

                        </td>
                    </tr>
                    </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="1%"><p style="text-align:center">NO.</p></th>
                        <%--<th width="20%"><p style="text-align:center">Date</p></th>--%>
                        <th width="23%"><p style="text-align:center">Description</p></th>
                        <th width="58%"><p style="text-align:center">Comments</p></th>
                        <th width="10%"><p style="text-align:center">RMB</p></th>
                        <th width="8%"></th>
                    </tr>
                
                    <tbody id="detailtable">
                    <asp:Repeater runat="server" ID="fld_detail_PROC_LocalExpense_DT" OnItemCommand="fld_detail_PROC_LocalExpense_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_LocalExpense_DT_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <th style="text-align:center" width="1%">
                                    <span style=" background:red;float:left">&nbsp;</span> 
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </th>
                                <td style="display:none;">
                                    <asp:TextBox ID="fld_Date" Text='<%# String.IsNullOrEmpty(Eval("Date").ToString())? "":DateTime.Parse(Eval("Date").ToString()).ToString("yyyy-MM-dd") %>' runat="server" Width="94%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="fld_SubjectName" Text='<%#Eval("SubjectName") %>' runat="server" onclick="subject_onclick(this)" Width="94%"></asp:TextBox>
                                     <asp:TextBox ID="fld_SubjectCode" Title="fld_SubjectCode" Text='<%#Eval("SubjectCode") %>' runat="server" style="display:none"></asp:TextBox>
                                    <asp:TextBox ID="fld_ItemValue" Text='<%#Eval("ItemValue") %>' runat="server" style="display:none"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="fld_Comments" runat="server" Text='<%#Eval("Comments")%>' CssClass="validate[required]"  MaxLength="50" Width="98%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="fld_RMB" Text='<%#Eval("RMB") %>'  onblur="getTotalRMB()" MaxLength="8" runat="server" CssClass="validate[required]" Width="82%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnDelete" runat="server" Text="delete" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tr>
                    <td colspan="2" style="text-align:center;">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn" CausesValidation="false" OnClick="btnAdd_Click" style="float:left" />
                    </td>
                    <td style="text-align:right">
                        <p style="float:right">Sub-total</p>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lb_RMB" Text="0.00" CssClass="moeny"></asp:Label>
                        <asp:TextBox runat="server" ID="fld_RMB" value="0.00" Width="82%" style="display:none"></asp:TextBox>
                    </td>
                    <td style="display:none;">
                        <asp:TextBox runat="server" ID="fld_USD" ></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                </table>



                  <p style="font-weight:bold;">ENTERTAINMENT (if space is insufficient,please attach a separate entertainment report)</p>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <th width="10%"><p style="text-align:center">Date</p></th>
                        <th width="23%"><p style="text-align:center">Amount</p></th>
                        <th width="36%"><p style="text-align:center">Guest Name & Company</p></th>
                        <th width="23%"><p style="text-align:center">Business Purpose</p></th>
                        <th width="10%"></th>
                    </tr>
                    <tbody id="SpecialTable">
                    <asp:Repeater runat="server" ID="fld_detail_PROC_LocalExpenseThird_DT" OnItemCommand="fld_detail_PROC_LocalExpenseThird_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_LocalExpenseThird_DT_ItemDataBound">
                        <ItemTemplate>
                        <tr>
                            <td>
                               <span style=" background:red;float:left">&nbsp;</span> 
                                <asp:TextBox runat="server" style="float:right" ID="fld_Date" CssClass="validate[required]" onfocus="ShowTime(this,'1');" Text='<%# String.IsNullOrEmpty(Eval("Date").ToString())? "":DateTime.Parse(Eval("Date").ToString()).ToString("yyyy-MM-dd") %>' Width="70%" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="fld_Amount" CssClass="validate[required]" Text='<%#Eval("Amount") %>' Width="92%" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="fld_GuestNameCompany" CssClass="validate[required]" Text='<%#Eval("GuestNameCompany") %>' Width="96%" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="fld_BusinessPurpose"   CssClass="validate[required]" Text='<%#Eval("BusinessPurpose") %>' Width="92%" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="delete" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                            </td>
                        </tr>
                        </ItemTemplate>
                     </asp:Repeater>
                     </tbody>
                     <tr>
                        <td colspan="9">
                            <asp:Button ID="btnAddThird" runat="server" Text="add" CssClass="btn" CausesValidation="false" OnClick="btnAddThird_Click"   />
                        </td>
                     </tr>
                </table>


                <table class="table table-condensed table-bordered">
                    <tr>
                        <td class="td-label" style="vertical-align:middle">
                            <p style="text-align:center">Remarks</p>
                        </td>
                        <td class="td-content" colspan="5">
                            <asp:TextBox runat="server" ID="fld_Remarks" Width="98%" MaxLength="100" Rows="3" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row"">
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


