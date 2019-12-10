<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachmentList.aspx.cs" Inherits="Ultimus.UWF.Common.AttachmentList" %>
<%@ Import Namespace="Ultimus.UWF.Common.Logic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
         <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/common.js"></script>
         <script type="text/javascript" src="<%=WebUtil.GetRootPath()%>/Assets/js/uploadify/jquery.uploadify.js"></script>
         <link rel="stylesheet" type="text/css" href="/Assets/js/uploadify/uploadify.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="queue"></div>
		<input id="file_upload" name="file_upload" type="file" multiple="true">
    </div>
    </form>

    <script type="text/javascript">
		$(function() {
			$('#file_upload').uploadify({
				'formData'     : {
					'timestamp' : '',
					'token'     : ''
				},
				'swf'      : 'uploadify.swf',
				'uploader' : 'uploadify.php'
			});
		});
	</script>
</body>
</html>
