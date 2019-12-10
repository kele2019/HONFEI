<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoSearchForm.aspx.cs" Inherits="Ultimus.UWF.Form.AutoSearchForm"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="<%=Ultimus.UWF.Common.Logic.WebUtil.GetRootPath()%>/Assets/js/common.js"></script>
</head>
<body>


    <form id="form1" runat="server">
    <div>
    name:<input type="text" id="name" onmousedown="selectLang(this);" onkeyup="selectLang(this);" />
    <br />
    code:<input type="text" id="code" />
    </div>

    <script type="text/javascript">
        function selectLang(obj) {
            var selectValue = $(obj).val();

            var xml = "<SearchEntity>"
                + "<SearchType>1</SearchType>"
                + "<ConnectionString>BizDB</ConnectionString>"
                + "<TableName>COM_LANGUAGE</TableName>"
                + "<Columns>"

                + "<ColumnEntity>"
                + "<Title>名称</Title>"
                + "<Column>ZH_CN</Column>"
                + "</ColumnEntity>"

                + "<ColumnEntity>"
                + "<Title>编号</Title>"
                + "<Column>NAME</Column>"
                + "</ColumnEntity>"

                + "<ColumnEntity>"
                + "<Title>描述</Title>"
                + "<Column>DESCRIPTION</Column>"
                + "</ColumnEntity>"

                + "</Columns>"
                + "<StrWhere></StrWhere>"
                + "<OrderBy></OrderBy>"
                + "<SearchCount>10</SearchCount>"
                + "</SearchEntity>";

            $.begin(obj, xml, function (data) {
                if (data != null) {
                    $("#name").val(data.ZH_CN);
                    $("#code").val(data.NAME);
                } else {
                    $(obj).val(selectValue);
                }
            });
        }
    </script>

    </form>
</body>
</html>
