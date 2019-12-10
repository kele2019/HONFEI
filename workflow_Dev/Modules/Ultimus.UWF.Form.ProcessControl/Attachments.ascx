<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Attachments.ascx.cs"
    Inherits="Ultimus.UWF.Form.ProcessControl.Attachments" %>
<script type="text/javascript" language="javascript">
    $().ready(function () {
        $("#fileinfo td").each(function () {
            $(this).css("text-align", "center");
        });
    })
</script>
<p style="font-weight:bold;" ><asp:Label ID="lblTitle" runat="server" Text="附件"></asp:Label></p>
<table class="table table-condensed table-bordered">
    
    <tr id="uploadrow" runat="server">
        <td>
            <asp:FileUpload ID="FilePath" runat="server" CssClass="btn"  />
            <!--<asp:Label ID="Label1" runat="server" Text="描述：" CssClass="strong" ></asp:Label> -->
            <asp:TextBox ID="FileComments" runat="server" Style="margin: 0; display: none;"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Upload" CssClass="btn" OnClientClick="return checkfile()"
                OnClick="Button1_Click" />
            <script type="text/javascript" language="javascript">
                function CheckFile() {
                    if ($("#<%= FilePath.ClientID %>").val() == "") {
                        alert("<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.RequireUpload%>");
                        return false;
                    }
                   
                    return true;
                }

                $().ready(function () {
                    if ($("#Attachments1_txtReadonly").val() == "1") {
                        $("#Attachments1_actionRow").hide();
                    }
                });
                var  browserCfg = {};  
                var ua = window.navigator.userAgent;  
                if (ua.indexOf("MSIE")>=1){  
                    browserCfg.ie = true;  
                }else if(ua.indexOf("Firefox")>=1){  
                    browserCfg.firefox = true;  
                }else if(ua.indexOf("Chrome")>=1){  
                    browserCfg.chrome = true;  
                }  
                function checkfile(){
                   var filename = $("#<%= FilePath.ClientID %>").val();
                 if(filename =='')     {
                     alert("<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.RequireUpload%>");
                     return false;
                 }
                 var extension = new String(filename.substring(filename.lastIndexOf(".") + 1, filename.length)).toLowerCase();
                 if (extension == "jpg" || extension == "gif" || extension == "jpeg" || extension == "png" || extension == "bmp" || extension == "doc" || extension == "docx" || extension == "xls" || extension == "xlsx" || extension == "ppt" || extension == "pptx" || extension == "pdf" || extension == "txt")
                 { }
                 else {
                     alert('File format is not supported!');
                     return false;
                 }
//                try {         
//                     var fso,f,fname,fsize;
//                     var flength=4000; 
//                     fso=new ActiveXObject("Scripting.FileSystemObject");   
//                     f=fso.GetFile(filename);
//                     fname=fso.GetFileName(filename);
//                     fsize=f.Size;
//                     fsize=fsize/1024;
//                     if(fsize>flength){
//                         // alert("上传的文件到小为："+fsize+"kb,\n超过最大限度"+flength+"kb,不允许上传 ");
//                         alert("文件太大，请上传小于4MB文件/File is too large, please upload less than 4MB file ");
//                     return false;
//                     }
//                 else {
//                     // alert("允许上传，文件大小为：" + fsize + "kb");
//                     return true;
//                     }        
//                     }
//                      catch(e) {
//                          alert(e + "\n 跳出此消息框，是由于你的activex控件没有设置好,\n" + "你可以在浏览器菜单栏上依次选择\n" + "工具->internet选项->\"安全\"选项卡->自定义级别,\n" + "打开\"安全设置\"对话框，把\"对没有标记为安全的\n" + "ActiveX控件进行初始化和脚本运行\"，改为\"启动\"即可");
//                            }
                       // return false;
             }
             function changecolor(obj) {
                 $(obj).attr("style", "color:#f00");
             }
            </script>
        </td>
    </tr>
    <tr>
        <td>
            <table class="table table-condensed table-bordered">
                <tr>
                    <th>
                        <asp:Label ID="Label2" runat="server" Text="No."></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="Label3" runat="server" Text="FileName"><%--<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.FileName%>--%></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="Label8" runat="server" Text="StepName"><%--<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.StepName%>--%></asp:Label>
                    </th>
                    <th style="display: none;">
                        <asp:Label ID="Label4" runat="server" Text="Describe"><%=Ultimus.UWF.Form.ProcessControl.Resources.lang.Description%></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="Label5" runat="server" Text="CreateName"><%--<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.CreateBy%>--%></asp:Label>
                    </th>
                    <%--<th>
                        <asp:Label ID="Label6" runat="server" Text="创建时间"></asp:Label>
                    </th>--%>
                    <th id="actionRow" runat="server" visible='<%# ReadOnly %>'>
                        <asp:Label ID="Label7" runat="server" Text="Operator"><%--<%=Ultimus.UWF.Form.ProcessControl.Resources.lang.Operation%>--%></asp:Label>
                    </th>
                </tr>
                <tbody id="fileinfo">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton2" OnClientClick="changecolor(this)" runat="server" CommandArgument='<%# Eval("NEWNAME") %>'
                                        CommandName="Download"><%# Eval("FileName")%></asp:LinkButton>
                                </td>
                                <td>
                                    <%# Eval("STEPNAME")%>
                                </td>
                                <td style="display: none;">
                                    <%# Eval("Comments")%>
                                </td>
                                <td>
                                    <%# Eval("CREATEBY")%> 
                                </td>
                                <%--<td>
                                    <%# Eval("CreateDate")%>
                                </td>--%>
                                <td runat="server" visible='<%# ReadOnly %>'>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Visible='<%# ReadOnly %>' OnClientClick="return confirm('Confirm Del?');"
                                        CommandArgument='<%# Eval("NEWNAME") %>' CommandName="Delete"><%=Ultimus.UWF.Form.ProcessControl.Resources.lang.Delete%></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </td>
    </tr>
</table>
<div style="display: none;">
    <asp:TextBox ID="txtMust" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtReadonly" runat="server"></asp:TextBox>
</div>
