using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyLib;
using System.Data;
using Ultimus.UWF.Common.Interface;

namespace Ultimus.UWF.Common
{
    public partial class ModuleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt= DataAccess.Instance("BizDB").ExecuteDataTable("select * from COM_MODULE where module='UltimusP1'");
            DataRow row = dt.Rows[0];
            string type =ConvertUtil.ToString( row["MODULETYPE"]);
            Object obj = ReflectUtil.GetType(type);
            if (obj is IModule)
            {
                IModule module = obj as IModule;
                module.Install();
            }
        }
    }
}