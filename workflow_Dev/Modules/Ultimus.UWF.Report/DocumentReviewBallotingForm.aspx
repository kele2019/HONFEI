<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentReviewBallotingForm.aspx.cs" Inherits="Ultimus.UWF.Report.DocumentReviewBallotingForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible"  content="IE=EmulateIE8" />
    <title>Document Review and Approve Form</title>
    <script type="text/javascript" src="/Assets/js/common.js"></script>
    <style media="print" type="text/css"> 
    </style> 
    <script type="text/javascript">
        function Print_click() {
            window.print();
        }
        function window_close() {
            window.close();
        }
        $(document).ready(function () {
//            $("#DocumentNameDisplay").text($("#DocumentName").text());
            if ($("#deptMan").text() == "yes") {
                $("#deptManO").show();
                $("#deptManAgree").show();
                $("#deptManImg").show();
            }
            else {
                $("#deptManNO").show();
            }
            if ($("#deptQM").text() == "yes") {
                $("#deptQMO").show();
                $("#deptQMAgree").show();
                $("#deptQMImg").show();
            }
            else {
                $("#deptQMNO").show();
            }
            if ($("#deptEng").text() == "yes") {
                $("#deptEngO").show();
                $("#deptEngAgree").show();
                $("#deptEngImg").show();
            }
            else {
                $("#deptEngNO").show();
            }
            if ($("#deptPM").text() == "yes") {
                $("#deptPMO").show();
                $("#deptPMAgree").show();
                $("#deptPMImg").show();
            }
            else {
                $("#deptPMNO").show();
            }
            if ($("#deptOP").text() == "yes") {
                $("#deptOPO").show();
                $("#deptOPAgree").show();
                $("#deptOPImg").show();
            }
            else {
                $("#deptOPNO").show();
            }
            if ($("#deptAdmin").text() == "yes") {
                $("#deptAdminO").show();
                $("#deptAdminAgree").show();
                $("#deptAdminImg").show();
            }
            else {
                $("#deptAdminNO").show();
            }
            if ($("#deptFin").text() == "yes") {
                $("#deptFinO").show();
                $("#deptFinAgree").show();
                $("#detpFinImg").show();
            }
            else {
                $("#deptFinNO").show();
            }
            if ($("#deptHR").text() == "yes") {
                $("#deptHRO").show();
                $("#deptHRAgree").show();
                $("#deptHRImg").show();
            }
            else {
                $("#deptHRNO").show();
            }
            if ($("#deptIT").text() == "yes") {
                $("#deptITO").show();
                $("#deptITAgree").show();
                $("#deptITImg").show();
            }
            else {
                $("#deptITNO").show();
            }
            if ($("#deptPUR").text() == "yes") {
                $("#deptPURO").show();
                $("#deptPURAgree").show();
                $("#deptPURImg").show();
            }
            else {
                $("#deptPURNO").show();
            }
            if ($("#deptGM").text() == "yes") {
                $("#deptGMO").show();
                $("#DeptGMAgree").show();
                $("#DeptGMImg").show();
            }
            else {
                $("#DeptGMNo").show();
            }
        });
    </script>
