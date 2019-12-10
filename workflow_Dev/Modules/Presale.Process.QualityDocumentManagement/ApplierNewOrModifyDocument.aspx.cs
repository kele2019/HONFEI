using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Presale.Process.QualityDocumentManagement
{
	public partial class ApplierNewDocument : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{


		}

		//protected void downLoad_Click(object sender, EventArgs e)
		//{
		//    string fileName = read_Docname.Text;//客户端保存的文件名
		//    string filePath = read_documentUrl.Text;// Server.MapPath("http://moss.ptcent.com/CompanyDocument/111/b.txt ");//路径
		//    FileInfo fileInfo = new FileInfo(filePath);
		//    Response.Clear();
		//    Response.ClearContent();
		//    Response.ClearHeaders();
		//    Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
		//    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
		//    Response.AddHeader("Content-Transfer-Encoding", "binary");
		//    Response.ContentType = "application/octet-stream";
		//    Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
		//    Response.WriteFile(fileInfo.FullName);
		//    Response.Flush();
		//    Response.End();
		//}
	}
}