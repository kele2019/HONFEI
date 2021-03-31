<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Presale.Process.PurchaseOrder.NewRequest" %>

<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="ui" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ApprovalHistory.ascx" TagName="ApprovalHistory" TagPrefix="ah" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/Attachments.ascx" TagName="Attachments" TagPrefix="attach" %>
<%@ Register Src="/Modules/Ultimus.UWF.Form.ProcessControl/ButtonList.ascx" TagName="ButtonList" TagPrefix="btn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Purchase Order Process</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <script type="text/javascript">
        function openITEMNO(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./Item.aspx", "", digStr);
            if (ReturnValue != null) {
                var purchaseNo = eval("(" + ReturnValue + ")");
                var ItemCode = purchaseNo[0].ItemCodeValue;
                var ItemName = purchaseNo[1].ItemNameValue;
                $(obj).val(ItemCode);
                $(obj).parent().next().children().val(ItemName);
            }
        }
        function project_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./ProjectList.aspx", "", digStr);
            if (ReturnValue != null) {
                var Project = eval("(" + ReturnValue + ")");
                var ProjectValue = Project[0].ProjectValue;
                var ProjectDisplay = Project[0].ProjectValue + "-" + Project[1].Description;
                $(obj).val(ProjectDisplay);
                $(obj).next().val(ProjectValue);
            }
        }
        function showPurchaseOrder() {
            var digStr = "dialogHeight:500px;dialogWidth:850px;"
            var ReturnValue = window.showModalDialog("./Purchase.aspx", null, digStr);
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
        function changetype(obj) {//写入type的值
            $(obj).next().val($(obj).val());
        }
        function changecode(obj) {
            var code = $(obj).val();
            $(obj).next().val($(obj).val());

            var st = new Array();
            st = code.split(";");
            for (var i = 0; i < st.length; i++) {
                $(obj).parent().parent().find("td:eq(11)").children().eq(2).val(st[1]);
            }
        }
        function checkcostcenter(obj) {
            var ITEMNO = $(obj).parent().parent().find("td:eq(1)").children().val();
            var costcenter = $(obj).val();
            if (ITEMNO.substr(0, 1) == "E" && costcenter == "") {
                alert("Must Fill in Costcenter");
                $(obj).focus();
                return false;
            }
        }
        function sumallcount() {
            var TotalValue = 0;
            var TaxAmount = 0;
            var TotalValueWithTax = 0;
            $("#detail").find("tr").each(function (i, Etr) {
                var qty = $(Etr).find("td:eq(8)").children().val() - 0;
                var unitprice = $(Etr).find("td:eq(9)").children().val() - 0;
                var taxcode = $(Etr).find("td:eq(11)").children().eq(1).val();
               
                if (taxcode != "") {
                    var st = taxcode.split(";");
                    for (var i = 0; i < st.length; i++) {
                        taxcode = st[0] - 0;

                    }

                    if (qty != "" && unitprice != "" && taxcode != "") {
                        TotalValue += unitprice * qty;
                        TotalValueWithTax += unitprice * qty * taxcode;
                    } 
                }
            });
            //TotalValueWithTax = TotalValue + TaxAmount;
            TaxAmount = TotalValueWithTax - TotalValue;


            $("#lbTotalValue").text(formatNumber(TotalValue.toFixed(2), 2, 1));
            $("#fld_TotalValue").val(TotalValue.toFixed(2));
            $("#lbTaxAmount").text(formatNumber(TaxAmount.toFixed(2), 2, 1));
            $("#fld_TaxAmount").val(TaxAmount.toFixed(2));
            $("#lbTotalValueWithTax").text(formatNumber(TotalValueWithTax.toFixed(2), 2, 1));
            $("#fld_TotalValueWithTax").val(TotalValueWithTax.toFixed(2));

        }
        function beforeSubmit() {
            if ($("#fld_Currency").val().replace(/(^\s*)|(\s*$)/g, "") == "RMB") {
                $("#fld_totalValueUSD").val((($("#fld_TotalValue").val() - 0) / ($("#fld_Rate").val() - 0)).toFixed(2));
            }
            if ($("#fld_Currency").val().replace(/(^\s*)|(\s*$)/g, "") == "USD") {
                $("#fld_totalValueUSD").val(($("#fld_TotalValue").val() - 0).toFixed(2));
            }
            var errorCount = 0;
            $("#detail").find("tr").each(function (i, Etr) {
                var ITEMNO = $(Etr).find("td:eq(1)").children().val();
                var facode = $(Etr).find("td:eq(5)").children().val();
                var costcenter = $(Etr).find("td:eq(12)").children().val();
                var project = $(Etr).find("td:eq(13)").children().val();
              
                if (ITEMNO.substr(0, 1) == "E" && costcenter == "") {
                    errorCount++;
                    alert("You have choose " + ITEMNO + ",so Must Fill in Costcenter");
                    return false;
                }
                if (ITEMNO.substr(0, 1) == "F" && facode != "") {
                    errorCount++;
                    alert("You have choose " + ITEMNO + ",so PRCODE have to null");
                    return false;
                }
                if ((ITEMNO.substr(0, 1) == "P" || ITEMNO.substr(0, 1) == "F") && costcenter != "") {
                    errorCount++;
                    alert("You have choose " + ITEMNO + ",so CostCenter have to null");
                    $(Etr).find("td:eq(12)").children().val("");
                    return false;
                }
                if (ITEMNO.substr(0, 1) == "P" && project == "") {
                    errorCount++;
                    alert("You have choose " + ITEMNO + ",so Must Fill in Project");
                    return false;
                }
                if (project != "") {
                    if (ITEMNO.substr(0, 1) == "E") {
                        if (project.substr(0, 1) != "E") {
                            errorCount++;
                            alert("You have choose " + ITEMNO + ",Project is wrong");
                            return false;
                        }
                    }
                    if (ITEMNO.substr(0, 1) == "P") {
                        if (project.substr(0, 1) != "C" ) {
                            errorCount++;
                            alert("You have choose " + ITEMNO + ",Project is wrong");
                            return false;
                        }
                    }
                    if (ITEMNO.substr(0, 1) == "F") {
                        if (project.substr(0, 2) != "CC" && project.substr(0, 2) != "EC") {
                            errorCount++;
                            alert("You have choose " + ITEMNO + ",Project is wrong");
                            return false;
                        }
                    }
                }
            });
            if (errorCount != 0) {
                return false;
            }
            var summary = "Purchase Order Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            return true;
        }
        function beforeSave() {
            var summary = "Purchase Order Process";
            $("#UserInfo1_fld_PROCESSSUMMARY").val(summary);
            $("#fld_TRSummary").val(summary);
            return true;
        }
        $(document).ready(function () {
            //     
            $("#buyer option:selected").text($("#fld_BUYER").val());
            $("#wareHouse option:selected").text($("#fld_WAREHOUSE").val());
            $("#detail").find("tr").each(function (i, Etr) {
                $(Etr).find(".items").val($(Etr).find(".items").next().val());
                $(Etr).find(".taxcode").val($(Etr).find(".taxcode").next().val());
            });
            //            var TotalValue = $("#fld_TotalValue").val();
            //            var TaxAmount = $("#fld_TaxAmount").val();
            //            var TotalValueWithTax = $("#fld_TotalValueWithTax").val();
            //            alert(TotalValueWithTax);
            //            alert(TotalValue);
            //            alert(TaxAmount);
            $("#currency").val($("#fld_Currency").val());
            sumallcount();
            if ($("#hdIncident").val() != "") {
                $("#ButtonList1_btnSubmit").val("Submit");
                $("#ButtonList1_btnBack").hide();
                $("#ButtonList1_btnReject").show();
            }
            if ($("#hdUrgeTask").val() == "Yes") {
                $("#ReturnBackTask").show();
            }

            $("#fld_Remark").keyup(function () {
                if ($("#fld_Remark").val().length > 254) {
                    $("#fld_Remark").val($("#fld_Remark").val().substr(0, 254));
                }
            });

        });
        function drop_onchange(obj) {
            $(obj).next().val($(obj).find("option:selected").text());
        }
        function supplier_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./Vendor.aspx", "", digStr);
            if (ReturnValue != null) {
                var vendor = eval("(" + ReturnValue + ")");
                var VendorCode = vendor[0].VendorCode;
                var VendorName = vendor[1].VendorName;
                var Currency = vendor[3].Currency;
                $("#fld_SUPPLIER").val(VendorCode + "-" + VendorName);
                $("#fld_CardCode").val(VendorCode);
                $("#fld_CardName").val(VendorName);
                $("#fld_Currency").val(Currency);
            }
        }
        function CostCenter_onclick(obj) {
            var digStr = "dialogHeight:500px;dialogWidth:850px;";
            var ReturnValue = window.showModalDialog("./CostCenter.aspx", "", digStr);
            if (ReturnValue != null) {
                var cosetcenterDetail = eval("(" + ReturnValue + ")");
                var CostCenter = cosetcenterDetail[0].cosetcenter;
                var Description = cosetcenterDetail[1].Description;
                $(obj).val(CostCenter);
                $(obj).parent().parent().find("td").eq(15).children().val(CostCenter + "-" + Description);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="myDiv" class="container">
            <div class="row">
                <ui:userinfo id="UserInfo1" processtitle="PURCHASE ORDER" processprefix="PURPO" tablename="PROC_PurchaseOrder" tablenamedetail="PROC_PurchaseOrder_DT" runat="server"  ></ui:userinfo>
            </div>
            <asp:TextBox runat="server" ID="fld_SupplierMLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_DGMLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_GMLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_DGM" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_GM" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_FMLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_PURMLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_deptManagerLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_SupplierLogin" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_CompareValue" value="5000.00" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_totalValueUSD" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="fld_Rate" style="display:none;"></asp:TextBox>
        <div class="row">
            <p style="font-weight:bold;">Request require（"<span style=" background:red;">&nbsp;</span>" must write） </p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <td class="td-label" style="vertical-align:middle;">
                        <span style=" background:red;  height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">SUPPLIER</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:TextBox runat="server" ID="fld_SUPPLIER" onfocus="this.blur()" CssClass="validate[required]" onclick="supplier_onclick(this)" Width="94%"></asp:TextBox>
                        <asp:TextBox runat="server" ID="fld_CardCode" style="display:none;"></asp:TextBox>
                        <asp:TextBox runat="server" ID="fld_CardName" style="display:none;"></asp:TextBox>
                    </td>
                     <td class="td-label"> 
                    <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">CURRENCY</p>
                    </td>
                    <td class="td-content" colspan="3"> 
                        <asp:TextBox ID="fld_Currency" runat="server"   style="Width:95%"></asp:TextBox>
                    </td>
                </tr>
             
                <tr>
                    <td class="td-label" style="vertical-align:middle;"> 
                        <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">BUYER</p>
                    </td>
                    <td class="td-content" colspan="3">
                        <asp:DropDownList runat="server" ID="buyer"  onchange="drop_onchange(this)">
                            <asp:ListItem Selected="True">Buyer1</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox runat="server"  ID="fld_BUYER" value="Buyer1" style=" display:none;" Width="94%"></asp:TextBox>
                    </td>
                    <td class="td-label" style="vertical-align:middle;"> 
                        <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">WAREHOUSE</p>
                    </td>
                    <td class="td-content" colspan="3">
                         <asp:DropDownList runat="server" ID="wareHouse"  onchange="drop_onchange(this)">
                            <asp:ListItem Selected="True">901</asp:ListItem>
                         </asp:DropDownList>
                        <asp:TextBox runat="server"  ID="fld_WAREHOUSE" value="901" style="display:none;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                    <span style=" background:red;height:30px; float:left;">&nbsp;</span>
                        <p style="text-align:center">PUR. REQUEST NO</p>
                    </td>
                    <td class="td-content" colspan="7">
                        
                        <asp:TextBox runat="server" ID="fld_PurchaseOrderNo" CssClass="validate[required]" Width="85%" style="background-color:White;" onfocus="this.blur()"></asp:TextBox>
                        <input type="button" value="..." class="btn" onclick="return showPurchaseOrder()" />

                    </td>
                </tr>
                <tr>
                <td class="td-label">PM Reveiw</td>
                 <td  colspan="3" class="td-content">
                 <asp:DropDownList runat="server" ID="DropPMUser" onchange="changetype(this)">
                 <asp:ListItem>selected</asp:ListItem>
                 </asp:DropDownList>
                 <asp:TextBox runat="server" ID="fld_PMUser" style="display:none"></asp:TextBox>
                 </td>
                  <td class="td-label">Qulity Reveiw</td>
                   <td colspan="3" class="td-content">
                    <asp:DropDownList runat="server" ID="DropQulityUser" onchange="changetype(this)">
                 <asp:ListItem>selected</asp:ListItem>
                 </asp:DropDownList>
                 <asp:TextBox runat="server" ID="fld_QulityUser" style="display:none"></asp:TextBox>
                   </td>
                </tr>
            </table>
            <p style="font-weight:bold">Request require</p>
            <table class="table table-condensed table-bordered">
                <tr>
                    <th width="0.3%">NO.</th>
                    <th width="6.8%">ITEMNO.</th>
                    <th width="7.6%">ITEMDESP</th>
                    <th width="7.6%">DETAIDESP</th>
                    <th width="7.6">MFRNO.</th>
                    <th width="7.1%">PRCODE</th>
                    <th width="10.1%">DEL.DATE</th>
                    <th width="10.1%">REO.DATE</th>
                    <th width="7%">QTY</th>
                    <th width="7%">UNIT PRICE</th>
                    <th width="4%">UNIT</th>
                    <th width="7.6%">TAXCODE</th>
                    <th width="5%">COSTCENTER</th>
                    <th width="6%">PROJECT</th>
                    <th width="6.2%"></th>
                </tr>
                <tbody id="detail">
                    <asp:Repeater runat="server" ID="fld_detail_PROC_PurchaseOrder_DT" OnItemCommand="fld_detail_PROC_PurchaseOrder_DT_ItemCommand" OnItemDataBound="fld_detail_PROC_PurchaseOrder_DT_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center">
                                    <asp:TextBox ID="fld_FORMID" Text='<%#Eval("FORMID") %>' runat="server" Style="display: none" ></asp:TextBox>
                                    <asp:Label ID="fld_ROWID" Text='<%# Container.ItemIndex+1%>' runat="server"></asp:Label>
                                </td>
                                <td>
                                    <%--<asp:DropDownList runat="server" ID="Items" Class="items" Width="99%" onchange="changetype(this)"></asp:DropDownList>--%>
                                    <asp:TextBox ID="fld_ITEMNO" runat="server" Text='<%#Eval("ITEMNO") %>' onfocus="this.blur()" CssClass="validate[required]" onclick="openITEMNO(this)" Width="70%"></asp:TextBox>
                                </td>
                                  <td>
                                    <asp:TextBox ID="fld_ITEMDESCRIPTON" runat="server"  CssClass="validate[required]" Text='<%#Eval("ITEMDESCRIPTON") %>' Width="80%"></asp:TextBox>
                                </td>
                               <td>
                                    <asp:TextBox ID="fld_DETAILDESCRIPTION" runat="server"  CssClass="validate[required]" Text='<%#Eval("DETAILDESCRIPTION") %>' Width="80%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="fld_MFRNO" runat="server" MaxLength="17" Text='<%#Eval("MFRNO") %>' Width="74%"></asp:TextBox>
                                </td>
                                  <td>
                                    <asp:TextBox ID="fld_PRCFACODE" runat="server" Text='<%#Eval("PRCFACODE") %>' Width="80%"></asp:TextBox>
                               </td>
                                <td>
                                    <asp:TextBox ID="fld_DELDATE" runat="server"  CssClass="validate[required]" Text='<%# String.IsNullOrEmpty(Eval("DELDATE").ToString())? "":DateTime.Parse(Eval("DelDate").ToString()).ToString("yyyy-MM-dd") %>' onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})" Width="84%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="fld_REODATE" runat="server"  CssClass="validate[required]" Text='<%# String.IsNullOrEmpty(Eval("REODATE").ToString())? "":DateTime.Parse(Eval("ReoDate").ToString()).ToString("yyyy-MM-dd") %>' onclick="WdatePicker({readOnly:false,dateFmt: 'yyyy-MM-dd'})" Width="84%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="fld_QTY" runat="server"  CssClass="validate[required]" Text='<%#Eval("QTY") %>' Width="74%" onclick="sumallcount()"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="fld_UNITPRICE" runat="server"  CssClass="validate[required]" Text='<%#Eval("UNITPRICE") %>' Width="74%" onclick="sumallcount()"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="fld_UNIT" runat="server"  CssClass="validate[required]" Text='<%#Eval("UNIT") %>' Width="40%"></asp:TextBox>
                                </td>
                                <td>
                               <asp:DropDownList runat="server" class="taxcode"  ID="DropTAXcode"  Width="99%" onchange="changecode(this);sumallcount()"></asp:DropDownList>
                               <asp:TextBox ID="fld_taxcodeYc" runat="server" Text='<%#Eval("taxcodeYc") %>' style="display:none" ></asp:TextBox>
                               <asp:TextBox ID="fld_TAXCODE" runat="server" Text='<%#Eval("TAXCODE") %>' Width="84%" style="display:none" ></asp:TextBox>
                                </td>
                                  <td>
                                    <asp:TextBox ID="fld_COSTCENTER" runat="server" Text='<%#Eval("COSTCENTER") %>' Width="84%" onclick="CostCenter_onclick(this)" ></asp:TextBox>
                                </td>
                                  <td>
                                    <asp:TextBox ID="fld_ProjectDisplay" runat="server" Text='<%#Eval("ProjectDisplay")%>' Width="74%" onclick="project_onclick(this)"></asp:TextBox>
                                    <asp:TextBox ID="fld_PROJECT" runat="server" Text='<%#Eval("PROJECT") %>' style="display:none;"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnDelete" runat="server" Text="Del" CssClass="btn" CommandName="del" ClientIDMode="Static" OnClientClick="return confirm('Confirm Del？')" />
                                </td>
                                <td style="display:none;">
                                    <asp:TextBox runat="server" ID="fld_COSTCENTERDISPLAY"  Text='<%#Eval("COSTCENTERDISPLAY") %>'></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                    <tr>
                        <td colspan="15">
                            <asp:Button ID="btnAdd" runat="server" Text="add" CssClass="btn" CausesValidation="false" OnClick="btnAdd_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="12">
                            <p style="text-align:right;">TOTAL VALUE EXCLUDING TAX</p>
                        </td>
                        <td colspan="3">
                         <asp:Label runat="server" ID="lbTotalValue" CssClass="money"></asp:Label>
                            <asp:TextBox ID="fld_TotalValue" runat="server" style="display:none" width="94%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="12">
                            <p style="text-align:right;">TAX AMOUNT</p>
                        </td>
                        <td colspan="3">
                        <asp:Label runat="server" ID="lbTaxAmount" CssClass="money"></asp:Label>
                            <asp:TextBox ID="fld_TaxAmount" runat="server" style="display:none" width="94%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="12">
                            <p style="text-align:right;">TOTAL VALUE WITH TAX</p>
                        </td>
                        <td colspan="3">
                        <asp:Label runat="server" ID="lbTotalValueWithTax" CssClass="money"></asp:Label>
                            <asp:TextBox ID="fld_TotalValueWithTax" runat="server" style="display:none"  width="94%"></asp:TextBox>
                        </td>
                    </tr>
            </table>
            <table class="table table-condensed table-bordered">
                <tr>
                        <td class="td-label" style="vertical-align:middle">
                            <span style="height:30px; float:left;">&nbsp;</span>
                            <p style="text-align:center;">Remarks</p>
                        </td>
                        <td class="td-content" colspan="7" >
                            <asp:TextBox runat="server" ID="fld_Remark" TextMode="MultiLine" Rows="3" Width="98%"></asp:TextBox>
                        </td>
                </tr>
            </table>
        </div>
        <asp:TextBox runat="server" ID="Allpplicant" style="display:none;"></asp:TextBox>
        <asp:TextBox runat="server" ID="fld_ApprovalArr_DeptApplicant" style="display:none;"></asp:TextBox>
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