</head>
<body>
    <!--class="container" style="width:90%;" -->
    <form id="form1" runat="server" style="text-align:center;width:99%;">
    <div id="myDiv" class="container">
            <div class="row">
            
          <table align="center" style="width:100%;margin-top:0.5%;margin-bottom:0.5%;"  >
            <tr>
                <td >
                    <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/img/logo.gif" Height="30px" Visible="true" />
                </td>
                <td style="text-align: center; padding-top: 10px; " width="90%">
                    <strong>
                        <asp:Label ID="lblProcessName" Font-Size="20px" runat="server" Text="Document Review and Approve Form"></asp:Label>
                    </strong>
                </td>
                <td align="right" style="padding-left:20;" width="250">
                    <asp:Literal ID="barcode" runat="server" Visible="false" ></asp:Literal>
                </td>
            </tr>
          </table>
                <hr />
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td width="25%">
                         <p style="text-align:center" >Document Name</p>
                        </td>
                        <td width="25%">
                         <p style="text-align:center" >Document No.</p>
                        </td>
                        <td  width="25%">
                         <p style="text-align:center">Document Owner</p>
                        </td>
                        <td  width="25%">
                         <p style="text-align:center">Status</p>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="DocumentName"  ></asp:Label>
                            <%--<label runat="server" id="DocumentNameDisplay" ></label>--%>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="DocumentNo" ></asp:Label>
                        </td>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="DocumentOwner" ></asp:Label>
                        </td>
                       <td style="text-align:center">
                            <asp:Label runat="server" ID="Status" ></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td>
                            Description of Document:<span><asp:Label runat="server" ID="DocDescription" ></asp:Label></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Major Changes(if applicable):<span><asp:Label runat="server" ID="MajorChange" ></asp:Label></span>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered">
                    <tr>
                        <td rowspan="2" colspan="2" width="20%">
                         <p style="text-align:center">Review Stankeholder<br/>(Make"O" in front of Function if<br />request ballot; if note, make"×")</p>
                        </td>
                        <td colspan="2" width="15%">
                         <p style="text-align:center">Result of Review<br/>("√" if applicable)</p>
                        </td>
                        <td rowspan="2" style="vertical-align:middle;width:10%">
                         <p style="text-align:center;">Signature<br/>Name</p>
                        </td>
                        <td rowspan="2" style="vertical-align:middle;width:10%">
                         <p style="text-align:center">Approvel Date</p>
                        </td>
                        <td rowspan="2" style="vertical-align:middle;width:35%">
                         <p style="text-align:center">Comments</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                         <p style="text-align:center;width:7.5%">Agree</p>
                        </td>
                        <td>
                         <p style="text-align:center;width:7.5%">Disagree</p>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle;width:3.5%">
                         <p style="text-align:center;display:none;" id="deptManO">O</p>
                         <p style="text-align:center;display:none;" id="deptManNO">×</p>
                        </td>
                        <td style="vertical-align:middle">
                         <p style="text-align:left">Regulatory and Export<br />Compliance</p>
                        </td>
                        <td style="vertical-align:middle">
                         <p style="text-align:center;display:none;" id="deptManAgree">√</p>
                        </td>
                        <td style="vertical-align:middle">
                         <p style="text-align:center;display:none;" id="deptManDisagree">√</p>
                        </td>
                        <td>
                         <asp:Label runat="server" ID="deptMan" style="display:none;"></asp:Label>
                         <asp:Image runat="server" ID="deptManImg"  style="display:none;width:80px;height:50px;" />
                        </td>
                        <td style="vertical-align:middle">
                         <asp:Label runat="server" ID="deptManSignDate" ></asp:Label>
                        </td>
                        <td style="vertical-align:middle">
                         <asp:Label runat="server" ID="deptManComments" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptQMO">O</p>
                         <p style="text-align:center;display:none;" id="deptQMNO">×</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:left">Quality</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptQMAgree">√</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptQMDisageree">√</p>
                        </td>
                        <td>
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptQM" style="display:none;"></asp:Label>
                         <asp:Image runat="server" ID="deptQMImg" style="display:none;width:80px;height:50px;"/>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptQMSignDate" ></asp:Label>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptQMComments"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptEngO">O</p>
                         <p style="text-align:center;display:none;" id="deptEngNO">×</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:left">Engineering</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptEngAgree">√</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptEngDisAgree">√</p>
                        </td>
                        <td>
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptEng" style="display:none;"></asp:Label>
                         <asp:Image runat="server" ID="deptEngImg" style="display:none;width:80px;height:50px;"/>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptEngSignDate" ></asp:Label>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptEngComments" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptPMO">O</p>
                         <p style="text-align:center;display:none;" id="deptPMNO">×</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:left">Program Management</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptPMAgree">√</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptPMDisagree">√</p>
                        </td>
                        <td>
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptPM" style="display:none;"></asp:Label>
                         <asp:Image runat="server" ID="deptPMImg" style="display:none;width:80px;height:50px;"/>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptPMSignDate" ></asp:Label>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptPMComments" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptOPO">O</p>
                         <p style="text-align:center;display:none;" id="deptOPNO">×</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:left">Operation/Production</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptOPAgree">√</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptOPDisagree">√</p>
                        </td>
                        <td>
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptOP" style="display:none;"></asp:Label>
                         <asp:Image runat="server" ID="deptOPImg" style="display:none;width:80px;height:50px;"/>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptOPSignDate" ></asp:Label>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptOPComments" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptAdminO">O</p>
                         <p style="text-align:center;display:none;" id="deptAdminNO">×</p>
                        </td>
                        <td style="vertical-align:middle">
                         <p style="text-align:left">Health,Satfty &<br />Environmental</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptAdminAgree">√</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptAdminDisagree">√</p>
                        </td>
                        <td>
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptAdmin" style="display:none;"></asp:Label>
                         <asp:Image runat="server" ID="deptAdminImg" style="display:none;width:80px;height:50px;"/>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptAdminSignDate" ></asp:Label>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptAdminComments" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptFinO">O</p>
                         <p style="text-align:center;display:none;" id="deptFinNO">×</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:left">Finance</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptFinAgree">√</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptFinDisagree">√</p>
                        </td>
                        <td>
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptFin" style="display:none;"></asp:Label>
                         <asp:Image runat="server" ID="detpFinImg" style="display:none;width:80px;height:50px;"/>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptFinSignDate" ></asp:Label>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptFinComments" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptHRO">O</p>
                         <p style="text-align:center;display:none;" id="deptHRNO">×</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:left">Human Resource</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptHRAgree">√</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptHRDisagree">√</p>
                        </td>
                        <td>
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptHR" style="display:none;"></asp:Label>
                         <asp:Image runat="server" ID="deptHRImg" style="display:none;width:80px;height:50px;"/>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptHRSignDate" ></asp:Label>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptHRComments" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptITO">O</p>
                         <p style="text-align:center;display:none;" id="deptITNO">×</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:left">IT</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptITAgree">√</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptITDisagree">√</p>
                        </td>
                        <td>
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptIT" style="display:none;"></asp:Label>
                         <asp:Image runat="server" ID="deptITImg" style="display:none;width:80px;height:50px;"/>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptITSignDate"></asp:Label>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptITComments" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptPURO">O</p>
                         <p style="text-align:center;display:none;" id="deptPURNO">×</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:left">Material Management</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptPURAgree">√</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptPUTDisagree">√</p>
                        </td>
                        <td>
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptPUR" style="display:none;"></asp:Label>
                         <asp:Image runat="server" ID="deptPURImg" style="display:none;width:80px;height:50px;" />
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptPURSignDate" ></asp:Label>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptPURComments" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptGMO">O</p>
                         <p style="text-align:center;display:none;" id="DeptGMNo">×</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:left">GM</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="DeptGMAgree">√</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;" id="deptGMDisagree">√</p>
                        </td>
                        <td>
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptGM" style="display:none;"></asp:Label>
                         <asp:Image runat="server" ID="DeptGMImg" style="display:none;width:80px;height:50px;" />
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptGMSignDate" ></asp:Label>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="deptGMComments" ></asp:Label>
                        </td>
                    </tr>



                    <tr>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;">O</p>
                         <p style="text-align:center;display:none;">×</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:left">Others:</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;">√</p>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <p style="text-align:center;display:none;">√</p>
                        </td>
                        <td>
                         <span style="height:30px; float:left;">&nbsp;</span>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="Label19" ></asp:Label>
                        </td>
                        <td style="vertical-align:middle">
                         <span style="height:30px; float:left;">&nbsp;</span>
                         <asp:Label runat="server" ID="Label20" ></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table table-condensed table-bordered" style="width:100%">
                    <tr>
                        <td>
                            <p>Follow-Up Action:</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="FollowUPAction"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>Name With Signature & Date:</p>
                            <asp:Image runat="server" ID="ApplierImg" style="width:80px;height:40px;"/>
                            <asp:Label runat="server" ID="CompleteDate" style="text-align:left"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="text-align:center" class="noprint">
            <input type="button" class="btn" value="Close" onclick="window_close()" />
            <input type="button" class="btn" value="Print" onclick="Print_click()" />
            </div>  
        </div>
    </form>
</body>
</html>

